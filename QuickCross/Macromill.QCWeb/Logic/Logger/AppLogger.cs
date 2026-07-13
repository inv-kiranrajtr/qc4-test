#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Logger.cs
 * バージョン：1.0.0
 * 概　　　要：共通クラス（アプリケーションログ出力）
 * 作　成　日：2012/03/09
 * 作　成　者：JOPS 佐々木 宏
 * $Id$ / $Date$ / $Rev$ / $Author$
 * --------------------------------------------------------------
 * 更　新　日：2013/09/05
 * 更　新　者：金森良昌
 * 更新　内容：AppLoggerを利用しているUI側を見直したので、本クラスのメソッドを整理した
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
    /// QCWeb共通クラス（アプリケーションログ出力）
    /// </summary>
    /// <remarks>アプリケーションログ出力を行うために利用</remarks>
    /// <example>
    /// <code lang="C#">
    /// FATALログを出力
    /// AppLogger.Fatal("画面ID", "userId", "surveyId", "msgId", [exception]);
    /// AppLogger.Fatal("画面ID", "userId", "surveyId", "msgId", replaceWords, [exception]);
    /// AppLogger.Fatal("画面ID", "userId", "surveyId", "msgId", "ログ詳細追加情報", ・・・, [exception]);
    /// AppLogger.Fatal("画面ID", "userId", "surveyId", "msgId", replaceWords, "ログ詳細追加情報", ・・・, [exception]);
    ///
    /// ERRORログを出力
    /// AppLogger.Error("画面ID", "userId", "surveyId", "msgId", [exception]);
    /// AppLogger.Error("画面ID", "userId", "surveyId", "msgId", replaceWords, [exception]);
    /// AppLogger.Error("画面ID", "userId", "surveyId", "msgId", "ログ詳細追加情報", ・・・, [exception]);
    /// AppLogger.Error("画面ID", "userId", "surveyId", "msgId", replaceWords, "ログ詳細追加情報", ・・・, [exception]);
    ///
    /// WARNログを出力
    /// AppLogger.Warn("画面ID", "userId", "surveyId", "msgId", [exception]);
    /// AppLogger.Warn("画面ID", "userId", "surveyId", "msgId", replaceWords, [exception]);
    /// AppLogger.Warn("画面ID", "userId", "surveyId", "msgId", "ログ詳細追加情報", ・・・, [exception]);
    /// AppLogger.Warn("画面ID", "userId", "surveyId", "msgId", replaceWords, "ログ詳細追加情報", ・・・, [exception]);
    ///   
    /// INFOログを出力
    /// AppLogger.Info("画面ID", "userId", "surveyId", "msgId", [exception]);
    /// AppLogger.Info("画面ID", "userId", "surveyId", "msgId", replaceWords, [exception]);
    /// AppLogger.Info("画面ID", "userId", "surveyId", "msgId", "ログ詳細追加情報", ・・・, [exception]);
    /// AppLogger.Info("画面ID", "userId", "surveyId", "msgId", replaceWords, "ログ詳細追加情報", ・・・, [exception]);
    ///   
    /// DEBUGログを出力
    /// AppLogger.Debug("画面ID", "userId", "surveyId", "msgId", [exception]);
    /// AppLogger.Debug("画面ID", "userId", "surveyId", "msgId", replaceWords, [exception]);
    /// AppLogger.Debug("画面ID", "userId", "surveyId", "msgId", "ログ詳細追加情報", ・・・, [exception]);
    /// AppLogger.Debug("画面ID", "userId", "surveyId", "msgId", replaceWords, "ログ詳細追加情報", ・・・, [exception]);
    /// </code>
    /// </example>
    [ComVisible(false), Guid("26D533F8-D3F8-461E-ABC2-BE721DAFF997")]
    public class AppLogger : Loggers
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /* modified by sasaki 2012/03/30
        private AppLogger() { }
         */
        protected AppLogger() { }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public static void init() {
            // アプリログのログ初期化
            initCommon(COMMON_LOGGER, COMMON_APPENDER);

            // Alert(Zabbix監視)ログのログ初期化
            initAlert(ALERT_LOGGER, ALERT_APPENDER);

            DeleteDefaultLogFile();
        }

        /// <summary>
        /// ログ出力根幹
        /// </summary>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="idName">画面ID</param>
        /// <param name="userId">ユーザID</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(LogType logType, string idName, string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) {
            OutputLog(typeof(AppLogger), logType, idName, userId, surveyId, msgId, replaceWords, details);
        }

        //#region 不要のため削除予定 (当面は、既存ロジックからの呼び出し対応のため残しておく)
        //#region [FATAL]ログ出力
        ///// <summary>
        ///// FATALログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][FATAL][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Fatal(string idName, string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        //{
        //    /*
        //    // ログ文字列を生成
        //    string msg = CreateCommonMessages(userId, surveyId, msgId, replaceWords, details);
        //    // アプリケーションログファイル名を変更
        //    ChangeCommonLogfileName(idName, APP_LOGFILENAME);
        //    // アプリケーションログ出力
        //    common.Fatal(msg);
        //    // ログ文字列を生成
        //    msg = CreateAlertMessages(userId, surveyId, msgId, replaceWords, details);
        //    // Alert(Zabbix監視)ログファイル名を変更
        //    ChangeAlertLogfileName(ALERT_LOGFILENAME + getHostName());
        //    // Alert(Zabbix監視)ログ出力
        //    alert.Fatal(msg);
        //    */
        //    OutputLog(LogType.Fatal, idName, userId, surveyId, msgId, replaceWords, details);
        //}

        ///// <summary>
        ///// FATALログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][FATAL][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Fatal(string idName, string userId, string surveyId, string msgId, params object[] details) 
        //{
        //    Fatal(idName, userId, surveyId, msgId, null, details);
        //}

        //#endregion 

        //#region [ERROR]ログ出力
        ///// <summary>
        ///// ERRORログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][ERROR][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Error(string idName, string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        //{
        //    /*
        //    // ログ文字列を生成
        //    string msg = CreateCommonMessages(userId, surveyId, msgId, replaceWords, details);
        //    // アプリケーションログファイル名を変更
        //    ChangeCommonLogfileName(idName, APP_LOGFILENAME);
        //    // アプリケーションログ出力
        //    common.Error(msg);

        //    // ログ文字列を生成
        //    msg = CreateAlertMessages(userId, surveyId, msgId, replaceWords, details);
        //    // Alert(Zabbix監視)ログファイル名を変更
        //    ChangeAlertLogfileName(ALERT_LOGFILENAME + getHostName());
        //    // Alert(Zabbix監視)ログ出力
        //    alert.Error(msg);
        //    */
        //    OutputLog(LogType.Error, idName, userId, surveyId, msgId, replaceWords, details);
        //}

        ///// <summary>
        ///// ERRORログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][ERROR][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Error(string idName, string userId, string surveyId, string msgId, params object[] details)
        //{
        //    Error(idName, userId, surveyId, msgId, null, details);
        //}
        //#endregion 

        //#region [WARN]ログ出力
        ///// <summary>
        ///// WARNログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][WARN ][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Warn(string idName, string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        //{
        //    /*
        //    // ログ文字列を生成
        //    string msg = CreateCommonMessages(userId, surveyId, msgId, replaceWords, details);
        //    // アプリケーションログファイル名を変更
        //    ChangeCommonLogfileName(idName, APP_LOGFILENAME);
        //    // アプリケーションログ出力
        //    common.Warn(msg);

        //    // ログ文字列を生成
        //    msg = CreateAlertMessages(userId, surveyId, msgId, replaceWords, details);
        //    // Alert(Zabbix監視)ログファイル名を変更
        //    ChangeAlertLogfileName(ALERT_LOGFILENAME + getHostName());
        //    // Alert(Zabbix監視)ログ出力
        //    alert.Warn(msg);
        //    */
        //    OutputLog(LogType.Warn, idName, userId, surveyId, msgId, replaceWords, details);
        //}

        ///// <summary>
        ///// WARNログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][WARN ][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Warn(string idName, string userId, string surveyId, string msgId, params object[] details) 
        //{
        //    Warn(idName, userId, surveyId, msgId, null, details);
        //}
        //#endregion 

        //#region [INFO]ログ出力
        ///// <summary>
        ///// INFOログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][INFO ][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Info(string idName, string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        //{
        //    /*
        //    // ログ文字列を生成
        //    string msg = CreateCommonMessages(userId, surveyId, msgId, replaceWords, details);
        //    // アプリケーションログファイル名を変更
        //    ChangeCommonLogfileName(idName, APP_LOGFILENAME);
        //    // アプリケーションログ出力
        //    common.Info(msg);
        //    */
        //    OutputLog(LogType.Info, idName, userId, surveyId, msgId, replaceWords, details);
        //}

        ///// <summary>
        ///// INFOログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][INFO ][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Info(string idName, string userId, string surveyId, string msgId, params object[] details) 
        //{
        //    Info(idName, userId, surveyId, msgId, null, details);
        //}
        //#endregion 

        //#region [DEBUG]ログ出力
        ///// <summary>
        ///// DEBUGログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][DEBUG][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Debug(string idName, string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) 
        //{
        //    /*
        //    // ログ文字列を生成
        //    string msg = CreateCommonMessages(userId, surveyId, msgId, replaceWords, details);
        //    // アプリケーションログファイル名を変更
        //    ChangeCommonLogfileName(idName, APP_LOGFILENAME);
        //    // アプリケーションログ出力
        //    common.Debug(msg);
        //    */
        //    OutputLog(LogType.Debug, idName, userId, surveyId, msgId, replaceWords, details);
        //}

        ///// <summary>
        ///// DEBUGログ出力
        ///// </summary>
        ///// <remarks>
        ///// [yyyy/MM/dd hh24:mm:ss][DEBUG][サーバ名][ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…
        ///// </remarks>
        ///// <param name="idName">画面ID</param>
        ///// <param name="userId">ユーザID</param>
        ///// <param name="surveyId">個別調査ID</param>
        ///// <param name="msgId">メッセージID</param>
        ///// <param name="details">ログ詳細追加情報文字列配列</param>
        //public static void Debug(string idName, string userId, string surveyId, string msgId, params object[] details)
        //{
        //    Debug(idName, userId, surveyId, msgId, null, details);
        //}
        //#endregion 
        //#endregion
    }
}