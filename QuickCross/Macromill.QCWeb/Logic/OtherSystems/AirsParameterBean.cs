using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Common;

namespace Macromill.QCWeb.Common.OtherSystems {
    /// <summary>
    /// AIRs連携パラメータ情報
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false), System.Runtime.InteropServices.GuidAttribute("542F32BD-9F18-4738-A899-EF9A42F34CF9")]
    public class AirsParameterBean {
        private GlobalsCommonConstant.AIRS_REQUEST_KIND kbn;    // 区分
        private String rid;         // 調査ID
        private String cid;         // クライアントID
        private String gcuserid;    // ゲストID
        private String result;      // QCWeb側処理の成否
        private String jobno;       // QCWebジョブNo
        private String mngno;       // 追加データ管理No

        /// <summary>
        /// 区分を保持するプロパティ
        /// </summary>
        public GlobalsCommonConstant.AIRS_REQUEST_KIND Kbn {
            get { return kbn; }
            set { kbn = value; }
        }

        /// <summary>
        /// 調査IDを保持するプロパティ
        /// </summary>
        public String Rid {
            get { return rid; }
            set { rid = value; }
        }

        /// <summary>
        /// クライアントIDを保持するプロパティ
        /// </summary>
        public String Cid {
            get { return cid; }
            set { cid = value; }
        }

        /// <summary>
        /// ゲストIDを保持するプロパティ
        /// </summary>
        public String Gcuserid {
            get { return gcuserid; }
            set { gcuserid = value; }
        }

        /// <summary>
        /// QCWeb側処理の成否を保持するプロパティ
        /// </summary>
        public String Result
        {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// QCWebジョブNoを保持するプロパティ
        /// </summary>
        public String Jobno
        {
            get { return jobno; }
            set { jobno = value; }
        }

        /// <summary>
        /// 追加データ管理Noを保持するプロパティ
        /// </summary>
        public String Mngno
        {
            get { return mngno; }
            set { mngno = value; }
        }

        /// <summary>
        /// クラスが保持している情報を返却します
        /// </summary>
        /// <returns></returns>
        public new String ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append("kbn:" + kbn);
            sb.Append(",rid:" + rid);
            sb.Append(",cid:" + cid);
            sb.Append(",gcuserid:" + gcuserid);
            sb.Append(",result:" + result);
            sb.Append(",jobno:" + jobno);
            sb.Append(",mngno:" + mngno);
            return sb.ToString();
        }
    }
}
