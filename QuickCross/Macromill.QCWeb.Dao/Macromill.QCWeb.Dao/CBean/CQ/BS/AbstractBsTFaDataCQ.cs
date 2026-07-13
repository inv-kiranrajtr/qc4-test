
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
    public abstract class AbstractBsTFaDataCQ : AbstractConditionQuery {

        public AbstractBsTFaDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public override String getTableDbName() { return "T_FA_DATA"; }
        public override String getTableSqlName() { return "T_FA_DATA"; }

        public void SetSampleId_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSampleId_Equal(fRES(v));
        }
        protected void DoSetSampleId_Equal(String v) { regSampleId(CK_EQ, v); }
        public void SetSampleId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetSampleId_NotEqual(fRES(v));
        }
        protected void DoSetSampleId_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_NES, v);
        }
        public void SetSampleId_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_GT, fRES(v));
        }
        public void SetSampleId_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_LT, fRES(v));
        }
        public void SetSampleId_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_GE, fRES(v));
        }
        public void SetSampleId_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_LE, fRES(v));
        }
        public void SetSampleId_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueSampleId(), "SAMPLE_ID");
        }
        public void SetSampleId_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueSampleId(), "SAMPLE_ID");
        }
        public void SetSampleId_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetSampleId_LikeSearch(v, cLSOP());
        }
        public void SetSampleId_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueSampleId(), "SAMPLE_ID", option);
        }
        public void SetSampleId_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueSampleId(), "SAMPLE_ID", option);
        }
        public void SetSampleId_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_ISN, DUMMY_OBJECT);
        }
        public void SetSampleId_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regSampleId(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regSampleId(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueSampleId(), "SAMPLE_ID");
        }
        protected abstract ConditionValue getCValueSampleId();

        public void SetFa001_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa001_Equal(fRES(v));
        }
        protected void DoSetFa001_Equal(String v) { regFa001(CK_EQ, v); }
        public void SetFa001_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa001_NotEqual(fRES(v));
        }
        protected void DoSetFa001_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_NES, v);
        }
        public void SetFa001_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_GT, fRES(v));
        }
        public void SetFa001_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_LT, fRES(v));
        }
        public void SetFa001_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_GE, fRES(v));
        }
        public void SetFa001_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_LE, fRES(v));
        }
        public void SetFa001_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa001(), "FA001");
        }
        public void SetFa001_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa001(), "FA001");
        }
        public void SetFa001_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa001_LikeSearch(v, cLSOP());
        }
        public void SetFa001_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa001(), "FA001", option);
        }
        public void SetFa001_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa001(), "FA001", option);
        }
        public void SetFa001_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa001_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa001(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa001(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa001(), "FA001");
        }
        protected abstract ConditionValue getCValueFa001();

        public void SetFa002_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa002_Equal(fRES(v));
        }
        protected void DoSetFa002_Equal(String v) { regFa002(CK_EQ, v); }
        public void SetFa002_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa002_NotEqual(fRES(v));
        }
        protected void DoSetFa002_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_NES, v);
        }
        public void SetFa002_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_GT, fRES(v));
        }
        public void SetFa002_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_LT, fRES(v));
        }
        public void SetFa002_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_GE, fRES(v));
        }
        public void SetFa002_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_LE, fRES(v));
        }
        public void SetFa002_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa002(), "FA002");
        }
        public void SetFa002_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa002(), "FA002");
        }
        public void SetFa002_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa002_LikeSearch(v, cLSOP());
        }
        public void SetFa002_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa002(), "FA002", option);
        }
        public void SetFa002_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa002(), "FA002", option);
        }
        public void SetFa002_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa002_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa002(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa002(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa002(), "FA002");
        }
        protected abstract ConditionValue getCValueFa002();

        public void SetFa003_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa003_Equal(fRES(v));
        }
        protected void DoSetFa003_Equal(String v) { regFa003(CK_EQ, v); }
        public void SetFa003_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa003_NotEqual(fRES(v));
        }
        protected void DoSetFa003_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_NES, v);
        }
        public void SetFa003_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_GT, fRES(v));
        }
        public void SetFa003_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_LT, fRES(v));
        }
        public void SetFa003_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_GE, fRES(v));
        }
        public void SetFa003_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_LE, fRES(v));
        }
        public void SetFa003_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa003(), "FA003");
        }
        public void SetFa003_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa003(), "FA003");
        }
        public void SetFa003_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa003_LikeSearch(v, cLSOP());
        }
        public void SetFa003_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa003(), "FA003", option);
        }
        public void SetFa003_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa003(), "FA003", option);
        }
        public void SetFa003_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa003_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa003(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa003(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa003(), "FA003");
        }
        protected abstract ConditionValue getCValueFa003();

        public void SetFa004_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa004_Equal(fRES(v));
        }
        protected void DoSetFa004_Equal(String v) { regFa004(CK_EQ, v); }
        public void SetFa004_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa004_NotEqual(fRES(v));
        }
        protected void DoSetFa004_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_NES, v);
        }
        public void SetFa004_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_GT, fRES(v));
        }
        public void SetFa004_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_LT, fRES(v));
        }
        public void SetFa004_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_GE, fRES(v));
        }
        public void SetFa004_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_LE, fRES(v));
        }
        public void SetFa004_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa004(), "FA004");
        }
        public void SetFa004_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa004(), "FA004");
        }
        public void SetFa004_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa004_LikeSearch(v, cLSOP());
        }
        public void SetFa004_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa004(), "FA004", option);
        }
        public void SetFa004_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa004(), "FA004", option);
        }
        public void SetFa004_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa004_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa004(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa004(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa004(), "FA004");
        }
        protected abstract ConditionValue getCValueFa004();

        public void SetFa005_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa005_Equal(fRES(v));
        }
        protected void DoSetFa005_Equal(String v) { regFa005(CK_EQ, v); }
        public void SetFa005_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa005_NotEqual(fRES(v));
        }
        protected void DoSetFa005_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_NES, v);
        }
        public void SetFa005_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_GT, fRES(v));
        }
        public void SetFa005_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_LT, fRES(v));
        }
        public void SetFa005_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_GE, fRES(v));
        }
        public void SetFa005_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_LE, fRES(v));
        }
        public void SetFa005_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa005(), "FA005");
        }
        public void SetFa005_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa005(), "FA005");
        }
        public void SetFa005_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa005_LikeSearch(v, cLSOP());
        }
        public void SetFa005_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa005(), "FA005", option);
        }
        public void SetFa005_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa005(), "FA005", option);
        }
        public void SetFa005_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa005_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa005(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa005(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa005(), "FA005");
        }
        protected abstract ConditionValue getCValueFa005();

        public void SetFa006_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa006_Equal(fRES(v));
        }
        protected void DoSetFa006_Equal(String v) { regFa006(CK_EQ, v); }
        public void SetFa006_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa006_NotEqual(fRES(v));
        }
        protected void DoSetFa006_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_NES, v);
        }
        public void SetFa006_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_GT, fRES(v));
        }
        public void SetFa006_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_LT, fRES(v));
        }
        public void SetFa006_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_GE, fRES(v));
        }
        public void SetFa006_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_LE, fRES(v));
        }
        public void SetFa006_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa006(), "FA006");
        }
        public void SetFa006_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa006(), "FA006");
        }
        public void SetFa006_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa006_LikeSearch(v, cLSOP());
        }
        public void SetFa006_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa006(), "FA006", option);
        }
        public void SetFa006_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa006(), "FA006", option);
        }
        public void SetFa006_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa006_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa006(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa006(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa006(), "FA006");
        }
        protected abstract ConditionValue getCValueFa006();

        public void SetFa007_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa007_Equal(fRES(v));
        }
        protected void DoSetFa007_Equal(String v) { regFa007(CK_EQ, v); }
        public void SetFa007_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa007_NotEqual(fRES(v));
        }
        protected void DoSetFa007_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_NES, v);
        }
        public void SetFa007_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_GT, fRES(v));
        }
        public void SetFa007_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_LT, fRES(v));
        }
        public void SetFa007_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_GE, fRES(v));
        }
        public void SetFa007_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_LE, fRES(v));
        }
        public void SetFa007_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa007(), "FA007");
        }
        public void SetFa007_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa007(), "FA007");
        }
        public void SetFa007_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa007_LikeSearch(v, cLSOP());
        }
        public void SetFa007_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa007(), "FA007", option);
        }
        public void SetFa007_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa007(), "FA007", option);
        }
        public void SetFa007_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa007_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa007(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa007(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa007(), "FA007");
        }
        protected abstract ConditionValue getCValueFa007();

        public void SetFa008_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa008_Equal(fRES(v));
        }
        protected void DoSetFa008_Equal(String v) { regFa008(CK_EQ, v); }
        public void SetFa008_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa008_NotEqual(fRES(v));
        }
        protected void DoSetFa008_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_NES, v);
        }
        public void SetFa008_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_GT, fRES(v));
        }
        public void SetFa008_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_LT, fRES(v));
        }
        public void SetFa008_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_GE, fRES(v));
        }
        public void SetFa008_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_LE, fRES(v));
        }
        public void SetFa008_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa008(), "FA008");
        }
        public void SetFa008_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa008(), "FA008");
        }
        public void SetFa008_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa008_LikeSearch(v, cLSOP());
        }
        public void SetFa008_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa008(), "FA008", option);
        }
        public void SetFa008_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa008(), "FA008", option);
        }
        public void SetFa008_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa008_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa008(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa008(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa008(), "FA008");
        }
        protected abstract ConditionValue getCValueFa008();

        public void SetFa009_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa009_Equal(fRES(v));
        }
        protected void DoSetFa009_Equal(String v) { regFa009(CK_EQ, v); }
        public void SetFa009_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa009_NotEqual(fRES(v));
        }
        protected void DoSetFa009_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_NES, v);
        }
        public void SetFa009_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_GT, fRES(v));
        }
        public void SetFa009_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_LT, fRES(v));
        }
        public void SetFa009_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_GE, fRES(v));
        }
        public void SetFa009_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_LE, fRES(v));
        }
        public void SetFa009_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa009(), "FA009");
        }
        public void SetFa009_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa009(), "FA009");
        }
        public void SetFa009_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa009_LikeSearch(v, cLSOP());
        }
        public void SetFa009_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa009(), "FA009", option);
        }
        public void SetFa009_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa009(), "FA009", option);
        }
        public void SetFa009_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa009_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa009(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa009(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa009(), "FA009");
        }
        protected abstract ConditionValue getCValueFa009();

        public void SetFa010_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa010_Equal(fRES(v));
        }
        protected void DoSetFa010_Equal(String v) { regFa010(CK_EQ, v); }
        public void SetFa010_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa010_NotEqual(fRES(v));
        }
        protected void DoSetFa010_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_NES, v);
        }
        public void SetFa010_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_GT, fRES(v));
        }
        public void SetFa010_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_LT, fRES(v));
        }
        public void SetFa010_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_GE, fRES(v));
        }
        public void SetFa010_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_LE, fRES(v));
        }
        public void SetFa010_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa010(), "FA010");
        }
        public void SetFa010_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa010(), "FA010");
        }
        public void SetFa010_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa010_LikeSearch(v, cLSOP());
        }
        public void SetFa010_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa010(), "FA010", option);
        }
        public void SetFa010_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa010(), "FA010", option);
        }
        public void SetFa010_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa010_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa010(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa010(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa010(), "FA010");
        }
        protected abstract ConditionValue getCValueFa010();

        public void SetFa011_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa011_Equal(fRES(v));
        }
        protected void DoSetFa011_Equal(String v) { regFa011(CK_EQ, v); }
        public void SetFa011_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa011_NotEqual(fRES(v));
        }
        protected void DoSetFa011_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_NES, v);
        }
        public void SetFa011_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_GT, fRES(v));
        }
        public void SetFa011_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_LT, fRES(v));
        }
        public void SetFa011_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_GE, fRES(v));
        }
        public void SetFa011_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_LE, fRES(v));
        }
        public void SetFa011_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa011(), "FA011");
        }
        public void SetFa011_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa011(), "FA011");
        }
        public void SetFa011_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa011_LikeSearch(v, cLSOP());
        }
        public void SetFa011_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa011(), "FA011", option);
        }
        public void SetFa011_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa011(), "FA011", option);
        }
        public void SetFa011_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa011_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa011(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa011(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa011(), "FA011");
        }
        protected abstract ConditionValue getCValueFa011();

        public void SetFa012_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa012_Equal(fRES(v));
        }
        protected void DoSetFa012_Equal(String v) { regFa012(CK_EQ, v); }
        public void SetFa012_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa012_NotEqual(fRES(v));
        }
        protected void DoSetFa012_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_NES, v);
        }
        public void SetFa012_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_GT, fRES(v));
        }
        public void SetFa012_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_LT, fRES(v));
        }
        public void SetFa012_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_GE, fRES(v));
        }
        public void SetFa012_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_LE, fRES(v));
        }
        public void SetFa012_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa012(), "FA012");
        }
        public void SetFa012_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa012(), "FA012");
        }
        public void SetFa012_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa012_LikeSearch(v, cLSOP());
        }
        public void SetFa012_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa012(), "FA012", option);
        }
        public void SetFa012_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa012(), "FA012", option);
        }
        public void SetFa012_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa012_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa012(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa012(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa012(), "FA012");
        }
        protected abstract ConditionValue getCValueFa012();

        public void SetFa013_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa013_Equal(fRES(v));
        }
        protected void DoSetFa013_Equal(String v) { regFa013(CK_EQ, v); }
        public void SetFa013_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa013_NotEqual(fRES(v));
        }
        protected void DoSetFa013_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_NES, v);
        }
        public void SetFa013_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_GT, fRES(v));
        }
        public void SetFa013_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_LT, fRES(v));
        }
        public void SetFa013_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_GE, fRES(v));
        }
        public void SetFa013_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_LE, fRES(v));
        }
        public void SetFa013_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa013(), "FA013");
        }
        public void SetFa013_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa013(), "FA013");
        }
        public void SetFa013_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa013_LikeSearch(v, cLSOP());
        }
        public void SetFa013_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa013(), "FA013", option);
        }
        public void SetFa013_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa013(), "FA013", option);
        }
        public void SetFa013_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa013_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa013(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa013(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa013(), "FA013");
        }
        protected abstract ConditionValue getCValueFa013();

        public void SetFa014_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa014_Equal(fRES(v));
        }
        protected void DoSetFa014_Equal(String v) { regFa014(CK_EQ, v); }
        public void SetFa014_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa014_NotEqual(fRES(v));
        }
        protected void DoSetFa014_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_NES, v);
        }
        public void SetFa014_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_GT, fRES(v));
        }
        public void SetFa014_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_LT, fRES(v));
        }
        public void SetFa014_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_GE, fRES(v));
        }
        public void SetFa014_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_LE, fRES(v));
        }
        public void SetFa014_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa014(), "FA014");
        }
        public void SetFa014_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa014(), "FA014");
        }
        public void SetFa014_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa014_LikeSearch(v, cLSOP());
        }
        public void SetFa014_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa014(), "FA014", option);
        }
        public void SetFa014_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa014(), "FA014", option);
        }
        public void SetFa014_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa014_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa014(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa014(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa014(), "FA014");
        }
        protected abstract ConditionValue getCValueFa014();

        public void SetFa015_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa015_Equal(fRES(v));
        }
        protected void DoSetFa015_Equal(String v) { regFa015(CK_EQ, v); }
        public void SetFa015_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa015_NotEqual(fRES(v));
        }
        protected void DoSetFa015_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_NES, v);
        }
        public void SetFa015_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_GT, fRES(v));
        }
        public void SetFa015_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_LT, fRES(v));
        }
        public void SetFa015_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_GE, fRES(v));
        }
        public void SetFa015_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_LE, fRES(v));
        }
        public void SetFa015_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa015(), "FA015");
        }
        public void SetFa015_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa015(), "FA015");
        }
        public void SetFa015_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa015_LikeSearch(v, cLSOP());
        }
        public void SetFa015_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa015(), "FA015", option);
        }
        public void SetFa015_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa015(), "FA015", option);
        }
        public void SetFa015_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa015_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa015(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa015(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa015(), "FA015");
        }
        protected abstract ConditionValue getCValueFa015();

        public void SetFa016_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa016_Equal(fRES(v));
        }
        protected void DoSetFa016_Equal(String v) { regFa016(CK_EQ, v); }
        public void SetFa016_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa016_NotEqual(fRES(v));
        }
        protected void DoSetFa016_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_NES, v);
        }
        public void SetFa016_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_GT, fRES(v));
        }
        public void SetFa016_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_LT, fRES(v));
        }
        public void SetFa016_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_GE, fRES(v));
        }
        public void SetFa016_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_LE, fRES(v));
        }
        public void SetFa016_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa016(), "FA016");
        }
        public void SetFa016_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa016(), "FA016");
        }
        public void SetFa016_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa016_LikeSearch(v, cLSOP());
        }
        public void SetFa016_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa016(), "FA016", option);
        }
        public void SetFa016_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa016(), "FA016", option);
        }
        public void SetFa016_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa016_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa016(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa016(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa016(), "FA016");
        }
        protected abstract ConditionValue getCValueFa016();

        public void SetFa017_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa017_Equal(fRES(v));
        }
        protected void DoSetFa017_Equal(String v) { regFa017(CK_EQ, v); }
        public void SetFa017_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa017_NotEqual(fRES(v));
        }
        protected void DoSetFa017_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_NES, v);
        }
        public void SetFa017_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_GT, fRES(v));
        }
        public void SetFa017_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_LT, fRES(v));
        }
        public void SetFa017_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_GE, fRES(v));
        }
        public void SetFa017_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_LE, fRES(v));
        }
        public void SetFa017_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa017(), "FA017");
        }
        public void SetFa017_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa017(), "FA017");
        }
        public void SetFa017_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa017_LikeSearch(v, cLSOP());
        }
        public void SetFa017_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa017(), "FA017", option);
        }
        public void SetFa017_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa017(), "FA017", option);
        }
        public void SetFa017_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa017_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa017(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa017(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa017(), "FA017");
        }
        protected abstract ConditionValue getCValueFa017();

        public void SetFa018_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa018_Equal(fRES(v));
        }
        protected void DoSetFa018_Equal(String v) { regFa018(CK_EQ, v); }
        public void SetFa018_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa018_NotEqual(fRES(v));
        }
        protected void DoSetFa018_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_NES, v);
        }
        public void SetFa018_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_GT, fRES(v));
        }
        public void SetFa018_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_LT, fRES(v));
        }
        public void SetFa018_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_GE, fRES(v));
        }
        public void SetFa018_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_LE, fRES(v));
        }
        public void SetFa018_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa018(), "FA018");
        }
        public void SetFa018_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa018(), "FA018");
        }
        public void SetFa018_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa018_LikeSearch(v, cLSOP());
        }
        public void SetFa018_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa018(), "FA018", option);
        }
        public void SetFa018_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa018(), "FA018", option);
        }
        public void SetFa018_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa018_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa018(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa018(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa018(), "FA018");
        }
        protected abstract ConditionValue getCValueFa018();

        public void SetFa019_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa019_Equal(fRES(v));
        }
        protected void DoSetFa019_Equal(String v) { regFa019(CK_EQ, v); }
        public void SetFa019_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa019_NotEqual(fRES(v));
        }
        protected void DoSetFa019_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_NES, v);
        }
        public void SetFa019_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_GT, fRES(v));
        }
        public void SetFa019_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_LT, fRES(v));
        }
        public void SetFa019_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_GE, fRES(v));
        }
        public void SetFa019_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_LE, fRES(v));
        }
        public void SetFa019_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa019(), "FA019");
        }
        public void SetFa019_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa019(), "FA019");
        }
        public void SetFa019_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa019_LikeSearch(v, cLSOP());
        }
        public void SetFa019_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa019(), "FA019", option);
        }
        public void SetFa019_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa019(), "FA019", option);
        }
        public void SetFa019_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa019_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa019(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa019(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa019(), "FA019");
        }
        protected abstract ConditionValue getCValueFa019();

        public void SetFa020_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa020_Equal(fRES(v));
        }
        protected void DoSetFa020_Equal(String v) { regFa020(CK_EQ, v); }
        public void SetFa020_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa020_NotEqual(fRES(v));
        }
        protected void DoSetFa020_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_NES, v);
        }
        public void SetFa020_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_GT, fRES(v));
        }
        public void SetFa020_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_LT, fRES(v));
        }
        public void SetFa020_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_GE, fRES(v));
        }
        public void SetFa020_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_LE, fRES(v));
        }
        public void SetFa020_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa020(), "FA020");
        }
        public void SetFa020_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa020(), "FA020");
        }
        public void SetFa020_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa020_LikeSearch(v, cLSOP());
        }
        public void SetFa020_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa020(), "FA020", option);
        }
        public void SetFa020_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa020(), "FA020", option);
        }
        public void SetFa020_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa020_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa020(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa020(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa020(), "FA020");
        }
        protected abstract ConditionValue getCValueFa020();

        public void SetFa021_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa021_Equal(fRES(v));
        }
        protected void DoSetFa021_Equal(String v) { regFa021(CK_EQ, v); }
        public void SetFa021_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa021_NotEqual(fRES(v));
        }
        protected void DoSetFa021_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_NES, v);
        }
        public void SetFa021_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_GT, fRES(v));
        }
        public void SetFa021_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_LT, fRES(v));
        }
        public void SetFa021_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_GE, fRES(v));
        }
        public void SetFa021_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_LE, fRES(v));
        }
        public void SetFa021_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa021(), "FA021");
        }
        public void SetFa021_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa021(), "FA021");
        }
        public void SetFa021_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa021_LikeSearch(v, cLSOP());
        }
        public void SetFa021_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa021(), "FA021", option);
        }
        public void SetFa021_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa021(), "FA021", option);
        }
        public void SetFa021_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa021_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa021(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa021(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa021(), "FA021");
        }
        protected abstract ConditionValue getCValueFa021();

        public void SetFa022_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa022_Equal(fRES(v));
        }
        protected void DoSetFa022_Equal(String v) { regFa022(CK_EQ, v); }
        public void SetFa022_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa022_NotEqual(fRES(v));
        }
        protected void DoSetFa022_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_NES, v);
        }
        public void SetFa022_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_GT, fRES(v));
        }
        public void SetFa022_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_LT, fRES(v));
        }
        public void SetFa022_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_GE, fRES(v));
        }
        public void SetFa022_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_LE, fRES(v));
        }
        public void SetFa022_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa022(), "FA022");
        }
        public void SetFa022_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa022(), "FA022");
        }
        public void SetFa022_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa022_LikeSearch(v, cLSOP());
        }
        public void SetFa022_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa022(), "FA022", option);
        }
        public void SetFa022_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa022(), "FA022", option);
        }
        public void SetFa022_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa022_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa022(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa022(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa022(), "FA022");
        }
        protected abstract ConditionValue getCValueFa022();

        public void SetFa023_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa023_Equal(fRES(v));
        }
        protected void DoSetFa023_Equal(String v) { regFa023(CK_EQ, v); }
        public void SetFa023_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa023_NotEqual(fRES(v));
        }
        protected void DoSetFa023_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_NES, v);
        }
        public void SetFa023_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_GT, fRES(v));
        }
        public void SetFa023_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_LT, fRES(v));
        }
        public void SetFa023_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_GE, fRES(v));
        }
        public void SetFa023_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_LE, fRES(v));
        }
        public void SetFa023_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa023(), "FA023");
        }
        public void SetFa023_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa023(), "FA023");
        }
        public void SetFa023_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa023_LikeSearch(v, cLSOP());
        }
        public void SetFa023_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa023(), "FA023", option);
        }
        public void SetFa023_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa023(), "FA023", option);
        }
        public void SetFa023_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa023_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa023(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa023(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa023(), "FA023");
        }
        protected abstract ConditionValue getCValueFa023();

        public void SetFa024_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa024_Equal(fRES(v));
        }
        protected void DoSetFa024_Equal(String v) { regFa024(CK_EQ, v); }
        public void SetFa024_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa024_NotEqual(fRES(v));
        }
        protected void DoSetFa024_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_NES, v);
        }
        public void SetFa024_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_GT, fRES(v));
        }
        public void SetFa024_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_LT, fRES(v));
        }
        public void SetFa024_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_GE, fRES(v));
        }
        public void SetFa024_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_LE, fRES(v));
        }
        public void SetFa024_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa024(), "FA024");
        }
        public void SetFa024_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa024(), "FA024");
        }
        public void SetFa024_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa024_LikeSearch(v, cLSOP());
        }
        public void SetFa024_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa024(), "FA024", option);
        }
        public void SetFa024_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa024(), "FA024", option);
        }
        public void SetFa024_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa024_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa024(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa024(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa024(), "FA024");
        }
        protected abstract ConditionValue getCValueFa024();

        public void SetFa025_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa025_Equal(fRES(v));
        }
        protected void DoSetFa025_Equal(String v) { regFa025(CK_EQ, v); }
        public void SetFa025_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa025_NotEqual(fRES(v));
        }
        protected void DoSetFa025_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_NES, v);
        }
        public void SetFa025_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_GT, fRES(v));
        }
        public void SetFa025_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_LT, fRES(v));
        }
        public void SetFa025_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_GE, fRES(v));
        }
        public void SetFa025_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_LE, fRES(v));
        }
        public void SetFa025_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa025(), "FA025");
        }
        public void SetFa025_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa025(), "FA025");
        }
        public void SetFa025_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa025_LikeSearch(v, cLSOP());
        }
        public void SetFa025_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa025(), "FA025", option);
        }
        public void SetFa025_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa025(), "FA025", option);
        }
        public void SetFa025_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa025_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa025(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa025(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa025(), "FA025");
        }
        protected abstract ConditionValue getCValueFa025();

        public void SetFa026_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa026_Equal(fRES(v));
        }
        protected void DoSetFa026_Equal(String v) { regFa026(CK_EQ, v); }
        public void SetFa026_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa026_NotEqual(fRES(v));
        }
        protected void DoSetFa026_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_NES, v);
        }
        public void SetFa026_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_GT, fRES(v));
        }
        public void SetFa026_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_LT, fRES(v));
        }
        public void SetFa026_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_GE, fRES(v));
        }
        public void SetFa026_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_LE, fRES(v));
        }
        public void SetFa026_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa026(), "FA026");
        }
        public void SetFa026_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa026(), "FA026");
        }
        public void SetFa026_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa026_LikeSearch(v, cLSOP());
        }
        public void SetFa026_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa026(), "FA026", option);
        }
        public void SetFa026_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa026(), "FA026", option);
        }
        public void SetFa026_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa026_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa026(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa026(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa026(), "FA026");
        }
        protected abstract ConditionValue getCValueFa026();

        public void SetFa027_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa027_Equal(fRES(v));
        }
        protected void DoSetFa027_Equal(String v) { regFa027(CK_EQ, v); }
        public void SetFa027_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa027_NotEqual(fRES(v));
        }
        protected void DoSetFa027_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_NES, v);
        }
        public void SetFa027_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_GT, fRES(v));
        }
        public void SetFa027_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_LT, fRES(v));
        }
        public void SetFa027_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_GE, fRES(v));
        }
        public void SetFa027_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_LE, fRES(v));
        }
        public void SetFa027_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa027(), "FA027");
        }
        public void SetFa027_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa027(), "FA027");
        }
        public void SetFa027_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa027_LikeSearch(v, cLSOP());
        }
        public void SetFa027_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa027(), "FA027", option);
        }
        public void SetFa027_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa027(), "FA027", option);
        }
        public void SetFa027_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa027_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa027(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa027(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa027(), "FA027");
        }
        protected abstract ConditionValue getCValueFa027();

        public void SetFa028_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa028_Equal(fRES(v));
        }
        protected void DoSetFa028_Equal(String v) { regFa028(CK_EQ, v); }
        public void SetFa028_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa028_NotEqual(fRES(v));
        }
        protected void DoSetFa028_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_NES, v);
        }
        public void SetFa028_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_GT, fRES(v));
        }
        public void SetFa028_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_LT, fRES(v));
        }
        public void SetFa028_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_GE, fRES(v));
        }
        public void SetFa028_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_LE, fRES(v));
        }
        public void SetFa028_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa028(), "FA028");
        }
        public void SetFa028_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa028(), "FA028");
        }
        public void SetFa028_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa028_LikeSearch(v, cLSOP());
        }
        public void SetFa028_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa028(), "FA028", option);
        }
        public void SetFa028_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa028(), "FA028", option);
        }
        public void SetFa028_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa028_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa028(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa028(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa028(), "FA028");
        }
        protected abstract ConditionValue getCValueFa028();

        public void SetFa029_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa029_Equal(fRES(v));
        }
        protected void DoSetFa029_Equal(String v) { regFa029(CK_EQ, v); }
        public void SetFa029_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa029_NotEqual(fRES(v));
        }
        protected void DoSetFa029_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_NES, v);
        }
        public void SetFa029_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_GT, fRES(v));
        }
        public void SetFa029_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_LT, fRES(v));
        }
        public void SetFa029_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_GE, fRES(v));
        }
        public void SetFa029_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_LE, fRES(v));
        }
        public void SetFa029_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa029(), "FA029");
        }
        public void SetFa029_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa029(), "FA029");
        }
        public void SetFa029_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa029_LikeSearch(v, cLSOP());
        }
        public void SetFa029_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa029(), "FA029", option);
        }
        public void SetFa029_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa029(), "FA029", option);
        }
        public void SetFa029_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa029_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa029(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa029(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa029(), "FA029");
        }
        protected abstract ConditionValue getCValueFa029();

        public void SetFa030_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa030_Equal(fRES(v));
        }
        protected void DoSetFa030_Equal(String v) { regFa030(CK_EQ, v); }
        public void SetFa030_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa030_NotEqual(fRES(v));
        }
        protected void DoSetFa030_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_NES, v);
        }
        public void SetFa030_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_GT, fRES(v));
        }
        public void SetFa030_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_LT, fRES(v));
        }
        public void SetFa030_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_GE, fRES(v));
        }
        public void SetFa030_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_LE, fRES(v));
        }
        public void SetFa030_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa030(), "FA030");
        }
        public void SetFa030_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa030(), "FA030");
        }
        public void SetFa030_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa030_LikeSearch(v, cLSOP());
        }
        public void SetFa030_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa030(), "FA030", option);
        }
        public void SetFa030_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa030(), "FA030", option);
        }
        public void SetFa030_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa030_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa030(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa030(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa030(), "FA030");
        }
        protected abstract ConditionValue getCValueFa030();

        public void SetFa031_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa031_Equal(fRES(v));
        }
        protected void DoSetFa031_Equal(String v) { regFa031(CK_EQ, v); }
        public void SetFa031_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa031_NotEqual(fRES(v));
        }
        protected void DoSetFa031_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_NES, v);
        }
        public void SetFa031_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_GT, fRES(v));
        }
        public void SetFa031_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_LT, fRES(v));
        }
        public void SetFa031_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_GE, fRES(v));
        }
        public void SetFa031_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_LE, fRES(v));
        }
        public void SetFa031_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa031(), "FA031");
        }
        public void SetFa031_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa031(), "FA031");
        }
        public void SetFa031_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa031_LikeSearch(v, cLSOP());
        }
        public void SetFa031_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa031(), "FA031", option);
        }
        public void SetFa031_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa031(), "FA031", option);
        }
        public void SetFa031_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa031_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa031(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa031(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa031(), "FA031");
        }
        protected abstract ConditionValue getCValueFa031();

        public void SetFa032_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa032_Equal(fRES(v));
        }
        protected void DoSetFa032_Equal(String v) { regFa032(CK_EQ, v); }
        public void SetFa032_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa032_NotEqual(fRES(v));
        }
        protected void DoSetFa032_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_NES, v);
        }
        public void SetFa032_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_GT, fRES(v));
        }
        public void SetFa032_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_LT, fRES(v));
        }
        public void SetFa032_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_GE, fRES(v));
        }
        public void SetFa032_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_LE, fRES(v));
        }
        public void SetFa032_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa032(), "FA032");
        }
        public void SetFa032_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa032(), "FA032");
        }
        public void SetFa032_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa032_LikeSearch(v, cLSOP());
        }
        public void SetFa032_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa032(), "FA032", option);
        }
        public void SetFa032_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa032(), "FA032", option);
        }
        public void SetFa032_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa032_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa032(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa032(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa032(), "FA032");
        }
        protected abstract ConditionValue getCValueFa032();

        public void SetFa033_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa033_Equal(fRES(v));
        }
        protected void DoSetFa033_Equal(String v) { regFa033(CK_EQ, v); }
        public void SetFa033_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa033_NotEqual(fRES(v));
        }
        protected void DoSetFa033_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_NES, v);
        }
        public void SetFa033_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_GT, fRES(v));
        }
        public void SetFa033_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_LT, fRES(v));
        }
        public void SetFa033_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_GE, fRES(v));
        }
        public void SetFa033_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_LE, fRES(v));
        }
        public void SetFa033_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa033(), "FA033");
        }
        public void SetFa033_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa033(), "FA033");
        }
        public void SetFa033_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa033_LikeSearch(v, cLSOP());
        }
        public void SetFa033_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa033(), "FA033", option);
        }
        public void SetFa033_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa033(), "FA033", option);
        }
        public void SetFa033_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa033_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa033(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa033(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa033(), "FA033");
        }
        protected abstract ConditionValue getCValueFa033();

        public void SetFa034_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa034_Equal(fRES(v));
        }
        protected void DoSetFa034_Equal(String v) { regFa034(CK_EQ, v); }
        public void SetFa034_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa034_NotEqual(fRES(v));
        }
        protected void DoSetFa034_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_NES, v);
        }
        public void SetFa034_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_GT, fRES(v));
        }
        public void SetFa034_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_LT, fRES(v));
        }
        public void SetFa034_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_GE, fRES(v));
        }
        public void SetFa034_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_LE, fRES(v));
        }
        public void SetFa034_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa034(), "FA034");
        }
        public void SetFa034_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa034(), "FA034");
        }
        public void SetFa034_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa034_LikeSearch(v, cLSOP());
        }
        public void SetFa034_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa034(), "FA034", option);
        }
        public void SetFa034_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa034(), "FA034", option);
        }
        public void SetFa034_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa034_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa034(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa034(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa034(), "FA034");
        }
        protected abstract ConditionValue getCValueFa034();

        public void SetFa035_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa035_Equal(fRES(v));
        }
        protected void DoSetFa035_Equal(String v) { regFa035(CK_EQ, v); }
        public void SetFa035_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa035_NotEqual(fRES(v));
        }
        protected void DoSetFa035_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_NES, v);
        }
        public void SetFa035_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_GT, fRES(v));
        }
        public void SetFa035_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_LT, fRES(v));
        }
        public void SetFa035_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_GE, fRES(v));
        }
        public void SetFa035_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_LE, fRES(v));
        }
        public void SetFa035_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa035(), "FA035");
        }
        public void SetFa035_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa035(), "FA035");
        }
        public void SetFa035_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa035_LikeSearch(v, cLSOP());
        }
        public void SetFa035_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa035(), "FA035", option);
        }
        public void SetFa035_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa035(), "FA035", option);
        }
        public void SetFa035_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa035_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa035(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa035(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa035(), "FA035");
        }
        protected abstract ConditionValue getCValueFa035();

        public void SetFa036_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa036_Equal(fRES(v));
        }
        protected void DoSetFa036_Equal(String v) { regFa036(CK_EQ, v); }
        public void SetFa036_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa036_NotEqual(fRES(v));
        }
        protected void DoSetFa036_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_NES, v);
        }
        public void SetFa036_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_GT, fRES(v));
        }
        public void SetFa036_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_LT, fRES(v));
        }
        public void SetFa036_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_GE, fRES(v));
        }
        public void SetFa036_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_LE, fRES(v));
        }
        public void SetFa036_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa036(), "FA036");
        }
        public void SetFa036_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa036(), "FA036");
        }
        public void SetFa036_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa036_LikeSearch(v, cLSOP());
        }
        public void SetFa036_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa036(), "FA036", option);
        }
        public void SetFa036_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa036(), "FA036", option);
        }
        public void SetFa036_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa036_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa036(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa036(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa036(), "FA036");
        }
        protected abstract ConditionValue getCValueFa036();

        public void SetFa037_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa037_Equal(fRES(v));
        }
        protected void DoSetFa037_Equal(String v) { regFa037(CK_EQ, v); }
        public void SetFa037_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa037_NotEqual(fRES(v));
        }
        protected void DoSetFa037_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_NES, v);
        }
        public void SetFa037_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_GT, fRES(v));
        }
        public void SetFa037_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_LT, fRES(v));
        }
        public void SetFa037_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_GE, fRES(v));
        }
        public void SetFa037_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_LE, fRES(v));
        }
        public void SetFa037_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa037(), "FA037");
        }
        public void SetFa037_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa037(), "FA037");
        }
        public void SetFa037_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa037_LikeSearch(v, cLSOP());
        }
        public void SetFa037_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa037(), "FA037", option);
        }
        public void SetFa037_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa037(), "FA037", option);
        }
        public void SetFa037_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa037_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa037(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa037(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa037(), "FA037");
        }
        protected abstract ConditionValue getCValueFa037();

        public void SetFa038_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa038_Equal(fRES(v));
        }
        protected void DoSetFa038_Equal(String v) { regFa038(CK_EQ, v); }
        public void SetFa038_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa038_NotEqual(fRES(v));
        }
        protected void DoSetFa038_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_NES, v);
        }
        public void SetFa038_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_GT, fRES(v));
        }
        public void SetFa038_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_LT, fRES(v));
        }
        public void SetFa038_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_GE, fRES(v));
        }
        public void SetFa038_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_LE, fRES(v));
        }
        public void SetFa038_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa038(), "FA038");
        }
        public void SetFa038_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa038(), "FA038");
        }
        public void SetFa038_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa038_LikeSearch(v, cLSOP());
        }
        public void SetFa038_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa038(), "FA038", option);
        }
        public void SetFa038_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa038(), "FA038", option);
        }
        public void SetFa038_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa038_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa038(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa038(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa038(), "FA038");
        }
        protected abstract ConditionValue getCValueFa038();

        public void SetFa039_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa039_Equal(fRES(v));
        }
        protected void DoSetFa039_Equal(String v) { regFa039(CK_EQ, v); }
        public void SetFa039_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa039_NotEqual(fRES(v));
        }
        protected void DoSetFa039_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_NES, v);
        }
        public void SetFa039_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_GT, fRES(v));
        }
        public void SetFa039_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_LT, fRES(v));
        }
        public void SetFa039_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_GE, fRES(v));
        }
        public void SetFa039_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_LE, fRES(v));
        }
        public void SetFa039_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa039(), "FA039");
        }
        public void SetFa039_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa039(), "FA039");
        }
        public void SetFa039_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa039_LikeSearch(v, cLSOP());
        }
        public void SetFa039_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa039(), "FA039", option);
        }
        public void SetFa039_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa039(), "FA039", option);
        }
        public void SetFa039_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa039_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa039(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa039(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa039(), "FA039");
        }
        protected abstract ConditionValue getCValueFa039();

        public void SetFa040_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa040_Equal(fRES(v));
        }
        protected void DoSetFa040_Equal(String v) { regFa040(CK_EQ, v); }
        public void SetFa040_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa040_NotEqual(fRES(v));
        }
        protected void DoSetFa040_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_NES, v);
        }
        public void SetFa040_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_GT, fRES(v));
        }
        public void SetFa040_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_LT, fRES(v));
        }
        public void SetFa040_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_GE, fRES(v));
        }
        public void SetFa040_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_LE, fRES(v));
        }
        public void SetFa040_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa040(), "FA040");
        }
        public void SetFa040_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa040(), "FA040");
        }
        public void SetFa040_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa040_LikeSearch(v, cLSOP());
        }
        public void SetFa040_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa040(), "FA040", option);
        }
        public void SetFa040_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa040(), "FA040", option);
        }
        public void SetFa040_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa040_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa040(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa040(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa040(), "FA040");
        }
        protected abstract ConditionValue getCValueFa040();

        public void SetFa041_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa041_Equal(fRES(v));
        }
        protected void DoSetFa041_Equal(String v) { regFa041(CK_EQ, v); }
        public void SetFa041_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa041_NotEqual(fRES(v));
        }
        protected void DoSetFa041_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_NES, v);
        }
        public void SetFa041_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_GT, fRES(v));
        }
        public void SetFa041_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_LT, fRES(v));
        }
        public void SetFa041_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_GE, fRES(v));
        }
        public void SetFa041_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_LE, fRES(v));
        }
        public void SetFa041_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa041(), "FA041");
        }
        public void SetFa041_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa041(), "FA041");
        }
        public void SetFa041_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa041_LikeSearch(v, cLSOP());
        }
        public void SetFa041_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa041(), "FA041", option);
        }
        public void SetFa041_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa041(), "FA041", option);
        }
        public void SetFa041_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa041_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa041(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa041(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa041(), "FA041");
        }
        protected abstract ConditionValue getCValueFa041();

        public void SetFa042_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa042_Equal(fRES(v));
        }
        protected void DoSetFa042_Equal(String v) { regFa042(CK_EQ, v); }
        public void SetFa042_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa042_NotEqual(fRES(v));
        }
        protected void DoSetFa042_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_NES, v);
        }
        public void SetFa042_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_GT, fRES(v));
        }
        public void SetFa042_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_LT, fRES(v));
        }
        public void SetFa042_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_GE, fRES(v));
        }
        public void SetFa042_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_LE, fRES(v));
        }
        public void SetFa042_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa042(), "FA042");
        }
        public void SetFa042_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa042(), "FA042");
        }
        public void SetFa042_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa042_LikeSearch(v, cLSOP());
        }
        public void SetFa042_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa042(), "FA042", option);
        }
        public void SetFa042_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa042(), "FA042", option);
        }
        public void SetFa042_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa042_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa042(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa042(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa042(), "FA042");
        }
        protected abstract ConditionValue getCValueFa042();

        public void SetFa043_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa043_Equal(fRES(v));
        }
        protected void DoSetFa043_Equal(String v) { regFa043(CK_EQ, v); }
        public void SetFa043_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa043_NotEqual(fRES(v));
        }
        protected void DoSetFa043_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_NES, v);
        }
        public void SetFa043_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_GT, fRES(v));
        }
        public void SetFa043_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_LT, fRES(v));
        }
        public void SetFa043_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_GE, fRES(v));
        }
        public void SetFa043_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_LE, fRES(v));
        }
        public void SetFa043_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa043(), "FA043");
        }
        public void SetFa043_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa043(), "FA043");
        }
        public void SetFa043_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa043_LikeSearch(v, cLSOP());
        }
        public void SetFa043_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa043(), "FA043", option);
        }
        public void SetFa043_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa043(), "FA043", option);
        }
        public void SetFa043_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa043_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa043(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa043(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa043(), "FA043");
        }
        protected abstract ConditionValue getCValueFa043();

        public void SetFa044_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa044_Equal(fRES(v));
        }
        protected void DoSetFa044_Equal(String v) { regFa044(CK_EQ, v); }
        public void SetFa044_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa044_NotEqual(fRES(v));
        }
        protected void DoSetFa044_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_NES, v);
        }
        public void SetFa044_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_GT, fRES(v));
        }
        public void SetFa044_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_LT, fRES(v));
        }
        public void SetFa044_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_GE, fRES(v));
        }
        public void SetFa044_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_LE, fRES(v));
        }
        public void SetFa044_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa044(), "FA044");
        }
        public void SetFa044_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa044(), "FA044");
        }
        public void SetFa044_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa044_LikeSearch(v, cLSOP());
        }
        public void SetFa044_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa044(), "FA044", option);
        }
        public void SetFa044_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa044(), "FA044", option);
        }
        public void SetFa044_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa044_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa044(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa044(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa044(), "FA044");
        }
        protected abstract ConditionValue getCValueFa044();

        public void SetFa045_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa045_Equal(fRES(v));
        }
        protected void DoSetFa045_Equal(String v) { regFa045(CK_EQ, v); }
        public void SetFa045_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa045_NotEqual(fRES(v));
        }
        protected void DoSetFa045_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_NES, v);
        }
        public void SetFa045_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_GT, fRES(v));
        }
        public void SetFa045_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_LT, fRES(v));
        }
        public void SetFa045_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_GE, fRES(v));
        }
        public void SetFa045_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_LE, fRES(v));
        }
        public void SetFa045_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa045(), "FA045");
        }
        public void SetFa045_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa045(), "FA045");
        }
        public void SetFa045_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa045_LikeSearch(v, cLSOP());
        }
        public void SetFa045_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa045(), "FA045", option);
        }
        public void SetFa045_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa045(), "FA045", option);
        }
        public void SetFa045_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa045_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa045(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa045(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa045(), "FA045");
        }
        protected abstract ConditionValue getCValueFa045();

        public void SetFa046_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa046_Equal(fRES(v));
        }
        protected void DoSetFa046_Equal(String v) { regFa046(CK_EQ, v); }
        public void SetFa046_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa046_NotEqual(fRES(v));
        }
        protected void DoSetFa046_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_NES, v);
        }
        public void SetFa046_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_GT, fRES(v));
        }
        public void SetFa046_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_LT, fRES(v));
        }
        public void SetFa046_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_GE, fRES(v));
        }
        public void SetFa046_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_LE, fRES(v));
        }
        public void SetFa046_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa046(), "FA046");
        }
        public void SetFa046_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa046(), "FA046");
        }
        public void SetFa046_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa046_LikeSearch(v, cLSOP());
        }
        public void SetFa046_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa046(), "FA046", option);
        }
        public void SetFa046_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa046(), "FA046", option);
        }
        public void SetFa046_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa046_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa046(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa046(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa046(), "FA046");
        }
        protected abstract ConditionValue getCValueFa046();

        public void SetFa047_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa047_Equal(fRES(v));
        }
        protected void DoSetFa047_Equal(String v) { regFa047(CK_EQ, v); }
        public void SetFa047_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa047_NotEqual(fRES(v));
        }
        protected void DoSetFa047_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_NES, v);
        }
        public void SetFa047_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_GT, fRES(v));
        }
        public void SetFa047_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_LT, fRES(v));
        }
        public void SetFa047_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_GE, fRES(v));
        }
        public void SetFa047_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_LE, fRES(v));
        }
        public void SetFa047_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa047(), "FA047");
        }
        public void SetFa047_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa047(), "FA047");
        }
        public void SetFa047_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa047_LikeSearch(v, cLSOP());
        }
        public void SetFa047_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa047(), "FA047", option);
        }
        public void SetFa047_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa047(), "FA047", option);
        }
        public void SetFa047_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa047_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa047(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa047(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa047(), "FA047");
        }
        protected abstract ConditionValue getCValueFa047();

        public void SetFa048_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa048_Equal(fRES(v));
        }
        protected void DoSetFa048_Equal(String v) { regFa048(CK_EQ, v); }
        public void SetFa048_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa048_NotEqual(fRES(v));
        }
        protected void DoSetFa048_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_NES, v);
        }
        public void SetFa048_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_GT, fRES(v));
        }
        public void SetFa048_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_LT, fRES(v));
        }
        public void SetFa048_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_GE, fRES(v));
        }
        public void SetFa048_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_LE, fRES(v));
        }
        public void SetFa048_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa048(), "FA048");
        }
        public void SetFa048_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa048(), "FA048");
        }
        public void SetFa048_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa048_LikeSearch(v, cLSOP());
        }
        public void SetFa048_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa048(), "FA048", option);
        }
        public void SetFa048_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa048(), "FA048", option);
        }
        public void SetFa048_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa048_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa048(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa048(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa048(), "FA048");
        }
        protected abstract ConditionValue getCValueFa048();

        public void SetFa049_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa049_Equal(fRES(v));
        }
        protected void DoSetFa049_Equal(String v) { regFa049(CK_EQ, v); }
        public void SetFa049_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa049_NotEqual(fRES(v));
        }
        protected void DoSetFa049_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_NES, v);
        }
        public void SetFa049_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_GT, fRES(v));
        }
        public void SetFa049_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_LT, fRES(v));
        }
        public void SetFa049_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_GE, fRES(v));
        }
        public void SetFa049_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_LE, fRES(v));
        }
        public void SetFa049_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa049(), "FA049");
        }
        public void SetFa049_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa049(), "FA049");
        }
        public void SetFa049_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa049_LikeSearch(v, cLSOP());
        }
        public void SetFa049_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa049(), "FA049", option);
        }
        public void SetFa049_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa049(), "FA049", option);
        }
        public void SetFa049_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa049_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa049(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa049(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa049(), "FA049");
        }
        protected abstract ConditionValue getCValueFa049();

        public void SetFa050_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa050_Equal(fRES(v));
        }
        protected void DoSetFa050_Equal(String v) { regFa050(CK_EQ, v); }
        public void SetFa050_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa050_NotEqual(fRES(v));
        }
        protected void DoSetFa050_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_NES, v);
        }
        public void SetFa050_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_GT, fRES(v));
        }
        public void SetFa050_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_LT, fRES(v));
        }
        public void SetFa050_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_GE, fRES(v));
        }
        public void SetFa050_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_LE, fRES(v));
        }
        public void SetFa050_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa050(), "FA050");
        }
        public void SetFa050_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa050(), "FA050");
        }
        public void SetFa050_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa050_LikeSearch(v, cLSOP());
        }
        public void SetFa050_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa050(), "FA050", option);
        }
        public void SetFa050_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa050(), "FA050", option);
        }
        public void SetFa050_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa050_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa050(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa050(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa050(), "FA050");
        }
        protected abstract ConditionValue getCValueFa050();

        public void SetFa051_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa051_Equal(fRES(v));
        }
        protected void DoSetFa051_Equal(String v) { regFa051(CK_EQ, v); }
        public void SetFa051_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa051_NotEqual(fRES(v));
        }
        protected void DoSetFa051_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_NES, v);
        }
        public void SetFa051_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_GT, fRES(v));
        }
        public void SetFa051_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_LT, fRES(v));
        }
        public void SetFa051_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_GE, fRES(v));
        }
        public void SetFa051_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_LE, fRES(v));
        }
        public void SetFa051_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa051(), "FA051");
        }
        public void SetFa051_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa051(), "FA051");
        }
        public void SetFa051_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa051_LikeSearch(v, cLSOP());
        }
        public void SetFa051_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa051(), "FA051", option);
        }
        public void SetFa051_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa051(), "FA051", option);
        }
        public void SetFa051_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa051_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa051(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa051(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa051(), "FA051");
        }
        protected abstract ConditionValue getCValueFa051();

        public void SetFa052_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa052_Equal(fRES(v));
        }
        protected void DoSetFa052_Equal(String v) { regFa052(CK_EQ, v); }
        public void SetFa052_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa052_NotEqual(fRES(v));
        }
        protected void DoSetFa052_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_NES, v);
        }
        public void SetFa052_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_GT, fRES(v));
        }
        public void SetFa052_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_LT, fRES(v));
        }
        public void SetFa052_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_GE, fRES(v));
        }
        public void SetFa052_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_LE, fRES(v));
        }
        public void SetFa052_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa052(), "FA052");
        }
        public void SetFa052_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa052(), "FA052");
        }
        public void SetFa052_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa052_LikeSearch(v, cLSOP());
        }
        public void SetFa052_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa052(), "FA052", option);
        }
        public void SetFa052_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa052(), "FA052", option);
        }
        public void SetFa052_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa052_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa052(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa052(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa052(), "FA052");
        }
        protected abstract ConditionValue getCValueFa052();

        public void SetFa053_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa053_Equal(fRES(v));
        }
        protected void DoSetFa053_Equal(String v) { regFa053(CK_EQ, v); }
        public void SetFa053_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa053_NotEqual(fRES(v));
        }
        protected void DoSetFa053_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_NES, v);
        }
        public void SetFa053_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_GT, fRES(v));
        }
        public void SetFa053_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_LT, fRES(v));
        }
        public void SetFa053_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_GE, fRES(v));
        }
        public void SetFa053_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_LE, fRES(v));
        }
        public void SetFa053_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa053(), "FA053");
        }
        public void SetFa053_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa053(), "FA053");
        }
        public void SetFa053_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa053_LikeSearch(v, cLSOP());
        }
        public void SetFa053_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa053(), "FA053", option);
        }
        public void SetFa053_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa053(), "FA053", option);
        }
        public void SetFa053_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa053_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa053(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa053(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa053(), "FA053");
        }
        protected abstract ConditionValue getCValueFa053();

        public void SetFa054_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa054_Equal(fRES(v));
        }
        protected void DoSetFa054_Equal(String v) { regFa054(CK_EQ, v); }
        public void SetFa054_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa054_NotEqual(fRES(v));
        }
        protected void DoSetFa054_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_NES, v);
        }
        public void SetFa054_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_GT, fRES(v));
        }
        public void SetFa054_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_LT, fRES(v));
        }
        public void SetFa054_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_GE, fRES(v));
        }
        public void SetFa054_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_LE, fRES(v));
        }
        public void SetFa054_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa054(), "FA054");
        }
        public void SetFa054_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa054(), "FA054");
        }
        public void SetFa054_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa054_LikeSearch(v, cLSOP());
        }
        public void SetFa054_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa054(), "FA054", option);
        }
        public void SetFa054_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa054(), "FA054", option);
        }
        public void SetFa054_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa054_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa054(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa054(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa054(), "FA054");
        }
        protected abstract ConditionValue getCValueFa054();

        public void SetFa055_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa055_Equal(fRES(v));
        }
        protected void DoSetFa055_Equal(String v) { regFa055(CK_EQ, v); }
        public void SetFa055_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa055_NotEqual(fRES(v));
        }
        protected void DoSetFa055_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_NES, v);
        }
        public void SetFa055_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_GT, fRES(v));
        }
        public void SetFa055_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_LT, fRES(v));
        }
        public void SetFa055_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_GE, fRES(v));
        }
        public void SetFa055_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_LE, fRES(v));
        }
        public void SetFa055_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa055(), "FA055");
        }
        public void SetFa055_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa055(), "FA055");
        }
        public void SetFa055_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa055_LikeSearch(v, cLSOP());
        }
        public void SetFa055_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa055(), "FA055", option);
        }
        public void SetFa055_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa055(), "FA055", option);
        }
        public void SetFa055_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa055_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa055(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa055(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa055(), "FA055");
        }
        protected abstract ConditionValue getCValueFa055();

        public void SetFa056_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa056_Equal(fRES(v));
        }
        protected void DoSetFa056_Equal(String v) { regFa056(CK_EQ, v); }
        public void SetFa056_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa056_NotEqual(fRES(v));
        }
        protected void DoSetFa056_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_NES, v);
        }
        public void SetFa056_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_GT, fRES(v));
        }
        public void SetFa056_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_LT, fRES(v));
        }
        public void SetFa056_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_GE, fRES(v));
        }
        public void SetFa056_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_LE, fRES(v));
        }
        public void SetFa056_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa056(), "FA056");
        }
        public void SetFa056_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa056(), "FA056");
        }
        public void SetFa056_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa056_LikeSearch(v, cLSOP());
        }
        public void SetFa056_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa056(), "FA056", option);
        }
        public void SetFa056_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa056(), "FA056", option);
        }
        public void SetFa056_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa056_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa056(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa056(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa056(), "FA056");
        }
        protected abstract ConditionValue getCValueFa056();

        public void SetFa057_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa057_Equal(fRES(v));
        }
        protected void DoSetFa057_Equal(String v) { regFa057(CK_EQ, v); }
        public void SetFa057_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa057_NotEqual(fRES(v));
        }
        protected void DoSetFa057_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_NES, v);
        }
        public void SetFa057_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_GT, fRES(v));
        }
        public void SetFa057_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_LT, fRES(v));
        }
        public void SetFa057_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_GE, fRES(v));
        }
        public void SetFa057_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_LE, fRES(v));
        }
        public void SetFa057_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa057(), "FA057");
        }
        public void SetFa057_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa057(), "FA057");
        }
        public void SetFa057_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa057_LikeSearch(v, cLSOP());
        }
        public void SetFa057_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa057(), "FA057", option);
        }
        public void SetFa057_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa057(), "FA057", option);
        }
        public void SetFa057_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa057_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa057(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa057(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa057(), "FA057");
        }
        protected abstract ConditionValue getCValueFa057();

        public void SetFa058_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa058_Equal(fRES(v));
        }
        protected void DoSetFa058_Equal(String v) { regFa058(CK_EQ, v); }
        public void SetFa058_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa058_NotEqual(fRES(v));
        }
        protected void DoSetFa058_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_NES, v);
        }
        public void SetFa058_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_GT, fRES(v));
        }
        public void SetFa058_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_LT, fRES(v));
        }
        public void SetFa058_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_GE, fRES(v));
        }
        public void SetFa058_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_LE, fRES(v));
        }
        public void SetFa058_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa058(), "FA058");
        }
        public void SetFa058_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa058(), "FA058");
        }
        public void SetFa058_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa058_LikeSearch(v, cLSOP());
        }
        public void SetFa058_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa058(), "FA058", option);
        }
        public void SetFa058_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa058(), "FA058", option);
        }
        public void SetFa058_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa058_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa058(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa058(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa058(), "FA058");
        }
        protected abstract ConditionValue getCValueFa058();

        public void SetFa059_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa059_Equal(fRES(v));
        }
        protected void DoSetFa059_Equal(String v) { regFa059(CK_EQ, v); }
        public void SetFa059_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa059_NotEqual(fRES(v));
        }
        protected void DoSetFa059_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_NES, v);
        }
        public void SetFa059_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_GT, fRES(v));
        }
        public void SetFa059_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_LT, fRES(v));
        }
        public void SetFa059_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_GE, fRES(v));
        }
        public void SetFa059_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_LE, fRES(v));
        }
        public void SetFa059_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa059(), "FA059");
        }
        public void SetFa059_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa059(), "FA059");
        }
        public void SetFa059_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa059_LikeSearch(v, cLSOP());
        }
        public void SetFa059_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa059(), "FA059", option);
        }
        public void SetFa059_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa059(), "FA059", option);
        }
        public void SetFa059_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa059_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa059(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa059(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa059(), "FA059");
        }
        protected abstract ConditionValue getCValueFa059();

        public void SetFa060_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa060_Equal(fRES(v));
        }
        protected void DoSetFa060_Equal(String v) { regFa060(CK_EQ, v); }
        public void SetFa060_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa060_NotEqual(fRES(v));
        }
        protected void DoSetFa060_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_NES, v);
        }
        public void SetFa060_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_GT, fRES(v));
        }
        public void SetFa060_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_LT, fRES(v));
        }
        public void SetFa060_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_GE, fRES(v));
        }
        public void SetFa060_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_LE, fRES(v));
        }
        public void SetFa060_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa060(), "FA060");
        }
        public void SetFa060_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa060(), "FA060");
        }
        public void SetFa060_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa060_LikeSearch(v, cLSOP());
        }
        public void SetFa060_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa060(), "FA060", option);
        }
        public void SetFa060_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa060(), "FA060", option);
        }
        public void SetFa060_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa060_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa060(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa060(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa060(), "FA060");
        }
        protected abstract ConditionValue getCValueFa060();

        public void SetFa061_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa061_Equal(fRES(v));
        }
        protected void DoSetFa061_Equal(String v) { regFa061(CK_EQ, v); }
        public void SetFa061_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa061_NotEqual(fRES(v));
        }
        protected void DoSetFa061_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_NES, v);
        }
        public void SetFa061_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_GT, fRES(v));
        }
        public void SetFa061_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_LT, fRES(v));
        }
        public void SetFa061_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_GE, fRES(v));
        }
        public void SetFa061_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_LE, fRES(v));
        }
        public void SetFa061_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa061(), "FA061");
        }
        public void SetFa061_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa061(), "FA061");
        }
        public void SetFa061_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa061_LikeSearch(v, cLSOP());
        }
        public void SetFa061_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa061(), "FA061", option);
        }
        public void SetFa061_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa061(), "FA061", option);
        }
        public void SetFa061_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa061_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa061(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa061(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa061(), "FA061");
        }
        protected abstract ConditionValue getCValueFa061();

        public void SetFa062_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa062_Equal(fRES(v));
        }
        protected void DoSetFa062_Equal(String v) { regFa062(CK_EQ, v); }
        public void SetFa062_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa062_NotEqual(fRES(v));
        }
        protected void DoSetFa062_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_NES, v);
        }
        public void SetFa062_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_GT, fRES(v));
        }
        public void SetFa062_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_LT, fRES(v));
        }
        public void SetFa062_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_GE, fRES(v));
        }
        public void SetFa062_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_LE, fRES(v));
        }
        public void SetFa062_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa062(), "FA062");
        }
        public void SetFa062_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa062(), "FA062");
        }
        public void SetFa062_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa062_LikeSearch(v, cLSOP());
        }
        public void SetFa062_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa062(), "FA062", option);
        }
        public void SetFa062_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa062(), "FA062", option);
        }
        public void SetFa062_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa062_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa062(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa062(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa062(), "FA062");
        }
        protected abstract ConditionValue getCValueFa062();

        public void SetFa063_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa063_Equal(fRES(v));
        }
        protected void DoSetFa063_Equal(String v) { regFa063(CK_EQ, v); }
        public void SetFa063_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa063_NotEqual(fRES(v));
        }
        protected void DoSetFa063_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_NES, v);
        }
        public void SetFa063_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_GT, fRES(v));
        }
        public void SetFa063_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_LT, fRES(v));
        }
        public void SetFa063_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_GE, fRES(v));
        }
        public void SetFa063_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_LE, fRES(v));
        }
        public void SetFa063_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa063(), "FA063");
        }
        public void SetFa063_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa063(), "FA063");
        }
        public void SetFa063_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa063_LikeSearch(v, cLSOP());
        }
        public void SetFa063_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa063(), "FA063", option);
        }
        public void SetFa063_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa063(), "FA063", option);
        }
        public void SetFa063_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa063_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa063(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa063(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa063(), "FA063");
        }
        protected abstract ConditionValue getCValueFa063();

        public void SetFa064_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa064_Equal(fRES(v));
        }
        protected void DoSetFa064_Equal(String v) { regFa064(CK_EQ, v); }
        public void SetFa064_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa064_NotEqual(fRES(v));
        }
        protected void DoSetFa064_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_NES, v);
        }
        public void SetFa064_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_GT, fRES(v));
        }
        public void SetFa064_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_LT, fRES(v));
        }
        public void SetFa064_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_GE, fRES(v));
        }
        public void SetFa064_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_LE, fRES(v));
        }
        public void SetFa064_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa064(), "FA064");
        }
        public void SetFa064_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa064(), "FA064");
        }
        public void SetFa064_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa064_LikeSearch(v, cLSOP());
        }
        public void SetFa064_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa064(), "FA064", option);
        }
        public void SetFa064_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa064(), "FA064", option);
        }
        public void SetFa064_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa064_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa064(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa064(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa064(), "FA064");
        }
        protected abstract ConditionValue getCValueFa064();

        public void SetFa065_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa065_Equal(fRES(v));
        }
        protected void DoSetFa065_Equal(String v) { regFa065(CK_EQ, v); }
        public void SetFa065_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa065_NotEqual(fRES(v));
        }
        protected void DoSetFa065_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_NES, v);
        }
        public void SetFa065_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_GT, fRES(v));
        }
        public void SetFa065_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_LT, fRES(v));
        }
        public void SetFa065_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_GE, fRES(v));
        }
        public void SetFa065_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_LE, fRES(v));
        }
        public void SetFa065_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa065(), "FA065");
        }
        public void SetFa065_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa065(), "FA065");
        }
        public void SetFa065_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa065_LikeSearch(v, cLSOP());
        }
        public void SetFa065_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa065(), "FA065", option);
        }
        public void SetFa065_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa065(), "FA065", option);
        }
        public void SetFa065_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa065_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa065(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa065(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa065(), "FA065");
        }
        protected abstract ConditionValue getCValueFa065();

        public void SetFa066_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa066_Equal(fRES(v));
        }
        protected void DoSetFa066_Equal(String v) { regFa066(CK_EQ, v); }
        public void SetFa066_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa066_NotEqual(fRES(v));
        }
        protected void DoSetFa066_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_NES, v);
        }
        public void SetFa066_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_GT, fRES(v));
        }
        public void SetFa066_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_LT, fRES(v));
        }
        public void SetFa066_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_GE, fRES(v));
        }
        public void SetFa066_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_LE, fRES(v));
        }
        public void SetFa066_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa066(), "FA066");
        }
        public void SetFa066_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa066(), "FA066");
        }
        public void SetFa066_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa066_LikeSearch(v, cLSOP());
        }
        public void SetFa066_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa066(), "FA066", option);
        }
        public void SetFa066_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa066(), "FA066", option);
        }
        public void SetFa066_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa066_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa066(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa066(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa066(), "FA066");
        }
        protected abstract ConditionValue getCValueFa066();

        public void SetFa067_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa067_Equal(fRES(v));
        }
        protected void DoSetFa067_Equal(String v) { regFa067(CK_EQ, v); }
        public void SetFa067_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa067_NotEqual(fRES(v));
        }
        protected void DoSetFa067_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_NES, v);
        }
        public void SetFa067_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_GT, fRES(v));
        }
        public void SetFa067_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_LT, fRES(v));
        }
        public void SetFa067_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_GE, fRES(v));
        }
        public void SetFa067_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_LE, fRES(v));
        }
        public void SetFa067_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa067(), "FA067");
        }
        public void SetFa067_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa067(), "FA067");
        }
        public void SetFa067_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa067_LikeSearch(v, cLSOP());
        }
        public void SetFa067_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa067(), "FA067", option);
        }
        public void SetFa067_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa067(), "FA067", option);
        }
        public void SetFa067_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa067_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa067(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa067(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa067(), "FA067");
        }
        protected abstract ConditionValue getCValueFa067();

        public void SetFa068_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa068_Equal(fRES(v));
        }
        protected void DoSetFa068_Equal(String v) { regFa068(CK_EQ, v); }
        public void SetFa068_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa068_NotEqual(fRES(v));
        }
        protected void DoSetFa068_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_NES, v);
        }
        public void SetFa068_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_GT, fRES(v));
        }
        public void SetFa068_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_LT, fRES(v));
        }
        public void SetFa068_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_GE, fRES(v));
        }
        public void SetFa068_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_LE, fRES(v));
        }
        public void SetFa068_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa068(), "FA068");
        }
        public void SetFa068_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa068(), "FA068");
        }
        public void SetFa068_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa068_LikeSearch(v, cLSOP());
        }
        public void SetFa068_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa068(), "FA068", option);
        }
        public void SetFa068_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa068(), "FA068", option);
        }
        public void SetFa068_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa068_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa068(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa068(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa068(), "FA068");
        }
        protected abstract ConditionValue getCValueFa068();

        public void SetFa069_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa069_Equal(fRES(v));
        }
        protected void DoSetFa069_Equal(String v) { regFa069(CK_EQ, v); }
        public void SetFa069_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa069_NotEqual(fRES(v));
        }
        protected void DoSetFa069_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_NES, v);
        }
        public void SetFa069_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_GT, fRES(v));
        }
        public void SetFa069_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_LT, fRES(v));
        }
        public void SetFa069_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_GE, fRES(v));
        }
        public void SetFa069_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_LE, fRES(v));
        }
        public void SetFa069_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa069(), "FA069");
        }
        public void SetFa069_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa069(), "FA069");
        }
        public void SetFa069_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa069_LikeSearch(v, cLSOP());
        }
        public void SetFa069_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa069(), "FA069", option);
        }
        public void SetFa069_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa069(), "FA069", option);
        }
        public void SetFa069_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa069_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa069(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa069(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa069(), "FA069");
        }
        protected abstract ConditionValue getCValueFa069();

        public void SetFa070_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa070_Equal(fRES(v));
        }
        protected void DoSetFa070_Equal(String v) { regFa070(CK_EQ, v); }
        public void SetFa070_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa070_NotEqual(fRES(v));
        }
        protected void DoSetFa070_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_NES, v);
        }
        public void SetFa070_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_GT, fRES(v));
        }
        public void SetFa070_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_LT, fRES(v));
        }
        public void SetFa070_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_GE, fRES(v));
        }
        public void SetFa070_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_LE, fRES(v));
        }
        public void SetFa070_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa070(), "FA070");
        }
        public void SetFa070_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa070(), "FA070");
        }
        public void SetFa070_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa070_LikeSearch(v, cLSOP());
        }
        public void SetFa070_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa070(), "FA070", option);
        }
        public void SetFa070_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa070(), "FA070", option);
        }
        public void SetFa070_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa070_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa070(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa070(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa070(), "FA070");
        }
        protected abstract ConditionValue getCValueFa070();

        public void SetFa071_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa071_Equal(fRES(v));
        }
        protected void DoSetFa071_Equal(String v) { regFa071(CK_EQ, v); }
        public void SetFa071_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa071_NotEqual(fRES(v));
        }
        protected void DoSetFa071_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_NES, v);
        }
        public void SetFa071_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_GT, fRES(v));
        }
        public void SetFa071_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_LT, fRES(v));
        }
        public void SetFa071_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_GE, fRES(v));
        }
        public void SetFa071_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_LE, fRES(v));
        }
        public void SetFa071_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa071(), "FA071");
        }
        public void SetFa071_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa071(), "FA071");
        }
        public void SetFa071_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa071_LikeSearch(v, cLSOP());
        }
        public void SetFa071_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa071(), "FA071", option);
        }
        public void SetFa071_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa071(), "FA071", option);
        }
        public void SetFa071_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa071_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa071(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa071(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa071(), "FA071");
        }
        protected abstract ConditionValue getCValueFa071();

        public void SetFa072_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa072_Equal(fRES(v));
        }
        protected void DoSetFa072_Equal(String v) { regFa072(CK_EQ, v); }
        public void SetFa072_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa072_NotEqual(fRES(v));
        }
        protected void DoSetFa072_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_NES, v);
        }
        public void SetFa072_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_GT, fRES(v));
        }
        public void SetFa072_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_LT, fRES(v));
        }
        public void SetFa072_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_GE, fRES(v));
        }
        public void SetFa072_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_LE, fRES(v));
        }
        public void SetFa072_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa072(), "FA072");
        }
        public void SetFa072_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa072(), "FA072");
        }
        public void SetFa072_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa072_LikeSearch(v, cLSOP());
        }
        public void SetFa072_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa072(), "FA072", option);
        }
        public void SetFa072_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa072(), "FA072", option);
        }
        public void SetFa072_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa072_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa072(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa072(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa072(), "FA072");
        }
        protected abstract ConditionValue getCValueFa072();

        public void SetFa073_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa073_Equal(fRES(v));
        }
        protected void DoSetFa073_Equal(String v) { regFa073(CK_EQ, v); }
        public void SetFa073_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa073_NotEqual(fRES(v));
        }
        protected void DoSetFa073_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_NES, v);
        }
        public void SetFa073_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_GT, fRES(v));
        }
        public void SetFa073_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_LT, fRES(v));
        }
        public void SetFa073_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_GE, fRES(v));
        }
        public void SetFa073_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_LE, fRES(v));
        }
        public void SetFa073_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa073(), "FA073");
        }
        public void SetFa073_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa073(), "FA073");
        }
        public void SetFa073_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa073_LikeSearch(v, cLSOP());
        }
        public void SetFa073_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa073(), "FA073", option);
        }
        public void SetFa073_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa073(), "FA073", option);
        }
        public void SetFa073_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa073_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa073(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa073(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa073(), "FA073");
        }
        protected abstract ConditionValue getCValueFa073();

        public void SetFa074_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa074_Equal(fRES(v));
        }
        protected void DoSetFa074_Equal(String v) { regFa074(CK_EQ, v); }
        public void SetFa074_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa074_NotEqual(fRES(v));
        }
        protected void DoSetFa074_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_NES, v);
        }
        public void SetFa074_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_GT, fRES(v));
        }
        public void SetFa074_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_LT, fRES(v));
        }
        public void SetFa074_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_GE, fRES(v));
        }
        public void SetFa074_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_LE, fRES(v));
        }
        public void SetFa074_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa074(), "FA074");
        }
        public void SetFa074_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa074(), "FA074");
        }
        public void SetFa074_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa074_LikeSearch(v, cLSOP());
        }
        public void SetFa074_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa074(), "FA074", option);
        }
        public void SetFa074_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa074(), "FA074", option);
        }
        public void SetFa074_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa074_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa074(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa074(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa074(), "FA074");
        }
        protected abstract ConditionValue getCValueFa074();

        public void SetFa075_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa075_Equal(fRES(v));
        }
        protected void DoSetFa075_Equal(String v) { regFa075(CK_EQ, v); }
        public void SetFa075_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa075_NotEqual(fRES(v));
        }
        protected void DoSetFa075_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_NES, v);
        }
        public void SetFa075_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_GT, fRES(v));
        }
        public void SetFa075_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_LT, fRES(v));
        }
        public void SetFa075_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_GE, fRES(v));
        }
        public void SetFa075_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_LE, fRES(v));
        }
        public void SetFa075_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa075(), "FA075");
        }
        public void SetFa075_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa075(), "FA075");
        }
        public void SetFa075_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa075_LikeSearch(v, cLSOP());
        }
        public void SetFa075_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa075(), "FA075", option);
        }
        public void SetFa075_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa075(), "FA075", option);
        }
        public void SetFa075_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa075_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa075(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa075(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa075(), "FA075");
        }
        protected abstract ConditionValue getCValueFa075();

        public void SetFa076_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa076_Equal(fRES(v));
        }
        protected void DoSetFa076_Equal(String v) { regFa076(CK_EQ, v); }
        public void SetFa076_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa076_NotEqual(fRES(v));
        }
        protected void DoSetFa076_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_NES, v);
        }
        public void SetFa076_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_GT, fRES(v));
        }
        public void SetFa076_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_LT, fRES(v));
        }
        public void SetFa076_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_GE, fRES(v));
        }
        public void SetFa076_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_LE, fRES(v));
        }
        public void SetFa076_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa076(), "FA076");
        }
        public void SetFa076_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa076(), "FA076");
        }
        public void SetFa076_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa076_LikeSearch(v, cLSOP());
        }
        public void SetFa076_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa076(), "FA076", option);
        }
        public void SetFa076_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa076(), "FA076", option);
        }
        public void SetFa076_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa076_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa076(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa076(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa076(), "FA076");
        }
        protected abstract ConditionValue getCValueFa076();

        public void SetFa077_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa077_Equal(fRES(v));
        }
        protected void DoSetFa077_Equal(String v) { regFa077(CK_EQ, v); }
        public void SetFa077_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa077_NotEqual(fRES(v));
        }
        protected void DoSetFa077_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_NES, v);
        }
        public void SetFa077_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_GT, fRES(v));
        }
        public void SetFa077_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_LT, fRES(v));
        }
        public void SetFa077_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_GE, fRES(v));
        }
        public void SetFa077_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_LE, fRES(v));
        }
        public void SetFa077_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa077(), "FA077");
        }
        public void SetFa077_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa077(), "FA077");
        }
        public void SetFa077_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa077_LikeSearch(v, cLSOP());
        }
        public void SetFa077_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa077(), "FA077", option);
        }
        public void SetFa077_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa077(), "FA077", option);
        }
        public void SetFa077_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa077_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa077(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa077(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa077(), "FA077");
        }
        protected abstract ConditionValue getCValueFa077();

        public void SetFa078_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa078_Equal(fRES(v));
        }
        protected void DoSetFa078_Equal(String v) { regFa078(CK_EQ, v); }
        public void SetFa078_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa078_NotEqual(fRES(v));
        }
        protected void DoSetFa078_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_NES, v);
        }
        public void SetFa078_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_GT, fRES(v));
        }
        public void SetFa078_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_LT, fRES(v));
        }
        public void SetFa078_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_GE, fRES(v));
        }
        public void SetFa078_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_LE, fRES(v));
        }
        public void SetFa078_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa078(), "FA078");
        }
        public void SetFa078_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa078(), "FA078");
        }
        public void SetFa078_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa078_LikeSearch(v, cLSOP());
        }
        public void SetFa078_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa078(), "FA078", option);
        }
        public void SetFa078_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa078(), "FA078", option);
        }
        public void SetFa078_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa078_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa078(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa078(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa078(), "FA078");
        }
        protected abstract ConditionValue getCValueFa078();

        public void SetFa079_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa079_Equal(fRES(v));
        }
        protected void DoSetFa079_Equal(String v) { regFa079(CK_EQ, v); }
        public void SetFa079_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa079_NotEqual(fRES(v));
        }
        protected void DoSetFa079_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_NES, v);
        }
        public void SetFa079_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_GT, fRES(v));
        }
        public void SetFa079_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_LT, fRES(v));
        }
        public void SetFa079_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_GE, fRES(v));
        }
        public void SetFa079_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_LE, fRES(v));
        }
        public void SetFa079_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa079(), "FA079");
        }
        public void SetFa079_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa079(), "FA079");
        }
        public void SetFa079_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa079_LikeSearch(v, cLSOP());
        }
        public void SetFa079_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa079(), "FA079", option);
        }
        public void SetFa079_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa079(), "FA079", option);
        }
        public void SetFa079_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa079_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa079(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa079(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa079(), "FA079");
        }
        protected abstract ConditionValue getCValueFa079();

        public void SetFa080_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa080_Equal(fRES(v));
        }
        protected void DoSetFa080_Equal(String v) { regFa080(CK_EQ, v); }
        public void SetFa080_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa080_NotEqual(fRES(v));
        }
        protected void DoSetFa080_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_NES, v);
        }
        public void SetFa080_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_GT, fRES(v));
        }
        public void SetFa080_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_LT, fRES(v));
        }
        public void SetFa080_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_GE, fRES(v));
        }
        public void SetFa080_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_LE, fRES(v));
        }
        public void SetFa080_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa080(), "FA080");
        }
        public void SetFa080_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa080(), "FA080");
        }
        public void SetFa080_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa080_LikeSearch(v, cLSOP());
        }
        public void SetFa080_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa080(), "FA080", option);
        }
        public void SetFa080_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa080(), "FA080", option);
        }
        public void SetFa080_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa080_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa080(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa080(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa080(), "FA080");
        }
        protected abstract ConditionValue getCValueFa080();

        public void SetFa081_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa081_Equal(fRES(v));
        }
        protected void DoSetFa081_Equal(String v) { regFa081(CK_EQ, v); }
        public void SetFa081_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa081_NotEqual(fRES(v));
        }
        protected void DoSetFa081_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_NES, v);
        }
        public void SetFa081_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_GT, fRES(v));
        }
        public void SetFa081_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_LT, fRES(v));
        }
        public void SetFa081_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_GE, fRES(v));
        }
        public void SetFa081_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_LE, fRES(v));
        }
        public void SetFa081_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa081(), "FA081");
        }
        public void SetFa081_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa081(), "FA081");
        }
        public void SetFa081_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa081_LikeSearch(v, cLSOP());
        }
        public void SetFa081_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa081(), "FA081", option);
        }
        public void SetFa081_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa081(), "FA081", option);
        }
        public void SetFa081_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa081_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa081(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa081(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa081(), "FA081");
        }
        protected abstract ConditionValue getCValueFa081();

        public void SetFa082_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa082_Equal(fRES(v));
        }
        protected void DoSetFa082_Equal(String v) { regFa082(CK_EQ, v); }
        public void SetFa082_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa082_NotEqual(fRES(v));
        }
        protected void DoSetFa082_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_NES, v);
        }
        public void SetFa082_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_GT, fRES(v));
        }
        public void SetFa082_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_LT, fRES(v));
        }
        public void SetFa082_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_GE, fRES(v));
        }
        public void SetFa082_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_LE, fRES(v));
        }
        public void SetFa082_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa082(), "FA082");
        }
        public void SetFa082_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa082(), "FA082");
        }
        public void SetFa082_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa082_LikeSearch(v, cLSOP());
        }
        public void SetFa082_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa082(), "FA082", option);
        }
        public void SetFa082_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa082(), "FA082", option);
        }
        public void SetFa082_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa082_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa082(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa082(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa082(), "FA082");
        }
        protected abstract ConditionValue getCValueFa082();

        public void SetFa083_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa083_Equal(fRES(v));
        }
        protected void DoSetFa083_Equal(String v) { regFa083(CK_EQ, v); }
        public void SetFa083_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa083_NotEqual(fRES(v));
        }
        protected void DoSetFa083_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_NES, v);
        }
        public void SetFa083_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_GT, fRES(v));
        }
        public void SetFa083_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_LT, fRES(v));
        }
        public void SetFa083_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_GE, fRES(v));
        }
        public void SetFa083_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_LE, fRES(v));
        }
        public void SetFa083_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa083(), "FA083");
        }
        public void SetFa083_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa083(), "FA083");
        }
        public void SetFa083_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa083_LikeSearch(v, cLSOP());
        }
        public void SetFa083_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa083(), "FA083", option);
        }
        public void SetFa083_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa083(), "FA083", option);
        }
        public void SetFa083_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa083_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa083(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa083(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa083(), "FA083");
        }
        protected abstract ConditionValue getCValueFa083();

        public void SetFa084_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa084_Equal(fRES(v));
        }
        protected void DoSetFa084_Equal(String v) { regFa084(CK_EQ, v); }
        public void SetFa084_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa084_NotEqual(fRES(v));
        }
        protected void DoSetFa084_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_NES, v);
        }
        public void SetFa084_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_GT, fRES(v));
        }
        public void SetFa084_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_LT, fRES(v));
        }
        public void SetFa084_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_GE, fRES(v));
        }
        public void SetFa084_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_LE, fRES(v));
        }
        public void SetFa084_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa084(), "FA084");
        }
        public void SetFa084_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa084(), "FA084");
        }
        public void SetFa084_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa084_LikeSearch(v, cLSOP());
        }
        public void SetFa084_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa084(), "FA084", option);
        }
        public void SetFa084_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa084(), "FA084", option);
        }
        public void SetFa084_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa084_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa084(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa084(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa084(), "FA084");
        }
        protected abstract ConditionValue getCValueFa084();

        public void SetFa085_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa085_Equal(fRES(v));
        }
        protected void DoSetFa085_Equal(String v) { regFa085(CK_EQ, v); }
        public void SetFa085_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa085_NotEqual(fRES(v));
        }
        protected void DoSetFa085_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_NES, v);
        }
        public void SetFa085_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_GT, fRES(v));
        }
        public void SetFa085_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_LT, fRES(v));
        }
        public void SetFa085_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_GE, fRES(v));
        }
        public void SetFa085_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_LE, fRES(v));
        }
        public void SetFa085_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa085(), "FA085");
        }
        public void SetFa085_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa085(), "FA085");
        }
        public void SetFa085_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa085_LikeSearch(v, cLSOP());
        }
        public void SetFa085_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa085(), "FA085", option);
        }
        public void SetFa085_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa085(), "FA085", option);
        }
        public void SetFa085_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa085_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa085(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa085(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa085(), "FA085");
        }
        protected abstract ConditionValue getCValueFa085();

        public void SetFa086_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa086_Equal(fRES(v));
        }
        protected void DoSetFa086_Equal(String v) { regFa086(CK_EQ, v); }
        public void SetFa086_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa086_NotEqual(fRES(v));
        }
        protected void DoSetFa086_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_NES, v);
        }
        public void SetFa086_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_GT, fRES(v));
        }
        public void SetFa086_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_LT, fRES(v));
        }
        public void SetFa086_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_GE, fRES(v));
        }
        public void SetFa086_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_LE, fRES(v));
        }
        public void SetFa086_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa086(), "FA086");
        }
        public void SetFa086_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa086(), "FA086");
        }
        public void SetFa086_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa086_LikeSearch(v, cLSOP());
        }
        public void SetFa086_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa086(), "FA086", option);
        }
        public void SetFa086_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa086(), "FA086", option);
        }
        public void SetFa086_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa086_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa086(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa086(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa086(), "FA086");
        }
        protected abstract ConditionValue getCValueFa086();

        public void SetFa087_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa087_Equal(fRES(v));
        }
        protected void DoSetFa087_Equal(String v) { regFa087(CK_EQ, v); }
        public void SetFa087_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa087_NotEqual(fRES(v));
        }
        protected void DoSetFa087_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_NES, v);
        }
        public void SetFa087_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_GT, fRES(v));
        }
        public void SetFa087_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_LT, fRES(v));
        }
        public void SetFa087_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_GE, fRES(v));
        }
        public void SetFa087_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_LE, fRES(v));
        }
        public void SetFa087_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa087(), "FA087");
        }
        public void SetFa087_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa087(), "FA087");
        }
        public void SetFa087_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa087_LikeSearch(v, cLSOP());
        }
        public void SetFa087_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa087(), "FA087", option);
        }
        public void SetFa087_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa087(), "FA087", option);
        }
        public void SetFa087_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa087_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa087(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa087(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa087(), "FA087");
        }
        protected abstract ConditionValue getCValueFa087();

        public void SetFa088_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa088_Equal(fRES(v));
        }
        protected void DoSetFa088_Equal(String v) { regFa088(CK_EQ, v); }
        public void SetFa088_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa088_NotEqual(fRES(v));
        }
        protected void DoSetFa088_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_NES, v);
        }
        public void SetFa088_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_GT, fRES(v));
        }
        public void SetFa088_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_LT, fRES(v));
        }
        public void SetFa088_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_GE, fRES(v));
        }
        public void SetFa088_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_LE, fRES(v));
        }
        public void SetFa088_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa088(), "FA088");
        }
        public void SetFa088_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa088(), "FA088");
        }
        public void SetFa088_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa088_LikeSearch(v, cLSOP());
        }
        public void SetFa088_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa088(), "FA088", option);
        }
        public void SetFa088_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa088(), "FA088", option);
        }
        public void SetFa088_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa088_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa088(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa088(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa088(), "FA088");
        }
        protected abstract ConditionValue getCValueFa088();

        public void SetFa089_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa089_Equal(fRES(v));
        }
        protected void DoSetFa089_Equal(String v) { regFa089(CK_EQ, v); }
        public void SetFa089_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa089_NotEqual(fRES(v));
        }
        protected void DoSetFa089_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_NES, v);
        }
        public void SetFa089_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_GT, fRES(v));
        }
        public void SetFa089_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_LT, fRES(v));
        }
        public void SetFa089_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_GE, fRES(v));
        }
        public void SetFa089_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_LE, fRES(v));
        }
        public void SetFa089_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa089(), "FA089");
        }
        public void SetFa089_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa089(), "FA089");
        }
        public void SetFa089_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa089_LikeSearch(v, cLSOP());
        }
        public void SetFa089_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa089(), "FA089", option);
        }
        public void SetFa089_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa089(), "FA089", option);
        }
        public void SetFa089_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa089_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa089(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa089(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa089(), "FA089");
        }
        protected abstract ConditionValue getCValueFa089();

        public void SetFa090_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa090_Equal(fRES(v));
        }
        protected void DoSetFa090_Equal(String v) { regFa090(CK_EQ, v); }
        public void SetFa090_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa090_NotEqual(fRES(v));
        }
        protected void DoSetFa090_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_NES, v);
        }
        public void SetFa090_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_GT, fRES(v));
        }
        public void SetFa090_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_LT, fRES(v));
        }
        public void SetFa090_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_GE, fRES(v));
        }
        public void SetFa090_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_LE, fRES(v));
        }
        public void SetFa090_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa090(), "FA090");
        }
        public void SetFa090_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa090(), "FA090");
        }
        public void SetFa090_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa090_LikeSearch(v, cLSOP());
        }
        public void SetFa090_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa090(), "FA090", option);
        }
        public void SetFa090_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa090(), "FA090", option);
        }
        public void SetFa090_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa090_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa090(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa090(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa090(), "FA090");
        }
        protected abstract ConditionValue getCValueFa090();

        public void SetFa091_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa091_Equal(fRES(v));
        }
        protected void DoSetFa091_Equal(String v) { regFa091(CK_EQ, v); }
        public void SetFa091_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa091_NotEqual(fRES(v));
        }
        protected void DoSetFa091_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_NES, v);
        }
        public void SetFa091_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_GT, fRES(v));
        }
        public void SetFa091_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_LT, fRES(v));
        }
        public void SetFa091_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_GE, fRES(v));
        }
        public void SetFa091_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_LE, fRES(v));
        }
        public void SetFa091_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa091(), "FA091");
        }
        public void SetFa091_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa091(), "FA091");
        }
        public void SetFa091_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa091_LikeSearch(v, cLSOP());
        }
        public void SetFa091_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa091(), "FA091", option);
        }
        public void SetFa091_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa091(), "FA091", option);
        }
        public void SetFa091_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa091_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa091(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa091(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa091(), "FA091");
        }
        protected abstract ConditionValue getCValueFa091();

        public void SetFa092_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa092_Equal(fRES(v));
        }
        protected void DoSetFa092_Equal(String v) { regFa092(CK_EQ, v); }
        public void SetFa092_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa092_NotEqual(fRES(v));
        }
        protected void DoSetFa092_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_NES, v);
        }
        public void SetFa092_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_GT, fRES(v));
        }
        public void SetFa092_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_LT, fRES(v));
        }
        public void SetFa092_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_GE, fRES(v));
        }
        public void SetFa092_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_LE, fRES(v));
        }
        public void SetFa092_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa092(), "FA092");
        }
        public void SetFa092_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa092(), "FA092");
        }
        public void SetFa092_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa092_LikeSearch(v, cLSOP());
        }
        public void SetFa092_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa092(), "FA092", option);
        }
        public void SetFa092_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa092(), "FA092", option);
        }
        public void SetFa092_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa092_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa092(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa092(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa092(), "FA092");
        }
        protected abstract ConditionValue getCValueFa092();

        public void SetFa093_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa093_Equal(fRES(v));
        }
        protected void DoSetFa093_Equal(String v) { regFa093(CK_EQ, v); }
        public void SetFa093_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa093_NotEqual(fRES(v));
        }
        protected void DoSetFa093_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_NES, v);
        }
        public void SetFa093_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_GT, fRES(v));
        }
        public void SetFa093_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_LT, fRES(v));
        }
        public void SetFa093_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_GE, fRES(v));
        }
        public void SetFa093_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_LE, fRES(v));
        }
        public void SetFa093_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa093(), "FA093");
        }
        public void SetFa093_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa093(), "FA093");
        }
        public void SetFa093_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa093_LikeSearch(v, cLSOP());
        }
        public void SetFa093_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa093(), "FA093", option);
        }
        public void SetFa093_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa093(), "FA093", option);
        }
        public void SetFa093_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa093_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa093(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa093(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa093(), "FA093");
        }
        protected abstract ConditionValue getCValueFa093();

        public void SetFa094_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa094_Equal(fRES(v));
        }
        protected void DoSetFa094_Equal(String v) { regFa094(CK_EQ, v); }
        public void SetFa094_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa094_NotEqual(fRES(v));
        }
        protected void DoSetFa094_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_NES, v);
        }
        public void SetFa094_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_GT, fRES(v));
        }
        public void SetFa094_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_LT, fRES(v));
        }
        public void SetFa094_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_GE, fRES(v));
        }
        public void SetFa094_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_LE, fRES(v));
        }
        public void SetFa094_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa094(), "FA094");
        }
        public void SetFa094_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa094(), "FA094");
        }
        public void SetFa094_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa094_LikeSearch(v, cLSOP());
        }
        public void SetFa094_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa094(), "FA094", option);
        }
        public void SetFa094_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa094(), "FA094", option);
        }
        public void SetFa094_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa094_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa094(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa094(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa094(), "FA094");
        }
        protected abstract ConditionValue getCValueFa094();

        public void SetFa095_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa095_Equal(fRES(v));
        }
        protected void DoSetFa095_Equal(String v) { regFa095(CK_EQ, v); }
        public void SetFa095_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa095_NotEqual(fRES(v));
        }
        protected void DoSetFa095_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_NES, v);
        }
        public void SetFa095_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_GT, fRES(v));
        }
        public void SetFa095_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_LT, fRES(v));
        }
        public void SetFa095_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_GE, fRES(v));
        }
        public void SetFa095_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_LE, fRES(v));
        }
        public void SetFa095_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa095(), "FA095");
        }
        public void SetFa095_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa095(), "FA095");
        }
        public void SetFa095_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa095_LikeSearch(v, cLSOP());
        }
        public void SetFa095_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa095(), "FA095", option);
        }
        public void SetFa095_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa095(), "FA095", option);
        }
        public void SetFa095_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa095_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa095(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa095(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa095(), "FA095");
        }
        protected abstract ConditionValue getCValueFa095();

        public void SetFa096_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa096_Equal(fRES(v));
        }
        protected void DoSetFa096_Equal(String v) { regFa096(CK_EQ, v); }
        public void SetFa096_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa096_NotEqual(fRES(v));
        }
        protected void DoSetFa096_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_NES, v);
        }
        public void SetFa096_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_GT, fRES(v));
        }
        public void SetFa096_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_LT, fRES(v));
        }
        public void SetFa096_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_GE, fRES(v));
        }
        public void SetFa096_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_LE, fRES(v));
        }
        public void SetFa096_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa096(), "FA096");
        }
        public void SetFa096_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa096(), "FA096");
        }
        public void SetFa096_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa096_LikeSearch(v, cLSOP());
        }
        public void SetFa096_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa096(), "FA096", option);
        }
        public void SetFa096_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa096(), "FA096", option);
        }
        public void SetFa096_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa096_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa096(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa096(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa096(), "FA096");
        }
        protected abstract ConditionValue getCValueFa096();

        public void SetFa097_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa097_Equal(fRES(v));
        }
        protected void DoSetFa097_Equal(String v) { regFa097(CK_EQ, v); }
        public void SetFa097_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa097_NotEqual(fRES(v));
        }
        protected void DoSetFa097_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_NES, v);
        }
        public void SetFa097_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_GT, fRES(v));
        }
        public void SetFa097_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_LT, fRES(v));
        }
        public void SetFa097_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_GE, fRES(v));
        }
        public void SetFa097_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_LE, fRES(v));
        }
        public void SetFa097_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa097(), "FA097");
        }
        public void SetFa097_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa097(), "FA097");
        }
        public void SetFa097_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa097_LikeSearch(v, cLSOP());
        }
        public void SetFa097_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa097(), "FA097", option);
        }
        public void SetFa097_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa097(), "FA097", option);
        }
        public void SetFa097_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa097_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa097(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa097(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa097(), "FA097");
        }
        protected abstract ConditionValue getCValueFa097();

        public void SetFa098_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa098_Equal(fRES(v));
        }
        protected void DoSetFa098_Equal(String v) { regFa098(CK_EQ, v); }
        public void SetFa098_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa098_NotEqual(fRES(v));
        }
        protected void DoSetFa098_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_NES, v);
        }
        public void SetFa098_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_GT, fRES(v));
        }
        public void SetFa098_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_LT, fRES(v));
        }
        public void SetFa098_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_GE, fRES(v));
        }
        public void SetFa098_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_LE, fRES(v));
        }
        public void SetFa098_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa098(), "FA098");
        }
        public void SetFa098_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa098(), "FA098");
        }
        public void SetFa098_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa098_LikeSearch(v, cLSOP());
        }
        public void SetFa098_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa098(), "FA098", option);
        }
        public void SetFa098_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa098(), "FA098", option);
        }
        public void SetFa098_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa098_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa098(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa098(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa098(), "FA098");
        }
        protected abstract ConditionValue getCValueFa098();

        public void SetFa099_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa099_Equal(fRES(v));
        }
        protected void DoSetFa099_Equal(String v) { regFa099(CK_EQ, v); }
        public void SetFa099_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa099_NotEqual(fRES(v));
        }
        protected void DoSetFa099_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_NES, v);
        }
        public void SetFa099_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_GT, fRES(v));
        }
        public void SetFa099_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_LT, fRES(v));
        }
        public void SetFa099_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_GE, fRES(v));
        }
        public void SetFa099_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_LE, fRES(v));
        }
        public void SetFa099_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa099(), "FA099");
        }
        public void SetFa099_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa099(), "FA099");
        }
        public void SetFa099_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa099_LikeSearch(v, cLSOP());
        }
        public void SetFa099_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa099(), "FA099", option);
        }
        public void SetFa099_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa099(), "FA099", option);
        }
        public void SetFa099_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa099_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa099(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa099(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa099(), "FA099");
        }
        protected abstract ConditionValue getCValueFa099();

        public void SetFa100_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa100_Equal(fRES(v));
        }
        protected void DoSetFa100_Equal(String v) { regFa100(CK_EQ, v); }
        public void SetFa100_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa100_NotEqual(fRES(v));
        }
        protected void DoSetFa100_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_NES, v);
        }
        public void SetFa100_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_GT, fRES(v));
        }
        public void SetFa100_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_LT, fRES(v));
        }
        public void SetFa100_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_GE, fRES(v));
        }
        public void SetFa100_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_LE, fRES(v));
        }
        public void SetFa100_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa100(), "FA100");
        }
        public void SetFa100_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa100(), "FA100");
        }
        public void SetFa100_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa100_LikeSearch(v, cLSOP());
        }
        public void SetFa100_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa100(), "FA100", option);
        }
        public void SetFa100_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa100(), "FA100", option);
        }
        public void SetFa100_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa100_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa100(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa100(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa100(), "FA100");
        }
        protected abstract ConditionValue getCValueFa100();

        public void SetFa101_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa101_Equal(fRES(v));
        }
        protected void DoSetFa101_Equal(String v) { regFa101(CK_EQ, v); }
        public void SetFa101_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa101_NotEqual(fRES(v));
        }
        protected void DoSetFa101_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_NES, v);
        }
        public void SetFa101_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_GT, fRES(v));
        }
        public void SetFa101_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_LT, fRES(v));
        }
        public void SetFa101_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_GE, fRES(v));
        }
        public void SetFa101_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_LE, fRES(v));
        }
        public void SetFa101_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa101(), "FA101");
        }
        public void SetFa101_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa101(), "FA101");
        }
        public void SetFa101_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa101_LikeSearch(v, cLSOP());
        }
        public void SetFa101_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa101(), "FA101", option);
        }
        public void SetFa101_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa101(), "FA101", option);
        }
        public void SetFa101_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa101_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa101(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa101(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa101(), "FA101");
        }
        protected abstract ConditionValue getCValueFa101();

        public void SetFa102_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa102_Equal(fRES(v));
        }
        protected void DoSetFa102_Equal(String v) { regFa102(CK_EQ, v); }
        public void SetFa102_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa102_NotEqual(fRES(v));
        }
        protected void DoSetFa102_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_NES, v);
        }
        public void SetFa102_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_GT, fRES(v));
        }
        public void SetFa102_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_LT, fRES(v));
        }
        public void SetFa102_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_GE, fRES(v));
        }
        public void SetFa102_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_LE, fRES(v));
        }
        public void SetFa102_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa102(), "FA102");
        }
        public void SetFa102_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa102(), "FA102");
        }
        public void SetFa102_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa102_LikeSearch(v, cLSOP());
        }
        public void SetFa102_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa102(), "FA102", option);
        }
        public void SetFa102_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa102(), "FA102", option);
        }
        public void SetFa102_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa102_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa102(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa102(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa102(), "FA102");
        }
        protected abstract ConditionValue getCValueFa102();

        public void SetFa103_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa103_Equal(fRES(v));
        }
        protected void DoSetFa103_Equal(String v) { regFa103(CK_EQ, v); }
        public void SetFa103_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa103_NotEqual(fRES(v));
        }
        protected void DoSetFa103_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_NES, v);
        }
        public void SetFa103_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_GT, fRES(v));
        }
        public void SetFa103_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_LT, fRES(v));
        }
        public void SetFa103_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_GE, fRES(v));
        }
        public void SetFa103_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_LE, fRES(v));
        }
        public void SetFa103_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa103(), "FA103");
        }
        public void SetFa103_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa103(), "FA103");
        }
        public void SetFa103_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa103_LikeSearch(v, cLSOP());
        }
        public void SetFa103_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa103(), "FA103", option);
        }
        public void SetFa103_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa103(), "FA103", option);
        }
        public void SetFa103_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa103_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa103(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa103(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa103(), "FA103");
        }
        protected abstract ConditionValue getCValueFa103();

        public void SetFa104_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa104_Equal(fRES(v));
        }
        protected void DoSetFa104_Equal(String v) { regFa104(CK_EQ, v); }
        public void SetFa104_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa104_NotEqual(fRES(v));
        }
        protected void DoSetFa104_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_NES, v);
        }
        public void SetFa104_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_GT, fRES(v));
        }
        public void SetFa104_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_LT, fRES(v));
        }
        public void SetFa104_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_GE, fRES(v));
        }
        public void SetFa104_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_LE, fRES(v));
        }
        public void SetFa104_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa104(), "FA104");
        }
        public void SetFa104_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa104(), "FA104");
        }
        public void SetFa104_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa104_LikeSearch(v, cLSOP());
        }
        public void SetFa104_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa104(), "FA104", option);
        }
        public void SetFa104_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa104(), "FA104", option);
        }
        public void SetFa104_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa104_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa104(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa104(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa104(), "FA104");
        }
        protected abstract ConditionValue getCValueFa104();

        public void SetFa105_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa105_Equal(fRES(v));
        }
        protected void DoSetFa105_Equal(String v) { regFa105(CK_EQ, v); }
        public void SetFa105_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa105_NotEqual(fRES(v));
        }
        protected void DoSetFa105_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_NES, v);
        }
        public void SetFa105_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_GT, fRES(v));
        }
        public void SetFa105_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_LT, fRES(v));
        }
        public void SetFa105_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_GE, fRES(v));
        }
        public void SetFa105_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_LE, fRES(v));
        }
        public void SetFa105_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa105(), "FA105");
        }
        public void SetFa105_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa105(), "FA105");
        }
        public void SetFa105_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa105_LikeSearch(v, cLSOP());
        }
        public void SetFa105_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa105(), "FA105", option);
        }
        public void SetFa105_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa105(), "FA105", option);
        }
        public void SetFa105_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa105_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa105(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa105(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa105(), "FA105");
        }
        protected abstract ConditionValue getCValueFa105();

        public void SetFa106_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa106_Equal(fRES(v));
        }
        protected void DoSetFa106_Equal(String v) { regFa106(CK_EQ, v); }
        public void SetFa106_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa106_NotEqual(fRES(v));
        }
        protected void DoSetFa106_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_NES, v);
        }
        public void SetFa106_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_GT, fRES(v));
        }
        public void SetFa106_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_LT, fRES(v));
        }
        public void SetFa106_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_GE, fRES(v));
        }
        public void SetFa106_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_LE, fRES(v));
        }
        public void SetFa106_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa106(), "FA106");
        }
        public void SetFa106_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa106(), "FA106");
        }
        public void SetFa106_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa106_LikeSearch(v, cLSOP());
        }
        public void SetFa106_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa106(), "FA106", option);
        }
        public void SetFa106_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa106(), "FA106", option);
        }
        public void SetFa106_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa106_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa106(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa106(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa106(), "FA106");
        }
        protected abstract ConditionValue getCValueFa106();

        public void SetFa107_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa107_Equal(fRES(v));
        }
        protected void DoSetFa107_Equal(String v) { regFa107(CK_EQ, v); }
        public void SetFa107_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa107_NotEqual(fRES(v));
        }
        protected void DoSetFa107_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_NES, v);
        }
        public void SetFa107_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_GT, fRES(v));
        }
        public void SetFa107_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_LT, fRES(v));
        }
        public void SetFa107_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_GE, fRES(v));
        }
        public void SetFa107_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_LE, fRES(v));
        }
        public void SetFa107_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa107(), "FA107");
        }
        public void SetFa107_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa107(), "FA107");
        }
        public void SetFa107_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa107_LikeSearch(v, cLSOP());
        }
        public void SetFa107_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa107(), "FA107", option);
        }
        public void SetFa107_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa107(), "FA107", option);
        }
        public void SetFa107_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa107_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa107(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa107(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa107(), "FA107");
        }
        protected abstract ConditionValue getCValueFa107();

        public void SetFa108_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa108_Equal(fRES(v));
        }
        protected void DoSetFa108_Equal(String v) { regFa108(CK_EQ, v); }
        public void SetFa108_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa108_NotEqual(fRES(v));
        }
        protected void DoSetFa108_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_NES, v);
        }
        public void SetFa108_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_GT, fRES(v));
        }
        public void SetFa108_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_LT, fRES(v));
        }
        public void SetFa108_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_GE, fRES(v));
        }
        public void SetFa108_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_LE, fRES(v));
        }
        public void SetFa108_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa108(), "FA108");
        }
        public void SetFa108_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa108(), "FA108");
        }
        public void SetFa108_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa108_LikeSearch(v, cLSOP());
        }
        public void SetFa108_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa108(), "FA108", option);
        }
        public void SetFa108_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa108(), "FA108", option);
        }
        public void SetFa108_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa108_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa108(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa108(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa108(), "FA108");
        }
        protected abstract ConditionValue getCValueFa108();

        public void SetFa109_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa109_Equal(fRES(v));
        }
        protected void DoSetFa109_Equal(String v) { regFa109(CK_EQ, v); }
        public void SetFa109_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa109_NotEqual(fRES(v));
        }
        protected void DoSetFa109_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_NES, v);
        }
        public void SetFa109_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_GT, fRES(v));
        }
        public void SetFa109_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_LT, fRES(v));
        }
        public void SetFa109_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_GE, fRES(v));
        }
        public void SetFa109_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_LE, fRES(v));
        }
        public void SetFa109_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa109(), "FA109");
        }
        public void SetFa109_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa109(), "FA109");
        }
        public void SetFa109_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa109_LikeSearch(v, cLSOP());
        }
        public void SetFa109_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa109(), "FA109", option);
        }
        public void SetFa109_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa109(), "FA109", option);
        }
        public void SetFa109_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa109_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa109(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa109(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa109(), "FA109");
        }
        protected abstract ConditionValue getCValueFa109();

        public void SetFa110_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa110_Equal(fRES(v));
        }
        protected void DoSetFa110_Equal(String v) { regFa110(CK_EQ, v); }
        public void SetFa110_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa110_NotEqual(fRES(v));
        }
        protected void DoSetFa110_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_NES, v);
        }
        public void SetFa110_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_GT, fRES(v));
        }
        public void SetFa110_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_LT, fRES(v));
        }
        public void SetFa110_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_GE, fRES(v));
        }
        public void SetFa110_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_LE, fRES(v));
        }
        public void SetFa110_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa110(), "FA110");
        }
        public void SetFa110_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa110(), "FA110");
        }
        public void SetFa110_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa110_LikeSearch(v, cLSOP());
        }
        public void SetFa110_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa110(), "FA110", option);
        }
        public void SetFa110_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa110(), "FA110", option);
        }
        public void SetFa110_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa110_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa110(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa110(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa110(), "FA110");
        }
        protected abstract ConditionValue getCValueFa110();

        public void SetFa111_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa111_Equal(fRES(v));
        }
        protected void DoSetFa111_Equal(String v) { regFa111(CK_EQ, v); }
        public void SetFa111_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa111_NotEqual(fRES(v));
        }
        protected void DoSetFa111_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_NES, v);
        }
        public void SetFa111_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_GT, fRES(v));
        }
        public void SetFa111_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_LT, fRES(v));
        }
        public void SetFa111_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_GE, fRES(v));
        }
        public void SetFa111_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_LE, fRES(v));
        }
        public void SetFa111_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa111(), "FA111");
        }
        public void SetFa111_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa111(), "FA111");
        }
        public void SetFa111_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa111_LikeSearch(v, cLSOP());
        }
        public void SetFa111_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa111(), "FA111", option);
        }
        public void SetFa111_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa111(), "FA111", option);
        }
        public void SetFa111_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa111_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa111(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa111(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa111(), "FA111");
        }
        protected abstract ConditionValue getCValueFa111();

        public void SetFa112_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa112_Equal(fRES(v));
        }
        protected void DoSetFa112_Equal(String v) { regFa112(CK_EQ, v); }
        public void SetFa112_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa112_NotEqual(fRES(v));
        }
        protected void DoSetFa112_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_NES, v);
        }
        public void SetFa112_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_GT, fRES(v));
        }
        public void SetFa112_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_LT, fRES(v));
        }
        public void SetFa112_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_GE, fRES(v));
        }
        public void SetFa112_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_LE, fRES(v));
        }
        public void SetFa112_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa112(), "FA112");
        }
        public void SetFa112_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa112(), "FA112");
        }
        public void SetFa112_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa112_LikeSearch(v, cLSOP());
        }
        public void SetFa112_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa112(), "FA112", option);
        }
        public void SetFa112_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa112(), "FA112", option);
        }
        public void SetFa112_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa112_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa112(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa112(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa112(), "FA112");
        }
        protected abstract ConditionValue getCValueFa112();

        public void SetFa113_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa113_Equal(fRES(v));
        }
        protected void DoSetFa113_Equal(String v) { regFa113(CK_EQ, v); }
        public void SetFa113_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa113_NotEqual(fRES(v));
        }
        protected void DoSetFa113_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_NES, v);
        }
        public void SetFa113_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_GT, fRES(v));
        }
        public void SetFa113_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_LT, fRES(v));
        }
        public void SetFa113_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_GE, fRES(v));
        }
        public void SetFa113_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_LE, fRES(v));
        }
        public void SetFa113_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa113(), "FA113");
        }
        public void SetFa113_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa113(), "FA113");
        }
        public void SetFa113_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa113_LikeSearch(v, cLSOP());
        }
        public void SetFa113_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa113(), "FA113", option);
        }
        public void SetFa113_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa113(), "FA113", option);
        }
        public void SetFa113_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa113_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa113(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa113(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa113(), "FA113");
        }
        protected abstract ConditionValue getCValueFa113();

        public void SetFa114_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa114_Equal(fRES(v));
        }
        protected void DoSetFa114_Equal(String v) { regFa114(CK_EQ, v); }
        public void SetFa114_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa114_NotEqual(fRES(v));
        }
        protected void DoSetFa114_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_NES, v);
        }
        public void SetFa114_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_GT, fRES(v));
        }
        public void SetFa114_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_LT, fRES(v));
        }
        public void SetFa114_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_GE, fRES(v));
        }
        public void SetFa114_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_LE, fRES(v));
        }
        public void SetFa114_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa114(), "FA114");
        }
        public void SetFa114_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa114(), "FA114");
        }
        public void SetFa114_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa114_LikeSearch(v, cLSOP());
        }
        public void SetFa114_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa114(), "FA114", option);
        }
        public void SetFa114_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa114(), "FA114", option);
        }
        public void SetFa114_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa114_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa114(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa114(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa114(), "FA114");
        }
        protected abstract ConditionValue getCValueFa114();

        public void SetFa115_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa115_Equal(fRES(v));
        }
        protected void DoSetFa115_Equal(String v) { regFa115(CK_EQ, v); }
        public void SetFa115_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa115_NotEqual(fRES(v));
        }
        protected void DoSetFa115_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_NES, v);
        }
        public void SetFa115_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_GT, fRES(v));
        }
        public void SetFa115_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_LT, fRES(v));
        }
        public void SetFa115_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_GE, fRES(v));
        }
        public void SetFa115_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_LE, fRES(v));
        }
        public void SetFa115_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa115(), "FA115");
        }
        public void SetFa115_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa115(), "FA115");
        }
        public void SetFa115_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa115_LikeSearch(v, cLSOP());
        }
        public void SetFa115_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa115(), "FA115", option);
        }
        public void SetFa115_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa115(), "FA115", option);
        }
        public void SetFa115_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa115_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa115(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa115(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa115(), "FA115");
        }
        protected abstract ConditionValue getCValueFa115();

        public void SetFa116_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa116_Equal(fRES(v));
        }
        protected void DoSetFa116_Equal(String v) { regFa116(CK_EQ, v); }
        public void SetFa116_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa116_NotEqual(fRES(v));
        }
        protected void DoSetFa116_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_NES, v);
        }
        public void SetFa116_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_GT, fRES(v));
        }
        public void SetFa116_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_LT, fRES(v));
        }
        public void SetFa116_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_GE, fRES(v));
        }
        public void SetFa116_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_LE, fRES(v));
        }
        public void SetFa116_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa116(), "FA116");
        }
        public void SetFa116_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa116(), "FA116");
        }
        public void SetFa116_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa116_LikeSearch(v, cLSOP());
        }
        public void SetFa116_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa116(), "FA116", option);
        }
        public void SetFa116_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa116(), "FA116", option);
        }
        public void SetFa116_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa116_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa116(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa116(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa116(), "FA116");
        }
        protected abstract ConditionValue getCValueFa116();

        public void SetFa117_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa117_Equal(fRES(v));
        }
        protected void DoSetFa117_Equal(String v) { regFa117(CK_EQ, v); }
        public void SetFa117_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa117_NotEqual(fRES(v));
        }
        protected void DoSetFa117_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_NES, v);
        }
        public void SetFa117_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_GT, fRES(v));
        }
        public void SetFa117_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_LT, fRES(v));
        }
        public void SetFa117_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_GE, fRES(v));
        }
        public void SetFa117_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_LE, fRES(v));
        }
        public void SetFa117_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa117(), "FA117");
        }
        public void SetFa117_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa117(), "FA117");
        }
        public void SetFa117_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa117_LikeSearch(v, cLSOP());
        }
        public void SetFa117_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa117(), "FA117", option);
        }
        public void SetFa117_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa117(), "FA117", option);
        }
        public void SetFa117_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa117_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa117(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa117(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa117(), "FA117");
        }
        protected abstract ConditionValue getCValueFa117();

        public void SetFa118_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa118_Equal(fRES(v));
        }
        protected void DoSetFa118_Equal(String v) { regFa118(CK_EQ, v); }
        public void SetFa118_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa118_NotEqual(fRES(v));
        }
        protected void DoSetFa118_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_NES, v);
        }
        public void SetFa118_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_GT, fRES(v));
        }
        public void SetFa118_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_LT, fRES(v));
        }
        public void SetFa118_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_GE, fRES(v));
        }
        public void SetFa118_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_LE, fRES(v));
        }
        public void SetFa118_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa118(), "FA118");
        }
        public void SetFa118_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa118(), "FA118");
        }
        public void SetFa118_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa118_LikeSearch(v, cLSOP());
        }
        public void SetFa118_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa118(), "FA118", option);
        }
        public void SetFa118_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa118(), "FA118", option);
        }
        public void SetFa118_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa118_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa118(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa118(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa118(), "FA118");
        }
        protected abstract ConditionValue getCValueFa118();

        public void SetFa119_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa119_Equal(fRES(v));
        }
        protected void DoSetFa119_Equal(String v) { regFa119(CK_EQ, v); }
        public void SetFa119_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa119_NotEqual(fRES(v));
        }
        protected void DoSetFa119_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_NES, v);
        }
        public void SetFa119_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_GT, fRES(v));
        }
        public void SetFa119_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_LT, fRES(v));
        }
        public void SetFa119_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_GE, fRES(v));
        }
        public void SetFa119_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_LE, fRES(v));
        }
        public void SetFa119_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa119(), "FA119");
        }
        public void SetFa119_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa119(), "FA119");
        }
        public void SetFa119_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa119_LikeSearch(v, cLSOP());
        }
        public void SetFa119_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa119(), "FA119", option);
        }
        public void SetFa119_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa119(), "FA119", option);
        }
        public void SetFa119_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa119_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa119(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa119(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa119(), "FA119");
        }
        protected abstract ConditionValue getCValueFa119();

        public void SetFa120_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa120_Equal(fRES(v));
        }
        protected void DoSetFa120_Equal(String v) { regFa120(CK_EQ, v); }
        public void SetFa120_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa120_NotEqual(fRES(v));
        }
        protected void DoSetFa120_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_NES, v);
        }
        public void SetFa120_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_GT, fRES(v));
        }
        public void SetFa120_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_LT, fRES(v));
        }
        public void SetFa120_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_GE, fRES(v));
        }
        public void SetFa120_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_LE, fRES(v));
        }
        public void SetFa120_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa120(), "FA120");
        }
        public void SetFa120_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa120(), "FA120");
        }
        public void SetFa120_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa120_LikeSearch(v, cLSOP());
        }
        public void SetFa120_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa120(), "FA120", option);
        }
        public void SetFa120_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa120(), "FA120", option);
        }
        public void SetFa120_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa120_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa120(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa120(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa120(), "FA120");
        }
        protected abstract ConditionValue getCValueFa120();

        public void SetFa121_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa121_Equal(fRES(v));
        }
        protected void DoSetFa121_Equal(String v) { regFa121(CK_EQ, v); }
        public void SetFa121_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa121_NotEqual(fRES(v));
        }
        protected void DoSetFa121_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_NES, v);
        }
        public void SetFa121_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_GT, fRES(v));
        }
        public void SetFa121_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_LT, fRES(v));
        }
        public void SetFa121_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_GE, fRES(v));
        }
        public void SetFa121_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_LE, fRES(v));
        }
        public void SetFa121_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa121(), "FA121");
        }
        public void SetFa121_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa121(), "FA121");
        }
        public void SetFa121_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa121_LikeSearch(v, cLSOP());
        }
        public void SetFa121_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa121(), "FA121", option);
        }
        public void SetFa121_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa121(), "FA121", option);
        }
        public void SetFa121_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa121_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa121(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa121(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa121(), "FA121");
        }
        protected abstract ConditionValue getCValueFa121();

        public void SetFa122_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa122_Equal(fRES(v));
        }
        protected void DoSetFa122_Equal(String v) { regFa122(CK_EQ, v); }
        public void SetFa122_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa122_NotEqual(fRES(v));
        }
        protected void DoSetFa122_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_NES, v);
        }
        public void SetFa122_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_GT, fRES(v));
        }
        public void SetFa122_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_LT, fRES(v));
        }
        public void SetFa122_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_GE, fRES(v));
        }
        public void SetFa122_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_LE, fRES(v));
        }
        public void SetFa122_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa122(), "FA122");
        }
        public void SetFa122_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa122(), "FA122");
        }
        public void SetFa122_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa122_LikeSearch(v, cLSOP());
        }
        public void SetFa122_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa122(), "FA122", option);
        }
        public void SetFa122_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa122(), "FA122", option);
        }
        public void SetFa122_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa122_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa122(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa122(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa122(), "FA122");
        }
        protected abstract ConditionValue getCValueFa122();

        public void SetFa123_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa123_Equal(fRES(v));
        }
        protected void DoSetFa123_Equal(String v) { regFa123(CK_EQ, v); }
        public void SetFa123_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa123_NotEqual(fRES(v));
        }
        protected void DoSetFa123_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_NES, v);
        }
        public void SetFa123_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_GT, fRES(v));
        }
        public void SetFa123_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_LT, fRES(v));
        }
        public void SetFa123_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_GE, fRES(v));
        }
        public void SetFa123_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_LE, fRES(v));
        }
        public void SetFa123_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa123(), "FA123");
        }
        public void SetFa123_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa123(), "FA123");
        }
        public void SetFa123_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa123_LikeSearch(v, cLSOP());
        }
        public void SetFa123_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa123(), "FA123", option);
        }
        public void SetFa123_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa123(), "FA123", option);
        }
        public void SetFa123_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa123_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa123(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa123(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa123(), "FA123");
        }
        protected abstract ConditionValue getCValueFa123();

        public void SetFa124_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa124_Equal(fRES(v));
        }
        protected void DoSetFa124_Equal(String v) { regFa124(CK_EQ, v); }
        public void SetFa124_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa124_NotEqual(fRES(v));
        }
        protected void DoSetFa124_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_NES, v);
        }
        public void SetFa124_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_GT, fRES(v));
        }
        public void SetFa124_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_LT, fRES(v));
        }
        public void SetFa124_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_GE, fRES(v));
        }
        public void SetFa124_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_LE, fRES(v));
        }
        public void SetFa124_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa124(), "FA124");
        }
        public void SetFa124_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa124(), "FA124");
        }
        public void SetFa124_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa124_LikeSearch(v, cLSOP());
        }
        public void SetFa124_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa124(), "FA124", option);
        }
        public void SetFa124_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa124(), "FA124", option);
        }
        public void SetFa124_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa124_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa124(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa124(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa124(), "FA124");
        }
        protected abstract ConditionValue getCValueFa124();

        public void SetFa125_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa125_Equal(fRES(v));
        }
        protected void DoSetFa125_Equal(String v) { regFa125(CK_EQ, v); }
        public void SetFa125_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa125_NotEqual(fRES(v));
        }
        protected void DoSetFa125_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_NES, v);
        }
        public void SetFa125_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_GT, fRES(v));
        }
        public void SetFa125_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_LT, fRES(v));
        }
        public void SetFa125_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_GE, fRES(v));
        }
        public void SetFa125_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_LE, fRES(v));
        }
        public void SetFa125_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa125(), "FA125");
        }
        public void SetFa125_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa125(), "FA125");
        }
        public void SetFa125_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa125_LikeSearch(v, cLSOP());
        }
        public void SetFa125_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa125(), "FA125", option);
        }
        public void SetFa125_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa125(), "FA125", option);
        }
        public void SetFa125_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa125_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa125(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa125(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa125(), "FA125");
        }
        protected abstract ConditionValue getCValueFa125();

        public void SetFa126_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa126_Equal(fRES(v));
        }
        protected void DoSetFa126_Equal(String v) { regFa126(CK_EQ, v); }
        public void SetFa126_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa126_NotEqual(fRES(v));
        }
        protected void DoSetFa126_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_NES, v);
        }
        public void SetFa126_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_GT, fRES(v));
        }
        public void SetFa126_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_LT, fRES(v));
        }
        public void SetFa126_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_GE, fRES(v));
        }
        public void SetFa126_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_LE, fRES(v));
        }
        public void SetFa126_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa126(), "FA126");
        }
        public void SetFa126_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa126(), "FA126");
        }
        public void SetFa126_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa126_LikeSearch(v, cLSOP());
        }
        public void SetFa126_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa126(), "FA126", option);
        }
        public void SetFa126_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa126(), "FA126", option);
        }
        public void SetFa126_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa126_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa126(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa126(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa126(), "FA126");
        }
        protected abstract ConditionValue getCValueFa126();

        public void SetFa127_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa127_Equal(fRES(v));
        }
        protected void DoSetFa127_Equal(String v) { regFa127(CK_EQ, v); }
        public void SetFa127_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa127_NotEqual(fRES(v));
        }
        protected void DoSetFa127_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_NES, v);
        }
        public void SetFa127_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_GT, fRES(v));
        }
        public void SetFa127_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_LT, fRES(v));
        }
        public void SetFa127_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_GE, fRES(v));
        }
        public void SetFa127_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_LE, fRES(v));
        }
        public void SetFa127_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa127(), "FA127");
        }
        public void SetFa127_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa127(), "FA127");
        }
        public void SetFa127_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa127_LikeSearch(v, cLSOP());
        }
        public void SetFa127_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa127(), "FA127", option);
        }
        public void SetFa127_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa127(), "FA127", option);
        }
        public void SetFa127_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa127_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa127(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa127(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa127(), "FA127");
        }
        protected abstract ConditionValue getCValueFa127();

        public void SetFa128_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa128_Equal(fRES(v));
        }
        protected void DoSetFa128_Equal(String v) { regFa128(CK_EQ, v); }
        public void SetFa128_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa128_NotEqual(fRES(v));
        }
        protected void DoSetFa128_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_NES, v);
        }
        public void SetFa128_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_GT, fRES(v));
        }
        public void SetFa128_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_LT, fRES(v));
        }
        public void SetFa128_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_GE, fRES(v));
        }
        public void SetFa128_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_LE, fRES(v));
        }
        public void SetFa128_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa128(), "FA128");
        }
        public void SetFa128_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa128(), "FA128");
        }
        public void SetFa128_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa128_LikeSearch(v, cLSOP());
        }
        public void SetFa128_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa128(), "FA128", option);
        }
        public void SetFa128_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa128(), "FA128", option);
        }
        public void SetFa128_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa128_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa128(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa128(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa128(), "FA128");
        }
        protected abstract ConditionValue getCValueFa128();

        public void SetFa129_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa129_Equal(fRES(v));
        }
        protected void DoSetFa129_Equal(String v) { regFa129(CK_EQ, v); }
        public void SetFa129_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa129_NotEqual(fRES(v));
        }
        protected void DoSetFa129_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_NES, v);
        }
        public void SetFa129_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_GT, fRES(v));
        }
        public void SetFa129_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_LT, fRES(v));
        }
        public void SetFa129_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_GE, fRES(v));
        }
        public void SetFa129_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_LE, fRES(v));
        }
        public void SetFa129_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa129(), "FA129");
        }
        public void SetFa129_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa129(), "FA129");
        }
        public void SetFa129_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa129_LikeSearch(v, cLSOP());
        }
        public void SetFa129_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa129(), "FA129", option);
        }
        public void SetFa129_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa129(), "FA129", option);
        }
        public void SetFa129_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa129_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa129(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa129(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa129(), "FA129");
        }
        protected abstract ConditionValue getCValueFa129();

        public void SetFa130_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa130_Equal(fRES(v));
        }
        protected void DoSetFa130_Equal(String v) { regFa130(CK_EQ, v); }
        public void SetFa130_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa130_NotEqual(fRES(v));
        }
        protected void DoSetFa130_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_NES, v);
        }
        public void SetFa130_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_GT, fRES(v));
        }
        public void SetFa130_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_LT, fRES(v));
        }
        public void SetFa130_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_GE, fRES(v));
        }
        public void SetFa130_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_LE, fRES(v));
        }
        public void SetFa130_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa130(), "FA130");
        }
        public void SetFa130_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa130(), "FA130");
        }
        public void SetFa130_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa130_LikeSearch(v, cLSOP());
        }
        public void SetFa130_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa130(), "FA130", option);
        }
        public void SetFa130_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa130(), "FA130", option);
        }
        public void SetFa130_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa130_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa130(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa130(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa130(), "FA130");
        }
        protected abstract ConditionValue getCValueFa130();

        public void SetFa131_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa131_Equal(fRES(v));
        }
        protected void DoSetFa131_Equal(String v) { regFa131(CK_EQ, v); }
        public void SetFa131_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa131_NotEqual(fRES(v));
        }
        protected void DoSetFa131_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_NES, v);
        }
        public void SetFa131_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_GT, fRES(v));
        }
        public void SetFa131_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_LT, fRES(v));
        }
        public void SetFa131_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_GE, fRES(v));
        }
        public void SetFa131_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_LE, fRES(v));
        }
        public void SetFa131_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa131(), "FA131");
        }
        public void SetFa131_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa131(), "FA131");
        }
        public void SetFa131_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa131_LikeSearch(v, cLSOP());
        }
        public void SetFa131_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa131(), "FA131", option);
        }
        public void SetFa131_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa131(), "FA131", option);
        }
        public void SetFa131_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa131_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa131(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa131(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa131(), "FA131");
        }
        protected abstract ConditionValue getCValueFa131();

        public void SetFa132_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa132_Equal(fRES(v));
        }
        protected void DoSetFa132_Equal(String v) { regFa132(CK_EQ, v); }
        public void SetFa132_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa132_NotEqual(fRES(v));
        }
        protected void DoSetFa132_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_NES, v);
        }
        public void SetFa132_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_GT, fRES(v));
        }
        public void SetFa132_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_LT, fRES(v));
        }
        public void SetFa132_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_GE, fRES(v));
        }
        public void SetFa132_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_LE, fRES(v));
        }
        public void SetFa132_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa132(), "FA132");
        }
        public void SetFa132_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa132(), "FA132");
        }
        public void SetFa132_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa132_LikeSearch(v, cLSOP());
        }
        public void SetFa132_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa132(), "FA132", option);
        }
        public void SetFa132_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa132(), "FA132", option);
        }
        public void SetFa132_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa132_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa132(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa132(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa132(), "FA132");
        }
        protected abstract ConditionValue getCValueFa132();

        public void SetFa133_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa133_Equal(fRES(v));
        }
        protected void DoSetFa133_Equal(String v) { regFa133(CK_EQ, v); }
        public void SetFa133_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa133_NotEqual(fRES(v));
        }
        protected void DoSetFa133_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_NES, v);
        }
        public void SetFa133_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_GT, fRES(v));
        }
        public void SetFa133_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_LT, fRES(v));
        }
        public void SetFa133_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_GE, fRES(v));
        }
        public void SetFa133_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_LE, fRES(v));
        }
        public void SetFa133_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa133(), "FA133");
        }
        public void SetFa133_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa133(), "FA133");
        }
        public void SetFa133_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa133_LikeSearch(v, cLSOP());
        }
        public void SetFa133_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa133(), "FA133", option);
        }
        public void SetFa133_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa133(), "FA133", option);
        }
        public void SetFa133_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa133_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa133(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa133(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa133(), "FA133");
        }
        protected abstract ConditionValue getCValueFa133();

        public void SetFa134_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa134_Equal(fRES(v));
        }
        protected void DoSetFa134_Equal(String v) { regFa134(CK_EQ, v); }
        public void SetFa134_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa134_NotEqual(fRES(v));
        }
        protected void DoSetFa134_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_NES, v);
        }
        public void SetFa134_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_GT, fRES(v));
        }
        public void SetFa134_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_LT, fRES(v));
        }
        public void SetFa134_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_GE, fRES(v));
        }
        public void SetFa134_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_LE, fRES(v));
        }
        public void SetFa134_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa134(), "FA134");
        }
        public void SetFa134_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa134(), "FA134");
        }
        public void SetFa134_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa134_LikeSearch(v, cLSOP());
        }
        public void SetFa134_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa134(), "FA134", option);
        }
        public void SetFa134_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa134(), "FA134", option);
        }
        public void SetFa134_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa134_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa134(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa134(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa134(), "FA134");
        }
        protected abstract ConditionValue getCValueFa134();

        public void SetFa135_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa135_Equal(fRES(v));
        }
        protected void DoSetFa135_Equal(String v) { regFa135(CK_EQ, v); }
        public void SetFa135_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa135_NotEqual(fRES(v));
        }
        protected void DoSetFa135_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_NES, v);
        }
        public void SetFa135_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_GT, fRES(v));
        }
        public void SetFa135_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_LT, fRES(v));
        }
        public void SetFa135_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_GE, fRES(v));
        }
        public void SetFa135_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_LE, fRES(v));
        }
        public void SetFa135_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa135(), "FA135");
        }
        public void SetFa135_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa135(), "FA135");
        }
        public void SetFa135_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa135_LikeSearch(v, cLSOP());
        }
        public void SetFa135_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa135(), "FA135", option);
        }
        public void SetFa135_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa135(), "FA135", option);
        }
        public void SetFa135_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa135_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa135(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa135(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa135(), "FA135");
        }
        protected abstract ConditionValue getCValueFa135();

        public void SetFa136_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa136_Equal(fRES(v));
        }
        protected void DoSetFa136_Equal(String v) { regFa136(CK_EQ, v); }
        public void SetFa136_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa136_NotEqual(fRES(v));
        }
        protected void DoSetFa136_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_NES, v);
        }
        public void SetFa136_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_GT, fRES(v));
        }
        public void SetFa136_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_LT, fRES(v));
        }
        public void SetFa136_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_GE, fRES(v));
        }
        public void SetFa136_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_LE, fRES(v));
        }
        public void SetFa136_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa136(), "FA136");
        }
        public void SetFa136_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa136(), "FA136");
        }
        public void SetFa136_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa136_LikeSearch(v, cLSOP());
        }
        public void SetFa136_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa136(), "FA136", option);
        }
        public void SetFa136_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa136(), "FA136", option);
        }
        public void SetFa136_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa136_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa136(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa136(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa136(), "FA136");
        }
        protected abstract ConditionValue getCValueFa136();

        public void SetFa137_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa137_Equal(fRES(v));
        }
        protected void DoSetFa137_Equal(String v) { regFa137(CK_EQ, v); }
        public void SetFa137_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa137_NotEqual(fRES(v));
        }
        protected void DoSetFa137_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_NES, v);
        }
        public void SetFa137_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_GT, fRES(v));
        }
        public void SetFa137_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_LT, fRES(v));
        }
        public void SetFa137_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_GE, fRES(v));
        }
        public void SetFa137_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_LE, fRES(v));
        }
        public void SetFa137_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa137(), "FA137");
        }
        public void SetFa137_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa137(), "FA137");
        }
        public void SetFa137_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa137_LikeSearch(v, cLSOP());
        }
        public void SetFa137_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa137(), "FA137", option);
        }
        public void SetFa137_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa137(), "FA137", option);
        }
        public void SetFa137_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa137_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa137(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa137(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa137(), "FA137");
        }
        protected abstract ConditionValue getCValueFa137();

        public void SetFa138_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa138_Equal(fRES(v));
        }
        protected void DoSetFa138_Equal(String v) { regFa138(CK_EQ, v); }
        public void SetFa138_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa138_NotEqual(fRES(v));
        }
        protected void DoSetFa138_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_NES, v);
        }
        public void SetFa138_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_GT, fRES(v));
        }
        public void SetFa138_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_LT, fRES(v));
        }
        public void SetFa138_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_GE, fRES(v));
        }
        public void SetFa138_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_LE, fRES(v));
        }
        public void SetFa138_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa138(), "FA138");
        }
        public void SetFa138_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa138(), "FA138");
        }
        public void SetFa138_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa138_LikeSearch(v, cLSOP());
        }
        public void SetFa138_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa138(), "FA138", option);
        }
        public void SetFa138_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa138(), "FA138", option);
        }
        public void SetFa138_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa138_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa138(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa138(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa138(), "FA138");
        }
        protected abstract ConditionValue getCValueFa138();

        public void SetFa139_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa139_Equal(fRES(v));
        }
        protected void DoSetFa139_Equal(String v) { regFa139(CK_EQ, v); }
        public void SetFa139_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa139_NotEqual(fRES(v));
        }
        protected void DoSetFa139_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_NES, v);
        }
        public void SetFa139_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_GT, fRES(v));
        }
        public void SetFa139_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_LT, fRES(v));
        }
        public void SetFa139_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_GE, fRES(v));
        }
        public void SetFa139_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_LE, fRES(v));
        }
        public void SetFa139_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa139(), "FA139");
        }
        public void SetFa139_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa139(), "FA139");
        }
        public void SetFa139_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa139_LikeSearch(v, cLSOP());
        }
        public void SetFa139_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa139(), "FA139", option);
        }
        public void SetFa139_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa139(), "FA139", option);
        }
        public void SetFa139_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa139_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa139(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa139(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa139(), "FA139");
        }
        protected abstract ConditionValue getCValueFa139();

        public void SetFa140_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa140_Equal(fRES(v));
        }
        protected void DoSetFa140_Equal(String v) { regFa140(CK_EQ, v); }
        public void SetFa140_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa140_NotEqual(fRES(v));
        }
        protected void DoSetFa140_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_NES, v);
        }
        public void SetFa140_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_GT, fRES(v));
        }
        public void SetFa140_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_LT, fRES(v));
        }
        public void SetFa140_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_GE, fRES(v));
        }
        public void SetFa140_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_LE, fRES(v));
        }
        public void SetFa140_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa140(), "FA140");
        }
        public void SetFa140_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa140(), "FA140");
        }
        public void SetFa140_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa140_LikeSearch(v, cLSOP());
        }
        public void SetFa140_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa140(), "FA140", option);
        }
        public void SetFa140_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa140(), "FA140", option);
        }
        public void SetFa140_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa140_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa140(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa140(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa140(), "FA140");
        }
        protected abstract ConditionValue getCValueFa140();

        public void SetFa141_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa141_Equal(fRES(v));
        }
        protected void DoSetFa141_Equal(String v) { regFa141(CK_EQ, v); }
        public void SetFa141_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa141_NotEqual(fRES(v));
        }
        protected void DoSetFa141_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_NES, v);
        }
        public void SetFa141_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_GT, fRES(v));
        }
        public void SetFa141_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_LT, fRES(v));
        }
        public void SetFa141_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_GE, fRES(v));
        }
        public void SetFa141_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_LE, fRES(v));
        }
        public void SetFa141_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa141(), "FA141");
        }
        public void SetFa141_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa141(), "FA141");
        }
        public void SetFa141_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa141_LikeSearch(v, cLSOP());
        }
        public void SetFa141_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa141(), "FA141", option);
        }
        public void SetFa141_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa141(), "FA141", option);
        }
        public void SetFa141_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa141_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa141(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa141(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa141(), "FA141");
        }
        protected abstract ConditionValue getCValueFa141();

        public void SetFa142_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa142_Equal(fRES(v));
        }
        protected void DoSetFa142_Equal(String v) { regFa142(CK_EQ, v); }
        public void SetFa142_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa142_NotEqual(fRES(v));
        }
        protected void DoSetFa142_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_NES, v);
        }
        public void SetFa142_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_GT, fRES(v));
        }
        public void SetFa142_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_LT, fRES(v));
        }
        public void SetFa142_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_GE, fRES(v));
        }
        public void SetFa142_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_LE, fRES(v));
        }
        public void SetFa142_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa142(), "FA142");
        }
        public void SetFa142_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa142(), "FA142");
        }
        public void SetFa142_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa142_LikeSearch(v, cLSOP());
        }
        public void SetFa142_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa142(), "FA142", option);
        }
        public void SetFa142_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa142(), "FA142", option);
        }
        public void SetFa142_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa142_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa142(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa142(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa142(), "FA142");
        }
        protected abstract ConditionValue getCValueFa142();

        public void SetFa143_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa143_Equal(fRES(v));
        }
        protected void DoSetFa143_Equal(String v) { regFa143(CK_EQ, v); }
        public void SetFa143_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa143_NotEqual(fRES(v));
        }
        protected void DoSetFa143_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_NES, v);
        }
        public void SetFa143_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_GT, fRES(v));
        }
        public void SetFa143_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_LT, fRES(v));
        }
        public void SetFa143_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_GE, fRES(v));
        }
        public void SetFa143_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_LE, fRES(v));
        }
        public void SetFa143_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa143(), "FA143");
        }
        public void SetFa143_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa143(), "FA143");
        }
        public void SetFa143_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa143_LikeSearch(v, cLSOP());
        }
        public void SetFa143_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa143(), "FA143", option);
        }
        public void SetFa143_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa143(), "FA143", option);
        }
        public void SetFa143_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa143_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa143(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa143(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa143(), "FA143");
        }
        protected abstract ConditionValue getCValueFa143();

        public void SetFa144_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa144_Equal(fRES(v));
        }
        protected void DoSetFa144_Equal(String v) { regFa144(CK_EQ, v); }
        public void SetFa144_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa144_NotEqual(fRES(v));
        }
        protected void DoSetFa144_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_NES, v);
        }
        public void SetFa144_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_GT, fRES(v));
        }
        public void SetFa144_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_LT, fRES(v));
        }
        public void SetFa144_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_GE, fRES(v));
        }
        public void SetFa144_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_LE, fRES(v));
        }
        public void SetFa144_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa144(), "FA144");
        }
        public void SetFa144_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa144(), "FA144");
        }
        public void SetFa144_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa144_LikeSearch(v, cLSOP());
        }
        public void SetFa144_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa144(), "FA144", option);
        }
        public void SetFa144_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa144(), "FA144", option);
        }
        public void SetFa144_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa144_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa144(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa144(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa144(), "FA144");
        }
        protected abstract ConditionValue getCValueFa144();

        public void SetFa145_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa145_Equal(fRES(v));
        }
        protected void DoSetFa145_Equal(String v) { regFa145(CK_EQ, v); }
        public void SetFa145_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa145_NotEqual(fRES(v));
        }
        protected void DoSetFa145_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_NES, v);
        }
        public void SetFa145_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_GT, fRES(v));
        }
        public void SetFa145_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_LT, fRES(v));
        }
        public void SetFa145_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_GE, fRES(v));
        }
        public void SetFa145_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_LE, fRES(v));
        }
        public void SetFa145_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa145(), "FA145");
        }
        public void SetFa145_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa145(), "FA145");
        }
        public void SetFa145_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa145_LikeSearch(v, cLSOP());
        }
        public void SetFa145_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa145(), "FA145", option);
        }
        public void SetFa145_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa145(), "FA145", option);
        }
        public void SetFa145_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa145_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa145(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa145(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa145(), "FA145");
        }
        protected abstract ConditionValue getCValueFa145();

        public void SetFa146_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa146_Equal(fRES(v));
        }
        protected void DoSetFa146_Equal(String v) { regFa146(CK_EQ, v); }
        public void SetFa146_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa146_NotEqual(fRES(v));
        }
        protected void DoSetFa146_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_NES, v);
        }
        public void SetFa146_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_GT, fRES(v));
        }
        public void SetFa146_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_LT, fRES(v));
        }
        public void SetFa146_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_GE, fRES(v));
        }
        public void SetFa146_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_LE, fRES(v));
        }
        public void SetFa146_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa146(), "FA146");
        }
        public void SetFa146_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa146(), "FA146");
        }
        public void SetFa146_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa146_LikeSearch(v, cLSOP());
        }
        public void SetFa146_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa146(), "FA146", option);
        }
        public void SetFa146_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa146(), "FA146", option);
        }
        public void SetFa146_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa146_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa146(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa146(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa146(), "FA146");
        }
        protected abstract ConditionValue getCValueFa146();

        public void SetFa147_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa147_Equal(fRES(v));
        }
        protected void DoSetFa147_Equal(String v) { regFa147(CK_EQ, v); }
        public void SetFa147_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa147_NotEqual(fRES(v));
        }
        protected void DoSetFa147_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_NES, v);
        }
        public void SetFa147_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_GT, fRES(v));
        }
        public void SetFa147_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_LT, fRES(v));
        }
        public void SetFa147_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_GE, fRES(v));
        }
        public void SetFa147_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_LE, fRES(v));
        }
        public void SetFa147_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa147(), "FA147");
        }
        public void SetFa147_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa147(), "FA147");
        }
        public void SetFa147_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa147_LikeSearch(v, cLSOP());
        }
        public void SetFa147_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa147(), "FA147", option);
        }
        public void SetFa147_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa147(), "FA147", option);
        }
        public void SetFa147_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa147_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa147(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa147(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa147(), "FA147");
        }
        protected abstract ConditionValue getCValueFa147();

        public void SetFa148_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa148_Equal(fRES(v));
        }
        protected void DoSetFa148_Equal(String v) { regFa148(CK_EQ, v); }
        public void SetFa148_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa148_NotEqual(fRES(v));
        }
        protected void DoSetFa148_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_NES, v);
        }
        public void SetFa148_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_GT, fRES(v));
        }
        public void SetFa148_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_LT, fRES(v));
        }
        public void SetFa148_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_GE, fRES(v));
        }
        public void SetFa148_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_LE, fRES(v));
        }
        public void SetFa148_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa148(), "FA148");
        }
        public void SetFa148_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa148(), "FA148");
        }
        public void SetFa148_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa148_LikeSearch(v, cLSOP());
        }
        public void SetFa148_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa148(), "FA148", option);
        }
        public void SetFa148_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa148(), "FA148", option);
        }
        public void SetFa148_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa148_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa148(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa148(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa148(), "FA148");
        }
        protected abstract ConditionValue getCValueFa148();

        public void SetFa149_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa149_Equal(fRES(v));
        }
        protected void DoSetFa149_Equal(String v) { regFa149(CK_EQ, v); }
        public void SetFa149_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa149_NotEqual(fRES(v));
        }
        protected void DoSetFa149_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_NES, v);
        }
        public void SetFa149_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_GT, fRES(v));
        }
        public void SetFa149_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_LT, fRES(v));
        }
        public void SetFa149_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_GE, fRES(v));
        }
        public void SetFa149_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_LE, fRES(v));
        }
        public void SetFa149_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa149(), "FA149");
        }
        public void SetFa149_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa149(), "FA149");
        }
        public void SetFa149_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa149_LikeSearch(v, cLSOP());
        }
        public void SetFa149_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa149(), "FA149", option);
        }
        public void SetFa149_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa149(), "FA149", option);
        }
        public void SetFa149_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa149_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa149(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa149(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa149(), "FA149");
        }
        protected abstract ConditionValue getCValueFa149();

        public void SetFa150_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa150_Equal(fRES(v));
        }
        protected void DoSetFa150_Equal(String v) { regFa150(CK_EQ, v); }
        public void SetFa150_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa150_NotEqual(fRES(v));
        }
        protected void DoSetFa150_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_NES, v);
        }
        public void SetFa150_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_GT, fRES(v));
        }
        public void SetFa150_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_LT, fRES(v));
        }
        public void SetFa150_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_GE, fRES(v));
        }
        public void SetFa150_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_LE, fRES(v));
        }
        public void SetFa150_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa150(), "FA150");
        }
        public void SetFa150_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa150(), "FA150");
        }
        public void SetFa150_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa150_LikeSearch(v, cLSOP());
        }
        public void SetFa150_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa150(), "FA150", option);
        }
        public void SetFa150_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa150(), "FA150", option);
        }
        public void SetFa150_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa150_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa150(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa150(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa150(), "FA150");
        }
        protected abstract ConditionValue getCValueFa150();

        public void SetFa151_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa151_Equal(fRES(v));
        }
        protected void DoSetFa151_Equal(String v) { regFa151(CK_EQ, v); }
        public void SetFa151_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa151_NotEqual(fRES(v));
        }
        protected void DoSetFa151_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_NES, v);
        }
        public void SetFa151_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_GT, fRES(v));
        }
        public void SetFa151_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_LT, fRES(v));
        }
        public void SetFa151_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_GE, fRES(v));
        }
        public void SetFa151_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_LE, fRES(v));
        }
        public void SetFa151_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa151(), "FA151");
        }
        public void SetFa151_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa151(), "FA151");
        }
        public void SetFa151_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa151_LikeSearch(v, cLSOP());
        }
        public void SetFa151_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa151(), "FA151", option);
        }
        public void SetFa151_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa151(), "FA151", option);
        }
        public void SetFa151_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa151_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa151(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa151(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa151(), "FA151");
        }
        protected abstract ConditionValue getCValueFa151();

        public void SetFa152_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa152_Equal(fRES(v));
        }
        protected void DoSetFa152_Equal(String v) { regFa152(CK_EQ, v); }
        public void SetFa152_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa152_NotEqual(fRES(v));
        }
        protected void DoSetFa152_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_NES, v);
        }
        public void SetFa152_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_GT, fRES(v));
        }
        public void SetFa152_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_LT, fRES(v));
        }
        public void SetFa152_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_GE, fRES(v));
        }
        public void SetFa152_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_LE, fRES(v));
        }
        public void SetFa152_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa152(), "FA152");
        }
        public void SetFa152_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa152(), "FA152");
        }
        public void SetFa152_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa152_LikeSearch(v, cLSOP());
        }
        public void SetFa152_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa152(), "FA152", option);
        }
        public void SetFa152_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa152(), "FA152", option);
        }
        public void SetFa152_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa152_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa152(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa152(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa152(), "FA152");
        }
        protected abstract ConditionValue getCValueFa152();

        public void SetFa153_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa153_Equal(fRES(v));
        }
        protected void DoSetFa153_Equal(String v) { regFa153(CK_EQ, v); }
        public void SetFa153_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa153_NotEqual(fRES(v));
        }
        protected void DoSetFa153_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_NES, v);
        }
        public void SetFa153_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_GT, fRES(v));
        }
        public void SetFa153_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_LT, fRES(v));
        }
        public void SetFa153_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_GE, fRES(v));
        }
        public void SetFa153_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_LE, fRES(v));
        }
        public void SetFa153_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa153(), "FA153");
        }
        public void SetFa153_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa153(), "FA153");
        }
        public void SetFa153_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa153_LikeSearch(v, cLSOP());
        }
        public void SetFa153_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa153(), "FA153", option);
        }
        public void SetFa153_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa153(), "FA153", option);
        }
        public void SetFa153_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa153_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa153(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa153(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa153(), "FA153");
        }
        protected abstract ConditionValue getCValueFa153();

        public void SetFa154_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa154_Equal(fRES(v));
        }
        protected void DoSetFa154_Equal(String v) { regFa154(CK_EQ, v); }
        public void SetFa154_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa154_NotEqual(fRES(v));
        }
        protected void DoSetFa154_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_NES, v);
        }
        public void SetFa154_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_GT, fRES(v));
        }
        public void SetFa154_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_LT, fRES(v));
        }
        public void SetFa154_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_GE, fRES(v));
        }
        public void SetFa154_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_LE, fRES(v));
        }
        public void SetFa154_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa154(), "FA154");
        }
        public void SetFa154_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa154(), "FA154");
        }
        public void SetFa154_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa154_LikeSearch(v, cLSOP());
        }
        public void SetFa154_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa154(), "FA154", option);
        }
        public void SetFa154_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa154(), "FA154", option);
        }
        public void SetFa154_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa154_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa154(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa154(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa154(), "FA154");
        }
        protected abstract ConditionValue getCValueFa154();

        public void SetFa155_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa155_Equal(fRES(v));
        }
        protected void DoSetFa155_Equal(String v) { regFa155(CK_EQ, v); }
        public void SetFa155_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa155_NotEqual(fRES(v));
        }
        protected void DoSetFa155_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_NES, v);
        }
        public void SetFa155_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_GT, fRES(v));
        }
        public void SetFa155_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_LT, fRES(v));
        }
        public void SetFa155_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_GE, fRES(v));
        }
        public void SetFa155_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_LE, fRES(v));
        }
        public void SetFa155_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa155(), "FA155");
        }
        public void SetFa155_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa155(), "FA155");
        }
        public void SetFa155_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa155_LikeSearch(v, cLSOP());
        }
        public void SetFa155_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa155(), "FA155", option);
        }
        public void SetFa155_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa155(), "FA155", option);
        }
        public void SetFa155_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa155_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa155(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa155(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa155(), "FA155");
        }
        protected abstract ConditionValue getCValueFa155();

        public void SetFa156_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa156_Equal(fRES(v));
        }
        protected void DoSetFa156_Equal(String v) { regFa156(CK_EQ, v); }
        public void SetFa156_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa156_NotEqual(fRES(v));
        }
        protected void DoSetFa156_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_NES, v);
        }
        public void SetFa156_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_GT, fRES(v));
        }
        public void SetFa156_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_LT, fRES(v));
        }
        public void SetFa156_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_GE, fRES(v));
        }
        public void SetFa156_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_LE, fRES(v));
        }
        public void SetFa156_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa156(), "FA156");
        }
        public void SetFa156_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa156(), "FA156");
        }
        public void SetFa156_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa156_LikeSearch(v, cLSOP());
        }
        public void SetFa156_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa156(), "FA156", option);
        }
        public void SetFa156_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa156(), "FA156", option);
        }
        public void SetFa156_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa156_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa156(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa156(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa156(), "FA156");
        }
        protected abstract ConditionValue getCValueFa156();

        public void SetFa157_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa157_Equal(fRES(v));
        }
        protected void DoSetFa157_Equal(String v) { regFa157(CK_EQ, v); }
        public void SetFa157_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa157_NotEqual(fRES(v));
        }
        protected void DoSetFa157_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_NES, v);
        }
        public void SetFa157_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_GT, fRES(v));
        }
        public void SetFa157_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_LT, fRES(v));
        }
        public void SetFa157_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_GE, fRES(v));
        }
        public void SetFa157_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_LE, fRES(v));
        }
        public void SetFa157_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa157(), "FA157");
        }
        public void SetFa157_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa157(), "FA157");
        }
        public void SetFa157_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa157_LikeSearch(v, cLSOP());
        }
        public void SetFa157_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa157(), "FA157", option);
        }
        public void SetFa157_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa157(), "FA157", option);
        }
        public void SetFa157_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa157_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa157(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa157(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa157(), "FA157");
        }
        protected abstract ConditionValue getCValueFa157();

        public void SetFa158_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa158_Equal(fRES(v));
        }
        protected void DoSetFa158_Equal(String v) { regFa158(CK_EQ, v); }
        public void SetFa158_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa158_NotEqual(fRES(v));
        }
        protected void DoSetFa158_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_NES, v);
        }
        public void SetFa158_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_GT, fRES(v));
        }
        public void SetFa158_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_LT, fRES(v));
        }
        public void SetFa158_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_GE, fRES(v));
        }
        public void SetFa158_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_LE, fRES(v));
        }
        public void SetFa158_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa158(), "FA158");
        }
        public void SetFa158_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa158(), "FA158");
        }
        public void SetFa158_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa158_LikeSearch(v, cLSOP());
        }
        public void SetFa158_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa158(), "FA158", option);
        }
        public void SetFa158_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa158(), "FA158", option);
        }
        public void SetFa158_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa158_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa158(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa158(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa158(), "FA158");
        }
        protected abstract ConditionValue getCValueFa158();

        public void SetFa159_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa159_Equal(fRES(v));
        }
        protected void DoSetFa159_Equal(String v) { regFa159(CK_EQ, v); }
        public void SetFa159_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa159_NotEqual(fRES(v));
        }
        protected void DoSetFa159_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_NES, v);
        }
        public void SetFa159_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_GT, fRES(v));
        }
        public void SetFa159_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_LT, fRES(v));
        }
        public void SetFa159_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_GE, fRES(v));
        }
        public void SetFa159_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_LE, fRES(v));
        }
        public void SetFa159_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa159(), "FA159");
        }
        public void SetFa159_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa159(), "FA159");
        }
        public void SetFa159_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa159_LikeSearch(v, cLSOP());
        }
        public void SetFa159_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa159(), "FA159", option);
        }
        public void SetFa159_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa159(), "FA159", option);
        }
        public void SetFa159_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa159_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa159(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa159(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa159(), "FA159");
        }
        protected abstract ConditionValue getCValueFa159();

        public void SetFa160_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa160_Equal(fRES(v));
        }
        protected void DoSetFa160_Equal(String v) { regFa160(CK_EQ, v); }
        public void SetFa160_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa160_NotEqual(fRES(v));
        }
        protected void DoSetFa160_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_NES, v);
        }
        public void SetFa160_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_GT, fRES(v));
        }
        public void SetFa160_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_LT, fRES(v));
        }
        public void SetFa160_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_GE, fRES(v));
        }
        public void SetFa160_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_LE, fRES(v));
        }
        public void SetFa160_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa160(), "FA160");
        }
        public void SetFa160_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa160(), "FA160");
        }
        public void SetFa160_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa160_LikeSearch(v, cLSOP());
        }
        public void SetFa160_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa160(), "FA160", option);
        }
        public void SetFa160_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa160(), "FA160", option);
        }
        public void SetFa160_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa160_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa160(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa160(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa160(), "FA160");
        }
        protected abstract ConditionValue getCValueFa160();

        public void SetFa161_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa161_Equal(fRES(v));
        }
        protected void DoSetFa161_Equal(String v) { regFa161(CK_EQ, v); }
        public void SetFa161_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa161_NotEqual(fRES(v));
        }
        protected void DoSetFa161_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_NES, v);
        }
        public void SetFa161_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_GT, fRES(v));
        }
        public void SetFa161_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_LT, fRES(v));
        }
        public void SetFa161_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_GE, fRES(v));
        }
        public void SetFa161_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_LE, fRES(v));
        }
        public void SetFa161_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa161(), "FA161");
        }
        public void SetFa161_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa161(), "FA161");
        }
        public void SetFa161_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa161_LikeSearch(v, cLSOP());
        }
        public void SetFa161_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa161(), "FA161", option);
        }
        public void SetFa161_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa161(), "FA161", option);
        }
        public void SetFa161_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa161_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa161(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa161(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa161(), "FA161");
        }
        protected abstract ConditionValue getCValueFa161();

        public void SetFa162_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa162_Equal(fRES(v));
        }
        protected void DoSetFa162_Equal(String v) { regFa162(CK_EQ, v); }
        public void SetFa162_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa162_NotEqual(fRES(v));
        }
        protected void DoSetFa162_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_NES, v);
        }
        public void SetFa162_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_GT, fRES(v));
        }
        public void SetFa162_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_LT, fRES(v));
        }
        public void SetFa162_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_GE, fRES(v));
        }
        public void SetFa162_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_LE, fRES(v));
        }
        public void SetFa162_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa162(), "FA162");
        }
        public void SetFa162_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa162(), "FA162");
        }
        public void SetFa162_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa162_LikeSearch(v, cLSOP());
        }
        public void SetFa162_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa162(), "FA162", option);
        }
        public void SetFa162_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa162(), "FA162", option);
        }
        public void SetFa162_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa162_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa162(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa162(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa162(), "FA162");
        }
        protected abstract ConditionValue getCValueFa162();

        public void SetFa163_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa163_Equal(fRES(v));
        }
        protected void DoSetFa163_Equal(String v) { regFa163(CK_EQ, v); }
        public void SetFa163_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa163_NotEqual(fRES(v));
        }
        protected void DoSetFa163_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_NES, v);
        }
        public void SetFa163_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_GT, fRES(v));
        }
        public void SetFa163_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_LT, fRES(v));
        }
        public void SetFa163_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_GE, fRES(v));
        }
        public void SetFa163_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_LE, fRES(v));
        }
        public void SetFa163_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa163(), "FA163");
        }
        public void SetFa163_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa163(), "FA163");
        }
        public void SetFa163_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa163_LikeSearch(v, cLSOP());
        }
        public void SetFa163_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa163(), "FA163", option);
        }
        public void SetFa163_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa163(), "FA163", option);
        }
        public void SetFa163_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa163_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa163(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa163(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa163(), "FA163");
        }
        protected abstract ConditionValue getCValueFa163();

        public void SetFa164_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa164_Equal(fRES(v));
        }
        protected void DoSetFa164_Equal(String v) { regFa164(CK_EQ, v); }
        public void SetFa164_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa164_NotEqual(fRES(v));
        }
        protected void DoSetFa164_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_NES, v);
        }
        public void SetFa164_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_GT, fRES(v));
        }
        public void SetFa164_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_LT, fRES(v));
        }
        public void SetFa164_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_GE, fRES(v));
        }
        public void SetFa164_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_LE, fRES(v));
        }
        public void SetFa164_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa164(), "FA164");
        }
        public void SetFa164_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa164(), "FA164");
        }
        public void SetFa164_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa164_LikeSearch(v, cLSOP());
        }
        public void SetFa164_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa164(), "FA164", option);
        }
        public void SetFa164_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa164(), "FA164", option);
        }
        public void SetFa164_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa164_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa164(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa164(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa164(), "FA164");
        }
        protected abstract ConditionValue getCValueFa164();

        public void SetFa165_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa165_Equal(fRES(v));
        }
        protected void DoSetFa165_Equal(String v) { regFa165(CK_EQ, v); }
        public void SetFa165_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa165_NotEqual(fRES(v));
        }
        protected void DoSetFa165_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_NES, v);
        }
        public void SetFa165_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_GT, fRES(v));
        }
        public void SetFa165_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_LT, fRES(v));
        }
        public void SetFa165_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_GE, fRES(v));
        }
        public void SetFa165_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_LE, fRES(v));
        }
        public void SetFa165_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa165(), "FA165");
        }
        public void SetFa165_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa165(), "FA165");
        }
        public void SetFa165_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa165_LikeSearch(v, cLSOP());
        }
        public void SetFa165_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa165(), "FA165", option);
        }
        public void SetFa165_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa165(), "FA165", option);
        }
        public void SetFa165_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa165_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa165(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa165(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa165(), "FA165");
        }
        protected abstract ConditionValue getCValueFa165();

        public void SetFa166_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa166_Equal(fRES(v));
        }
        protected void DoSetFa166_Equal(String v) { regFa166(CK_EQ, v); }
        public void SetFa166_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa166_NotEqual(fRES(v));
        }
        protected void DoSetFa166_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_NES, v);
        }
        public void SetFa166_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_GT, fRES(v));
        }
        public void SetFa166_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_LT, fRES(v));
        }
        public void SetFa166_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_GE, fRES(v));
        }
        public void SetFa166_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_LE, fRES(v));
        }
        public void SetFa166_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa166(), "FA166");
        }
        public void SetFa166_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa166(), "FA166");
        }
        public void SetFa166_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa166_LikeSearch(v, cLSOP());
        }
        public void SetFa166_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa166(), "FA166", option);
        }
        public void SetFa166_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa166(), "FA166", option);
        }
        public void SetFa166_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa166_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa166(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa166(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa166(), "FA166");
        }
        protected abstract ConditionValue getCValueFa166();

        public void SetFa167_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa167_Equal(fRES(v));
        }
        protected void DoSetFa167_Equal(String v) { regFa167(CK_EQ, v); }
        public void SetFa167_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa167_NotEqual(fRES(v));
        }
        protected void DoSetFa167_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_NES, v);
        }
        public void SetFa167_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_GT, fRES(v));
        }
        public void SetFa167_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_LT, fRES(v));
        }
        public void SetFa167_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_GE, fRES(v));
        }
        public void SetFa167_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_LE, fRES(v));
        }
        public void SetFa167_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa167(), "FA167");
        }
        public void SetFa167_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa167(), "FA167");
        }
        public void SetFa167_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa167_LikeSearch(v, cLSOP());
        }
        public void SetFa167_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa167(), "FA167", option);
        }
        public void SetFa167_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa167(), "FA167", option);
        }
        public void SetFa167_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa167_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa167(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa167(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa167(), "FA167");
        }
        protected abstract ConditionValue getCValueFa167();

        public void SetFa168_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa168_Equal(fRES(v));
        }
        protected void DoSetFa168_Equal(String v) { regFa168(CK_EQ, v); }
        public void SetFa168_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa168_NotEqual(fRES(v));
        }
        protected void DoSetFa168_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_NES, v);
        }
        public void SetFa168_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_GT, fRES(v));
        }
        public void SetFa168_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_LT, fRES(v));
        }
        public void SetFa168_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_GE, fRES(v));
        }
        public void SetFa168_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_LE, fRES(v));
        }
        public void SetFa168_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa168(), "FA168");
        }
        public void SetFa168_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa168(), "FA168");
        }
        public void SetFa168_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa168_LikeSearch(v, cLSOP());
        }
        public void SetFa168_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa168(), "FA168", option);
        }
        public void SetFa168_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa168(), "FA168", option);
        }
        public void SetFa168_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa168_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa168(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa168(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa168(), "FA168");
        }
        protected abstract ConditionValue getCValueFa168();

        public void SetFa169_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa169_Equal(fRES(v));
        }
        protected void DoSetFa169_Equal(String v) { regFa169(CK_EQ, v); }
        public void SetFa169_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa169_NotEqual(fRES(v));
        }
        protected void DoSetFa169_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_NES, v);
        }
        public void SetFa169_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_GT, fRES(v));
        }
        public void SetFa169_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_LT, fRES(v));
        }
        public void SetFa169_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_GE, fRES(v));
        }
        public void SetFa169_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_LE, fRES(v));
        }
        public void SetFa169_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa169(), "FA169");
        }
        public void SetFa169_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa169(), "FA169");
        }
        public void SetFa169_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa169_LikeSearch(v, cLSOP());
        }
        public void SetFa169_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa169(), "FA169", option);
        }
        public void SetFa169_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa169(), "FA169", option);
        }
        public void SetFa169_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa169_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa169(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa169(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa169(), "FA169");
        }
        protected abstract ConditionValue getCValueFa169();

        public void SetFa170_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa170_Equal(fRES(v));
        }
        protected void DoSetFa170_Equal(String v) { regFa170(CK_EQ, v); }
        public void SetFa170_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa170_NotEqual(fRES(v));
        }
        protected void DoSetFa170_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_NES, v);
        }
        public void SetFa170_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_GT, fRES(v));
        }
        public void SetFa170_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_LT, fRES(v));
        }
        public void SetFa170_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_GE, fRES(v));
        }
        public void SetFa170_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_LE, fRES(v));
        }
        public void SetFa170_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa170(), "FA170");
        }
        public void SetFa170_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa170(), "FA170");
        }
        public void SetFa170_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa170_LikeSearch(v, cLSOP());
        }
        public void SetFa170_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa170(), "FA170", option);
        }
        public void SetFa170_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa170(), "FA170", option);
        }
        public void SetFa170_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa170_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa170(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa170(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa170(), "FA170");
        }
        protected abstract ConditionValue getCValueFa170();

        public void SetFa171_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa171_Equal(fRES(v));
        }
        protected void DoSetFa171_Equal(String v) { regFa171(CK_EQ, v); }
        public void SetFa171_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa171_NotEqual(fRES(v));
        }
        protected void DoSetFa171_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_NES, v);
        }
        public void SetFa171_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_GT, fRES(v));
        }
        public void SetFa171_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_LT, fRES(v));
        }
        public void SetFa171_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_GE, fRES(v));
        }
        public void SetFa171_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_LE, fRES(v));
        }
        public void SetFa171_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa171(), "FA171");
        }
        public void SetFa171_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa171(), "FA171");
        }
        public void SetFa171_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa171_LikeSearch(v, cLSOP());
        }
        public void SetFa171_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa171(), "FA171", option);
        }
        public void SetFa171_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa171(), "FA171", option);
        }
        public void SetFa171_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa171_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa171(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa171(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa171(), "FA171");
        }
        protected abstract ConditionValue getCValueFa171();

        public void SetFa172_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa172_Equal(fRES(v));
        }
        protected void DoSetFa172_Equal(String v) { regFa172(CK_EQ, v); }
        public void SetFa172_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa172_NotEqual(fRES(v));
        }
        protected void DoSetFa172_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_NES, v);
        }
        public void SetFa172_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_GT, fRES(v));
        }
        public void SetFa172_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_LT, fRES(v));
        }
        public void SetFa172_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_GE, fRES(v));
        }
        public void SetFa172_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_LE, fRES(v));
        }
        public void SetFa172_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa172(), "FA172");
        }
        public void SetFa172_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa172(), "FA172");
        }
        public void SetFa172_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa172_LikeSearch(v, cLSOP());
        }
        public void SetFa172_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa172(), "FA172", option);
        }
        public void SetFa172_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa172(), "FA172", option);
        }
        public void SetFa172_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa172_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa172(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa172(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa172(), "FA172");
        }
        protected abstract ConditionValue getCValueFa172();

        public void SetFa173_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa173_Equal(fRES(v));
        }
        protected void DoSetFa173_Equal(String v) { regFa173(CK_EQ, v); }
        public void SetFa173_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa173_NotEqual(fRES(v));
        }
        protected void DoSetFa173_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_NES, v);
        }
        public void SetFa173_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_GT, fRES(v));
        }
        public void SetFa173_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_LT, fRES(v));
        }
        public void SetFa173_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_GE, fRES(v));
        }
        public void SetFa173_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_LE, fRES(v));
        }
        public void SetFa173_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa173(), "FA173");
        }
        public void SetFa173_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa173(), "FA173");
        }
        public void SetFa173_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa173_LikeSearch(v, cLSOP());
        }
        public void SetFa173_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa173(), "FA173", option);
        }
        public void SetFa173_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa173(), "FA173", option);
        }
        public void SetFa173_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa173_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa173(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa173(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa173(), "FA173");
        }
        protected abstract ConditionValue getCValueFa173();

        public void SetFa174_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa174_Equal(fRES(v));
        }
        protected void DoSetFa174_Equal(String v) { regFa174(CK_EQ, v); }
        public void SetFa174_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa174_NotEqual(fRES(v));
        }
        protected void DoSetFa174_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_NES, v);
        }
        public void SetFa174_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_GT, fRES(v));
        }
        public void SetFa174_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_LT, fRES(v));
        }
        public void SetFa174_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_GE, fRES(v));
        }
        public void SetFa174_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_LE, fRES(v));
        }
        public void SetFa174_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa174(), "FA174");
        }
        public void SetFa174_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa174(), "FA174");
        }
        public void SetFa174_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa174_LikeSearch(v, cLSOP());
        }
        public void SetFa174_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa174(), "FA174", option);
        }
        public void SetFa174_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa174(), "FA174", option);
        }
        public void SetFa174_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa174_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa174(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa174(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa174(), "FA174");
        }
        protected abstract ConditionValue getCValueFa174();

        public void SetFa175_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa175_Equal(fRES(v));
        }
        protected void DoSetFa175_Equal(String v) { regFa175(CK_EQ, v); }
        public void SetFa175_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa175_NotEqual(fRES(v));
        }
        protected void DoSetFa175_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_NES, v);
        }
        public void SetFa175_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_GT, fRES(v));
        }
        public void SetFa175_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_LT, fRES(v));
        }
        public void SetFa175_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_GE, fRES(v));
        }
        public void SetFa175_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_LE, fRES(v));
        }
        public void SetFa175_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa175(), "FA175");
        }
        public void SetFa175_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa175(), "FA175");
        }
        public void SetFa175_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa175_LikeSearch(v, cLSOP());
        }
        public void SetFa175_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa175(), "FA175", option);
        }
        public void SetFa175_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa175(), "FA175", option);
        }
        public void SetFa175_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa175_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa175(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa175(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa175(), "FA175");
        }
        protected abstract ConditionValue getCValueFa175();

        public void SetFa176_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa176_Equal(fRES(v));
        }
        protected void DoSetFa176_Equal(String v) { regFa176(CK_EQ, v); }
        public void SetFa176_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa176_NotEqual(fRES(v));
        }
        protected void DoSetFa176_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_NES, v);
        }
        public void SetFa176_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_GT, fRES(v));
        }
        public void SetFa176_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_LT, fRES(v));
        }
        public void SetFa176_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_GE, fRES(v));
        }
        public void SetFa176_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_LE, fRES(v));
        }
        public void SetFa176_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa176(), "FA176");
        }
        public void SetFa176_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa176(), "FA176");
        }
        public void SetFa176_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa176_LikeSearch(v, cLSOP());
        }
        public void SetFa176_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa176(), "FA176", option);
        }
        public void SetFa176_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa176(), "FA176", option);
        }
        public void SetFa176_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa176_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa176(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa176(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa176(), "FA176");
        }
        protected abstract ConditionValue getCValueFa176();

        public void SetFa177_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa177_Equal(fRES(v));
        }
        protected void DoSetFa177_Equal(String v) { regFa177(CK_EQ, v); }
        public void SetFa177_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa177_NotEqual(fRES(v));
        }
        protected void DoSetFa177_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_NES, v);
        }
        public void SetFa177_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_GT, fRES(v));
        }
        public void SetFa177_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_LT, fRES(v));
        }
        public void SetFa177_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_GE, fRES(v));
        }
        public void SetFa177_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_LE, fRES(v));
        }
        public void SetFa177_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa177(), "FA177");
        }
        public void SetFa177_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa177(), "FA177");
        }
        public void SetFa177_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa177_LikeSearch(v, cLSOP());
        }
        public void SetFa177_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa177(), "FA177", option);
        }
        public void SetFa177_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa177(), "FA177", option);
        }
        public void SetFa177_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa177_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa177(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa177(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa177(), "FA177");
        }
        protected abstract ConditionValue getCValueFa177();

        public void SetFa178_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa178_Equal(fRES(v));
        }
        protected void DoSetFa178_Equal(String v) { regFa178(CK_EQ, v); }
        public void SetFa178_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa178_NotEqual(fRES(v));
        }
        protected void DoSetFa178_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_NES, v);
        }
        public void SetFa178_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_GT, fRES(v));
        }
        public void SetFa178_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_LT, fRES(v));
        }
        public void SetFa178_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_GE, fRES(v));
        }
        public void SetFa178_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_LE, fRES(v));
        }
        public void SetFa178_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa178(), "FA178");
        }
        public void SetFa178_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa178(), "FA178");
        }
        public void SetFa178_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa178_LikeSearch(v, cLSOP());
        }
        public void SetFa178_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa178(), "FA178", option);
        }
        public void SetFa178_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa178(), "FA178", option);
        }
        public void SetFa178_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa178_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa178(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa178(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa178(), "FA178");
        }
        protected abstract ConditionValue getCValueFa178();

        public void SetFa179_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa179_Equal(fRES(v));
        }
        protected void DoSetFa179_Equal(String v) { regFa179(CK_EQ, v); }
        public void SetFa179_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa179_NotEqual(fRES(v));
        }
        protected void DoSetFa179_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_NES, v);
        }
        public void SetFa179_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_GT, fRES(v));
        }
        public void SetFa179_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_LT, fRES(v));
        }
        public void SetFa179_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_GE, fRES(v));
        }
        public void SetFa179_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_LE, fRES(v));
        }
        public void SetFa179_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa179(), "FA179");
        }
        public void SetFa179_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa179(), "FA179");
        }
        public void SetFa179_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa179_LikeSearch(v, cLSOP());
        }
        public void SetFa179_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa179(), "FA179", option);
        }
        public void SetFa179_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa179(), "FA179", option);
        }
        public void SetFa179_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa179_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa179(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa179(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa179(), "FA179");
        }
        protected abstract ConditionValue getCValueFa179();

        public void SetFa180_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa180_Equal(fRES(v));
        }
        protected void DoSetFa180_Equal(String v) { regFa180(CK_EQ, v); }
        public void SetFa180_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa180_NotEqual(fRES(v));
        }
        protected void DoSetFa180_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_NES, v);
        }
        public void SetFa180_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_GT, fRES(v));
        }
        public void SetFa180_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_LT, fRES(v));
        }
        public void SetFa180_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_GE, fRES(v));
        }
        public void SetFa180_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_LE, fRES(v));
        }
        public void SetFa180_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa180(), "FA180");
        }
        public void SetFa180_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa180(), "FA180");
        }
        public void SetFa180_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa180_LikeSearch(v, cLSOP());
        }
        public void SetFa180_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa180(), "FA180", option);
        }
        public void SetFa180_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa180(), "FA180", option);
        }
        public void SetFa180_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa180_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa180(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa180(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa180(), "FA180");
        }
        protected abstract ConditionValue getCValueFa180();

        public void SetFa181_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa181_Equal(fRES(v));
        }
        protected void DoSetFa181_Equal(String v) { regFa181(CK_EQ, v); }
        public void SetFa181_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa181_NotEqual(fRES(v));
        }
        protected void DoSetFa181_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_NES, v);
        }
        public void SetFa181_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_GT, fRES(v));
        }
        public void SetFa181_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_LT, fRES(v));
        }
        public void SetFa181_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_GE, fRES(v));
        }
        public void SetFa181_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_LE, fRES(v));
        }
        public void SetFa181_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa181(), "FA181");
        }
        public void SetFa181_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa181(), "FA181");
        }
        public void SetFa181_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa181_LikeSearch(v, cLSOP());
        }
        public void SetFa181_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa181(), "FA181", option);
        }
        public void SetFa181_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa181(), "FA181", option);
        }
        public void SetFa181_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa181_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa181(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa181(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa181(), "FA181");
        }
        protected abstract ConditionValue getCValueFa181();

        public void SetFa182_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa182_Equal(fRES(v));
        }
        protected void DoSetFa182_Equal(String v) { regFa182(CK_EQ, v); }
        public void SetFa182_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa182_NotEqual(fRES(v));
        }
        protected void DoSetFa182_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_NES, v);
        }
        public void SetFa182_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_GT, fRES(v));
        }
        public void SetFa182_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_LT, fRES(v));
        }
        public void SetFa182_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_GE, fRES(v));
        }
        public void SetFa182_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_LE, fRES(v));
        }
        public void SetFa182_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa182(), "FA182");
        }
        public void SetFa182_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa182(), "FA182");
        }
        public void SetFa182_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa182_LikeSearch(v, cLSOP());
        }
        public void SetFa182_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa182(), "FA182", option);
        }
        public void SetFa182_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa182(), "FA182", option);
        }
        public void SetFa182_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa182_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa182(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa182(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa182(), "FA182");
        }
        protected abstract ConditionValue getCValueFa182();

        public void SetFa183_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa183_Equal(fRES(v));
        }
        protected void DoSetFa183_Equal(String v) { regFa183(CK_EQ, v); }
        public void SetFa183_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa183_NotEqual(fRES(v));
        }
        protected void DoSetFa183_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_NES, v);
        }
        public void SetFa183_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_GT, fRES(v));
        }
        public void SetFa183_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_LT, fRES(v));
        }
        public void SetFa183_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_GE, fRES(v));
        }
        public void SetFa183_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_LE, fRES(v));
        }
        public void SetFa183_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa183(), "FA183");
        }
        public void SetFa183_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa183(), "FA183");
        }
        public void SetFa183_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa183_LikeSearch(v, cLSOP());
        }
        public void SetFa183_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa183(), "FA183", option);
        }
        public void SetFa183_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa183(), "FA183", option);
        }
        public void SetFa183_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa183_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa183(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa183(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa183(), "FA183");
        }
        protected abstract ConditionValue getCValueFa183();

        public void SetFa184_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa184_Equal(fRES(v));
        }
        protected void DoSetFa184_Equal(String v) { regFa184(CK_EQ, v); }
        public void SetFa184_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa184_NotEqual(fRES(v));
        }
        protected void DoSetFa184_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_NES, v);
        }
        public void SetFa184_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_GT, fRES(v));
        }
        public void SetFa184_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_LT, fRES(v));
        }
        public void SetFa184_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_GE, fRES(v));
        }
        public void SetFa184_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_LE, fRES(v));
        }
        public void SetFa184_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa184(), "FA184");
        }
        public void SetFa184_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa184(), "FA184");
        }
        public void SetFa184_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa184_LikeSearch(v, cLSOP());
        }
        public void SetFa184_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa184(), "FA184", option);
        }
        public void SetFa184_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa184(), "FA184", option);
        }
        public void SetFa184_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa184_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa184(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa184(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa184(), "FA184");
        }
        protected abstract ConditionValue getCValueFa184();

        public void SetFa185_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa185_Equal(fRES(v));
        }
        protected void DoSetFa185_Equal(String v) { regFa185(CK_EQ, v); }
        public void SetFa185_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa185_NotEqual(fRES(v));
        }
        protected void DoSetFa185_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_NES, v);
        }
        public void SetFa185_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_GT, fRES(v));
        }
        public void SetFa185_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_LT, fRES(v));
        }
        public void SetFa185_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_GE, fRES(v));
        }
        public void SetFa185_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_LE, fRES(v));
        }
        public void SetFa185_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa185(), "FA185");
        }
        public void SetFa185_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa185(), "FA185");
        }
        public void SetFa185_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa185_LikeSearch(v, cLSOP());
        }
        public void SetFa185_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa185(), "FA185", option);
        }
        public void SetFa185_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa185(), "FA185", option);
        }
        public void SetFa185_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa185_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa185(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa185(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa185(), "FA185");
        }
        protected abstract ConditionValue getCValueFa185();

        public void SetFa186_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa186_Equal(fRES(v));
        }
        protected void DoSetFa186_Equal(String v) { regFa186(CK_EQ, v); }
        public void SetFa186_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa186_NotEqual(fRES(v));
        }
        protected void DoSetFa186_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_NES, v);
        }
        public void SetFa186_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_GT, fRES(v));
        }
        public void SetFa186_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_LT, fRES(v));
        }
        public void SetFa186_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_GE, fRES(v));
        }
        public void SetFa186_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_LE, fRES(v));
        }
        public void SetFa186_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa186(), "FA186");
        }
        public void SetFa186_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa186(), "FA186");
        }
        public void SetFa186_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa186_LikeSearch(v, cLSOP());
        }
        public void SetFa186_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa186(), "FA186", option);
        }
        public void SetFa186_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa186(), "FA186", option);
        }
        public void SetFa186_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa186_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa186(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa186(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa186(), "FA186");
        }
        protected abstract ConditionValue getCValueFa186();

        public void SetFa187_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa187_Equal(fRES(v));
        }
        protected void DoSetFa187_Equal(String v) { regFa187(CK_EQ, v); }
        public void SetFa187_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa187_NotEqual(fRES(v));
        }
        protected void DoSetFa187_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_NES, v);
        }
        public void SetFa187_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_GT, fRES(v));
        }
        public void SetFa187_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_LT, fRES(v));
        }
        public void SetFa187_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_GE, fRES(v));
        }
        public void SetFa187_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_LE, fRES(v));
        }
        public void SetFa187_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa187(), "FA187");
        }
        public void SetFa187_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa187(), "FA187");
        }
        public void SetFa187_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa187_LikeSearch(v, cLSOP());
        }
        public void SetFa187_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa187(), "FA187", option);
        }
        public void SetFa187_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa187(), "FA187", option);
        }
        public void SetFa187_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa187_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa187(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa187(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa187(), "FA187");
        }
        protected abstract ConditionValue getCValueFa187();

        public void SetFa188_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa188_Equal(fRES(v));
        }
        protected void DoSetFa188_Equal(String v) { regFa188(CK_EQ, v); }
        public void SetFa188_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa188_NotEqual(fRES(v));
        }
        protected void DoSetFa188_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_NES, v);
        }
        public void SetFa188_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_GT, fRES(v));
        }
        public void SetFa188_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_LT, fRES(v));
        }
        public void SetFa188_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_GE, fRES(v));
        }
        public void SetFa188_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_LE, fRES(v));
        }
        public void SetFa188_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa188(), "FA188");
        }
        public void SetFa188_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa188(), "FA188");
        }
        public void SetFa188_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa188_LikeSearch(v, cLSOP());
        }
        public void SetFa188_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa188(), "FA188", option);
        }
        public void SetFa188_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa188(), "FA188", option);
        }
        public void SetFa188_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa188_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa188(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa188(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa188(), "FA188");
        }
        protected abstract ConditionValue getCValueFa188();

        public void SetFa189_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa189_Equal(fRES(v));
        }
        protected void DoSetFa189_Equal(String v) { regFa189(CK_EQ, v); }
        public void SetFa189_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa189_NotEqual(fRES(v));
        }
        protected void DoSetFa189_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_NES, v);
        }
        public void SetFa189_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_GT, fRES(v));
        }
        public void SetFa189_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_LT, fRES(v));
        }
        public void SetFa189_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_GE, fRES(v));
        }
        public void SetFa189_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_LE, fRES(v));
        }
        public void SetFa189_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa189(), "FA189");
        }
        public void SetFa189_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa189(), "FA189");
        }
        public void SetFa189_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa189_LikeSearch(v, cLSOP());
        }
        public void SetFa189_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa189(), "FA189", option);
        }
        public void SetFa189_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa189(), "FA189", option);
        }
        public void SetFa189_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa189_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa189(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa189(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa189(), "FA189");
        }
        protected abstract ConditionValue getCValueFa189();

        public void SetFa190_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa190_Equal(fRES(v));
        }
        protected void DoSetFa190_Equal(String v) { regFa190(CK_EQ, v); }
        public void SetFa190_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa190_NotEqual(fRES(v));
        }
        protected void DoSetFa190_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_NES, v);
        }
        public void SetFa190_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_GT, fRES(v));
        }
        public void SetFa190_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_LT, fRES(v));
        }
        public void SetFa190_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_GE, fRES(v));
        }
        public void SetFa190_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_LE, fRES(v));
        }
        public void SetFa190_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa190(), "FA190");
        }
        public void SetFa190_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa190(), "FA190");
        }
        public void SetFa190_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa190_LikeSearch(v, cLSOP());
        }
        public void SetFa190_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa190(), "FA190", option);
        }
        public void SetFa190_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa190(), "FA190", option);
        }
        public void SetFa190_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa190_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa190(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa190(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa190(), "FA190");
        }
        protected abstract ConditionValue getCValueFa190();

        public void SetFa191_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa191_Equal(fRES(v));
        }
        protected void DoSetFa191_Equal(String v) { regFa191(CK_EQ, v); }
        public void SetFa191_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa191_NotEqual(fRES(v));
        }
        protected void DoSetFa191_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_NES, v);
        }
        public void SetFa191_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_GT, fRES(v));
        }
        public void SetFa191_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_LT, fRES(v));
        }
        public void SetFa191_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_GE, fRES(v));
        }
        public void SetFa191_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_LE, fRES(v));
        }
        public void SetFa191_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa191(), "FA191");
        }
        public void SetFa191_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa191(), "FA191");
        }
        public void SetFa191_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa191_LikeSearch(v, cLSOP());
        }
        public void SetFa191_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa191(), "FA191", option);
        }
        public void SetFa191_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa191(), "FA191", option);
        }
        public void SetFa191_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa191_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa191(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa191(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa191(), "FA191");
        }
        protected abstract ConditionValue getCValueFa191();

        public void SetFa192_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa192_Equal(fRES(v));
        }
        protected void DoSetFa192_Equal(String v) { regFa192(CK_EQ, v); }
        public void SetFa192_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa192_NotEqual(fRES(v));
        }
        protected void DoSetFa192_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_NES, v);
        }
        public void SetFa192_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_GT, fRES(v));
        }
        public void SetFa192_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_LT, fRES(v));
        }
        public void SetFa192_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_GE, fRES(v));
        }
        public void SetFa192_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_LE, fRES(v));
        }
        public void SetFa192_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa192(), "FA192");
        }
        public void SetFa192_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa192(), "FA192");
        }
        public void SetFa192_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa192_LikeSearch(v, cLSOP());
        }
        public void SetFa192_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa192(), "FA192", option);
        }
        public void SetFa192_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa192(), "FA192", option);
        }
        public void SetFa192_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa192_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa192(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa192(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa192(), "FA192");
        }
        protected abstract ConditionValue getCValueFa192();

        public void SetFa193_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa193_Equal(fRES(v));
        }
        protected void DoSetFa193_Equal(String v) { regFa193(CK_EQ, v); }
        public void SetFa193_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa193_NotEqual(fRES(v));
        }
        protected void DoSetFa193_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_NES, v);
        }
        public void SetFa193_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_GT, fRES(v));
        }
        public void SetFa193_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_LT, fRES(v));
        }
        public void SetFa193_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_GE, fRES(v));
        }
        public void SetFa193_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_LE, fRES(v));
        }
        public void SetFa193_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa193(), "FA193");
        }
        public void SetFa193_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa193(), "FA193");
        }
        public void SetFa193_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa193_LikeSearch(v, cLSOP());
        }
        public void SetFa193_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa193(), "FA193", option);
        }
        public void SetFa193_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa193(), "FA193", option);
        }
        public void SetFa193_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa193_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa193(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa193(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa193(), "FA193");
        }
        protected abstract ConditionValue getCValueFa193();

        public void SetFa194_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa194_Equal(fRES(v));
        }
        protected void DoSetFa194_Equal(String v) { regFa194(CK_EQ, v); }
        public void SetFa194_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa194_NotEqual(fRES(v));
        }
        protected void DoSetFa194_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_NES, v);
        }
        public void SetFa194_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_GT, fRES(v));
        }
        public void SetFa194_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_LT, fRES(v));
        }
        public void SetFa194_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_GE, fRES(v));
        }
        public void SetFa194_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_LE, fRES(v));
        }
        public void SetFa194_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa194(), "FA194");
        }
        public void SetFa194_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa194(), "FA194");
        }
        public void SetFa194_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa194_LikeSearch(v, cLSOP());
        }
        public void SetFa194_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa194(), "FA194", option);
        }
        public void SetFa194_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa194(), "FA194", option);
        }
        public void SetFa194_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa194_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa194(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa194(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa194(), "FA194");
        }
        protected abstract ConditionValue getCValueFa194();

        public void SetFa195_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa195_Equal(fRES(v));
        }
        protected void DoSetFa195_Equal(String v) { regFa195(CK_EQ, v); }
        public void SetFa195_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa195_NotEqual(fRES(v));
        }
        protected void DoSetFa195_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_NES, v);
        }
        public void SetFa195_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_GT, fRES(v));
        }
        public void SetFa195_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_LT, fRES(v));
        }
        public void SetFa195_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_GE, fRES(v));
        }
        public void SetFa195_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_LE, fRES(v));
        }
        public void SetFa195_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa195(), "FA195");
        }
        public void SetFa195_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa195(), "FA195");
        }
        public void SetFa195_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa195_LikeSearch(v, cLSOP());
        }
        public void SetFa195_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa195(), "FA195", option);
        }
        public void SetFa195_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa195(), "FA195", option);
        }
        public void SetFa195_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa195_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa195(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa195(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa195(), "FA195");
        }
        protected abstract ConditionValue getCValueFa195();

        public void SetFa196_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa196_Equal(fRES(v));
        }
        protected void DoSetFa196_Equal(String v) { regFa196(CK_EQ, v); }
        public void SetFa196_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa196_NotEqual(fRES(v));
        }
        protected void DoSetFa196_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_NES, v);
        }
        public void SetFa196_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_GT, fRES(v));
        }
        public void SetFa196_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_LT, fRES(v));
        }
        public void SetFa196_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_GE, fRES(v));
        }
        public void SetFa196_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_LE, fRES(v));
        }
        public void SetFa196_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa196(), "FA196");
        }
        public void SetFa196_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa196(), "FA196");
        }
        public void SetFa196_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa196_LikeSearch(v, cLSOP());
        }
        public void SetFa196_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa196(), "FA196", option);
        }
        public void SetFa196_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa196(), "FA196", option);
        }
        public void SetFa196_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa196_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa196(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa196(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa196(), "FA196");
        }
        protected abstract ConditionValue getCValueFa196();

        public void SetFa197_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa197_Equal(fRES(v));
        }
        protected void DoSetFa197_Equal(String v) { regFa197(CK_EQ, v); }
        public void SetFa197_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa197_NotEqual(fRES(v));
        }
        protected void DoSetFa197_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_NES, v);
        }
        public void SetFa197_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_GT, fRES(v));
        }
        public void SetFa197_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_LT, fRES(v));
        }
        public void SetFa197_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_GE, fRES(v));
        }
        public void SetFa197_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_LE, fRES(v));
        }
        public void SetFa197_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa197(), "FA197");
        }
        public void SetFa197_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa197(), "FA197");
        }
        public void SetFa197_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa197_LikeSearch(v, cLSOP());
        }
        public void SetFa197_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa197(), "FA197", option);
        }
        public void SetFa197_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa197(), "FA197", option);
        }
        public void SetFa197_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa197_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa197(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa197(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa197(), "FA197");
        }
        protected abstract ConditionValue getCValueFa197();

        public void SetFa198_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa198_Equal(fRES(v));
        }
        protected void DoSetFa198_Equal(String v) { regFa198(CK_EQ, v); }
        public void SetFa198_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa198_NotEqual(fRES(v));
        }
        protected void DoSetFa198_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_NES, v);
        }
        public void SetFa198_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_GT, fRES(v));
        }
        public void SetFa198_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_LT, fRES(v));
        }
        public void SetFa198_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_GE, fRES(v));
        }
        public void SetFa198_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_LE, fRES(v));
        }
        public void SetFa198_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa198(), "FA198");
        }
        public void SetFa198_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa198(), "FA198");
        }
        public void SetFa198_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa198_LikeSearch(v, cLSOP());
        }
        public void SetFa198_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa198(), "FA198", option);
        }
        public void SetFa198_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa198(), "FA198", option);
        }
        public void SetFa198_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa198_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa198(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa198(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa198(), "FA198");
        }
        protected abstract ConditionValue getCValueFa198();

        public void SetFa199_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa199_Equal(fRES(v));
        }
        protected void DoSetFa199_Equal(String v) { regFa199(CK_EQ, v); }
        public void SetFa199_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa199_NotEqual(fRES(v));
        }
        protected void DoSetFa199_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_NES, v);
        }
        public void SetFa199_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_GT, fRES(v));
        }
        public void SetFa199_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_LT, fRES(v));
        }
        public void SetFa199_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_GE, fRES(v));
        }
        public void SetFa199_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_LE, fRES(v));
        }
        public void SetFa199_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa199(), "FA199");
        }
        public void SetFa199_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa199(), "FA199");
        }
        public void SetFa199_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa199_LikeSearch(v, cLSOP());
        }
        public void SetFa199_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa199(), "FA199", option);
        }
        public void SetFa199_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa199(), "FA199", option);
        }
        public void SetFa199_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa199_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa199(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa199(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa199(), "FA199");
        }
        protected abstract ConditionValue getCValueFa199();

        public void SetFa200_Equal(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa200_Equal(fRES(v));
        }
        protected void DoSetFa200_Equal(String v) { regFa200(CK_EQ, v); }
        public void SetFa200_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            DoSetFa200_NotEqual(fRES(v));
        }
        protected void DoSetFa200_NotEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_NES, v);
        }
        public void SetFa200_GreaterThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_GT, fRES(v));
        }
        public void SetFa200_LessThan(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_LT, fRES(v));
        }
        public void SetFa200_GreaterEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_GE, fRES(v));
        }
        public void SetFa200_LessEqual(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_LE, fRES(v));
        }
        public void SetFa200_InScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_INS, cTL<String>(ls), getCValueFa200(), "FA200");
        }
        public void SetFa200_NotInScope(IList<String> ls) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regINS<String>(CK_NINS, cTL<String>(ls), getCValueFa200(), "FA200");
        }
        public void SetFa200_PrefixSearch(String v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            SetFa200_LikeSearch(v, cLSOP());
        }
        public void SetFa200_LikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_LS, fRES(v), getCValueFa200(), "FA200", option);
        }
        public void SetFa200_NotLikeSearch(String v, LikeSearchOption option) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regLSQ(CK_NLS, fRES(v), getCValueFa200(), "FA200", option);
        }
        public void SetFa200_IsNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_ISN, DUMMY_OBJECT);
        }
        public void SetFa200_IsNotNull() {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regFa200(CK_ISNN, DUMMY_OBJECT);
        }
        protected void regFa200(ConditionKey k, Object v) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            regQ(k, v, getCValueFa200(), "FA200");
        }
        protected abstract ConditionValue getCValueFa200();

        // ===================================================================================
        //                                                                    Scalar Condition
        //                                                                    ================
        public SSQFunction<TFaDataCB> Scalar_Equal() {
            return xcreateSSQFunction("=");
        }

        public SSQFunction<TFaDataCB> Scalar_NotEqual() {
            return xcreateSSQFunction("<>");
        }

        public SSQFunction<TFaDataCB> Scalar_GreaterEqual() {
            return xcreateSSQFunction(">=");
        }

        public SSQFunction<TFaDataCB> Scalar_GreaterThan() {
            return xcreateSSQFunction(">");
        }

        public SSQFunction<TFaDataCB> Scalar_LessEqual() {
            return xcreateSSQFunction("<=");
        }

        public SSQFunction<TFaDataCB> Scalar_LessThan() {
            return xcreateSSQFunction("<");
        }

        protected SSQFunction<TFaDataCB> xcreateSSQFunction(String operand) {
            return new SSQFunction<TFaDataCB>(delegate(String function, SubQuery<TFaDataCB> subQuery) {
                xscalarSubQuery(function, subQuery, operand);
            });
        }

        protected void xscalarSubQuery(String function, SubQuery<TFaDataCB> subQuery, String operand) {
            assertObjectNotNull("subQuery<TFaDataCB>", subQuery);
            TFaDataCB cb = new TFaDataCB(); cb.xsetupForScalarCondition(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepScalarSubQuery(cb.Query()); // for saving query-value.
            registerScalarSubQuery(function, cb.Query(), subQueryPropertyName, operand);
        }
        public abstract String keepScalarSubQuery(TFaDataCQ subQuery);

        // ===============================================================================
        //                                                                  MySelf InScope
        //                                                                  ==============
        public void MyselfInScope(SubQuery<TFaDataCB> subQuery) {
            WhereSetterFlag = true;    // 2013/02/21 Add cterash
            assertObjectNotNull("subQuery<TFaDataCB>", subQuery);
            TFaDataCB cb = new TFaDataCB(); cb.xsetupForInScopeRelation(this); subQuery.Invoke(cb);
            String subQueryPropertyName = keepMyselfInScopeSubQuery(cb.Query()); // for saving query-value.
            registerInScopeSubQuery(cb.Query(), "SAMPLE_ID", "SAMPLE_ID", subQueryPropertyName);
        }
        public abstract String keepMyselfInScopeSubQuery(TFaDataCQ subQuery);

        public override String ToString() { return xgetSqlClause().getClause(); }
    }
}
