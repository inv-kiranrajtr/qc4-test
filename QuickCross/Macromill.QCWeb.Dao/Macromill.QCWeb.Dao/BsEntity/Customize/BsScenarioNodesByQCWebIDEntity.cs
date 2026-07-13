

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
    /// The entity of ScenarioNodesByQCWebIDEntity. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     
    /// 
    /// [column]
    ///     SCENARIO_TOTALIZATION_ID, SCENARIO_NAME, SCENARIO_TYPE, SORT_NO, WEIGHTBACK_FLAG, ITEM_COUNT, REPORT_COUNT
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
    [Seasar.Dao.Attrs.Table("ScenarioNodesByQCWebIDEntity")]
    [System.Serializable]
    public partial class ScenarioNodesByQCWebIDEntity : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SCENARIO_TOTALIZATION_ID: {NUMBER(27)}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>SCENARIO_NAME: {VARCHAR2(50)}</summary>
        protected String _scenarioName;

        /// <summary>SCENARIO_TYPE: {CHAR(1)}</summary>
        protected String _scenarioType;

        /// <summary>SORT_NO: {NUMBER(5)}</summary>
        protected int? _sortNo;

        /// <summary>WEIGHTBACK_FLAG: {NUMBER(1), classification=Flag}</summary>
        protected int? _weightbackFlag;

        /// <summary>ITEM_COUNT: {NUMBER(22)}</summary>
        protected decimal? _itemCount;

        /// <summary>REPORT_COUNT: {NUMBER(22)}</summary>
        protected decimal? _reportCount;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "ScenarioNodesByQCWebIDEntity"; } }
        public String TablePropertyName { get { return "ScenarioNodesByQCWebIDEntity"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return ScenarioNodesByQCWebIDEntityDbm.GetInstance(); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag WeightbackFlagAsFlag { get {
            return CDef.Flag.CodeOf(_weightbackFlag);
        } set {
            WeightbackFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of weightbackFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetWeightbackFlag_True() {
            WeightbackFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of weightbackFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetWeightbackFlag_False() {
            WeightbackFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of weightbackFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsWeightbackFlagTrue {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of weightbackFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsWeightbackFlagFalse {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String WeightbackFlagName {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String WeightbackFlagAlias {
            get {
                CDef.Flag cls = WeightbackFlagAsFlag;
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
            if (other == null || !(other is ScenarioNodesByQCWebIDEntity)) { return false; }
            ScenarioNodesByQCWebIDEntity otherEntity = (ScenarioNodesByQCWebIDEntity)other;
            if (!xSV(this.ScenarioTotalizationId, otherEntity.ScenarioTotalizationId)) { return false; }
            if (!xSV(this.ScenarioName, otherEntity.ScenarioName)) { return false; }
            if (!xSV(this.ScenarioType, otherEntity.ScenarioType)) { return false; }
            if (!xSV(this.SortNo, otherEntity.SortNo)) { return false; }
            if (!xSV(this.WeightbackFlag, otherEntity.WeightbackFlag)) { return false; }
            if (!xSV(this.ItemCount, otherEntity.ItemCount)) { return false; }
            if (!xSV(this.ReportCount, otherEntity.ReportCount)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _scenarioTotalizationId);
            result = xCH(result, _scenarioName);
            result = xCH(result, _scenarioType);
            result = xCH(result, _sortNo);
            result = xCH(result, _weightbackFlag);
            result = xCH(result, _itemCount);
            result = xCH(result, _reportCount);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "ScenarioNodesByQCWebIDEntity:" + BuildColumnString() + BuildRelationString();
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
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.ScenarioName);
            sb.Append(c).Append(this.ScenarioType);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.WeightbackFlag);
            sb.Append(c).Append(this.ItemCount);
            sb.Append(c).Append(this.ReportCount);
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
        /// <summary>SCENARIO_TOTALIZATION_ID: {NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TOTALIZATION_ID")]
        public decimal? ScenarioTotalizationId {
            get { return _scenarioTotalizationId; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioTotalizationId");
                _scenarioTotalizationId = value;
            }
        }

        /// <summary>SCENARIO_NAME: {VARCHAR2(50)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_NAME")]
        public String ScenarioName {
            get { return _scenarioName; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioName");
                _scenarioName = value;
            }
        }

        /// <summary>SCENARIO_TYPE: {CHAR(1)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TYPE")]
        public String ScenarioType {
            get { return _scenarioType; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioType");
                _scenarioType = value;
            }
        }

        /// <summary>SORT_NO: {NUMBER(5)}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public int? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>WEIGHTBACK_FLAG: {NUMBER(1), classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("WEIGHTBACK_FLAG")]
        public int? WeightbackFlag {
            get { return _weightbackFlag; }
            set {
                __modifiedProperties.AddPropertyName("WeightbackFlag");
                _weightbackFlag = value;
            }
        }

        /// <summary>ITEM_COUNT: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_COUNT")]
        public decimal? ItemCount {
            get { return _itemCount; }
            set {
                __modifiedProperties.AddPropertyName("ItemCount");
                _itemCount = value;
            }
        }

        /// <summary>REPORT_COUNT: {NUMBER(22)}</summary>
        [Seasar.Dao.Attrs.Column("REPORT_COUNT")]
        public decimal? ReportCount {
            get { return _reportCount; }
            set {
                __modifiedProperties.AddPropertyName("ReportCount");
                _reportCount = value;
            }
        }

        #endregion
    }
}
