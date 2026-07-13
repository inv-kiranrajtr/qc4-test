
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TSurveyDataInsertOrUpdatePmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TSurveyDataInsertOrUpdatePmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected string _tableName;
        protected string _sampleId;
        protected string _insertStr;
        protected string _insertVal;
        protected string _updateStr;
    
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
            sb.Append("TSurveyDataInsertOrUpdatePmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_tableName);
            sb.Append(c).Append(_sampleId);
            sb.Append(c).Append(_insertStr);
            sb.Append(c).Append(_insertVal);
            sb.Append(c).Append(_updateStr);
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

        public string SampleId {
            get { return _sampleId; }
            set { _sampleId = value; }
        }

        public string InsertStr {
            get { return _insertStr; }
            set { _insertStr = value; }
        }

        public string InsertVal {
            get { return _insertVal; }
            set { _insertVal = value; }
        }

        public string UpdateStr {
            get { return _updateStr; }
            set { _updateStr = value; }
        }

    }
}
