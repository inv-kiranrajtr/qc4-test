
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TRawDataPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TRawDataPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected String _wkTbl;
        protected String _wkCol;
        protected String _wkColVal;
    
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
            sb.Append("TRawDataPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_wkTbl);
            sb.Append(c).Append(_wkCol);
            sb.Append(c).Append(_wkColVal);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public String WkTbl {
            get { return (String)ConvertEmptyToNullIfString(_wkTbl); }
            set { _wkTbl = value; }
        }

        public String WkCol {
            get { return (String)ConvertEmptyToNullIfString(_wkCol); }
            set { _wkCol = value; }
        }

        public String WkColVal {
            get { return (String)ConvertEmptyToNullIfString(_wkColVal); }
            set { _wkColVal = value; }
        }

    }
}
