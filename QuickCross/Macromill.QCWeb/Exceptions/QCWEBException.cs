using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Macromill.QCWeb.Common;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Exceptions {
    /// <summary>
    /// QCWebシステム例外クラス
    /// </summary>
    [ComVisible(false), Guid("ACFE9BAD-B0CE-4de5-8E19-E6074582E04F")]
    public class QCWebException : ApplicationException {
        private string userId;
        private string windId;
        private decimal qc3UniqueId = -1;
        private string msgId;
        private string[] param;
        private GlobalsCommonConstant.LogLevel logLevel = GlobalsCommonConstant.LogLevel.FATAL;

        #region 廃止
        ///// <summary>
        ///// QCWebシステム例外クラス コンストラクタ
        ///// </summary>
        ///// <param name="msg">エラーメッセージ</param>
        //public QCWebException(string msg)
        //    : base(msg) {
        //}

        ///// <summary>
        ///// QCWebシステム例外クラス コンストラクタ
        ///// </summary>
        ///// <param name="msg">エラーメッセージ</param>
        ///// <param name="innerException">例外クラス</param>
        //public QCWebException(string msg, Exception innerException)
        //    : base(msg, innerException) {
        //}

        ///// <summary>
        ///// QCWebシステム例外クラス コンストラクタ
        ///// </summary>
        ///// <param name="qc3UniqueId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="lv">ログレベル</param>
        ///// <param name="innerException">例外クラス</param>
        //public QCWebException(decimal qc3UniqueId, String msgId, GlobalsCommonConstant.LogLevel lv, Exception innerException)
        //    : base("", innerException) {
        //    this.qc3UniqueId = qc3UniqueId;
        //    this.msgId = msgId;
        //    this.logLevel = lv;
        //}

        ///// <summary>
        ///// QCWebシステム例外クラス コンストラクタ
        ///// </summary>
        ///// <param name="qc3UniqueId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="param">メッセージパラメータ</param>
        ///// <param name="lv">ログレベル</param>
        ///// <param name="innerException">例外クラス</param>
        //public QCWebException(decimal qc3UniqueId, string msgId, string[] param, GlobalsCommonConstant.LogLevel lv, Exception innerException)
        //    : base("", innerException) {
        //    this.qc3UniqueId = qc3UniqueId;
        //    this.msgId = msgId;
        //    this.param = param;
        //    this.logLevel = lv;
        //}

        ///// <summary>
        ///// QCWebシステム例外クラス コンストラクタ
        ///// </summary>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="windId">画面ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="lv">ログレベル</param>
        ///// <param name="innerException">例外クラス</param>
        //public QCWebException(string userId, string windId, string msgId, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL, Exception innerException = null)
        //    : base("", innerException) {
        //    this.userId = userId;
        //    this.windId = windId;
        //    this.msgId = msgId;
        //    this.logLevel = lv;
        //}

        ///// <summary>
        ///// QCWebシステム例外クラス コンストラクタ
        ///// </summary>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="windId">画面ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="param">メッセージパラメータ</param>
        ///// <param name="lv">ログレベル</param>
        ///// <param name="innerException">例外クラス</param>
        //public QCWebException(string userId, string windId, string msgId, string[] param, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL, Exception innerException = null)
        //    : base("", innerException) {
        //    this.userId = userId;
        //    this.windId = windId;
        //    this.msgId = msgId;
        //    this.param = param;
        //    this.logLevel = lv;
        //}
        #endregion

        #region コンストラクタ1
        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="windId">画面ID</param>
        /// <param name="qc3UniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="lv">ログレベル</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebException(string userId, string windId, decimal qc3UniqueId, string msgId, GlobalsCommonConstant.LogLevel lv, Exception innerException)
            : base("", innerException) {
            this.userId = userId;
            this.windId = windId;
            this.qc3UniqueId = qc3UniqueId;
            this.msgId = msgId;
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="windId">画面ID</param>
        /// <param name="qc3UniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="param">メッセージパラメータ</param>
        /// <param name="lv">ログレベル</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebException(string userId, string windId, decimal qc3UniqueId, string msgId, string[] param, GlobalsCommonConstant.LogLevel lv, Exception innerException)
            : base("", innerException) {
            this.userId = userId;
            this.windId = windId;
            this.qc3UniqueId = qc3UniqueId;
            this.msgId = msgId;
            this.param = param;
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="lv">ログレベル</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebException(string msgId, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL, Exception innerException = null)
            : base("", innerException) {
            this.msgId = msgId;
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="msgId">メッセージID</param>
        /// <param name="param">メッセージパラメータ</param>
        /// <param name="lv">ログレベル</param>
        /// <param name="innerException">例外クラス</param>
        public QCWebException(string msgId, string[] param, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL, Exception innerException = null)
            : base("", innerException) {
            this.msgId = msgId;
            this.param = param;
            this.logLevel = lv;
        }
        #endregion

        #region コンストラクタ2
        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="mainMessage">メインのメッセージを表すMessageクラスのインスタンスへの参照</param>
        /// <param name="lv">ログレベルを表すLogLevel列挙型の値</param>
        /// <param name="subMessages">メインメッセージのパラメータリストに指定するメッセージリストを表すMessageクラスのインスタンスへの参照群 (可変長)</param>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalsCommonConstant.LogLevel">LogLevel列挙型</seealso>
        public QCWebException(Message mainMessage, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL
                        , params Message[] subMessages)
            : base(mainMessage == null ? string.Empty : mainMessage.Description)
        {
            this.msgId = mainMessage.MessageID;
            if (subMessages != null) this.param = Array.ConvertAll<Message, string>(subMessages, x => x.Description);
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="mainMessage">メインのメッセージを表すMessageクラスのインスタンスへの参照</param>
        /// <param name="lv">ログレベルを表すLogLevel列挙型の値</param>
        /// <param name="subMessages">メインメッセージのパラメータリストに指定する文字列群 (可変長)</param>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalsCommonConstant.LogLevel">LogLevel列挙型</seealso>
        public QCWebException(Message mainMessage, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL
                        , params string[] subMessages)
            : base(mainMessage == null ? string.Empty : mainMessage.Description)
        {
            this.msgId = mainMessage.MessageID;
            if (subMessages != null) this.param = subMessages;
            else this.param = new string[] { mainMessage.Description };
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="message">メッセージを表すMessageクラスのインスタンスへの参照</param>
        /// <param name="lv">ログレベルを表すLogLevel列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalsCommonConstant.LogLevel">LogLevel列挙型</seealso>
        public QCWebException(Message message, GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL)
            : base(message == null ? string.Empty : message.Description)
        {
            this.msgId = message.MessageID;
            if (message.MessageID == Constants.CUSTOM_MESSAGE_ID)
            {
                this.param = new string[] { message.Description };
            }
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="mainMessage">メインのメッセージを表すMessageクラスのインスタンスへの参照</param>
        /// <param name="innerException">内部Exceptionクラスのインスタンスへの参照</param>
        /// <param name="lv">ログレベルを表すLogLevel列挙型の値</param>
        /// <param name="subMessages">メインメッセージのパラメータリストに指定するメッセージリストを表すMessageクラスのインスタンスへの参照群 (可変長)</param>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalsCommonConstant.LogLevel">LogLevel列挙型</seealso>
        public QCWebException(Message mainMessage, Exception innerException
                        , GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL
                        , params Message[] subMessages)
            : base(mainMessage == null ? string.Empty : mainMessage.Description, innerException)
        {
            this.msgId = mainMessage.MessageID;
            if (subMessages != null) this.param = Array.ConvertAll<Message, string>(subMessages, x => x.Description);
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="mainMessage">メインのメッセージを表すMessageクラスのインスタンスへの参照</param>
        /// <param name="innerException">内部Exceptionクラスのインスタンスへの参照</param>
        /// <param name="lv">ログレベルを表すLogLevel列挙型の値</param>
        /// <param name="subMessages">メインメッセージのパラメータリストに指定する文字列群 (可変長)</param>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalsCommonConstant.LogLevel">LogLevel列挙型</seealso>
        public QCWebException(Message mainMessage, Exception innerException
                        , GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL
                        , params string[] subMessages)
            : base(mainMessage == null ? string.Empty : mainMessage.Description, innerException)
        {
            this.msgId = mainMessage.MessageID;
            if (subMessages != null) this.param = subMessages;
            this.logLevel = lv;
        }

        /// <summary>
        /// QCWebシステム例外クラス コンストラクタ
        /// </summary>
        /// <param name="message">メッセージを表すMessageクラスのインスタンスへの参照</param>
        /// <param name="innerException">内部Exceptionクラスのインスタンスへの参照</param>
        /// <param name="lv">ログレベルを表すLogLevel列挙型の値</param>
        /// <seealso cref="T:Macromill.QCWeb.Common.GlobalsCommonConstant.LogLevel">LogLevel列挙型</seealso>
        public QCWebException(Message message, Exception innerException
                        , GlobalsCommonConstant.LogLevel lv = GlobalsCommonConstant.LogLevel.FATAL)
            : base(message == null ? string.Empty : message.Description, innerException)
        {
            this.msgId = message.MessageID;
            if (message.MessageID == Constants.CUSTOM_MESSAGE_ID)
            {
                this.param = new string[] { this.Message };
            }
            this.logLevel = lv;
        }
        #endregion

        #region override
        /// <summary>
        /// 例外情報の取得
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}]", LogLevel));
            sb.Append(string.Format("[{0}]", UserId));
            sb.Append(string.Format("[{0}]", WindId));
            sb.Append(string.Format("[{0}]", Qc3UniqueId));
            sb.Append(string.Format("[{0}]", GetResource.GetLogMessage(MsgId, Param)));
            if (InnerException != null) {
                sb.Append(string.Format("[{0}]", InnerException.GetType().ToString()));
            }

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
                sb.Append(string.Format("[{0}]", LogLevel));
                sb.Append(string.Format("[{0}]", UserId));
                sb.Append(string.Format("[{0}]", WindId));
                sb.Append(string.Format("[{0}]", Qc3UniqueId));
                sb.Append(string.Format("[{0}]", GetResource.GetLogMessage(MsgId, Param)));
                if (InnerException != null) {
                    sb.Append(string.Format("[{0}]", InnerException.GetType().ToString()));
                }

                sb.Append(" : ");
                sb.Append(base.Message);
                return sb.ToString();
            }
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// ユーザIDを取得するプロパティ
        /// </summary>
        public string UserId {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// 画面IDを取得するプロパティ
        /// </summary>
        public string WindId {
            get { return windId; }
            set { windId = value; }
        }

        /// <summary>
        /// 個別調査ID（数値形式）を取得するプロパティ
        /// </summary>
        public decimal Qc3UniqueId {
            get { return qc3UniqueId; }
            set { qc3UniqueId = value; }
        }

        /// <summary>
        /// 個別調査ID（文字列形式）を取得するプロパティ
        /// </summary>
        public string Qc3UniqueIdStr {
            get {
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

        /// <summary>
        /// ログ出力レベルを取得プロパティ
        /// </summary>
        public GlobalsCommonConstant.LogLevel LogLevel {
            get { return logLevel; }
        }
        #endregion
    }
}


