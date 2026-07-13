
using System;
using System.Collections.Generic;
using System.Text;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;

namespace Macromill.QCWeb.Dao.ExDao.PmBean {

    /// <summary>
    /// The parametaer-bean of TItemInfoFindByQCWebIDPmb.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    [System.Serializable]
    public partial class TItemInfoFindByQCWebIDPmb {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected Decimal _qcwebid;
        protected List<int> _statusList;
        protected List<int> _matrixDivList;
        protected List<String> _sourceDivList;
        protected Decimal _categoryEditId;
        protected String _itemName;
        protected List<String> _answerTypeList;
    
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
            sb.Append("TItemInfoFindByQCWebIDPmb:");
            sb.Append(xbuildColumnString());
            return sb.ToString();
        }
        private String xbuildColumnString() {
            String c = ", ";
            StringBuilder sb = new StringBuilder();
            sb.Append(c).Append(_qcwebid);
            sb.Append(c).Append(_statusList);
            sb.Append(c).Append(_matrixDivList);
            sb.Append(c).Append(_sourceDivList);
            sb.Append(c).Append(_categoryEditId);
            sb.Append(c).Append(_itemName);
            sb.Append(c).Append(_answerTypeList);
            if (sb.Length > 0) { sb.Remove(0, c.Length); }
            sb.Insert(0, "{").Append("}");
            return sb.ToString();
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public Decimal Qcwebid {
            get { return _qcwebid; }
            set { _qcwebid = value; }
        }

        public List<int> StatusList {
            get { return _statusList; }
            set { _statusList = value; }
        }

        public List<int> MatrixDivList {
            get { return _matrixDivList; }
            set { _matrixDivList = value; }
        }

        public List<String> SourceDivList {
            get { return _sourceDivList; }
            set { _sourceDivList = value; }
        }

        public Decimal CategoryEditId {
            get { return _categoryEditId; }
            set { _categoryEditId = value; }
        }

        public String ItemName {
            get { return (String)ConvertEmptyToNullIfString(_itemName); }
            set { _itemName = value; }
        }

        public List<String> AnswerTypeList {
            get { return _answerTypeList; }
            set { _answerTypeList = value; }
        }

    }
}
