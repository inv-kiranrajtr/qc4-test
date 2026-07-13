#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Logger.cs
 * バージョン：1.0.0
 * 概　　　要：共通クラス（定数定義）
 * 作　成　日：2012/03/12
 * 作　成　者：JOPS 佐々木 宏
 * $Id$ / $Date$ / $Rev$ / $Author$
 * --------------------------------------------------------------
 * 更　新　日：
 * 更　新　者：
 * 更新　内容：
 * 　　　　　　
 * --------------------------------------------------------------
 * 更　新　日：
 * 更　新　者：
 * 更新　内容：
 * 　　　　　　
 ***************************************************************/
#endregion

using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common
{
    /// <summary>
    /// QCWeb共通ログ出力の定数クラス
    /// </summary>
    /// <remarks>QCWebのログ出力を行うための定数を定義</remarks>
    [ComVisible(false), Guid("BFE55467-D2CE-49EF-9DB4-EB5332A172E1")]
    public class LoggerConst
    {
        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通ロガー
        /// </summary>
        protected const string COMMON_LOGGER = "CommonLogger";

        /* added by sasaki 2012/05/02 */
        /// <summary>
        /// Alert(Zabbix監視)のロガー
        /// </summary>
        protected const string ALERT_LOGGER = "AlertLogger";

        /// <summary>
        /// 操作ログのロガー 
        /// </summary>
        protected const string OPERATION_LOGGER = "OperationLogger";

        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通ロガーappender名
        /// </summary>
        protected const string COMMON_APPENDER = "CommonLoggerAppender";

        /// <summary>
        /// Alert(Zabbix監視)の共通ロガーappender名
        /// </summary>
        protected const string ALERT_APPENDER = "AlertLoggerAppender";

        /// <summary>
        /// ログファイルプレフィックス
        /// </summary>
        protected const string LOGFILE_PREFIX = "QCWeb_";

        /// <summary>
        /// アプリケーションログファイル名
        /// </summary>
        protected const string APP_LOGFILENAME = LOGFILE_PREFIX + "Application";

        /// <summary>
        /// バッチログファイル名
        /// </summary>
        protected const string BATCH_LOGFILENAME = LOGFILE_PREFIX + "Batch";

        /// <summary>
        /// レポートログファイル名
        /// </summary>
        protected const string REPORT_LOGFILENAME = LOGFILE_PREFIX + "Report";

        /// <summary>
        /// Alert(Zabbix監視)ログファイル名
        /// </summary>
        protected const string ALERT_LOGFILENAME = LOGFILE_PREFIX + "Alert_";
        
        /// <summary>
        /// ログ文字列の書式（[情報]）の[
        /// </summary>
        protected const string BRACKET_BEGIN = "[";
        
        /// <summary>
        /// ログ文字列の書式（[情報]）の]
        /// </summary>
        protected const string BRACKET_END = "]";

        /// <summary>
        /// リソースファイル名
        /// </summary>
        protected const string RESOURCE_NAME = ".Resources.Messages";

        /// <summary>
        /// メッセージID不正メッセージ
        /// </summary>
        protected const string MSGID_ERROR = "メッセージID が不正です。";
    }
}