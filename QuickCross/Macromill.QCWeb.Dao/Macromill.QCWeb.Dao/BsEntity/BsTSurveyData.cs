

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Helper;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.BsEntity.Dbm;


namespace Macromill.QCWeb.Dao.ExEntity {

    /// <summary>
    /// The entity of T_SURVEY_DATA as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     SAMPLE_ID
    /// 
    /// [column]
    ///     SAMPLE_ID, MERGE_CODE, SORT_NO, DELETE_FLAG, ANSWER_DATE, SEX, AGE, AGE_ID, PREFECTURE, AREA, MARRIED, CHILD, HINCOME, PINCOME, JOB, STUDENT, CELL, CELL_NAME, Q0001, Q0002, Q0003, Q0004, Q0005, Q0006, Q0007, Q0008, Q0009, Q0010, Q0011, Q0012, Q0013, Q0014, Q0015, Q0016, Q0017, Q0018, Q0019, Q0020, Q0021, Q0022, Q0023, Q0024, Q0025, Q0026, Q0027, Q0028, Q0029, Q0030, Q0031, Q0032, Q0033, Q0034, Q0035, Q0036, Q0037, Q0038, Q0039, Q0040, Q0041, Q0042, Q0043, Q0044, Q0045, Q0046, Q0047, Q0048, Q0049, Q0050, Q0051, Q0052, Q0053, Q0054, Q0055, Q0056, Q0057, Q0058, Q0059, Q0060, Q0061, Q0062, Q0063, Q0064, Q0065, Q0066, Q0067, Q0068, Q0069, Q0070, Q0071, Q0072, Q0073, Q0074, Q0075, Q0076, Q0077, Q0078, Q0079, Q0080, Q0081, Q0082, Q0083, Q0084, Q0085, Q0086, Q0087, Q0088, Q0089, Q0090, Q0091, Q0092, Q0093, Q0094, Q0095, Q0096, Q0097, Q0098, Q0099, Q0100
    /// 
    /// [sequence]
    ///     
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_SURVEY_DATA")]
    [System.Serializable]
    public partial class TSurveyData : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SAMPLE_ID: {PK, NotNull, VARCHAR2(10)}</summary>
        protected String _sampleId;

        /// <summary>MERGE_CODE: {VARCHAR2(10)}</summary>
        protected String _mergeCode;

        /// <summary>SORT_NO: {NotNull, NUMBER(10)}</summary>
        protected long? _sortNo;

        /// <summary>DELETE_FLAG: {NUMBER(1), classification=DeleteFlag}</summary>
        protected int? _deleteFlag;

        /// <summary>ANSWER_DATE: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _answerDate;

        /// <summary>SEX: {CHAR(1)}</summary>
        protected String _sex;

        /// <summary>AGE: {NUMBER(3)}</summary>
        protected int? _age;

        /// <summary>AGE_ID: {CHAR(2)}</summary>
        protected String _ageId;

        /// <summary>PREFECTURE: {CHAR(2)}</summary>
        protected String _prefecture;

        /// <summary>AREA: {CHAR(2)}</summary>
        protected String _area;

        /// <summary>MARRIED: {CHAR(1)}</summary>
        protected String _married;

        /// <summary>CHILD: {CHAR(1)}</summary>
        protected String _child;

        /// <summary>HINCOME: {CHAR(2)}</summary>
        protected String _hincome;

        /// <summary>PINCOME: {CHAR(2)}</summary>
        protected String _pincome;

        /// <summary>JOB: {CHAR(2)}</summary>
        protected String _job;

        /// <summary>STUDENT: {CHAR(1)}</summary>
        protected String _student;

        /// <summary>CELL: {VARCHAR2(20)}</summary>
        protected String _cell;

        /// <summary>CELL_NAME: {NVARCHAR2(200)}</summary>
        protected String _cellName;

        /// <summary>Q0001: {VARCHAR2(2000)}</summary>
        protected String _q0001;

        /// <summary>Q0002: {VARCHAR2(2000)}</summary>
        protected String _q0002;

        /// <summary>Q0003: {VARCHAR2(2000)}</summary>
        protected String _q0003;

        /// <summary>Q0004: {VARCHAR2(2000)}</summary>
        protected String _q0004;

        /// <summary>Q0005: {VARCHAR2(2000)}</summary>
        protected String _q0005;

        /// <summary>Q0006: {VARCHAR2(2000)}</summary>
        protected String _q0006;

        /// <summary>Q0007: {VARCHAR2(2000)}</summary>
        protected String _q0007;

        /// <summary>Q0008: {VARCHAR2(2000)}</summary>
        protected String _q0008;

        /// <summary>Q0009: {VARCHAR2(2000)}</summary>
        protected String _q0009;

        /// <summary>Q0010: {VARCHAR2(2000)}</summary>
        protected String _q0010;

        /// <summary>Q0011: {VARCHAR2(2000)}</summary>
        protected String _q0011;

        /// <summary>Q0012: {VARCHAR2(2000)}</summary>
        protected String _q0012;

        /// <summary>Q0013: {VARCHAR2(2000)}</summary>
        protected String _q0013;

        /// <summary>Q0014: {VARCHAR2(2000)}</summary>
        protected String _q0014;

        /// <summary>Q0015: {VARCHAR2(2000)}</summary>
        protected String _q0015;

        /// <summary>Q0016: {VARCHAR2(2000)}</summary>
        protected String _q0016;

        /// <summary>Q0017: {VARCHAR2(2000)}</summary>
        protected String _q0017;

        /// <summary>Q0018: {VARCHAR2(2000)}</summary>
        protected String _q0018;

        /// <summary>Q0019: {VARCHAR2(2000)}</summary>
        protected String _q0019;

        /// <summary>Q0020: {VARCHAR2(2000)}</summary>
        protected String _q0020;

        /// <summary>Q0021: {VARCHAR2(2000)}</summary>
        protected String _q0021;

        /// <summary>Q0022: {VARCHAR2(2000)}</summary>
        protected String _q0022;

        /// <summary>Q0023: {VARCHAR2(2000)}</summary>
        protected String _q0023;

        /// <summary>Q0024: {VARCHAR2(2000)}</summary>
        protected String _q0024;

        /// <summary>Q0025: {VARCHAR2(2000)}</summary>
        protected String _q0025;

        /// <summary>Q0026: {VARCHAR2(2000)}</summary>
        protected String _q0026;

        /// <summary>Q0027: {VARCHAR2(2000)}</summary>
        protected String _q0027;

        /// <summary>Q0028: {VARCHAR2(2000)}</summary>
        protected String _q0028;

        /// <summary>Q0029: {VARCHAR2(2000)}</summary>
        protected String _q0029;

        /// <summary>Q0030: {VARCHAR2(2000)}</summary>
        protected String _q0030;

        /// <summary>Q0031: {VARCHAR2(2000)}</summary>
        protected String _q0031;

        /// <summary>Q0032: {VARCHAR2(2000)}</summary>
        protected String _q0032;

        /// <summary>Q0033: {VARCHAR2(2000)}</summary>
        protected String _q0033;

        /// <summary>Q0034: {VARCHAR2(2000)}</summary>
        protected String _q0034;

        /// <summary>Q0035: {VARCHAR2(2000)}</summary>
        protected String _q0035;

        /// <summary>Q0036: {VARCHAR2(2000)}</summary>
        protected String _q0036;

        /// <summary>Q0037: {VARCHAR2(2000)}</summary>
        protected String _q0037;

        /// <summary>Q0038: {VARCHAR2(2000)}</summary>
        protected String _q0038;

        /// <summary>Q0039: {VARCHAR2(2000)}</summary>
        protected String _q0039;

        /// <summary>Q0040: {VARCHAR2(2000)}</summary>
        protected String _q0040;

        /// <summary>Q0041: {VARCHAR2(2000)}</summary>
        protected String _q0041;

        /// <summary>Q0042: {VARCHAR2(2000)}</summary>
        protected String _q0042;

        /// <summary>Q0043: {VARCHAR2(2000)}</summary>
        protected String _q0043;

        /// <summary>Q0044: {VARCHAR2(2000)}</summary>
        protected String _q0044;

        /// <summary>Q0045: {VARCHAR2(2000)}</summary>
        protected String _q0045;

        /// <summary>Q0046: {VARCHAR2(2000)}</summary>
        protected String _q0046;

        /// <summary>Q0047: {VARCHAR2(2000)}</summary>
        protected String _q0047;

        /// <summary>Q0048: {VARCHAR2(2000)}</summary>
        protected String _q0048;

        /// <summary>Q0049: {VARCHAR2(2000)}</summary>
        protected String _q0049;

        /// <summary>Q0050: {VARCHAR2(2000)}</summary>
        protected String _q0050;

        /// <summary>Q0051: {VARCHAR2(2000)}</summary>
        protected String _q0051;

        /// <summary>Q0052: {VARCHAR2(2000)}</summary>
        protected String _q0052;

        /// <summary>Q0053: {VARCHAR2(2000)}</summary>
        protected String _q0053;

        /// <summary>Q0054: {VARCHAR2(2000)}</summary>
        protected String _q0054;

        /// <summary>Q0055: {VARCHAR2(2000)}</summary>
        protected String _q0055;

        /// <summary>Q0056: {VARCHAR2(2000)}</summary>
        protected String _q0056;

        /// <summary>Q0057: {VARCHAR2(2000)}</summary>
        protected String _q0057;

        /// <summary>Q0058: {VARCHAR2(2000)}</summary>
        protected String _q0058;

        /// <summary>Q0059: {VARCHAR2(2000)}</summary>
        protected String _q0059;

        /// <summary>Q0060: {VARCHAR2(2000)}</summary>
        protected String _q0060;

        /// <summary>Q0061: {VARCHAR2(2000)}</summary>
        protected String _q0061;

        /// <summary>Q0062: {VARCHAR2(2000)}</summary>
        protected String _q0062;

        /// <summary>Q0063: {VARCHAR2(2000)}</summary>
        protected String _q0063;

        /// <summary>Q0064: {VARCHAR2(2000)}</summary>
        protected String _q0064;

        /// <summary>Q0065: {VARCHAR2(2000)}</summary>
        protected String _q0065;

        /// <summary>Q0066: {VARCHAR2(2000)}</summary>
        protected String _q0066;

        /// <summary>Q0067: {VARCHAR2(2000)}</summary>
        protected String _q0067;

        /// <summary>Q0068: {VARCHAR2(2000)}</summary>
        protected String _q0068;

        /// <summary>Q0069: {VARCHAR2(2000)}</summary>
        protected String _q0069;

        /// <summary>Q0070: {VARCHAR2(2000)}</summary>
        protected String _q0070;

        /// <summary>Q0071: {VARCHAR2(2000)}</summary>
        protected String _q0071;

        /// <summary>Q0072: {VARCHAR2(2000)}</summary>
        protected String _q0072;

        /// <summary>Q0073: {VARCHAR2(2000)}</summary>
        protected String _q0073;

        /// <summary>Q0074: {VARCHAR2(2000)}</summary>
        protected String _q0074;

        /// <summary>Q0075: {VARCHAR2(2000)}</summary>
        protected String _q0075;

        /// <summary>Q0076: {VARCHAR2(2000)}</summary>
        protected String _q0076;

        /// <summary>Q0077: {VARCHAR2(2000)}</summary>
        protected String _q0077;

        /// <summary>Q0078: {VARCHAR2(2000)}</summary>
        protected String _q0078;

        /// <summary>Q0079: {VARCHAR2(2000)}</summary>
        protected String _q0079;

        /// <summary>Q0080: {VARCHAR2(2000)}</summary>
        protected String _q0080;

        /// <summary>Q0081: {VARCHAR2(2000)}</summary>
        protected String _q0081;

        /// <summary>Q0082: {VARCHAR2(2000)}</summary>
        protected String _q0082;

        /// <summary>Q0083: {VARCHAR2(2000)}</summary>
        protected String _q0083;

        /// <summary>Q0084: {VARCHAR2(2000)}</summary>
        protected String _q0084;

        /// <summary>Q0085: {VARCHAR2(2000)}</summary>
        protected String _q0085;

        /// <summary>Q0086: {VARCHAR2(2000)}</summary>
        protected String _q0086;

        /// <summary>Q0087: {VARCHAR2(2000)}</summary>
        protected String _q0087;

        /// <summary>Q0088: {VARCHAR2(2000)}</summary>
        protected String _q0088;

        /// <summary>Q0089: {VARCHAR2(2000)}</summary>
        protected String _q0089;

        /// <summary>Q0090: {VARCHAR2(2000)}</summary>
        protected String _q0090;

        /// <summary>Q0091: {VARCHAR2(2000)}</summary>
        protected String _q0091;

        /// <summary>Q0092: {VARCHAR2(2000)}</summary>
        protected String _q0092;

        /// <summary>Q0093: {VARCHAR2(2000)}</summary>
        protected String _q0093;

        /// <summary>Q0094: {VARCHAR2(2000)}</summary>
        protected String _q0094;

        /// <summary>Q0095: {VARCHAR2(2000)}</summary>
        protected String _q0095;

        /// <summary>Q0096: {VARCHAR2(2000)}</summary>
        protected String _q0096;

        /// <summary>Q0097: {VARCHAR2(2000)}</summary>
        protected String _q0097;

        /// <summary>Q0098: {VARCHAR2(2000)}</summary>
        protected String _q0098;

        /// <summary>Q0099: {VARCHAR2(2000)}</summary>
        protected String _q0099;

        /// <summary>Q0100: {VARCHAR2(2000)}</summary>
        protected String _q0100;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_SURVEY_DATA"; } }
        public String TablePropertyName { get { return "TSurveyData"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.DeleteFlag DeleteFlagAsDeleteFlag { get {
            return CDef.DeleteFlag.CodeOf(_deleteFlag);
        } set {
            DeleteFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of deleteFlag as True.
        /// <![CDATA[
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_True() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.True;
        }

        /// <summary>
        /// Set the value of deleteFlag as False.
        /// <![CDATA[
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public void SetDeleteFlag_False() {
            DeleteFlagAsDeleteFlag = CDef.DeleteFlag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of deleteFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagTrue {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of deleteFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 未削除を示す
        /// ]]>
        /// </summary>
        public bool IsDeleteFlagFalse {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Equals(CDef.DeleteFlag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String DeleteFlagName {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String DeleteFlagAlias {
            get {
                CDef.DeleteFlag cls = DeleteFlagAsDeleteFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_sampleId == null) { return false; }
                return true;
            }
        }

        // ===============================================================================
        //                                                             Modified Properties
        //                                                             ===================
        public virtual IDictionary<String, Object> ModifiedPropertyNames {
            get { return __modifiedProperties.PropertyNames; }
        }

        public virtual void ClearModifiedPropertyNames() {
            __modifiedProperties.Clear();
        }

        // ===============================================================================
        //                                                                  Basic Override
        //                                                                  ==============
        #region Basic Override
        public override bool Equals(Object other) {
            if (other == null || !(other is TSurveyData)) { return false; }
            TSurveyData otherEntity = (TSurveyData)other;
            if (!xSV(this.SampleId, otherEntity.SampleId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _sampleId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TSurveyData:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            return sb.ToString();
        }

        public virtual String BuildDisplayString(String name, bool column, bool relation) {
            StringBuilder sb = new StringBuilder();
            if (name != null) { sb.Append(name).Append(column || relation ? ":" : ""); }
            if (column) { sb.Append(BuildColumnString()); }
            if (relation) { sb.Append(BuildRelationString()); }
            return sb.ToString();
        }
        protected virtual String BuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(this.SampleId);
            sb.Append(c).Append(this.MergeCode);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.DeleteFlag);
            sb.Append(c).Append(this.AnswerDate);
            sb.Append(c).Append(this.Sex);
            sb.Append(c).Append(this.Age);
            sb.Append(c).Append(this.AgeId);
            sb.Append(c).Append(this.Prefecture);
            sb.Append(c).Append(this.Area);
            sb.Append(c).Append(this.Married);
            sb.Append(c).Append(this.Child);
            sb.Append(c).Append(this.Hincome);
            sb.Append(c).Append(this.Pincome);
            sb.Append(c).Append(this.Job);
            sb.Append(c).Append(this.Student);
            sb.Append(c).Append(this.Cell);
            sb.Append(c).Append(this.CellName);
            sb.Append(c).Append(this.Q0001);
            sb.Append(c).Append(this.Q0002);
            sb.Append(c).Append(this.Q0003);
            sb.Append(c).Append(this.Q0004);
            sb.Append(c).Append(this.Q0005);
            sb.Append(c).Append(this.Q0006);
            sb.Append(c).Append(this.Q0007);
            sb.Append(c).Append(this.Q0008);
            sb.Append(c).Append(this.Q0009);
            sb.Append(c).Append(this.Q0010);
            sb.Append(c).Append(this.Q0011);
            sb.Append(c).Append(this.Q0012);
            sb.Append(c).Append(this.Q0013);
            sb.Append(c).Append(this.Q0014);
            sb.Append(c).Append(this.Q0015);
            sb.Append(c).Append(this.Q0016);
            sb.Append(c).Append(this.Q0017);
            sb.Append(c).Append(this.Q0018);
            sb.Append(c).Append(this.Q0019);
            sb.Append(c).Append(this.Q0020);
            sb.Append(c).Append(this.Q0021);
            sb.Append(c).Append(this.Q0022);
            sb.Append(c).Append(this.Q0023);
            sb.Append(c).Append(this.Q0024);
            sb.Append(c).Append(this.Q0025);
            sb.Append(c).Append(this.Q0026);
            sb.Append(c).Append(this.Q0027);
            sb.Append(c).Append(this.Q0028);
            sb.Append(c).Append(this.Q0029);
            sb.Append(c).Append(this.Q0030);
            sb.Append(c).Append(this.Q0031);
            sb.Append(c).Append(this.Q0032);
            sb.Append(c).Append(this.Q0033);
            sb.Append(c).Append(this.Q0034);
            sb.Append(c).Append(this.Q0035);
            sb.Append(c).Append(this.Q0036);
            sb.Append(c).Append(this.Q0037);
            sb.Append(c).Append(this.Q0038);
            sb.Append(c).Append(this.Q0039);
            sb.Append(c).Append(this.Q0040);
            sb.Append(c).Append(this.Q0041);
            sb.Append(c).Append(this.Q0042);
            sb.Append(c).Append(this.Q0043);
            sb.Append(c).Append(this.Q0044);
            sb.Append(c).Append(this.Q0045);
            sb.Append(c).Append(this.Q0046);
            sb.Append(c).Append(this.Q0047);
            sb.Append(c).Append(this.Q0048);
            sb.Append(c).Append(this.Q0049);
            sb.Append(c).Append(this.Q0050);
            sb.Append(c).Append(this.Q0051);
            sb.Append(c).Append(this.Q0052);
            sb.Append(c).Append(this.Q0053);
            sb.Append(c).Append(this.Q0054);
            sb.Append(c).Append(this.Q0055);
            sb.Append(c).Append(this.Q0056);
            sb.Append(c).Append(this.Q0057);
            sb.Append(c).Append(this.Q0058);
            sb.Append(c).Append(this.Q0059);
            sb.Append(c).Append(this.Q0060);
            sb.Append(c).Append(this.Q0061);
            sb.Append(c).Append(this.Q0062);
            sb.Append(c).Append(this.Q0063);
            sb.Append(c).Append(this.Q0064);
            sb.Append(c).Append(this.Q0065);
            sb.Append(c).Append(this.Q0066);
            sb.Append(c).Append(this.Q0067);
            sb.Append(c).Append(this.Q0068);
            sb.Append(c).Append(this.Q0069);
            sb.Append(c).Append(this.Q0070);
            sb.Append(c).Append(this.Q0071);
            sb.Append(c).Append(this.Q0072);
            sb.Append(c).Append(this.Q0073);
            sb.Append(c).Append(this.Q0074);
            sb.Append(c).Append(this.Q0075);
            sb.Append(c).Append(this.Q0076);
            sb.Append(c).Append(this.Q0077);
            sb.Append(c).Append(this.Q0078);
            sb.Append(c).Append(this.Q0079);
            sb.Append(c).Append(this.Q0080);
            sb.Append(c).Append(this.Q0081);
            sb.Append(c).Append(this.Q0082);
            sb.Append(c).Append(this.Q0083);
            sb.Append(c).Append(this.Q0084);
            sb.Append(c).Append(this.Q0085);
            sb.Append(c).Append(this.Q0086);
            sb.Append(c).Append(this.Q0087);
            sb.Append(c).Append(this.Q0088);
            sb.Append(c).Append(this.Q0089);
            sb.Append(c).Append(this.Q0090);
            sb.Append(c).Append(this.Q0091);
            sb.Append(c).Append(this.Q0092);
            sb.Append(c).Append(this.Q0093);
            sb.Append(c).Append(this.Q0094);
            sb.Append(c).Append(this.Q0095);
            sb.Append(c).Append(this.Q0096);
            sb.Append(c).Append(this.Q0097);
            sb.Append(c).Append(this.Q0098);
            sb.Append(c).Append(this.Q0099);
            sb.Append(c).Append(this.Q0100);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            return "";
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>SAMPLE_ID: {PK, NotNull, VARCHAR2(10)}</summary>
        [Seasar.Dao.Attrs.Column("SAMPLE_ID")]
        public String SampleId {
            get { return _sampleId; }
            set {
                __modifiedProperties.AddPropertyName("SampleId");
                _sampleId = value;
            }
        }

        /// <summary>MERGE_CODE: {VARCHAR2(10)}</summary>
        [Seasar.Dao.Attrs.Column("MERGE_CODE")]
        public String MergeCode {
            get { return _mergeCode; }
            set {
                __modifiedProperties.AddPropertyName("MergeCode");
                _mergeCode = value;
            }
        }

        /// <summary>SORT_NO: {NotNull, NUMBER(10)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public long? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>DELETE_FLAG: {NUMBER(1), classification=DeleteFlag}</summary>
        [Seasar.Dao.Attrs.Column("DELETE_FLAG")]
        public int? DeleteFlag {
            get { return _deleteFlag; }
            set {
                __modifiedProperties.AddPropertyName("DeleteFlag");
                _deleteFlag = value;
            }
        }

        /// <summary>ANSWER_DATE: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("ANSWER_DATE")]
        public DateTime? AnswerDate {
            get { return _answerDate; }
            set {
                __modifiedProperties.AddPropertyName("AnswerDate");
                _answerDate = value;
            }
        }

        /// <summary>SEX: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("SEX")]
        public String Sex {
            get { return _sex; }
            set {
                __modifiedProperties.AddPropertyName("Sex");
                _sex = value;
            }
        }

        /// <summary>AGE: {NUMBER(3)}</summary>
        [Seasar.Dao.Attrs.Column("AGE")]
        public int? Age {
            get { return _age; }
            set {
                __modifiedProperties.AddPropertyName("Age");
                _age = value;
            }
        }

        /// <summary>AGE_ID: {CHAR(2)}</summary>
        [Seasar.Dao.Attrs.Column("AGE_ID")]
        public String AgeId {
            get { return _ageId; }
            set {
                __modifiedProperties.AddPropertyName("AgeId");
                _ageId = value;
            }
        }

        /// <summary>PREFECTURE: {CHAR(2)}</summary>
        [Seasar.Dao.Attrs.Column("PREFECTURE")]
        public String Prefecture {
            get { return _prefecture; }
            set {
                __modifiedProperties.AddPropertyName("Prefecture");
                _prefecture = value;
            }
        }

        /// <summary>AREA: {CHAR(2)}</summary>
        [Seasar.Dao.Attrs.Column("AREA")]
        public String Area {
            get { return _area; }
            set {
                __modifiedProperties.AddPropertyName("Area");
                _area = value;
            }
        }

        /// <summary>MARRIED: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("MARRIED")]
        public String Married {
            get { return _married; }
            set {
                __modifiedProperties.AddPropertyName("Married");
                _married = value;
            }
        }

        /// <summary>CHILD: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("CHILD")]
        public String Child {
            get { return _child; }
            set {
                __modifiedProperties.AddPropertyName("Child");
                _child = value;
            }
        }

        /// <summary>HINCOME: {CHAR(2)}</summary>
        [Seasar.Dao.Attrs.Column("HINCOME")]
        public String Hincome {
            get { return _hincome; }
            set {
                __modifiedProperties.AddPropertyName("Hincome");
                _hincome = value;
            }
        }

        /// <summary>PINCOME: {CHAR(2)}</summary>
        [Seasar.Dao.Attrs.Column("PINCOME")]
        public String Pincome {
            get { return _pincome; }
            set {
                __modifiedProperties.AddPropertyName("Pincome");
                _pincome = value;
            }
        }

        /// <summary>JOB: {CHAR(2)}</summary>
        [Seasar.Dao.Attrs.Column("JOB")]
        public String Job {
            get { return _job; }
            set {
                __modifiedProperties.AddPropertyName("Job");
                _job = value;
            }
        }

        /// <summary>STUDENT: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("STUDENT")]
        public String Student {
            get { return _student; }
            set {
                __modifiedProperties.AddPropertyName("Student");
                _student = value;
            }
        }

        /// <summary>CELL: {VARCHAR2(20)}</summary>
        [Seasar.Dao.Attrs.Column("CELL")]
        public String Cell {
            get { return _cell; }
            set {
                __modifiedProperties.AddPropertyName("Cell");
                _cell = value;
            }
        }

        /// <summary>CELL_NAME: {NVARCHAR2(200)}</summary>
        [Seasar.Dao.Attrs.Column("CELL_NAME")]
        public String CellName {
            get { return _cellName; }
            set {
                __modifiedProperties.AddPropertyName("CellName");
                _cellName = value;
            }
        }

        /// <summary>Q0001: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0001")]
        public String Q0001 {
            get { return _q0001; }
            set {
                __modifiedProperties.AddPropertyName("Q0001");
                _q0001 = value;
            }
        }

        /// <summary>Q0002: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0002")]
        public String Q0002 {
            get { return _q0002; }
            set {
                __modifiedProperties.AddPropertyName("Q0002");
                _q0002 = value;
            }
        }

        /// <summary>Q0003: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0003")]
        public String Q0003 {
            get { return _q0003; }
            set {
                __modifiedProperties.AddPropertyName("Q0003");
                _q0003 = value;
            }
        }

        /// <summary>Q0004: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0004")]
        public String Q0004 {
            get { return _q0004; }
            set {
                __modifiedProperties.AddPropertyName("Q0004");
                _q0004 = value;
            }
        }

        /// <summary>Q0005: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0005")]
        public String Q0005 {
            get { return _q0005; }
            set {
                __modifiedProperties.AddPropertyName("Q0005");
                _q0005 = value;
            }
        }

        /// <summary>Q0006: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0006")]
        public String Q0006 {
            get { return _q0006; }
            set {
                __modifiedProperties.AddPropertyName("Q0006");
                _q0006 = value;
            }
        }

        /// <summary>Q0007: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0007")]
        public String Q0007 {
            get { return _q0007; }
            set {
                __modifiedProperties.AddPropertyName("Q0007");
                _q0007 = value;
            }
        }

        /// <summary>Q0008: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0008")]
        public String Q0008 {
            get { return _q0008; }
            set {
                __modifiedProperties.AddPropertyName("Q0008");
                _q0008 = value;
            }
        }

        /// <summary>Q0009: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0009")]
        public String Q0009 {
            get { return _q0009; }
            set {
                __modifiedProperties.AddPropertyName("Q0009");
                _q0009 = value;
            }
        }

        /// <summary>Q0010: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0010")]
        public String Q0010 {
            get { return _q0010; }
            set {
                __modifiedProperties.AddPropertyName("Q0010");
                _q0010 = value;
            }
        }

        /// <summary>Q0011: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0011")]
        public String Q0011 {
            get { return _q0011; }
            set {
                __modifiedProperties.AddPropertyName("Q0011");
                _q0011 = value;
            }
        }

        /// <summary>Q0012: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0012")]
        public String Q0012 {
            get { return _q0012; }
            set {
                __modifiedProperties.AddPropertyName("Q0012");
                _q0012 = value;
            }
        }

        /// <summary>Q0013: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0013")]
        public String Q0013 {
            get { return _q0013; }
            set {
                __modifiedProperties.AddPropertyName("Q0013");
                _q0013 = value;
            }
        }

        /// <summary>Q0014: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0014")]
        public String Q0014 {
            get { return _q0014; }
            set {
                __modifiedProperties.AddPropertyName("Q0014");
                _q0014 = value;
            }
        }

        /// <summary>Q0015: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0015")]
        public String Q0015 {
            get { return _q0015; }
            set {
                __modifiedProperties.AddPropertyName("Q0015");
                _q0015 = value;
            }
        }

        /// <summary>Q0016: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0016")]
        public String Q0016 {
            get { return _q0016; }
            set {
                __modifiedProperties.AddPropertyName("Q0016");
                _q0016 = value;
            }
        }

        /// <summary>Q0017: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0017")]
        public String Q0017 {
            get { return _q0017; }
            set {
                __modifiedProperties.AddPropertyName("Q0017");
                _q0017 = value;
            }
        }

        /// <summary>Q0018: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0018")]
        public String Q0018 {
            get { return _q0018; }
            set {
                __modifiedProperties.AddPropertyName("Q0018");
                _q0018 = value;
            }
        }

        /// <summary>Q0019: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0019")]
        public String Q0019 {
            get { return _q0019; }
            set {
                __modifiedProperties.AddPropertyName("Q0019");
                _q0019 = value;
            }
        }

        /// <summary>Q0020: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0020")]
        public String Q0020 {
            get { return _q0020; }
            set {
                __modifiedProperties.AddPropertyName("Q0020");
                _q0020 = value;
            }
        }

        /// <summary>Q0021: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0021")]
        public String Q0021 {
            get { return _q0021; }
            set {
                __modifiedProperties.AddPropertyName("Q0021");
                _q0021 = value;
            }
        }

        /// <summary>Q0022: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0022")]
        public String Q0022 {
            get { return _q0022; }
            set {
                __modifiedProperties.AddPropertyName("Q0022");
                _q0022 = value;
            }
        }

        /// <summary>Q0023: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0023")]
        public String Q0023 {
            get { return _q0023; }
            set {
                __modifiedProperties.AddPropertyName("Q0023");
                _q0023 = value;
            }
        }

        /// <summary>Q0024: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0024")]
        public String Q0024 {
            get { return _q0024; }
            set {
                __modifiedProperties.AddPropertyName("Q0024");
                _q0024 = value;
            }
        }

        /// <summary>Q0025: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0025")]
        public String Q0025 {
            get { return _q0025; }
            set {
                __modifiedProperties.AddPropertyName("Q0025");
                _q0025 = value;
            }
        }

        /// <summary>Q0026: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0026")]
        public String Q0026 {
            get { return _q0026; }
            set {
                __modifiedProperties.AddPropertyName("Q0026");
                _q0026 = value;
            }
        }

        /// <summary>Q0027: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0027")]
        public String Q0027 {
            get { return _q0027; }
            set {
                __modifiedProperties.AddPropertyName("Q0027");
                _q0027 = value;
            }
        }

        /// <summary>Q0028: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0028")]
        public String Q0028 {
            get { return _q0028; }
            set {
                __modifiedProperties.AddPropertyName("Q0028");
                _q0028 = value;
            }
        }

        /// <summary>Q0029: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0029")]
        public String Q0029 {
            get { return _q0029; }
            set {
                __modifiedProperties.AddPropertyName("Q0029");
                _q0029 = value;
            }
        }

        /// <summary>Q0030: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0030")]
        public String Q0030 {
            get { return _q0030; }
            set {
                __modifiedProperties.AddPropertyName("Q0030");
                _q0030 = value;
            }
        }

        /// <summary>Q0031: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0031")]
        public String Q0031 {
            get { return _q0031; }
            set {
                __modifiedProperties.AddPropertyName("Q0031");
                _q0031 = value;
            }
        }

        /// <summary>Q0032: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0032")]
        public String Q0032 {
            get { return _q0032; }
            set {
                __modifiedProperties.AddPropertyName("Q0032");
                _q0032 = value;
            }
        }

        /// <summary>Q0033: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0033")]
        public String Q0033 {
            get { return _q0033; }
            set {
                __modifiedProperties.AddPropertyName("Q0033");
                _q0033 = value;
            }
        }

        /// <summary>Q0034: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0034")]
        public String Q0034 {
            get { return _q0034; }
            set {
                __modifiedProperties.AddPropertyName("Q0034");
                _q0034 = value;
            }
        }

        /// <summary>Q0035: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0035")]
        public String Q0035 {
            get { return _q0035; }
            set {
                __modifiedProperties.AddPropertyName("Q0035");
                _q0035 = value;
            }
        }

        /// <summary>Q0036: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0036")]
        public String Q0036 {
            get { return _q0036; }
            set {
                __modifiedProperties.AddPropertyName("Q0036");
                _q0036 = value;
            }
        }

        /// <summary>Q0037: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0037")]
        public String Q0037 {
            get { return _q0037; }
            set {
                __modifiedProperties.AddPropertyName("Q0037");
                _q0037 = value;
            }
        }

        /// <summary>Q0038: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0038")]
        public String Q0038 {
            get { return _q0038; }
            set {
                __modifiedProperties.AddPropertyName("Q0038");
                _q0038 = value;
            }
        }

        /// <summary>Q0039: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0039")]
        public String Q0039 {
            get { return _q0039; }
            set {
                __modifiedProperties.AddPropertyName("Q0039");
                _q0039 = value;
            }
        }

        /// <summary>Q0040: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0040")]
        public String Q0040 {
            get { return _q0040; }
            set {
                __modifiedProperties.AddPropertyName("Q0040");
                _q0040 = value;
            }
        }

        /// <summary>Q0041: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0041")]
        public String Q0041 {
            get { return _q0041; }
            set {
                __modifiedProperties.AddPropertyName("Q0041");
                _q0041 = value;
            }
        }

        /// <summary>Q0042: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0042")]
        public String Q0042 {
            get { return _q0042; }
            set {
                __modifiedProperties.AddPropertyName("Q0042");
                _q0042 = value;
            }
        }

        /// <summary>Q0043: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0043")]
        public String Q0043 {
            get { return _q0043; }
            set {
                __modifiedProperties.AddPropertyName("Q0043");
                _q0043 = value;
            }
        }

        /// <summary>Q0044: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0044")]
        public String Q0044 {
            get { return _q0044; }
            set {
                __modifiedProperties.AddPropertyName("Q0044");
                _q0044 = value;
            }
        }

        /// <summary>Q0045: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0045")]
        public String Q0045 {
            get { return _q0045; }
            set {
                __modifiedProperties.AddPropertyName("Q0045");
                _q0045 = value;
            }
        }

        /// <summary>Q0046: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0046")]
        public String Q0046 {
            get { return _q0046; }
            set {
                __modifiedProperties.AddPropertyName("Q0046");
                _q0046 = value;
            }
        }

        /// <summary>Q0047: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0047")]
        public String Q0047 {
            get { return _q0047; }
            set {
                __modifiedProperties.AddPropertyName("Q0047");
                _q0047 = value;
            }
        }

        /// <summary>Q0048: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0048")]
        public String Q0048 {
            get { return _q0048; }
            set {
                __modifiedProperties.AddPropertyName("Q0048");
                _q0048 = value;
            }
        }

        /// <summary>Q0049: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0049")]
        public String Q0049 {
            get { return _q0049; }
            set {
                __modifiedProperties.AddPropertyName("Q0049");
                _q0049 = value;
            }
        }

        /// <summary>Q0050: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0050")]
        public String Q0050 {
            get { return _q0050; }
            set {
                __modifiedProperties.AddPropertyName("Q0050");
                _q0050 = value;
            }
        }

        /// <summary>Q0051: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0051")]
        public String Q0051 {
            get { return _q0051; }
            set {
                __modifiedProperties.AddPropertyName("Q0051");
                _q0051 = value;
            }
        }

        /// <summary>Q0052: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0052")]
        public String Q0052 {
            get { return _q0052; }
            set {
                __modifiedProperties.AddPropertyName("Q0052");
                _q0052 = value;
            }
        }

        /// <summary>Q0053: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0053")]
        public String Q0053 {
            get { return _q0053; }
            set {
                __modifiedProperties.AddPropertyName("Q0053");
                _q0053 = value;
            }
        }

        /// <summary>Q0054: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0054")]
        public String Q0054 {
            get { return _q0054; }
            set {
                __modifiedProperties.AddPropertyName("Q0054");
                _q0054 = value;
            }
        }

        /// <summary>Q0055: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0055")]
        public String Q0055 {
            get { return _q0055; }
            set {
                __modifiedProperties.AddPropertyName("Q0055");
                _q0055 = value;
            }
        }

        /// <summary>Q0056: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0056")]
        public String Q0056 {
            get { return _q0056; }
            set {
                __modifiedProperties.AddPropertyName("Q0056");
                _q0056 = value;
            }
        }

        /// <summary>Q0057: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0057")]
        public String Q0057 {
            get { return _q0057; }
            set {
                __modifiedProperties.AddPropertyName("Q0057");
                _q0057 = value;
            }
        }

        /// <summary>Q0058: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0058")]
        public String Q0058 {
            get { return _q0058; }
            set {
                __modifiedProperties.AddPropertyName("Q0058");
                _q0058 = value;
            }
        }

        /// <summary>Q0059: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0059")]
        public String Q0059 {
            get { return _q0059; }
            set {
                __modifiedProperties.AddPropertyName("Q0059");
                _q0059 = value;
            }
        }

        /// <summary>Q0060: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0060")]
        public String Q0060 {
            get { return _q0060; }
            set {
                __modifiedProperties.AddPropertyName("Q0060");
                _q0060 = value;
            }
        }

        /// <summary>Q0061: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0061")]
        public String Q0061 {
            get { return _q0061; }
            set {
                __modifiedProperties.AddPropertyName("Q0061");
                _q0061 = value;
            }
        }

        /// <summary>Q0062: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0062")]
        public String Q0062 {
            get { return _q0062; }
            set {
                __modifiedProperties.AddPropertyName("Q0062");
                _q0062 = value;
            }
        }

        /// <summary>Q0063: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0063")]
        public String Q0063 {
            get { return _q0063; }
            set {
                __modifiedProperties.AddPropertyName("Q0063");
                _q0063 = value;
            }
        }

        /// <summary>Q0064: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0064")]
        public String Q0064 {
            get { return _q0064; }
            set {
                __modifiedProperties.AddPropertyName("Q0064");
                _q0064 = value;
            }
        }

        /// <summary>Q0065: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0065")]
        public String Q0065 {
            get { return _q0065; }
            set {
                __modifiedProperties.AddPropertyName("Q0065");
                _q0065 = value;
            }
        }

        /// <summary>Q0066: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0066")]
        public String Q0066 {
            get { return _q0066; }
            set {
                __modifiedProperties.AddPropertyName("Q0066");
                _q0066 = value;
            }
        }

        /// <summary>Q0067: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0067")]
        public String Q0067 {
            get { return _q0067; }
            set {
                __modifiedProperties.AddPropertyName("Q0067");
                _q0067 = value;
            }
        }

        /// <summary>Q0068: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0068")]
        public String Q0068 {
            get { return _q0068; }
            set {
                __modifiedProperties.AddPropertyName("Q0068");
                _q0068 = value;
            }
        }

        /// <summary>Q0069: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0069")]
        public String Q0069 {
            get { return _q0069; }
            set {
                __modifiedProperties.AddPropertyName("Q0069");
                _q0069 = value;
            }
        }

        /// <summary>Q0070: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0070")]
        public String Q0070 {
            get { return _q0070; }
            set {
                __modifiedProperties.AddPropertyName("Q0070");
                _q0070 = value;
            }
        }

        /// <summary>Q0071: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0071")]
        public String Q0071 {
            get { return _q0071; }
            set {
                __modifiedProperties.AddPropertyName("Q0071");
                _q0071 = value;
            }
        }

        /// <summary>Q0072: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0072")]
        public String Q0072 {
            get { return _q0072; }
            set {
                __modifiedProperties.AddPropertyName("Q0072");
                _q0072 = value;
            }
        }

        /// <summary>Q0073: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0073")]
        public String Q0073 {
            get { return _q0073; }
            set {
                __modifiedProperties.AddPropertyName("Q0073");
                _q0073 = value;
            }
        }

        /// <summary>Q0074: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0074")]
        public String Q0074 {
            get { return _q0074; }
            set {
                __modifiedProperties.AddPropertyName("Q0074");
                _q0074 = value;
            }
        }

        /// <summary>Q0075: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0075")]
        public String Q0075 {
            get { return _q0075; }
            set {
                __modifiedProperties.AddPropertyName("Q0075");
                _q0075 = value;
            }
        }

        /// <summary>Q0076: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0076")]
        public String Q0076 {
            get { return _q0076; }
            set {
                __modifiedProperties.AddPropertyName("Q0076");
                _q0076 = value;
            }
        }

        /// <summary>Q0077: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0077")]
        public String Q0077 {
            get { return _q0077; }
            set {
                __modifiedProperties.AddPropertyName("Q0077");
                _q0077 = value;
            }
        }

        /// <summary>Q0078: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0078")]
        public String Q0078 {
            get { return _q0078; }
            set {
                __modifiedProperties.AddPropertyName("Q0078");
                _q0078 = value;
            }
        }

        /// <summary>Q0079: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0079")]
        public String Q0079 {
            get { return _q0079; }
            set {
                __modifiedProperties.AddPropertyName("Q0079");
                _q0079 = value;
            }
        }

        /// <summary>Q0080: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0080")]
        public String Q0080 {
            get { return _q0080; }
            set {
                __modifiedProperties.AddPropertyName("Q0080");
                _q0080 = value;
            }
        }

        /// <summary>Q0081: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0081")]
        public String Q0081 {
            get { return _q0081; }
            set {
                __modifiedProperties.AddPropertyName("Q0081");
                _q0081 = value;
            }
        }

        /// <summary>Q0082: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0082")]
        public String Q0082 {
            get { return _q0082; }
            set {
                __modifiedProperties.AddPropertyName("Q0082");
                _q0082 = value;
            }
        }

        /// <summary>Q0083: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0083")]
        public String Q0083 {
            get { return _q0083; }
            set {
                __modifiedProperties.AddPropertyName("Q0083");
                _q0083 = value;
            }
        }

        /// <summary>Q0084: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0084")]
        public String Q0084 {
            get { return _q0084; }
            set {
                __modifiedProperties.AddPropertyName("Q0084");
                _q0084 = value;
            }
        }

        /// <summary>Q0085: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0085")]
        public String Q0085 {
            get { return _q0085; }
            set {
                __modifiedProperties.AddPropertyName("Q0085");
                _q0085 = value;
            }
        }

        /// <summary>Q0086: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0086")]
        public String Q0086 {
            get { return _q0086; }
            set {
                __modifiedProperties.AddPropertyName("Q0086");
                _q0086 = value;
            }
        }

        /// <summary>Q0087: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0087")]
        public String Q0087 {
            get { return _q0087; }
            set {
                __modifiedProperties.AddPropertyName("Q0087");
                _q0087 = value;
            }
        }

        /// <summary>Q0088: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0088")]
        public String Q0088 {
            get { return _q0088; }
            set {
                __modifiedProperties.AddPropertyName("Q0088");
                _q0088 = value;
            }
        }

        /// <summary>Q0089: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0089")]
        public String Q0089 {
            get { return _q0089; }
            set {
                __modifiedProperties.AddPropertyName("Q0089");
                _q0089 = value;
            }
        }

        /// <summary>Q0090: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0090")]
        public String Q0090 {
            get { return _q0090; }
            set {
                __modifiedProperties.AddPropertyName("Q0090");
                _q0090 = value;
            }
        }

        /// <summary>Q0091: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0091")]
        public String Q0091 {
            get { return _q0091; }
            set {
                __modifiedProperties.AddPropertyName("Q0091");
                _q0091 = value;
            }
        }

        /// <summary>Q0092: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0092")]
        public String Q0092 {
            get { return _q0092; }
            set {
                __modifiedProperties.AddPropertyName("Q0092");
                _q0092 = value;
            }
        }

        /// <summary>Q0093: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0093")]
        public String Q0093 {
            get { return _q0093; }
            set {
                __modifiedProperties.AddPropertyName("Q0093");
                _q0093 = value;
            }
        }

        /// <summary>Q0094: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0094")]
        public String Q0094 {
            get { return _q0094; }
            set {
                __modifiedProperties.AddPropertyName("Q0094");
                _q0094 = value;
            }
        }

        /// <summary>Q0095: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0095")]
        public String Q0095 {
            get { return _q0095; }
            set {
                __modifiedProperties.AddPropertyName("Q0095");
                _q0095 = value;
            }
        }

        /// <summary>Q0096: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0096")]
        public String Q0096 {
            get { return _q0096; }
            set {
                __modifiedProperties.AddPropertyName("Q0096");
                _q0096 = value;
            }
        }

        /// <summary>Q0097: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0097")]
        public String Q0097 {
            get { return _q0097; }
            set {
                __modifiedProperties.AddPropertyName("Q0097");
                _q0097 = value;
            }
        }

        /// <summary>Q0098: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0098")]
        public String Q0098 {
            get { return _q0098; }
            set {
                __modifiedProperties.AddPropertyName("Q0098");
                _q0098 = value;
            }
        }

        /// <summary>Q0099: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0099")]
        public String Q0099 {
            get { return _q0099; }
            set {
                __modifiedProperties.AddPropertyName("Q0099");
                _q0099 = value;
            }
        }

        /// <summary>Q0100: {VARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("Q0100")]
        public String Q0100 {
            get { return _q0100; }
            set {
                __modifiedProperties.AddPropertyName("Q0100");
                _q0100 = value;
            }
        }

        #endregion
    }
}
