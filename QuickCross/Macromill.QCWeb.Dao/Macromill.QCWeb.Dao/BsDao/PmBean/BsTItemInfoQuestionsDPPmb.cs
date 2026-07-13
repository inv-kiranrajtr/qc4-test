
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TItemInfoQuestionsDPPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TItemInfoQuestionsDPPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected Decimal _qCWebId;
        protected int _executeNo;
        protected List<String> _answerType;
    
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
            sb.Append("TItemInfoQuestionsDPPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_qCWebId);
            sb.Append(c).Append(_executeNo);
            sb.Append(c).Append(_answerType);
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

        public int ExecuteNo {
            get { return _executeNo; }
            set { _executeNo = value; }
        }

        public List<String> AnswerType {
            get { return _answerType; }
            set { _answerType = value; }
        }

    }
}
