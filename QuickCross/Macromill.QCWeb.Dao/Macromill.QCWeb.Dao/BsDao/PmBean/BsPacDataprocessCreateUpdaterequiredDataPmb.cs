
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.OutsideSql;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of PacDataprocessCreateUpdaterequiredDataPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class PacDataprocessCreateUpdaterequiredDataPmb : ProcedurePmb {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        // -------------------------------------------------
        //                               Procedure Parameter
        //                               -------------------
        public static readonly String arg1_PROCEDURE_PARAMETER = "arg1, return";
        public static readonly String inOldDataEditId_PROCEDURE_PARAMETER = "IN_OLD_DATA_EDIT_ID, in";
        public static readonly String inNewDataEditId_PROCEDURE_PARAMETER = "IN_NEW_DATA_EDIT_ID, in";
        public static readonly String errCd_PROCEDURE_PARAMETER = "ERR_CD, out";
        public static readonly String errMsg_PROCEDURE_PARAMETER = "ERR_MSG, out";

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected decimal? _arg1;
        protected decimal? _inOldDataEditId;
        protected decimal? _inNewDataEditId;
        protected String _errCd;
        protected String _errMsg;
    
        // ===============================================================================
        //                                                        Procedure Implementation
        //                                                        ========================
        public virtual String ProcedureName { get {
            return "PAC_DATAPROCESS.CREATE_UPDATEREQUIRED_DATA";
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
            sb.Append("PacDataprocessCreateUpdaterequiredDataPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_arg1);
            sb.Append(c).Append(_inOldDataEditId);
            sb.Append(c).Append(_inNewDataEditId);
            sb.Append(c).Append(_errCd);
            sb.Append(c).Append(_errMsg);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public decimal? Arg1 {
            get { return _arg1; }
            set { _arg1 = value; }
        }

        public decimal? InOldDataEditId {
            get { return _inOldDataEditId; }
            set { _inOldDataEditId = value; }
        }

        public decimal? InNewDataEditId {
            get { return _inNewDataEditId; }
            set { _inNewDataEditId = value; }
        }

        public String ErrCd {
            get { return (String)ConvertEmptyToNullIfString(_errCd); }
            set { _errCd = value; }
        }

        public String ErrMsg {
            get { return (String)ConvertEmptyToNullIfString(_errMsg); }
            set { _errMsg = value; }
        }

    }
}
