

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
    /// The entity of T_COLOR_SET_INFO_CROSS as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     COLOR_SET_INFO_CROSS_ID
    /// 
    /// [column]
    ///     COLOR_SET_INFO_CROSS_ID, TYPE_CODE, GRADATION_TYPE, CROSS_SCENARIO_TARGET_ID
    /// 
    /// [sequence]
    ///     T_Color_Set_Info_Cross_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_CROSS_SCENARIO_TARGET
    /// 
    /// [referrer-table]
    ///     T_COLOR_INFO_DETAIL_CROSS
    /// 
    /// [foreign-property]
    ///     tCrossScenarioTarget
    /// 
    /// [referrer-property]
    ///     tColorInfoDetailCrossList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_COLOR_SET_INFO_CROSS")]
    [System.Serializable]
    public partial class TColorSetInfoCross : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>COLOR_SET_INFO_CROSS_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _colorSetInfoCrossId;

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        protected String _typeCode;

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        protected String _gradationType;

        /// <summary>CROSS_SCENARIO_TARGET_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_CROSS_SCENARIO_TARGET}</summary>
        protected decimal? _crossScenarioTargetId;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_COLOR_SET_INFO_CROSS"; } }
        public String TablePropertyName { get { return "TColorSetInfoCross"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TCrossScenarioTarget _tCrossScenarioTarget;

        /// <summary>T_CROSS_SCENARIO_TARGET as 'TCrossScenarioTarget'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("CROSS_SCENARIO_TARGET_ID:CROSS_SCENARIO_TARGET_ID")]
        public TCrossScenarioTarget TCrossScenarioTarget {
            get { return _tCrossScenarioTarget; }
            set { _tCrossScenarioTarget = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TColorInfoDetailCross> _tColorInfoDetailCrossList;

        /// <summary>T_COLOR_INFO_DETAIL_CROSS as 'TColorInfoDetailCrossList'.</summary>
        public IList<TColorInfoDetailCross> TColorInfoDetailCrossList {
            get { if (_tColorInfoDetailCrossList == null) { _tColorInfoDetailCrossList = new List<TColorInfoDetailCross>(); } return _tColorInfoDetailCrossList; }
            set { _tColorInfoDetailCrossList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_colorSetInfoCrossId == null) { return false; }
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
            if (other == null || !(other is TColorSetInfoCross)) { return false; }
            TColorSetInfoCross otherEntity = (TColorSetInfoCross)other;
            if (!xSV(this.ColorSetInfoCrossId, otherEntity.ColorSetInfoCrossId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _colorSetInfoCrossId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TColorSetInfoCross:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tCrossScenarioTarget != null)
            { sb.Append(l).Append(xbRDS(_tCrossScenarioTarget, "TCrossScenarioTarget")); }
            if (_tColorInfoDetailCrossList != null) { foreach (Entity e in _tColorInfoDetailCrossList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TColorInfoDetailCrossList")); } } }
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
            sb.Append(c).Append(this.ColorSetInfoCrossId);
            sb.Append(c).Append(this.TypeCode);
            sb.Append(c).Append(this.GradationType);
            sb.Append(c).Append(this.CrossScenarioTargetId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tCrossScenarioTarget != null) { sb.Append(c).Append("TCrossScenarioTarget"); }
            if (_tColorInfoDetailCrossList != null && _tColorInfoDetailCrossList.Count > 0)
            { sb.Append(c).Append("TColorInfoDetailCrossList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>COLOR_SET_INFO_CROSS_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("COLOR_SET_INFO_CROSS_ID")]
        public decimal? ColorSetInfoCrossId {
            get { return _colorSetInfoCrossId; }
            set {
                __modifiedProperties.AddPropertyName("ColorSetInfoCrossId");
                _colorSetInfoCrossId = value;
            }
        }

        /// <summary>TYPE_CODE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("TYPE_CODE")]
        public String TypeCode {
            get { return _typeCode; }
            set {
                __modifiedProperties.AddPropertyName("TypeCode");
                _typeCode = value;
            }
        }

        /// <summary>GRADATION_TYPE: {NotNull, VARCHAR2(3)}</summary>
        [Seasar.Dao.Attrs.Column("GRADATION_TYPE")]
        public String GradationType {
            get { return _gradationType; }
            set {
                __modifiedProperties.AddPropertyName("GradationType");
                _gradationType = value;
            }
        }

        /// <summary>CROSS_SCENARIO_TARGET_ID: {IX, NotNull, NUMBER(27), default=[0], FK to T_CROSS_SCENARIO_TARGET}</summary>
        [Seasar.Dao.Attrs.Column("CROSS_SCENARIO_TARGET_ID")]
        public decimal? CrossScenarioTargetId {
            get { return _crossScenarioTargetId; }
            set {
                __modifiedProperties.AddPropertyName("CrossScenarioTargetId");
                _crossScenarioTargetId = value;
            }
        }

        #endregion
    }
}
