using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Macromill.QCWeb.Common;

namespace Macromill.QCWeb.Exceptions {
    /// <summary>
    /// QCWeb業務ハンドリング例外クラス
    /// </summary>
    [ComVisible(false), Guid("9D84B38A-1F8F-46ba-BB3C-CAA6203D1C44")]
    public class QCWebBusinessException : ApplicationException {
        private string userId;
        private string windId;
        private decimal qc3UniqueId = -1;
        private string msgId;
        private string[] param;

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="msg">エラーメッセージ</param>
        public QCWebBusinessException(string msg)
            : base(msg) {
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="qc3UniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(decimal qc3UniqueId, string msgId, Exception innerException)
            : base("", innerException) {
                this.qc3UniqueId = qc3UniqueId;
                this.msgId = msgId;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="qc3UniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="param">メッセージパラメータ</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(decimal qc3UniqueId, string msgId, string[] param, Exception innerException)
            : base("", innerException) {
                this.qc3UniqueId = qc3UniqueId;
                this.msgId = msgId;
                this.param = param;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="windId">画面ID</param>
        /// <param name="qc3UniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(string userId, string windId, decimal qc3UniqueId, string msgId, Exception innerException)
            : base("", innerException) {
                this.userId = userId;
                this.windId = windId;
                this.qc3UniqueId = qc3UniqueId;
                this.msgId = msgId;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="windId">画面ID</param>
        /// <param name="qc3UniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="param">メッセージパラメータ</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(string userId, string windId, decimal qc3UniqueId, string msgId, string[] param, Exception innerException)
            : base("", innerException) {
            this.userId = userId;
            this.windId = windId;
            this.qc3UniqueId = qc3UniqueId;
            this.msgId = msgId;
            this.param = param;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(string msgId, Exception innerException)
            : base("", innerException)
        {
            this.msgId = msgId;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="param">メッセージパラメータ</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(string msgId, string[] param, Exception innerException)
            : base("", innerException)
        {
            this.msgId = msgId;
            this.param = param;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="windId">画面ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(string userId, string windId, string msgId, Exception innerException)
            : base("", innerException)
        {
            this.userId = userId;
            this.windId = windId;
            this.msgId = msgId;
        }

        /// <summary>
        /// QCWeb業務ハンドリング例外クラス コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="windId">画面ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="param">メッセージパラメータ</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebBusinessException(string userId, string windId, string msgId, string[] param, Exception innerException)
            : base("", innerException)
        {
            this.userId = userId;
            this.windId = windId;
            this.msgId = msgId;
            this.param = param;
        }

        /// <summary>
        /// 例外情報の取得
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", UserId));
            sb.Append(string.Format("[{0}]", WindId));
            sb.Append(string.Format("[{0}]", Qc3UniqueId));
            sb.Append(string.Format("[{0}]", GetResource.GetLogMessage(MsgId, Param)));

            sb.Append(" : ");
            sb.Append(base.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// 現在の例外を説明するメッセージを取得します。
        /// </summary>
        public override string Message {
            get {
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("[{0}]", UserId));
                sb.Append(string.Format("[{0}]", WindId));
                sb.Append(string.Format("[{0}]", Qc3UniqueId));
                sb.Append(string.Format("[{0}]", GetResource.GetLogMessage(MsgId, Param)));

                sb.Append(" : ");
                sb.Append(base.Message);
                return sb.ToString();
            }
        }

        /// <summary>
        /// ユーザIDを取得するプロパティ
        /// </summary>
        public string UserId {
            get { return userId; }
        }

        /// <summary>
        /// 画面IDを取得するプロパティ
        /// </summary>
        public string WindId {
            get { return windId; }
        }

        /// <summary>
        /// 個別調査ID（数値形式）を取得するプロパティ
        /// </summary>
        public decimal Qc3UniqueId {
            get { return qc3UniqueId; }
        }

        /// <summary>
        /// 個別調査ID（文字列形式）を取得するプロパティ
        /// </summary>
        public string Qc3UniqueIdStr
        {
            get
            {
                if (qc3UniqueId == -1) return null;
                return qc3UniqueId.ToString();
            }
        }

        /// <summary>
        /// メッセージIDを取得するプロパティ
        /// </summary>
        public string MsgId {
            get { return msgId; }
        }

        /// <summary>
        /// メッセージ用パラメータを取得するプロパティ
        /// </summary>
        public string[] Param {
            get { return param; }
        }
    }
}
