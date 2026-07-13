

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
    /// The entity of T_OUTPUT_COMMON as TABLE. (partial class for auto-generation)
    /// <![CDATA[
    /// [primary-key]
    ///     OUTPUT_COMMON_ID
    /// 
    /// [column]
    ///     OUTPUT_COMMON_ID, ORDER_COUNT, TSV_FILE_PATH, EXCELBOOK_NAME_PREFIX, PROCESS_START_DATETIME, PROCESS_FORECAST_END_DATETIME, PROCESS_END_DATETIME, STATUS_CODE, DESCRIPTION, OUTPUT_TYPE, OUTPUT_REQUEST_ID, WB_SETTING_CODE, NOANSWER_VISIBLE_CODE, UNMATCH_VISIBLE_CODE
    /// 
    /// [sequence]
    ///     T_Output_Common_SEQ_01
    /// 
    /// [identity]
    ///     
    /// 
    /// [version-no]
    ///     
    /// 
    /// [foreign-table]
    ///     T_OUTPUT_REQUEST, T_Output_Sub_GT, T_Output_Sub_Cross, T_Output_Sub_FA, T_Output_Sub_CKList
    /// 
    /// [referrer-table]
    ///     T_OUTPUT_SUB_CKLIST, T_OUTPUT_SUB_CROSS, T_OUTPUT_SUB_FA, T_OUTPUT_SUB_GT
    /// 
    /// [foreign-property]
    ///     tOutputRequest, tOutputSubGt, tOutputSubCross, tOutputSubFa, tOutputSubCklist
    /// 
    /// [referrer-property]
    ///     tOutputSubCklistList, tOutputSubCrossList, tOutputSubFaList, tOutputSubGtList
    /// ]]>
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [Seasar.Dao.Attrs.Table("T_OUTPUT_COMMON")]
    [System.Serializable]
    public partial class TOutputCommon : Entity {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        #region Attribute
        /// <summary>OUTPUT_COMMON_ID: {PK, NotNull, NUMBER(27), FK to T_Output_Sub_GT}</summary>
        protected decimal? _outputCommonId;

        /// <summary>ORDER_COUNT: {NotNull, NUMBER(10), default=[1]}</summary>
        protected long? _orderCount;

        /// <summary>TSV_FILE_PATH: {NCLOB(4000)}</summary>
        protected String _tsvFilePath;

        /// <summary>EXCELBOOK_NAME_PREFIX: {VARCHAR2(100)}</summary>
        protected String _excelbookNamePrefix;

        /// <summary>PROCESS_START_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _processStartDatetime;

        /// <summary>PROCESS_FORECAST_END_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _processForecastEndDatetime;

        /// <summary>PROCESS_END_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        protected DateTime? _processEndDatetime;

        /// <summary>STATUS_CODE: {NotNull, NUMBER(5), default=[0]}</summary>
        protected int? _statusCode;

        /// <summary>DESCRIPTION: {NVARCHAR2(256)}</summary>
        protected String _description;

        /// <summary>OUTPUT_TYPE: {NotNull, NUMBER(1)}</summary>
        protected int? _outputType;

        /// <summary>OUTPUT_REQUEST_ID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_REQUEST}</summary>
        protected decimal? _outputRequestId;

        /// <summary>WB_SETTING_CODE: {NotNull, NUMBER(1), default=[0]}</summary>
        protected int? _wbSettingCode;

        /// <summary>NOANSWER_VISIBLE_CODE: {NUMBER(1)}</summary>
        protected int? _noanswerVisibleCode;

        /// <summary>UNMATCH_VISIBLE_CODE: {NUMBER(1)}</summary>
        protected int? _unmatchVisibleCode;

        protected EntityModifiedProperties __modifiedProperties = new EntityModifiedProperties();
        #endregion

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public String TableDbName { get { return "T_OUTPUT_COMMON"; } }
        public String TablePropertyName { get { return "TOutputCommon"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public DBMeta DBMeta { get { return DBMetaInstanceHandler.FindDBMeta(TableDbName); } }

        // ===============================================================================
        //                                                                Foreign Property
        //                                                                ================
        #region Foreign Property
        protected TOutputRequest _tOutputRequest;

        /// <summary>T_OUTPUT_REQUEST as 'TOutputRequest'.</summary>
        [Seasar.Dao.Attrs.Relno(0), Seasar.Dao.Attrs.Relkeys("OUTPUT_REQUEST_ID:OUTPUT_REQUEST_ID")]
        public TOutputRequest TOutputRequest {
            get { return _tOutputRequest; }
            set { _tOutputRequest = value; }
        }

        protected TOutputSubGt _tOutputSubGt;

        /// <summary>T_OUTPUT_SUB_GT as 'TOutputSubGt'.</summary>
        [Seasar.Dao.Attrs.Relno(1), Seasar.Dao.Attrs.Relkeys("OUTPUT_COMMON_ID:OUTPUT_COMMON_ID")]
        public TOutputSubGt TOutputSubGt {
            get { return _tOutputSubGt; }
            set { _tOutputSubGt = value; }
        }

        protected TOutputSubCross _tOutputSubCross;

        /// <summary>T_OUTPUT_SUB_CROSS as 'TOutputSubCross'.</summary>
        [Seasar.Dao.Attrs.Relno(2), Seasar.Dao.Attrs.Relkeys("OUTPUT_COMMON_ID:OUTPUT_COMMON_ID")]
        public TOutputSubCross TOutputSubCross {
            get { return _tOutputSubCross; }
            set { _tOutputSubCross = value; }
        }

        protected TOutputSubFa _tOutputSubFa;

        /// <summary>T_OUTPUT_SUB_FA as 'TOutputSubFa'.</summary>
        [Seasar.Dao.Attrs.Relno(3), Seasar.Dao.Attrs.Relkeys("OUTPUT_COMMON_ID:OUTPUT_COMMON_ID")]
        public TOutputSubFa TOutputSubFa {
            get { return _tOutputSubFa; }
            set { _tOutputSubFa = value; }
        }

        protected TOutputSubCklist _tOutputSubCklist;

        /// <summary>T_OUTPUT_SUB_CKLIST as 'TOutputSubCklist'.</summary>
        [Seasar.Dao.Attrs.Relno(4), Seasar.Dao.Attrs.Relkeys("OUTPUT_COMMON_ID:OUTPUT_COMMON_ID")]
        public TOutputSubCklist TOutputSubCklist {
            get { return _tOutputSubCklist; }
            set { _tOutputSubCklist = value; }
        }

        #endregion

        // ===============================================================================
        //                                                               Referrer Property
        //                                                               =================
        #region Referrer Property
        protected IList<TOutputSubCklist> _tOutputSubCklistList;

        /// <summary>T_OUTPUT_SUB_CKLIST as 'TOutputSubCklistList'.</summary>
        public IList<TOutputSubCklist> TOutputSubCklistList {
            get { if (_tOutputSubCklistList == null) { _tOutputSubCklistList = new List<TOutputSubCklist>(); } return _tOutputSubCklistList; }
            set { _tOutputSubCklistList = value; }
        }

        protected IList<TOutputSubCross> _tOutputSubCrossList;

        /// <summary>T_OUTPUT_SUB_CROSS as 'TOutputSubCrossList'.</summary>
        public IList<TOutputSubCross> TOutputSubCrossList {
            get { if (_tOutputSubCrossList == null) { _tOutputSubCrossList = new List<TOutputSubCross>(); } return _tOutputSubCrossList; }
            set { _tOutputSubCrossList = value; }
        }

        protected IList<TOutputSubFa> _tOutputSubFaList;

        /// <summary>T_OUTPUT_SUB_FA as 'TOutputSubFaList'.</summary>
        public IList<TOutputSubFa> TOutputSubFaList {
            get { if (_tOutputSubFaList == null) { _tOutputSubFaList = new List<TOutputSubFa>(); } return _tOutputSubFaList; }
            set { _tOutputSubFaList = value; }
        }

        protected IList<TOutputSubGt> _tOutputSubGtList;

        /// <summary>T_OUTPUT_SUB_GT as 'TOutputSubGtList'.</summary>
        public IList<TOutputSubGt> TOutputSubGtList {
            get { if (_tOutputSubGtList == null) { _tOutputSubGtList = new List<TOutputSubGt>(); } return _tOutputSubGtList; }
            set { _tOutputSubGtList = value; }
        }

        #endregion

        // ===============================================================================
        //                                                                   Determination
        //                                                                   =============
        public virtual bool HasPrimaryKeyValue {
            get {
                if (_outputCommonId == null) { return false; }
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
            if (other == null || !(other is TOutputCommon)) { return false; }
            TOutputCommon otherEntity = (TOutputCommon)other;
            if (!xSV(this.OutputCommonId, otherEntity.OutputCommonId)) { return false; }
            return true;
        }
        protected bool xSV(Object value1, Object value2) { // isSameValue()
            if (value1 == null && value2 == null) { return true; }
            if (value1 == null || value2 == null) { return false; }
            return value1.Equals(value2);
        }

        public override int GetHashCode() {
            int result = 17;
            result = xCH(result, _outputCommonId);
            return result;
        }
        protected int xCH(int result, Object value) { // calculateHashcode()
            if (value == null) { return result; }
            return (31*result) + (value is byte[] ? ((byte[])value).Length : value.GetHashCode());
        }

        public override String ToString() {
            return "TOutputCommon:" + BuildColumnString() + BuildRelationString();
        }

        public virtual String ToStringWithRelation() {
            StringBuilder sb = new StringBuilder();
            sb.Append(ToString());
            String l = "\n  ";
            if (_tOutputRequest != null)
            { sb.Append(l).Append(xbRDS(_tOutputRequest, "TOutputRequest")); }
            if (_tOutputSubGt != null)
            { sb.Append(l).Append(xbRDS(_tOutputSubGt, "TOutputSubGt")); }
            if (_tOutputSubCross != null)
            { sb.Append(l).Append(xbRDS(_tOutputSubCross, "TOutputSubCross")); }
            if (_tOutputSubFa != null)
            { sb.Append(l).Append(xbRDS(_tOutputSubFa, "TOutputSubFa")); }
            if (_tOutputSubCklist != null)
            { sb.Append(l).Append(xbRDS(_tOutputSubCklist, "TOutputSubCklist")); }
            if (_tOutputSubCklistList != null) { foreach (Entity e in _tOutputSubCklistList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputSubCklistList")); } } }
            if (_tOutputSubCrossList != null) { foreach (Entity e in _tOutputSubCrossList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputSubCrossList")); } } }
            if (_tOutputSubFaList != null) { foreach (Entity e in _tOutputSubFaList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputSubFaList")); } } }
            if (_tOutputSubGtList != null) { foreach (Entity e in _tOutputSubGtList)
            { if (e != null) { sb.Append(l).Append(xbRDS(e, "TOutputSubGtList")); } } }
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
            sb.Append(c).Append(this.OutputCommonId);
            sb.Append(c).Append(this.OrderCount);
            sb.Append(c).Append(this.TsvFilePath);
            sb.Append(c).Append(this.ExcelbookNamePrefix);
            sb.Append(c).Append(this.ProcessStartDatetime);
            sb.Append(c).Append(this.ProcessForecastEndDatetime);
            sb.Append(c).Append(this.ProcessEndDatetime);
            sb.Append(c).Append(this.StatusCode);
            sb.Append(c).Append(this.Description);
            sb.Append(c).Append(this.OutputType);
            sb.Append(c).Append(this.OutputRequestId);
            sb.Append(c).Append(this.WbSettingCode);
            sb.Append(c).Append(this.NoanswerVisibleCode);
            sb.Append(c).Append(this.UnmatchVisibleCode);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }
        protected virtual String BuildRelationString() {
            StringBuilder sb = new StringBuilder();
            String c = ",";
            if (_tOutputRequest != null) { sb.Append(c).Append("TOutputRequest"); }
            if (_tOutputSubGt != null) { sb.Append(c).Append("TOutputSubGt"); }
            if (_tOutputSubCross != null) { sb.Append(c).Append("TOutputSubCross"); }
            if (_tOutputSubFa != null) { sb.Append(c).Append("TOutputSubFa"); }
            if (_tOutputSubCklist != null) { sb.Append(c).Append("TOutputSubCklist"); }
            if (_tOutputSubCklistList != null && _tOutputSubCklistList.Count > 0)
            { sb.Append(c).Append("TOutputSubCklistList"); }
            if (_tOutputSubCrossList != null && _tOutputSubCrossList.Count > 0)
            { sb.Append(c).Append("TOutputSubCrossList"); }
            if (_tOutputSubFaList != null && _tOutputSubFaList.Count > 0)
            { sb.Append(c).Append("TOutputSubFaList"); }
            if (_tOutputSubGtList != null && _tOutputSubGtList.Count > 0)
            { sb.Append(c).Append("TOutputSubGtList"); }
            if (sb.Length > 0) { sb.Remove(0, c.Length).Insert(0, "(").Append(")"); }
            return sb.ToString();
        }
        #endregion

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        #region Accessor
        /// <summary>OUTPUT_COMMON_ID: {PK, NotNull, NUMBER(27), FK to T_Output_Sub_GT}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_COMMON_ID")]
        public decimal? OutputCommonId {
            get { return _outputCommonId; }
            set {
                __modifiedProperties.AddPropertyName("OutputCommonId");
                _outputCommonId = value;
            }
        }

        /// <summary>ORDER_COUNT: {NotNull, NUMBER(10), default=[1]}</summary>
        [Seasar.Dao.Attrs.Column("ORDER_COUNT")]
        public long? OrderCount {
            get { return _orderCount; }
            set {
                __modifiedProperties.AddPropertyName("OrderCount");
                _orderCount = value;
            }
        }

        /// <summary>TSV_FILE_PATH: {NCLOB(4000)}</summary>
        [Seasar.Dao.Attrs.Column("TSV_FILE_PATH")]
        public String TsvFilePath {
            get { return _tsvFilePath; }
            set {
                __modifiedProperties.AddPropertyName("TsvFilePath");
                _tsvFilePath = value;
            }
        }

        /// <summary>EXCELBOOK_NAME_PREFIX: {VARCHAR2(100)}</summary>
        [Seasar.Dao.Attrs.Column("EXCELBOOK_NAME_PREFIX")]
        public String ExcelbookNamePrefix {
            get { return _excelbookNamePrefix; }
            set {
                __modifiedProperties.AddPropertyName("ExcelbookNamePrefix");
                _excelbookNamePrefix = value;
            }
        }

        /// <summary>PROCESS_START_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("PROCESS_START_DATETIME")]
        public DateTime? ProcessStartDatetime {
            get { return _processStartDatetime; }
            set {
                __modifiedProperties.AddPropertyName("ProcessStartDatetime");
                _processStartDatetime = value;
            }
        }

        /// <summary>PROCESS_FORECAST_END_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("PROCESS_FORECAST_END_DATETIME")]
        public DateTime? ProcessForecastEndDatetime {
            get { return _processForecastEndDatetime; }
            set {
                __modifiedProperties.AddPropertyName("ProcessForecastEndDatetime");
                _processForecastEndDatetime = value;
            }
        }

        /// <summary>PROCESS_END_DATETIME: {TIMESTAMP(6)(11, 6)}</summary>
        [Seasar.Dao.Attrs.Column("PROCESS_END_DATETIME")]
        public DateTime? ProcessEndDatetime {
            get { return _processEndDatetime; }
            set {
                __modifiedProperties.AddPropertyName("ProcessEndDatetime");
                _processEndDatetime = value;
            }
        }

        /// <summary>STATUS_CODE: {NotNull, NUMBER(5), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("STATUS_CODE")]
        public int? StatusCode {
            get { return _statusCode; }
            set {
                __modifiedProperties.AddPropertyName("StatusCode");
                _statusCode = value;
            }
        }

        /// <summary>DESCRIPTION: {NVARCHAR2(256)}</summary>
        [Seasar.Dao.Attrs.Column("DESCRIPTION")]
        public String Description {
            get { return _description; }
            set {
                __modifiedProperties.AddPropertyName("Description");
                _description = value;
            }
        }

        /// <summary>OUTPUT_TYPE: {NotNull, NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_TYPE")]
        public int? OutputType {
            get { return _outputType; }
            set {
                __modifiedProperties.AddPropertyName("OutputType");
                _outputType = value;
            }
        }

        /// <summary>OUTPUT_REQUEST_ID: {IX, NotNull, NUMBER(27), FK to T_OUTPUT_REQUEST}</summary>
        [Seasar.Dao.Attrs.Column("OUTPUT_REQUEST_ID")]
        public decimal? OutputRequestId {
            get { return _outputRequestId; }
            set {
                __modifiedProperties.AddPropertyName("OutputRequestId");
                _outputRequestId = value;
            }
        }

        /// <summary>WB_SETTING_CODE: {NotNull, NUMBER(1), default=[0]}</summary>
        [Seasar.Dao.Attrs.Column("WB_SETTING_CODE")]
        public int? WbSettingCode {
            get { return _wbSettingCode; }
            set {
                __modifiedProperties.AddPropertyName("WbSettingCode");
                _wbSettingCode = value;
            }
        }

        /// <summary>NOANSWER_VISIBLE_CODE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("NOANSWER_VISIBLE_CODE")]
        public int? NoanswerVisibleCode {
            get { return _noanswerVisibleCode; }
            set {
                __modifiedProperties.AddPropertyName("NoanswerVisibleCode");
                _noanswerVisibleCode = value;
            }
        }

        /// <summary>UNMATCH_VISIBLE_CODE: {NUMBER(1)}</summary>
        [Seasar.Dao.Attrs.Column("UNMATCH_VISIBLE_CODE")]
        public int? UnmatchVisibleCode {
            get { return _unmatchVisibleCode; }
            set {
                __modifiedProperties.AddPropertyName("UnmatchVisibleCode");
                _unmatchVisibleCode = value;
            }
        }

        #endregion
    }
}
