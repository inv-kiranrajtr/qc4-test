#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：QCWebBatchException.cs
 * バージョン：1.0.0
 * 概　　　要：バッチ共通例外クラス
 * 作　成　日：2012/03/15
 * 作　成　者：寺嶋　千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Batch.Common.Exception {
    /// <summary>
    /// バッチ共通例外クラス
    /// </summary>
    [ComVisible(false), Guid("8CBD6EC2-8C03-452a-BBA1-6E17BC67EDAB")]
    public class QCWebBatchException : ApplicationException {
        /// <summary>メッセージID</summary>
        private int _msgId;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msgId"></param>
        public QCWebBatchException(int msgId) {
            _msgId = msgId;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg"></param>
        public QCWebBatchException(String msg)
            : base(msg) {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        public QCWebBatchException(String msg, System.Exception innerException)
            : base(msg, innerException) {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="msg"></param>
        /// <param name="innerException"></param>
        public QCWebBatchException(int msgId, String msg, System.Exception innerException)
            : base(msg, innerException) {
            _msgId = msgId;
        }

        /// <summary>
        /// メッセージIDを取得する
        /// </summary>
        public int MsgId {
            get { return this._msgId; }
        }

    }
}
