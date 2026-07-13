
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
    public class TSurveyDataCIQ : AbstractBsTSurveyDataCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTSurveyDataCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TSurveyDataCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTSurveyDataCQ myCQ)
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


        protected override ConditionValue getCValueSampleId() {
            return _myCQ.SampleId;
        }


        protected override ConditionValue getCValueMergeCode() {
            return _myCQ.MergeCode;
        }


        protected override ConditionValue getCValueSortNo() {
            return _myCQ.SortNo;
        }


        protected override ConditionValue getCValueDeleteFlag() {
            return _myCQ.DeleteFlag;
        }


        protected override ConditionValue getCValueAnswerDate() {
            return _myCQ.AnswerDate;
        }


        protected override ConditionValue getCValueSex() {
            return _myCQ.Sex;
        }


        protected override ConditionValue getCValueAge() {
            return _myCQ.Age;
        }


        protected override ConditionValue getCValueAgeId() {
            return _myCQ.AgeId;
        }


        protected override ConditionValue getCValuePrefecture() {
            return _myCQ.Prefecture;
        }


        protected override ConditionValue getCValueArea() {
            return _myCQ.Area;
        }


        protected override ConditionValue getCValueMarried() {
            return _myCQ.Married;
        }


        protected override ConditionValue getCValueChild() {
            return _myCQ.Child;
        }


        protected override ConditionValue getCValueHincome() {
            return _myCQ.Hincome;
        }


        protected override ConditionValue getCValuePincome() {
            return _myCQ.Pincome;
        }


        protected override ConditionValue getCValueJob() {
            return _myCQ.Job;
        }


        protected override ConditionValue getCValueStudent() {
            return _myCQ.Student;
        }


        protected override ConditionValue getCValueCell() {
            return _myCQ.Cell;
        }


        protected override ConditionValue getCValueCellName() {
            return _myCQ.CellName;
        }


        protected override ConditionValue getCValueQ0001() {
            return _myCQ.Q0001;
        }


        protected override ConditionValue getCValueQ0002() {
            return _myCQ.Q0002;
        }


        protected override ConditionValue getCValueQ0003() {
            return _myCQ.Q0003;
        }


        protected override ConditionValue getCValueQ0004() {
            return _myCQ.Q0004;
        }


        protected override ConditionValue getCValueQ0005() {
            return _myCQ.Q0005;
        }


        protected override ConditionValue getCValueQ0006() {
            return _myCQ.Q0006;
        }


        protected override ConditionValue getCValueQ0007() {
            return _myCQ.Q0007;
        }


        protected override ConditionValue getCValueQ0008() {
            return _myCQ.Q0008;
        }


        protected override ConditionValue getCValueQ0009() {
            return _myCQ.Q0009;
        }


        protected override ConditionValue getCValueQ0010() {
            return _myCQ.Q0010;
        }


        protected override ConditionValue getCValueQ0011() {
            return _myCQ.Q0011;
        }


        protected override ConditionValue getCValueQ0012() {
            return _myCQ.Q0012;
        }


        protected override ConditionValue getCValueQ0013() {
            return _myCQ.Q0013;
        }


        protected override ConditionValue getCValueQ0014() {
            return _myCQ.Q0014;
        }


        protected override ConditionValue getCValueQ0015() {
            return _myCQ.Q0015;
        }


        protected override ConditionValue getCValueQ0016() {
            return _myCQ.Q0016;
        }


        protected override ConditionValue getCValueQ0017() {
            return _myCQ.Q0017;
        }


        protected override ConditionValue getCValueQ0018() {
            return _myCQ.Q0018;
        }


        protected override ConditionValue getCValueQ0019() {
            return _myCQ.Q0019;
        }


        protected override ConditionValue getCValueQ0020() {
            return _myCQ.Q0020;
        }


        protected override ConditionValue getCValueQ0021() {
            return _myCQ.Q0021;
        }


        protected override ConditionValue getCValueQ0022() {
            return _myCQ.Q0022;
        }


        protected override ConditionValue getCValueQ0023() {
            return _myCQ.Q0023;
        }


        protected override ConditionValue getCValueQ0024() {
            return _myCQ.Q0024;
        }


        protected override ConditionValue getCValueQ0025() {
            return _myCQ.Q0025;
        }


        protected override ConditionValue getCValueQ0026() {
            return _myCQ.Q0026;
        }


        protected override ConditionValue getCValueQ0027() {
            return _myCQ.Q0027;
        }


        protected override ConditionValue getCValueQ0028() {
            return _myCQ.Q0028;
        }


        protected override ConditionValue getCValueQ0029() {
            return _myCQ.Q0029;
        }


        protected override ConditionValue getCValueQ0030() {
            return _myCQ.Q0030;
        }


        protected override ConditionValue getCValueQ0031() {
            return _myCQ.Q0031;
        }


        protected override ConditionValue getCValueQ0032() {
            return _myCQ.Q0032;
        }


        protected override ConditionValue getCValueQ0033() {
            return _myCQ.Q0033;
        }


        protected override ConditionValue getCValueQ0034() {
            return _myCQ.Q0034;
        }


        protected override ConditionValue getCValueQ0035() {
            return _myCQ.Q0035;
        }


        protected override ConditionValue getCValueQ0036() {
            return _myCQ.Q0036;
        }


        protected override ConditionValue getCValueQ0037() {
            return _myCQ.Q0037;
        }


        protected override ConditionValue getCValueQ0038() {
            return _myCQ.Q0038;
        }


        protected override ConditionValue getCValueQ0039() {
            return _myCQ.Q0039;
        }


        protected override ConditionValue getCValueQ0040() {
            return _myCQ.Q0040;
        }


        protected override ConditionValue getCValueQ0041() {
            return _myCQ.Q0041;
        }


        protected override ConditionValue getCValueQ0042() {
            return _myCQ.Q0042;
        }


        protected override ConditionValue getCValueQ0043() {
            return _myCQ.Q0043;
        }


        protected override ConditionValue getCValueQ0044() {
            return _myCQ.Q0044;
        }


        protected override ConditionValue getCValueQ0045() {
            return _myCQ.Q0045;
        }


        protected override ConditionValue getCValueQ0046() {
            return _myCQ.Q0046;
        }


        protected override ConditionValue getCValueQ0047() {
            return _myCQ.Q0047;
        }


        protected override ConditionValue getCValueQ0048() {
            return _myCQ.Q0048;
        }


        protected override ConditionValue getCValueQ0049() {
            return _myCQ.Q0049;
        }


        protected override ConditionValue getCValueQ0050() {
            return _myCQ.Q0050;
        }


        protected override ConditionValue getCValueQ0051() {
            return _myCQ.Q0051;
        }


        protected override ConditionValue getCValueQ0052() {
            return _myCQ.Q0052;
        }


        protected override ConditionValue getCValueQ0053() {
            return _myCQ.Q0053;
        }


        protected override ConditionValue getCValueQ0054() {
            return _myCQ.Q0054;
        }


        protected override ConditionValue getCValueQ0055() {
            return _myCQ.Q0055;
        }


        protected override ConditionValue getCValueQ0056() {
            return _myCQ.Q0056;
        }


        protected override ConditionValue getCValueQ0057() {
            return _myCQ.Q0057;
        }


        protected override ConditionValue getCValueQ0058() {
            return _myCQ.Q0058;
        }


        protected override ConditionValue getCValueQ0059() {
            return _myCQ.Q0059;
        }


        protected override ConditionValue getCValueQ0060() {
            return _myCQ.Q0060;
        }


        protected override ConditionValue getCValueQ0061() {
            return _myCQ.Q0061;
        }


        protected override ConditionValue getCValueQ0062() {
            return _myCQ.Q0062;
        }


        protected override ConditionValue getCValueQ0063() {
            return _myCQ.Q0063;
        }


        protected override ConditionValue getCValueQ0064() {
            return _myCQ.Q0064;
        }


        protected override ConditionValue getCValueQ0065() {
            return _myCQ.Q0065;
        }


        protected override ConditionValue getCValueQ0066() {
            return _myCQ.Q0066;
        }


        protected override ConditionValue getCValueQ0067() {
            return _myCQ.Q0067;
        }


        protected override ConditionValue getCValueQ0068() {
            return _myCQ.Q0068;
        }


        protected override ConditionValue getCValueQ0069() {
            return _myCQ.Q0069;
        }


        protected override ConditionValue getCValueQ0070() {
            return _myCQ.Q0070;
        }


        protected override ConditionValue getCValueQ0071() {
            return _myCQ.Q0071;
        }


        protected override ConditionValue getCValueQ0072() {
            return _myCQ.Q0072;
        }


        protected override ConditionValue getCValueQ0073() {
            return _myCQ.Q0073;
        }


        protected override ConditionValue getCValueQ0074() {
            return _myCQ.Q0074;
        }


        protected override ConditionValue getCValueQ0075() {
            return _myCQ.Q0075;
        }


        protected override ConditionValue getCValueQ0076() {
            return _myCQ.Q0076;
        }


        protected override ConditionValue getCValueQ0077() {
            return _myCQ.Q0077;
        }


        protected override ConditionValue getCValueQ0078() {
            return _myCQ.Q0078;
        }


        protected override ConditionValue getCValueQ0079() {
            return _myCQ.Q0079;
        }


        protected override ConditionValue getCValueQ0080() {
            return _myCQ.Q0080;
        }


        protected override ConditionValue getCValueQ0081() {
            return _myCQ.Q0081;
        }


        protected override ConditionValue getCValueQ0082() {
            return _myCQ.Q0082;
        }


        protected override ConditionValue getCValueQ0083() {
            return _myCQ.Q0083;
        }


        protected override ConditionValue getCValueQ0084() {
            return _myCQ.Q0084;
        }


        protected override ConditionValue getCValueQ0085() {
            return _myCQ.Q0085;
        }


        protected override ConditionValue getCValueQ0086() {
            return _myCQ.Q0086;
        }


        protected override ConditionValue getCValueQ0087() {
            return _myCQ.Q0087;
        }


        protected override ConditionValue getCValueQ0088() {
            return _myCQ.Q0088;
        }


        protected override ConditionValue getCValueQ0089() {
            return _myCQ.Q0089;
        }


        protected override ConditionValue getCValueQ0090() {
            return _myCQ.Q0090;
        }


        protected override ConditionValue getCValueQ0091() {
            return _myCQ.Q0091;
        }


        protected override ConditionValue getCValueQ0092() {
            return _myCQ.Q0092;
        }


        protected override ConditionValue getCValueQ0093() {
            return _myCQ.Q0093;
        }


        protected override ConditionValue getCValueQ0094() {
            return _myCQ.Q0094;
        }


        protected override ConditionValue getCValueQ0095() {
            return _myCQ.Q0095;
        }


        protected override ConditionValue getCValueQ0096() {
            return _myCQ.Q0096;
        }


        protected override ConditionValue getCValueQ0097() {
            return _myCQ.Q0097;
        }


        protected override ConditionValue getCValueQ0098() {
            return _myCQ.Q0098;
        }


        protected override ConditionValue getCValueQ0099() {
            return _myCQ.Q0099;
        }


        protected override ConditionValue getCValueQ0100() {
            return _myCQ.Q0100;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TSurveyDataCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TSurveyDataCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
