
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTSurveyDataCQ : AbstractBsTSurveyDataCQ {

        protected TSurveyDataCIQ _inlineQuery;

        public BsTSurveyDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TSurveyDataCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TSurveyDataCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TSurveyDataCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TSurveyDataCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _sampleId;
        public ConditionValue SampleId {
            get { if (_sampleId == null) { _sampleId = new ConditionValue(); } return _sampleId; }
        }
        protected override ConditionValue getCValueSampleId() { return this.SampleId; }


        public BsTSurveyDataCQ AddOrderBy_SampleId_Asc() { regOBA("SAMPLE_ID");return this; }
        public BsTSurveyDataCQ AddOrderBy_SampleId_Desc() { regOBD("SAMPLE_ID");return this; }

        protected ConditionValue _mergeCode;
        public ConditionValue MergeCode {
            get { if (_mergeCode == null) { _mergeCode = new ConditionValue(); } return _mergeCode; }
        }
        protected override ConditionValue getCValueMergeCode() { return this.MergeCode; }


        public BsTSurveyDataCQ AddOrderBy_MergeCode_Asc() { regOBA("MERGE_CODE");return this; }
        public BsTSurveyDataCQ AddOrderBy_MergeCode_Desc() { regOBD("MERGE_CODE");return this; }

        protected ConditionValue _sortNo;
        public ConditionValue SortNo {
            get { if (_sortNo == null) { _sortNo = new ConditionValue(); } return _sortNo; }
        }
        protected override ConditionValue getCValueSortNo() { return this.SortNo; }


        public BsTSurveyDataCQ AddOrderBy_SortNo_Asc() { regOBA("SORT_NO");return this; }
        public BsTSurveyDataCQ AddOrderBy_SortNo_Desc() { regOBD("SORT_NO");return this; }

        protected ConditionValue _deleteFlag;
        public ConditionValue DeleteFlag {
            get { if (_deleteFlag == null) { _deleteFlag = new ConditionValue(); } return _deleteFlag; }
        }
        protected override ConditionValue getCValueDeleteFlag() { return this.DeleteFlag; }


        public BsTSurveyDataCQ AddOrderBy_DeleteFlag_Asc() { regOBA("DELETE_FLAG");return this; }
        public BsTSurveyDataCQ AddOrderBy_DeleteFlag_Desc() { regOBD("DELETE_FLAG");return this; }

        protected ConditionValue _answerDate;
        public ConditionValue AnswerDate {
            get { if (_answerDate == null) { _answerDate = new ConditionValue(); } return _answerDate; }
        }
        protected override ConditionValue getCValueAnswerDate() { return this.AnswerDate; }


        public BsTSurveyDataCQ AddOrderBy_AnswerDate_Asc() { regOBA("ANSWER_DATE");return this; }
        public BsTSurveyDataCQ AddOrderBy_AnswerDate_Desc() { regOBD("ANSWER_DATE");return this; }

        protected ConditionValue _sex;
        public ConditionValue Sex {
            get { if (_sex == null) { _sex = new ConditionValue(); } return _sex; }
        }
        protected override ConditionValue getCValueSex() { return this.Sex; }


        public BsTSurveyDataCQ AddOrderBy_Sex_Asc() { regOBA("SEX");return this; }
        public BsTSurveyDataCQ AddOrderBy_Sex_Desc() { regOBD("SEX");return this; }

        protected ConditionValue _age;
        public ConditionValue Age {
            get { if (_age == null) { _age = new ConditionValue(); } return _age; }
        }
        protected override ConditionValue getCValueAge() { return this.Age; }


        public BsTSurveyDataCQ AddOrderBy_Age_Asc() { regOBA("AGE");return this; }
        public BsTSurveyDataCQ AddOrderBy_Age_Desc() { regOBD("AGE");return this; }

        protected ConditionValue _ageId;
        public ConditionValue AgeId {
            get { if (_ageId == null) { _ageId = new ConditionValue(); } return _ageId; }
        }
        protected override ConditionValue getCValueAgeId() { return this.AgeId; }


        public BsTSurveyDataCQ AddOrderBy_AgeId_Asc() { regOBA("AGE_ID");return this; }
        public BsTSurveyDataCQ AddOrderBy_AgeId_Desc() { regOBD("AGE_ID");return this; }

        protected ConditionValue _prefecture;
        public ConditionValue Prefecture {
            get { if (_prefecture == null) { _prefecture = new ConditionValue(); } return _prefecture; }
        }
        protected override ConditionValue getCValuePrefecture() { return this.Prefecture; }


        public BsTSurveyDataCQ AddOrderBy_Prefecture_Asc() { regOBA("PREFECTURE");return this; }
        public BsTSurveyDataCQ AddOrderBy_Prefecture_Desc() { regOBD("PREFECTURE");return this; }

        protected ConditionValue _area;
        public ConditionValue Area {
            get { if (_area == null) { _area = new ConditionValue(); } return _area; }
        }
        protected override ConditionValue getCValueArea() { return this.Area; }


        public BsTSurveyDataCQ AddOrderBy_Area_Asc() { regOBA("AREA");return this; }
        public BsTSurveyDataCQ AddOrderBy_Area_Desc() { regOBD("AREA");return this; }

        protected ConditionValue _married;
        public ConditionValue Married {
            get { if (_married == null) { _married = new ConditionValue(); } return _married; }
        }
        protected override ConditionValue getCValueMarried() { return this.Married; }


        public BsTSurveyDataCQ AddOrderBy_Married_Asc() { regOBA("MARRIED");return this; }
        public BsTSurveyDataCQ AddOrderBy_Married_Desc() { regOBD("MARRIED");return this; }

        protected ConditionValue _child;
        public ConditionValue Child {
            get { if (_child == null) { _child = new ConditionValue(); } return _child; }
        }
        protected override ConditionValue getCValueChild() { return this.Child; }


        public BsTSurveyDataCQ AddOrderBy_Child_Asc() { regOBA("CHILD");return this; }
        public BsTSurveyDataCQ AddOrderBy_Child_Desc() { regOBD("CHILD");return this; }

        protected ConditionValue _hincome;
        public ConditionValue Hincome {
            get { if (_hincome == null) { _hincome = new ConditionValue(); } return _hincome; }
        }
        protected override ConditionValue getCValueHincome() { return this.Hincome; }


        public BsTSurveyDataCQ AddOrderBy_Hincome_Asc() { regOBA("HINCOME");return this; }
        public BsTSurveyDataCQ AddOrderBy_Hincome_Desc() { regOBD("HINCOME");return this; }

        protected ConditionValue _pincome;
        public ConditionValue Pincome {
            get { if (_pincome == null) { _pincome = new ConditionValue(); } return _pincome; }
        }
        protected override ConditionValue getCValuePincome() { return this.Pincome; }


        public BsTSurveyDataCQ AddOrderBy_Pincome_Asc() { regOBA("PINCOME");return this; }
        public BsTSurveyDataCQ AddOrderBy_Pincome_Desc() { regOBD("PINCOME");return this; }

        protected ConditionValue _job;
        public ConditionValue Job {
            get { if (_job == null) { _job = new ConditionValue(); } return _job; }
        }
        protected override ConditionValue getCValueJob() { return this.Job; }


        public BsTSurveyDataCQ AddOrderBy_Job_Asc() { regOBA("JOB");return this; }
        public BsTSurveyDataCQ AddOrderBy_Job_Desc() { regOBD("JOB");return this; }

        protected ConditionValue _student;
        public ConditionValue Student {
            get { if (_student == null) { _student = new ConditionValue(); } return _student; }
        }
        protected override ConditionValue getCValueStudent() { return this.Student; }


        public BsTSurveyDataCQ AddOrderBy_Student_Asc() { regOBA("STUDENT");return this; }
        public BsTSurveyDataCQ AddOrderBy_Student_Desc() { regOBD("STUDENT");return this; }

        protected ConditionValue _cell;
        public ConditionValue Cell {
            get { if (_cell == null) { _cell = new ConditionValue(); } return _cell; }
        }
        protected override ConditionValue getCValueCell() { return this.Cell; }


        public BsTSurveyDataCQ AddOrderBy_Cell_Asc() { regOBA("CELL");return this; }
        public BsTSurveyDataCQ AddOrderBy_Cell_Desc() { regOBD("CELL");return this; }

        protected ConditionValue _cellName;
        public ConditionValue CellName {
            get { if (_cellName == null) { _cellName = new ConditionValue(); } return _cellName; }
        }
        protected override ConditionValue getCValueCellName() { return this.CellName; }


        public BsTSurveyDataCQ AddOrderBy_CellName_Asc() { regOBA("CELL_NAME");return this; }
        public BsTSurveyDataCQ AddOrderBy_CellName_Desc() { regOBD("CELL_NAME");return this; }

        protected ConditionValue _q0001;
        public ConditionValue Q0001 {
            get { if (_q0001 == null) { _q0001 = new ConditionValue(); } return _q0001; }
        }
        protected override ConditionValue getCValueQ0001() { return this.Q0001; }


        public BsTSurveyDataCQ AddOrderBy_Q0001_Asc() { regOBA("Q0001");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0001_Desc() { regOBD("Q0001");return this; }

        protected ConditionValue _q0002;
        public ConditionValue Q0002 {
            get { if (_q0002 == null) { _q0002 = new ConditionValue(); } return _q0002; }
        }
        protected override ConditionValue getCValueQ0002() { return this.Q0002; }


        public BsTSurveyDataCQ AddOrderBy_Q0002_Asc() { regOBA("Q0002");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0002_Desc() { regOBD("Q0002");return this; }

        protected ConditionValue _q0003;
        public ConditionValue Q0003 {
            get { if (_q0003 == null) { _q0003 = new ConditionValue(); } return _q0003; }
        }
        protected override ConditionValue getCValueQ0003() { return this.Q0003; }


        public BsTSurveyDataCQ AddOrderBy_Q0003_Asc() { regOBA("Q0003");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0003_Desc() { regOBD("Q0003");return this; }

        protected ConditionValue _q0004;
        public ConditionValue Q0004 {
            get { if (_q0004 == null) { _q0004 = new ConditionValue(); } return _q0004; }
        }
        protected override ConditionValue getCValueQ0004() { return this.Q0004; }


        public BsTSurveyDataCQ AddOrderBy_Q0004_Asc() { regOBA("Q0004");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0004_Desc() { regOBD("Q0004");return this; }

        protected ConditionValue _q0005;
        public ConditionValue Q0005 {
            get { if (_q0005 == null) { _q0005 = new ConditionValue(); } return _q0005; }
        }
        protected override ConditionValue getCValueQ0005() { return this.Q0005; }


        public BsTSurveyDataCQ AddOrderBy_Q0005_Asc() { regOBA("Q0005");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0005_Desc() { regOBD("Q0005");return this; }

        protected ConditionValue _q0006;
        public ConditionValue Q0006 {
            get { if (_q0006 == null) { _q0006 = new ConditionValue(); } return _q0006; }
        }
        protected override ConditionValue getCValueQ0006() { return this.Q0006; }


        public BsTSurveyDataCQ AddOrderBy_Q0006_Asc() { regOBA("Q0006");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0006_Desc() { regOBD("Q0006");return this; }

        protected ConditionValue _q0007;
        public ConditionValue Q0007 {
            get { if (_q0007 == null) { _q0007 = new ConditionValue(); } return _q0007; }
        }
        protected override ConditionValue getCValueQ0007() { return this.Q0007; }


        public BsTSurveyDataCQ AddOrderBy_Q0007_Asc() { regOBA("Q0007");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0007_Desc() { regOBD("Q0007");return this; }

        protected ConditionValue _q0008;
        public ConditionValue Q0008 {
            get { if (_q0008 == null) { _q0008 = new ConditionValue(); } return _q0008; }
        }
        protected override ConditionValue getCValueQ0008() { return this.Q0008; }


        public BsTSurveyDataCQ AddOrderBy_Q0008_Asc() { regOBA("Q0008");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0008_Desc() { regOBD("Q0008");return this; }

        protected ConditionValue _q0009;
        public ConditionValue Q0009 {
            get { if (_q0009 == null) { _q0009 = new ConditionValue(); } return _q0009; }
        }
        protected override ConditionValue getCValueQ0009() { return this.Q0009; }


        public BsTSurveyDataCQ AddOrderBy_Q0009_Asc() { regOBA("Q0009");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0009_Desc() { regOBD("Q0009");return this; }

        protected ConditionValue _q0010;
        public ConditionValue Q0010 {
            get { if (_q0010 == null) { _q0010 = new ConditionValue(); } return _q0010; }
        }
        protected override ConditionValue getCValueQ0010() { return this.Q0010; }


        public BsTSurveyDataCQ AddOrderBy_Q0010_Asc() { regOBA("Q0010");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0010_Desc() { regOBD("Q0010");return this; }

        protected ConditionValue _q0011;
        public ConditionValue Q0011 {
            get { if (_q0011 == null) { _q0011 = new ConditionValue(); } return _q0011; }
        }
        protected override ConditionValue getCValueQ0011() { return this.Q0011; }


        public BsTSurveyDataCQ AddOrderBy_Q0011_Asc() { regOBA("Q0011");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0011_Desc() { regOBD("Q0011");return this; }

        protected ConditionValue _q0012;
        public ConditionValue Q0012 {
            get { if (_q0012 == null) { _q0012 = new ConditionValue(); } return _q0012; }
        }
        protected override ConditionValue getCValueQ0012() { return this.Q0012; }


        public BsTSurveyDataCQ AddOrderBy_Q0012_Asc() { regOBA("Q0012");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0012_Desc() { regOBD("Q0012");return this; }

        protected ConditionValue _q0013;
        public ConditionValue Q0013 {
            get { if (_q0013 == null) { _q0013 = new ConditionValue(); } return _q0013; }
        }
        protected override ConditionValue getCValueQ0013() { return this.Q0013; }


        public BsTSurveyDataCQ AddOrderBy_Q0013_Asc() { regOBA("Q0013");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0013_Desc() { regOBD("Q0013");return this; }

        protected ConditionValue _q0014;
        public ConditionValue Q0014 {
            get { if (_q0014 == null) { _q0014 = new ConditionValue(); } return _q0014; }
        }
        protected override ConditionValue getCValueQ0014() { return this.Q0014; }


        public BsTSurveyDataCQ AddOrderBy_Q0014_Asc() { regOBA("Q0014");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0014_Desc() { regOBD("Q0014");return this; }

        protected ConditionValue _q0015;
        public ConditionValue Q0015 {
            get { if (_q0015 == null) { _q0015 = new ConditionValue(); } return _q0015; }
        }
        protected override ConditionValue getCValueQ0015() { return this.Q0015; }


        public BsTSurveyDataCQ AddOrderBy_Q0015_Asc() { regOBA("Q0015");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0015_Desc() { regOBD("Q0015");return this; }

        protected ConditionValue _q0016;
        public ConditionValue Q0016 {
            get { if (_q0016 == null) { _q0016 = new ConditionValue(); } return _q0016; }
        }
        protected override ConditionValue getCValueQ0016() { return this.Q0016; }


        public BsTSurveyDataCQ AddOrderBy_Q0016_Asc() { regOBA("Q0016");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0016_Desc() { regOBD("Q0016");return this; }

        protected ConditionValue _q0017;
        public ConditionValue Q0017 {
            get { if (_q0017 == null) { _q0017 = new ConditionValue(); } return _q0017; }
        }
        protected override ConditionValue getCValueQ0017() { return this.Q0017; }


        public BsTSurveyDataCQ AddOrderBy_Q0017_Asc() { regOBA("Q0017");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0017_Desc() { regOBD("Q0017");return this; }

        protected ConditionValue _q0018;
        public ConditionValue Q0018 {
            get { if (_q0018 == null) { _q0018 = new ConditionValue(); } return _q0018; }
        }
        protected override ConditionValue getCValueQ0018() { return this.Q0018; }


        public BsTSurveyDataCQ AddOrderBy_Q0018_Asc() { regOBA("Q0018");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0018_Desc() { regOBD("Q0018");return this; }

        protected ConditionValue _q0019;
        public ConditionValue Q0019 {
            get { if (_q0019 == null) { _q0019 = new ConditionValue(); } return _q0019; }
        }
        protected override ConditionValue getCValueQ0019() { return this.Q0019; }


        public BsTSurveyDataCQ AddOrderBy_Q0019_Asc() { regOBA("Q0019");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0019_Desc() { regOBD("Q0019");return this; }

        protected ConditionValue _q0020;
        public ConditionValue Q0020 {
            get { if (_q0020 == null) { _q0020 = new ConditionValue(); } return _q0020; }
        }
        protected override ConditionValue getCValueQ0020() { return this.Q0020; }


        public BsTSurveyDataCQ AddOrderBy_Q0020_Asc() { regOBA("Q0020");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0020_Desc() { regOBD("Q0020");return this; }

        protected ConditionValue _q0021;
        public ConditionValue Q0021 {
            get { if (_q0021 == null) { _q0021 = new ConditionValue(); } return _q0021; }
        }
        protected override ConditionValue getCValueQ0021() { return this.Q0021; }


        public BsTSurveyDataCQ AddOrderBy_Q0021_Asc() { regOBA("Q0021");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0021_Desc() { regOBD("Q0021");return this; }

        protected ConditionValue _q0022;
        public ConditionValue Q0022 {
            get { if (_q0022 == null) { _q0022 = new ConditionValue(); } return _q0022; }
        }
        protected override ConditionValue getCValueQ0022() { return this.Q0022; }


        public BsTSurveyDataCQ AddOrderBy_Q0022_Asc() { regOBA("Q0022");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0022_Desc() { regOBD("Q0022");return this; }

        protected ConditionValue _q0023;
        public ConditionValue Q0023 {
            get { if (_q0023 == null) { _q0023 = new ConditionValue(); } return _q0023; }
        }
        protected override ConditionValue getCValueQ0023() { return this.Q0023; }


        public BsTSurveyDataCQ AddOrderBy_Q0023_Asc() { regOBA("Q0023");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0023_Desc() { regOBD("Q0023");return this; }

        protected ConditionValue _q0024;
        public ConditionValue Q0024 {
            get { if (_q0024 == null) { _q0024 = new ConditionValue(); } return _q0024; }
        }
        protected override ConditionValue getCValueQ0024() { return this.Q0024; }


        public BsTSurveyDataCQ AddOrderBy_Q0024_Asc() { regOBA("Q0024");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0024_Desc() { regOBD("Q0024");return this; }

        protected ConditionValue _q0025;
        public ConditionValue Q0025 {
            get { if (_q0025 == null) { _q0025 = new ConditionValue(); } return _q0025; }
        }
        protected override ConditionValue getCValueQ0025() { return this.Q0025; }


        public BsTSurveyDataCQ AddOrderBy_Q0025_Asc() { regOBA("Q0025");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0025_Desc() { regOBD("Q0025");return this; }

        protected ConditionValue _q0026;
        public ConditionValue Q0026 {
            get { if (_q0026 == null) { _q0026 = new ConditionValue(); } return _q0026; }
        }
        protected override ConditionValue getCValueQ0026() { return this.Q0026; }


        public BsTSurveyDataCQ AddOrderBy_Q0026_Asc() { regOBA("Q0026");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0026_Desc() { regOBD("Q0026");return this; }

        protected ConditionValue _q0027;
        public ConditionValue Q0027 {
            get { if (_q0027 == null) { _q0027 = new ConditionValue(); } return _q0027; }
        }
        protected override ConditionValue getCValueQ0027() { return this.Q0027; }


        public BsTSurveyDataCQ AddOrderBy_Q0027_Asc() { regOBA("Q0027");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0027_Desc() { regOBD("Q0027");return this; }

        protected ConditionValue _q0028;
        public ConditionValue Q0028 {
            get { if (_q0028 == null) { _q0028 = new ConditionValue(); } return _q0028; }
        }
        protected override ConditionValue getCValueQ0028() { return this.Q0028; }


        public BsTSurveyDataCQ AddOrderBy_Q0028_Asc() { regOBA("Q0028");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0028_Desc() { regOBD("Q0028");return this; }

        protected ConditionValue _q0029;
        public ConditionValue Q0029 {
            get { if (_q0029 == null) { _q0029 = new ConditionValue(); } return _q0029; }
        }
        protected override ConditionValue getCValueQ0029() { return this.Q0029; }


        public BsTSurveyDataCQ AddOrderBy_Q0029_Asc() { regOBA("Q0029");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0029_Desc() { regOBD("Q0029");return this; }

        protected ConditionValue _q0030;
        public ConditionValue Q0030 {
            get { if (_q0030 == null) { _q0030 = new ConditionValue(); } return _q0030; }
        }
        protected override ConditionValue getCValueQ0030() { return this.Q0030; }


        public BsTSurveyDataCQ AddOrderBy_Q0030_Asc() { regOBA("Q0030");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0030_Desc() { regOBD("Q0030");return this; }

        protected ConditionValue _q0031;
        public ConditionValue Q0031 {
            get { if (_q0031 == null) { _q0031 = new ConditionValue(); } return _q0031; }
        }
        protected override ConditionValue getCValueQ0031() { return this.Q0031; }


        public BsTSurveyDataCQ AddOrderBy_Q0031_Asc() { regOBA("Q0031");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0031_Desc() { regOBD("Q0031");return this; }

        protected ConditionValue _q0032;
        public ConditionValue Q0032 {
            get { if (_q0032 == null) { _q0032 = new ConditionValue(); } return _q0032; }
        }
        protected override ConditionValue getCValueQ0032() { return this.Q0032; }


        public BsTSurveyDataCQ AddOrderBy_Q0032_Asc() { regOBA("Q0032");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0032_Desc() { regOBD("Q0032");return this; }

        protected ConditionValue _q0033;
        public ConditionValue Q0033 {
            get { if (_q0033 == null) { _q0033 = new ConditionValue(); } return _q0033; }
        }
        protected override ConditionValue getCValueQ0033() { return this.Q0033; }


        public BsTSurveyDataCQ AddOrderBy_Q0033_Asc() { regOBA("Q0033");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0033_Desc() { regOBD("Q0033");return this; }

        protected ConditionValue _q0034;
        public ConditionValue Q0034 {
            get { if (_q0034 == null) { _q0034 = new ConditionValue(); } return _q0034; }
        }
        protected override ConditionValue getCValueQ0034() { return this.Q0034; }


        public BsTSurveyDataCQ AddOrderBy_Q0034_Asc() { regOBA("Q0034");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0034_Desc() { regOBD("Q0034");return this; }

        protected ConditionValue _q0035;
        public ConditionValue Q0035 {
            get { if (_q0035 == null) { _q0035 = new ConditionValue(); } return _q0035; }
        }
        protected override ConditionValue getCValueQ0035() { return this.Q0035; }


        public BsTSurveyDataCQ AddOrderBy_Q0035_Asc() { regOBA("Q0035");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0035_Desc() { regOBD("Q0035");return this; }

        protected ConditionValue _q0036;
        public ConditionValue Q0036 {
            get { if (_q0036 == null) { _q0036 = new ConditionValue(); } return _q0036; }
        }
        protected override ConditionValue getCValueQ0036() { return this.Q0036; }


        public BsTSurveyDataCQ AddOrderBy_Q0036_Asc() { regOBA("Q0036");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0036_Desc() { regOBD("Q0036");return this; }

        protected ConditionValue _q0037;
        public ConditionValue Q0037 {
            get { if (_q0037 == null) { _q0037 = new ConditionValue(); } return _q0037; }
        }
        protected override ConditionValue getCValueQ0037() { return this.Q0037; }


        public BsTSurveyDataCQ AddOrderBy_Q0037_Asc() { regOBA("Q0037");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0037_Desc() { regOBD("Q0037");return this; }

        protected ConditionValue _q0038;
        public ConditionValue Q0038 {
            get { if (_q0038 == null) { _q0038 = new ConditionValue(); } return _q0038; }
        }
        protected override ConditionValue getCValueQ0038() { return this.Q0038; }


        public BsTSurveyDataCQ AddOrderBy_Q0038_Asc() { regOBA("Q0038");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0038_Desc() { regOBD("Q0038");return this; }

        protected ConditionValue _q0039;
        public ConditionValue Q0039 {
            get { if (_q0039 == null) { _q0039 = new ConditionValue(); } return _q0039; }
        }
        protected override ConditionValue getCValueQ0039() { return this.Q0039; }


        public BsTSurveyDataCQ AddOrderBy_Q0039_Asc() { regOBA("Q0039");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0039_Desc() { regOBD("Q0039");return this; }

        protected ConditionValue _q0040;
        public ConditionValue Q0040 {
            get { if (_q0040 == null) { _q0040 = new ConditionValue(); } return _q0040; }
        }
        protected override ConditionValue getCValueQ0040() { return this.Q0040; }


        public BsTSurveyDataCQ AddOrderBy_Q0040_Asc() { regOBA("Q0040");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0040_Desc() { regOBD("Q0040");return this; }

        protected ConditionValue _q0041;
        public ConditionValue Q0041 {
            get { if (_q0041 == null) { _q0041 = new ConditionValue(); } return _q0041; }
        }
        protected override ConditionValue getCValueQ0041() { return this.Q0041; }


        public BsTSurveyDataCQ AddOrderBy_Q0041_Asc() { regOBA("Q0041");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0041_Desc() { regOBD("Q0041");return this; }

        protected ConditionValue _q0042;
        public ConditionValue Q0042 {
            get { if (_q0042 == null) { _q0042 = new ConditionValue(); } return _q0042; }
        }
        protected override ConditionValue getCValueQ0042() { return this.Q0042; }


        public BsTSurveyDataCQ AddOrderBy_Q0042_Asc() { regOBA("Q0042");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0042_Desc() { regOBD("Q0042");return this; }

        protected ConditionValue _q0043;
        public ConditionValue Q0043 {
            get { if (_q0043 == null) { _q0043 = new ConditionValue(); } return _q0043; }
        }
        protected override ConditionValue getCValueQ0043() { return this.Q0043; }


        public BsTSurveyDataCQ AddOrderBy_Q0043_Asc() { regOBA("Q0043");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0043_Desc() { regOBD("Q0043");return this; }

        protected ConditionValue _q0044;
        public ConditionValue Q0044 {
            get { if (_q0044 == null) { _q0044 = new ConditionValue(); } return _q0044; }
        }
        protected override ConditionValue getCValueQ0044() { return this.Q0044; }


        public BsTSurveyDataCQ AddOrderBy_Q0044_Asc() { regOBA("Q0044");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0044_Desc() { regOBD("Q0044");return this; }

        protected ConditionValue _q0045;
        public ConditionValue Q0045 {
            get { if (_q0045 == null) { _q0045 = new ConditionValue(); } return _q0045; }
        }
        protected override ConditionValue getCValueQ0045() { return this.Q0045; }


        public BsTSurveyDataCQ AddOrderBy_Q0045_Asc() { regOBA("Q0045");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0045_Desc() { regOBD("Q0045");return this; }

        protected ConditionValue _q0046;
        public ConditionValue Q0046 {
            get { if (_q0046 == null) { _q0046 = new ConditionValue(); } return _q0046; }
        }
        protected override ConditionValue getCValueQ0046() { return this.Q0046; }


        public BsTSurveyDataCQ AddOrderBy_Q0046_Asc() { regOBA("Q0046");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0046_Desc() { regOBD("Q0046");return this; }

        protected ConditionValue _q0047;
        public ConditionValue Q0047 {
            get { if (_q0047 == null) { _q0047 = new ConditionValue(); } return _q0047; }
        }
        protected override ConditionValue getCValueQ0047() { return this.Q0047; }


        public BsTSurveyDataCQ AddOrderBy_Q0047_Asc() { regOBA("Q0047");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0047_Desc() { regOBD("Q0047");return this; }

        protected ConditionValue _q0048;
        public ConditionValue Q0048 {
            get { if (_q0048 == null) { _q0048 = new ConditionValue(); } return _q0048; }
        }
        protected override ConditionValue getCValueQ0048() { return this.Q0048; }


        public BsTSurveyDataCQ AddOrderBy_Q0048_Asc() { regOBA("Q0048");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0048_Desc() { regOBD("Q0048");return this; }

        protected ConditionValue _q0049;
        public ConditionValue Q0049 {
            get { if (_q0049 == null) { _q0049 = new ConditionValue(); } return _q0049; }
        }
        protected override ConditionValue getCValueQ0049() { return this.Q0049; }


        public BsTSurveyDataCQ AddOrderBy_Q0049_Asc() { regOBA("Q0049");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0049_Desc() { regOBD("Q0049");return this; }

        protected ConditionValue _q0050;
        public ConditionValue Q0050 {
            get { if (_q0050 == null) { _q0050 = new ConditionValue(); } return _q0050; }
        }
        protected override ConditionValue getCValueQ0050() { return this.Q0050; }


        public BsTSurveyDataCQ AddOrderBy_Q0050_Asc() { regOBA("Q0050");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0050_Desc() { regOBD("Q0050");return this; }

        protected ConditionValue _q0051;
        public ConditionValue Q0051 {
            get { if (_q0051 == null) { _q0051 = new ConditionValue(); } return _q0051; }
        }
        protected override ConditionValue getCValueQ0051() { return this.Q0051; }


        public BsTSurveyDataCQ AddOrderBy_Q0051_Asc() { regOBA("Q0051");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0051_Desc() { regOBD("Q0051");return this; }

        protected ConditionValue _q0052;
        public ConditionValue Q0052 {
            get { if (_q0052 == null) { _q0052 = new ConditionValue(); } return _q0052; }
        }
        protected override ConditionValue getCValueQ0052() { return this.Q0052; }


        public BsTSurveyDataCQ AddOrderBy_Q0052_Asc() { regOBA("Q0052");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0052_Desc() { regOBD("Q0052");return this; }

        protected ConditionValue _q0053;
        public ConditionValue Q0053 {
            get { if (_q0053 == null) { _q0053 = new ConditionValue(); } return _q0053; }
        }
        protected override ConditionValue getCValueQ0053() { return this.Q0053; }


        public BsTSurveyDataCQ AddOrderBy_Q0053_Asc() { regOBA("Q0053");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0053_Desc() { regOBD("Q0053");return this; }

        protected ConditionValue _q0054;
        public ConditionValue Q0054 {
            get { if (_q0054 == null) { _q0054 = new ConditionValue(); } return _q0054; }
        }
        protected override ConditionValue getCValueQ0054() { return this.Q0054; }


        public BsTSurveyDataCQ AddOrderBy_Q0054_Asc() { regOBA("Q0054");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0054_Desc() { regOBD("Q0054");return this; }

        protected ConditionValue _q0055;
        public ConditionValue Q0055 {
            get { if (_q0055 == null) { _q0055 = new ConditionValue(); } return _q0055; }
        }
        protected override ConditionValue getCValueQ0055() { return this.Q0055; }


        public BsTSurveyDataCQ AddOrderBy_Q0055_Asc() { regOBA("Q0055");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0055_Desc() { regOBD("Q0055");return this; }

        protected ConditionValue _q0056;
        public ConditionValue Q0056 {
            get { if (_q0056 == null) { _q0056 = new ConditionValue(); } return _q0056; }
        }
        protected override ConditionValue getCValueQ0056() { return this.Q0056; }


        public BsTSurveyDataCQ AddOrderBy_Q0056_Asc() { regOBA("Q0056");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0056_Desc() { regOBD("Q0056");return this; }

        protected ConditionValue _q0057;
        public ConditionValue Q0057 {
            get { if (_q0057 == null) { _q0057 = new ConditionValue(); } return _q0057; }
        }
        protected override ConditionValue getCValueQ0057() { return this.Q0057; }


        public BsTSurveyDataCQ AddOrderBy_Q0057_Asc() { regOBA("Q0057");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0057_Desc() { regOBD("Q0057");return this; }

        protected ConditionValue _q0058;
        public ConditionValue Q0058 {
            get { if (_q0058 == null) { _q0058 = new ConditionValue(); } return _q0058; }
        }
        protected override ConditionValue getCValueQ0058() { return this.Q0058; }


        public BsTSurveyDataCQ AddOrderBy_Q0058_Asc() { regOBA("Q0058");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0058_Desc() { regOBD("Q0058");return this; }

        protected ConditionValue _q0059;
        public ConditionValue Q0059 {
            get { if (_q0059 == null) { _q0059 = new ConditionValue(); } return _q0059; }
        }
        protected override ConditionValue getCValueQ0059() { return this.Q0059; }


        public BsTSurveyDataCQ AddOrderBy_Q0059_Asc() { regOBA("Q0059");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0059_Desc() { regOBD("Q0059");return this; }

        protected ConditionValue _q0060;
        public ConditionValue Q0060 {
            get { if (_q0060 == null) { _q0060 = new ConditionValue(); } return _q0060; }
        }
        protected override ConditionValue getCValueQ0060() { return this.Q0060; }


        public BsTSurveyDataCQ AddOrderBy_Q0060_Asc() { regOBA("Q0060");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0060_Desc() { regOBD("Q0060");return this; }

        protected ConditionValue _q0061;
        public ConditionValue Q0061 {
            get { if (_q0061 == null) { _q0061 = new ConditionValue(); } return _q0061; }
        }
        protected override ConditionValue getCValueQ0061() { return this.Q0061; }


        public BsTSurveyDataCQ AddOrderBy_Q0061_Asc() { regOBA("Q0061");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0061_Desc() { regOBD("Q0061");return this; }

        protected ConditionValue _q0062;
        public ConditionValue Q0062 {
            get { if (_q0062 == null) { _q0062 = new ConditionValue(); } return _q0062; }
        }
        protected override ConditionValue getCValueQ0062() { return this.Q0062; }


        public BsTSurveyDataCQ AddOrderBy_Q0062_Asc() { regOBA("Q0062");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0062_Desc() { regOBD("Q0062");return this; }

        protected ConditionValue _q0063;
        public ConditionValue Q0063 {
            get { if (_q0063 == null) { _q0063 = new ConditionValue(); } return _q0063; }
        }
        protected override ConditionValue getCValueQ0063() { return this.Q0063; }


        public BsTSurveyDataCQ AddOrderBy_Q0063_Asc() { regOBA("Q0063");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0063_Desc() { regOBD("Q0063");return this; }

        protected ConditionValue _q0064;
        public ConditionValue Q0064 {
            get { if (_q0064 == null) { _q0064 = new ConditionValue(); } return _q0064; }
        }
        protected override ConditionValue getCValueQ0064() { return this.Q0064; }


        public BsTSurveyDataCQ AddOrderBy_Q0064_Asc() { regOBA("Q0064");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0064_Desc() { regOBD("Q0064");return this; }

        protected ConditionValue _q0065;
        public ConditionValue Q0065 {
            get { if (_q0065 == null) { _q0065 = new ConditionValue(); } return _q0065; }
        }
        protected override ConditionValue getCValueQ0065() { return this.Q0065; }


        public BsTSurveyDataCQ AddOrderBy_Q0065_Asc() { regOBA("Q0065");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0065_Desc() { regOBD("Q0065");return this; }

        protected ConditionValue _q0066;
        public ConditionValue Q0066 {
            get { if (_q0066 == null) { _q0066 = new ConditionValue(); } return _q0066; }
        }
        protected override ConditionValue getCValueQ0066() { return this.Q0066; }


        public BsTSurveyDataCQ AddOrderBy_Q0066_Asc() { regOBA("Q0066");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0066_Desc() { regOBD("Q0066");return this; }

        protected ConditionValue _q0067;
        public ConditionValue Q0067 {
            get { if (_q0067 == null) { _q0067 = new ConditionValue(); } return _q0067; }
        }
        protected override ConditionValue getCValueQ0067() { return this.Q0067; }


        public BsTSurveyDataCQ AddOrderBy_Q0067_Asc() { regOBA("Q0067");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0067_Desc() { regOBD("Q0067");return this; }

        protected ConditionValue _q0068;
        public ConditionValue Q0068 {
            get { if (_q0068 == null) { _q0068 = new ConditionValue(); } return _q0068; }
        }
        protected override ConditionValue getCValueQ0068() { return this.Q0068; }


        public BsTSurveyDataCQ AddOrderBy_Q0068_Asc() { regOBA("Q0068");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0068_Desc() { regOBD("Q0068");return this; }

        protected ConditionValue _q0069;
        public ConditionValue Q0069 {
            get { if (_q0069 == null) { _q0069 = new ConditionValue(); } return _q0069; }
        }
        protected override ConditionValue getCValueQ0069() { return this.Q0069; }


        public BsTSurveyDataCQ AddOrderBy_Q0069_Asc() { regOBA("Q0069");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0069_Desc() { regOBD("Q0069");return this; }

        protected ConditionValue _q0070;
        public ConditionValue Q0070 {
            get { if (_q0070 == null) { _q0070 = new ConditionValue(); } return _q0070; }
        }
        protected override ConditionValue getCValueQ0070() { return this.Q0070; }


        public BsTSurveyDataCQ AddOrderBy_Q0070_Asc() { regOBA("Q0070");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0070_Desc() { regOBD("Q0070");return this; }

        protected ConditionValue _q0071;
        public ConditionValue Q0071 {
            get { if (_q0071 == null) { _q0071 = new ConditionValue(); } return _q0071; }
        }
        protected override ConditionValue getCValueQ0071() { return this.Q0071; }


        public BsTSurveyDataCQ AddOrderBy_Q0071_Asc() { regOBA("Q0071");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0071_Desc() { regOBD("Q0071");return this; }

        protected ConditionValue _q0072;
        public ConditionValue Q0072 {
            get { if (_q0072 == null) { _q0072 = new ConditionValue(); } return _q0072; }
        }
        protected override ConditionValue getCValueQ0072() { return this.Q0072; }


        public BsTSurveyDataCQ AddOrderBy_Q0072_Asc() { regOBA("Q0072");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0072_Desc() { regOBD("Q0072");return this; }

        protected ConditionValue _q0073;
        public ConditionValue Q0073 {
            get { if (_q0073 == null) { _q0073 = new ConditionValue(); } return _q0073; }
        }
        protected override ConditionValue getCValueQ0073() { return this.Q0073; }


        public BsTSurveyDataCQ AddOrderBy_Q0073_Asc() { regOBA("Q0073");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0073_Desc() { regOBD("Q0073");return this; }

        protected ConditionValue _q0074;
        public ConditionValue Q0074 {
            get { if (_q0074 == null) { _q0074 = new ConditionValue(); } return _q0074; }
        }
        protected override ConditionValue getCValueQ0074() { return this.Q0074; }


        public BsTSurveyDataCQ AddOrderBy_Q0074_Asc() { regOBA("Q0074");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0074_Desc() { regOBD("Q0074");return this; }

        protected ConditionValue _q0075;
        public ConditionValue Q0075 {
            get { if (_q0075 == null) { _q0075 = new ConditionValue(); } return _q0075; }
        }
        protected override ConditionValue getCValueQ0075() { return this.Q0075; }


        public BsTSurveyDataCQ AddOrderBy_Q0075_Asc() { regOBA("Q0075");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0075_Desc() { regOBD("Q0075");return this; }

        protected ConditionValue _q0076;
        public ConditionValue Q0076 {
            get { if (_q0076 == null) { _q0076 = new ConditionValue(); } return _q0076; }
        }
        protected override ConditionValue getCValueQ0076() { return this.Q0076; }


        public BsTSurveyDataCQ AddOrderBy_Q0076_Asc() { regOBA("Q0076");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0076_Desc() { regOBD("Q0076");return this; }

        protected ConditionValue _q0077;
        public ConditionValue Q0077 {
            get { if (_q0077 == null) { _q0077 = new ConditionValue(); } return _q0077; }
        }
        protected override ConditionValue getCValueQ0077() { return this.Q0077; }


        public BsTSurveyDataCQ AddOrderBy_Q0077_Asc() { regOBA("Q0077");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0077_Desc() { regOBD("Q0077");return this; }

        protected ConditionValue _q0078;
        public ConditionValue Q0078 {
            get { if (_q0078 == null) { _q0078 = new ConditionValue(); } return _q0078; }
        }
        protected override ConditionValue getCValueQ0078() { return this.Q0078; }


        public BsTSurveyDataCQ AddOrderBy_Q0078_Asc() { regOBA("Q0078");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0078_Desc() { regOBD("Q0078");return this; }

        protected ConditionValue _q0079;
        public ConditionValue Q0079 {
            get { if (_q0079 == null) { _q0079 = new ConditionValue(); } return _q0079; }
        }
        protected override ConditionValue getCValueQ0079() { return this.Q0079; }


        public BsTSurveyDataCQ AddOrderBy_Q0079_Asc() { regOBA("Q0079");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0079_Desc() { regOBD("Q0079");return this; }

        protected ConditionValue _q0080;
        public ConditionValue Q0080 {
            get { if (_q0080 == null) { _q0080 = new ConditionValue(); } return _q0080; }
        }
        protected override ConditionValue getCValueQ0080() { return this.Q0080; }


        public BsTSurveyDataCQ AddOrderBy_Q0080_Asc() { regOBA("Q0080");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0080_Desc() { regOBD("Q0080");return this; }

        protected ConditionValue _q0081;
        public ConditionValue Q0081 {
            get { if (_q0081 == null) { _q0081 = new ConditionValue(); } return _q0081; }
        }
        protected override ConditionValue getCValueQ0081() { return this.Q0081; }


        public BsTSurveyDataCQ AddOrderBy_Q0081_Asc() { regOBA("Q0081");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0081_Desc() { regOBD("Q0081");return this; }

        protected ConditionValue _q0082;
        public ConditionValue Q0082 {
            get { if (_q0082 == null) { _q0082 = new ConditionValue(); } return _q0082; }
        }
        protected override ConditionValue getCValueQ0082() { return this.Q0082; }


        public BsTSurveyDataCQ AddOrderBy_Q0082_Asc() { regOBA("Q0082");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0082_Desc() { regOBD("Q0082");return this; }

        protected ConditionValue _q0083;
        public ConditionValue Q0083 {
            get { if (_q0083 == null) { _q0083 = new ConditionValue(); } return _q0083; }
        }
        protected override ConditionValue getCValueQ0083() { return this.Q0083; }


        public BsTSurveyDataCQ AddOrderBy_Q0083_Asc() { regOBA("Q0083");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0083_Desc() { regOBD("Q0083");return this; }

        protected ConditionValue _q0084;
        public ConditionValue Q0084 {
            get { if (_q0084 == null) { _q0084 = new ConditionValue(); } return _q0084; }
        }
        protected override ConditionValue getCValueQ0084() { return this.Q0084; }


        public BsTSurveyDataCQ AddOrderBy_Q0084_Asc() { regOBA("Q0084");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0084_Desc() { regOBD("Q0084");return this; }

        protected ConditionValue _q0085;
        public ConditionValue Q0085 {
            get { if (_q0085 == null) { _q0085 = new ConditionValue(); } return _q0085; }
        }
        protected override ConditionValue getCValueQ0085() { return this.Q0085; }


        public BsTSurveyDataCQ AddOrderBy_Q0085_Asc() { regOBA("Q0085");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0085_Desc() { regOBD("Q0085");return this; }

        protected ConditionValue _q0086;
        public ConditionValue Q0086 {
            get { if (_q0086 == null) { _q0086 = new ConditionValue(); } return _q0086; }
        }
        protected override ConditionValue getCValueQ0086() { return this.Q0086; }


        public BsTSurveyDataCQ AddOrderBy_Q0086_Asc() { regOBA("Q0086");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0086_Desc() { regOBD("Q0086");return this; }

        protected ConditionValue _q0087;
        public ConditionValue Q0087 {
            get { if (_q0087 == null) { _q0087 = new ConditionValue(); } return _q0087; }
        }
        protected override ConditionValue getCValueQ0087() { return this.Q0087; }


        public BsTSurveyDataCQ AddOrderBy_Q0087_Asc() { regOBA("Q0087");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0087_Desc() { regOBD("Q0087");return this; }

        protected ConditionValue _q0088;
        public ConditionValue Q0088 {
            get { if (_q0088 == null) { _q0088 = new ConditionValue(); } return _q0088; }
        }
        protected override ConditionValue getCValueQ0088() { return this.Q0088; }


        public BsTSurveyDataCQ AddOrderBy_Q0088_Asc() { regOBA("Q0088");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0088_Desc() { regOBD("Q0088");return this; }

        protected ConditionValue _q0089;
        public ConditionValue Q0089 {
            get { if (_q0089 == null) { _q0089 = new ConditionValue(); } return _q0089; }
        }
        protected override ConditionValue getCValueQ0089() { return this.Q0089; }


        public BsTSurveyDataCQ AddOrderBy_Q0089_Asc() { regOBA("Q0089");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0089_Desc() { regOBD("Q0089");return this; }

        protected ConditionValue _q0090;
        public ConditionValue Q0090 {
            get { if (_q0090 == null) { _q0090 = new ConditionValue(); } return _q0090; }
        }
        protected override ConditionValue getCValueQ0090() { return this.Q0090; }


        public BsTSurveyDataCQ AddOrderBy_Q0090_Asc() { regOBA("Q0090");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0090_Desc() { regOBD("Q0090");return this; }

        protected ConditionValue _q0091;
        public ConditionValue Q0091 {
            get { if (_q0091 == null) { _q0091 = new ConditionValue(); } return _q0091; }
        }
        protected override ConditionValue getCValueQ0091() { return this.Q0091; }


        public BsTSurveyDataCQ AddOrderBy_Q0091_Asc() { regOBA("Q0091");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0091_Desc() { regOBD("Q0091");return this; }

        protected ConditionValue _q0092;
        public ConditionValue Q0092 {
            get { if (_q0092 == null) { _q0092 = new ConditionValue(); } return _q0092; }
        }
        protected override ConditionValue getCValueQ0092() { return this.Q0092; }


        public BsTSurveyDataCQ AddOrderBy_Q0092_Asc() { regOBA("Q0092");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0092_Desc() { regOBD("Q0092");return this; }

        protected ConditionValue _q0093;
        public ConditionValue Q0093 {
            get { if (_q0093 == null) { _q0093 = new ConditionValue(); } return _q0093; }
        }
        protected override ConditionValue getCValueQ0093() { return this.Q0093; }


        public BsTSurveyDataCQ AddOrderBy_Q0093_Asc() { regOBA("Q0093");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0093_Desc() { regOBD("Q0093");return this; }

        protected ConditionValue _q0094;
        public ConditionValue Q0094 {
            get { if (_q0094 == null) { _q0094 = new ConditionValue(); } return _q0094; }
        }
        protected override ConditionValue getCValueQ0094() { return this.Q0094; }


        public BsTSurveyDataCQ AddOrderBy_Q0094_Asc() { regOBA("Q0094");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0094_Desc() { regOBD("Q0094");return this; }

        protected ConditionValue _q0095;
        public ConditionValue Q0095 {
            get { if (_q0095 == null) { _q0095 = new ConditionValue(); } return _q0095; }
        }
        protected override ConditionValue getCValueQ0095() { return this.Q0095; }


        public BsTSurveyDataCQ AddOrderBy_Q0095_Asc() { regOBA("Q0095");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0095_Desc() { regOBD("Q0095");return this; }

        protected ConditionValue _q0096;
        public ConditionValue Q0096 {
            get { if (_q0096 == null) { _q0096 = new ConditionValue(); } return _q0096; }
        }
        protected override ConditionValue getCValueQ0096() { return this.Q0096; }


        public BsTSurveyDataCQ AddOrderBy_Q0096_Asc() { regOBA("Q0096");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0096_Desc() { regOBD("Q0096");return this; }

        protected ConditionValue _q0097;
        public ConditionValue Q0097 {
            get { if (_q0097 == null) { _q0097 = new ConditionValue(); } return _q0097; }
        }
        protected override ConditionValue getCValueQ0097() { return this.Q0097; }


        public BsTSurveyDataCQ AddOrderBy_Q0097_Asc() { regOBA("Q0097");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0097_Desc() { regOBD("Q0097");return this; }

        protected ConditionValue _q0098;
        public ConditionValue Q0098 {
            get { if (_q0098 == null) { _q0098 = new ConditionValue(); } return _q0098; }
        }
        protected override ConditionValue getCValueQ0098() { return this.Q0098; }


        public BsTSurveyDataCQ AddOrderBy_Q0098_Asc() { regOBA("Q0098");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0098_Desc() { regOBD("Q0098");return this; }

        protected ConditionValue _q0099;
        public ConditionValue Q0099 {
            get { if (_q0099 == null) { _q0099 = new ConditionValue(); } return _q0099; }
        }
        protected override ConditionValue getCValueQ0099() { return this.Q0099; }


        public BsTSurveyDataCQ AddOrderBy_Q0099_Asc() { regOBA("Q0099");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0099_Desc() { regOBD("Q0099");return this; }

        protected ConditionValue _q0100;
        public ConditionValue Q0100 {
            get { if (_q0100 == null) { _q0100 = new ConditionValue(); } return _q0100; }
        }
        protected override ConditionValue getCValueQ0100() { return this.Q0100; }


        public BsTSurveyDataCQ AddOrderBy_Q0100_Asc() { regOBA("Q0100");return this; }
        public BsTSurveyDataCQ AddOrderBy_Q0100_Desc() { regOBD("Q0100");return this; }

        public BsTSurveyDataCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTSurveyDataCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TSurveyDataCQ> _scalarSubQueryMap;
	    public Map<String, TSurveyDataCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TSurveyDataCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TSurveyDataCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TSurveyDataCQ> _myselfInScopeSubQueryMap;
        public Map<String, TSurveyDataCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TSurveyDataCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TSurveyDataCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
