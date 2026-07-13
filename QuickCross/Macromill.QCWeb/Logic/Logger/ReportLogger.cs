#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Logger.cs
 * バージョン：1.0.0
 * 概　　　要：共通クラス（レポートログ出力）
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
using System.IO;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Common;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// QCWeb共通クラス（レポートログ出力）
    /// </summary>
    /// <remarks>レポートログ出力を行うために利用</remarks>
    /// <example>
    /// <code lang="C#">
    /// ReportLoggerを初期化
    /// ReportLogger.init("バッチID");
    /// 
    /// FATALログを出力
    /// ReportLogger.Fatal("重みづけレベル", "個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列"], ["ログ詳細追加情報"], ・・・, [exception]]);
    ///
    /// ERRORログを出力
    /// ReportLogger.Error("重みづけレベル", "個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列"], ["ログ詳細追加情報"], ・・・, [exception]]);
    ///
    /// WARNログを出力
    /// ReportLogger.Warn("重みづけレベル", "個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列"], ["ログ詳細追加情報"], ・・・, [exception]]);
    ///   
    /// INFOログを出力
    /// ReportLogger.Info("個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列]", ["ログ詳細追加情報"], ・・・, [exception]]);
    ///   
    /// DEBUGログを出力
    /// ReportLogger.Debug("個別調査ID", "メッセージID", [["ログメッセージ置き換え文字列配列", ["ログ詳細追加情報"], ・・・, [exception]]);
    /// </code>
    /// </example>
    [ComVisible(false), Guid("2E00275F-C67B-446C-81E5-BFE346345169")]
    public class ReportLogger : Loggers {
        /// <summary>バッチID</summary>
        private static string BatchId = null;

        /// <summary>
        /// Zabbix管理ログルートディレクトリ
        /// </summary>
        private static string[] _alertLogRootDirectorys;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ReportLogger() { }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="batchId">バッチID</param>
        public static void init(string batchId) {
            //バッチID
            BatchId = batchId;

            // レポートログのログ初期化
            initCommon(COMMON_LOGGER, COMMON_APPENDER);

            // Alert(Zabbix監視)ログのログ初期化
            initAlert(ALERT_LOGGER, ALERT_APPENDER);

            DeleteDefaultLogFile();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="rootDirs">Zabbix監視ログルートディレクトリ配列</param>
        public static void init(string[] rootDirs) {
            _alertLogRootDirectorys = rootDirs;

            // レポート出力のみ出力先の切り替える設定を行う
            if (!ExistsAlertLogDir()) {
                throw new QCWebException(
                    new Message("Zabbix監視ログルートディレクトリが存在しません。{0}/{1}")
                    , GlobalsCommonConstant.LogLevel.FATAL, rootDirs);
            }
        }

        /// <summary>
        /// ログ出力根幹
        /// </summary>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="level">重みづけレベル</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(LogType logType, string level, string surveyId, string msgId, string[] replaceWords, params object[] details) {
            GlobalsCommonConstant.ReportProcLevel procLevel;
            GlobalsCommonConstant.ReportLogKind logKind;
            if (!Enum.TryParse<GlobalsCommonConstant.ReportProcLevel>(level, out procLevel)) {
                logKind = GlobalsCommonConstant.ReportLogKind.Common;
            } else {
                switch (procLevel) {
                    case GlobalsCommonConstant.ReportProcLevel.CHK_LIST:
                        logKind = GlobalsCommonConstant.ReportLogKind.ChkList;
                        break;
                    case GlobalsCommonConstant.ReportProcLevel.TINY:
                        logKind = GlobalsCommonConstant.ReportLogKind.ReportLv1;
                        break;
                    case GlobalsCommonConstant.ReportProcLevel.SMALL:
                        logKind = GlobalsCommonConstant.ReportLogKind.ReportLv2;
                        break;
                    case GlobalsCommonConstant.ReportProcLevel.MEDIUM:
                        logKind = GlobalsCommonConstant.ReportLogKind.ReportLv3;
                        break;
                    case GlobalsCommonConstant.ReportProcLevel.LARGE:
                        logKind = GlobalsCommonConstant.ReportLogKind.ReportLv4;
                        break;
                    case GlobalsCommonConstant.ReportProcLevel.EXTRA_LARGE:
                        logKind = GlobalsCommonConstant.ReportLogKind.ReportLv5;
                        break;
                    case GlobalsCommonConstant.ReportProcLevel.TEMPLATE_CHK:
                        logKind = GlobalsCommonConstant.ReportLogKind.FmtChk;
                        break;
                    default:
                        logKind = GlobalsCommonConstant.ReportLogKind.Common;
                        break;
                }
            }
            OutputLog(typeof(ReportLogger), logType, logKind.ToString(), BatchId, BATCH_NAME, surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// ログ出力根幹
        /// </summary>
        /// <param name="logType">ログの種類を表すLogType列挙型の値</param>
        /// <param name="surveyId">個別調査ID</param>
        /// <param name="msgId">メッセージID</param>
        /// <param name="replaceWords">ログメッセージ置き換え文字列配列</param>
        /// <param name="details">ログ詳細追加情報文字列配列</param>
        public static void OutputLog(LogType logType, string surveyId, string msgId, string[] replaceWords, params object[] details) {
            //OutputLog(typeof(ReportLogger), logType, BatchId, BATCH_NAME, surveyId, msgId, replaceWords, details);
            OutputLog(logType, "", surveyId, msgId, replaceWords, details);
        }

        /// <summary>
        /// DEBUGログ出力
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message) {
            OutputLog(LogType.Debug, message, null, null);
        }

        /// <summary>
        /// Zabbix監視ログルートディレクトリが存在するか
        /// </summary>
        /// <returns>true: ログルートディレクトリが存在する   false: ログルートディレクトリが存在しない</returns>
        private static bool ExistsAlertLogDir() {
            bool flag = false;
            int index = 0;

            if (_alertLogRootDirectorys != null) {
                for (index = 0; index < _alertLogRootDirectorys.Length; index++) {
                    // ログルートディレクトリが存在するか
                    if (Directory.Exists(_alertLogRootDirectorys[index])) {
                        flag = true;
                        break;
                    } else {
                        try {
                            // ログルートディレクトリを作成する
                            Directory.CreateDirectory(_alertLogRootDirectorys[index]);
                            flag = true;
                            break;
                        } catch(Exception) {
                            ; // 何もしない
                        }
                    }
                }
            }

            // ログルートディレクトリが存在するならば、ロガーのルートディレクトリを設定する
            if (flag) {
                SetAlertLogRootDirectory(_alertLogRootDirectorys[index]);
            }

            return flag;
        }
    }
}