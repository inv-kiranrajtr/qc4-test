
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.OutsideSql;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of PacDataprocessCheckCheckScenarioUsePmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class PacDataprocessCheckCheckScenarioUsePmb : ProcedurePmb {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        // -------------------------------------------------
        //                               Procedure Parameter
        //                               -------------------
        public static readonly String arg1_PROCEDURE_PARAMETER = "arg1, return";
        public static readonly String qcwebId_PROCEDURE_PARAMETER = "QCWEB_ID, in";
        public static readonly String itemId_PROCEDURE_PARAMETER = "ITEM_ID, in";
        public static readonly String checkResult_PROCEDURE_PARAMETER = "CHECK_RESULT, out";

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected decimal? _arg1;
        protected decimal? _qcwebId;
        protected decimal? _itemId;
        protected decimal? _checkResult;
    
        // ===============================================================================
        //                                                        Procedure Implementation
        //                                                        ========================
        public virtual String ProcedureName { get {
            return "PAC_DATAPROCESS_CHECK.CHECK_SCENARIO_USE";
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
            sb.Append("PacDataprocessCheckCheckScenarioUsePmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_arg1);
            sb.Append(c).Append(_qcwebId);
            sb.Append(c).Append(_itemId);
            sb.Append(c).Append(_checkResult);
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

        public decimal? QcwebId {
            get { return _qcwebId; }
            set { _qcwebId = value; }
        }

        public decimal? ItemId {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public decimal? CheckResult {
            get { return _checkResult; }
            set { _checkResult = value; }
        }

    }
}
