

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
    /// The entity of T_TABLE_CONTROL as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     QCWEBID
    /// 
    /// [column]
    ///     QCWEBID, BASE_TABLE_NAME, ACTIVE_TABLE_NO, MAX_NO
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
    ///     T_QCWEB_SURVEY_INFO(AsOne)
    /// 
    /// [referrer-table]
    ///     T_TABLE_DETAIL_INFO, T_ITEM_INFO, T_QCWEB_SURVEY_INFO
    /// 
    /// [foreign-property]
    ///     tQcwebSurveyInfoAsOne
    /// 
    /// [referrer-property]
    ///     tTableDetailInfoList, tItemInfoList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_TABLE_CONTROL")]
    [System.Serializable]
    public partial class TTableControl : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27)}</summary>
        protected decimal? _qcwebid;

        /// <summary>BASE_TABLE_NAME: {NotNull, VARCHAR2(25)}</summary>
        protected String _baseTableName;

        /// <summary>ACTIVE_TABLE_NO: {NotNull, NUMBER(2)}</summary>
        protected int? _activeTableNo;

        /// <summary>MAX_NO: {NotNull, NUMBER(2)}</summary>
        protected int? _maxNo;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_TABLE_CONTROL"; } }
        public String TablePropertyName { get { return "TTableControl"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TQcwebSurveyInfo _tQcwebSurveyInfoAsOne;

        /// <summary>T_QCWEB_SURVEY_INFO as 'TQcwebSurveyInfoAsOne'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("QCWEBID:QCWEBID")]
        public TQcwebSurveyInfo TQcwebSurveyInfoAsOne {
            get { return _tQcwebSurveyInfoAsOne; }
            set { _tQcwebSurveyInfoAsOne = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TTableDetailInfo> _tTableDetailInfoList;

        /// <summary>T_TABLE_DETAIL_INFO as 'TTableDetailInfoList'.</summary>
        public IList<TTableDetailInfo> TTableDetailInfoList {
            get { if (_tTableDetailInfoList == null) { _tTableDetailInfoList = new List<TTableDetailInfo>(); } return _tTableDetailInfoList; }
            set { _tTableDetailInfoList = value; }
        }

        protected IList<TItemInfo> _tItemInfoList;

        /// <summary>T_ITEM_INFO as 'TItemInfoList'.</summary>
        public IList<TItemInfo> TItemInfoList {
            get { if (_tItemInfoList == null) { _tItemInfoList = new List<TItemInfo>(); } return _tItemInfoList; }
            set { _tItemInfoList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_qcwebid == null) { return false; }
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
            if (other == null || !(other is TTableControl)) { return false; }
            TTableControl otherEntity = (TTableControl)other;
            if (!xSV(this.Qcwebid, otherEntity.Qcwebid)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _qcwebid);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TTableControl:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tQcwebSurveyInfoAsOne != null)
            { sb.Append(l).Append(xbRDS(_tQcwebSurveyInfoAsOne, "TQcwebSurveyInfoAsOne")); }
            if (_tTableDetailInfoList != null) { foreach (Entity e in _tTableDetailInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TTableDetailInfoList")); } } }
            if (_tItemInfoList != null) { foreach (Entity e in _tItemInfoList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TItemInfoList")); } } }
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
            sb.Append(c).Append(this.Qcwebid);
            sb.Append(c).Append(this.BaseTableName);
            sb.Append(c).Append(this.ActiveTableNo);
            sb.Append(c).Append(this.MaxNo);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tQcwebSurveyInfoAsOne != null) { sb.Append(c).Append("TQcwebSurveyInfoAsOne"); }
            if (_tTableDetailInfoList != null && _tTableDetailInfoList.Count > 0)
            { sb.Append(c).Append("TTableDetailInfoList"); }
            if (_tItemInfoList != null && _tItemInfoList.Count > 0)
            { sb.Append(c).Append("TItemInfoList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>QCWEBID: {PK, NotNull, NUMBER(27)}</summary>
        [Seasar.Dao.Attrs.Column("QCWEBID")]
        public decimal? Qcwebid {
            get { return _qcwebid; }
            set {
                __modifiedProperties.AddPropertyName("Qcwebid");
                _qcwebid = value;
            }
        }

        /// <summary>BASE_TABLE_NAME: {NotNull, VARCHAR2(25)}</summary>
        [Seasar.Dao.Attrs.Column("BASE_TABLE_NAME")]
        public String BaseTableName {
            get { return _baseTableName; }
            set {
                __modifiedProperties.AddPropertyName("BaseTableName");
                _baseTableName = value;
            }
        }

        /// <summary>ACTIVE_TABLE_NO: {NotNull, NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("ACTIVE_TABLE_NO")]
        public int? ActiveTableNo {
            get { return _activeTableNo; }
            set {
                __modifiedProperties.AddPropertyName("ActiveTableNo");
                _activeTableNo = value;
            }
        }

        /// <summary>MAX_NO: {NotNull, NUMBER(2)}</summary>
        [Seasar.Dao.Attrs.Column("MAX_NO")]
        public int? MaxNo {
            get { return _maxNo; }
            set {
                __modifiedProperties.AddPropertyName("MaxNo");
                _maxNo = value;
            }
        }

        #endregion
    }
}
