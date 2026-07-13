#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Logger.cs
 * バージョン：1.0.0
 * 概　　　要：共通クラス（ログ出力）
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
using System.IO;
using System.Collections;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using System.Text.RegularExpressions;

//using Macromill.QCWeb.bean;

namespace Macromill.QCWeb.Common {


    /// <summary>
    /// ログ出力タイプを表す列挙型
    /// </summary>
    [ComVisible(false)]
    public enum LogType : int {
        /// <summary>
        /// FATAL
        /// </summary>
        Fatal,
        /// <summary>
        /// ERROR
        /// </summary>
        Error,
        /// <summary>
        /// WARN
        /// </summary>
        Warn,
        /// <summary>
        /// INFO
        /// </summary>
        Info,
        /// <summary>
        /// DEBUG
        /// </summary>
        Debug
    }

    /// <summary>
    /// QCWeb共通ログ出力クラス
    /// </summary>
    /// <remarks>QCWebのログ出力を行うために利用</remarks>
    [ComVisible(false), Guid("4DFBC61E-3B66-43B8-82D2-3A6FADEA35B0")]
    public class Loggers : LoggerConst {
        /// <summary>
        /// log4netのロガー
        /// </summary>
        private static Loggers instance = new Loggers();

        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通ロガー
        /// </summary>
        protected static ILog common = LogManager.GetLogger(COMMON_LOGGER);

        /* added by sasaki 2012/05/02 */
        /// <summary>
        /// Alert(Zabbix監視)ログのロガー
        /// </summary>
        protected static ILog alert = LogManager.GetLogger(ALERT_LOGGER);

        /// <summary>
        /// 操作ログのロガー 
        /// </summary>
        protected static ILog ope = LogManager.GetLogger(OPERATION_LOGGER);

        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通appender
        /// </summary>
        private static log4net.Appender.RollingFileAppender commonAppender;

        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通ログルート（ログ出力ディレクトリ）
        /// </summary>
        private static string commonLogRootDirectory;

        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通デフォルトログファイル名
        /// </summary>
        private static string commonDefaultLogFile;

        /// <summary>
        /// Alert(Zabbix監視)ログの共通appender
        /// </summary>
        private static log4net.Appender.RollingFileAppender alertAppender;

        /// <summary>
        /// Alert(Zabbix監視)ログのログルート（ログ出力ディレクトリ）
        /// </summary>
        private static string alertLogRootDirectory;

        /// <summary>
        /// Alert(Zabbix監視)ログのデフォルトログファイル名
        /// </summary>
        private static string alertDefaultLogFile;

        /// <summary>
        /// バッチ名
        /// </summary>
        protected const string BATCH_NAME = "BATCH";

        /// <summary>Logger名称配列</summary>
        protected static readonly string[] LOGGERS = new string[] { COMMON_LOGGER, ALERT_LOGGER, OPERATION_LOGGER };
        /// <summary>Appender名称配列</summary>
        protected static readonly string[] APPENDERS = new string[] { COMMON_APPENDER, ALERT_APPENDER };

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Loggers() { }

        /// <summary>
        /// Zabbix監視ログのルートディレクトリを設定
        /// </summary>
        /// <param name="dir">ルートディレクトリ</param>
        protected static void SetAlertLogRootDirectory(string dir) {
            alertLogRootDirectory = dir;
        }

        /// <summary>
        /// デフォルトで作成されるログファイルを削除する
        /// </summary>
        protected static void DeleteDefaultLogFile() {
            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;
            Regex regex = new Regex(@"(QCWEB_Alert\.|QCWeb_Application\.)\d{4}-\d{1,2}-\d{1,2}\.log$");
            if (hierarchy == null) return;
            for (int i = 0; i < LOGGERS.Length; ++i) {
                Logger logger = hierarchy.GetLogger(LOGGERS[i], hierarchy.LoggerFactory);
                string path = null;
                for (int j = 0; j < APPENDERS.Length; ++j) {
                    RollingFileAppender appender = (RollingFileAppender)logger.GetAppender(APPENDERS[j]);
                    if (appender == null) continue;
                    try {
                        File.Delete(appender.File);
                    } catch (Exception) {
                    }
                    path = Path.GetDirectoryName(appender.File);
                }

                if (path != null) {
                    string[] files = System.IO.Directory.GetFiles(path, "*.log");
                    foreach (string file in files) {
                        if (regex.IsMatch(Path.GetFileName(file))) {
                            try {
                                File.Delete(file);
                            } catch (Exception) {
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ログ初期化根幹
        /// </summary>
        /// <param name="loggerName">ロガー名</param>
        /// <param name="appenderName">アペンダ名</param>
        /// <param name="appender">アペンダへの参照を表すRollingFileAppender型変数</param>
        /// <param name="defaultLogFile">デフォルトログファイルパスを表すstring型変数</param>
        /// <param name="logRootDirectory">ログ出力ディレクトリパスを表すstring型変数</param>
        private static void initLog(string loggerName, string appenderName
                                  , ref RollingFileAppender appender, ref string defaultLogFile, ref string logRootDirectory) {
            if (loggerName == null || appenderName == null) return;
            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;
            if (hierarchy == null) return;
            Logger logger = hierarchy.GetLogger(loggerName, hierarchy.LoggerFactory);
            if ((appender = logger.GetAppender(appenderName) as RollingFileAppender) == null) return;
            defaultLogFile = appender.File;
            logRootDirectory = Path.GetDirectoryName(defaultLogFile);
        }

        /// <summary>
        /// アプリケーション、バッチ、レポートログの共通ログ初期化
        /// </summary>
        /// <param name="loggerName">ロガー名</param>
        /// <param name="appenderName">appender名</param>
        protected static void initCommon(string loggerName, string appenderName) {
            initLog(loggerName, appenderName, ref commonAppender, ref commonDefaultLogFile, ref commonLogRootDirectory);
        }

        /// <summary>
        /// Alert(Zabbix監視)ログのログ初期化
        /// </summary>
        /// <param name="loggerName">ロガー名</param>
        /// <param name="appenderName">appender名</param>
        protected static void initAlert(string loggerName, string appenderName) {
            initLog(loggerName, appenderName, ref alertAppender, ref alertDefaultLogFile, ref alertLogRootDirectory);
        }

        /// <summary>
        /// ログ文字列生成根幹
        /// </summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="message">メッセージ文</param>
        /// <param name="ignoreSurveryId">
        /// パラメータリスト(規定値：true)
        /// <note>
        /// true->個別調査IDを出力する
        /// false->個別調査IDを出力しない
        /// </note>
        /// </param>
        /// <param name="userKind">ログインユーザ区分</param>
        /// <param name="details">ログ詳細追加情報配列 (文字列だけじゃなくてExceptionとかも入るはず)</param>
        /// <returns>ログ文字列</returns>
        private static string CreateMessage(string userId, string surveyId, string msgId, string message
                                  , bool ignoreSurveryId = true, string userKind = null, params object[] details) {
            StringBuilder buf = new StringBuilder("");
            buf.Append(ConvertMessage(userId));
            if (ignoreSurveryId) buf.Append(ConvertMessage(surveyId));
            else buf.Append(ConvertMessage(userKind));
            buf.Append(ConvertMessage(msgId));
            buf.Append(ConvertMessage(message));
            if (details != null) buf.Append(ConvertDetails(details));
            return buf.ToString();
        }

        private static string CreateMessage(string userId, string surveyId, string msgId, string[] replaceWords
                                          , bool ignoreSurveryId = true, string userKind = null, params object[] details) {
            return CreateMessage(userId, surveyId, msgId, GetResource.GetLogMessage(msgId, replaceWords)
                               , ignoreSurveryId, userKind, details);
        }

        /// <summary>
        /// ログ文字列を生成
        /// </summary>
        /// <remarks>[ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…</remarks>
        /// <param name="userId">ユーザID</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        /// <returns>ログ文字列</returns>
        protected static string CreateCommonMessages(string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) {
            return CreateMessage(userId, surveyId, msgId, replaceWords, true, null, details);
        }

        /// <summary>
        /// ログ文字列を生成
        /// </summary>
        /// <remarks>[ユーザID][個別調査ID][メッセージID][ログメッセージ][ログ詳細追加情報]…</remarks>
        /// <param name="userId">ユーザID</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        /// <returns>ログ文字列</returns>
        protected static string CreateAlertMessages(string userId, string surveyId, string msgId, string[] replaceWords, params object[] details) {
            if (details != null) {
                List<object> newDetailList = new List<object>();
                for (int i = 0; i < details.Length; ++i) {
                    if (details[i] != null && !(details[i] is Exception)) {
                        newDetailList.Add(details[i]);
                    }
                }
                return CreateMessage(userId, surveyId, msgId, replaceWords, true, null, newDetailList.ToArray());
            }
            return CreateMessage(userId, surveyId, msgId, replaceWords, true, null, details);
        }

        /// <summary>
        /// ログ文字列を生成
        /// </summary>
        /// <remarks>[ユーザID][メッセージID][ログメッセージ]</remarks>
        /// <param name="userId">ユーザID</param>
        /// <param name="userKind">ユーザ区分</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <returns>ログ文字列</returns>
        protected static string CreateOpeMessages(string userId, string userKind, string msgId, string[] replaceWords) {
            return CreateMessage(userId, null, msgId, replaceWords, false, userKind);
        }

        /// <summary>
        /// ログ出力ファイル名変更根幹
        /// </summary>
        /// <param name="newFileName">新たなログファイル名</param>
        /// <param name="appender">アペンダを表すRollingFileAppenderクラスのインスタンスへの参照</param>
        /// <param name="defaultLogFile">デフォルトログファイルパス</param>
        /// <param name="dirPath">ログ出力ディレクトリパス</param>
        private static void changeLogFileName(string newFileName
            , RollingFileAppender appender, string defaultLogFile, string dirPath) {
            if (newFileName == null || appender == null || defaultLogFile == null || dirPath == null)　return;
            try {
                appender.File = Path.Combine(dirPath, newFileName);
                //2012/08/20 yzhengy add start
                appender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
                //2012/08/20 yzhengy add end
                appender.ActivateOptions();
                // デフォルトログファイルが存在していれば削除
                // Deleteメソッドはファイルが存在しない場合にはエラーをスローしないので、存在確認は不要
                // これ消して問題ないのか？と言うか、新たに指定された名前が、デフォルトと同じってことは担保されているのか？
                File.Delete(defaultLogFile);
            } catch (Exception e) {
                // 何もしない
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
            }

        }

        /// <summary>
        /// Alert(Zabbix監視)ログ出力ファイル名を変更
        /// </summary>
        /// <param name="filename">ログ出力ファイル名</param>
        protected static void ChangeAlertLogfileName(string filename) {
            changeLogFileName(filename, alertAppender, alertDefaultLogFile, alertLogRootDirectory);
        }

        /// <summary>
        /// [画面ID]/[バッチID]付きログ出力ファイル名を変更
        /// </summary>
        /// <param name="idName">[画面ID]/[バッチID]</param>
        /// <param name="filename">ログ出力ファイル名</param>
        protected static void ChangeCommonLogfileName(string idName, string filename) {
            string dirPath = null;
            try {
                if (commonLogRootDirectory == null || idName == null) return;
                dirPath = Path.Combine(commonLogRootDirectory, idName);
            } catch (Exception e) {
                // 何もしない
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                return;
            }
            changeLogFileName(filename, commonAppender, commonDefaultLogFile, dirPath);
        }

        /// <summary>
        /// ログ詳細追加情報文字列配列をログフォーマットに変換し取得
        /// </summary>
        /// <remarks>
        /// [ログ詳細追加情報][ログ詳細追加情報]…の書式に変換
        /// </remarks>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        /// <returns>ログ詳細追加情報</returns>
        private static string ConvertDetails(object[] details) {
            if (details == null) return null;
            string[] msgs = Array.ConvertAll<object, string>(details
                          , x => {
                                    if (x == null)
                                        return ConvertMessage(null);
                                    if (x is Exception) {
                                        Exception exp = x as Exception;
                                        return ConvertMessage(exp.Message) + ConvertMessage(exp.StackTrace);
                                    } else {
                                        return ConvertMessage(x.ToString());
                                    }
                                }
                          );
            return string.Join("", msgs);
        }

        /// <summary>
        /// ログ詳細追加情報文字列配列をログフォーマットに変換し取得
        /// </summary>
        /// <remarks>
        /// [ログ詳細追加情報][ログ詳細追加情報]…の書式に変換
        /// </remarks>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        /// <returns>ログ詳細追加情報</returns>
        private static string ConvertAlertDetails(object[] details) {
            if (details == null)
                return null;
            string[] msgs = Array.ConvertAll<object, string>(details
                          , x => {
                                    if (x == null)
                                        return ConvertMessage(null);
                                    if (x is Exception) {
                                        Exception exp = x as Exception;
                                        return ConvertMessage(exp.GetType().FullName);
                                    } else {
                                        return ConvertMessage(x.ToString());
                                    }
                                }
                          );
            return string.Join("", msgs);
        }

        /// <summary>
        /// ログメッセージ書式文字列[ｘｘｘ]に変換
        /// </summary>
        /// <param name="str">文字列</param>
        /// <returns>ログメッセージ書式文字列</returns>
        private static string ConvertMessage(string str) {
            if (str == null)
                str = string.Empty;
            return BRACKET_BEGIN + str + BRACKET_END;
        }

        /// <summary>
        /// ホスト名を取得
        /// </summary>
        /// <returns>ホスト名</returns>
        protected static string getHostName() {
            return System.Net.Dns.GetHostName();
        }

        private static void OutputCommonLog(Type loggerType, LogType logType
                                          , string idName, string userId, string surveyId
                                          , string msgId, string[] replaceWords, params object[] details) {
            if (!Enum.IsDefined(typeof(LogType), logType))
                return;
            string logfilename = null;
            if (loggerType == typeof(AppLogger)) {
                logfilename = APP_LOGFILENAME;
            } else if (loggerType == typeof(BatchLogger)) {
                logfilename = BATCH_LOGFILENAME;
            } else if (loggerType == typeof(ReportLogger)) {
                logfilename = REPORT_LOGFILENAME;
            } else {
                return;
            }
            // ログ文字列を生成
            string msg = CreateCommonMessages(userId, surveyId, msgId, replaceWords, details);
            // ログファイル名を変更
            ChangeCommonLogfileName(idName, logfilename);
            // アプリケーションログ出力
            switch (logType) {
                case LogType.Fatal:
                    common.Fatal(msg);
                    break;
                case LogType.Error:
                    common.Error(msg);
                    break;
                case LogType.Warn:
                    common.Warn(msg);
                    break;
                case LogType.Info:
                    common.Info(msg);
                    break;
                case LogType.Debug:
                    common.Debug(msg);
                    break;
            }
        }

        private static void OutputAlertLog(LogType logType, string alertName
                                         , string userId, string surveyId
                                         , string msgId, string[] replaceWords, params object[] details) {
            string logfilename = ALERT_LOGFILENAME + (alertName == null ? getHostName() : alertName);
            // ログ文字列を生成
            string msg = CreateAlertMessages(userId, surveyId, msgId, replaceWords, details);
            // Alert(Zabbix監視)ログファイル名を変更
            ChangeAlertLogfileName(logfilename);
            // Alert(Zabbix監視)ログ出力
            switch (logType) {
                case LogType.Fatal:
                    alert.Fatal(msg);
                    break;
                case LogType.Error:
                    alert.Error(msg);
                    break;
                case LogType.Warn:
                    alert.Warn(msg);
                    break;
            }
        }

        /// <summary>
        /// ログ出力を行う静的メソッド
        /// </summary>
        /// <param name="loggerType">ロガーのタイプ</param>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="alertName">サーバ名(BatchLogger)または重みづけレベル(ReportLogger)</param>
        /// <param name="idName">画面ID</param>
        /// <param name="userId">ユーザID</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(Type loggerType, LogType logType, string alertName, string idName, string userId, string surveyId
                           , string msgId, string[] replaceWords, params object[] details) {
            OutputCommonLog(loggerType, logType, idName, userId, surveyId, msgId, replaceWords, details);
            switch (logType) {
                case LogType.Fatal:
                case LogType.Error:
                case LogType.Warn:
                    OutputAlertLog(logType, alertName, userId, surveyId, msgId, replaceWords, details);
                    break;
            }
        }

        /// <summary>
        /// ログ出力を行う静的メソッド
        /// </summary>
        /// <param name="loggerType">ロガーのタイプ</param>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="idName">画面ID</param>
        /// <param name="userId">ユーザID</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(Type loggerType, LogType logType, string idName, string userId, string surveyId
                                   , string msgId, string[] replaceWords, params object[] details) {
            OutputLog(loggerType, logType, null, idName, userId, surveyId, msgId, replaceWords, details);
        }
    }
}
