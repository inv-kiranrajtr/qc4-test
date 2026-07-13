

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
    /// The entity of T_SCENARIO_QUERYLIST as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     SCENARIO_QUERYLIST_ID
    /// 
    /// [column]
    ///     SCENARIO_QUERYLIST_ID, SCENARIO_TOTALIZATION_ID, SEQ_NO, ITEM_INFO_ID, OPERATION_CODE, CONDITION_STRING
    /// 
    /// [sequence]
    ///     T_Scenario_QueryList_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_SCENARIO_TOTALIZATION, T_ITEM_INFO
    /// 
    /// [referrer-table]
    ///     
    /// 
    /// [foreign-property]
    ///     tScenarioTotalization, tItemInfo
    /// 
    /// [referrer-property]
    ///     
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_SCENARIO_QUERYLIST")]
    [System.Serializable]
    public partial class TScenarioQuerylist : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>SCENARIO_QUERYLIST_ID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _scenarioQuerylistId;

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        protected decimal? _scenarioTotalizationId;

        /// <summary>SEQ_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _seqNo;

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        protected decimal? _itemInfoId;

        /// <summary>OPERATION_CODE: {NotNull, CHAR(1), classification=OperationCode}</summary>
        protected String _operationCode;

        /// <summary>CONDITION_STRING: {NotNull, VARCHAR2(1000)}</summary>
        protected String _conditionString;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_SCENARIO_QUERYLIST"; } }
        public String TablePropertyName { get { return "TScenarioQuerylist"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                         Classification Property
        //                                                         =======================
        #region Classification Property
        public CDef.OperationCode OperationCodeAsOperationCode { get {
            return CDef.OperationCode.CodeOf(_operationCode);
        } set {
            OperationCode = value != null ? value.Code : null;
        }}

        #endregion

        // ===============================================================================
        //                                                          Classification Setting
        //                                                          ======================
        #region Classification Setting
        /// <summary>
        /// Set the value of operationCode as Equal.
        /// <![CDATA[
        /// =: Equal(=)を示す
        /// ]]>
        /// </summary>
        public void SetOperationCode_Equal() {
            OperationCodeAsOperationCode = CDef.OperationCode.Equal;
        }

        /// <summary>
        /// Set the value of operationCode as NotEqual.
        /// <![CDATA[
        /// <>: NotEqual(<>)を示す
        /// ]]>
        /// </summary>
        public void SetOperationCode_NotEqual() {
            OperationCodeAsOperationCode = CDef.OperationCode.NotEqual;
        }

        /// <summary>
        /// Set the value of operationCode as LessThan.
        /// <![CDATA[
        /// <: LessThan(<)を示す
        /// ]]>
        /// </summary>
        public void SetOperationCode_LessThan() {
            OperationCodeAsOperationCode = CDef.OperationCode.LessThan;
        }

        /// <summary>
        /// Set the value of operationCode as GreaterThan.
        /// <![CDATA[
        /// >: GreaterThan(>)を示す
        /// ]]>
        /// </summary>
        public void SetOperationCode_GreaterThan() {
            OperationCodeAsOperationCode = CDef.OperationCode.GreaterThan;
        }

        /// <summary>
        /// Set the value of operationCode as LessEqual.
        /// <![CDATA[
        /// <=: LessEqual(<=)を示す
        /// ]]>
        /// </summary>
        public void SetOperationCode_LessEqual() {
            OperationCodeAsOperationCode = CDef.OperationCode.LessEqual;
        }

        /// <summary>
        /// Set the value of operationCode as GreaterEqual.
        /// <![CDATA[
        /// >=: GreaterEqual(>=)を示す
        /// ]]>
        /// </summary>
        public void SetOperationCode_GreaterEqual() {
            OperationCodeAsOperationCode = CDef.OperationCode.GreaterEqual;
        }

        #endregion

        // ===============================================================================
        //                                                    Classification Determination
        //                                                    ============================
        #region Classification Determination
        /// <summary>
        /// Is the value of operationCode 'Equal'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// =: Equal(=)を示す
        /// ]]>
        /// </summary>
        public bool IsOperationCodeEqual {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Equals(CDef.OperationCode.Equal) : false;
            }
        }

        /// <summary>
        /// Is the value of operationCode 'NotEqual'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// <>: NotEqual(<>)を示す
        /// ]]>
        /// </summary>
        public bool IsOperationCodeNotEqual {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Equals(CDef.OperationCode.NotEqual) : false;
            }
        }

        /// <summary>
        /// Is the value of operationCode 'LessThan'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// <: LessThan(<)を示す
        /// ]]>
        /// </summary>
        public bool IsOperationCodeLessThan {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Equals(CDef.OperationCode.LessThan) : false;
            }
        }

        /// <summary>
        /// Is the value of operationCode 'GreaterThan'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// >: GreaterThan(>)を示す
        /// ]]>
        /// </summary>
        public bool IsOperationCodeGreaterThan {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Equals(CDef.OperationCode.GreaterThan) : false;
            }
        }

        /// <summary>
        /// Is the value of operationCode 'LessEqual'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// <=: LessEqual(<=)を示す
        /// ]]>
        /// </summary>
        public bool IsOperationCodeLessEqual {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Equals(CDef.OperationCode.LessEqual) : false;
            }
        }

        /// <summary>
        /// Is the value of operationCode 'GreaterEqual'?
        /// <![CDATA[
        /// The difference of capital letters and small letters is NOT distinguished.
        /// If the value is null, this method returns false!
        /// >=: GreaterEqual(>=)を示す
        /// ]]>
        /// </summary>
        public bool IsOperationCodeGreaterEqual {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Equals(CDef.OperationCode.GreaterEqual) : false;
            }
        }

        #endregion

        // ===============================================================================
        //                                                       Classification Name/Alias
        //                                                       =========================
        #region Classification Name/Alias
        public String OperationCodeName {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Name : null;
            }
        }
        public String OperationCodeAlias {
            get {
                CDef.OperationCode cls = OperationCodeAsOperationCode;
                return cls != null ? cls.Alias : null;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TScenarioTotalization _tScenarioTotalization;

        /// <summary>T_SCENARIO_TOTALIZATION as 'TScenarioTotalization'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("SCENARIO_TOTALIZATION_ID:SCENARIO_TOTALIZATION_ID")]
        public TScenarioTotalization TScenarioTotalization {
            get { return _tScenarioTotalization; }
            set { _tScenarioTotalization = value; }
        }

        protected TItemInfo _tItemInfo;

        /// <summary>T_ITEM_INFO as 'TItemInfo'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("ITEM_INFO_ID:ITEM_INFO_ID")]
        public TItemInfo TItemInfo {
            get { return _tItemInfo; }
            set { _tItemInfo = value; }
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
                if (_scenarioQuerylistId == null) { return false; }
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
            if (other == null || !(other is TScenarioQuerylist)) { return false; }
            TScenarioQuerylist otherEntity = (TScenarioQuerylist)other;
            if (!xSV(this.ScenarioQuerylistId, otherEntity.ScenarioQuerylistId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _scenarioQuerylistId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TScenarioQuerylist:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tScenarioTotalization != null)
            { sb.Append(l).Append(xbRDS(_tScenarioTotalization, "TScenarioTotalization")); }
            if (_tItemInfo != null)
            { sb.Append(l).Append(xbRDS(_tItemInfo, "TItemInfo")); }
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
            sb.Append(c).Append(this.ScenarioQuerylistId);
            sb.Append(c).Append(this.ScenarioTotalizationId);
            sb.Append(c).Append(this.SeqNo);
            sb.Append(c).Append(this.ItemInfoId);
            sb.Append(c).Append(this.OperationCode);
            sb.Append(c).Append(this.ConditionString);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tScenarioTotalization != null) { sb.Append(c).Append("TScenarioTotalization"); }
            if (_tItemInfo != null) { sb.Append(c).Append("TItemInfo"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>SCENARIO_QUERYLIST_ID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_QUERYLIST_ID")]
        public decimal? ScenarioQuerylistId {
            get { return _scenarioQuerylistId; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioQuerylistId");
                _scenarioQuerylistId = value;
            }
        }

        /// <summary>SCENARIO_TOTALIZATION_ID: {IX, NotNull, NUMBER(27), FK to T_SCENARIO_TOTALIZATION}</summary>
        [Seasar.Dao.Attrs.Column("SCENARIO_TOTALIZATION_ID")]
        public decimal? ScenarioTotalizationId {
            get { return _scenarioTotalizationId; }
            set {
                __modifiedProperties.AddPropertyName("ScenarioTotalizationId");
                _scenarioTotalizationId = value;
            }
        }

        /// <summary>SEQ_NO: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("SEQ_NO")]
        public int? SeqNo {
            get { return _seqNo; }
            set {
                __modifiedProperties.AddPropertyName("SeqNo");
                _seqNo = value;
            }
        }

        /// <summary>ITEM_INFO_ID: {IX, NotNull, NUMBER(27), FK to T_ITEM_INFO}</summary>
        [Seasar.Dao.Attrs.Column("ITEM_INFO_ID")]
        public decimal? ItemInfoId {
            get { return _itemInfoId; }
            set {
                __modifiedProperties.AddPropertyName("ItemInfoId");
                _itemInfoId = value;
            }
        }

        /// <summary>OPERATION_CODE: {NotNull, CHAR(1), classification=OperationCode}</summary>
        [Seasar.Dao.Attrs.Column("OPERATION_CODE")]
        public String OperationCode {
            get { return _operationCode; }
            set {
                __modifiedProperties.AddPropertyName("OperationCode");
                _operationCode = value;
            }
        }

        /// <summary>CONDITION_STRING: {NotNull, VARCHAR2(1000)}</summary>
        [Seasar.Dao.Attrs.Column("CONDITION_STRING")]
        public String ConditionString {
            get { return _conditionString; }
            set {
                __modifiedProperties.AddPropertyName("ConditionString");
                _conditionString = value;
            }
        }

        #endregion
    }
}
