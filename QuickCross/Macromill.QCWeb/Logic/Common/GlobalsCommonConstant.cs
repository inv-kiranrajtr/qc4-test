#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：GlobalsCommonConstant.cs
 * バージョン：1.0.0
 * 概　　　要：Web・バッチ共通Const定義クラス
 * 作　成　日：2012/04/02
 * 作　成　者：寺嶋千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common
{
    /// <summary>
    /// Web・バッチ共通Const定義クラス
    /// </summary>
    [ComVisible(false), Guid("457C9117-1BC2-4F11-BAEC-58FEF359E08F")]
    public abstract class GlobalsCommonConstant
    {
        #region アプリ環境設定キーワード
        /// <summary>AIRs連携URL</summary>
        public const string APP_CONFIG_COMMON_AIRS_WEBAPI_URL = "Common.Airs.WebAPI.Url";

        /// <summary>AIRsファイルサーバ</summary>
        public const string APP_CONFIG_COMMON_AIRS_FILE_SV = "Common.Airs.File.SV";

        /// <summary>AIRsファイルサーバSFTPユーザ</summary>
        public const string APP_CONFIG_COMMON_AIRS_SFTP_USER = "Common.Airs.Sftp.User";

        /// <summary>AIRsファイルサーバSFTPパスワード</summary>
        public const string APP_CONFIG_COMMON_AIRS_SFTP_PASSWORD = "Common.Airs.Sftp.Password";

        /// <summary>サーバの鍵(指紋)</summary>
        public const string APP_CONFIG_COMMON_SSH_HOST_KEY = "Common.Ssh.Host.Key";

        /// <summary>AIRsカレントディレクトリ</summary>
        public const string APP_CONFIG_COMMON_AIRS_CURRENT_DIRECTORY = "Common.Airs.Current.Directory";

        /// <summary>WinSCPのパス</summary>
        public const string APP_CONFIG_COMMON_WINSCP_COMMAND = "Common.Winscp.Command";

        /// <summary>スクリプトパス</summary>
        public const string APP_CONFIG_COMMON_SFTP_SCRIPT_PATH = "Common.Sftp.Script.Path";

        /// <summary>(APサーバ)ローデータTXTのパス</summary>
        public const string APP_CONFIG_COMMON_RAWDATA_PATH_AP = "Common.Rawdata.Path.AP";

        /// <summary>(APサーバ)集計結果TSVのパス</summary>
        public const string APP_CONFIG_COMMON_ACCUMULATE_PATH_AP = "Common.Accumulate.Path.AP";

        /// <summary>AP1のネットワークドライブ</summary>
        public const string APP_CONFIG_COMMON_NETWORK_DRIVE_AP1 = "Common.Network.Drive.AP1";

        /// <summary>AP2のネットワークドライブ</summary>
        public const string APP_CONFIG_COMMON_NETWORK_DRIVE_AP2 = "Common.Network.Drive.AP2";

        /// <summary>(Load・Reportサーバ)集計結果TSVの相対パス</summary>
        public const string COMMON_ACCUMULATE_NETWORK_PATH = "Common.Accumulate.Network.Path";

        /// <summary>QC3ファイルのパスワード</summary>
        public const string COMMON_QC3_PASSWORD = "Common.QC3.Password";

        /// <summary>APの一時パス</summary>
        public const string APP_CONFIG_COMMON_TEMPORARY_PATH_AP = "Common.Temporary.Path.AP";

        /// <summary>Loadの一時パス</summary>
        public const string APP_CONFIG_COMMON_TEMPORARY_PATH_LD = "Common.Temporary.Path.LD";

        /// <summary>Reportの一時パス</summary>
        public const string APP_CONFIG_COMMON_TEMPORARY_PATH_RP = "Common.Temporary.Path.RP";

        /// <summary>取込アイテム数上限</summary>
        public const string APP_CONFIG_LOAD_ITEM_MAX = "Load.Item.Max";

        /// <summary>取込マトリックス内アイテム数上限</summary>
        public const string APP_CONFIG_LOAD_MATRIX_ITEM_MAX = "Load.Matrix.Item.Max";

        /// <summary>取込カテゴリ数上限</summary>
        public const string APP_CONFIG_LOAD_CATEGORY_ITEM_MAX = "Load.Category.Item.Max";

        /// <summary>取込FAアイテム数上限</summary>
        public const string APP_CONFIG_LOAD_FA_ITEM_MAX = "Load.FA.Item.Max";

        /// <summary>(Loadサーバ)APサーバローデータTXTのパス</summary>
        public const string APP_CONFIG_LOAD_AP_RAWDATA_PATH = "Load.AP.Rawdata.Path";

        /// <summary>調査データ保持期限(単位は年)</summary>
        public const string APP_CONFIG_SURVEY_DATA_EFFECTIVE_YAER = "Survey.Data.Effective.Yaer";

        /// <summary>削除日カウントダウン表示日数(単位は月)</summary>
        public const string APP_CONFIG_SURVEY_DELETE_DISP_MONTH = "Survey.Delete.Disp.Month";

        /// <summary>シナリオ作成可能数</summary>
        public const string APP_CONFIG_COMMON_SCENARIO_CREATE_MAX = "Common.Scenario.Create.Max";

        /// <summary>レポートセット数上限</summary>
        public const string APP_CONFIG_SCENARIO_REPORTSET_MAX = "Scenario.Reportset.Max";

        /// <summary>GT/クロス/FA集計の絞込み条件数上限</summary>
        public const string APP_CONFIG_SCENARIO_FILTER_MAX = "Scenario.Filter.Max";

        /// <summary>円グラフ選択肢名非表示比率</summary>
        public const string APP_CONFIG_SCENARIO_PIE_CHART_RATIO = "Scenario.Pie.Chart.Ratio";

        /// <summary>GT表設定追加数上限</summary>
        public const string APP_CONFIG_SCENARIO_GTSETTING_ADD_MAX = "Scenario.GTSetting.Add.Max";

        /// <summary>1シナリオ内の集計セット数上限</summary>
        public const string APP_CONFIG_SCENARIO_AXIS_SURVEYSET_MAX = "Scenario.Axis.Surveyset.Max";

        /// <summary>カテゴリ出力編集 設問まとめ数上限</summary>
        public const string APP_CONFIG_SCENARIO_CATEGORY_OUTPUT_MAX = "Scenario.Category.Output.Max";

        /// <summary>FAリスト FAアイテム数上限</summary>
        public const string APP_CONFIG_FALIST_FA_ITEM_MAX = "FAList.FA.Item.Max";

        /// <summary>FAリスト FA付加アイテム数上限</summary>
        public const string APP_CONFIG_FALIST_FA_ADD_ITEM_MAX = "FAList.FA.Add.Item.Max";

        /// <summary>アップロード可能なファイルサイズ上限(単位はKB)</summary>
        public const string APP_CONFIG_PPT_UPLOAD_SIZE_MAX = "PPT.UpLoad.Size.Max";

        /// <summary>アップロード可能なファイル数上限(単位はファイル数)</summary>
        public const string APP_CONFIG_PPT_UPLOAD_NUM_MAX = "PPT.UpLoad.Num.Max";

        /// <summary>アップロードファイルの保持期間(単位は日)</summary>
        public const string APP_CONFIG_PPT_UPLOAD_EFFECTIVE_DAYS = "PPT.Upload.Effective.Days";

        /// <summary>アップロードファイルの格納先</summary>
        public const string APP_CONFIG_PPT_UPLOAD_DIR = "PPT.Upload.Dir";

        /// <summary>出力ファイルの一時格納先</summary>
        public const string APP_CONFIG_OUTPUT_TEMP_DIR = "Output.Temp.Dir";

        /// <summary>DLファイルの一時格納先</summary>
        public const string APP_CONFIG_DL_TEMP_DIR = "DL.Temp.Dir";

        /// <summary>出力物ファイル保持期間(単位は日)</summary>
        public const string APP_CONFIG_OUTPUT_FILE_EFFECTIVE_DAYS = "Output.File.Effective.Days";

        /// <summary>シナリオ管理 水準1</summary>
        public const string APP_CONFIG_SCENARIO_LEVEL1_PERCENT = "Scenario.Level1.Percent";

        /// <summary>シナリオ管理 水準2</summary>
        public const string APP_CONFIG_SCENARIO_LEVEL2_PERCENT = "Scenario.Level2.Percent";

        /// <summary>アクセス権限管理 アクセスしていない期間(単位は分)</summary>
        public const string APP_CONFIG_ACCESSPERMISSION_ACCESS_OVERTIME = "Accesspermission.Access.Overtime";

        /// <summary>出力物重度判定ポイント(極小|小|中|大|極大)</summary>
        public const string APP_CONFIG_DATAOUTPUT_WAIT_JUDGMENT_POINT = "DataOutput.Wait.Judgment.Point";

        /// <summary>(Reportサーバ)監視ログ出力先の相対パス</summary>
        public const string APP_CONFIG_COMMON_ALERT_NETWORK_PATH = "Common.Alert.Network.Path";

        /// <summary>一度に表示する列数</summary>
        public const string APP_CONFIG_DATA_REFERENCE_DEFAULT_LANDSCAPE_PAGE = "DataReference.DefaultLandscapePage";

        /// <summary>一度に表示する行数</summary>
        public const string APP_CONFIG_DATA_REFERENCE_DEFAULT_PORTRAIT_PAGE = "DataReference.DefaultPortraitPage";

        /// <summary>論理削除調整日数</summary>
        public const string APP_CONFIG_DELETE_ADJUST_DAYS_LOGICAL = "Delete.Adjust.Days.Logical";

        /// <summary>追加データ物理削除調整日数</summary>
        public const string APP_CONFIG_DELETE_ADJUST_DAYS_ADD_DATA = "Delete.Adjust.Days.Add.Data";

        /// <summary>調査データ物理削除調整日数</summary>
        public const string APP_CONFIG_DELETE_ADJUST_DAYS_SURVEY_DATA = "Delete.Adjust.Days.Survey.Data";

        /// <summary>一括自動コメント作成時の作成可能数上限</summary>
        public const string APP_CONFIG_COMMON_AUTOCOMMENT_MAX = "Common.AutoComment.Max";

        /// <summary>PPテンプレートのSFTPアップロード先パス</summary>
        public const string APP_CONFIG_PPT_SFTP_UPLOAD_DIR = "PPT.Sftp.Upload.Dir";

        /// <summary>出力物のSFTPアップロード先パス</summary>
        public const string APP_CONFIG_OUTPUT_FILE_SFTP_Upload_DIR = "Output.File.Sftp.Upload.Dir";

        /// <summary>アイテムビューFAの1ページの最大表示行数</summary>
        public const string APP_CONFIG_ITEMVIEW_FA_DISPLAY_PAGEMAXROWS = "ItemView.FA.Display.PageMaxRows";

        /// <summary>AIRs連携認証用キー</summary>
        public const string APP_CONFIG_AIRS_AUTH_KEY = "Airs.Auth.Key";

        /// <summary>AIRs連携認証時間(秒)</summary>
        public const string APP_CONFIG_AIRS_AUTH_TIME = "Airs.Auth.Time";

        /// <summary>Excelマクロ格納パス</summary>
        public const string APP_CONFIG_MACROBOOK_DIRECTORY_PATH = "Macrobook.Directory.Path";

        /// <summary>ブラウザの言語設定の基準識別子</summary>
        public const string APP_CONFIG_COMMON_USERLANGUAGE_BASENAME = "Common.UserLanguage.Basename";

        /// <summary>ブラウザの言語設定の既定値</summary>
        public const string APP_CONFIG_COMMON_USERLANGUAGE_DEFAULT = "Common.UserLanguage.Default";

        /// <summary>(Loadサーバ)APサーバローデータTSVのパス</summary>
        public const string APP_CONFIG_LOAD_AP_TSV_PATH = "Load.AP.Tsv.Path";

        /// <summary>クロス 集計タブ上限数</summary>
        public const string APP_CONFIG_SCENARIO_CROSS_SET_TAB_LIMIT = "Scenario.Cross.Set.Tab.Limit";

        /// <summary>クロス 集計タブ内の集計軸数上限</summary>
        public const string APP_CONFIG_SCENARIO_CROSS_AXIS_LIMIT = "Scenario.Cross.Axis.Limit";

        /// <summary>PPTデフォルトテンプレートのパス</summary>
        public const string APP_CONFIG_PPT_DEFAULT_TEMPLATE_PATH = "PPT.Default.Template.Path";

        /// <summary>出力物重度毎の予想処理時間</summary>
        public const string APP_CONFIG_PROCWEIGHT_ESTIMATE_TIME = "ProcWeight.Estimate.Time";

        /// <summary>小フォント</summary>
        public const string QCWCHARTS_SMALL_FONT = "QcwCharts.SmallFont";

        /// <summary>中フォント</summary>
        public const string QCWCHARTS_MEDIUM_FONT = "QcwCharts.MediumFont";

        /// <summary>大フォント</summary>
        public const string QCWCHARTS_LARGE_FONT = "QcwCharts.LargeFont";

        /// <summary>fontFamily</summary>
        public const string QCWCHARTS_FONT_FAMILY = "QcwCharts.FontFamily";

        /// <summary>凡例のラベル幅（折り返し）</summary>
        public const string QCWCHARTS_LEGEND_LINE_WIDTH = "QcwCharts.LegendLineWidth";

        /// <summary>円グラフのラベル幅</summary>
        public const string QCWCHARTS_PIE_LABEL_WIDTH = "QcwCharts.PieLabelWidth";

        /// <summary>円グラフのサイズ</summary>
        public const string QCWCHARTS_PIE_SIZE = "QcwCharts.PieSize";

        /// <summary>ツールチップのラベル幅（折り返し）</summary>
        public const string QCWCHARTS_TOOLTIP_LINE_WIDTH = "QcwCharts.TooltipLineWidth";

        /// <summary>X軸の選択肢上限（横棒）</summary>
        public const string QCWCHARTS_XAXIS_BAR_CATEGORY_SIZE = "QcwCharts.XaxisBarCategorySize";

        /// <summary>X軸のラベル幅（横棒）</summary>
        public const string QCWCHARTS_XAXIS_BAR_LABEL_WIDTH = "QcwCharts.XaxisBarLabelWidth";

        /// <summary>X軸の選択肢上限（縦棒）</summary>
        public const string QCWCHARTS_XAXIS_COLUMN_CATEGORY_SIZE = "QcwCharts.XaxisColumnCategorySize";

        /// <summary>X軸のラベル行数（縦棒）</summary>
        public const string QCWCHARTS_XAXIS_COLUMN_LABEL_LINE_SIZE = "QcwCharts.XaxisColumnLabelLineSize";

        /// <summary>X軸のラベル幅（縦棒）</summary>
        public const string QCWCHARTS_XAXIS_COLUMN_LABEL_WIDTH = "QcwCharts.XaxisColumnLabelWidth";

        /// <summary>表示する小数点の桁数最小</summary>
        public const string APP_CONFIG_NUMERIC_CONTENTS_MIN = "Numeric.Contents.Min";

        /// <summary>表示する小数点の桁数最大</summary>
        public const string APP_CONFIG_NUMERIC_CONTENTS_MAX = "Numeric.Contents.Max";

        /// <summary>INTEGRATEの条件数上限</summary>
        public const string APP_CONFIG_INTEGRATE_CONDITION_MAX = "DPIntegrate.Condition.Max";

        /// <summary>データ加工数式計算プログラムのパス</summary>
        public const string APP_CONFIG_COMMON_COMPUTE_EXPRESSION_PATH = "Common.ComputeExpression.Path";

        /// <summary>絞込み条件上限数（データ加工）</summary>
        public const string APP_CONFIG_DATA_PROCESS_FILTER_MAX = "DataProcess.Filter.Max";

        /// <summary>横(縦)棒付きの折れ線の上限値</summary>
        public const string APP_CONFIG_UPPER_LIMIT_VALUE_OF_POLYLINE_WITH_BAR = "UpperLimitValue.Of.PolylineWithBar";

        /// <summary>クライアントCallBackのリトライ回数上限</summary>
        public const string APP_CONFIG_SCENARIO_CALLSERVER_RETRYLIMIT = "Scenario.CallServer.RetryLimit";

        /// <summary>クライアントCallBackのリトライ間隔(msec)</summary>
        public const string APP_CONFIG_SCENARIO_CALLSERVER_RETRYINTERVAL = "Scenario.CallServer.RetryInterval";

        /// <summary>お知らせ通知のリトライ回数上限</summary>
        public const string APP_CONFIG_SCENARIO_NOTIFY_RETRYLIMIT = "Scenario.Notify.RetryLimit";

        /// <summary>お知らせ通知のリトライ間隔(msec)</summary>
        public const string APP_CONFIG_SCENARIO_NOTIFY_RETRYINTERVAL = "Scenario.Notify.RetryInterval";

        /// <summary>AIRs連携整合性チェックを行うかを判定するフラグ</summary>
        public const string APP_CONFIG_AIRS_COORDINATE_CHECK_FLAG = "AIRs.Coordinate.Check.Flag";

        /// <summary>メインURL</summary>
        public const string APP_CONFIG_DOMAIN_URL = "Domain.URL";

        #region 検定対応
        /// <summary>検定ログファイル保持期間</summary>
        public const string APP_CONFIG_TESTLOG_EFFECTIVE_DAYS = "TestLog.Effective.Days";
        /// <summary>検定ログ一時出力パス</summary>
        public const string APP_CONFIG_TESTLOG_TEMP_PATH = "TestLog.Temp.Path";
        #endregion
        #endregion

        #region AIRs連携情報
        /// <summary>
        /// AIRs連携WebAPI区分
        /// </summary>
        [ComVisible(false)]
        public enum AIRS_REQUEST_KIND
        {
            /// <summary>
            /// 処理対象の要求受信IF
            /// </summary>
            PROC_OBJECT = 1,
            /// <summary>
            /// 処理完了の通知IF
            /// </summary>
            PROC_END = 2,
            /// <summary>
            /// 削除対象の要求受信IF
            /// </summary>
            DELETE_OBJECT = 3,
            /// <summary>
            /// クライアント情報の取得IF
            /// </summary>
            CLIENT_INFO = 4
        }

        /// <summary>
        /// AIRs連携処理結果
        /// </summary>
        [ComVisible(false)]
        public enum AIRS_RESULT_CODE
        {
            /// <summary>
            /// なし
            /// </summary>
            None = -999,
            /// <summary>
            /// 失敗
            /// </summary>
            Failure = -1,
            /// <summary>
            /// 成功
            /// </summary>
            Success = 0,
            /// <summary>
            /// 成功(検索結果なし)
            /// </summary>
            SuccessNone = 1
        }

        /// <summary>
        /// AIRs連携 処理区分
        /// </summary>
        [ComVisible(false)]
        public enum AIRS_PROC_KBN
        {
            /// <summary>
            /// なし
            /// </summary>
            None = -999,
            /// <summary>
            /// 標準納品
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 追加納品
            /// </summary>
            ADD = 1
        }
        #endregion

        #region テーブル情報管理
        /// <summary>
        /// テーブル名 prefix
        /// </summary>
        public const string TABLE_PREFIX = "T_";
        /// <summary>
        /// テーブル名 suffix：ノーマル（SA/MA/N/D型)
        /// </summary>
        public const string TABLE_SUFFIX = "_";
        /// <summary>
        /// テーブル名 suffix：FA型
        /// </summary>
        public const string TABLE_SUFFIX_FA = "_FA";
        /// <summary>
        /// テーブル名 suffix：ウエイトバック
        /// </summary>
        public const string TABLE_SUFFIX_WB = "_WB";
        /// <summary>
        /// フィールド名 prefix：ノーマル（SA/MA/N/D型)
        /// </summary>
        public const string FIELD_PREFIX = "Q";
        /// <summary>
        /// フィールド名 prefix：FA型
        /// </summary>
        public const string FIELD_PREFIX_FA = "FA";
        /// <summary>
        /// テーブルのカラム最大値：999
        /// </summary>
        public const int MAX_COLUMN_COUNT = 999;
        /// <summary>
        /// FA型のテーブル番号
        /// </summary>
        public const int FA_TABLE_NO = -1;
        /// <summary>
        /// WeightBackのテーブル番号
        /// </summary>
        public const int WB_TABLE_NO = -9;
        #endregion

        #region 汎用コードマスタ
        /// <summary>
        /// 汎用コードマスタ（キー）
        /// </summary>
        [ComVisible(false), Guid("7037D4BE-FAA4-4c04-ACA7-CC0804F88BEF")]
        public static class GROUP_KEY
        {
            /// <summary>グラデーション方向</summary>
            public static string GradationType = "GradationType";
            /// <summary>出力ファイル形式</summary>
            public static string OutputFileType = "OutputFileType";
            /// <summary>Excel出力形式</summary>
            public static string ExcelType = "ExcelType";
            /// <summary>出力タイプ</summary>
            public static string OutputType = "OutputType";
            /// <summary>無回答の出力文字</summary>
            public static string NoAnswerChar = "NoAnswerChar";
            /// <summary>用紙サイズ</summary>
            public static string PaperSize = "PaperSize";
            /// <summary>用紙の向き</summary>
            public static string PaperOrientation = "PaperOrientation";
            /// <summary>PowerPoint出力形式</summary>
            public static string PPType = "PPType";
            /// <summary>SA</summary>
            public static string GraphTypeSA = "GraphTypeSA";
            /// <summary>SAマトリックス</summary>
            public static string GraphTypeSAMatrix = "GraphTypeSAMatrix";
            /// <summary>MA</summary>
            public static string GraphTypeMA = "GraphTypeMA";
            /// <summary>MAマトリックス</summary>
            public static string GraphTypeMAMatrix = "GraphTypeMAMatrix";
            /// <summary>N(割合回答)</summary>
            public static string GraphTypeNRate = "GraphTypeNRate";
            /// <summary>N(順位回答)</summary>
            public static string GraphTypeNRanking = "GraphTypeNRanking";
            /// <summary>グラフ種別</summary>
            public static string GraphType = "GraphType";
            /// <summary>グラフタイプクロス</summary>
            public static string GraphTypeCross = "GraphTypeCross";
            /// <summary>集計種類</summary>
            public static string TotalizationKbn = "TotalizationKbn";
            /// <summary>MTOS処理方法</summary>
            public static string MtosProcessKbn = "MtosProcessKbn";
            /// <summary>GROUP算出値</summary>
            public static string GroupCalcType = "GroupCalcType";
            /// <summary>データ加工Recode入力補助DropDown　先頭、末尾</summary>
            public static string GroupRecodeScaleCate = "GroupRecodeScaleCate";
        }

        /// <summary>
        /// 汎用コードマスタ（コード）
        /// </summary>
        [ComVisible(false), Guid("5A2262B5-ECE0-4685-9364-00D42ECE1431")]
        public static class CODE_VALUE
        {
            /// <summary>001</summary>
            public static string CODE_001 = "001";
            /// <summary>002</summary>
            public static string CODE_002 = "002";
            /// <summary>003</summary>
            public static string CODE_003 = "003";
            /// <summary>004</summary>
            public static string CODE_004 = "004";
            /// <summary>005</summary>
            public static string CODE_005 = "005";
            /// <summary>006</summary>
            public static string CODE_006 = "006";
            /// <summary>007</summary>
            public static string CODE_007 = "007";
            /// <summary>008</summary>
            public static string CODE_008 = "008";
            /// <summary>009</summary>
            public static string CODE_009 = "009";
            /// <summary>010</summary>
            public static string CODE_010 = "010";
            /// <summary>011</summary>
            public static string CODE_011 = "011";
            /// <summary>012</summary>
            public static string CODE_012 = "012";
            /// <summary>013</summary>
            public static string CODE_013 = "013";
            /// <summary>051</summary>
            public static string CODE_051 = "051";
            /// <summary>056</summary>
            public static string CODE_056 = "056";

            /// <summary>なし(= 001)</summary>
            public const string GRADATION_TYPE_NONE = "001";
            /// <summary>横(= 002)</summary>
            public const string GRADATION_TYPE_MSO_GRADIENT_HORIZONTAL = "002";
            /// <summary>縦 (= 003)</summary>
            public const string GRADATION_TYPE_MSO_GRADIENT_VERTICAL = "003";
            /// <summary>右上対角線 (= 004)</summary>
            public const string GRADATION_TYPE_MSO_GRADIENT_DIAGONALUP = "004";
            /// <summary>右下対角線 (= 005)</summary>
            public const string GRADATION_TYPE_MSO_GRADIENT_DIAGONALDOWN = "005";
            /// <summary>角から (= 006)</summary>
            public const string GRADATION_TYPE_MSO_GRADIENT_FROMCORNER = "006";
            /// <summary>中央から (= 007)</summary>
            public const string GRADATION_TYPE_MSO_GRADIENT_FROMCENTER = "007";
        }
        #endregion

        #region グラフ種別
        /// <summary>グラフ種別：QC円グラフ</summary>
        public const string GRAPH_TYPE_QCCIRCLE = "001";
        /// <summary>グラフ種別：QC横帯グラフ</summary>
        public const string GRAPH_TYPE_QCWIDTHBELT = "002";
        /// <summary>グラフ種別：QC縦帯グラフ</summary>
        public const string GRAPH_TYPE_QCHEIGHTBELT = "003";
        /// <summary>グラフ種別：QC横棒グラフ</summary>
        public const string GRAPH_TYPE_QCWIDTHSTICK = "004";
        /// <summary>グラフ種別：QC縦棒グラフ</summary>
        public const string GRAPH_TYPE_QCHEIGHTSTICK = "005";
        /// <summary>グラフ種別：QCM横棒グラフ</summary>
        public const string GRAPH_TYPE_QCMWIDTHSTICK = "006";
        /// <summary>グラフ種別：QCM縦棒グラフ</summary>
        public const string GRAPH_TYPE_QCMHEIGHTSTICK = "007";
        /// <summary>グラフ種別：QCM円グラフ</summary>
        public const string GRAPH_TYPE_QCMCIRCLE = "008";
        /// <summary>グラフ種別：QC横棒RATグラフ</summary>
        public const string GRAPH_TYPE_QCWIDTHSTICKRAT = "009";
        /// <summary>グラフ種別：QC縦棒RATグラフ</summary>
        public const string GRAPH_TYPE_QCHEIGHTSTICKRAT = "010";
        /// <summary>グラフ種別：QCM円RATグラフ</summary>
        public const string GRAPH_TYPE_QCMCIRCLERAT = "011";
        /// <summary>グラフ種別：QC積上横棒グラフ</summary>
        public const string GRAPH_TYPE_QCWIDTHONSTICK = "012";
        /// <summary>グラフ種別：QC積上縦棒グラフ</summary>
        public const string GRAPH_TYPE_QCHEIGHTONSTICK = "013";
        /// <summary>グラフ種別：QC折れ線グラフ</summary>
        public const string GRAPH_TYPE_QCLINE = "014";
        /// <summary>グラフ種別：QCM横帯グラフ</summary>
        public const string GRAPH_TYPE_SA_MATRIX_QCM_WIDTH_BELT_CODE_VALUE = "015";
        /// <summary>グラフ種別：QCM縦帯グラフ</summary>
        public const string GRAPH_TYPE_SA_MATRIX_QCM_HEIGHT_BELT_CODE_VALUE = "016";
        #endregion

        #region グラフ種別(色設定情報テーブル)
        /// <summary>グラフ種別：横棒積上</summary>
        public const string TYPE_CODE_QCWIDTHONSTICK = "1";
        /// <summary>グラフ種別：縦棒積上</summary>
        public const string TYPE_CODE_QCHEIGHTONSTICK = "2";
        /// <summary>グラフ種別：円</summary>
        public const string TYPE_CODE_QCCIRCLE = "3";
        /// <summary>グラフ種別：横棒</summary>
        public const string TYPE_CODE_QCWIDTHSTICK = "4";
        /// <summary>グラフ種別：縦棒</summary>
        public const string TYPE_CODE_QCHEIGHTSTICK = "5";
        /// <summary>グラフ種別：グラフの背景</summary>
        public const string TYPE_CODE_QCBACKGROUND = "6";
        /// <summary>グラフ種別：折れ線</summary>
        public const string TYPE_CODE_QCLINE = "7";

        /// <summary>グラフ種別：横棒積上</summary>
        public const string TYPE_NAME_QCWIDTHONSTICK = "横棒積上";
        /// <summary>グラフ種別：縦棒積上</summary>
        public const string TYPE_NAME_QCHEIGHTONSTICK = "縦棒積上";
        /// <summary>グラフ種別：円</summary>
        public const string TYPE_NAME_QCCIRCLE = "円";
        /// <summary>グラフ種別：横棒</summary>
        public const string TYPE_NAME_QCWIDTHSTICK = "横棒";
        /// <summary>グラフ種別：縦棒</summary>
        public const string TYPE_NAME_QCHEIGHTSTICK = "縦棒";
        /// <summary>グラフ種別：グラフの背景</summary>
        public const string TYPE_NAME_QCBACKGROUND = "グラフの背景";
        /// <summary>グラフ種別：折れ線</summary>
        public const string TYPE_NAME_QCLINE = "折れ線";
        #endregion

        #region メッセージID
        /// <summary>メッセージID（共通）：ハンドリングできなかったエラー</summary>
        public const string MSGID_COMMON_UNHANDLED_ERR = "QCCMN00000000";
        /// <summary>メッセージID（共通）：AIRs連携エラー</summary>
        public const string MSGID_COMMON_AIRS_COM_ERR = "QCCMN90000101";

        /// <summary>メッセージID（バッチ共通）：バッチ開始ログ</summary>
        public const string MSGID_BATCH_START = "QCB0000000100";
        /// <summary>メッセージID（バッチ共通）：バッチ終了ログ</summary>
        public const string MSGID_BATCH_END = "QCB0000000101";
        /// <summary>メッセージID（バッチ共通）：バッチ起動済</summary>
        public const string MSGID_BATCH_RUNNING = "QCB0000000102";
        /// <summary>メッセージID（バッチ共通）：メンテナンス中</summary>
        public const string MSGID_MAINTENANCE = "QCB0000000103";

        /// <summary>メッセージID（AIRs連携バッチ）：処理対象なし</summary>
        public const string MSGID_AIRS_DATA_NOTHING = "QCB0000000104";
        /// <summary>メッセージID（AIRs連携バッチ）：取込管理情報重複</summary>
        public const string MSGID_AIRS_IMPORT_QUE_DUPLICATE = "QCB0000000105";
        /// <summary>メッセージID（AIRs連携バッチ）：追加ローデータ削除キュー重複</summary>
        public const string MSGID_AIRS_DELETE_QUE_DUPLICATE = "QCB0000000106";
        #endregion

        /// <summary>
        /// データ加工状態
        /// </summary>
        public static class ProcessStatus {
            /// <summary>
            /// 未実行
            /// </summary>
            public static readonly string Unexecuted = "0";
            /// <summary>
            /// 実行済
            /// </summary>
            public static readonly string BeenExecuted = "1";
            /// <summary>
            /// 要更新
            /// </summary>
            public static readonly string UpdateRequired = "2";
        }

        /// <summary>
        /// ログレベル
        /// </summary>
        [ComVisible(false)]
        public enum LogLevel {
            /// <summary>FATALログ</summary>
            FATAL,
            /// <summary>ERRORログ</summary>
            ERROR,
            /// <summary>WARNログ</summary>
            WARN,
            /// <summary>INFOログ</summary>
            INFO
        }

        /// <summary>
        /// レポートバッチ処理レベル
        /// </summary>
        [ComVisible(false)]
        public enum ReportProcLevel
        {
            /// <summary>チェックリスト</summary>
            CHK_LIST = 0,
            /// <summary>極小</summary>
            TINY = 1,
            /// <summary>小</summary>
            SMALL = 2,
            /// <summary>中</summary>
            MEDIUM = 3,
            /// <summary>大</summary>
            LARGE = 4,
            /// <summary>特大</summary>
            EXTRA_LARGE = 5,
            /// <summary>テンプレートチェック</summary>
            TEMPLATE_CHK = 10
        }

        /// <summary>
        /// レポートバッチログ区分
        /// </summary>
        public enum ReportLogKind {
            /// <summary>共通</summary>
            Common,
            /// <summary>極小</summary>
            ReportLv1,
            /// <summary>小</summary>
            ReportLv2,
            /// <summary>中</summary>
            ReportLv3,
            /// <summary>大</summary>
            ReportLv4,
            /// <summary>特大</summary>
            ReportLv5,
            /// <summary>?</summary>
            ReportLv6,
            /// <summary>フォーマットチェック</summary>
            FmtChk,
            /// <summary>チェックリスト</summary>
            ChkList
        }

        /// <summary>
        /// データ加工処理種別
        /// </summary>
        /// <remarks>8.5.加工メニューマスタ(T_Edit_Menu_Master)の加工メニューID(Edit_Menu_Master_ID)とenumの値は対応しているので注意してください。</remarks>
        [ComVisible(false)]
        public enum PROCESS_TYPE {
            /// <summary>INTEGRATE</summary>
            INTEGRATE = 1,
            /// <summary>RECODE</summary>
            RECODE = 2,
            /// <summary>MCONVERT</summary>
            MCONVERT = 3,
            /// <summary>CLASS</summary>
            CLASS = 4,
            /// <summary>COUNT</summary>
            COUNT = 5,
            /// <summary>MTOS</summary>
            MTOS = 6,
            /// <summary>GROUP</summary>
            GROUP = 7,
            /// <summary>COMPUTE</summary>
            COMPUTE = 8,
            /// <summary>DATAMODIFY</summary>
            DATAMODIFY = 9,
            /// <summary>SAMPLEDELETE</summary>
            SAMPLEDELETE = 10
        }

        /// <summary>
        /// データ加工MTOS処理方法
        /// </summary>
        [ComVisible(false)]
        public enum MtoS_SelectMethod
        {
            /// <summary>前優先</summary>
            BEFORE = 1,
            /// <summary>後優先</summary>
            AFTER,
            /// <summary>ランダム</summary>
            RANDOM
        }

        /// <summary>
        /// 一時作成データ加工
        /// </summary>
        [ComVisible(false)]
        public enum TemporaryDataProcess {
            /// <summary>なし</summary>
            None,
            /// <summary>GT集計設定</summary>
            GTSetting,
            /// <summary>カテゴリ出力設定</summary>
            CategoryEdit
        }

        /// <summary>
        /// RAWDATAファイル拡張子タイプ
        /// </summary>
        [ComVisible(false)]
        public enum fileExtension
        {
            /// <summary>ファイル拡張子.txt　一般データファイル</summary>
            txt,
            /// <summary>ファイル拡張子 .dp　データ加工</summary>
            dp,
            /// <summary>ファイル拡張子 .tmp 一時</summary>
            tmp,
            /// <summary>ファイル拡張子 .dp2 カテゴリ出力編集</summary>
            dp2

        }

        /// <summary>
        /// COMPUTE式評価用Exeファイル名
        /// </summary>
        [ComVisible(false)]
        public const string computeExpressionExe = "ComputeExpression.exe";

        /// <summary>
        /// ウエイトバック設定 非該当・無回答
        /// </summary>
        [ComVisible(false)]
        public enum WeightbackNo
        {
            /// <summary>
            /// -1 非該当 *
            /// </summary>
            Iv = -1,
            /// <summary>
            ///  -2 無回答 
            /// </summary>
            Na = -2
        }

        /// <summary>
        /// 出力種別を記号を表す静的クラス
        /// </summary>
        /// <remarks>MANTIS#0002328</remarks>
        public static class OutputTypeSign
        {
            /// <summary>
            /// WB有無を表す開始括り
            /// </summary>
            public static readonly string OpeningWb= "(";

            /// <summary>
            /// WB有無を表す終了括り
            /// </summary>
            public static readonly string ClosingWb = ")";

            /// <summary>
            /// ファイル拡張子を表す開始括り
            /// </summary>
            public static readonly string OpeningFileExtensions = "("; // MANTIS#0002328(0008701)

            /// <summary>
            /// ファイル拡張子を表す終了括り
            /// </summary>
            public static readonly string ClosingFileExtensions = ")"; // MANTIS#0002328(0008701)


            /// <summary>
            /// ファイル拡張子が複数存在する場合の区切りを表す
            /// </summary>
            public static readonly string SeparatorFileExtensions = "-";

            /// <summary>
            /// ピリオドを表す
            /// </summary>
            public static readonly string Period = ".";

            /// <summary>
            /// アンダースコアを表す
            /// </summary>
            public static readonly string Underscore = "_";

            /// <summary>
            /// Weightbackを表す
            /// </summary>
            public static readonly string Wb = "WB";

            /// <summary>
            /// 拡張子を表す列挙子
            /// </summary>
            public enum FileExtensions 
            { 
                /// <summary>
                /// Excel拡張子を表す
                /// </summary>
                xls,

                /// <summary>
                /// Power Point拡張子を表す
                /// </summary>
                ppt,
 
                /// <summary>
                /// PDF拡張子を表す
                /// </summary>
                pdf,

                /// <summary>
                /// Quick-CROSS 3拡張子を表す
                /// </summary>
                qc3
            }

        }

        #region アイテム予約語
        /// <summary>アイテム予約語：sampleid</summary>
        public const string ITEM_WORD_SAMPLEID = "sampleid";
        /// <summary>アイテム予約語：answerdate</summary>
        public const string ITEM_WORD_ANSWERDATE = "answerdate";
        /// <summary>アイテム予約語：weightback</summary>
        public const string ITEM_WORD_WEIGHTBACK = "weightback";
        #endregion

    }
}
