

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
    /// The entity of T_APPLICATION as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     IDENTIFIER
    /// 
    /// [column]
    ///     IDENTIFIER, SETTING_VALUE, DESCRIPTION
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
    [Seasar.Dao.Attrs.Table("T_APPLICATION")]
    [System.Serializable]
    public partial class TApplication : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>IDENTIFIER: {PK, NotNull, VARCHAR2(35)}</summary>
        protected String _identifier;

        /// <summary>SETTING_VALUE: {NotNull, VARCHAR2(90)}</summary>
        protected String _settingValue;

        /// <summary>DESCRIPTION: {VARCHAR2(150)}</summary>
        protected String _description;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_APPLICATION"; } }
        public String TablePropertyName { get { return "TApplication"; } }

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
                if (_identifier == null) { return false; }
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
            if (other == null || !(other is TApplication)) { return false; }
            TApplication otherEntity = (TApplication)other;
            if (!xSV(this.Identifier, otherEntity.Identifier)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _identifier);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TApplication:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.Identifier);
            sb.Append(c).Append(this.SettingValue);
            sb.Append(c).Append(this.Description);
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
        /// <summary>IDENTIFIER: {PK, NotNull, VARCHAR2(35)}</summary>
        [Seasar.Dao.Attrs.Column("IDENTIFIER")]
        public String Identifier {
            get { return _identifier; }
            set {
                __modifiedProperties.AddPropertyName("Identifier");
                _identifier = value;
            }
        }

        /// <summary>SETTING_VALUE: {NotNull, VARCHAR2(90)}</summary>
        [Seasar.Dao.Attrs.Column("SETTING_VALUE")]
        public String SettingValue {
            get { return _settingValue; }
            set {
                __modifiedProperties.AddPropertyName("SettingValue");
                _settingValue = value;
            }
        }

        /// <summary>DESCRIPTION: {VARCHAR2(150)}</summary>
        [Seasar.Dao.Attrs.Column("DESCRIPTION")]
        public String Description {
            get { return _description; }
            set {
                __modifiedProperties.AddPropertyName("Description");
                _description = value;
            }
        }

        #endregion
    }
}
