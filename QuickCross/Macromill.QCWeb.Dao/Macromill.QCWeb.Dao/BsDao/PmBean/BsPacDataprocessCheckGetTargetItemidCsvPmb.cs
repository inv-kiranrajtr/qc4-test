
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.OutsideSql;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of PacDataprocessCheckGetTargetItemidCsvPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class PacDataprocessCheckGetTargetItemidCsvPmb : ProcedurePmb {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        // -------------------------------------------------
        //                               Procedure Parameter
        //                               -------------------
        public static readonly String arg1_PROCEDURE_PARAMETER = "arg1, return";
        public static readonly String inDataEditId_PROCEDURE_PARAMETER = "IN_DATA_EDIT_ID, in";

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected String _arg1;
        protected decimal? _inDataEditId;
    
        // ===============================================================================
        //                                                        Procedure Implementation
        //                                                        ========================
        public virtual String ProcedureName { get {
            return "PAC_DATAPROCESS_CHECK.GET_TARGET_ITEMID_CSV";
        }}

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
            sb.Append("PacDataprocessCheckGetTargetItemidCsvPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_arg1);
            sb.Append(c).Append(_inDataEditId);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public String Arg1 {
            get { return (String)ConvertEmptyToNullIfString(_arg1); }
            set { _arg1 = value; }
        }

        public decimal? InDataEditId {
            get { return _inDataEditId; }
            set { _inDataEditId = value; }
        }

    }
}
