#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：BatchProgram.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/7/27
 * 作　成　者：井川はるき
 * 更　新　日：2012/7/27
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using Macromill.QCWeb.Exceptions;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// Programクラス類の共有ロジックを管理するクラス
    /// </summary>
    [ComVisible(false), Guid("D519A9DB-326A-43D8-9E9C-E37BD310582C")]
    public class ProgramSuper {
        private delegate void BatchOutputMethod(LogType logType, string alertName, string surveyId, string msgId, string[] replaceWords, params object[] details);

        /// <summary>
        /// 同一プロセス起動チェック
        /// </summary>
        /// <returns>true:同一プロセス起動中　false:同一プロセス未起動</returns>
        protected bool PrevInstance() {
            return System.Diagnostics.Process.GetProcessesByName(
                    System.Diagnostics.Process.GetCurrentProcess().ProcessName).GetUpperBound(0) > 0;
        }

        /// <summary>
        /// エラー情報出力
        /// </summary>
        /// <param name="ex">例外を表すExceptionクラスのインスタンスへの参照</param>
        /// <param name="loggerType">ロガーのタイプ</param>
        public static void ShowErrorMessage(Exception ex, Type loggerType) {
            if (ex == null) return;
            LogType logType = LogType.Fatal;
            string idName = string.Empty;
            string userId = string.Empty;
            string surveyId = string.Empty;
            string msgId = Constants.CUSTOM_MESSAGE_ID;
            string[] msgParam = null;

            if (ex is QCWebException) {
                QCWebException qcEx = ex as QCWebException;
                if (qcEx.LogLevel == GlobalsCommonConstant.LogLevel.ERROR) {
                    logType = LogType.Error;
                } else if (qcEx.LogLevel == GlobalsCommonConstant.LogLevel.WARN) {
                    logType = LogType.Warn;
                } else if (qcEx.LogLevel == GlobalsCommonConstant.LogLevel.INFO) {
                    logType = LogType.Info;
                }
                idName = qcEx.WindId;
                userId = qcEx.UserId;
                surveyId = qcEx.Qc3UniqueIdStr;
                msgId = qcEx.MsgId;
                msgParam = qcEx.Param;
                ex = qcEx.InnerException;
                if (ex == null) ex = qcEx;

                // Dummyメッセージの設定
                if (loggerType == typeof(AppLogger)) {
                    if (msgParam == null || msgParam.Length == 0) {
                        string dummyMsg = "";
                        if (!string.IsNullOrEmpty(idName)) {
                            dummyMsg = idName + "で";
                        }
                        dummyMsg += ex.GetType().ToString() + "が発生しました。";
                        msgParam = new string[] { dummyMsg };
                    }
                }
            } else {
                if (!string.IsNullOrEmpty(ex.Message)) {
                    msgParam = new string[] { ex.Message };
                } else {
                    // Dummyメッセージの設定
                    if (loggerType == typeof(AppLogger)) {
                        string dummyMsg = "";
                        if (!string.IsNullOrEmpty(idName)) {
                            dummyMsg = idName + "で";
                        }
                        dummyMsg += ex.GetType().ToString() + "が発生しました。";
                        msgParam = new string[] { dummyMsg };
                    }
                }
            }
            if (string.IsNullOrEmpty(idName)) idName = "QCS000000";

            if (loggerType == typeof(AppLogger)) {
                AppLogger.OutputLog(logType, idName, userId, surveyId, msgId, msgParam, ex);
                return;
            }
            BatchOutputMethod outputmethod = null;
            if (loggerType == typeof(BatchLogger)) {
                outputmethod = BatchLogger.OutputLog;
            } else if (loggerType == typeof(ReportLogger)) {
                outputmethod = ReportLogger.OutputLog;
            }

            if (outputmethod != null) outputmethod(logType, System.Net.Dns.GetHostName(), surveyId, msgId, msgParam, ex);
        }
    }
}
