

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
    /// The entity of T_FA_DATA as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     SAMPLE_ID
    /// 
    /// [column]
    ///     SAMPLE_ID, FA001, FA002, FA003, FA004, FA005, FA006, FA007, FA008, FA009, FA010, FA011, FA012, FA013, FA014, FA015, FA016, FA017, FA018, FA019, FA020, FA021, FA022, FA023, FA024, FA025, FA026, FA027, FA028, FA029, FA030, FA031, FA032, FA033, FA034, FA035, FA036, FA037, FA038, FA039, FA040, FA041, FA042, FA043, FA044, FA045, FA046, FA047, FA048, FA049, FA050, FA051, FA052, FA053, FA054, FA055, FA056, FA057, FA058, FA059, FA060, FA061, FA062, FA063, FA064, FA065, FA066, FA067, FA068, FA069, FA070, FA071, FA072, FA073, FA074, FA075, FA076, FA077, FA078, FA079, FA080, FA081, FA082, FA083, FA084, FA085, FA086, FA087, FA088, FA089, FA090, FA091, FA092, FA093, FA094, FA095, FA096, FA097, FA098, FA099, FA100, FA101, FA102, FA103, FA104, FA105, FA106, FA107, FA108, FA109, FA110, FA111, FA112, FA113, FA114, FA115, FA116, FA117, FA118, FA119, FA120, FA121, FA122, FA123, FA124, FA125, FA126, FA127, FA128, FA129, FA130, FA131, FA132, FA133, FA134, FA135, FA136, FA137, FA138, FA139, FA140, FA141, FA142, FA143, FA144, FA145, FA146, FA147, FA148, FA149, FA150, FA151, FA152, FA153, FA154, FA155, FA156, FA157, FA158, FA159, FA160, FA161, FA162, FA163, FA164, FA165, FA166, FA167, FA168, FA169, FA170, FA171, FA172, FA173, FA174, FA175, FA176, FA177, FA178, FA179, FA180, FA181, FA182, FA183, FA184, FA185, FA186, FA187, FA188, FA189, FA190, FA191, FA192, FA193, FA194, FA195, FA196, FA197, FA198, FA199, FA200
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
    [Seasar.Dao.Attrs.Table("T_FA_DATA")]
    [System.Serializable]
    public partial class TFaData : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SAMPLE_ID: {PK, NotNull, VARCHAR2(10)}</summary>
        protected String _sampleId;

        /// <summary>FA001: {NVARCHAR2(2000)}</summary>
        protected String _fa001;

        /// <summary>FA002: {NVARCHAR2(2000)}</summary>
        protected String _fa002;

        /// <summary>FA003: {NVARCHAR2(2000)}</summary>
        protected String _fa003;

        /// <summary>FA004: {NVARCHAR2(2000)}</summary>
        protected String _fa004;

        /// <summary>FA005: {NVARCHAR2(2000)}</summary>
        protected String _fa005;

        /// <summary>FA006: {NVARCHAR2(2000)}</summary>
        protected String _fa006;

        /// <summary>FA007: {NVARCHAR2(2000)}</summary>
        protected String _fa007;

        /// <summary>FA008: {NVARCHAR2(2000)}</summary>
        protected String _fa008;

        /// <summary>FA009: {NVARCHAR2(2000)}</summary>
        protected String _fa009;

        /// <summary>FA010: {NVARCHAR2(2000)}</summary>
        protected String _fa010;

        /// <summary>FA011: {NVARCHAR2(2000)}</summary>
        protected String _fa011;

        /// <summary>FA012: {NVARCHAR2(2000)}</summary>
        protected String _fa012;

        /// <summary>FA013: {NVARCHAR2(2000)}</summary>
        protected String _fa013;

        /// <summary>FA014: {NVARCHAR2(2000)}</summary>
        protected String _fa014;

        /// <summary>FA015: {NVARCHAR2(2000)}</summary>
        protected String _fa015;

        /// <summary>FA016: {NVARCHAR2(2000)}</summary>
        protected String _fa016;

        /// <summary>FA017: {NVARCHAR2(2000)}</summary>
        protected String _fa017;

        /// <summary>FA018: {NVARCHAR2(2000)}</summary>
        protected String _fa018;

        /// <summary>FA019: {NVARCHAR2(2000)}</summary>
        protected String _fa019;

        /// <summary>FA020: {NVARCHAR2(2000)}</summary>
        protected String _fa020;

        /// <summary>FA021: {NVARCHAR2(2000)}</summary>
        protected String _fa021;

        /// <summary>FA022: {NVARCHAR2(2000)}</summary>
        protected String _fa022;

        /// <summary>FA023: {NVARCHAR2(2000)}</summary>
        protected String _fa023;

        /// <summary>FA024: {NVARCHAR2(2000)}</summary>
        protected String _fa024;

        /// <summary>FA025: {NVARCHAR2(2000)}</summary>
        protected String _fa025;

        /// <summary>FA026: {NVARCHAR2(2000)}</summary>
        protected String _fa026;

        /// <summary>FA027: {NVARCHAR2(2000)}</summary>
        protected String _fa027;

        /// <summary>FA028: {NVARCHAR2(2000)}</summary>
        protected String _fa028;

        /// <summary>FA029: {NVARCHAR2(2000)}</summary>
        protected String _fa029;

        /// <summary>FA030: {NVARCHAR2(2000)}</summary>
        protected String _fa030;

        /// <summary>FA031: {NVARCHAR2(2000)}</summary>
        protected String _fa031;

        /// <summary>FA032: {NVARCHAR2(2000)}</summary>
        protected String _fa032;

        /// <summary>FA033: {NVARCHAR2(2000)}</summary>
        protected String _fa033;

        /// <summary>FA034: {NVARCHAR2(2000)}</summary>
        protected String _fa034;

        /// <summary>FA035: {NVARCHAR2(2000)}</summary>
        protected String _fa035;

        /// <summary>FA036: {NVARCHAR2(2000)}</summary>
        protected String _fa036;

        /// <summary>FA037: {NVARCHAR2(2000)}</summary>
        protected String _fa037;

        /// <summary>FA038: {NVARCHAR2(2000)}</summary>
        protected String _fa038;

        /// <summary>FA039: {NVARCHAR2(2000)}</summary>
        protected String _fa039;

        /// <summary>FA040: {NVARCHAR2(2000)}</summary>
        protected String _fa040;

        /// <summary>FA041: {NVARCHAR2(2000)}</summary>
        protected String _fa041;

        /// <summary>FA042: {NVARCHAR2(2000)}</summary>
        protected String _fa042;

        /// <summary>FA043: {NVARCHAR2(2000)}</summary>
        protected String _fa043;

        /// <summary>FA044: {NVARCHAR2(2000)}</summary>
        protected String _fa044;

        /// <summary>FA045: {NVARCHAR2(2000)}</summary>
        protected String _fa045;

        /// <summary>FA046: {NVARCHAR2(2000)}</summary>
        protected String _fa046;

        /// <summary>FA047: {NVARCHAR2(2000)}</summary>
        protected String _fa047;

        /// <summary>FA048: {NVARCHAR2(2000)}</summary>
        protected String _fa048;

        /// <summary>FA049: {NVARCHAR2(2000)}</summary>
        protected String _fa049;

        /// <summary>FA050: {NVARCHAR2(2000)}</summary>
        protected String _fa050;

        /// <summary>FA051: {NVARCHAR2(2000)}</summary>
        protected String _fa051;

        /// <summary>FA052: {NVARCHAR2(2000)}</summary>
        protected String _fa052;

        /// <summary>FA053: {NVARCHAR2(2000)}</summary>
        protected String _fa053;

        /// <summary>FA054: {NVARCHAR2(2000)}</summary>
        protected String _fa054;

        /// <summary>FA055: {NVARCHAR2(2000)}</summary>
        protected String _fa055;

        /// <summary>FA056: {NVARCHAR2(2000)}</summary>
        protected String _fa056;

        /// <summary>FA057: {NVARCHAR2(2000)}</summary>
        protected String _fa057;

        /// <summary>FA058: {NVARCHAR2(2000)}</summary>
        protected String _fa058;

        /// <summary>FA059: {NVARCHAR2(2000)}</summary>
        protected String _fa059;

        /// <summary>FA060: {NVARCHAR2(2000)}</summary>
        protected String _fa060;

        /// <summary>FA061: {NVARCHAR2(2000)}</summary>
        protected String _fa061;

        /// <summary>FA062: {NVARCHAR2(2000)}</summary>
        protected String _fa062;

        /// <summary>FA063: {NVARCHAR2(2000)}</summary>
        protected String _fa063;

        /// <summary>FA064: {NVARCHAR2(2000)}</summary>
        protected String _fa064;

        /// <summary>FA065: {NVARCHAR2(2000)}</summary>
        protected String _fa065;

        /// <summary>FA066: {NVARCHAR2(2000)}</summary>
        protected String _fa066;

        /// <summary>FA067: {NVARCHAR2(2000)}</summary>
        protected String _fa067;

        /// <summary>FA068: {NVARCHAR2(2000)}</summary>
        protected String _fa068;

        /// <summary>FA069: {NVARCHAR2(2000)}</summary>
        protected String _fa069;

        /// <summary>FA070: {NVARCHAR2(2000)}</summary>
        protected String _fa070;

        /// <summary>FA071: {NVARCHAR2(2000)}</summary>
        protected String _fa071;

        /// <summary>FA072: {NVARCHAR2(2000)}</summary>
        protected String _fa072;

        /// <summary>FA073: {NVARCHAR2(2000)}</summary>
        protected String _fa073;

        /// <summary>FA074: {NVARCHAR2(2000)}</summary>
        protected String _fa074;

        /// <summary>FA075: {NVARCHAR2(2000)}</summary>
        protected String _fa075;

        /// <summary>FA076: {NVARCHAR2(2000)}</summary>
        protected String _fa076;

        /// <summary>FA077: {NVARCHAR2(2000)}</summary>
        protected String _fa077;

        /// <summary>FA078: {NVARCHAR2(2000)}</summary>
        protected String _fa078;

        /// <summary>FA079: {NVARCHAR2(2000)}</summary>
        protected String _fa079;

        /// <summary>FA080: {NVARCHAR2(2000)}</summary>
        protected String _fa080;

        /// <summary>FA081: {NVARCHAR2(2000)}</summary>
        protected String _fa081;

        /// <summary>FA082: {NVARCHAR2(2000)}</summary>
        protected String _fa082;

        /// <summary>FA083: {NVARCHAR2(2000)}</summary>
        protected String _fa083;

        /// <summary>FA084: {NVARCHAR2(2000)}</summary>
        protected String _fa084;

        /// <summary>FA085: {NVARCHAR2(2000)}</summary>
        protected String _fa085;

        /// <summary>FA086: {NVARCHAR2(2000)}</summary>
        protected String _fa086;

        /// <summary>FA087: {NVARCHAR2(2000)}</summary>
        protected String _fa087;

        /// <summary>FA088: {NVARCHAR2(2000)}</summary>
        protected String _fa088;

        /// <summary>FA089: {NVARCHAR2(2000)}</summary>
        protected String _fa089;

        /// <summary>FA090: {NVARCHAR2(2000)}</summary>
        protected String _fa090;

        /// <summary>FA091: {NVARCHAR2(2000)}</summary>
        protected String _fa091;

        /// <summary>FA092: {NVARCHAR2(2000)}</summary>
        protected String _fa092;

        /// <summary>FA093: {NVARCHAR2(2000)}</summary>
        protected String _fa093;

        /// <summary>FA094: {NVARCHAR2(2000)}</summary>
        protected String _fa094;

        /// <summary>FA095: {NVARCHAR2(2000)}</summary>
        protected String _fa095;

        /// <summary>FA096: {NVARCHAR2(2000)}</summary>
        protected String _fa096;

        /// <summary>FA097: {NVARCHAR2(2000)}</summary>
        protected String _fa097;

        /// <summary>FA098: {NVARCHAR2(2000)}</summary>
        protected String _fa098;

        /// <summary>FA099: {NVARCHAR2(2000)}</summary>
        protected String _fa099;

        /// <summary>FA100: {NVARCHAR2(2000)}</summary>
        protected String _fa100;

        /// <summary>FA101: {NVARCHAR2(2000)}</summary>
        protected String _fa101;

        /// <summary>FA102: {NVARCHAR2(2000)}</summary>
        protected String _fa102;

        /// <summary>FA103: {NVARCHAR2(2000)}</summary>
        protected String _fa103;

        /// <summary>FA104: {NVARCHAR2(2000)}</summary>
        protected String _fa104;

        /// <summary>FA105: {NVARCHAR2(2000)}</summary>
        protected String _fa105;

        /// <summary>FA106: {NVARCHAR2(2000)}</summary>
        protected String _fa106;

        /// <summary>FA107: {NVARCHAR2(2000)}</summary>
        protected String _fa107;

        /// <summary>FA108: {NVARCHAR2(2000)}</summary>
        protected String _fa108;

        /// <summary>FA109: {NVARCHAR2(2000)}</summary>
        protected String _fa109;

        /// <summary>FA110: {NVARCHAR2(2000)}</summary>
        protected String _fa110;

        /// <summary>FA111: {NVARCHAR2(2000)}</summary>
        protected String _fa111;

        /// <summary>FA112: {NVARCHAR2(2000)}</summary>
        protected String _fa112;

        /// <summary>FA113: {NVARCHAR2(2000)}</summary>
        protected String _fa113;

        /// <summary>FA114: {NVARCHAR2(2000)}</summary>
        protected String _fa114;

        /// <summary>FA115: {NVARCHAR2(2000)}</summary>
        protected String _fa115;

        /// <summary>FA116: {NVARCHAR2(2000)}</summary>
        protected String _fa116;

        /// <summary>FA117: {NVARCHAR2(2000)}</summary>
        protected String _fa117;

        /// <summary>FA118: {NVARCHAR2(2000)}</summary>
        protected String _fa118;

        /// <summary>FA119: {NVARCHAR2(2000)}</summary>
        protected String _fa119;

        /// <summary>FA120: {NVARCHAR2(2000)}</summary>
        protected String _fa120;

        /// <summary>FA121: {NVARCHAR2(2000)}</summary>
        protected String _fa121;

        /// <summary>FA122: {NVARCHAR2(2000)}</summary>
        protected String _fa122;

        /// <summary>FA123: {NVARCHAR2(2000)}</summary>
        protected String _fa123;

        /// <summary>FA124: {NVARCHAR2(2000)}</summary>
        protected String _fa124;

        /// <summary>FA125: {NVARCHAR2(2000)}</summary>
        protected String _fa125;

        /// <summary>FA126: {NVARCHAR2(2000)}</summary>
        protected String _fa126;

        /// <summary>FA127: {NVARCHAR2(2000)}</summary>
        protected String _fa127;

        /// <summary>FA128: {NVARCHAR2(2000)}</summary>
        protected String _fa128;

        /// <summary>FA129: {NVARCHAR2(2000)}</summary>
        protected String _fa129;

        /// <summary>FA130: {NVARCHAR2(2000)}</summary>
        protected String _fa130;

        /// <summary>FA131: {NVARCHAR2(2000)}</summary>
        protected String _fa131;

        /// <summary>FA132: {NVARCHAR2(2000)}</summary>
        protected String _fa132;

        /// <summary>FA133: {NVARCHAR2(2000)}</summary>
        protected String _fa133;

        /// <summary>FA134: {NVARCHAR2(2000)}</summary>
        protected String _fa134;

        /// <summary>FA135: {NVARCHAR2(2000)}</summary>
        protected String _fa135;

        /// <summary>FA136: {NVARCHAR2(2000)}</summary>
        protected String _fa136;

        /// <summary>FA137: {NVARCHAR2(2000)}</summary>
        protected String _fa137;

        /// <summary>FA138: {NVARCHAR2(2000)}</summary>
        protected String _fa138;

        /// <summary>FA139: {NVARCHAR2(2000)}</summary>
        protected String _fa139;

        /// <summary>FA140: {NVARCHAR2(2000)}</summary>
        protected String _fa140;

        /// <summary>FA141: {NVARCHAR2(2000)}</summary>
        protected String _fa141;

        /// <summary>FA142: {NVARCHAR2(2000)}</summary>
        protected String _fa142;

        /// <summary>FA143: {NVARCHAR2(2000)}</summary>
        protected String _fa143;

        /// <summary>FA144: {NVARCHAR2(2000)}</summary>
        protected String _fa144;

        /// <summary>FA145: {NVARCHAR2(2000)}</summary>
        protected String _fa145;

        /// <summary>FA146: {NVARCHAR2(2000)}</summary>
        protected String _fa146;

        /// <summary>FA147: {NVARCHAR2(2000)}</summary>
        protected String _fa147;

        /// <summary>FA148: {NVARCHAR2(2000)}</summary>
        protected String _fa148;

        /// <summary>FA149: {NVARCHAR2(2000)}</summary>
        protected String _fa149;

        /// <summary>FA150: {NVARCHAR2(2000)}</summary>
        protected String _fa150;

        /// <summary>FA151: {NVARCHAR2(2000)}</summary>
        protected String _fa151;

        /// <summary>FA152: {NVARCHAR2(2000)}</summary>
        protected String _fa152;

        /// <summary>FA153: {NVARCHAR2(2000)}</summary>
        protected String _fa153;

        /// <summary>FA154: {NVARCHAR2(2000)}</summary>
        protected String _fa154;

        /// <summary>FA155: {NVARCHAR2(2000)}</summary>
        protected String _fa155;

        /// <summary>FA156: {NVARCHAR2(2000)}</summary>
        protected String _fa156;

        /// <summary>FA157: {NVARCHAR2(2000)}</summary>
        protected String _fa157;

        /// <summary>FA158: {NVARCHAR2(2000)}</summary>
        protected String _fa158;

        /// <summary>FA159: {NVARCHAR2(2000)}</summary>
        protected String _fa159;

        /// <summary>FA160: {NVARCHAR2(2000)}</summary>
        protected String _fa160;

        /// <summary>FA161: {NVARCHAR2(2000)}</summary>
        protected String _fa161;

        /// <summary>FA162: {NVARCHAR2(2000)}</summary>
        protected String _fa162;

        /// <summary>FA163: {NVARCHAR2(2000)}</summary>
        protected String _fa163;

        /// <summary>FA164: {NVARCHAR2(2000)}</summary>
        protected String _fa164;

        /// <summary>FA165: {NVARCHAR2(2000)}</summary>
        protected String _fa165;

        /// <summary>FA166: {NVARCHAR2(2000)}</summary>
        protected String _fa166;

        /// <summary>FA167: {NVARCHAR2(2000)}</summary>
        protected String _fa167;

        /// <summary>FA168: {NVARCHAR2(2000)}</summary>
        protected String _fa168;

        /// <summary>FA169: {NVARCHAR2(2000)}</summary>
        protected String _fa169;

        /// <summary>FA170: {NVARCHAR2(2000)}</summary>
        protected String _fa170;

        /// <summary>FA171: {NVARCHAR2(2000)}</summary>
        protected String _fa171;

        /// <summary>FA172: {NVARCHAR2(2000)}</summary>
        protected String _fa172;

        /// <summary>FA173: {NVARCHAR2(2000)}</summary>
        protected String _fa173;

        /// <summary>FA174: {NVARCHAR2(2000)}</summary>
        protected String _fa174;

        /// <summary>FA175: {NVARCHAR2(2000)}</summary>
        protected String _fa175;

        /// <summary>FA176: {NVARCHAR2(2000)}</summary>
        protected String _fa176;

        /// <summary>FA177: {NVARCHAR2(2000)}</summary>
        protected String _fa177;

        /// <summary>FA178: {NVARCHAR2(2000)}</summary>
        protected String _fa178;

        /// <summary>FA179: {NVARCHAR2(2000)}</summary>
        protected String _fa179;

        /// <summary>FA180: {NVARCHAR2(2000)}</summary>
        protected String _fa180;

        /// <summary>FA181: {NVARCHAR2(2000)}</summary>
        protected String _fa181;

        /// <summary>FA182: {NVARCHAR2(2000)}</summary>
        protected String _fa182;

        /// <summary>FA183: {NVARCHAR2(2000)}</summary>
        protected String _fa183;

        /// <summary>FA184: {NVARCHAR2(2000)}</summary>
        protected String _fa184;

        /// <summary>FA185: {NVARCHAR2(2000)}</summary>
        protected String _fa185;

        /// <summary>FA186: {NVARCHAR2(2000)}</summary>
        protected String _fa186;

        /// <summary>FA187: {NVARCHAR2(2000)}</summary>
        protected String _fa187;

        /// <summary>FA188: {NVARCHAR2(2000)}</summary>
        protected String _fa188;

        /// <summary>FA189: {NVARCHAR2(2000)}</summary>
        protected String _fa189;

        /// <summary>FA190: {NVARCHAR2(2000)}</summary>
        protected String _fa190;

        /// <summary>FA191: {NVARCHAR2(2000)}</summary>
        protected String _fa191;

        /// <summary>FA192: {NVARCHAR2(2000)}</summary>
        protected String _fa192;

        /// <summary>FA193: {NVARCHAR2(2000)}</summary>
        protected String _fa193;

        /// <summary>FA194: {NVARCHAR2(2000)}</summary>
        protected String _fa194;

        /// <summary>FA195: {NVARCHAR2(2000)}</summary>
        protected String _fa195;

        /// <summary>FA196: {NVARCHAR2(2000)}</summary>
        protected String _fa196;

        /// <summary>FA197: {NVARCHAR2(2000)}</summary>
        protected String _fa197;

        /// <summary>FA198: {NVARCHAR2(2000)}</summary>
        protected String _fa198;

        /// <summary>FA199: {NVARCHAR2(2000)}</summary>
        protected String _fa199;

        /// <summary>FA200: {NVARCHAR2(2000)}</summary>
        protected String _fa200;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_FA_DATA"; } }
        public String TablePropertyName { get { return "TFaData"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

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
            if (other == null || !(other is TFaData)) { return false; }
            TFaData otherEntity = (TFaData)other;
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
            return "TFaData:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.Fa001);
            sb.Append(c).Append(this.Fa002);
            sb.Append(c).Append(this.Fa003);
            sb.Append(c).Append(this.Fa004);
            sb.Append(c).Append(this.Fa005);
            sb.Append(c).Append(this.Fa006);
            sb.Append(c).Append(this.Fa007);
            sb.Append(c).Append(this.Fa008);
            sb.Append(c).Append(this.Fa009);
            sb.Append(c).Append(this.Fa010);
            sb.Append(c).Append(this.Fa011);
            sb.Append(c).Append(this.Fa012);
            sb.Append(c).Append(this.Fa013);
            sb.Append(c).Append(this.Fa014);
            sb.Append(c).Append(this.Fa015);
            sb.Append(c).Append(this.Fa016);
            sb.Append(c).Append(this.Fa017);
            sb.Append(c).Append(this.Fa018);
            sb.Append(c).Append(this.Fa019);
            sb.Append(c).Append(this.Fa020);
            sb.Append(c).Append(this.Fa021);
            sb.Append(c).Append(this.Fa022);
            sb.Append(c).Append(this.Fa023);
            sb.Append(c).Append(this.Fa024);
            sb.Append(c).Append(this.Fa025);
            sb.Append(c).Append(this.Fa026);
            sb.Append(c).Append(this.Fa027);
            sb.Append(c).Append(this.Fa028);
            sb.Append(c).Append(this.Fa029);
            sb.Append(c).Append(this.Fa030);
            sb.Append(c).Append(this.Fa031);
            sb.Append(c).Append(this.Fa032);
            sb.Append(c).Append(this.Fa033);
            sb.Append(c).Append(this.Fa034);
            sb.Append(c).Append(this.Fa035);
            sb.Append(c).Append(this.Fa036);
            sb.Append(c).Append(this.Fa037);
            sb.Append(c).Append(this.Fa038);
            sb.Append(c).Append(this.Fa039);
            sb.Append(c).Append(this.Fa040);
            sb.Append(c).Append(this.Fa041);
            sb.Append(c).Append(this.Fa042);
            sb.Append(c).Append(this.Fa043);
            sb.Append(c).Append(this.Fa044);
            sb.Append(c).Append(this.Fa045);
            sb.Append(c).Append(this.Fa046);
            sb.Append(c).Append(this.Fa047);
            sb.Append(c).Append(this.Fa048);
            sb.Append(c).Append(this.Fa049);
            sb.Append(c).Append(this.Fa050);
            sb.Append(c).Append(this.Fa051);
            sb.Append(c).Append(this.Fa052);
            sb.Append(c).Append(this.Fa053);
            sb.Append(c).Append(this.Fa054);
            sb.Append(c).Append(this.Fa055);
            sb.Append(c).Append(this.Fa056);
            sb.Append(c).Append(this.Fa057);
            sb.Append(c).Append(this.Fa058);
            sb.Append(c).Append(this.Fa059);
            sb.Append(c).Append(this.Fa060);
            sb.Append(c).Append(this.Fa061);
            sb.Append(c).Append(this.Fa062);
            sb.Append(c).Append(this.Fa063);
            sb.Append(c).Append(this.Fa064);
            sb.Append(c).Append(this.Fa065);
            sb.Append(c).Append(this.Fa066);
            sb.Append(c).Append(this.Fa067);
            sb.Append(c).Append(this.Fa068);
            sb.Append(c).Append(this.Fa069);
            sb.Append(c).Append(this.Fa070);
            sb.Append(c).Append(this.Fa071);
            sb.Append(c).Append(this.Fa072);
            sb.Append(c).Append(this.Fa073);
            sb.Append(c).Append(this.Fa074);
            sb.Append(c).Append(this.Fa075);
            sb.Append(c).Append(this.Fa076);
            sb.Append(c).Append(this.Fa077);
            sb.Append(c).Append(this.Fa078);
            sb.Append(c).Append(this.Fa079);
            sb.Append(c).Append(this.Fa080);
            sb.Append(c).Append(this.Fa081);
            sb.Append(c).Append(this.Fa082);
            sb.Append(c).Append(this.Fa083);
            sb.Append(c).Append(this.Fa084);
            sb.Append(c).Append(this.Fa085);
            sb.Append(c).Append(this.Fa086);
            sb.Append(c).Append(this.Fa087);
            sb.Append(c).Append(this.Fa088);
            sb.Append(c).Append(this.Fa089);
            sb.Append(c).Append(this.Fa090);
            sb.Append(c).Append(this.Fa091);
            sb.Append(c).Append(this.Fa092);
            sb.Append(c).Append(this.Fa093);
            sb.Append(c).Append(this.Fa094);
            sb.Append(c).Append(this.Fa095);
            sb.Append(c).Append(this.Fa096);
            sb.Append(c).Append(this.Fa097);
            sb.Append(c).Append(this.Fa098);
            sb.Append(c).Append(this.Fa099);
            sb.Append(c).Append(this.Fa100);
            sb.Append(c).Append(this.Fa101);
            sb.Append(c).Append(this.Fa102);
            sb.Append(c).Append(this.Fa103);
            sb.Append(c).Append(this.Fa104);
            sb.Append(c).Append(this.Fa105);
            sb.Append(c).Append(this.Fa106);
            sb.Append(c).Append(this.Fa107);
            sb.Append(c).Append(this.Fa108);
            sb.Append(c).Append(this.Fa109);
            sb.Append(c).Append(this.Fa110);
            sb.Append(c).Append(this.Fa111);
            sb.Append(c).Append(this.Fa112);
            sb.Append(c).Append(this.Fa113);
            sb.Append(c).Append(this.Fa114);
            sb.Append(c).Append(this.Fa115);
            sb.Append(c).Append(this.Fa116);
            sb.Append(c).Append(this.Fa117);
            sb.Append(c).Append(this.Fa118);
            sb.Append(c).Append(this.Fa119);
            sb.Append(c).Append(this.Fa120);
            sb.Append(c).Append(this.Fa121);
            sb.Append(c).Append(this.Fa122);
            sb.Append(c).Append(this.Fa123);
            sb.Append(c).Append(this.Fa124);
            sb.Append(c).Append(this.Fa125);
            sb.Append(c).Append(this.Fa126);
            sb.Append(c).Append(this.Fa127);
            sb.Append(c).Append(this.Fa128);
            sb.Append(c).Append(this.Fa129);
            sb.Append(c).Append(this.Fa130);
            sb.Append(c).Append(this.Fa131);
            sb.Append(c).Append(this.Fa132);
            sb.Append(c).Append(this.Fa133);
            sb.Append(c).Append(this.Fa134);
            sb.Append(c).Append(this.Fa135);
            sb.Append(c).Append(this.Fa136);
            sb.Append(c).Append(this.Fa137);
            sb.Append(c).Append(this.Fa138);
            sb.Append(c).Append(this.Fa139);
            sb.Append(c).Append(this.Fa140);
            sb.Append(c).Append(this.Fa141);
            sb.Append(c).Append(this.Fa142);
            sb.Append(c).Append(this.Fa143);
            sb.Append(c).Append(this.Fa144);
            sb.Append(c).Append(this.Fa145);
            sb.Append(c).Append(this.Fa146);
            sb.Append(c).Append(this.Fa147);
            sb.Append(c).Append(this.Fa148);
            sb.Append(c).Append(this.Fa149);
            sb.Append(c).Append(this.Fa150);
            sb.Append(c).Append(this.Fa151);
            sb.Append(c).Append(this.Fa152);
            sb.Append(c).Append(this.Fa153);
            sb.Append(c).Append(this.Fa154);
            sb.Append(c).Append(this.Fa155);
            sb.Append(c).Append(this.Fa156);
            sb.Append(c).Append(this.Fa157);
            sb.Append(c).Append(this.Fa158);
            sb.Append(c).Append(this.Fa159);
            sb.Append(c).Append(this.Fa160);
            sb.Append(c).Append(this.Fa161);
            sb.Append(c).Append(this.Fa162);
            sb.Append(c).Append(this.Fa163);
            sb.Append(c).Append(this.Fa164);
            sb.Append(c).Append(this.Fa165);
            sb.Append(c).Append(this.Fa166);
            sb.Append(c).Append(this.Fa167);
            sb.Append(c).Append(this.Fa168);
            sb.Append(c).Append(this.Fa169);
            sb.Append(c).Append(this.Fa170);
            sb.Append(c).Append(this.Fa171);
            sb.Append(c).Append(this.Fa172);
            sb.Append(c).Append(this.Fa173);
            sb.Append(c).Append(this.Fa174);
            sb.Append(c).Append(this.Fa175);
            sb.Append(c).Append(this.Fa176);
            sb.Append(c).Append(this.Fa177);
            sb.Append(c).Append(this.Fa178);
            sb.Append(c).Append(this.Fa179);
            sb.Append(c).Append(this.Fa180);
            sb.Append(c).Append(this.Fa181);
            sb.Append(c).Append(this.Fa182);
            sb.Append(c).Append(this.Fa183);
            sb.Append(c).Append(this.Fa184);
            sb.Append(c).Append(this.Fa185);
            sb.Append(c).Append(this.Fa186);
            sb.Append(c).Append(this.Fa187);
            sb.Append(c).Append(this.Fa188);
            sb.Append(c).Append(this.Fa189);
            sb.Append(c).Append(this.Fa190);
            sb.Append(c).Append(this.Fa191);
            sb.Append(c).Append(this.Fa192);
            sb.Append(c).Append(this.Fa193);
            sb.Append(c).Append(this.Fa194);
            sb.Append(c).Append(this.Fa195);
            sb.Append(c).Append(this.Fa196);
            sb.Append(c).Append(this.Fa197);
            sb.Append(c).Append(this.Fa198);
            sb.Append(c).Append(this.Fa199);
            sb.Append(c).Append(this.Fa200);
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

        /// <summary>FA001: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA001")]
        public String Fa001 {
            get { return _fa001; }
            set {
                __modifiedProperties.AddPropertyName("Fa001");
                _fa001 = value;
            }
        }

        /// <summary>FA002: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA002")]
        public String Fa002 {
            get { return _fa002; }
            set {
                __modifiedProperties.AddPropertyName("Fa002");
                _fa002 = value;
            }
        }

        /// <summary>FA003: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA003")]
        public String Fa003 {
            get { return _fa003; }
            set {
                __modifiedProperties.AddPropertyName("Fa003");
                _fa003 = value;
            }
        }

        /// <summary>FA004: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA004")]
        public String Fa004 {
            get { return _fa004; }
            set {
                __modifiedProperties.AddPropertyName("Fa004");
                _fa004 = value;
            }
        }

        /// <summary>FA005: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA005")]
        public String Fa005 {
            get { return _fa005; }
            set {
                __modifiedProperties.AddPropertyName("Fa005");
                _fa005 = value;
            }
        }

        /// <summary>FA006: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA006")]
        public String Fa006 {
            get { return _fa006; }
            set {
                __modifiedProperties.AddPropertyName("Fa006");
                _fa006 = value;
            }
        }

        /// <summary>FA007: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA007")]
        public String Fa007 {
            get { return _fa007; }
            set {
                __modifiedProperties.AddPropertyName("Fa007");
                _fa007 = value;
            }
        }

        /// <summary>FA008: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA008")]
        public String Fa008 {
            get { return _fa008; }
            set {
                __modifiedProperties.AddPropertyName("Fa008");
                _fa008 = value;
            }
        }

        /// <summary>FA009: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA009")]
        public String Fa009 {
            get { return _fa009; }
            set {
                __modifiedProperties.AddPropertyName("Fa009");
                _fa009 = value;
            }
        }

        /// <summary>FA010: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA010")]
        public String Fa010 {
            get { return _fa010; }
            set {
                __modifiedProperties.AddPropertyName("Fa010");
                _fa010 = value;
            }
        }

        /// <summary>FA011: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA011")]
        public String Fa011 {
            get { return _fa011; }
            set {
                __modifiedProperties.AddPropertyName("Fa011");
                _fa011 = value;
            }
        }

        /// <summary>FA012: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA012")]
        public String Fa012 {
            get { return _fa012; }
            set {
                __modifiedProperties.AddPropertyName("Fa012");
                _fa012 = value;
            }
        }

        /// <summary>FA013: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA013")]
        public String Fa013 {
            get { return _fa013; }
            set {
                __modifiedProperties.AddPropertyName("Fa013");
                _fa013 = value;
            }
        }

        /// <summary>FA014: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA014")]
        public String Fa014 {
            get { return _fa014; }
            set {
                __modifiedProperties.AddPropertyName("Fa014");
                _fa014 = value;
            }
        }

        /// <summary>FA015: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA015")]
        public String Fa015 {
            get { return _fa015; }
            set {
                __modifiedProperties.AddPropertyName("Fa015");
                _fa015 = value;
            }
        }

        /// <summary>FA016: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA016")]
        public String Fa016 {
            get { return _fa016; }
            set {
                __modifiedProperties.AddPropertyName("Fa016");
                _fa016 = value;
            }
        }

        /// <summary>FA017: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA017")]
        public String Fa017 {
            get { return _fa017; }
            set {
                __modifiedProperties.AddPropertyName("Fa017");
                _fa017 = value;
            }
        }

        /// <summary>FA018: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA018")]
        public String Fa018 {
            get { return _fa018; }
            set {
                __modifiedProperties.AddPropertyName("Fa018");
                _fa018 = value;
            }
        }

        /// <summary>FA019: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA019")]
        public String Fa019 {
            get { return _fa019; }
            set {
                __modifiedProperties.AddPropertyName("Fa019");
                _fa019 = value;
            }
        }

        /// <summary>FA020: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA020")]
        public String Fa020 {
            get { return _fa020; }
            set {
                __modifiedProperties.AddPropertyName("Fa020");
                _fa020 = value;
            }
        }

        /// <summary>FA021: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA021")]
        public String Fa021 {
            get { return _fa021; }
            set {
                __modifiedProperties.AddPropertyName("Fa021");
                _fa021 = value;
            }
        }

        /// <summary>FA022: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA022")]
        public String Fa022 {
            get { return _fa022; }
            set {
                __modifiedProperties.AddPropertyName("Fa022");
                _fa022 = value;
            }
        }

        /// <summary>FA023: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA023")]
        public String Fa023 {
            get { return _fa023; }
            set {
                __modifiedProperties.AddPropertyName("Fa023");
                _fa023 = value;
            }
        }

        /// <summary>FA024: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA024")]
        public String Fa024 {
            get { return _fa024; }
            set {
                __modifiedProperties.AddPropertyName("Fa024");
                _fa024 = value;
            }
        }

        /// <summary>FA025: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA025")]
        public String Fa025 {
            get { return _fa025; }
            set {
                __modifiedProperties.AddPropertyName("Fa025");
                _fa025 = value;
            }
        }

        /// <summary>FA026: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA026")]
        public String Fa026 {
            get { return _fa026; }
            set {
                __modifiedProperties.AddPropertyName("Fa026");
                _fa026 = value;
            }
        }

        /// <summary>FA027: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA027")]
        public String Fa027 {
            get { return _fa027; }
            set {
                __modifiedProperties.AddPropertyName("Fa027");
                _fa027 = value;
            }
        }

        /// <summary>FA028: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA028")]
        public String Fa028 {
            get { return _fa028; }
            set {
                __modifiedProperties.AddPropertyName("Fa028");
                _fa028 = value;
            }
        }

        /// <summary>FA029: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA029")]
        public String Fa029 {
            get { return _fa029; }
            set {
                __modifiedProperties.AddPropertyName("Fa029");
                _fa029 = value;
            }
        }

        /// <summary>FA030: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA030")]
        public String Fa030 {
            get { return _fa030; }
            set {
                __modifiedProperties.AddPropertyName("Fa030");
                _fa030 = value;
            }
        }

        /// <summary>FA031: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA031")]
        public String Fa031 {
            get { return _fa031; }
            set {
                __modifiedProperties.AddPropertyName("Fa031");
                _fa031 = value;
            }
        }

        /// <summary>FA032: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA032")]
        public String Fa032 {
            get { return _fa032; }
            set {
                __modifiedProperties.AddPropertyName("Fa032");
                _fa032 = value;
            }
        }

        /// <summary>FA033: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA033")]
        public String Fa033 {
            get { return _fa033; }
            set {
                __modifiedProperties.AddPropertyName("Fa033");
                _fa033 = value;
            }
        }

        /// <summary>FA034: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA034")]
        public String Fa034 {
            get { return _fa034; }
            set {
                __modifiedProperties.AddPropertyName("Fa034");
                _fa034 = value;
            }
        }

        /// <summary>FA035: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA035")]
        public String Fa035 {
            get { return _fa035; }
            set {
                __modifiedProperties.AddPropertyName("Fa035");
                _fa035 = value;
            }
        }

        /// <summary>FA036: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA036")]
        public String Fa036 {
            get { return _fa036; }
            set {
                __modifiedProperties.AddPropertyName("Fa036");
                _fa036 = value;
            }
        }

        /// <summary>FA037: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA037")]
        public String Fa037 {
            get { return _fa037; }
            set {
                __modifiedProperties.AddPropertyName("Fa037");
                _fa037 = value;
            }
        }

        /// <summary>FA038: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA038")]
        public String Fa038 {
            get { return _fa038; }
            set {
                __modifiedProperties.AddPropertyName("Fa038");
                _fa038 = value;
            }
        }

        /// <summary>FA039: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA039")]
        public String Fa039 {
            get { return _fa039; }
            set {
                __modifiedProperties.AddPropertyName("Fa039");
                _fa039 = value;
            }
        }

        /// <summary>FA040: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA040")]
        public String Fa040 {
            get { return _fa040; }
            set {
                __modifiedProperties.AddPropertyName("Fa040");
                _fa040 = value;
            }
        }

        /// <summary>FA041: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA041")]
        public String Fa041 {
            get { return _fa041; }
            set {
                __modifiedProperties.AddPropertyName("Fa041");
                _fa041 = value;
            }
        }

        /// <summary>FA042: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA042")]
        public String Fa042 {
            get { return _fa042; }
            set {
                __modifiedProperties.AddPropertyName("Fa042");
                _fa042 = value;
            }
        }

        /// <summary>FA043: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA043")]
        public String Fa043 {
            get { return _fa043; }
            set {
                __modifiedProperties.AddPropertyName("Fa043");
                _fa043 = value;
            }
        }

        /// <summary>FA044: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA044")]
        public String Fa044 {
            get { return _fa044; }
            set {
                __modifiedProperties.AddPropertyName("Fa044");
                _fa044 = value;
            }
        }

        /// <summary>FA045: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA045")]
        public String Fa045 {
            get { return _fa045; }
            set {
                __modifiedProperties.AddPropertyName("Fa045");
                _fa045 = value;
            }
        }

        /// <summary>FA046: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA046")]
        public String Fa046 {
            get { return _fa046; }
            set {
                __modifiedProperties.AddPropertyName("Fa046");
                _fa046 = value;
            }
        }

        /// <summary>FA047: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA047")]
        public String Fa047 {
            get { return _fa047; }
            set {
                __modifiedProperties.AddPropertyName("Fa047");
                _fa047 = value;
            }
        }

        /// <summary>FA048: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA048")]
        public String Fa048 {
            get { return _fa048; }
            set {
                __modifiedProperties.AddPropertyName("Fa048");
                _fa048 = value;
            }
        }

        /// <summary>FA049: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA049")]
        public String Fa049 {
            get { return _fa049; }
            set {
                __modifiedProperties.AddPropertyName("Fa049");
                _fa049 = value;
            }
        }

        /// <summary>FA050: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA050")]
        public String Fa050 {
            get { return _fa050; }
            set {
                __modifiedProperties.AddPropertyName("Fa050");
                _fa050 = value;
            }
        }

        /// <summary>FA051: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA051")]
        public String Fa051 {
            get { return _fa051; }
            set {
                __modifiedProperties.AddPropertyName("Fa051");
                _fa051 = value;
            }
        }

        /// <summary>FA052: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA052")]
        public String Fa052 {
            get { return _fa052; }
            set {
                __modifiedProperties.AddPropertyName("Fa052");
                _fa052 = value;
            }
        }

        /// <summary>FA053: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA053")]
        public String Fa053 {
            get { return _fa053; }
            set {
                __modifiedProperties.AddPropertyName("Fa053");
                _fa053 = value;
            }
        }

        /// <summary>FA054: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA054")]
        public String Fa054 {
            get { return _fa054; }
            set {
                __modifiedProperties.AddPropertyName("Fa054");
                _fa054 = value;
            }
        }

        /// <summary>FA055: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA055")]
        public String Fa055 {
            get { return _fa055; }
            set {
                __modifiedProperties.AddPropertyName("Fa055");
                _fa055 = value;
            }
        }

        /// <summary>FA056: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA056")]
        public String Fa056 {
            get { return _fa056; }
            set {
                __modifiedProperties.AddPropertyName("Fa056");
                _fa056 = value;
            }
        }

        /// <summary>FA057: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA057")]
        public String Fa057 {
            get { return _fa057; }
            set {
                __modifiedProperties.AddPropertyName("Fa057");
                _fa057 = value;
            }
        }

        /// <summary>FA058: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA058")]
        public String Fa058 {
            get { return _fa058; }
            set {
                __modifiedProperties.AddPropertyName("Fa058");
                _fa058 = value;
            }
        }

        /// <summary>FA059: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA059")]
        public String Fa059 {
            get { return _fa059; }
            set {
                __modifiedProperties.AddPropertyName("Fa059");
                _fa059 = value;
            }
        }

        /// <summary>FA060: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA060")]
        public String Fa060 {
            get { return _fa060; }
            set {
                __modifiedProperties.AddPropertyName("Fa060");
                _fa060 = value;
            }
        }

        /// <summary>FA061: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA061")]
        public String Fa061 {
            get { return _fa061; }
            set {
                __modifiedProperties.AddPropertyName("Fa061");
                _fa061 = value;
            }
        }

        /// <summary>FA062: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA062")]
        public String Fa062 {
            get { return _fa062; }
            set {
                __modifiedProperties.AddPropertyName("Fa062");
                _fa062 = value;
            }
        }

        /// <summary>FA063: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA063")]
        public String Fa063 {
            get { return _fa063; }
            set {
                __modifiedProperties.AddPropertyName("Fa063");
                _fa063 = value;
            }
        }

        /// <summary>FA064: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA064")]
        public String Fa064 {
            get { return _fa064; }
            set {
                __modifiedProperties.AddPropertyName("Fa064");
                _fa064 = value;
            }
        }

        /// <summary>FA065: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA065")]
        public String Fa065 {
            get { return _fa065; }
            set {
                __modifiedProperties.AddPropertyName("Fa065");
                _fa065 = value;
            }
        }

        /// <summary>FA066: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA066")]
        public String Fa066 {
            get { return _fa066; }
            set {
                __modifiedProperties.AddPropertyName("Fa066");
                _fa066 = value;
            }
        }

        /// <summary>FA067: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA067")]
        public String Fa067 {
            get { return _fa067; }
            set {
                __modifiedProperties.AddPropertyName("Fa067");
                _fa067 = value;
            }
        }

        /// <summary>FA068: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA068")]
        public String Fa068 {
            get { return _fa068; }
            set {
                __modifiedProperties.AddPropertyName("Fa068");
                _fa068 = value;
            }
        }

        /// <summary>FA069: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA069")]
        public String Fa069 {
            get { return _fa069; }
            set {
                __modifiedProperties.AddPropertyName("Fa069");
                _fa069 = value;
            }
        }

        /// <summary>FA070: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA070")]
        public String Fa070 {
            get { return _fa070; }
            set {
                __modifiedProperties.AddPropertyName("Fa070");
                _fa070 = value;
            }
        }

        /// <summary>FA071: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA071")]
        public String Fa071 {
            get { return _fa071; }
            set {
                __modifiedProperties.AddPropertyName("Fa071");
                _fa071 = value;
            }
        }

        /// <summary>FA072: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA072")]
        public String Fa072 {
            get { return _fa072; }
            set {
                __modifiedProperties.AddPropertyName("Fa072");
                _fa072 = value;
            }
        }

        /// <summary>FA073: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA073")]
        public String Fa073 {
            get { return _fa073; }
            set {
                __modifiedProperties.AddPropertyName("Fa073");
                _fa073 = value;
            }
        }

        /// <summary>FA074: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA074")]
        public String Fa074 {
            get { return _fa074; }
            set {
                __modifiedProperties.AddPropertyName("Fa074");
                _fa074 = value;
            }
        }

        /// <summary>FA075: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA075")]
        public String Fa075 {
            get { return _fa075; }
            set {
                __modifiedProperties.AddPropertyName("Fa075");
                _fa075 = value;
            }
        }

        /// <summary>FA076: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA076")]
        public String Fa076 {
            get { return _fa076; }
            set {
                __modifiedProperties.AddPropertyName("Fa076");
                _fa076 = value;
            }
        }

        /// <summary>FA077: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA077")]
        public String Fa077 {
            get { return _fa077; }
            set {
                __modifiedProperties.AddPropertyName("Fa077");
                _fa077 = value;
            }
        }

        /// <summary>FA078: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA078")]
        public String Fa078 {
            get { return _fa078; }
            set {
                __modifiedProperties.AddPropertyName("Fa078");
                _fa078 = value;
            }
        }

        /// <summary>FA079: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA079")]
        public String Fa079 {
            get { return _fa079; }
            set {
                __modifiedProperties.AddPropertyName("Fa079");
                _fa079 = value;
            }
        }

        /// <summary>FA080: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA080")]
        public String Fa080 {
            get { return _fa080; }
            set {
                __modifiedProperties.AddPropertyName("Fa080");
                _fa080 = value;
            }
        }

        /// <summary>FA081: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA081")]
        public String Fa081 {
            get { return _fa081; }
            set {
                __modifiedProperties.AddPropertyName("Fa081");
                _fa081 = value;
            }
        }

        /// <summary>FA082: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA082")]
        public String Fa082 {
            get { return _fa082; }
            set {
                __modifiedProperties.AddPropertyName("Fa082");
                _fa082 = value;
            }
        }

        /// <summary>FA083: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA083")]
        public String Fa083 {
            get { return _fa083; }
            set {
                __modifiedProperties.AddPropertyName("Fa083");
                _fa083 = value;
            }
        }

        /// <summary>FA084: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA084")]
        public String Fa084 {
            get { return _fa084; }
            set {
                __modifiedProperties.AddPropertyName("Fa084");
                _fa084 = value;
            }
        }

        /// <summary>FA085: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA085")]
        public String Fa085 {
            get { return _fa085; }
            set {
                __modifiedProperties.AddPropertyName("Fa085");
                _fa085 = value;
            }
        }

        /// <summary>FA086: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA086")]
        public String Fa086 {
            get { return _fa086; }
            set {
                __modifiedProperties.AddPropertyName("Fa086");
                _fa086 = value;
            }
        }

        /// <summary>FA087: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA087")]
        public String Fa087 {
            get { return _fa087; }
            set {
                __modifiedProperties.AddPropertyName("Fa087");
                _fa087 = value;
            }
        }

        /// <summary>FA088: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA088")]
        public String Fa088 {
            get { return _fa088; }
            set {
                __modifiedProperties.AddPropertyName("Fa088");
                _fa088 = value;
            }
        }

        /// <summary>FA089: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA089")]
        public String Fa089 {
            get { return _fa089; }
            set {
                __modifiedProperties.AddPropertyName("Fa089");
                _fa089 = value;
            }
        }

        /// <summary>FA090: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA090")]
        public String Fa090 {
            get { return _fa090; }
            set {
                __modifiedProperties.AddPropertyName("Fa090");
                _fa090 = value;
            }
        }

        /// <summary>FA091: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA091")]
        public String Fa091 {
            get { return _fa091; }
            set {
                __modifiedProperties.AddPropertyName("Fa091");
                _fa091 = value;
            }
        }

        /// <summary>FA092: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA092")]
        public String Fa092 {
            get { return _fa092; }
            set {
                __modifiedProperties.AddPropertyName("Fa092");
                _fa092 = value;
            }
        }

        /// <summary>FA093: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA093")]
        public String Fa093 {
            get { return _fa093; }
            set {
                __modifiedProperties.AddPropertyName("Fa093");
                _fa093 = value;
            }
        }

        /// <summary>FA094: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA094")]
        public String Fa094 {
            get { return _fa094; }
            set {
                __modifiedProperties.AddPropertyName("Fa094");
                _fa094 = value;
            }
        }

        /// <summary>FA095: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA095")]
        public String Fa095 {
            get { return _fa095; }
            set {
                __modifiedProperties.AddPropertyName("Fa095");
                _fa095 = value;
            }
        }

        /// <summary>FA096: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA096")]
        public String Fa096 {
            get { return _fa096; }
            set {
                __modifiedProperties.AddPropertyName("Fa096");
                _fa096 = value;
            }
        }

        /// <summary>FA097: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA097")]
        public String Fa097 {
            get { return _fa097; }
            set {
                __modifiedProperties.AddPropertyName("Fa097");
                _fa097 = value;
            }
        }

        /// <summary>FA098: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA098")]
        public String Fa098 {
            get { return _fa098; }
            set {
                __modifiedProperties.AddPropertyName("Fa098");
                _fa098 = value;
            }
        }

        /// <summary>FA099: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA099")]
        public String Fa099 {
            get { return _fa099; }
            set {
                __modifiedProperties.AddPropertyName("Fa099");
                _fa099 = value;
            }
        }

        /// <summary>FA100: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA100")]
        public String Fa100 {
            get { return _fa100; }
            set {
                __modifiedProperties.AddPropertyName("Fa100");
                _fa100 = value;
            }
        }

        /// <summary>FA101: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA101")]
        public String Fa101 {
            get { return _fa101; }
            set {
                __modifiedProperties.AddPropertyName("Fa101");
                _fa101 = value;
            }
        }

        /// <summary>FA102: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA102")]
        public String Fa102 {
            get { return _fa102; }
            set {
                __modifiedProperties.AddPropertyName("Fa102");
                _fa102 = value;
            }
        }

        /// <summary>FA103: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA103")]
        public String Fa103 {
            get { return _fa103; }
            set {
                __modifiedProperties.AddPropertyName("Fa103");
                _fa103 = value;
            }
        }

        /// <summary>FA104: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA104")]
        public String Fa104 {
            get { return _fa104; }
            set {
                __modifiedProperties.AddPropertyName("Fa104");
                _fa104 = value;
            }
        }

        /// <summary>FA105: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA105")]
        public String Fa105 {
            get { return _fa105; }
            set {
                __modifiedProperties.AddPropertyName("Fa105");
                _fa105 = value;
            }
        }

        /// <summary>FA106: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA106")]
        public String Fa106 {
            get { return _fa106; }
            set {
                __modifiedProperties.AddPropertyName("Fa106");
                _fa106 = value;
            }
        }

        /// <summary>FA107: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA107")]
        public String Fa107 {
            get { return _fa107; }
            set {
                __modifiedProperties.AddPropertyName("Fa107");
                _fa107 = value;
            }
        }

        /// <summary>FA108: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA108")]
        public String Fa108 {
            get { return _fa108; }
            set {
                __modifiedProperties.AddPropertyName("Fa108");
                _fa108 = value;
            }
        }

        /// <summary>FA109: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA109")]
        public String Fa109 {
            get { return _fa109; }
            set {
                __modifiedProperties.AddPropertyName("Fa109");
                _fa109 = value;
            }
        }

        /// <summary>FA110: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA110")]
        public String Fa110 {
            get { return _fa110; }
            set {
                __modifiedProperties.AddPropertyName("Fa110");
                _fa110 = value;
            }
        }

        /// <summary>FA111: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA111")]
        public String Fa111 {
            get { return _fa111; }
            set {
                __modifiedProperties.AddPropertyName("Fa111");
                _fa111 = value;
            }
        }

        /// <summary>FA112: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA112")]
        public String Fa112 {
            get { return _fa112; }
            set {
                __modifiedProperties.AddPropertyName("Fa112");
                _fa112 = value;
            }
        }

        /// <summary>FA113: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA113")]
        public String Fa113 {
            get { return _fa113; }
            set {
                __modifiedProperties.AddPropertyName("Fa113");
                _fa113 = value;
            }
        }

        /// <summary>FA114: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA114")]
        public String Fa114 {
            get { return _fa114; }
            set {
                __modifiedProperties.AddPropertyName("Fa114");
                _fa114 = value;
            }
        }

        /// <summary>FA115: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA115")]
        public String Fa115 {
            get { return _fa115; }
            set {
                __modifiedProperties.AddPropertyName("Fa115");
                _fa115 = value;
            }
        }

        /// <summary>FA116: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA116")]
        public String Fa116 {
            get { return _fa116; }
            set {
                __modifiedProperties.AddPropertyName("Fa116");
                _fa116 = value;
            }
        }

        /// <summary>FA117: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA117")]
        public String Fa117 {
            get { return _fa117; }
            set {
                __modifiedProperties.AddPropertyName("Fa117");
                _fa117 = value;
            }
        }

        /// <summary>FA118: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA118")]
        public String Fa118 {
            get { return _fa118; }
            set {
                __modifiedProperties.AddPropertyName("Fa118");
                _fa118 = value;
            }
        }

        /// <summary>FA119: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA119")]
        public String Fa119 {
            get { return _fa119; }
            set {
                __modifiedProperties.AddPropertyName("Fa119");
                _fa119 = value;
            }
        }

        /// <summary>FA120: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA120")]
        public String Fa120 {
            get { return _fa120; }
            set {
                __modifiedProperties.AddPropertyName("Fa120");
                _fa120 = value;
            }
        }

        /// <summary>FA121: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA121")]
        public String Fa121 {
            get { return _fa121; }
            set {
                __modifiedProperties.AddPropertyName("Fa121");
                _fa121 = value;
            }
        }

        /// <summary>FA122: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA122")]
        public String Fa122 {
            get { return _fa122; }
            set {
                __modifiedProperties.AddPropertyName("Fa122");
                _fa122 = value;
            }
        }

        /// <summary>FA123: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA123")]
        public String Fa123 {
            get { return _fa123; }
            set {
                __modifiedProperties.AddPropertyName("Fa123");
                _fa123 = value;
            }
        }

        /// <summary>FA124: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA124")]
        public String Fa124 {
            get { return _fa124; }
            set {
                __modifiedProperties.AddPropertyName("Fa124");
                _fa124 = value;
            }
        }

        /// <summary>FA125: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA125")]
        public String Fa125 {
            get { return _fa125; }
            set {
                __modifiedProperties.AddPropertyName("Fa125");
                _fa125 = value;
            }
        }

        /// <summary>FA126: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA126")]
        public String Fa126 {
            get { return _fa126; }
            set {
                __modifiedProperties.AddPropertyName("Fa126");
                _fa126 = value;
            }
        }

        /// <summary>FA127: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA127")]
        public String Fa127 {
            get { return _fa127; }
            set {
                __modifiedProperties.AddPropertyName("Fa127");
                _fa127 = value;
            }
        }

        /// <summary>FA128: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA128")]
        public String Fa128 {
            get { return _fa128; }
            set {
                __modifiedProperties.AddPropertyName("Fa128");
                _fa128 = value;
            }
        }

        /// <summary>FA129: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA129")]
        public String Fa129 {
            get { return _fa129; }
            set {
                __modifiedProperties.AddPropertyName("Fa129");
                _fa129 = value;
            }
        }

        /// <summary>FA130: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA130")]
        public String Fa130 {
            get { return _fa130; }
            set {
                __modifiedProperties.AddPropertyName("Fa130");
                _fa130 = value;
            }
        }

        /// <summary>FA131: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA131")]
        public String Fa131 {
            get { return _fa131; }
            set {
                __modifiedProperties.AddPropertyName("Fa131");
                _fa131 = value;
            }
        }

        /// <summary>FA132: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA132")]
        public String Fa132 {
            get { return _fa132; }
            set {
                __modifiedProperties.AddPropertyName("Fa132");
                _fa132 = value;
            }
        }

        /// <summary>FA133: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA133")]
        public String Fa133 {
            get { return _fa133; }
            set {
                __modifiedProperties.AddPropertyName("Fa133");
                _fa133 = value;
            }
        }

        /// <summary>FA134: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA134")]
        public String Fa134 {
            get { return _fa134; }
            set {
                __modifiedProperties.AddPropertyName("Fa134");
                _fa134 = value;
            }
        }

        /// <summary>FA135: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA135")]
        public String Fa135 {
            get { return _fa135; }
            set {
                __modifiedProperties.AddPropertyName("Fa135");
                _fa135 = value;
            }
        }

        /// <summary>FA136: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA136")]
        public String Fa136 {
            get { return _fa136; }
            set {
                __modifiedProperties.AddPropertyName("Fa136");
                _fa136 = value;
            }
        }

        /// <summary>FA137: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA137")]
        public String Fa137 {
            get { return _fa137; }
            set {
                __modifiedProperties.AddPropertyName("Fa137");
                _fa137 = value;
            }
        }

        /// <summary>FA138: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA138")]
        public String Fa138 {
            get { return _fa138; }
            set {
                __modifiedProperties.AddPropertyName("Fa138");
                _fa138 = value;
            }
        }

        /// <summary>FA139: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA139")]
        public String Fa139 {
            get { return _fa139; }
            set {
                __modifiedProperties.AddPropertyName("Fa139");
                _fa139 = value;
            }
        }

        /// <summary>FA140: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA140")]
        public String Fa140 {
            get { return _fa140; }
            set {
                __modifiedProperties.AddPropertyName("Fa140");
                _fa140 = value;
            }
        }

        /// <summary>FA141: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA141")]
        public String Fa141 {
            get { return _fa141; }
            set {
                __modifiedProperties.AddPropertyName("Fa141");
                _fa141 = value;
            }
        }

        /// <summary>FA142: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA142")]
        public String Fa142 {
            get { return _fa142; }
            set {
                __modifiedProperties.AddPropertyName("Fa142");
                _fa142 = value;
            }
        }

        /// <summary>FA143: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA143")]
        public String Fa143 {
            get { return _fa143; }
            set {
                __modifiedProperties.AddPropertyName("Fa143");
                _fa143 = value;
            }
        }

        /// <summary>FA144: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA144")]
        public String Fa144 {
            get { return _fa144; }
            set {
                __modifiedProperties.AddPropertyName("Fa144");
                _fa144 = value;
            }
        }

        /// <summary>FA145: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA145")]
        public String Fa145 {
            get { return _fa145; }
            set {
                __modifiedProperties.AddPropertyName("Fa145");
                _fa145 = value;
            }
        }

        /// <summary>FA146: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA146")]
        public String Fa146 {
            get { return _fa146; }
            set {
                __modifiedProperties.AddPropertyName("Fa146");
                _fa146 = value;
            }
        }

        /// <summary>FA147: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA147")]
        public String Fa147 {
            get { return _fa147; }
            set {
                __modifiedProperties.AddPropertyName("Fa147");
                _fa147 = value;
            }
        }

        /// <summary>FA148: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA148")]
        public String Fa148 {
            get { return _fa148; }
            set {
                __modifiedProperties.AddPropertyName("Fa148");
                _fa148 = value;
            }
        }

        /// <summary>FA149: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA149")]
        public String Fa149 {
            get { return _fa149; }
            set {
                __modifiedProperties.AddPropertyName("Fa149");
                _fa149 = value;
            }
        }

        /// <summary>FA150: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA150")]
        public String Fa150 {
            get { return _fa150; }
            set {
                __modifiedProperties.AddPropertyName("Fa150");
                _fa150 = value;
            }
        }

        /// <summary>FA151: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA151")]
        public String Fa151 {
            get { return _fa151; }
            set {
                __modifiedProperties.AddPropertyName("Fa151");
                _fa151 = value;
            }
        }

        /// <summary>FA152: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA152")]
        public String Fa152 {
            get { return _fa152; }
            set {
                __modifiedProperties.AddPropertyName("Fa152");
                _fa152 = value;
            }
        }

        /// <summary>FA153: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA153")]
        public String Fa153 {
            get { return _fa153; }
            set {
                __modifiedProperties.AddPropertyName("Fa153");
                _fa153 = value;
            }
        }

        /// <summary>FA154: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA154")]
        public String Fa154 {
            get { return _fa154; }
            set {
                __modifiedProperties.AddPropertyName("Fa154");
                _fa154 = value;
            }
        }

        /// <summary>FA155: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA155")]
        public String Fa155 {
            get { return _fa155; }
            set {
                __modifiedProperties.AddPropertyName("Fa155");
                _fa155 = value;
            }
        }

        /// <summary>FA156: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA156")]
        public String Fa156 {
            get { return _fa156; }
            set {
                __modifiedProperties.AddPropertyName("Fa156");
                _fa156 = value;
            }
        }

        /// <summary>FA157: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA157")]
        public String Fa157 {
            get { return _fa157; }
            set {
                __modifiedProperties.AddPropertyName("Fa157");
                _fa157 = value;
            }
        }

        /// <summary>FA158: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA158")]
        public String Fa158 {
            get { return _fa158; }
            set {
                __modifiedProperties.AddPropertyName("Fa158");
                _fa158 = value;
            }
        }

        /// <summary>FA159: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA159")]
        public String Fa159 {
            get { return _fa159; }
            set {
                __modifiedProperties.AddPropertyName("Fa159");
                _fa159 = value;
            }
        }

        /// <summary>FA160: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA160")]
        public String Fa160 {
            get { return _fa160; }
            set {
                __modifiedProperties.AddPropertyName("Fa160");
                _fa160 = value;
            }
        }

        /// <summary>FA161: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA161")]
        public String Fa161 {
            get { return _fa161; }
            set {
                __modifiedProperties.AddPropertyName("Fa161");
                _fa161 = value;
            }
        }

        /// <summary>FA162: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA162")]
        public String Fa162 {
            get { return _fa162; }
            set {
                __modifiedProperties.AddPropertyName("Fa162");
                _fa162 = value;
            }
        }

        /// <summary>FA163: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA163")]
        public String Fa163 {
            get { return _fa163; }
            set {
                __modifiedProperties.AddPropertyName("Fa163");
                _fa163 = value;
            }
        }

        /// <summary>FA164: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA164")]
        public String Fa164 {
            get { return _fa164; }
            set {
                __modifiedProperties.AddPropertyName("Fa164");
                _fa164 = value;
            }
        }

        /// <summary>FA165: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA165")]
        public String Fa165 {
            get { return _fa165; }
            set {
                __modifiedProperties.AddPropertyName("Fa165");
                _fa165 = value;
            }
        }

        /// <summary>FA166: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA166")]
        public String Fa166 {
            get { return _fa166; }
            set {
                __modifiedProperties.AddPropertyName("Fa166");
                _fa166 = value;
            }
        }

        /// <summary>FA167: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA167")]
        public String Fa167 {
            get { return _fa167; }
            set {
                __modifiedProperties.AddPropertyName("Fa167");
                _fa167 = value;
            }
        }

        /// <summary>FA168: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA168")]
        public String Fa168 {
            get { return _fa168; }
            set {
                __modifiedProperties.AddPropertyName("Fa168");
                _fa168 = value;
            }
        }

        /// <summary>FA169: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA169")]
        public String Fa169 {
            get { return _fa169; }
            set {
                __modifiedProperties.AddPropertyName("Fa169");
                _fa169 = value;
            }
        }

        /// <summary>FA170: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA170")]
        public String Fa170 {
            get { return _fa170; }
            set {
                __modifiedProperties.AddPropertyName("Fa170");
                _fa170 = value;
            }
        }

        /// <summary>FA171: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA171")]
        public String Fa171 {
            get { return _fa171; }
            set {
                __modifiedProperties.AddPropertyName("Fa171");
                _fa171 = value;
            }
        }

        /// <summary>FA172: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA172")]
        public String Fa172 {
            get { return _fa172; }
            set {
                __modifiedProperties.AddPropertyName("Fa172");
                _fa172 = value;
            }
        }

        /// <summary>FA173: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA173")]
        public String Fa173 {
            get { return _fa173; }
            set {
                __modifiedProperties.AddPropertyName("Fa173");
                _fa173 = value;
            }
        }

        /// <summary>FA174: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA174")]
        public String Fa174 {
            get { return _fa174; }
            set {
                __modifiedProperties.AddPropertyName("Fa174");
                _fa174 = value;
            }
        }

        /// <summary>FA175: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA175")]
        public String Fa175 {
            get { return _fa175; }
            set {
                __modifiedProperties.AddPropertyName("Fa175");
                _fa175 = value;
            }
        }

        /// <summary>FA176: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA176")]
        public String Fa176 {
            get { return _fa176; }
            set {
                __modifiedProperties.AddPropertyName("Fa176");
                _fa176 = value;
            }
        }

        /// <summary>FA177: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA177")]
        public String Fa177 {
            get { return _fa177; }
            set {
                __modifiedProperties.AddPropertyName("Fa177");
                _fa177 = value;
            }
        }

        /// <summary>FA178: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA178")]
        public String Fa178 {
            get { return _fa178; }
            set {
                __modifiedProperties.AddPropertyName("Fa178");
                _fa178 = value;
            }
        }

        /// <summary>FA179: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA179")]
        public String Fa179 {
            get { return _fa179; }
            set {
                __modifiedProperties.AddPropertyName("Fa179");
                _fa179 = value;
            }
        }

        /// <summary>FA180: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA180")]
        public String Fa180 {
            get { return _fa180; }
            set {
                __modifiedProperties.AddPropertyName("Fa180");
                _fa180 = value;
            }
        }

        /// <summary>FA181: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA181")]
        public String Fa181 {
            get { return _fa181; }
            set {
                __modifiedProperties.AddPropertyName("Fa181");
                _fa181 = value;
            }
        }

        /// <summary>FA182: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA182")]
        public String Fa182 {
            get { return _fa182; }
            set {
                __modifiedProperties.AddPropertyName("Fa182");
                _fa182 = value;
            }
        }

        /// <summary>FA183: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA183")]
        public String Fa183 {
            get { return _fa183; }
            set {
                __modifiedProperties.AddPropertyName("Fa183");
                _fa183 = value;
            }
        }

        /// <summary>FA184: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA184")]
        public String Fa184 {
            get { return _fa184; }
            set {
                __modifiedProperties.AddPropertyName("Fa184");
                _fa184 = value;
            }
        }

        /// <summary>FA185: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA185")]
        public String Fa185 {
            get { return _fa185; }
            set {
                __modifiedProperties.AddPropertyName("Fa185");
                _fa185 = value;
            }
        }

        /// <summary>FA186: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA186")]
        public String Fa186 {
            get { return _fa186; }
            set {
                __modifiedProperties.AddPropertyName("Fa186");
                _fa186 = value;
            }
        }

        /// <summary>FA187: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA187")]
        public String Fa187 {
            get { return _fa187; }
            set {
                __modifiedProperties.AddPropertyName("Fa187");
                _fa187 = value;
            }
        }

        /// <summary>FA188: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA188")]
        public String Fa188 {
            get { return _fa188; }
            set {
                __modifiedProperties.AddPropertyName("Fa188");
                _fa188 = value;
            }
        }

        /// <summary>FA189: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA189")]
        public String Fa189 {
            get { return _fa189; }
            set {
                __modifiedProperties.AddPropertyName("Fa189");
                _fa189 = value;
            }
        }

        /// <summary>FA190: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA190")]
        public String Fa190 {
            get { return _fa190; }
            set {
                __modifiedProperties.AddPropertyName("Fa190");
                _fa190 = value;
            }
        }

        /// <summary>FA191: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA191")]
        public String Fa191 {
            get { return _fa191; }
            set {
                __modifiedProperties.AddPropertyName("Fa191");
                _fa191 = value;
            }
        }

        /// <summary>FA192: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA192")]
        public String Fa192 {
            get { return _fa192; }
            set {
                __modifiedProperties.AddPropertyName("Fa192");
                _fa192 = value;
            }
        }

        /// <summary>FA193: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA193")]
        public String Fa193 {
            get { return _fa193; }
            set {
                __modifiedProperties.AddPropertyName("Fa193");
                _fa193 = value;
            }
        }

        /// <summary>FA194: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA194")]
        public String Fa194 {
            get { return _fa194; }
            set {
                __modifiedProperties.AddPropertyName("Fa194");
                _fa194 = value;
            }
        }

        /// <summary>FA195: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA195")]
        public String Fa195 {
            get { return _fa195; }
            set {
                __modifiedProperties.AddPropertyName("Fa195");
                _fa195 = value;
            }
        }

        /// <summary>FA196: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA196")]
        public String Fa196 {
            get { return _fa196; }
            set {
                __modifiedProperties.AddPropertyName("Fa196");
                _fa196 = value;
            }
        }

        /// <summary>FA197: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA197")]
        public String Fa197 {
            get { return _fa197; }
            set {
                __modifiedProperties.AddPropertyName("Fa197");
                _fa197 = value;
            }
        }

        /// <summary>FA198: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA198")]
        public String Fa198 {
            get { return _fa198; }
            set {
                __modifiedProperties.AddPropertyName("Fa198");
                _fa198 = value;
            }
        }

        /// <summary>FA199: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA199")]
        public String Fa199 {
            get { return _fa199; }
            set {
                __modifiedProperties.AddPropertyName("Fa199");
                _fa199 = value;
            }
        }

        /// <summary>FA200: {NVARCHAR2(2000)}</summary>
        [Seasar.Dao.Attrs.Column("FA200")]
        public String Fa200 {
            get { return _fa200; }
            set {
                __modifiedProperties.AddPropertyName("Fa200");
                _fa200 = value;
            }
        }

        #endregion
    }
}
