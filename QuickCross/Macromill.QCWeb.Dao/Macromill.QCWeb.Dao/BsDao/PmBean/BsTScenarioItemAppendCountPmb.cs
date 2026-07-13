
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TScenarioItemAppendCountPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TScenarioItemAppendCountPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected decimal _wkQCWebId;
        protected bool _wkGTtableFlg;
    
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
            sb.Append("TScenarioItemAppendCountPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_wkQCWebId);
            sb.Append(c).Append(_wkGTtableFlg);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public decimal WkQCWebId {
            get { return _wkQCWebId; }
            set { _wkQCWebId = value; }
        }

        public bool WkGTtableFlg {
            get { return _wkGTtableFlg; }
            set { _wkGTtableFlg = value; }
        }

    }
}
