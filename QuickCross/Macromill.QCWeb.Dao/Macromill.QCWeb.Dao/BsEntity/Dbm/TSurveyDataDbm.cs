
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity;

using Macromill.QCWeb.Dao.ExDao;
using Macromill.QCWeb.Dao.CBean;

namespace Macromill.QCWeb.Dao.BsEntity.Dbm {

    public class TSurveyDataDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TSurveyData);

        private static readonly TSurveyDataDbm _instance = new TSurveyDataDbm();
        private TSurveyDataDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TSurveyDataDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_SURVEY_DATA"; } }
        public override String TablePropertyName { get { return "TSurveyData"; } }
        public override String TableSqlName { get { return "T_SURVEY_DATA"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnSampleId;
        protected ColumnInfo _columnMergeCode;
        protected ColumnInfo _columnSortNo;
        protected ColumnInfo _columnDeleteFlag;
        protected ColumnInfo _columnAnswerDate;
        protected ColumnInfo _columnSex;
        protected ColumnInfo _columnAge;
        protected ColumnInfo _columnAgeId;
        protected ColumnInfo _columnPrefecture;
        protected ColumnInfo _columnArea;
        protected ColumnInfo _columnMarried;
        protected ColumnInfo _columnChild;
        protected ColumnInfo _columnHincome;
        protected ColumnInfo _columnPincome;
        protected ColumnInfo _columnJob;
        protected ColumnInfo _columnStudent;
        protected ColumnInfo _columnCell;
        protected ColumnInfo _columnCellName;
        protected ColumnInfo _columnQ0001;
        protected ColumnInfo _columnQ0002;
        protected ColumnInfo _columnQ0003;
        protected ColumnInfo _columnQ0004;
        protected ColumnInfo _columnQ0005;
        protected ColumnInfo _columnQ0006;
        protected ColumnInfo _columnQ0007;
        protected ColumnInfo _columnQ0008;
        protected ColumnInfo _columnQ0009;
        protected ColumnInfo _columnQ0010;
        protected ColumnInfo _columnQ0011;
        protected ColumnInfo _columnQ0012;
        protected ColumnInfo _columnQ0013;
        protected ColumnInfo _columnQ0014;
        protected ColumnInfo _columnQ0015;
        protected ColumnInfo _columnQ0016;
        protected ColumnInfo _columnQ0017;
        protected ColumnInfo _columnQ0018;
        protected ColumnInfo _columnQ0019;
        protected ColumnInfo _columnQ0020;
        protected ColumnInfo _columnQ0021;
        protected ColumnInfo _columnQ0022;
        protected ColumnInfo _columnQ0023;
        protected ColumnInfo _columnQ0024;
        protected ColumnInfo _columnQ0025;
        protected ColumnInfo _columnQ0026;
        protected ColumnInfo _columnQ0027;
        protected ColumnInfo _columnQ0028;
        protected ColumnInfo _columnQ0029;
        protected ColumnInfo _columnQ0030;
        protected ColumnInfo _columnQ0031;
        protected ColumnInfo _columnQ0032;
        protected ColumnInfo _columnQ0033;
        protected ColumnInfo _columnQ0034;
        protected ColumnInfo _columnQ0035;
        protected ColumnInfo _columnQ0036;
        protected ColumnInfo _columnQ0037;
        protected ColumnInfo _columnQ0038;
        protected ColumnInfo _columnQ0039;
        protected ColumnInfo _columnQ0040;
        protected ColumnInfo _columnQ0041;
        protected ColumnInfo _columnQ0042;
        protected ColumnInfo _columnQ0043;
        protected ColumnInfo _columnQ0044;
        protected ColumnInfo _columnQ0045;
        protected ColumnInfo _columnQ0046;
        protected ColumnInfo _columnQ0047;
        protected ColumnInfo _columnQ0048;
        protected ColumnInfo _columnQ0049;
        protected ColumnInfo _columnQ0050;
        protected ColumnInfo _columnQ0051;
        protected ColumnInfo _columnQ0052;
        protected ColumnInfo _columnQ0053;
        protected ColumnInfo _columnQ0054;
        protected ColumnInfo _columnQ0055;
        protected ColumnInfo _columnQ0056;
        protected ColumnInfo _columnQ0057;
        protected ColumnInfo _columnQ0058;
        protected ColumnInfo _columnQ0059;
        protected ColumnInfo _columnQ0060;
        protected ColumnInfo _columnQ0061;
        protected ColumnInfo _columnQ0062;
        protected ColumnInfo _columnQ0063;
        protected ColumnInfo _columnQ0064;
        protected ColumnInfo _columnQ0065;
        protected ColumnInfo _columnQ0066;
        protected ColumnInfo _columnQ0067;
        protected ColumnInfo _columnQ0068;
        protected ColumnInfo _columnQ0069;
        protected ColumnInfo _columnQ0070;
        protected ColumnInfo _columnQ0071;
        protected ColumnInfo _columnQ0072;
        protected ColumnInfo _columnQ0073;
        protected ColumnInfo _columnQ0074;
        protected ColumnInfo _columnQ0075;
        protected ColumnInfo _columnQ0076;
        protected ColumnInfo _columnQ0077;
        protected ColumnInfo _columnQ0078;
        protected ColumnInfo _columnQ0079;
        protected ColumnInfo _columnQ0080;
        protected ColumnInfo _columnQ0081;
        protected ColumnInfo _columnQ0082;
        protected ColumnInfo _columnQ0083;
        protected ColumnInfo _columnQ0084;
        protected ColumnInfo _columnQ0085;
        protected ColumnInfo _columnQ0086;
        protected ColumnInfo _columnQ0087;
        protected ColumnInfo _columnQ0088;
        protected ColumnInfo _columnQ0089;
        protected ColumnInfo _columnQ0090;
        protected ColumnInfo _columnQ0091;
        protected ColumnInfo _columnQ0092;
        protected ColumnInfo _columnQ0093;
        protected ColumnInfo _columnQ0094;
        protected ColumnInfo _columnQ0095;
        protected ColumnInfo _columnQ0096;
        protected ColumnInfo _columnQ0097;
        protected ColumnInfo _columnQ0098;
        protected ColumnInfo _columnQ0099;
        protected ColumnInfo _columnQ0100;

        public ColumnInfo ColumnSampleId { get { return _columnSampleId; } }
        public ColumnInfo ColumnMergeCode { get { return _columnMergeCode; } }
        public ColumnInfo ColumnSortNo { get { return _columnSortNo; } }
        public ColumnInfo ColumnDeleteFlag { get { return _columnDeleteFlag; } }
        public ColumnInfo ColumnAnswerDate { get { return _columnAnswerDate; } }
        public ColumnInfo ColumnSex { get { return _columnSex; } }
        public ColumnInfo ColumnAge { get { return _columnAge; } }
        public ColumnInfo ColumnAgeId { get { return _columnAgeId; } }
        public ColumnInfo ColumnPrefecture { get { return _columnPrefecture; } }
        public ColumnInfo ColumnArea { get { return _columnArea; } }
        public ColumnInfo ColumnMarried { get { return _columnMarried; } }
        public ColumnInfo ColumnChild { get { return _columnChild; } }
        public ColumnInfo ColumnHincome { get { return _columnHincome; } }
        public ColumnInfo ColumnPincome { get { return _columnPincome; } }
        public ColumnInfo ColumnJob { get { return _columnJob; } }
        public ColumnInfo ColumnStudent { get { return _columnStudent; } }
        public ColumnInfo ColumnCell { get { return _columnCell; } }
        public ColumnInfo ColumnCellName { get { return _columnCellName; } }
        public ColumnInfo ColumnQ0001 { get { return _columnQ0001; } }
        public ColumnInfo ColumnQ0002 { get { return _columnQ0002; } }
        public ColumnInfo ColumnQ0003 { get { return _columnQ0003; } }
        public ColumnInfo ColumnQ0004 { get { return _columnQ0004; } }
        public ColumnInfo ColumnQ0005 { get { return _columnQ0005; } }
        public ColumnInfo ColumnQ0006 { get { return _columnQ0006; } }
        public ColumnInfo ColumnQ0007 { get { return _columnQ0007; } }
        public ColumnInfo ColumnQ0008 { get { return _columnQ0008; } }
        public ColumnInfo ColumnQ0009 { get { return _columnQ0009; } }
        public ColumnInfo ColumnQ0010 { get { return _columnQ0010; } }
        public ColumnInfo ColumnQ0011 { get { return _columnQ0011; } }
        public ColumnInfo ColumnQ0012 { get { return _columnQ0012; } }
        public ColumnInfo ColumnQ0013 { get { return _columnQ0013; } }
        public ColumnInfo ColumnQ0014 { get { return _columnQ0014; } }
        public ColumnInfo ColumnQ0015 { get { return _columnQ0015; } }
        public ColumnInfo ColumnQ0016 { get { return _columnQ0016; } }
        public ColumnInfo ColumnQ0017 { get { return _columnQ0017; } }
        public ColumnInfo ColumnQ0018 { get { return _columnQ0018; } }
        public ColumnInfo ColumnQ0019 { get { return _columnQ0019; } }
        public ColumnInfo ColumnQ0020 { get { return _columnQ0020; } }
        public ColumnInfo ColumnQ0021 { get { return _columnQ0021; } }
        public ColumnInfo ColumnQ0022 { get { return _columnQ0022; } }
        public ColumnInfo ColumnQ0023 { get { return _columnQ0023; } }
        public ColumnInfo ColumnQ0024 { get { return _columnQ0024; } }
        public ColumnInfo ColumnQ0025 { get { return _columnQ0025; } }
        public ColumnInfo ColumnQ0026 { get { return _columnQ0026; } }
        public ColumnInfo ColumnQ0027 { get { return _columnQ0027; } }
        public ColumnInfo ColumnQ0028 { get { return _columnQ0028; } }
        public ColumnInfo ColumnQ0029 { get { return _columnQ0029; } }
        public ColumnInfo ColumnQ0030 { get { return _columnQ0030; } }
        public ColumnInfo ColumnQ0031 { get { return _columnQ0031; } }
        public ColumnInfo ColumnQ0032 { get { return _columnQ0032; } }
        public ColumnInfo ColumnQ0033 { get { return _columnQ0033; } }
        public ColumnInfo ColumnQ0034 { get { return _columnQ0034; } }
        public ColumnInfo ColumnQ0035 { get { return _columnQ0035; } }
        public ColumnInfo ColumnQ0036 { get { return _columnQ0036; } }
        public ColumnInfo ColumnQ0037 { get { return _columnQ0037; } }
        public ColumnInfo ColumnQ0038 { get { return _columnQ0038; } }
        public ColumnInfo ColumnQ0039 { get { return _columnQ0039; } }
        public ColumnInfo ColumnQ0040 { get { return _columnQ0040; } }
        public ColumnInfo ColumnQ0041 { get { return _columnQ0041; } }
        public ColumnInfo ColumnQ0042 { get { return _columnQ0042; } }
        public ColumnInfo ColumnQ0043 { get { return _columnQ0043; } }
        public ColumnInfo ColumnQ0044 { get { return _columnQ0044; } }
        public ColumnInfo ColumnQ0045 { get { return _columnQ0045; } }
        public ColumnInfo ColumnQ0046 { get { return _columnQ0046; } }
        public ColumnInfo ColumnQ0047 { get { return _columnQ0047; } }
        public ColumnInfo ColumnQ0048 { get { return _columnQ0048; } }
        public ColumnInfo ColumnQ0049 { get { return _columnQ0049; } }
        public ColumnInfo ColumnQ0050 { get { return _columnQ0050; } }
        public ColumnInfo ColumnQ0051 { get { return _columnQ0051; } }
        public ColumnInfo ColumnQ0052 { get { return _columnQ0052; } }
        public ColumnInfo ColumnQ0053 { get { return _columnQ0053; } }
        public ColumnInfo ColumnQ0054 { get { return _columnQ0054; } }
        public ColumnInfo ColumnQ0055 { get { return _columnQ0055; } }
        public ColumnInfo ColumnQ0056 { get { return _columnQ0056; } }
        public ColumnInfo ColumnQ0057 { get { return _columnQ0057; } }
        public ColumnInfo ColumnQ0058 { get { return _columnQ0058; } }
        public ColumnInfo ColumnQ0059 { get { return _columnQ0059; } }
        public ColumnInfo ColumnQ0060 { get { return _columnQ0060; } }
        public ColumnInfo ColumnQ0061 { get { return _columnQ0061; } }
        public ColumnInfo ColumnQ0062 { get { return _columnQ0062; } }
        public ColumnInfo ColumnQ0063 { get { return _columnQ0063; } }
        public ColumnInfo ColumnQ0064 { get { return _columnQ0064; } }
        public ColumnInfo ColumnQ0065 { get { return _columnQ0065; } }
        public ColumnInfo ColumnQ0066 { get { return _columnQ0066; } }
        public ColumnInfo ColumnQ0067 { get { return _columnQ0067; } }
        public ColumnInfo ColumnQ0068 { get { return _columnQ0068; } }
        public ColumnInfo ColumnQ0069 { get { return _columnQ0069; } }
        public ColumnInfo ColumnQ0070 { get { return _columnQ0070; } }
        public ColumnInfo ColumnQ0071 { get { return _columnQ0071; } }
        public ColumnInfo ColumnQ0072 { get { return _columnQ0072; } }
        public ColumnInfo ColumnQ0073 { get { return _columnQ0073; } }
        public ColumnInfo ColumnQ0074 { get { return _columnQ0074; } }
        public ColumnInfo ColumnQ0075 { get { return _columnQ0075; } }
        public ColumnInfo ColumnQ0076 { get { return _columnQ0076; } }
        public ColumnInfo ColumnQ0077 { get { return _columnQ0077; } }
        public ColumnInfo ColumnQ0078 { get { return _columnQ0078; } }
        public ColumnInfo ColumnQ0079 { get { return _columnQ0079; } }
        public ColumnInfo ColumnQ0080 { get { return _columnQ0080; } }
        public ColumnInfo ColumnQ0081 { get { return _columnQ0081; } }
        public ColumnInfo ColumnQ0082 { get { return _columnQ0082; } }
        public ColumnInfo ColumnQ0083 { get { return _columnQ0083; } }
        public ColumnInfo ColumnQ0084 { get { return _columnQ0084; } }
        public ColumnInfo ColumnQ0085 { get { return _columnQ0085; } }
        public ColumnInfo ColumnQ0086 { get { return _columnQ0086; } }
        public ColumnInfo ColumnQ0087 { get { return _columnQ0087; } }
        public ColumnInfo ColumnQ0088 { get { return _columnQ0088; } }
        public ColumnInfo ColumnQ0089 { get { return _columnQ0089; } }
        public ColumnInfo ColumnQ0090 { get { return _columnQ0090; } }
        public ColumnInfo ColumnQ0091 { get { return _columnQ0091; } }
        public ColumnInfo ColumnQ0092 { get { return _columnQ0092; } }
        public ColumnInfo ColumnQ0093 { get { return _columnQ0093; } }
        public ColumnInfo ColumnQ0094 { get { return _columnQ0094; } }
        public ColumnInfo ColumnQ0095 { get { return _columnQ0095; } }
        public ColumnInfo ColumnQ0096 { get { return _columnQ0096; } }
        public ColumnInfo ColumnQ0097 { get { return _columnQ0097; } }
        public ColumnInfo ColumnQ0098 { get { return _columnQ0098; } }
        public ColumnInfo ColumnQ0099 { get { return _columnQ0099; } }
        public ColumnInfo ColumnQ0100 { get { return _columnQ0100; } }

        protected void InitializeColumnInfo() {
            _columnSampleId = cci("SAMPLE_ID", "SAMPLE_ID", null, null, true, "SampleId", typeof(String), true, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMergeCode = cci("MERGE_CODE", "MERGE_CODE", null, null, false, "MergeCode", typeof(String), false, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnSortNo = cci("SORT_NO", "SORT_NO", null, null, true, "SortNo", typeof(long?), false, "NUMBER", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnDeleteFlag = cci("DELETE_FLAG", "DELETE_FLAG", null, null, false, "DeleteFlag", typeof(int?), false, "NUMBER", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAnswerDate = cci("ANSWER_DATE", "ANSWER_DATE", null, null, false, "AnswerDate", typeof(DateTime?), false, "TIMESTAMP(6)", 11, 6, false, OptimisticLockType.NONE, null, null, null);
            _columnSex = cci("SEX", "SEX", null, null, false, "Sex", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAge = cci("AGE", "AGE", null, null, false, "Age", typeof(int?), false, "NUMBER", 3, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnAgeId = cci("AGE_ID", "AGE_ID", null, null, false, "AgeId", typeof(String), false, "CHAR", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPrefecture = cci("PREFECTURE", "PREFECTURE", null, null, false, "Prefecture", typeof(String), false, "CHAR", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnArea = cci("AREA", "AREA", null, null, false, "Area", typeof(String), false, "CHAR", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnMarried = cci("MARRIED", "MARRIED", null, null, false, "Married", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnChild = cci("CHILD", "CHILD", null, null, false, "Child", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnHincome = cci("HINCOME", "HINCOME", null, null, false, "Hincome", typeof(String), false, "CHAR", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnPincome = cci("PINCOME", "PINCOME", null, null, false, "Pincome", typeof(String), false, "CHAR", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnJob = cci("JOB", "JOB", null, null, false, "Job", typeof(String), false, "CHAR", 2, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnStudent = cci("STUDENT", "STUDENT", null, null, false, "Student", typeof(String), false, "CHAR", 1, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCell = cci("CELL", "CELL", null, null, false, "Cell", typeof(String), false, "VARCHAR2", 20, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnCellName = cci("CELL_NAME", "CELL_NAME", null, null, false, "CellName", typeof(String), false, "NVARCHAR2", 200, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0001 = cci("Q0001", "Q0001", null, null, false, "Q0001", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0002 = cci("Q0002", "Q0002", null, null, false, "Q0002", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0003 = cci("Q0003", "Q0003", null, null, false, "Q0003", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0004 = cci("Q0004", "Q0004", null, null, false, "Q0004", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0005 = cci("Q0005", "Q0005", null, null, false, "Q0005", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0006 = cci("Q0006", "Q0006", null, null, false, "Q0006", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0007 = cci("Q0007", "Q0007", null, null, false, "Q0007", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0008 = cci("Q0008", "Q0008", null, null, false, "Q0008", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0009 = cci("Q0009", "Q0009", null, null, false, "Q0009", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0010 = cci("Q0010", "Q0010", null, null, false, "Q0010", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0011 = cci("Q0011", "Q0011", null, null, false, "Q0011", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0012 = cci("Q0012", "Q0012", null, null, false, "Q0012", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0013 = cci("Q0013", "Q0013", null, null, false, "Q0013", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0014 = cci("Q0014", "Q0014", null, null, false, "Q0014", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0015 = cci("Q0015", "Q0015", null, null, false, "Q0015", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0016 = cci("Q0016", "Q0016", null, null, false, "Q0016", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0017 = cci("Q0017", "Q0017", null, null, false, "Q0017", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0018 = cci("Q0018", "Q0018", null, null, false, "Q0018", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0019 = cci("Q0019", "Q0019", null, null, false, "Q0019", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0020 = cci("Q0020", "Q0020", null, null, false, "Q0020", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0021 = cci("Q0021", "Q0021", null, null, false, "Q0021", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0022 = cci("Q0022", "Q0022", null, null, false, "Q0022", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0023 = cci("Q0023", "Q0023", null, null, false, "Q0023", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0024 = cci("Q0024", "Q0024", null, null, false, "Q0024", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0025 = cci("Q0025", "Q0025", null, null, false, "Q0025", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0026 = cci("Q0026", "Q0026", null, null, false, "Q0026", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0027 = cci("Q0027", "Q0027", null, null, false, "Q0027", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0028 = cci("Q0028", "Q0028", null, null, false, "Q0028", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0029 = cci("Q0029", "Q0029", null, null, false, "Q0029", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0030 = cci("Q0030", "Q0030", null, null, false, "Q0030", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0031 = cci("Q0031", "Q0031", null, null, false, "Q0031", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0032 = cci("Q0032", "Q0032", null, null, false, "Q0032", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0033 = cci("Q0033", "Q0033", null, null, false, "Q0033", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0034 = cci("Q0034", "Q0034", null, null, false, "Q0034", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0035 = cci("Q0035", "Q0035", null, null, false, "Q0035", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0036 = cci("Q0036", "Q0036", null, null, false, "Q0036", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0037 = cci("Q0037", "Q0037", null, null, false, "Q0037", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0038 = cci("Q0038", "Q0038", null, null, false, "Q0038", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0039 = cci("Q0039", "Q0039", null, null, false, "Q0039", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0040 = cci("Q0040", "Q0040", null, null, false, "Q0040", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0041 = cci("Q0041", "Q0041", null, null, false, "Q0041", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0042 = cci("Q0042", "Q0042", null, null, false, "Q0042", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0043 = cci("Q0043", "Q0043", null, null, false, "Q0043", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0044 = cci("Q0044", "Q0044", null, null, false, "Q0044", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0045 = cci("Q0045", "Q0045", null, null, false, "Q0045", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0046 = cci("Q0046", "Q0046", null, null, false, "Q0046", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0047 = cci("Q0047", "Q0047", null, null, false, "Q0047", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0048 = cci("Q0048", "Q0048", null, null, false, "Q0048", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0049 = cci("Q0049", "Q0049", null, null, false, "Q0049", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0050 = cci("Q0050", "Q0050", null, null, false, "Q0050", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0051 = cci("Q0051", "Q0051", null, null, false, "Q0051", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0052 = cci("Q0052", "Q0052", null, null, false, "Q0052", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0053 = cci("Q0053", "Q0053", null, null, false, "Q0053", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0054 = cci("Q0054", "Q0054", null, null, false, "Q0054", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0055 = cci("Q0055", "Q0055", null, null, false, "Q0055", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0056 = cci("Q0056", "Q0056", null, null, false, "Q0056", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0057 = cci("Q0057", "Q0057", null, null, false, "Q0057", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0058 = cci("Q0058", "Q0058", null, null, false, "Q0058", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0059 = cci("Q0059", "Q0059", null, null, false, "Q0059", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0060 = cci("Q0060", "Q0060", null, null, false, "Q0060", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0061 = cci("Q0061", "Q0061", null, null, false, "Q0061", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0062 = cci("Q0062", "Q0062", null, null, false, "Q0062", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0063 = cci("Q0063", "Q0063", null, null, false, "Q0063", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0064 = cci("Q0064", "Q0064", null, null, false, "Q0064", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0065 = cci("Q0065", "Q0065", null, null, false, "Q0065", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0066 = cci("Q0066", "Q0066", null, null, false, "Q0066", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0067 = cci("Q0067", "Q0067", null, null, false, "Q0067", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0068 = cci("Q0068", "Q0068", null, null, false, "Q0068", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0069 = cci("Q0069", "Q0069", null, null, false, "Q0069", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0070 = cci("Q0070", "Q0070", null, null, false, "Q0070", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0071 = cci("Q0071", "Q0071", null, null, false, "Q0071", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0072 = cci("Q0072", "Q0072", null, null, false, "Q0072", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0073 = cci("Q0073", "Q0073", null, null, false, "Q0073", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0074 = cci("Q0074", "Q0074", null, null, false, "Q0074", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0075 = cci("Q0075", "Q0075", null, null, false, "Q0075", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0076 = cci("Q0076", "Q0076", null, null, false, "Q0076", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0077 = cci("Q0077", "Q0077", null, null, false, "Q0077", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0078 = cci("Q0078", "Q0078", null, null, false, "Q0078", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0079 = cci("Q0079", "Q0079", null, null, false, "Q0079", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0080 = cci("Q0080", "Q0080", null, null, false, "Q0080", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0081 = cci("Q0081", "Q0081", null, null, false, "Q0081", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0082 = cci("Q0082", "Q0082", null, null, false, "Q0082", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0083 = cci("Q0083", "Q0083", null, null, false, "Q0083", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0084 = cci("Q0084", "Q0084", null, null, false, "Q0084", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0085 = cci("Q0085", "Q0085", null, null, false, "Q0085", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0086 = cci("Q0086", "Q0086", null, null, false, "Q0086", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0087 = cci("Q0087", "Q0087", null, null, false, "Q0087", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0088 = cci("Q0088", "Q0088", null, null, false, "Q0088", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0089 = cci("Q0089", "Q0089", null, null, false, "Q0089", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0090 = cci("Q0090", "Q0090", null, null, false, "Q0090", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0091 = cci("Q0091", "Q0091", null, null, false, "Q0091", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0092 = cci("Q0092", "Q0092", null, null, false, "Q0092", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0093 = cci("Q0093", "Q0093", null, null, false, "Q0093", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0094 = cci("Q0094", "Q0094", null, null, false, "Q0094", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0095 = cci("Q0095", "Q0095", null, null, false, "Q0095", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0096 = cci("Q0096", "Q0096", null, null, false, "Q0096", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0097 = cci("Q0097", "Q0097", null, null, false, "Q0097", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0098 = cci("Q0098", "Q0098", null, null, false, "Q0098", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0099 = cci("Q0099", "Q0099", null, null, false, "Q0099", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnQ0100 = cci("Q0100", "Q0100", null, null, false, "Q0100", typeof(String), false, "VARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnSampleId);
            _columnInfoList.add(ColumnMergeCode);
            _columnInfoList.add(ColumnSortNo);
            _columnInfoList.add(ColumnDeleteFlag);
            _columnInfoList.add(ColumnAnswerDate);
            _columnInfoList.add(ColumnSex);
            _columnInfoList.add(ColumnAge);
            _columnInfoList.add(ColumnAgeId);
            _columnInfoList.add(ColumnPrefecture);
            _columnInfoList.add(ColumnArea);
            _columnInfoList.add(ColumnMarried);
            _columnInfoList.add(ColumnChild);
            _columnInfoList.add(ColumnHincome);
            _columnInfoList.add(ColumnPincome);
            _columnInfoList.add(ColumnJob);
            _columnInfoList.add(ColumnStudent);
            _columnInfoList.add(ColumnCell);
            _columnInfoList.add(ColumnCellName);
            _columnInfoList.add(ColumnQ0001);
            _columnInfoList.add(ColumnQ0002);
            _columnInfoList.add(ColumnQ0003);
            _columnInfoList.add(ColumnQ0004);
            _columnInfoList.add(ColumnQ0005);
            _columnInfoList.add(ColumnQ0006);
            _columnInfoList.add(ColumnQ0007);
            _columnInfoList.add(ColumnQ0008);
            _columnInfoList.add(ColumnQ0009);
            _columnInfoList.add(ColumnQ0010);
            _columnInfoList.add(ColumnQ0011);
            _columnInfoList.add(ColumnQ0012);
            _columnInfoList.add(ColumnQ0013);
            _columnInfoList.add(ColumnQ0014);
            _columnInfoList.add(ColumnQ0015);
            _columnInfoList.add(ColumnQ0016);
            _columnInfoList.add(ColumnQ0017);
            _columnInfoList.add(ColumnQ0018);
            _columnInfoList.add(ColumnQ0019);
            _columnInfoList.add(ColumnQ0020);
            _columnInfoList.add(ColumnQ0021);
            _columnInfoList.add(ColumnQ0022);
            _columnInfoList.add(ColumnQ0023);
            _columnInfoList.add(ColumnQ0024);
            _columnInfoList.add(ColumnQ0025);
            _columnInfoList.add(ColumnQ0026);
            _columnInfoList.add(ColumnQ0027);
            _columnInfoList.add(ColumnQ0028);
            _columnInfoList.add(ColumnQ0029);
            _columnInfoList.add(ColumnQ0030);
            _columnInfoList.add(ColumnQ0031);
            _columnInfoList.add(ColumnQ0032);
            _columnInfoList.add(ColumnQ0033);
            _columnInfoList.add(ColumnQ0034);
            _columnInfoList.add(ColumnQ0035);
            _columnInfoList.add(ColumnQ0036);
            _columnInfoList.add(ColumnQ0037);
            _columnInfoList.add(ColumnQ0038);
            _columnInfoList.add(ColumnQ0039);
            _columnInfoList.add(ColumnQ0040);
            _columnInfoList.add(ColumnQ0041);
            _columnInfoList.add(ColumnQ0042);
            _columnInfoList.add(ColumnQ0043);
            _columnInfoList.add(ColumnQ0044);
            _columnInfoList.add(ColumnQ0045);
            _columnInfoList.add(ColumnQ0046);
            _columnInfoList.add(ColumnQ0047);
            _columnInfoList.add(ColumnQ0048);
            _columnInfoList.add(ColumnQ0049);
            _columnInfoList.add(ColumnQ0050);
            _columnInfoList.add(ColumnQ0051);
            _columnInfoList.add(ColumnQ0052);
            _columnInfoList.add(ColumnQ0053);
            _columnInfoList.add(ColumnQ0054);
            _columnInfoList.add(ColumnQ0055);
            _columnInfoList.add(ColumnQ0056);
            _columnInfoList.add(ColumnQ0057);
            _columnInfoList.add(ColumnQ0058);
            _columnInfoList.add(ColumnQ0059);
            _columnInfoList.add(ColumnQ0060);
            _columnInfoList.add(ColumnQ0061);
            _columnInfoList.add(ColumnQ0062);
            _columnInfoList.add(ColumnQ0063);
            _columnInfoList.add(ColumnQ0064);
            _columnInfoList.add(ColumnQ0065);
            _columnInfoList.add(ColumnQ0066);
            _columnInfoList.add(ColumnQ0067);
            _columnInfoList.add(ColumnQ0068);
            _columnInfoList.add(ColumnQ0069);
            _columnInfoList.add(ColumnQ0070);
            _columnInfoList.add(ColumnQ0071);
            _columnInfoList.add(ColumnQ0072);
            _columnInfoList.add(ColumnQ0073);
            _columnInfoList.add(ColumnQ0074);
            _columnInfoList.add(ColumnQ0075);
            _columnInfoList.add(ColumnQ0076);
            _columnInfoList.add(ColumnQ0077);
            _columnInfoList.add(ColumnQ0078);
            _columnInfoList.add(ColumnQ0079);
            _columnInfoList.add(ColumnQ0080);
            _columnInfoList.add(ColumnQ0081);
            _columnInfoList.add(ColumnQ0082);
            _columnInfoList.add(ColumnQ0083);
            _columnInfoList.add(ColumnQ0084);
            _columnInfoList.add(ColumnQ0085);
            _columnInfoList.add(ColumnQ0086);
            _columnInfoList.add(ColumnQ0087);
            _columnInfoList.add(ColumnQ0088);
            _columnInfoList.add(ColumnQ0089);
            _columnInfoList.add(ColumnQ0090);
            _columnInfoList.add(ColumnQ0091);
            _columnInfoList.add(ColumnQ0092);
            _columnInfoList.add(ColumnQ0093);
            _columnInfoList.add(ColumnQ0094);
            _columnInfoList.add(ColumnQ0095);
            _columnInfoList.add(ColumnQ0096);
            _columnInfoList.add(ColumnQ0097);
            _columnInfoList.add(ColumnQ0098);
            _columnInfoList.add(ColumnQ0099);
            _columnInfoList.add(ColumnQ0100);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnSampleId);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return false; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasCommonColumn { get { return false; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_SURVEY_DATA";
        public static readonly String TABLE_PROPERTY_NAME = "TSurveyData";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SAMPLE_ID = "SAMPLE_ID";
        public static readonly String DB_NAME_MERGE_CODE = "MERGE_CODE";
        public static readonly String DB_NAME_SORT_NO = "SORT_NO";
        public static readonly String DB_NAME_DELETE_FLAG = "DELETE_FLAG";
        public static readonly String DB_NAME_ANSWER_DATE = "ANSWER_DATE";
        public static readonly String DB_NAME_SEX = "SEX";
        public static readonly String DB_NAME_AGE = "AGE";
        public static readonly String DB_NAME_AGE_ID = "AGE_ID";
        public static readonly String DB_NAME_PREFECTURE = "PREFECTURE";
        public static readonly String DB_NAME_AREA = "AREA";
        public static readonly String DB_NAME_MARRIED = "MARRIED";
        public static readonly String DB_NAME_CHILD = "CHILD";
        public static readonly String DB_NAME_HINCOME = "HINCOME";
        public static readonly String DB_NAME_PINCOME = "PINCOME";
        public static readonly String DB_NAME_JOB = "JOB";
        public static readonly String DB_NAME_STUDENT = "STUDENT";
        public static readonly String DB_NAME_CELL = "CELL";
        public static readonly String DB_NAME_CELL_NAME = "CELL_NAME";
        public static readonly String DB_NAME_Q0001 = "Q0001";
        public static readonly String DB_NAME_Q0002 = "Q0002";
        public static readonly String DB_NAME_Q0003 = "Q0003";
        public static readonly String DB_NAME_Q0004 = "Q0004";
        public static readonly String DB_NAME_Q0005 = "Q0005";
        public static readonly String DB_NAME_Q0006 = "Q0006";
        public static readonly String DB_NAME_Q0007 = "Q0007";
        public static readonly String DB_NAME_Q0008 = "Q0008";
        public static readonly String DB_NAME_Q0009 = "Q0009";
        public static readonly String DB_NAME_Q0010 = "Q0010";
        public static readonly String DB_NAME_Q0011 = "Q0011";
        public static readonly String DB_NAME_Q0012 = "Q0012";
        public static readonly String DB_NAME_Q0013 = "Q0013";
        public static readonly String DB_NAME_Q0014 = "Q0014";
        public static readonly String DB_NAME_Q0015 = "Q0015";
        public static readonly String DB_NAME_Q0016 = "Q0016";
        public static readonly String DB_NAME_Q0017 = "Q0017";
        public static readonly String DB_NAME_Q0018 = "Q0018";
        public static readonly String DB_NAME_Q0019 = "Q0019";
        public static readonly String DB_NAME_Q0020 = "Q0020";
        public static readonly String DB_NAME_Q0021 = "Q0021";
        public static readonly String DB_NAME_Q0022 = "Q0022";
        public static readonly String DB_NAME_Q0023 = "Q0023";
        public static readonly String DB_NAME_Q0024 = "Q0024";
        public static readonly String DB_NAME_Q0025 = "Q0025";
        public static readonly String DB_NAME_Q0026 = "Q0026";
        public static readonly String DB_NAME_Q0027 = "Q0027";
        public static readonly String DB_NAME_Q0028 = "Q0028";
        public static readonly String DB_NAME_Q0029 = "Q0029";
        public static readonly String DB_NAME_Q0030 = "Q0030";
        public static readonly String DB_NAME_Q0031 = "Q0031";
        public static readonly String DB_NAME_Q0032 = "Q0032";
        public static readonly String DB_NAME_Q0033 = "Q0033";
        public static readonly String DB_NAME_Q0034 = "Q0034";
        public static readonly String DB_NAME_Q0035 = "Q0035";
        public static readonly String DB_NAME_Q0036 = "Q0036";
        public static readonly String DB_NAME_Q0037 = "Q0037";
        public static readonly String DB_NAME_Q0038 = "Q0038";
        public static readonly String DB_NAME_Q0039 = "Q0039";
        public static readonly String DB_NAME_Q0040 = "Q0040";
        public static readonly String DB_NAME_Q0041 = "Q0041";
        public static readonly String DB_NAME_Q0042 = "Q0042";
        public static readonly String DB_NAME_Q0043 = "Q0043";
        public static readonly String DB_NAME_Q0044 = "Q0044";
        public static readonly String DB_NAME_Q0045 = "Q0045";
        public static readonly String DB_NAME_Q0046 = "Q0046";
        public static readonly String DB_NAME_Q0047 = "Q0047";
        public static readonly String DB_NAME_Q0048 = "Q0048";
        public static readonly String DB_NAME_Q0049 = "Q0049";
        public static readonly String DB_NAME_Q0050 = "Q0050";
        public static readonly String DB_NAME_Q0051 = "Q0051";
        public static readonly String DB_NAME_Q0052 = "Q0052";
        public static readonly String DB_NAME_Q0053 = "Q0053";
        public static readonly String DB_NAME_Q0054 = "Q0054";
        public static readonly String DB_NAME_Q0055 = "Q0055";
        public static readonly String DB_NAME_Q0056 = "Q0056";
        public static readonly String DB_NAME_Q0057 = "Q0057";
        public static readonly String DB_NAME_Q0058 = "Q0058";
        public static readonly String DB_NAME_Q0059 = "Q0059";
        public static readonly String DB_NAME_Q0060 = "Q0060";
        public static readonly String DB_NAME_Q0061 = "Q0061";
        public static readonly String DB_NAME_Q0062 = "Q0062";
        public static readonly String DB_NAME_Q0063 = "Q0063";
        public static readonly String DB_NAME_Q0064 = "Q0064";
        public static readonly String DB_NAME_Q0065 = "Q0065";
        public static readonly String DB_NAME_Q0066 = "Q0066";
        public static readonly String DB_NAME_Q0067 = "Q0067";
        public static readonly String DB_NAME_Q0068 = "Q0068";
        public static readonly String DB_NAME_Q0069 = "Q0069";
        public static readonly String DB_NAME_Q0070 = "Q0070";
        public static readonly String DB_NAME_Q0071 = "Q0071";
        public static readonly String DB_NAME_Q0072 = "Q0072";
        public static readonly String DB_NAME_Q0073 = "Q0073";
        public static readonly String DB_NAME_Q0074 = "Q0074";
        public static readonly String DB_NAME_Q0075 = "Q0075";
        public static readonly String DB_NAME_Q0076 = "Q0076";
        public static readonly String DB_NAME_Q0077 = "Q0077";
        public static readonly String DB_NAME_Q0078 = "Q0078";
        public static readonly String DB_NAME_Q0079 = "Q0079";
        public static readonly String DB_NAME_Q0080 = "Q0080";
        public static readonly String DB_NAME_Q0081 = "Q0081";
        public static readonly String DB_NAME_Q0082 = "Q0082";
        public static readonly String DB_NAME_Q0083 = "Q0083";
        public static readonly String DB_NAME_Q0084 = "Q0084";
        public static readonly String DB_NAME_Q0085 = "Q0085";
        public static readonly String DB_NAME_Q0086 = "Q0086";
        public static readonly String DB_NAME_Q0087 = "Q0087";
        public static readonly String DB_NAME_Q0088 = "Q0088";
        public static readonly String DB_NAME_Q0089 = "Q0089";
        public static readonly String DB_NAME_Q0090 = "Q0090";
        public static readonly String DB_NAME_Q0091 = "Q0091";
        public static readonly String DB_NAME_Q0092 = "Q0092";
        public static readonly String DB_NAME_Q0093 = "Q0093";
        public static readonly String DB_NAME_Q0094 = "Q0094";
        public static readonly String DB_NAME_Q0095 = "Q0095";
        public static readonly String DB_NAME_Q0096 = "Q0096";
        public static readonly String DB_NAME_Q0097 = "Q0097";
        public static readonly String DB_NAME_Q0098 = "Q0098";
        public static readonly String DB_NAME_Q0099 = "Q0099";
        public static readonly String DB_NAME_Q0100 = "Q0100";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SAMPLE_ID = "SampleId";
        public static readonly String PROPERTY_NAME_MERGE_CODE = "MergeCode";
        public static readonly String PROPERTY_NAME_SORT_NO = "SortNo";
        public static readonly String PROPERTY_NAME_DELETE_FLAG = "DeleteFlag";
        public static readonly String PROPERTY_NAME_ANSWER_DATE = "AnswerDate";
        public static readonly String PROPERTY_NAME_SEX = "Sex";
        public static readonly String PROPERTY_NAME_AGE = "Age";
        public static readonly String PROPERTY_NAME_AGE_ID = "AgeId";
        public static readonly String PROPERTY_NAME_PREFECTURE = "Prefecture";
        public static readonly String PROPERTY_NAME_AREA = "Area";
        public static readonly String PROPERTY_NAME_MARRIED = "Married";
        public static readonly String PROPERTY_NAME_CHILD = "Child";
        public static readonly String PROPERTY_NAME_HINCOME = "Hincome";
        public static readonly String PROPERTY_NAME_PINCOME = "Pincome";
        public static readonly String PROPERTY_NAME_JOB = "Job";
        public static readonly String PROPERTY_NAME_STUDENT = "Student";
        public static readonly String PROPERTY_NAME_CELL = "Cell";
        public static readonly String PROPERTY_NAME_CELL_NAME = "CellName";
        public static readonly String PROPERTY_NAME_Q0001 = "Q0001";
        public static readonly String PROPERTY_NAME_Q0002 = "Q0002";
        public static readonly String PROPERTY_NAME_Q0003 = "Q0003";
        public static readonly String PROPERTY_NAME_Q0004 = "Q0004";
        public static readonly String PROPERTY_NAME_Q0005 = "Q0005";
        public static readonly String PROPERTY_NAME_Q0006 = "Q0006";
        public static readonly String PROPERTY_NAME_Q0007 = "Q0007";
        public static readonly String PROPERTY_NAME_Q0008 = "Q0008";
        public static readonly String PROPERTY_NAME_Q0009 = "Q0009";
        public static readonly String PROPERTY_NAME_Q0010 = "Q0010";
        public static readonly String PROPERTY_NAME_Q0011 = "Q0011";
        public static readonly String PROPERTY_NAME_Q0012 = "Q0012";
        public static readonly String PROPERTY_NAME_Q0013 = "Q0013";
        public static readonly String PROPERTY_NAME_Q0014 = "Q0014";
        public static readonly String PROPERTY_NAME_Q0015 = "Q0015";
        public static readonly String PROPERTY_NAME_Q0016 = "Q0016";
        public static readonly String PROPERTY_NAME_Q0017 = "Q0017";
        public static readonly String PROPERTY_NAME_Q0018 = "Q0018";
        public static readonly String PROPERTY_NAME_Q0019 = "Q0019";
        public static readonly String PROPERTY_NAME_Q0020 = "Q0020";
        public static readonly String PROPERTY_NAME_Q0021 = "Q0021";
        public static readonly String PROPERTY_NAME_Q0022 = "Q0022";
        public static readonly String PROPERTY_NAME_Q0023 = "Q0023";
        public static readonly String PROPERTY_NAME_Q0024 = "Q0024";
        public static readonly String PROPERTY_NAME_Q0025 = "Q0025";
        public static readonly String PROPERTY_NAME_Q0026 = "Q0026";
        public static readonly String PROPERTY_NAME_Q0027 = "Q0027";
        public static readonly String PROPERTY_NAME_Q0028 = "Q0028";
        public static readonly String PROPERTY_NAME_Q0029 = "Q0029";
        public static readonly String PROPERTY_NAME_Q0030 = "Q0030";
        public static readonly String PROPERTY_NAME_Q0031 = "Q0031";
        public static readonly String PROPERTY_NAME_Q0032 = "Q0032";
        public static readonly String PROPERTY_NAME_Q0033 = "Q0033";
        public static readonly String PROPERTY_NAME_Q0034 = "Q0034";
        public static readonly String PROPERTY_NAME_Q0035 = "Q0035";
        public static readonly String PROPERTY_NAME_Q0036 = "Q0036";
        public static readonly String PROPERTY_NAME_Q0037 = "Q0037";
        public static readonly String PROPERTY_NAME_Q0038 = "Q0038";
        public static readonly String PROPERTY_NAME_Q0039 = "Q0039";
        public static readonly String PROPERTY_NAME_Q0040 = "Q0040";
        public static readonly String PROPERTY_NAME_Q0041 = "Q0041";
        public static readonly String PROPERTY_NAME_Q0042 = "Q0042";
        public static readonly String PROPERTY_NAME_Q0043 = "Q0043";
        public static readonly String PROPERTY_NAME_Q0044 = "Q0044";
        public static readonly String PROPERTY_NAME_Q0045 = "Q0045";
        public static readonly String PROPERTY_NAME_Q0046 = "Q0046";
        public static readonly String PROPERTY_NAME_Q0047 = "Q0047";
        public static readonly String PROPERTY_NAME_Q0048 = "Q0048";
        public static readonly String PROPERTY_NAME_Q0049 = "Q0049";
        public static readonly String PROPERTY_NAME_Q0050 = "Q0050";
        public static readonly String PROPERTY_NAME_Q0051 = "Q0051";
        public static readonly String PROPERTY_NAME_Q0052 = "Q0052";
        public static readonly String PROPERTY_NAME_Q0053 = "Q0053";
        public static readonly String PROPERTY_NAME_Q0054 = "Q0054";
        public static readonly String PROPERTY_NAME_Q0055 = "Q0055";
        public static readonly String PROPERTY_NAME_Q0056 = "Q0056";
        public static readonly String PROPERTY_NAME_Q0057 = "Q0057";
        public static readonly String PROPERTY_NAME_Q0058 = "Q0058";
        public static readonly String PROPERTY_NAME_Q0059 = "Q0059";
        public static readonly String PROPERTY_NAME_Q0060 = "Q0060";
        public static readonly String PROPERTY_NAME_Q0061 = "Q0061";
        public static readonly String PROPERTY_NAME_Q0062 = "Q0062";
        public static readonly String PROPERTY_NAME_Q0063 = "Q0063";
        public static readonly String PROPERTY_NAME_Q0064 = "Q0064";
        public static readonly String PROPERTY_NAME_Q0065 = "Q0065";
        public static readonly String PROPERTY_NAME_Q0066 = "Q0066";
        public static readonly String PROPERTY_NAME_Q0067 = "Q0067";
        public static readonly String PROPERTY_NAME_Q0068 = "Q0068";
        public static readonly String PROPERTY_NAME_Q0069 = "Q0069";
        public static readonly String PROPERTY_NAME_Q0070 = "Q0070";
        public static readonly String PROPERTY_NAME_Q0071 = "Q0071";
        public static readonly String PROPERTY_NAME_Q0072 = "Q0072";
        public static readonly String PROPERTY_NAME_Q0073 = "Q0073";
        public static readonly String PROPERTY_NAME_Q0074 = "Q0074";
        public static readonly String PROPERTY_NAME_Q0075 = "Q0075";
        public static readonly String PROPERTY_NAME_Q0076 = "Q0076";
        public static readonly String PROPERTY_NAME_Q0077 = "Q0077";
        public static readonly String PROPERTY_NAME_Q0078 = "Q0078";
        public static readonly String PROPERTY_NAME_Q0079 = "Q0079";
        public static readonly String PROPERTY_NAME_Q0080 = "Q0080";
        public static readonly String PROPERTY_NAME_Q0081 = "Q0081";
        public static readonly String PROPERTY_NAME_Q0082 = "Q0082";
        public static readonly String PROPERTY_NAME_Q0083 = "Q0083";
        public static readonly String PROPERTY_NAME_Q0084 = "Q0084";
        public static readonly String PROPERTY_NAME_Q0085 = "Q0085";
        public static readonly String PROPERTY_NAME_Q0086 = "Q0086";
        public static readonly String PROPERTY_NAME_Q0087 = "Q0087";
        public static readonly String PROPERTY_NAME_Q0088 = "Q0088";
        public static readonly String PROPERTY_NAME_Q0089 = "Q0089";
        public static readonly String PROPERTY_NAME_Q0090 = "Q0090";
        public static readonly String PROPERTY_NAME_Q0091 = "Q0091";
        public static readonly String PROPERTY_NAME_Q0092 = "Q0092";
        public static readonly String PROPERTY_NAME_Q0093 = "Q0093";
        public static readonly String PROPERTY_NAME_Q0094 = "Q0094";
        public static readonly String PROPERTY_NAME_Q0095 = "Q0095";
        public static readonly String PROPERTY_NAME_Q0096 = "Q0096";
        public static readonly String PROPERTY_NAME_Q0097 = "Q0097";
        public static readonly String PROPERTY_NAME_Q0098 = "Q0098";
        public static readonly String PROPERTY_NAME_Q0099 = "Q0099";
        public static readonly String PROPERTY_NAME_Q0100 = "Q0100";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TSurveyDataDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SAMPLE_ID.ToLower(), PROPERTY_NAME_SAMPLE_ID);
                map.put(DB_NAME_MERGE_CODE.ToLower(), PROPERTY_NAME_MERGE_CODE);
                map.put(DB_NAME_SORT_NO.ToLower(), PROPERTY_NAME_SORT_NO);
                map.put(DB_NAME_DELETE_FLAG.ToLower(), PROPERTY_NAME_DELETE_FLAG);
                map.put(DB_NAME_ANSWER_DATE.ToLower(), PROPERTY_NAME_ANSWER_DATE);
                map.put(DB_NAME_SEX.ToLower(), PROPERTY_NAME_SEX);
                map.put(DB_NAME_AGE.ToLower(), PROPERTY_NAME_AGE);
                map.put(DB_NAME_AGE_ID.ToLower(), PROPERTY_NAME_AGE_ID);
                map.put(DB_NAME_PREFECTURE.ToLower(), PROPERTY_NAME_PREFECTURE);
                map.put(DB_NAME_AREA.ToLower(), PROPERTY_NAME_AREA);
                map.put(DB_NAME_MARRIED.ToLower(), PROPERTY_NAME_MARRIED);
                map.put(DB_NAME_CHILD.ToLower(), PROPERTY_NAME_CHILD);
                map.put(DB_NAME_HINCOME.ToLower(), PROPERTY_NAME_HINCOME);
                map.put(DB_NAME_PINCOME.ToLower(), PROPERTY_NAME_PINCOME);
                map.put(DB_NAME_JOB.ToLower(), PROPERTY_NAME_JOB);
                map.put(DB_NAME_STUDENT.ToLower(), PROPERTY_NAME_STUDENT);
                map.put(DB_NAME_CELL.ToLower(), PROPERTY_NAME_CELL);
                map.put(DB_NAME_CELL_NAME.ToLower(), PROPERTY_NAME_CELL_NAME);
                map.put(DB_NAME_Q0001.ToLower(), PROPERTY_NAME_Q0001);
                map.put(DB_NAME_Q0002.ToLower(), PROPERTY_NAME_Q0002);
                map.put(DB_NAME_Q0003.ToLower(), PROPERTY_NAME_Q0003);
                map.put(DB_NAME_Q0004.ToLower(), PROPERTY_NAME_Q0004);
                map.put(DB_NAME_Q0005.ToLower(), PROPERTY_NAME_Q0005);
                map.put(DB_NAME_Q0006.ToLower(), PROPERTY_NAME_Q0006);
                map.put(DB_NAME_Q0007.ToLower(), PROPERTY_NAME_Q0007);
                map.put(DB_NAME_Q0008.ToLower(), PROPERTY_NAME_Q0008);
                map.put(DB_NAME_Q0009.ToLower(), PROPERTY_NAME_Q0009);
                map.put(DB_NAME_Q0010.ToLower(), PROPERTY_NAME_Q0010);
                map.put(DB_NAME_Q0011.ToLower(), PROPERTY_NAME_Q0011);
                map.put(DB_NAME_Q0012.ToLower(), PROPERTY_NAME_Q0012);
                map.put(DB_NAME_Q0013.ToLower(), PROPERTY_NAME_Q0013);
                map.put(DB_NAME_Q0014.ToLower(), PROPERTY_NAME_Q0014);
                map.put(DB_NAME_Q0015.ToLower(), PROPERTY_NAME_Q0015);
                map.put(DB_NAME_Q0016.ToLower(), PROPERTY_NAME_Q0016);
                map.put(DB_NAME_Q0017.ToLower(), PROPERTY_NAME_Q0017);
                map.put(DB_NAME_Q0018.ToLower(), PROPERTY_NAME_Q0018);
                map.put(DB_NAME_Q0019.ToLower(), PROPERTY_NAME_Q0019);
                map.put(DB_NAME_Q0020.ToLower(), PROPERTY_NAME_Q0020);
                map.put(DB_NAME_Q0021.ToLower(), PROPERTY_NAME_Q0021);
                map.put(DB_NAME_Q0022.ToLower(), PROPERTY_NAME_Q0022);
                map.put(DB_NAME_Q0023.ToLower(), PROPERTY_NAME_Q0023);
                map.put(DB_NAME_Q0024.ToLower(), PROPERTY_NAME_Q0024);
                map.put(DB_NAME_Q0025.ToLower(), PROPERTY_NAME_Q0025);
                map.put(DB_NAME_Q0026.ToLower(), PROPERTY_NAME_Q0026);
                map.put(DB_NAME_Q0027.ToLower(), PROPERTY_NAME_Q0027);
                map.put(DB_NAME_Q0028.ToLower(), PROPERTY_NAME_Q0028);
                map.put(DB_NAME_Q0029.ToLower(), PROPERTY_NAME_Q0029);
                map.put(DB_NAME_Q0030.ToLower(), PROPERTY_NAME_Q0030);
                map.put(DB_NAME_Q0031.ToLower(), PROPERTY_NAME_Q0031);
                map.put(DB_NAME_Q0032.ToLower(), PROPERTY_NAME_Q0032);
                map.put(DB_NAME_Q0033.ToLower(), PROPERTY_NAME_Q0033);
                map.put(DB_NAME_Q0034.ToLower(), PROPERTY_NAME_Q0034);
                map.put(DB_NAME_Q0035.ToLower(), PROPERTY_NAME_Q0035);
                map.put(DB_NAME_Q0036.ToLower(), PROPERTY_NAME_Q0036);
                map.put(DB_NAME_Q0037.ToLower(), PROPERTY_NAME_Q0037);
                map.put(DB_NAME_Q0038.ToLower(), PROPERTY_NAME_Q0038);
                map.put(DB_NAME_Q0039.ToLower(), PROPERTY_NAME_Q0039);
                map.put(DB_NAME_Q0040.ToLower(), PROPERTY_NAME_Q0040);
                map.put(DB_NAME_Q0041.ToLower(), PROPERTY_NAME_Q0041);
                map.put(DB_NAME_Q0042.ToLower(), PROPERTY_NAME_Q0042);
                map.put(DB_NAME_Q0043.ToLower(), PROPERTY_NAME_Q0043);
                map.put(DB_NAME_Q0044.ToLower(), PROPERTY_NAME_Q0044);
                map.put(DB_NAME_Q0045.ToLower(), PROPERTY_NAME_Q0045);
                map.put(DB_NAME_Q0046.ToLower(), PROPERTY_NAME_Q0046);
                map.put(DB_NAME_Q0047.ToLower(), PROPERTY_NAME_Q0047);
                map.put(DB_NAME_Q0048.ToLower(), PROPERTY_NAME_Q0048);
                map.put(DB_NAME_Q0049.ToLower(), PROPERTY_NAME_Q0049);
                map.put(DB_NAME_Q0050.ToLower(), PROPERTY_NAME_Q0050);
                map.put(DB_NAME_Q0051.ToLower(), PROPERTY_NAME_Q0051);
                map.put(DB_NAME_Q0052.ToLower(), PROPERTY_NAME_Q0052);
                map.put(DB_NAME_Q0053.ToLower(), PROPERTY_NAME_Q0053);
                map.put(DB_NAME_Q0054.ToLower(), PROPERTY_NAME_Q0054);
                map.put(DB_NAME_Q0055.ToLower(), PROPERTY_NAME_Q0055);
                map.put(DB_NAME_Q0056.ToLower(), PROPERTY_NAME_Q0056);
                map.put(DB_NAME_Q0057.ToLower(), PROPERTY_NAME_Q0057);
                map.put(DB_NAME_Q0058.ToLower(), PROPERTY_NAME_Q0058);
                map.put(DB_NAME_Q0059.ToLower(), PROPERTY_NAME_Q0059);
                map.put(DB_NAME_Q0060.ToLower(), PROPERTY_NAME_Q0060);
                map.put(DB_NAME_Q0061.ToLower(), PROPERTY_NAME_Q0061);
                map.put(DB_NAME_Q0062.ToLower(), PROPERTY_NAME_Q0062);
                map.put(DB_NAME_Q0063.ToLower(), PROPERTY_NAME_Q0063);
                map.put(DB_NAME_Q0064.ToLower(), PROPERTY_NAME_Q0064);
                map.put(DB_NAME_Q0065.ToLower(), PROPERTY_NAME_Q0065);
                map.put(DB_NAME_Q0066.ToLower(), PROPERTY_NAME_Q0066);
                map.put(DB_NAME_Q0067.ToLower(), PROPERTY_NAME_Q0067);
                map.put(DB_NAME_Q0068.ToLower(), PROPERTY_NAME_Q0068);
                map.put(DB_NAME_Q0069.ToLower(), PROPERTY_NAME_Q0069);
                map.put(DB_NAME_Q0070.ToLower(), PROPERTY_NAME_Q0070);
                map.put(DB_NAME_Q0071.ToLower(), PROPERTY_NAME_Q0071);
                map.put(DB_NAME_Q0072.ToLower(), PROPERTY_NAME_Q0072);
                map.put(DB_NAME_Q0073.ToLower(), PROPERTY_NAME_Q0073);
                map.put(DB_NAME_Q0074.ToLower(), PROPERTY_NAME_Q0074);
                map.put(DB_NAME_Q0075.ToLower(), PROPERTY_NAME_Q0075);
                map.put(DB_NAME_Q0076.ToLower(), PROPERTY_NAME_Q0076);
                map.put(DB_NAME_Q0077.ToLower(), PROPERTY_NAME_Q0077);
                map.put(DB_NAME_Q0078.ToLower(), PROPERTY_NAME_Q0078);
                map.put(DB_NAME_Q0079.ToLower(), PROPERTY_NAME_Q0079);
                map.put(DB_NAME_Q0080.ToLower(), PROPERTY_NAME_Q0080);
                map.put(DB_NAME_Q0081.ToLower(), PROPERTY_NAME_Q0081);
                map.put(DB_NAME_Q0082.ToLower(), PROPERTY_NAME_Q0082);
                map.put(DB_NAME_Q0083.ToLower(), PROPERTY_NAME_Q0083);
                map.put(DB_NAME_Q0084.ToLower(), PROPERTY_NAME_Q0084);
                map.put(DB_NAME_Q0085.ToLower(), PROPERTY_NAME_Q0085);
                map.put(DB_NAME_Q0086.ToLower(), PROPERTY_NAME_Q0086);
                map.put(DB_NAME_Q0087.ToLower(), PROPERTY_NAME_Q0087);
                map.put(DB_NAME_Q0088.ToLower(), PROPERTY_NAME_Q0088);
                map.put(DB_NAME_Q0089.ToLower(), PROPERTY_NAME_Q0089);
                map.put(DB_NAME_Q0090.ToLower(), PROPERTY_NAME_Q0090);
                map.put(DB_NAME_Q0091.ToLower(), PROPERTY_NAME_Q0091);
                map.put(DB_NAME_Q0092.ToLower(), PROPERTY_NAME_Q0092);
                map.put(DB_NAME_Q0093.ToLower(), PROPERTY_NAME_Q0093);
                map.put(DB_NAME_Q0094.ToLower(), PROPERTY_NAME_Q0094);
                map.put(DB_NAME_Q0095.ToLower(), PROPERTY_NAME_Q0095);
                map.put(DB_NAME_Q0096.ToLower(), PROPERTY_NAME_Q0096);
                map.put(DB_NAME_Q0097.ToLower(), PROPERTY_NAME_Q0097);
                map.put(DB_NAME_Q0098.ToLower(), PROPERTY_NAME_Q0098);
                map.put(DB_NAME_Q0099.ToLower(), PROPERTY_NAME_Q0099);
                map.put(DB_NAME_Q0100.ToLower(), PROPERTY_NAME_Q0100);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SAMPLE_ID.ToLower(), DB_NAME_SAMPLE_ID);
                map.put(PROPERTY_NAME_MERGE_CODE.ToLower(), DB_NAME_MERGE_CODE);
                map.put(PROPERTY_NAME_SORT_NO.ToLower(), DB_NAME_SORT_NO);
                map.put(PROPERTY_NAME_DELETE_FLAG.ToLower(), DB_NAME_DELETE_FLAG);
                map.put(PROPERTY_NAME_ANSWER_DATE.ToLower(), DB_NAME_ANSWER_DATE);
                map.put(PROPERTY_NAME_SEX.ToLower(), DB_NAME_SEX);
                map.put(PROPERTY_NAME_AGE.ToLower(), DB_NAME_AGE);
                map.put(PROPERTY_NAME_AGE_ID.ToLower(), DB_NAME_AGE_ID);
                map.put(PROPERTY_NAME_PREFECTURE.ToLower(), DB_NAME_PREFECTURE);
                map.put(PROPERTY_NAME_AREA.ToLower(), DB_NAME_AREA);
                map.put(PROPERTY_NAME_MARRIED.ToLower(), DB_NAME_MARRIED);
                map.put(PROPERTY_NAME_CHILD.ToLower(), DB_NAME_CHILD);
                map.put(PROPERTY_NAME_HINCOME.ToLower(), DB_NAME_HINCOME);
                map.put(PROPERTY_NAME_PINCOME.ToLower(), DB_NAME_PINCOME);
                map.put(PROPERTY_NAME_JOB.ToLower(), DB_NAME_JOB);
                map.put(PROPERTY_NAME_STUDENT.ToLower(), DB_NAME_STUDENT);
                map.put(PROPERTY_NAME_CELL.ToLower(), DB_NAME_CELL);
                map.put(PROPERTY_NAME_CELL_NAME.ToLower(), DB_NAME_CELL_NAME);
                map.put(PROPERTY_NAME_Q0001.ToLower(), DB_NAME_Q0001);
                map.put(PROPERTY_NAME_Q0002.ToLower(), DB_NAME_Q0002);
                map.put(PROPERTY_NAME_Q0003.ToLower(), DB_NAME_Q0003);
                map.put(PROPERTY_NAME_Q0004.ToLower(), DB_NAME_Q0004);
                map.put(PROPERTY_NAME_Q0005.ToLower(), DB_NAME_Q0005);
                map.put(PROPERTY_NAME_Q0006.ToLower(), DB_NAME_Q0006);
                map.put(PROPERTY_NAME_Q0007.ToLower(), DB_NAME_Q0007);
                map.put(PROPERTY_NAME_Q0008.ToLower(), DB_NAME_Q0008);
                map.put(PROPERTY_NAME_Q0009.ToLower(), DB_NAME_Q0009);
                map.put(PROPERTY_NAME_Q0010.ToLower(), DB_NAME_Q0010);
                map.put(PROPERTY_NAME_Q0011.ToLower(), DB_NAME_Q0011);
                map.put(PROPERTY_NAME_Q0012.ToLower(), DB_NAME_Q0012);
                map.put(PROPERTY_NAME_Q0013.ToLower(), DB_NAME_Q0013);
                map.put(PROPERTY_NAME_Q0014.ToLower(), DB_NAME_Q0014);
                map.put(PROPERTY_NAME_Q0015.ToLower(), DB_NAME_Q0015);
                map.put(PROPERTY_NAME_Q0016.ToLower(), DB_NAME_Q0016);
                map.put(PROPERTY_NAME_Q0017.ToLower(), DB_NAME_Q0017);
                map.put(PROPERTY_NAME_Q0018.ToLower(), DB_NAME_Q0018);
                map.put(PROPERTY_NAME_Q0019.ToLower(), DB_NAME_Q0019);
                map.put(PROPERTY_NAME_Q0020.ToLower(), DB_NAME_Q0020);
                map.put(PROPERTY_NAME_Q0021.ToLower(), DB_NAME_Q0021);
                map.put(PROPERTY_NAME_Q0022.ToLower(), DB_NAME_Q0022);
                map.put(PROPERTY_NAME_Q0023.ToLower(), DB_NAME_Q0023);
                map.put(PROPERTY_NAME_Q0024.ToLower(), DB_NAME_Q0024);
                map.put(PROPERTY_NAME_Q0025.ToLower(), DB_NAME_Q0025);
                map.put(PROPERTY_NAME_Q0026.ToLower(), DB_NAME_Q0026);
                map.put(PROPERTY_NAME_Q0027.ToLower(), DB_NAME_Q0027);
                map.put(PROPERTY_NAME_Q0028.ToLower(), DB_NAME_Q0028);
                map.put(PROPERTY_NAME_Q0029.ToLower(), DB_NAME_Q0029);
                map.put(PROPERTY_NAME_Q0030.ToLower(), DB_NAME_Q0030);
                map.put(PROPERTY_NAME_Q0031.ToLower(), DB_NAME_Q0031);
                map.put(PROPERTY_NAME_Q0032.ToLower(), DB_NAME_Q0032);
                map.put(PROPERTY_NAME_Q0033.ToLower(), DB_NAME_Q0033);
                map.put(PROPERTY_NAME_Q0034.ToLower(), DB_NAME_Q0034);
                map.put(PROPERTY_NAME_Q0035.ToLower(), DB_NAME_Q0035);
                map.put(PROPERTY_NAME_Q0036.ToLower(), DB_NAME_Q0036);
                map.put(PROPERTY_NAME_Q0037.ToLower(), DB_NAME_Q0037);
                map.put(PROPERTY_NAME_Q0038.ToLower(), DB_NAME_Q0038);
                map.put(PROPERTY_NAME_Q0039.ToLower(), DB_NAME_Q0039);
                map.put(PROPERTY_NAME_Q0040.ToLower(), DB_NAME_Q0040);
                map.put(PROPERTY_NAME_Q0041.ToLower(), DB_NAME_Q0041);
                map.put(PROPERTY_NAME_Q0042.ToLower(), DB_NAME_Q0042);
                map.put(PROPERTY_NAME_Q0043.ToLower(), DB_NAME_Q0043);
                map.put(PROPERTY_NAME_Q0044.ToLower(), DB_NAME_Q0044);
                map.put(PROPERTY_NAME_Q0045.ToLower(), DB_NAME_Q0045);
                map.put(PROPERTY_NAME_Q0046.ToLower(), DB_NAME_Q0046);
                map.put(PROPERTY_NAME_Q0047.ToLower(), DB_NAME_Q0047);
                map.put(PROPERTY_NAME_Q0048.ToLower(), DB_NAME_Q0048);
                map.put(PROPERTY_NAME_Q0049.ToLower(), DB_NAME_Q0049);
                map.put(PROPERTY_NAME_Q0050.ToLower(), DB_NAME_Q0050);
                map.put(PROPERTY_NAME_Q0051.ToLower(), DB_NAME_Q0051);
                map.put(PROPERTY_NAME_Q0052.ToLower(), DB_NAME_Q0052);
                map.put(PROPERTY_NAME_Q0053.ToLower(), DB_NAME_Q0053);
                map.put(PROPERTY_NAME_Q0054.ToLower(), DB_NAME_Q0054);
                map.put(PROPERTY_NAME_Q0055.ToLower(), DB_NAME_Q0055);
                map.put(PROPERTY_NAME_Q0056.ToLower(), DB_NAME_Q0056);
                map.put(PROPERTY_NAME_Q0057.ToLower(), DB_NAME_Q0057);
                map.put(PROPERTY_NAME_Q0058.ToLower(), DB_NAME_Q0058);
                map.put(PROPERTY_NAME_Q0059.ToLower(), DB_NAME_Q0059);
                map.put(PROPERTY_NAME_Q0060.ToLower(), DB_NAME_Q0060);
                map.put(PROPERTY_NAME_Q0061.ToLower(), DB_NAME_Q0061);
                map.put(PROPERTY_NAME_Q0062.ToLower(), DB_NAME_Q0062);
                map.put(PROPERTY_NAME_Q0063.ToLower(), DB_NAME_Q0063);
                map.put(PROPERTY_NAME_Q0064.ToLower(), DB_NAME_Q0064);
                map.put(PROPERTY_NAME_Q0065.ToLower(), DB_NAME_Q0065);
                map.put(PROPERTY_NAME_Q0066.ToLower(), DB_NAME_Q0066);
                map.put(PROPERTY_NAME_Q0067.ToLower(), DB_NAME_Q0067);
                map.put(PROPERTY_NAME_Q0068.ToLower(), DB_NAME_Q0068);
                map.put(PROPERTY_NAME_Q0069.ToLower(), DB_NAME_Q0069);
                map.put(PROPERTY_NAME_Q0070.ToLower(), DB_NAME_Q0070);
                map.put(PROPERTY_NAME_Q0071.ToLower(), DB_NAME_Q0071);
                map.put(PROPERTY_NAME_Q0072.ToLower(), DB_NAME_Q0072);
                map.put(PROPERTY_NAME_Q0073.ToLower(), DB_NAME_Q0073);
                map.put(PROPERTY_NAME_Q0074.ToLower(), DB_NAME_Q0074);
                map.put(PROPERTY_NAME_Q0075.ToLower(), DB_NAME_Q0075);
                map.put(PROPERTY_NAME_Q0076.ToLower(), DB_NAME_Q0076);
                map.put(PROPERTY_NAME_Q0077.ToLower(), DB_NAME_Q0077);
                map.put(PROPERTY_NAME_Q0078.ToLower(), DB_NAME_Q0078);
                map.put(PROPERTY_NAME_Q0079.ToLower(), DB_NAME_Q0079);
                map.put(PROPERTY_NAME_Q0080.ToLower(), DB_NAME_Q0080);
                map.put(PROPERTY_NAME_Q0081.ToLower(), DB_NAME_Q0081);
                map.put(PROPERTY_NAME_Q0082.ToLower(), DB_NAME_Q0082);
                map.put(PROPERTY_NAME_Q0083.ToLower(), DB_NAME_Q0083);
                map.put(PROPERTY_NAME_Q0084.ToLower(), DB_NAME_Q0084);
                map.put(PROPERTY_NAME_Q0085.ToLower(), DB_NAME_Q0085);
                map.put(PROPERTY_NAME_Q0086.ToLower(), DB_NAME_Q0086);
                map.put(PROPERTY_NAME_Q0087.ToLower(), DB_NAME_Q0087);
                map.put(PROPERTY_NAME_Q0088.ToLower(), DB_NAME_Q0088);
                map.put(PROPERTY_NAME_Q0089.ToLower(), DB_NAME_Q0089);
                map.put(PROPERTY_NAME_Q0090.ToLower(), DB_NAME_Q0090);
                map.put(PROPERTY_NAME_Q0091.ToLower(), DB_NAME_Q0091);
                map.put(PROPERTY_NAME_Q0092.ToLower(), DB_NAME_Q0092);
                map.put(PROPERTY_NAME_Q0093.ToLower(), DB_NAME_Q0093);
                map.put(PROPERTY_NAME_Q0094.ToLower(), DB_NAME_Q0094);
                map.put(PROPERTY_NAME_Q0095.ToLower(), DB_NAME_Q0095);
                map.put(PROPERTY_NAME_Q0096.ToLower(), DB_NAME_Q0096);
                map.put(PROPERTY_NAME_Q0097.ToLower(), DB_NAME_Q0097);
                map.put(PROPERTY_NAME_Q0098.ToLower(), DB_NAME_Q0098);
                map.put(PROPERTY_NAME_Q0099.ToLower(), DB_NAME_Q0099);
                map.put(PROPERTY_NAME_Q0100.ToLower(), DB_NAME_Q0100);
                _propertyNameDbNameKeyToLowerMap = map;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                        Name Map
        //                                                                        ========
        #region Name Map
        public override Map<String, String> DbNamePropertyNameKeyToLowerMap { get { return _dbNamePropertyNameKeyToLowerMap; } }
        public override Map<String, String> PropertyNameDbNameKeyToLowerMap { get { return _propertyNameDbNameKeyToLowerMap; } }
        #endregion

        // ===============================================================================
        //                                                                       Type Name
        //                                                                       =========
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TSurveyData"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TSurveyDataDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TSurveyDataCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TSurveyDataBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TSurveyData NewMyEntity() { return new TSurveyData(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TSurveyDataCB NewMyConditionBean() { return new TSurveyDataCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TSurveyData>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TSurveyData>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SAMPLE_ID", "SampleId", new EntityPropertySampleIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MERGE_CODE", "MergeCode", new EntityPropertyMergeCodeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SORT_NO", "SortNo", new EntityPropertySortNoSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("DELETE_FLAG", "DeleteFlag", new EntityPropertyDeleteFlagSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("ANSWER_DATE", "AnswerDate", new EntityPropertyAnswerDateSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("SEX", "Sex", new EntityPropertySexSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AGE", "Age", new EntityPropertyAgeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AGE_ID", "AgeId", new EntityPropertyAgeIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PREFECTURE", "Prefecture", new EntityPropertyPrefectureSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("AREA", "Area", new EntityPropertyAreaSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("MARRIED", "Married", new EntityPropertyMarriedSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CHILD", "Child", new EntityPropertyChildSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("HINCOME", "Hincome", new EntityPropertyHincomeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("PINCOME", "Pincome", new EntityPropertyPincomeSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("JOB", "Job", new EntityPropertyJobSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("STUDENT", "Student", new EntityPropertyStudentSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CELL", "Cell", new EntityPropertyCellSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("CELL_NAME", "CellName", new EntityPropertyCellNameSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0001", "Q0001", new EntityPropertyQ0001Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0002", "Q0002", new EntityPropertyQ0002Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0003", "Q0003", new EntityPropertyQ0003Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0004", "Q0004", new EntityPropertyQ0004Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0005", "Q0005", new EntityPropertyQ0005Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0006", "Q0006", new EntityPropertyQ0006Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0007", "Q0007", new EntityPropertyQ0007Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0008", "Q0008", new EntityPropertyQ0008Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0009", "Q0009", new EntityPropertyQ0009Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0010", "Q0010", new EntityPropertyQ0010Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0011", "Q0011", new EntityPropertyQ0011Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0012", "Q0012", new EntityPropertyQ0012Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0013", "Q0013", new EntityPropertyQ0013Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0014", "Q0014", new EntityPropertyQ0014Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0015", "Q0015", new EntityPropertyQ0015Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0016", "Q0016", new EntityPropertyQ0016Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0017", "Q0017", new EntityPropertyQ0017Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0018", "Q0018", new EntityPropertyQ0018Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0019", "Q0019", new EntityPropertyQ0019Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0020", "Q0020", new EntityPropertyQ0020Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0021", "Q0021", new EntityPropertyQ0021Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0022", "Q0022", new EntityPropertyQ0022Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0023", "Q0023", new EntityPropertyQ0023Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0024", "Q0024", new EntityPropertyQ0024Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0025", "Q0025", new EntityPropertyQ0025Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0026", "Q0026", new EntityPropertyQ0026Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0027", "Q0027", new EntityPropertyQ0027Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0028", "Q0028", new EntityPropertyQ0028Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0029", "Q0029", new EntityPropertyQ0029Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0030", "Q0030", new EntityPropertyQ0030Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0031", "Q0031", new EntityPropertyQ0031Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0032", "Q0032", new EntityPropertyQ0032Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0033", "Q0033", new EntityPropertyQ0033Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0034", "Q0034", new EntityPropertyQ0034Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0035", "Q0035", new EntityPropertyQ0035Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0036", "Q0036", new EntityPropertyQ0036Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0037", "Q0037", new EntityPropertyQ0037Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0038", "Q0038", new EntityPropertyQ0038Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0039", "Q0039", new EntityPropertyQ0039Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0040", "Q0040", new EntityPropertyQ0040Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0041", "Q0041", new EntityPropertyQ0041Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0042", "Q0042", new EntityPropertyQ0042Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0043", "Q0043", new EntityPropertyQ0043Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0044", "Q0044", new EntityPropertyQ0044Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0045", "Q0045", new EntityPropertyQ0045Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0046", "Q0046", new EntityPropertyQ0046Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0047", "Q0047", new EntityPropertyQ0047Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0048", "Q0048", new EntityPropertyQ0048Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0049", "Q0049", new EntityPropertyQ0049Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0050", "Q0050", new EntityPropertyQ0050Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0051", "Q0051", new EntityPropertyQ0051Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0052", "Q0052", new EntityPropertyQ0052Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0053", "Q0053", new EntityPropertyQ0053Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0054", "Q0054", new EntityPropertyQ0054Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0055", "Q0055", new EntityPropertyQ0055Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0056", "Q0056", new EntityPropertyQ0056Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0057", "Q0057", new EntityPropertyQ0057Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0058", "Q0058", new EntityPropertyQ0058Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0059", "Q0059", new EntityPropertyQ0059Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0060", "Q0060", new EntityPropertyQ0060Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0061", "Q0061", new EntityPropertyQ0061Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0062", "Q0062", new EntityPropertyQ0062Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0063", "Q0063", new EntityPropertyQ0063Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0064", "Q0064", new EntityPropertyQ0064Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0065", "Q0065", new EntityPropertyQ0065Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0066", "Q0066", new EntityPropertyQ0066Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0067", "Q0067", new EntityPropertyQ0067Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0068", "Q0068", new EntityPropertyQ0068Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0069", "Q0069", new EntityPropertyQ0069Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0070", "Q0070", new EntityPropertyQ0070Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0071", "Q0071", new EntityPropertyQ0071Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0072", "Q0072", new EntityPropertyQ0072Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0073", "Q0073", new EntityPropertyQ0073Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0074", "Q0074", new EntityPropertyQ0074Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0075", "Q0075", new EntityPropertyQ0075Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0076", "Q0076", new EntityPropertyQ0076Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0077", "Q0077", new EntityPropertyQ0077Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0078", "Q0078", new EntityPropertyQ0078Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0079", "Q0079", new EntityPropertyQ0079Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0080", "Q0080", new EntityPropertyQ0080Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0081", "Q0081", new EntityPropertyQ0081Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0082", "Q0082", new EntityPropertyQ0082Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0083", "Q0083", new EntityPropertyQ0083Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0084", "Q0084", new EntityPropertyQ0084Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0085", "Q0085", new EntityPropertyQ0085Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0086", "Q0086", new EntityPropertyQ0086Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0087", "Q0087", new EntityPropertyQ0087Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0088", "Q0088", new EntityPropertyQ0088Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0089", "Q0089", new EntityPropertyQ0089Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0090", "Q0090", new EntityPropertyQ0090Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0091", "Q0091", new EntityPropertyQ0091Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0092", "Q0092", new EntityPropertyQ0092Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0093", "Q0093", new EntityPropertyQ0093Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0094", "Q0094", new EntityPropertyQ0094Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0095", "Q0095", new EntityPropertyQ0095Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0096", "Q0096", new EntityPropertyQ0096Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0097", "Q0097", new EntityPropertyQ0097Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0098", "Q0098", new EntityPropertyQ0098Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0099", "Q0099", new EntityPropertyQ0099Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("Q0100", "Q0100", new EntityPropertyQ0100Setupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TSurveyData> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TSurveyData)entity, value);
        }

        public class EntityPropertySampleIdSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.SampleId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMergeCodeSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.MergeCode = (value != null) ? (String)value : null; }
        }
        public class EntityPropertySortNoSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.SortNo = (value != null) ? (long?)value : null; }
        }
        public class EntityPropertyDeleteFlagSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.DeleteFlag = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAnswerDateSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.AnswerDate = (value != null) ? (DateTime?)value : null; }
        }
        public class EntityPropertySexSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Sex = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyAgeSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Age = (value != null) ? (int?)value : null; }
        }
        public class EntityPropertyAgeIdSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.AgeId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyPrefectureSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Prefecture = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyAreaSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Area = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyMarriedSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Married = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyChildSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Child = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyHincomeSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Hincome = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyPincomeSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Pincome = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyJobSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Job = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyStudentSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Student = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCellSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Cell = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyCellNameSetupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.CellName = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0001Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0001 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0002Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0002 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0003Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0003 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0004Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0004 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0005Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0005 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0006Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0006 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0007Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0007 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0008Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0008 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0009Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0009 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0010Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0010 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0011Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0011 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0012Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0012 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0013Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0013 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0014Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0014 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0015Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0015 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0016Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0016 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0017Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0017 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0018Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0018 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0019Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0019 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0020Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0020 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0021Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0021 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0022Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0022 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0023Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0023 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0024Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0024 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0025Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0025 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0026Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0026 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0027Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0027 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0028Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0028 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0029Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0029 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0030Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0030 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0031Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0031 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0032Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0032 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0033Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0033 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0034Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0034 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0035Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0035 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0036Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0036 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0037Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0037 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0038Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0038 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0039Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0039 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0040Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0040 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0041Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0041 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0042Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0042 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0043Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0043 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0044Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0044 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0045Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0045 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0046Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0046 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0047Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0047 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0048Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0048 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0049Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0049 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0050Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0050 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0051Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0051 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0052Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0052 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0053Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0053 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0054Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0054 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0055Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0055 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0056Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0056 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0057Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0057 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0058Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0058 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0059Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0059 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0060Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0060 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0061Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0061 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0062Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0062 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0063Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0063 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0064Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0064 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0065Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0065 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0066Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0066 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0067Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0067 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0068Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0068 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0069Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0069 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0070Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0070 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0071Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0071 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0072Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0072 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0073Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0073 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0074Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0074 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0075Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0075 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0076Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0076 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0077Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0077 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0078Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0078 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0079Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0079 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0080Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0080 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0081Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0081 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0082Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0082 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0083Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0083 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0084Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0084 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0085Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0085 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0086Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0086 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0087Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0087 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0088Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0088 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0089Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0089 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0090Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0090 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0091Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0091 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0092Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0092 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0093Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0093 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0094Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0094 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0095Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0095 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0096Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0096 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0097Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0097 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0098Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0098 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0099Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0099 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyQ0100Setupper : EntityPropertySetupper<TSurveyData> {
            public void Setup(TSurveyData entity, Object value) { entity.Q0100 = (value != null) ? (String)value : null; }
        }
    }
}
