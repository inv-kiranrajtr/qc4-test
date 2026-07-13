
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TItemInfoNamePmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TItemInfoNamePmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected Decimal _qCWebID;
        protected String _itemName;
        protected Decimal? _scenarioId;
    
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
            sb.Append("TItemInfoNamePmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_qCWebID);
            sb.Append(c).Append(_itemName);
            sb.Append(c).Append(_scenarioId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public Decimal QCWebID {
            get { return _qCWebID; }
            set { _qCWebID = value; }
        }

        public String ItemName {
            get { return (String)ConvertEmptyToNullIfString(_itemName); }
            set { _itemName = value; }
        }

        public Decimal? ScenarioId {
            get { return _scenarioId; }
            set { _scenarioId = value; }
        }

    }
}
