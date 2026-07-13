#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Logger.cs
 * バージョン：1.0.0
 * 概　　　要：共通クラス（バッチログ出力）
 * 作　成　日：2012/03/09
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

using System;

using log4net;
using log4net.Appender;
using log4net.Config;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common
{
    /// <summary>
    /// QCWeb共通クラス（バッチログ出力）
    /// </summary>
    /// <remarks>バッチログ出力を行うために利用</remarks>
    /// <example>
    /// <code lang="C#">
    /// BatchLoggerを初期化
    /// BatchLogger.init("バッチID");
    /// 
    /// FATALログを出力
    /// BatchLogger.Fatal("サーバ名", "個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列"], ["ログ詳細追加情報"], ・・・, [exception]]);
    ///
    /// ERRORログを出力
    /// BatchLogger.Error("サーバ名", "個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列"], ["ログ詳細追加情報"], ・・・, [exception]]);
    ///
    /// WARNログを出力
    /// BatchLogger.Warn("サーバ名", "個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列"], ["ログ詳細追加情報"], ・・・, [exception]]);
    ///   
    /// INFOログを出力
    /// BatchLogger.Info("個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列]", ["ログ詳細追加情報"], ・・・, [exception]]);
    ///   
    /// DEBUGログを出力
    /// BatchLogger.Debug("個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列", ["ログ詳細追加情報"], ・・・, [exception]]);
    /// </code>
    /// </example>
    [ComVisible(false), Guid("D178DFB6-D794-42D2-9653-6DEF6FB60A29")]
    public class BatchLogger : Loggers
    {
        /// <summary>バッチID</summary>
        private static string BatchId = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private BatchLogger() { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="batchId">バッチID</param>
        public static void init(string batchId) {
            // バッチID
            BatchId = batchId;

            // バッチログのログ初期化
            initCommon(COMMON_LOGGER, COMMON_APPENDER);

            // Alert(Zabbix監視)ログのログ初期化
            initAlert(ALERT_LOGGER, ALERT_APPENDER);

            DeleteDefaultLogFile();
        }

        /// <summary>
        /// ログ出力根幹
        /// </summary>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(LogType logType, string alertName, string surveyId, string msgId, string[] replaceWords, params object[] details)
        {
            OutputLog(typeof(BatchLogger), logType, alertName, BatchId, BATCH_NAME, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// ログ出力根幹
        /// </summary>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(LogType logType, string surveyId, string msgId, string[] replaceWords, params object[] details)
        {
            OutputLog(typeof(BatchLogger), logType, BatchId, BATCH_NAME, surveyId, msgId, replaceWords, details);
        }

        #region 不要のため削除予定 (当面は、既存ロジックからの呼び出し対応のため残しておく)
        #region [FATAL]ログ出力
        /// <summary>
        /// FATALログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][FATAL][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// 監視ログ出力先： {ログルート}\QCWEB_Alert_[サーバ名].yyyy-mm-dd.log
        /// </remarks>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Fatal(string alertName, string surveyId, string msgId, string[] replaceWords, params object[] details)
        {
            /*
            //Fatal(BatchId, serverName, surveyId, msgId, replaceWords, details);
            // ログ文字列を生成
            string msg = CreateCommonMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // バッチログファイル名を変更
            ChangeCommonLogfileName(BatchId, BATCH_LOGFILENAME);
            // レポートログ出力
            common.Fatal(msg);

            // ログ文字列を生成
            msg = CreateAlertMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // Alert(Zabbix監視)ログファイル名を変更
            ChangeAlertLogfileName(ALERT_LOGFILENAME + alertName);
            // Alert(Zabbix監視)ログ出力
            alert.Fatal(msg);
            */
            OutputLog(LogType.Fatal, alertName, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// FATALログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][FATAL][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// 監視ログ出力先： {ログルート}\QCWEB_Alert_[サーバ名].yyyy-mm-dd.log
        /// </remarks>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Fatal(string alertName, string surveyId, string msgId, params object[] details) 
        {
            Fatal(alertName, surveyId, msgId, null, details);

        }

        #endregion 

        #region [ERROR]ログ出力
        /// <summary>
        /// ERRORログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][ERROR][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// 監視ログ出力先： {ログルート}\QCWEB_Alert_[サーバ名].yyyy-mm-dd.log
        /// </remarks>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Error(string alertName, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        {
            /*
            // ログ文字列を生成
            string msg = CreateCommonMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // バッチログファイル名を変更
            ChangeCommonLogfileName(BatchId, BATCH_LOGFILENAME);
            // レポートログ出力
            common.Error(msg);

            // ログ文字列を生成
            msg = CreateAlertMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // Alert(Zabbix監視)ログファイル名を変更
            ChangeAlertLogfileName(ALERT_LOGFILENAME + alertName);
            // Alert(Zabbix監視)ログ出力
            alert.Error(msg);
            */
            OutputLog(LogType.Error, alertName, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// ERRORログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][ERROR][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// 監視ログ出力先： {ログルート}\QCWEB_Alert_[サーバ名].yyyy-mm-dd.log
        /// </remarks>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Error(string alertName, string surveyId, string msgId, params object[] details) 
        {
            Error(alertName, surveyId, msgId, null, details);
        }
        #endregion 

        #region [WARN]ログ出力
        /// <summary>
        /// WARNログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][WARN][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// 監視ログ出力先： {ログルート}\QCWEB_Alert_[サーバ名].yyyy-mm-dd.log
        /// </remarks>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Warn(string alertName, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        {
            /*
            // ログ文字列を生成
            string msg = CreateCommonMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // バッチログファイル名を変更
            ChangeCommonLogfileName(BatchId, BATCH_LOGFILENAME);
            // レポートログ出力
            common.Warn(msg);

            // ログ文字列を生成
            msg = CreateAlertMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // Alert(Zabbix監視)ログファイル名を変更
            ChangeAlertLogfileName(ALERT_LOGFILENAME + alertName);
            // Alert(Zabbix監視)ログ出力
            alert.Warn(msg);
            */
            OutputLog(LogType.Warn, alertName, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// WARNログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][WARN][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// 監視ログ出力先： {ログルート}\QCWEB_Alert_[サーバ名].yyyy-mm-dd.log
        /// </remarks>
        /// <param name="alertName">サーバ名</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Warn(string alertName, string surveyId, string msgId, params object[] details) 
        {
            Warn(alertName, surveyId, msgId, null, details);
        }
        #endregion 

        #region [INFO]ログ出力
        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Info(string surveyId, string msgId, string[] replaceWords, params object[] details) 
        {
            /*
            // ログ文字列を生成
            string msg = CreateCommonMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // バッチログファイル名を変更
            ChangeCommonLogfileName(BatchId, BATCH_LOGFILENAME);
            // レポートログ出力
            common.Info(msg);
            */
            OutputLog(LogType.Info, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Info(string surveyId, string msgId, params object[] details) 
        {
            Info(surveyId, msgId, null, details);
        }

        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="msgId">メッセージID</param>
        public static void Info(string msgId)
        {
            Info("", msgId, null, new object[0]);
        }

        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="msgId">メッセージID</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Info(string msgId, params object[] details)
        {
            Info("", msgId, null, details);
        }

        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        public static void Info(string msgId, string[] replaceWords)
        {
            Info("", msgId, replaceWords, new object[0]);
        }

        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Info(string msgId, string[] replaceWords, params object[] details)
        {
            Info("", msgId, replaceWords, details);
        }

        /// <summary>
        /// INFOログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][INFO][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="qc3uniqueId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        public static void Info(decimal qc3uniqueId, string msgId, string[] replaceWords)
        {
            Info(qc3uniqueId.ToString(), msgId, replaceWords, new object[0]);
        }
        #endregion 

        #region [DEBUG]ログ出力
        /// <summary>
        /// DEBUGログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][DEBUG][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Debug(string surveyId, string msgId, string[] replaceWords, params object[] details) 
        {
            /*
            // ログ文字列を生成
            string msg = CreateCommonMessages(BATCH_NAME, surveyId, msgId, replaceWords, details);
            // バッチログファイル名を変更
            ChangeCommonLogfileName(BatchId, BATCH_LOGFILENAME);
            // レポートログ出力
            common.Debug(msg);
            */
            OutputLog(LogType.Debug, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// DEBUGログ出力
        /// </summary>
        /// <remarks>
        /// [yyyy/MM/dd hh24:mm:ss][DEBUG][サーバ名][BATCH][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        /// </remarks>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void Debug(string surveyId, string msgId, params object[] details) 
        {
            Debug(surveyId, msgId, null, details);
        }

        /// <summary>
        /// DEBUGログ出力
        /// </summary>
        /// <param name="message">デバッグメッセージ</param>
        public static void Debug(string message)
        {
            // レポートログファイル名を変更
            ChangeCommonLogfileName(BatchId, BATCH_LOGFILENAME);
            // レポートログ出力
            common.Debug(message);
        }
        #endregion 
        #endregion
    }
}