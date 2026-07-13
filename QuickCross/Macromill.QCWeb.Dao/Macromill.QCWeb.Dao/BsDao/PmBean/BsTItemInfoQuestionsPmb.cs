
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TItemInfoQuestionsPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TItemInfoQuestionsPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected Decimal _qCWebId;
        protected Decimal _itemInfoId;
    
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
            sb.Append("TItemInfoQuestionsPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_qCWebId);
            sb.Append(c).Append(_itemInfoId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public Decimal QCWebId {
            get { return _qCWebId; }
            set { _qCWebId = value; }
        }

        public Decimal ItemInfoId {
            get { return _itemInfoId; }
            set { _itemInfoId = value; }
        }

    }
}
