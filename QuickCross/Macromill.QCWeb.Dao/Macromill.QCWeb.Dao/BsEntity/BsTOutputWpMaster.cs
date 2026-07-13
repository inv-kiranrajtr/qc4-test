

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
    /// The entity of T_OUTPUT_WP_MASTER as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_WP_MASTER_ID
    /// 
    /// [column]
    ///     OUTPUT_WP_MASTER_ID, POINT
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
    [Seasar.Dao.Attrs.Table("T_OUTPUT_WP_MASTER")]
    [System.Serializable]
    public partial class TOutputWpMaster : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_WP_MASTER_ID: {PK, NotNull, VARCHAR2(2), classification=OutputWPMasterID}</summary>
        protected String _outputWpMasterId;

        /// <summary>POINT: {NotNull, NUMBER(6)}</summary>
        protected int? _point;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_WP_MASTER"; } }
        public String TablePropertyName { get { return "TOutputWpMaster"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.OutputWPMasterID OutputWpMasterIdAsOutputWPMasterID { get {
            return CDef.OutputWPMasterID.CodeOf(_outputWpMasterId);
        } set {
            OutputWpMasterId = value != null ? value.Code : null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of outputWpMasterId as GT.
        /// <![CDATA[
        /// GT: GTを示す
        /// ]]>
        /// </summary>
        public void SetOutputWpMasterId_GT() {
            OutputWpMasterIdAsOutputWPMasterID = CDef.OutputWPMasterID.GT;
        }

        /// <summary>
        /// Set the value of outputWpMasterId as CROSS.
        /// <![CDATA[
        /// CROSS: CROSSを示す
        /// ]]>
        /// </summary>
        public void SetOutputWpMasterId_CROSS() {
            OutputWpMasterIdAsOutputWPMasterID = CDef.OutputWPMasterID.CROSS;
        }

        /// <summary>
        /// Set the value of outputWpMasterId as FA.
        /// <![CDATA[
        /// FA: FAを示す
        /// ]]>
        /// </summary>
        public void SetOutputWpMasterId_FA() {
            OutputWpMasterIdAsOutputWPMasterID = CDef.OutputWPMasterID.FA;
        }

        /// <summary>
        /// Set the value of outputWpMasterId as DataOutput.
        /// <![CDATA[
        /// データ出力: データ出力を示す
        /// ]]>
        /// </summary>
        public void SetOutputWpMasterId_DataOutput() {
            OutputWpMasterIdAsOutputWPMasterID = CDef.OutputWPMasterID.DataOutput;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of outputWpMasterId 'GT'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// GT: GTを示す
        /// ]]>
        /// </summary>
        public bool IsOutputWpMasterIdGT {
            get {
                CDef.OutputWPMasterID cls = OutputWpMasterIdAsOutputWPMasterID;
                return cls != null ? cls.Equals(CDef.OutputWPMasterID.GT) : false;
            }
        }

        /// <summary>
        /// Is the value of outputWpMasterId 'CROSS'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// CROSS: CROSSを示す
        /// ]]>
        /// </summary>
        public bool IsOutputWpMasterIdCROSS {
            get {
                CDef.OutputWPMasterID cls = OutputWpMasterIdAsOutputWPMasterID;
                return cls != null ? cls.Equals(CDef.OutputWPMasterID.CROSS) : false;
            }
        }

        /// <summary>
        /// Is the value of outputWpMasterId 'FA'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// FA: FAを示す
        /// ]]>
        /// </summary>
        public bool IsOutputWpMasterIdFA {
            get {
                CDef.OutputWPMasterID cls = OutputWpMasterIdAsOutputWPMasterID;
                return cls != null ? cls.Equals(CDef.OutputWPMasterID.FA) : false;
            }
        }

        /// <summary>
        /// Is the value of outputWpMasterId 'DataOutput'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// データ出力: データ出力を示す
        /// ]]>
        /// </summary>
        public bool IsOutputWpMasterIdDataOutput {
            get {
                CDef.OutputWPMasterID cls = OutputWpMasterIdAsOutputWPMasterID;
                return cls != null ? cls.Equals(CDef.OutputWPMasterID.DataOutput) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String OutputWpMasterIdName {
            get {
                CDef.OutputWPMasterID cls = OutputWpMasterIdAsOutputWPMasterID;
                return cls != null ? cls.Name : null;
            }
        }
        public String OutputWpMasterIdAlias {
            get {
                CDef.OutputWPMasterID cls = OutputWpMasterIdAsOutputWPMasterID;
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
                if (_outputWpMasterId == null) { return false; }
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
            if (other == null || !(other is TOutputWpMaster)) { return false; }
            TOutputWpMaster otherEntity = (TOutputWpMaster)other;
            if (!xSV(this.OutputWpMasterId, otherEntity.OutputWpMasterId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputWpMasterId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputWpMaster:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.OutputWpMasterId);
            sb.Append(c).Append(this.Point);
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
        /// <summary>OUTPUT_WP_MASTER_ID: {PK, NotNull, VARCHAR2(2), classification=OutputWPMasterID}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_WP_MASTER_ID")]
        public String OutputWpMasterId {
            get { return _outputWpMasterId; }
            set {
                __modifiedProperties.AddPropertyName("OutputWpMasterId");
                _outputWpMasterId = value;
            }
        }

        /// <summary>POINT: {NotNull, NUMBER(6)}</summary>
        [Seasar.Dao.Attrs.Column("POINT")]
        public int? Point {
            get { return _point; }
            set {
                __modifiedProperties.AddPropertyName("Point");
                _point = value;
            }
        }

        #endregion
    }
}
