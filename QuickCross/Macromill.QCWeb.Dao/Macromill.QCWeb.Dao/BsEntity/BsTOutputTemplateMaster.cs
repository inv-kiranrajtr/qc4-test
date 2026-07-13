

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
    /// The entity of T_OUTPUT_TEMPLATE_MASTER as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_TEMPLATE_MASTER_ID
    /// 
    /// [column]
    ///     OUTPUT_TEMPLATE_MASTER_ID, PATH, MD5_HASH
    /// 
    /// [sequence]
    ///     T_Output_Template_Master_SEQ01
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
    ///     T_OUTPUT_TEMPLATE
    /// 
    /// [foreign-property]
    ///     
    /// 
    /// [referrer-property]
    ///     tOutputTemplateList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_TEMPLATE_MASTER")]
    [System.Serializable]
    public partial class TOutputTemplateMaster : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_TEMPLATE_MASTER_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputTemplateMasterId;

        /// <summary>PATH: {NotNull, VARCHAR2(780)}</summary>
        protected String _path;

        /// <summary>MD5_HASH: {NotNull, CHAR(128)}</summary>
        protected String _md5Hash;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_TEMPLATE_MASTER"; } }
        public String TablePropertyName { get { return "TOutputTemplateMaster"; } }

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
        protected IList<TOutputTemplate> _tOutputTemplateList;

        /// <summary>T_OUTPUT_TEMPLATE as 'TOutputTemplateList'.</summary>
        public IList<TOutputTemplate> TOutputTemplateList {
            get { if (_tOutputTemplateList == null) { _tOutputTemplateList = new List<TOutputTemplate>(); } return _tOutputTemplateList; }
            set { _tOutputTemplateList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_outputTemplateMasterId == null) { return false; }
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
            if (other == null || !(other is TOutputTemplateMaster)) { return false; }
            TOutputTemplateMaster otherEntity = (TOutputTemplateMaster)other;
            if (!xSV(this.OutputTemplateMasterId, otherEntity.OutputTemplateMasterId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputTemplateMasterId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputTemplateMaster:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tOutputTemplateList != null) { foreach (Entity e in _tOutputTemplateList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputTemplateList")); } } }
            return sb.ToString();
        }
        protected String xbRDS(Entity e, String name) { // buildRelationDisplayString()
            return e.BuildDisplayString(name, true, true);
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
            sb.Append(c).Append(this.OutputTemplateMasterId);
            sb.Append(c).Append(this.Path);
            sb.Append(c).Append(this.Md5Hash);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tOutputTemplateList != null && _tOutputTemplateList.Count > 0)
            { sb.Append(c).Append("TOutputTemplateList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_TEMPLATE_MASTER_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TEMPLATE_MASTER_ID")]
        public decimal? OutputTemplateMasterId {
            get { return _outputTemplateMasterId; }
            set {
                __modifiedProperties.AddPropertyName("OutputTemplateMasterId");
                _outputTemplateMasterId = value;
            }
        }

        /// <summary>PATH: {NotNull, VARCHAR2(780)}</summary>
        [Seasar.Dao.Attrs.Column("PATH")]
        public String Path {
            get { return _path; }
            set {
                __modifiedProperties.AddPropertyName("Path");
                _path = value;
            }
        }

        /// <summary>MD5_HASH: {NotNull, CHAR(128)}</summary>
        [Seasar.Dao.Attrs.Column("MD5_HASH")]
        public String Md5Hash {
            get { return _md5Hash; }
            set {
                __modifiedProperties.AddPropertyName("Md5Hash");
                _md5Hash = value;
            }
        }

        #endregion
    }
}
