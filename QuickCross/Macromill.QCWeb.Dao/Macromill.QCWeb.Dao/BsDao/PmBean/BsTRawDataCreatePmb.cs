
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TRawDataCreatePmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TRawDataCreatePmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected String _fromTable;
        protected String _fromField;
        protected String _toTable;
        protected String _toField;
    
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
            sb.Append("TRawDataCreatePmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_fromTable);
            sb.Append(c).Append(_fromField);
            sb.Append(c).Append(_toTable);
            sb.Append(c).Append(_toField);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public String FromTable {
            get { return (String)ConvertEmptyToNullIfString(_fromTable); }
            set { _fromTable = value; }
        }

        public String FromField {
            get { return (String)ConvertEmptyToNullIfString(_fromField); }
            set { _fromField = value; }
        }

        public String ToTable {
            get { return (String)ConvertEmptyToNullIfString(_toTable); }
            set { _toTable = value; }
        }

        public String ToField {
            get { return (String)ConvertEmptyToNullIfString(_toField); }
            set { _toField = value; }
        }

    }
}
