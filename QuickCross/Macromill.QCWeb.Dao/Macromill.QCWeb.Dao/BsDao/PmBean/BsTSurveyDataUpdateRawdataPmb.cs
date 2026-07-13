
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TSurveyDataUpdateRawdataPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TSurveyDataUpdateRawdataPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected string _tableName;
        protected string _columnName;
        protected string _updValue;
        protected string _sampleId;
    
        // ===============================================================================
        //                                                                   Assist Helper
        //                                                                   =============
        protected String ConvertEmptyToNullIfString(String value) {
            return FilterRemoveEmptyString(value);
        }

        protected String FilterRemoveEmptyString(String value) {
            return ((value != null && !"".Equals(value)) ? value : null);
        }

        protected String FormatByteArray(byte[] bytes) {
            return "byte[" + (bytes != null ? bytes.Length.ToString() : "null") + "]";
        }

        // ===============================================================================
        //                                                                  Basic Override
        //                                                                  ==============
        public override String ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("TSurveyDataUpdateRawdataPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_tableName);
            sb.Append(c).Append(_columnName);
            sb.Append(c).Append(_updValue);
            sb.Append(c).Append(_sampleId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public string TableName {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string ColumnName {
            get { return _columnName; }
            set { _columnName = value; }
        }

        public string UpdValue {
            get { return _updValue; }
            set { _updValue = value; }
        }

        public string SampleId {
            get { return _sampleId; }
            set { _sampleId = value; }
        }

    }
}
