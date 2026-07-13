
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TScenarioUpdateWeightbackFlagPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TScenarioUpdateWeightbackFlagPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected decimal _qcwebid;
        protected decimal _itemInfoId;
        protected string _lastUpdateUser;
        protected DateTime _lastUpdateDatetime;
    
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
            sb.Append("TScenarioUpdateWeightbackFlagPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_qcwebid);
            sb.Append(c).Append(_itemInfoId);
            sb.Append(c).Append(_lastUpdateUser);
            sb.Append(c).Append(_lastUpdateDatetime);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public decimal Qcwebid {
            get { return _qcwebid; }
            set { _qcwebid = value; }
        }

        public decimal ItemInfoId {
            get { return _itemInfoId; }
            set { _itemInfoId = value; }
        }

        public string LastUpdateUser {
            get { return _lastUpdateUser; }
            set { _lastUpdateUser = value; }
        }

        public DateTime LastUpdateDatetime {
            get { return _lastUpdateDatetime; }
            set { _lastUpdateDatetime = value; }
        }

    }
}
