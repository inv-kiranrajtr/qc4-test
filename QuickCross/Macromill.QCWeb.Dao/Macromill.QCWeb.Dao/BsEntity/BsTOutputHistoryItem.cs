

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
    /// The entity of T_OUTPUT_HISTORY_ITEM as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_HISTORY_ITEM_ID
    /// 
    /// [column]
    ///     OUTPUT_HISTORY_ITEM_ID, QCWEBID, ITEM_INFO_ID, SORT_NO, OUTPUT_FLAG
    /// 
    /// [sequence]
    ///     T_Output_History_Item_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_OUTPUT_SETTING
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tOutputSetting
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_HISTORY_ITEM")]
    [System.Serializable]
    public partial class TOutputHistoryItem : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_HISTORY_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _outputHistoryItemId;

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_SETTING}</summary>
        protected decimal? _qcwebid;

        /// <summary>ITEM_INFO_ID: {NotNull, NUMBER(27)}</summary>
        protected decimal? _itemInfoId;

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _sortNo;

        /// <summary>OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        protected int? _outputFlag;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_HISTORY_ITEM"; } }
        public String TablePropertyName { get { return "TOutputHistoryItem"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.Flag OutputFlagAsFlag { get {
            return CDef.Flag.CodeOf(_outputFlag);
        } set {
            OutputFlag = value != null ? int.Parse(value.Code) : (int?)null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of outputFlag as True.
        /// <![CDATA[
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public void SetOutputFlag_True() {
            OutputFlagAsFlag = CDef.Flag.True;
        }

        /// <summary>
        /// Set the value of outputFlag as False.
        /// <![CDATA[
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public void SetOutputFlag_False() {
            OutputFlagAsFlag = CDef.Flag.False;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of outputFlag 'True'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// はい: 有効を示す
        /// ]]>
        /// </summary>
        public bool IsOutputFlagTrue {
            get {
                CDef.Flag cls = OutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.True) : false;
            }
        }

        /// <summary>
        /// Is the value of outputFlag 'False'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// いいえ: 無効を示す
        /// ]]>
        /// </summary>
        public bool IsOutputFlagFalse {
            get {
                CDef.Flag cls = OutputFlagAsFlag;
                return cls != null ? cls.Equals(CDef.Flag.False) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String OutputFlagName {
            get {
                CDef.Flag cls = OutputFlagAsFlag;
                return cls != null ? cls.Name : null;
            }
        }
        public String OutputFlagAlias {
            get {
                CDef.Flag cls = OutputFlagAsFlag;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TOutputSetting _tOutputSetting;

        /// <summary>T_OUTPUT_SETTING as 'TOutputSetting'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TOutputSetting TOutputSetting {
            get { return _tOutputSetting; }
            set { _tOutputSetting = value; }
        }

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
                if (_outputHistoryItemId == null) { return false; }
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
            if (other == null || !(other is TOutputHistoryItem)) { return false; }
            TOutputHistoryItem otherEntity = (TOutputHistoryItem)other;
            if (!xSV(this.OutputHistoryItemId, otherEntity.OutputHistoryItemId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputHistoryItemId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputHistoryItem:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tOutputSetting != null)
            { sb.Append(l).Append(xbRDS(_tOutputSetting, "TOutputSetting")); }
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
            sb.Append(c).Append(this.OutputHistoryItemId);
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.SortNo);
            sb.Append(c).Append(this.OutputFlag);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tOutputSetting != null) { sb.Append(c).Append("TOutputSetting"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_HISTORY_ITEM_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_HISTORY_ITEM_ID")]
        public decimal? OutputHistoryItemId {
            get { return _outputHistoryItemId; }
            set {
                __modifiedProperties.AddPropertyName("OutputHistoryItemId");
                _outputHistoryItemId = value;
            }
        }

        /// <summary>QCWEBID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_SETTING}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>ITEM_INFO_ID: {NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_INFO_ID")]
        public decimal? ItemInfoId {
            get { return _itemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ItemInfoId");
                _itemInfoId = value;
            }
        }

        /// <summary>SORT_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SORT_NO")]
        public int? SortNo {
            get { return _sortNo; }
            set {
                __modifiedProperties.AddPropertyName("SortNo");
                _sortNo = value;
            }
        }

        /// <summary>OUTPUT_FLAG: {NotNull, NUMBER(1), default=[0], classification=Flag}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_FLAG")]
        public int? OutputFlag {
            get { return _outputFlag; }
            set {
                __modifiedProperties.AddPropertyName("OutputFlag");
                _outputFlag = value;
            }
        }

        #endregion
    }
}
