

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Helper;
using Macromill.QCWeb.Dao.ExEntity.Customize;
using Macromill.QCWeb.Dao.BsEntity.Customize.Dbm;


namespace Macromill.QCWeb.Dao.ExEntity.Customize {

    /// <summary>
    /// The entity of ScenarioSelectItemBeingUsedEntity. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     SCENARIOID, SCENARIOTYPE, SCENARIONAME, WB, WBCODE, QUERYLIST, TARGET, AXIS, FAADD
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
    [Seasar.Dao.Attrs.Table("ScenarioSelectItemBeingUsedEntity")]
    [System.Serializable]
    public partial class ScenarioSelectItemBeingUsedEntity : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SCENARIOID: {NUMBER(22)}</summary>
        protected decimal? _scenarioid;

        /// <summary>SCENARIOTYPE: {VARCHAR2(1)}</summary>
        protected String _scenariotype;

        /// <summary>SCENARIONAME: {VARCHAR2(50)}</summary>
        protected String _scenarioname;

        /// <summary>WB: {NUMBER(22)}</summary>
        protected decimal? _wb;

        /// <summary>WBCODE: {NUMBER(22)}</summary>
        protected decimal? _wbcode;

        /// <summary>QUERYLIST: {NUMBER(22)}</summary>
        protected decimal? _querylist;

        /// <summary>TARGET: {NUMBER(22)}</summary>
        protected decimal? _target;

        /// <summary>AXIS: {NUMBER(22)}</summary>
        protected decimal? _axis;

        /// <summary>FAADD: {NUMBER(22)}</summary>
        protected decimal? _faadd;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "ScenarioSelectItemBeingUsedEntity"; } }
        public String TablePropertyName { get { return "ScenarioSelectItemBeingUsedEntity"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return ScenarioSelectItemBeingUsedEntityDbm.GetInstance(); } }

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
                return false;
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
            if (other == null || !(other is ScenarioSelectItemBeingUsedEntity)) { return false; }
            ScenarioSelectItemBeingUsedEntity otherEntity = (ScenarioSelectItemBeingUsedEntity)other;
            if (!xSV(this.Scenarioid, otherEntity.Scenarioid)) { return false; }
            if (!xSV(this.Scenariotype, otherEntity.Scenariotype)) { return false; }
            if (!xSV(this.Scenarioname, otherEntity.Scenarioname)) { return false; }
            if (!xSV(this.Wb, otherEntity.Wb)) { return false; }
            if (!xSV(this.Wbcode, otherEntity.Wbcode)) { return false; }
            if (!xSV(this.Querylist, otherEntity.Querylist)) { return false; }
            if (!xSV(this.Target, otherEntity.Target)) { return false; }
            if (!xSV(this.Axis, otherEntity.Axis)) { return false; }
            if (!xSV(this.Faadd, otherEntity.Faadd)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _scenarioid);
            result = xCH(result, _scenariotype);
            result = xCH(result, _scenarioname);
            result = xCH(result, _wb);
            result = xCH(result, _wbcode);
            result = xCH(result, _querylist);
            result = xCH(result, _target);
            result = xCH(result, _axis);
            result = xCH(result, _faadd);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "ScenarioSelectItemBeingUsedEntity:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.Scenarioid);
            sb.Append(c).Append(this.Scenariotype);
            sb.Append(c).Append(this.Scenarioname);
            sb.Append(c).Append(this.Wb);
            sb.Append(c).Append(this.Wbcode);
            sb.Append(c).Append(this.Querylist);
            sb.Append(c).Append(this.Target);
            sb.Append(c).Append(this.Axis);
            sb.Append(c).Append(this.Faadd);
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
        /// <summary>SCENARIOID: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIOID")]
        public decimal? Scenarioid {
            get { return _scenarioid; }
            set {
                __modifiedProperties.AddPropertyName("Scenarioid");
                _scenarioid = value;
            }
        }

        /// <summary>SCENARIOTYPE: {VARCHAR2(1)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIOTYPE")]
        public String Scenariotype {
            get { return _scenariotype; }
            set {
                __modifiedProperties.AddPropertyName("Scenariotype");
                _scenariotype = value;
            }
        }

        /// <summary>SCENARIONAME: {VARCHAR2(50)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIONAME")]
        public String Scenarioname {
            get { return _scenarioname; }
            set {
                __modifiedProperties.AddPropertyName("Scenarioname");
                _scenarioname = value;
            }
        }

        /// <summary>WB: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("WB")]
        public decimal? Wb {
            get { return _wb; }
            set {
                __modifiedProperties.AddPropertyName("Wb");
                _wb = value;
            }
        }

        /// <summary>WBCODE: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("WBCODE")]
        public decimal? Wbcode {
            get { return _wbcode; }
            set {
                __modifiedProperties.AddPropertyName("Wbcode");
                _wbcode = value;
            }
        }

        /// <summary>QUERYLIST: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("QUERYLIST")]
        public decimal? Querylist {
            get { return _querylist; }
            set {
                __modifiedProperties.AddPropertyName("Querylist");
                _querylist = value;
            }
        }

        /// <summary>TARGET: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("TARGET")]
        public decimal? Target {
            get { return _target; }
            set {
                __modifiedProperties.AddPropertyName("Target");
                _target = value;
            }
        }

        /// <summary>AXIS: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("AXIS")]
        public decimal? Axis {
            get { return _axis; }
            set {
                __modifiedProperties.AddPropertyName("Axis");
                _axis = value;
            }
        }

        /// <summary>FAADD: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("FAADD")]
        public decimal? Faadd {
            get { return _faadd; }
            set {
                __modifiedProperties.AddPropertyName("Faadd");
                _faadd = value;
            }
        }

        #endregion
    }
}
