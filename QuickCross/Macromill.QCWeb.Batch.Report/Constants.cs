#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Constants.cs
 * バージョン：1.0.0
 * 概　　　要：メッセージIDなどの定数の定義
 * 作　成　日：2012/7/3
 * 作　成　者：井川はるき
 * 更　新　日：2012/7/3
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Batch.Report
{
    /// <summary>
    /// Reportバッチ内の定数(主にメッセージID)を定義した静的クラス
    /// </summary>
    [ComVisible(false), Guid("938D3B6D-D9BA-4174-AF1D-C94347B94E8A")]
    public static class Constants
    {
        /// <summary>
        /// Reportバッチ内の共有定数を定義した静的クラス
        /// </summary>
        [ComVisible(false), Guid("0FB5A23B-68E6-434D-979D-EC4D11667899")]
        public static class GlobalConstants
        {
            /// <summary>
            /// ReportバッチのバッチID
            /// </summary>
            public const string BATCH_ID = "QCB030101";

            /// <summary>
            /// 共有メッセージIDを定義した静的クラス
            /// </summary>
            [ComVisible(false), Guid("DF262F69-D48D-43B0-8209-41AD56A27508")]
            public static class MessageID
            {
                // 連番00xx
            }
        }

        /// <summary>
        /// 狭い範囲で使用する定数を定義した静的クラス
        /// </summary>
        [ComVisible(false), Guid("41D1C2FD-E22E-4E63-8166-B7698D6F926A")]
        public static class LocalConstants
        {
            /// <summary>
            /// QCB030101プロジェクト内の定数を定義したクラス
            /// </summary>
            [ComVisible(false), Guid("A179A79A-1C88-4345-99F6-3838B68CAE69")]
            public static class Executer
            {
                /// <summary>
                /// Program.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("55F709B3-65FB-4E4D-919A-681D996BA420")]
                public static class ProgramMessageID
                {
                    // 連番10xx

                }

                /// <summary>
                /// QCB030101LogicImpl.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("D83EC500-3AB0-433B-B9BA-E6C101F1FA85")]
                public static class QCB030101LogicImplMessageID
                {
                    // 連番11xx

                    #region BatchProcessStart                    
                    /// <summary>
                    /// 自身のサーバコード:{0}
                    /// </summary>
                    public const string SERVERCODE_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1100";
                    /// <summary>
                    /// リクエスト問い合わせのインターバル(ミリ秒):{0}
                    /// </summary>
                    public const string REQUEST_INTERVAL_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1101";
                    /// <summary>
                    /// CPU監視のインターバル(ミリ秒):{0}
                    /// </summary>
                    public const string CPU_WATCH_INTERVAL_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1102";
                    /// <summary>
                    /// CPU負荷の閾値(%):{0}
                    /// </summary>
                    public const string MAX_CPU_USABLE_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1103";
                    /// <summary>
                    /// CPU過剰負荷時間の限界値(分):{0}
                    /// </summary>
                    public const string MAX_OVERWORK_TIME_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1104";
                    /// <summary>
                    /// 生成処理時間の限界値(分):{0}
                    /// </summary>
                    public const string MAX_RUNNING_TIME_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1105";
                    /// <summary>
                    /// 処理重度:{0}
                    /// </summary>
                    public const string PROCESSING_WEIGHT_CAPTION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1106";
                    /// <summary>
                    /// {0}ミリ秒ポーリングします。
                    /// </summary>
                    public const string POLING_INFO_MESSAGE_ID = GlobalConstants.BATCH_ID + "1107";

                    /// <summary>
                    /// Reportバッチ処理を開始します。
                    /// </summary>
                    public const string BATCH_START_MESSAGE_ID = GlobalConstants.BATCH_ID + "1108";
                    /// <summary>
                    /// 以下の環境設定情報で動作します。
                    /// </summary>
                    public const string ENVIRONMENT_SETTING_MESSAGE_ID = GlobalConstants.BATCH_ID + "1109";
                    #endregion

                    #region ReportProc                    
                    /// <summary>
                    /// 規定時間を超えた処理がされています。
                    /// </summary>
                    public const string OVERWORK_TIMEOUT_INFO_MESSAGE_ID = GlobalConstants.BATCH_ID + "1110";
                    /// <summary>
                    /// 規定時間を超えた処理が行われたため終了します。
                    /// </summary>
                    public const string OVERWORK_TIMEOUT_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "1111";
                    #endregion

                    /// <summary>
                    /// 処理対象情報の出力
                    /// </summary>
                    public const string PROCESSING_OBJECT_MESSAGE_ID = GlobalConstants.BATCH_ID + "1112";
                    /// <summary>
                    /// リクエストサーバ：{0}
                    /// </summary>
                    public const string REQUEST_SERVER_MESSAGE_ID = GlobalConstants.BATCH_ID + "1113";
                    /// <summary>
                    /// リクエストID：{0}
                    /// </summary>
                    public const string REQUEST_ID_MESSAGE_ID = GlobalConstants.BATCH_ID + "1114";
                    /// <summary>
                    /// 出力物作成依頼キューがありません。
                    /// </summary>
                    public const string PROC_REQUEST_NONE_QUE_MESSAGE_ID = GlobalConstants.BATCH_ID + "1115";
                    /// <summary>
                    /// 行ロックされています。リクエストID：{0}
                    /// </summary>
                    public const string ROW_LOCK_MESSAGE_ID = GlobalConstants.BATCH_ID + "1116";
                    /// <summary>
                    /// Report.Requestクラス生成に失敗しました。{0}
                    /// </summary>
                    public const string REPORT_REQUEST_ERROR_MESSAGE_ID = GlobalConstants.BATCH_ID + "1117";

                    /// <summary>
                    /// 生成処理時間を超えたので強制終了します。処理時間:{0} 生成処理既定時間:{1}
                    /// </summary>
                    public const string FORCED_TERMINATION_MESSAGE_ID = GlobalConstants.BATCH_ID + "1118";
                    /// <summary>
                    /// 規定時間待機します：{0}秒
                    /// </summary>
                    public const string WAIT_MESSAGE_ID = GlobalConstants.BATCH_ID + "1119";

                    /// <summary>
                    /// ZIP圧縮します。{0}
                    /// </summary>
                    public const string ZIP_MESSAGE_ID = GlobalConstants.BATCH_ID + "1120";
                    /// <summary>
                    /// SFTP転送を行います。{0}→{1}
                    /// </summary>
                    public const string SFTP_MESSAGE_ID = GlobalConstants.BATCH_ID + "1121";

                }
            }

            /// <summary>
            /// Macromill.QCWeb.Batch.Reportプロジェクト内の定数を定義したクラス
            /// </summary>
            [ComVisible(false), Guid("92E8A586-8CBB-47AA-806F-8FCD0DCFFD62")]
            public static class ClassLibrary
            {
                /// <summary>
                /// Request.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("FBB5BE4B-0E53-4B79-9290-EC26619B1422")]
                public static class RequestMessageID
                {
                    // 連番20xx

                    #region MakeRequest                    
                    /// <summary>
                    /// リクエスト情報の取得に失敗しました。リクエストID:{0}
                    /// </summary>
                    public const string GET_REQUEST_INFORMATION_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "2000";
                    /// <summary>
                    /// 出力物情報の取得に失敗しました。リクエストID:{0}
                    /// </summary>
                    public const string GET_OUTPUT_INFORMATION_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "2001";
                    /// <summary>
                    /// 出力物の種類に不明な値が指定されています。出力物共通ID:{0}
                    /// </summary>
                    public const string UNKNOWN_OUTPUT_TYPE_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "2010";
                    #endregion

                }

                /// <summary>
                /// Reportset.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("E346FF44-7AA5-42BF-932D-D884BCFC46E2")]
                public static class ReportsetMessageID
                {
                    // 連番21xx

                }

                /// <summary>
                /// Output.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("789E18A2-1F0D-41E5-88D3-097519B52B8C")]
                public static class OutputMessageID
                {
                    // 連番22xx

                }

                /// <summary>
                /// Table.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("52C3F247-B1A4-4702-8E64-837530848536")]
                public static class TableMessageID
                {
                    // 連番23xx/24xx

                    /// <summary>
                    /// {0}の集計表設定情報{1}が不足しています。
                    /// </summary>
                    public const string INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "2300";
                    /// <summary>
                    /// {0}の集計表設定情報{1}が正しく取得できません。
                    /// </summary>
                    public const string INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "2301";
                }

                /// <summary>
                /// ConfigReader.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("069091F8-FACD-41D6-919B-C47607AF3757")]
                public static class ConfigReaderMessageID
                {
                    // 連番28xx

                }

                /// <summary>
                /// MacroExecuter.cs内で主に使用するメッセージIDを定義した静的クラス
                /// </summary>
                [ComVisible(false), Guid("6D36997F-20D5-416D-9F6F-A254414888A4")]
                public static class MacroExecuterMessageID
                {
                    // 連番29xx

                    #region ExecMacro                    
                    /// <summary>
                    /// Excelマクロブックが見つかりません。'{0}'
                    /// </summary>
                    public const string MACROBOOK_NOT_FOUND_FATAL_MESSAGE_ID = GlobalConstants.BATCH_ID + "2900";
                    #endregion
                }
            }
        }

        #region ラッパー (階層が深いので)
        /// <summary>
        /// GlobalConstants.BATCH_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.GlobalConstants.BATCH_ID"/>
        public const string BATCH_ID = GlobalConstants.BATCH_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.SERVERCODE_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.SERVERCODE_CAPTION_MESSAGE_ID"/>
        public const string SERVERCODE_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.SERVERCODE_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.REQUEST_INTERVAL_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.REQUEST_INTERVAL_CAPTION_MESSAGE_ID"/>
        public const string REQUEST_INTERVAL_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.REQUEST_INTERVAL_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.CPU_WATCH_INTERVAL_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.CPU_WATCH_INTERVAL_CAPTION_MESSAGE_ID"/>
        public const string CPU_WATCH_INTERVAL_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.CPU_WATCH_INTERVAL_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_CPU_USABLE_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_CPU_USABLE_CAPTION_MESSAGE_ID"/>
        public const string MAX_CPU_USABLE_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_CPU_USABLE_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_OVERWORK_TIME_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_OVERWORK_TIME_CAPTION_MESSAGE_ID"/>
        public const string MAX_OVERWORK_TIME_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_OVERWORK_TIME_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_RUNNING_TIME_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_RUNNING_TIME_CAPTION_MESSAGE_ID"/>
        public const string MAX_RUNNING_TIME_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.MAX_RUNNING_TIME_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.PROCESSING_WEIGHT_CAPTION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.PROCESSING_WEIGHT_CAPTION_MESSAGE_ID"/>
        public const string PROCESSING_WEIGHT_CAPTION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.PROCESSING_WEIGHT_CAPTION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.POLING_INFO_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.POLING_INFO_MESSAGE_ID"/>
        public const string POLING_INFO_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.POLING_INFO_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.OVERWORK_TIMEOUT_INFO_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.OVERWORK_TIMEOUT_INFO_MESSAGE_ID"/>
        public const string OVERWORK_TIMEOUT_INFO_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.OVERWORK_TIMEOUT_INFO_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.Executer.QCB030101LogicImplMessageID.OVERWORK_TIMEOUT_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.Executer.QCB030101LogicImplMessageID.OVERWORK_TIMEOUT_FATAL_MESSAGE_ID"/>
        public const string OVERWORK_TIMEOUT_FATAL_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.OVERWORK_TIMEOUT_FATAL_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.RequestMessageID.GET_REQUEST_INFORMATION_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.RequestMessageID.GET_REQUEST_INFORMATION_FATAL_MESSAGE_ID"/>
        public const string GET_REQUEST_INFORMATION_FATAL_MESSAGE_ID = LocalConstants.ClassLibrary.RequestMessageID.GET_REQUEST_INFORMATION_FATAL_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.RequestMessageID.GET_OUTPUT_INFORMATION_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.RequestMessageID.GET_OUTPUT_INFORMATION_FATAL_MESSAGE_ID"/>
        public const string GET_OUTPUT_INFORMATION_FATAL_MESSAGE_ID = LocalConstants.ClassLibrary.RequestMessageID.GET_OUTPUT_INFORMATION_FATAL_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.RequestMessageID.UNKNOWN_OUTPUT_TYPE_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.RequestMessageID.UNKNOWN_OUTPUT_TYPE_FATAL_MESSAGE_ID"/>
        public const string UNKNOWN_OUTPUT_TYPE_FATAL_MESSAGE_ID = LocalConstants.ClassLibrary.RequestMessageID.UNKNOWN_OUTPUT_TYPE_FATAL_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.MacroExecuterMessageID.MACROBOOK_NOT_FOUND_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.MacroExecuterMessageID.MACROBOOK_NOT_FOUND_FATAL_MESSAGE_ID"/>
        public const string MACROBOOK_NOT_FOUND_FATAL_MESSAGE_ID = LocalConstants.ClassLibrary.MacroExecuterMessageID.MACROBOOK_NOT_FOUND_FATAL_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID"/>
        public const string INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID = LocalConstants.ClassLibrary.TableMessageID.INSUFFICIENT_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID"/>
        public const string INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID = LocalConstants.ClassLibrary.TableMessageID.INVALID_TABLE_SETTING_INFORMATION_FATAL_MESSAGE_ID;

        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.BATCH_START_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.BATCH_START_MESSAGE_ID"/>
        public const string BATCH_START_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.BATCH_START_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.ENVIRONMENT_SETTING_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.ENVIRONMENT_SETTING_MESSAGE_ID"/>
        public const string ENVIRONMENT_SETTING_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.ENVIRONMENT_SETTING_MESSAGE_ID;

        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.PROCESSING_OBJECT_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.PROCESSING_OBJECT_MESSAGE_ID"/>
        public const string PROCESSING_OBJECT_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.PROCESSING_OBJECT_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.REQUEST_SERVER_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.REQUEST_SERVER_MESSAGE_ID"/>
        public const string REQUEST_SERVER_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.REQUEST_SERVER_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.REQUEST_ID_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.REQUEST_ID_MESSAGE_ID"/>
        public const string REQUEST_ID_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.REQUEST_ID_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.PROC_REQUEST_NONE_QUE_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.PROC_REQUEST_NONE_QUE_MESSAGE_ID"/>
        public const string PROC_REQUEST_NONE_QUE_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.PROC_REQUEST_NONE_QUE_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.ROW_LOCK_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.ROW_LOCK_MESSAGE_ID"/>
        public const string ROW_LOCK_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.ROW_LOCK_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.REPORT_REQUEST_ERROR_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.REPORT_REQUEST_ERROR_MESSAGE_ID"/>
        public const string REPORT_REQUEST_ERROR_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.REPORT_REQUEST_ERROR_MESSAGE_ID;

        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.FORCED_TERMINATION_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.FORCED_TERMINATION_MESSAGE_ID"/>
        public const string FORCED_TERMINATION_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.FORCED_TERMINATION_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.WAIT_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.WAIT_MESSAGE_ID"/>
        public const string WAIT_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.WAIT_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.ZIP_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.ZIP_MESSAGE_ID"/>
        public const string ZIP_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.ZIP_MESSAGE_ID;
        /// <summary>
        /// LocalConstants.ClassLibrary.TableMessageID.SFTP_MESSAGE_ID
        /// </summary>
        /// <seealso cref="F:Macromill.QCWeb.Batch.Report.Constants.LocalConstants.ClassLibrary.TableMessageID.SFTP_MESSAGE_ID"/>
        public const string SFTP_MESSAGE_ID = LocalConstants.Executer.QCB030101LogicImplMessageID.SFTP_MESSAGE_ID;
        #endregion
    }
}
