#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：GlobalsConstant.cs
 * バージョン：1.0.0
 * 概　　　要：Const定義インターフェース
 * 作　成　日：2012/03/22
 * 作　成　者：寺嶋　千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using Macromill.QCWeb.Common;

namespace Macromill.QCWeb.Logic {
    /// <summary>
    /// Const定義
    /// </summary>
    public abstract class GlobalsConstant : GlobalsCommonConstant {
        /// <summary>
        /// セッションからオブジェクトを取り出すためのキー
        /// </summary>
        public const string BROWSERINFO_SESSION_KEY = "QCWEB_SESSION_QCWEBBROWSERINFO";

        /// <summary>
        /// セッションからオブジェクトを取り出すためのキー
        ///
        /// アイテムビューとカラーパレット間の受け渡しデータ
        /// </summary>
        public const string COLOR_PALETTE_SESSION_KEY = "COLOR_PALETTE";

        /// <summary>
        /// セッションからオブジェクトを取り出すためのキー
        ///
        /// アイテムビューとカラーパレット間の受け渡しデータ
        /// </summary>
        public const string COLOR_PALETTE_HEADER_SESSION_KEY = "COLOR_PALETTE_HEADER";

        /// <summary>
        /// セッションからオブジェクトを取り出すためのキー
        ///
        /// データ加工のエラー情報
        /// </summary>
        public const string SESSION_KEY_ERROR_INFO = "SESSION_KEY_ERROR_INFO";

        /// <summary>
        /// セッションからオブジェクトを取り出すためのキー:シナリオ管理の絞り込み状態を表す
        /// </summary>
        public const string SESSION_KEY_FILTER_TYPE = "FILTER_TYPE";

        /// <summary>
        /// セッションキー
        /// </summary>
        public enum SESSION_KEY
        {
            /// <summary>
            /// シナリオ編集GTと検定対象設定画面でやりとりするための集計対象のデータを格納、取り出すためのキー
            /// </summary>
            ScenarioGTQItemPanelData,

            /// <summary>
            /// Mantis 2526 対応で追加
            /// シナリオ編集クロスと折れ線設定画面でやりとるするためのキー
            /// CrossSetを格納する
            /// </summary>
            CrossSetList,

            /// <summary>
            /// Mantis 2473 対応で追加
            /// 新アイテムのアイテムIDを格納するためのキー
            /// シナリオ編集GT、クロス、FAで使用している
            /// </summary>
            CategoryEditItemInfoId
        }

        /// <summary>
        /// <para>画面IDを格納するGETパラメータのキー</para>
        /// <para>玄関ページからシナリオ管理への遷移時に利用(Default.aspx→ScenarioAdmin.aspx)</para>
        /// </summary>
        public const string QUERYSTRING_GUID = "GUID";

        /// <summary>
        /// <para>画面IDを格納するGETパラメータのキー</para>
        /// <para>シナリオ管理から各画面への遷移時に利用(ScenarioAdmin.aspx→XXXXXXXX.aspx)</para>
        /// </summary>
        public const string QUERYSTRING_WID = "WID";

        /// <summary>
        /// シナリオ管理画面にて変更があった場合、
        /// シナリオ管理から各画面への遷移時に利用する更新フラグ.(ScenarioAdmin.aspx→XXXXXXXX.aspx)
        /// </summary>
        public const string QUERYSTRING_SCENARIO_UPDATEFLAG = "UpdateFlag";

        /// <summary>
        /// シナリオIDを格納するGETパラメータのキー
        /// </summary>
        public const string QUERYSTRING_SID = "SID";

        /// <summary>
        /// Mantis 2473 対応で追加
        /// 対象シナリオアイテムIDを格納するGETパラメーターのキー
        /// </summary>
        public const string QUERYSTRING_SITEMID = "SItemID";

        /// <summary>
        /// Mantis 2473 対応で追加
        /// シナリオタイプを格納するGETパラメーターのキー
        /// </summary>
        public const string QUERYSTRING_STYPE = "ScenarioType";

        /// <summary>
        /// イメージのURL(IE6)
        /// </summary>
        public const string QCWEB_IMAGE_URL_IE6 = "~/Contents/Images/ie6/";

        /// <summary>
        /// イメージのURL(IE6以外)
        /// </summary>
        public const string QCWEB_IMAGE_URL_DEFAULT = "~/Contents/Images/";

        /// <summary>
        /// 
        /// </summary>
        public const string QCWEB_IMAGE_URL_EXTENSION = "-ie6.png";

        /// <summary>
        /// 
        /// </summary>
        public const string QCWEB_IMAGE_URL_PATH = "/ie6";

        /// <summary>
        /// 楽観排他失敗時のメッセージ、DefaultResources リソースキー
        /// </summary>
        public const string DEFAULT_RESOURCES_KEY_EXCLUSIVE_FAILS = "ReDisplayInfo";

        /// <summary>
        /// シナリオ管理のTreeViewに利用する
        /// </summary>
        public const string TAGID_NAMEBASE = "TreeViewItem";

        #region 画面ID
        /// <summary>
        /// 画面ID
        /// </summary>
        public enum PageId {
            /// <summary>
            /// 調査リスト
            /// </summary>
            QCS010101,
            /// <summary>
            /// シナリオ管理－メイン
            /// </summary>
            QCS020101,
            /// <summary>
            /// シナリオ作成
            /// </summary>
            QCS030101,
            /// <summary>
            /// シナリオ編集－GT設定
            /// </summary>
            QCS040101,
            /// <summary>
            /// シナリオ編集－GT設定－GT集計設定追加
            /// </summary>
            QCS040102,
            /// <summary>
            /// シナリオ編集－GT設定－検定対象設定
            /// </summary>
            QCS040103,
            /// <summary>
            /// シナリオ編集－クロス設定
            /// </summary>
            QCS040201,
            /// <summary>
            /// シナリオ編集－クロス設定－カテゴリ編集
            /// </summary>
            QCS040202,

            /// <summary>
            /// Mantis 2526 対応で追加
            /// シナリオ編集－クロス設定－折れ線設定
            /// </summary>
            QCS040203,

            /// <summary>
            /// シナリオ編集－FA設定
            /// </summary>
            QCS040301,
            /// <summary>
            /// シナリオ編集－共通－ウエイトバック設定
            /// </summary>
            QCS040401,
            /// <summary>
            /// アイテムビュー－GT
            /// </summary>
            QCS050101,
            /// <summary>
            /// アイテムビュー－クロス
            /// </summary>              
            QCS050102,
            /// <summary>
            /// アイテムビュー－FAリスト
            /// </summary>
            QCS050103,
            /// <summary>
            /// 出力設定－GT
            /// </summary>
            QCS050201,
            /// <summary>
            /// 出力設定－クロス
            /// </summary>
            QCS050202,
            /// <summary>
            /// 出力設定－FA
            /// </summary>
            QCS050203,
            /// <summary>
            /// 出力設定－レポート
            /// </summary>
            QCS050204,
            /// <summary>
            /// カラーパレットA
            /// </summary>
            QCS050301,
            /// <summary>
            /// カラーパレットB
            /// </summary>
            QCS050302,
            /// <summary>
            /// カラーパレットC
            /// </summary>
            QCS050303,
            /// <summary>
            /// カラーパレットD
            /// </summary>
            QCS050304,
            /// <summary>
            /// 折れ線の設定
            /// </summary>
            QCS050305,
            /// <summary>
            /// 設問設定
            /// </summary>
            QCS060101,
            /// <summary>
            /// データ加工－メイン
            /// </summary>
            QCS070101,
            /// <summary>
            /// RECODE
            /// </summary>
            QCS070201,
            /// <summary>
            /// RECODE－入力補助
            /// </summary>
            QCS070202,
            /// <summary>
            /// INTEGRATE
            /// </summary>
            QCS070301,
            /// <summary>
            /// INTEGRATE－入力補助
            /// </summary>
            QCS070302,
            /// <summary>
            /// CLASS
            /// </summary>
            QCS070401,
            /// <summary>
            /// CLASS－カテゴリ名転記
            /// </summary>
            QCS070402,
            /// <summary>
            /// MCONVERT
            /// </summary>
            QCS070501,
            /// <summary>
            /// MCONVERT－入力補助
            /// </summary>
            QCS070502,
            /// <summary>
            /// COUNT
            /// </summary>
            QCS070601,
            /// <summary>
            /// MTOS
            /// </summary>
            QCS070701,
            /// <summary>
            /// GROUP
            /// </summary>
            QCS070801,
            /// <summary>
            /// COMPUTE
            /// </summary>
            QCS070901,
            /// <summary>
            /// サンプル削除
            /// </summary>
            QCS071001,
            /// <summary>
            /// データ修正
            /// </summary>
            QCS071101,
            /// <summary>
            /// 実行状況-タイプA
            /// </summary>
            QCS071201,
            /// <summary>
            /// 実行状況-タイプB
            /// </summary>
            QCS071202,
            /// <summary>
            /// 実行状況-タイプC
            /// </summary>
            QCS071203,
            /// <summary>
            /// 流用アイテム一覧
            /// </summary>
            QCS070001,
            /// <summary>
            /// データ参照
            /// </summary>
            QCS080101,
            /// <summary>
            /// データ参照－フィルタ設定
            /// </summary>
            QCS080102,
            /// <summary>
            /// データ出力
            /// </summary>
            QCS090101,
            /// <summary>
            /// 表示設定
            /// </summary>
            QCS100101,
            /// <summary>
            /// グラフの設定
            /// </summary>
            QCS100102,
            /// <summary>
            /// PPテンプレート設定
            /// </summary>
            QCS100103,
            /// <summary>
            /// 環境設定－集計の設定
            /// </summary>
            QCS110101,
            /// <summary>
            /// 環境設定－初期化
            /// </summary>
            QCS110102,
            /// <summary>
            /// ダウンロードリスト
            /// </summary>
            QCS120101,
            /// <summary>
            /// ヘルプ
            /// </summary>
            QCS130101,
            /// <summary>
            /// カラーパレット（色設定）
            /// </summary>
            QCS000001,
            /// <summary>
            /// 汎用
            /// </summary>
            QCS000000
        }

        #endregion

        #region 固有ID

        /// <summary>
        /// アイテムビューとカラーパレット間の受け渡しID
        /// </summary>
        public const string UNIQUEID_COLOR_PALETTE = "COLOR_PALETTE_ID";

        #endregion

        /// <summary>
        /// レポート.子状態
        /// </summary>
        public enum CHILD_DIV {
            /// <summary>0：子レポート無し</summary>
            NODATA = 0,
            /// <summary>1：子レポートあり</summary>
            DATA = 1
        }

        /// <summary>
        /// フラグを表すコード
        /// </summary>
        public enum FLAG {
            /// <summary>フラグ：False</summary>
            FALSE = 0,
            /// <summary>フラグ：True</summary>
            TRUE = 1
        }

        #region シナリオ区分
        /// <summary>
        /// シナリオ区分のDB値：G
        /// </summary>
        public const string SCENARIO_TYPE_GT = "G";
        /// <summary>
        /// シナリオ区分のDB値：C
        /// </summary>
        public const string SCENARIO_TYPE_CROSS = "C";
        /// <summary>
        /// シナリオ区分のDB値：F
        /// </summary>
        public const string SCENARIO_TYPE_FA = "F";
        #endregion

        /// <summary>
        /// シナリオ名の上限数（文字数）
        /// </summary>
        public const int SCENARIO_NAME_MAX_LENGTH = 50;

        #region アイテムビュー区分

        /// <summary>
        /// アイテムビューGT
        /// </summary>
        public const string ITEMVIEW_TYPE_GT = "GTItemView";

        /// <summary>
        /// アイテムビュークロス
        /// </summary>
        public const string ITEMVIEW_TYPE_CROSS = "CrossItemView";

        /// <summary>
        /// アイテムビューFA
        /// </summary>
        public const string ITEMVIEW_TYPE_FA = "FAItemView";

        #endregion

        /// <summary>
        /// Originalデータ区分
        /// </summary>
        public enum SOURCE_DIV{
            /// <summary>0：Original</summary>
            ORIGINAL = 0,
            /// <summary>1:加工データ</summary>
            PROCESS_DATA = 1,
            /// <summary>2:シナリオ加工データ</summary>
            SCENARIO_PROCESS_DATA = 2
        }

        /// <summary>
        /// アイテム情報ステータス
        /// </summary>
        public enum ITEM_INFO_STATUS{
            /// <summary>0：無効</summary>
            INVALID = 0,
            /// <summary>1:有効</summary>
            VALID = 1,
            /// <summary>2:仮登録</summary>
            TEMPREGIST = 2
        }

        /// <summary>
        /// 汎用コードマスタのエクセルタイプのグループID
        /// </summary>
        public const int CODE_MASTER_GROUP_ID_EXCEL_TYPE = 20;

        /// <summary>
        /// 汎用コードマスタの用紙サイズのグループID
        /// </summary>
        public const int CODE_MASTER_GROUP_ID_PAPER_SIZE = 21;
        // add kom2 2012/03/25

        /// <summary>
        /// 表種別
        /// </summary>
        public enum REPORT_TYPE{
            /// <summary>表種別：N%表</summary>
            N_PER = 1,
            /// <summary>表種別：N表</summary>
            N = 2,
            /// <summary>表種別：%表</summary>
            PER = 4
        }


        /// <summary>
        /// 汎用コードマスタの用紙の向きのグループID
        /// </summary>
        public const int CODE_MASTER_GROUP_ID_PAPER_ORIENTATION = 22;
        /// <summary>
        /// 1シートに1クロス
        /// </summary>
        public const int CROSS_REPORT_COUNT_ONE_SHEET = 1;
        /// <summary>
        /// 1シートに複数クロス
        /// </summary>
        public const int CROSS_REPORT_COUNT_ANY_SHEET = 2;

        /// <summary>
        /// 回答タイプ文字列 SA
        /// </summary>
        public const string ANSWER_TYPE_STRING_SA = "SA";
        /// <summary>
        /// 回答タイプ文字列 MA
        /// </summary>
        public const string ANSWER_TYPE_STRING_MA = "MA";
        /// <summary>
        /// 回答タイプ文字列 FA
        /// </summary>
        public const string ANSWER_TYPE_STRING_FA = "FA";
        /// <summary>
        /// 回答タイプ文字列 N
        /// </summary>
        public const string ANSWER_TYPE_STRING_N = "N";
        /// <summary>
        /// 回答タイプ文字列 D
        /// </summary>
        public const string ANSWER_TYPE_STRING_D = "D";

        #region マトリクス区分
        /// <summary>
        /// マトリクス区分：通常アイテム
        /// </summary>
        public const int MATRIX_DIV_NORMAL_ITEM = 0;
        /// <summary>
        /// マトリクス区分：マトリクスアイテム（ヘッダー）
        /// </summary>
        public const int MATRIX_DIV_MATRIX_ITEM_HEADER = 1;
        /// <summary>
        /// マトリクス区分：マトリクス子アイテム（親作成元）
        /// </summary>
        public const int MATRIX_DIV_MATRIX_FIRST_CHILD = 4;
        /// <summary>
        /// マトリクス区分：マトリクス子アイテム（通常子アイテム）
        /// </summary>
        public const int MATRIX_DIV_MATRIX_CHILD_ITEM = 2;
        /// <summary>
        /// マトリクス区分：子アイテム（付加FAアイテム）
        /// </summary>
        public const int MATRIX_DIV_ADD_FA_ITEM = 3;
        #endregion

        #region 集計種類
        /// <summary>
        /// 集計種類　1:シングル回答用
        /// </summary>
        public const int TOTALIZATION_TYPE_SA = 1;
        /// <summary>
        /// 集計種類　2:マルチ回答用
        /// </summary>
        public const int TOTALIZATION_TYPE_MA = 2;
        /// <summary>
        /// 集計種類　3:数値回答用
        /// </summary>
        public const int TOTALIZATION_TYPE_N = 3;
        /// <summary>
        /// 集計種類　4:マトリックスのマルチ回答用	GT-MTM
        /// </summary>
        public const int TOTALIZATION_TYPE_GT_MTM = 4;
        /// <summary>
        /// 集計種類　5:マトリックスのシングル回答用	GT-MTS
        /// </summary>
        public const int TOTALIZATION_TYPE_GT_MTS = 5;
        /// <summary>
        /// 集計種類　6:複数の数値回答を1つにする表	GT-MTN
        /// </summary>
        public const int TOTALIZATION_TYPE_GT_MTN = 6;
        /// <summary>
        /// 集計種類　7:割合回答用			GT-RAT
        /// </summary>
        public const int TOTALIZATION_TYPE_GT_RAT = 7;
        /// <summary>
        /// 集計種類　8:順位回答用			GT-RNK
        /// </summary>
        public const int TOTALIZATION_TYPE_GT_RNK = 8;
        #endregion

        /// <summary>
        /// 付加アイテムの並べ替え：並べ替えなし
        /// </summary>
        public const int NOSORT_ADD_ITEM = 0;
        /// <summary>
        /// 付加アイテムの並べ替え：昇順で並べ替え
        /// </summary>
        public const int SORT_ADD_ITEM = 1;

        /// <summary>
        /// ページ設定：表種別：デフォルトは0
        /// </summary>
        public const int PAGE_PAPER_TABLE_TYPE_DEFAULT = 0;
        /// <summary>
        /// ページ設定：用紙サイズ A3
        /// </summary>
        public const int PAGE_PAPER_SIZE_A3 = 8;
        /// <summary>
        /// ページ設定：用紙サイズ A4
        /// </summary>
        public const int PAGE_PAPER_SIZE_A4 = 9;
        /// <summary>
        /// ページ設定：用紙サイズ B4
        /// </summary>
        public const int PAGE_PAPER_SIZE_B4 = 12;

        /// <summary>
        /// ページ設定：用紙向き 縦
        /// </summary>
        public const int PAGE_ORIENTATION_VERTICAL = 1;
        /// <summary>
        /// ページ設定：用紙向き 横
        /// </summary>
        public const int PAGE_ORIENTATION_HORIZONTAL = 2;

        /// <summary>
        /// グラフ出力の有無：出力しない
        /// </summary>
        public const int OUTPUT_GRAPH_OFF = 0;
        /// <summary>
        /// グラフ出力の有無：出力する
        /// </summary>
        public const int OUTPUT_GRAPH_ON = 1;

        /// <summary>
        /// コメント出力の有無：出力しない
        /// </summary>
        public const int OUTPUT_COMMENT_OFF = 0;
        /// <summary>
        /// コメント出力の有無：出力する
        /// </summary>
        public const int OUTPUT_COMMENT_ON = 1;

        /// <summary>
        /// 調査票の出力：出力しない
        /// </summary>
        public const int OUTPUT_SURVEY_REPORT_OFF = 0;
        /// <summary>
        /// 調査票の出力：出力する
        /// </summary>
        public const int OUTPUT_SURVEY_REPORT_ON = 1;

        /// <summary>
        /// Excelファイル形式：Excel97-2003形式
        /// </summary>
        public const int EXCEL_FILE_TYPE_2003 = 56;
        /// <summary>
        /// Excelファイル形式：Excel2007形式
        /// </summary>
        public const int EXCEL_FILE_TYPE_2007 = 51;

        /// <summary>
        /// PowerPointファイル形式：PowerPoint97-2003形式
        /// </summary>
        public const int POWERPOINT_FILE_TYPE_2003 = 1;
        /// <summary>
        /// PowerPointファイル形式：PowerPoint2007形式
        /// </summary>
        public const int POWERPOINT_FILE_TYPE_2007 = 24;

        /// <summary>
        /// ファイル形式：Excel
        /// </summary>
        public const int OUTPUT_FILE_TYPE_EXCEL = 1;
        /// <summary>
        /// ファイル形式：PowerPoint
        /// </summary>
        public const int OUTPUT_FILE_TYPE_POWERPOINT = 2;
        /// <summary>
        /// ファイル形式：PDF
        /// </summary>
        public const int OUTPUT_FILE_TYPE_PDF = 4;

        /// <summary>
        /// PowerPointデフォルトファイル(default.ppt)の内部コード
        /// </summary>
        public const string POWERPOINT_DEFALUT_FILE_CODE = "00000000000";

        #region データタイプ
        /// <summary>
        /// 無回答
        /// </summary>
        public const string DATA_TYPE_DK = "DK";

        /// <summary>
        /// 非該当
        /// </summary>
        public const string DATA_TYPE_UNMATCH = "*";
        #endregion

        /// <summary>
        /// コロン符号
        /// </summary>
        public const string SIGN_COLON = ":";

        /// <summary> アイテム付加文字「M」 </summary>
        public const string GTSETTING_ITEM_NAME_PREFIX = "M";

        /// <summary> アイテム付加文字「N」 </summary>
        public const string ITEM_NAME_PREFIX_N = "N";

        /// <summary> データ加工条件値範囲フォーマット </summary>
        public static readonly string categoryRngFormat = "={0}-{1}";

        #region データ出力

        /// <summary>
        /// データ出力 定数値
        /// </summary>
        public static class OutputConstant
        {
            /// <summary>
            /// csvファイル拡張子
            /// </summary>
            public static readonly string csvFileExtension = ".csv";

            /// <summary>
            /// txtファイル拡張子
            /// </summary>
            public static readonly string txtFileExtension = ".txt";

            /// <summary>
            /// zipファイル拡張子
            /// </summary>
            public static readonly string zipFileExtension = ".zip";

            /// <summary>
            /// tmpファイル拡張子
            /// </summary>
            public static readonly string tmpFileExtension = ".tmp";

            /// <summary>
            /// レイアウト
            /// </summary>
            public static readonly string layout = "Layout";

            /// <summary>
            /// 数値フォーマット
            /// </summary>
            public static readonly string numberFormat = "000";

            /// <summary>
            /// Excel最大値
            /// </summary>
            public enum ExcelMax
            {
                /// <summary>
                /// Excel最大列数
                /// </summary>
                Column = 256,
                /// <summary>
                /// Excel最大行数
                /// </summary>
                Row = 65000
            }

            #region enum

            /// <summary>
            /// Macromill.QCWeb.Tabulation.RawDataTabulation.GetLayoutArrayメソッドのインデックス構成
            /// </summary>
            public enum LayoutIndex
            {
                /// <summary>
                /// 横レイアウトのアイテム名 列No
                /// </summary>
                ItemNameLandscape = 0,
                /// <summary>
                /// 縦レイアウトのアイテム名 列No
                /// </summary>
                ItemNamePortrait = 2,
                /// <summary>
                /// 横レイアウトの回答タイプ 列No
                /// </summary>
                AnswerTypeLandscape = 1,
                /// <summary>
                /// 縦レイアウトの回答タイプ 列No
                /// </summary>
                AnswerTypePortrait = 4,
                /// <summary>
                /// 横レイアウトのカテゴリ数 列No
                /// </summary>
                CategoryCountLandscape = 2,
                /// <summary>
                /// 縦レイアウトのカテゴリ数 列No
                /// </summary>
                CategoryCountPortrait = 5
            }

            #endregion
        }

        #endregion

        #region FA設定
        /// <summary>
        /// シナリオ表示名の最大長さ
        /// </summary>
        public const int FA_SCENARIO_VIEW_NAME_MAX_LENGTH = 50;
        #endregion

        /// <summary>アイテム名最大サイズ</summary>
        public const int ITEM_NAME_MAX_LENGTH = 25;
        /// <summary>
        /// 表題の最大サイズ
        /// </summary>
        public const int ITEM_LV1TITLE_MAX_LENGTH = 1000;  //表題の最大サイズ
        /// <summary>
        /// 質問最大サイズ
        /// </summary>
        public const int ITEM_LV2TITLE_MAX_LENGTH = 1000;  //質問文最大サイズ

        /// <summary>
        /// コメント最大サイズ
        /// </summary>
        public const int ITEMVIEW_COMMENT_MAX_LENGTH = 10000;  //アイテムビューコメント最大サイズ

        /// <summary>
        /// Mantis2307対応
        /// 帯グラフを表示できる選択肢数
        /// GT、かつ、表示できる選択肢数を超過している場合、帯グラフは表示しない
        /// </summary>
        public const int GT_ITEMVIEW_BELT_CATEGORY_MAX_COUNT = 256;

        /// <summary>
        /// 表頭に設定できる最大選択肢数
        /// 横%の場合、集計対象、縦%の場合、集計軸が対象
        /// </summary>
        public const int CROSS_ITEMVIEW_MAX_TABLE_HEAD_CATEGORY_COUNT = 200;

        #region カテゴリ編集
        /// <summary>
        /// 加工後アイテム名前付
        /// </summary>
        public const string CAE_ITEM_NAME_PREFIX = "C";

        /// <summary>カテゴリ選択肢の最大長さ200文字</summary>
        public const int CATEGORY_NAME_MAX_LENGTH = 200;    //カテゴリ選択肢の最大長さ200文字

        /// <summary>アンダースコア</summary>
        public const string SIGN_UNDER_SCORE = "_";
        /// <summary>SAMPLEIDのアイテム名</summary>
        public const string SAMPLE_ID = "SAMPLEID";
        /// <summary>半角空白</summary>
        public const string SIGN_WHITE = " ";
        /// <summary>カンマ</summary>
        public const char SPLIT_COMMA_CHAR = ',';
        /// <summary>省略符号</summary>
        public const string ABBREIVE_CHAR = "…";
        #endregion

        #region グラフ設定
        /// <summary>
        /// グラフ種別コード
        /// </summary>
        public enum COLOR_INFO_TYPE_CODE {
            /// <summary>
            /// 横棒積上：1
            /// </summary>
            SIDEBAR_STACK = 1,
            /// <summary>
            /// 縦棒積上G：2
            /// </summary>
            VERTICAL_STACK = 2,
            /// <summary>
            /// 円：3
            /// </summary>
            CIRCLE = 3,
            /// <summary>
            /// 横棒：4
            /// </summary>
            SIDEBAR = 4,
            /// <summary>
            /// 縦棒：5
            /// </summary>
            VERTICAL = 5,
            /// <summary>
            /// グラフ背景：6
            /// </summary>
            BACKGROUND = 6,
            /// <summary>
            /// 折れ線：7
            /// </summary>
            LINE = 7
        }
        #endregion

        //public const int SUCCESS = 0;
        //public const int WARM = -1;
        //public const int ERROR = -2;
        //public const int FATAL = -3;

        /// <summary>
        /// ファイルセパレータ
        /// </summary>
        public static class FileSeparator {
            /// <summary>
            /// Windows
            /// </summary>
            public static char Windows = '\\';
            /// <summary>
            /// Linux
            /// </summary>
            public static char Linux = '/';
        }

        #region カラーパレット

        /// <summary>
        /// カラーコード
        /// </summary>
        public enum COLOR_CODE {
            /// <summary>黒</summary>
            Black = 1,
            /// <summary>白</summary>
            White,
            /// <summary>赤</summary>
            Red,
            /// <summary>明るい緑</summary>
            BrightGreen,
            /// <summary>青</summary>
            Blue1,
            /// <summary>黄</summary>
            Yellow1,
            /// <summary>ﾋﾟﾝｸ</summary>
            Pink1,
            /// <summary>水色</summary>
            LightBlue1,
            /// <summary>濃い赤</summary>
            DarkRed1,
            /// <summary>緑</summary>
            Green,
            /// <summary>濃い青</summary>
            DarkBlue1,
            /// <summary>濃い黄</summary>
            DarkYellow,
            /// <summary>紫</summary>
            Purple1,
            /// <summary>青緑</summary>
            BluishGreen1,
            /// <summary>25%灰色</summary>
            Gray25,
            /// <summary>50%灰色</summary>
            Gray50,
            /// <summary>ｸﾞﾚｰ</summary>
            Gray,
            /// <summary>ﾌﾟﾗﾑ</summary>
            Plum1,
            /// <summary>ｱｲﾎﾞﾘｰ</summary>
            Ivory,
            /// <summary>薄い水色</summary>
            LightLightBlue1,
            /// <summary>濃い紫</summary>
            DarkPurple,
            /// <summary>ｺｰﾗﾙ</summary>
            Coral,
            /// <summary>ｵｰｼｬﾝﾌﾞﾙｰ</summary>
            OceanBlue,
            /// <summary>ｱｲｽﾌﾞﾙｰ</summary>
            IceBlue,
            /// <summary>濃い青</summary>
            DarkBlue2,
            /// <summary>ﾋﾟﾝｸ</summary>
            Pink2,
            /// <summary>黄</summary>
            Yellow2,
            /// <summary>水色</summary>
            LightBlue2,
            /// <summary>紫</summary>
            Purple2,
            /// <summary>濃い赤</summary>
            DarkRed2,
            /// <summary>青緑</summary>
            BluishGreen2,
            /// <summary>青</summary>
            Blue2,
            /// <summary>ｽｶｲﾌﾞﾙｰ</summary>
            SkyBlue,
            /// <summary>薄い水色</summary>
            LightLightBlue2,
            /// <summary>薄い緑</summary>
            LightGreen,
            /// <summary>薄い黄</summary>
            LightYellow,
            /// <summary>ﾍﾟｰﾙﾌﾞﾙｰ</summary>
            PeerBlue,
            /// <summary>ﾛｰｽﾞ</summary>
            Rose,
            /// <summary>ﾗﾍﾞﾝﾀﾞｰ</summary>
            Lavender,
            /// <summary>ﾍﾞｰｼﾞｭ</summary>
            Beige,
            /// <summary>薄い青</summary>
            LightBlue,
            /// <summary>ｱｸｱ</summary>
            Aqua,
            /// <summary>ﾗｲﾑ</summary>
            Lime,
            /// <summary>ｺﾞｰﾙﾄﾞ</summary>
            Gold,
            /// <summary>薄いｵﾚﾝｼﾞ</summary>
            LightOrange,
            /// <summary>ｵﾚﾝｼﾞ</summary>
            Orange,
            /// <summary>ﾌﾞﾙｰｸﾞﾚｰ</summary>
            BlueGray,
            /// <summary>40%灰色</summary>
            Gray40,
            /// <summary>濃い青緑</summary>
            DarkBluishGreen,
            /// <summary>ｼｰｸｸﾞﾘｰﾝ</summary>
            GreenToSeek,
            /// <summary>濃い緑</summary>
            DarkGreen,
            /// <summary>ｵﾘｰﾌﾞ</summary>
            Olive,
            /// <summary>茶</summary>
            Brown,
            /// <summary>ﾌﾟﾗﾑ</summary>
            Plum2,
            /// <summary>ｲﾝﾃﾞｨｺﾞ</summary>
            Indigo,
            /// <summary>80%灰色</summary>
            Gray80
        }

        /// <summary>
        /// 機能
        /// </summary>
        public static class Contents
        {
            /// <summary>
            /// カラーパレットA
            /// </summary>
            public const string ColorPaletteTypeA = "ColorPaletteTypeA.aspx";

            /// <summary>
            /// カラーパレットB
            /// </summary>
            public const string ColorPaletteTypeB = "ColorPaletteTypeB.aspx";

            /// <summary>
            /// カラーパレットC
            /// </summary>
            public const string ColorPaletteTypeC = "ColorPaletteTypeC.aspx";

            /// <summary>
            /// カラーパレットD
            /// </summary>
            public const string ColorPaletteTypeD = "ColorPaletteTypeD.aspx";
        }
        #endregion

        #region データ加工全般

        /// <summary>
        /// データ加工メインで選択されたアイテムIDをセッションに入れる為の文字列
        /// </summary>
        public const string DATA_PROCESS_SELECTED_ITEM_ID = "DATA_PROCESS_SELECTED_ITEM_ID";
        /// <summary>
        /// データ加工メインで修正時に「加工ID」をセッションに入れる為の文字列
        /// </summary>
        public const string DATA_PROCESS_EDIT_ID = "DATA_PROCESS_EDIT_ID";
        /// <summary>
        /// データ加工メインで修正時に処理区分をセッションに入れる為の文字列
        /// </summary>
        public const string DATA_PROCESS_PROCESS_KBN = "DATA_PROCESS_PROCESS_KBN";

        ///<summary> データ加工説明文最大長さ </summary> ///
        public const int DATA_PROCESS_DISCRIPTION_MAXLEN = 200;

        ///<summary> データ加工アイテムViewName最大長さ </summary> ///
        public const int DATA_PROCESS_VIEW_ITEMNAME_MAXLEN = 500;

        /// <summary>
        /// データ加工の処理区分
        /// 新規/編集/コピー
        /// </summary>
        /// <remarks>KANA:命名規則にのっとり、後で列挙値の変数名を変更します。newdata→New、modify→Modify、copy→Copy</remarks>
        public enum PROCESS_KBN {
            /// <summary>
            /// 新規
            /// </summary>
            newdata = 1,
            /// <summary>
            /// 編集
            /// </summary>
            modify,
            /// <summary>
            /// コピー
            /// </summary>
            copy
        }

        /// <summary>
        /// データ加工GROUPの算出値
        /// MAX/MIN/AVG/SUM
        /// <note>8.4.汎用コードマスターのGROUP_KEY'GroupCalcType'で定義されている値</note>
        /// </summary>
        public static class GroupCalcType
        {
            /// <summary>MAX:001</summary>
            public const string MAX = "001";
            /// <summary>MIN:002</summary>
            public const string MIN = "002";
            /// <summary>AVG:003</summary>
            public const string AVG = "003";
            /// <summary>SUM:004</summary>
            public const string SUM = "004";
        }

        /// <summary>
        /// データ加工COMPUTEの式の最大長さ
        /// </summary>
        public const int DATA_PROCESS_FORMULA_MAXLEN = 2000;

        /// <summary>
        /// RECODE補助入力スケールカテゴリ位置
        /// </summary>
        public enum  RECODE_SCALE_CATEGORY_POSITION{       
        /// <summary>スケールカテゴリ先頭</summary>
            HEAD = 1,
        /// <summary>スケールカテゴリ末尾</summary>
            TAIL = 2
    }
        /// <summary>
        /// データ加工の絞り込み条件連結区分：AND
        /// </summary>
        public const string CONDITION_DIV_AND = "1";
        /// <summary>
        /// データ加工の絞り込み条件連結区分：OR
        /// </summary>
        public const string CONDITION_DIV_OR = "2";

        /// <summary>
        /// データ加工の削除区分：条件指定
        /// </summary>
        public const int DELETE_SAMPLE_DELETE_TYPE_CONDITION = 1;
        /// <summary>
        /// データ加工の削除区分：サンプルID指定
        /// </summary>
        public const int DELETE_SAMPLE_DELETE_TYPE_SAMPLE_ID = 2;

/*
Macromill.QCWeb.DataProcess.EditMethod列挙値へ移動
        /// <summary>
        /// データ加工データ修正の修正方法：修正値を代入する
        /// </summary>
        public const int MODIFY_DATA_EDIT_METHOD_SUBSTITUTION = 1;
        /// <summary>
        /// データ加工データ修正の修正方法：修正値を追加する
        /// </summary>
        public const int MODIFY_DATA_EDIT_METHOD_APPEND = 2;
        /// <summary>
        /// データ加工データ修正の修正方法：修正値を除外する
        /// </summary>
        public const int MODIFY_DATA_EDIT_METHOD_REMOVE = 3;
*/
/*
Macromill.QCWeb.DataProcess.ModifyDataEdit列挙値へ移動
        /// <summary>
        /// データ加工データ修正の修正値タイプ：非該当
        /// </summary>
        public const int MODIFY_DATA_EDIT_VALUE_TYPE_UNMATCH = 1;
        /// <summary>
        /// データ加工データ修正の修正値タイプ：無回答
        /// </summary>
        public const int MODIFY_DATA_EDIT_VALUE_TYPE_DK = 2;
        /// <summary>
        /// データ加工データ修正の修正値タイプ：カテゴリ
        /// </summary>
        public const int MODIFY_DATA_EDIT_VALUE_TYPE_CATEGORY = 3;
        /// <summary>
        /// データ加工データ修正の修正値タイプ：アイテム
        /// </summary>
        public const int MODIFY_DATA_EDIT_VALUE_TYPE_ITEM = 4;
        /// <summary>
        /// データ加工データ修正の修正値タイプ：フリー入力
        /// </summary>
        public const int MODIFY_DATA_EDIT_VALUE_TYPE_FREE = 5;
*/

        /// <summary>
        /// データ加工データ修正のフリー入力文字列の最大長さ
        /// </summary>
        public const int DATA_PROCESS_EDIT_VALUE_FREE_MAXLEN = 1000;
        /// <summary>
        /// 回答タイプ【N】のデータ加工データ修正のフリー入力での整数部の最大長さ
        /// </summary>
        public const int DATA_PROCESS_EDIT_VALUE_FREE_TYPE_N_INTEGER_MAXLEN = 8;
        /// <summary>
        /// 回答タイプ【N】のデータ加工データ修正のフリー入力での小数部の最大長さ
        /// </summary>
        public const int DATA_PROCESS_EDIT_VALUE_FREE_TYPE_N_DECIMAL_MAXLEN = 2;
        /// <summary>
        /// 小数点
        /// </summary>
        public const char DECIMAL_POINT = '.';

        #endregion

        #region 条件値用
        /// <summary>条件値演算子・等号</summary>
        public const string CONDITION_SIGN_EQUAL = "=";

        /// <summary>条件値演算子・不等号</summary>
        public const string CONDITION_SIGN_NOT_EQUAL = "<>";

        /// <summary>条件値演算子・超過</summary>
        public const string CONDITION_SIGN_GREATER_THAN = ">";

        /// <summary>条件値演算子・以上</summary>
        public const string CONDITION_SIGN_GREATER_EQUAL = ">=";

        /// <summary>条件値演算子・未満</summary>
        public const string CONDITION_SIGN_LESS_THAN = "<";

        /// <summary>条件値演算子・以下</summary>
        public const string CONDITION_SIGN_LESS_EQUAL = "<=";

        /// <summary>条件値連結記号・ハイフン</summary>
        public const char CONDITION_SPLIT_CHAR_HYPHEN = '-';

        /// <summary>条件値連結記号・スラッシュ</summary>
        public const char CONDITION_SPLIT_CHAR_SLASH = '/';

        /// <summary>条件値連結記号・カンマ</summary>
        public const char CONDITION_SPLIT_CHAR_COMMA = ',';

        /// <summary>条件値記号・開き括弧</summary>
        public const char CONDITION_OPENING_PARENTHESIS = '(';

        /// <summary>条件値記号・閉じ括弧</summary>
        public const char CONDITION_CLOSING_PARENTHESIS = ')';
        #endregion

        /// <summary> ウェイトバック変数に関する定数を保持するクラス </summary>
        public static class WeightbackVariable {
            /// <summary>名称</summary>
            public static readonly string NAME = "Weightback";
            /// <summary>コード</summary>
            public static readonly decimal CODE = -1;
        }

        /// <summary> アイテムビューの表示上限数 </summary>
        public const int ITEMVIEW_MAX_CELL_COUNT = 40000;

        /// <summary>
        /// ItemViewデータの小数点表示フォーマット
        /// </summary>
        public static readonly string ITEMVIEW_DECIMAL_FORMATS = "F1"; // ##0.0 = F1

        /// <summary>
        /// ブラウザバージョン
        /// </summary>
        public static class BrowserVersion
        {
            /// <summary>IE6</summary>
            public const int BROWSER_VERSION_IE6 = 6;
            /// <summary>IE7</summary>
            public const int BROWSER_VERSION_IE7 = 7;
            /// <summary>IE8</summary>
            public const int BROWSER_VERSION_IE8 = 8;
            /// <summary>IE9</summary>
            public const int BROWSER_VERSION_IE9 = 9;
        }
        /// <summary>
        /// spreadのボタンのスタイル
        /// </summary>
        public static class SpreadButtonStyle
        {
            /// <summary>
            /// spreadのボタンに適用するCssClass
            /// </summary>
            public const string CssClass = "data-process-spread-button";
        }

        /// <summary>
        /// シナリオ管理より子画面を表示した際に、子画面に表示される×ボタンのイメージタグのHTML上のID
        /// </summary>
        public const string CloseButtonIconId = "BodyContent_SharedPanelCloseButton";

        /// <summary>
        /// 有意水準のBit値
        /// </summary>
        public enum TestSignificanceLvBit
        {
            /// <summary>
            /// 設定なし
            /// </summary>
            NoSetting = 0,
            /// <summary>
            /// 1%
            /// </summary>
            SignificanceLevel1 = 1,
            /// <summary>
            /// 5%
            /// </summary>
            SignificanceLevel5 = 2,
            /// <summary>
            /// 10%
            /// </summary>
            SignificanceLevel10 = 4,
            /// <summary>
            /// 1%, 5%, 10% すべてをあらわす
            /// クロスの全体との差の検定で利用する
            /// </summary>
            SignificanceLevelAll = 7
        }

        /// <summary>
        /// エラーページレベル
        /// </summary>
        public enum ErrorPageLevel {
            /// <summary>Fatal</summary>
            Fatal,
            /// <summary>Error</summary>
            Error,
            /// <summary>Warn</summary>
            Warn,
            /// <summary>Timeout</summary>
            Timeout,
            /// <summary>Exclusive(楽観排他)</summary>
            Exclusive
        }

        /// <summary>
        /// 回答タイプ=SA
        /// 表の向き=横
        /// の場合に使用可能なグラフ
        /// </summary>
        public static readonly string[] AllowedGraphTypesSA_Horizontal = new[]{
               GlobalsConstant.GRAPH_TYPE_QCHEIGHTSTICK,
               GlobalsConstant.GRAPH_TYPE_QCWIDTHBELT,
        };

        /// <summary>
        /// 回答タイプ=MA
        /// 表の向き=横
        /// の場合に使用可能なグラフ
        /// </summary>
        public static readonly string[] AllowedGraphTypesMA_Horizontal = new[]{
               GlobalsConstant.GRAPH_TYPE_QCHEIGHTSTICK
        };

        /// <summary>
        /// 回答タイプ=SA
        /// 表の向き=縦
        /// の場合に使用可能なグラフ
        /// </summary>
        public static readonly string[] AllowedGraphTypesSA_Vertical = new[]{
               GlobalsConstant.GRAPH_TYPE_QCHEIGHTBELT,
               GlobalsConstant.GRAPH_TYPE_QCWIDTHSTICK,
        };

        /// <summary>
        /// 回答タイプ=MA
        /// 表の向き=縦
        /// の場合に使用可能なグラフ
        /// </summary>
        public static readonly string[] AllowedGraphTypesMA_Vertical = new[]{
               GlobalsConstant.GRAPH_TYPE_QCWIDTHSTICK
        };

        /// <summary>
        /// Mantis 2527 対応で追加
        /// ashxファイルをjsonファイルとして返却するための設定
        /// </summary>
        public static string ContentTypeJson = "application/json; charset=utf-8";

        /// <summary>
        /// Mantis 2527 対応で追加
        /// 選択肢を作成するashxファイルに渡すパラメーター
        /// </summary>
        public static string AshxParamCategoryList = "CategoryList";

        /// <summary>
        /// Mantis 2473 対応で追加
        /// 選択肢を作成するashxファイルに渡すパラメーター
        /// </summary>
        public static string VersionNoKey = "VersionNoKey";

        /// <summary>
        /// Mantis 2473 対応で追加
        /// 選択肢を作成するashxファイルに渡すパラメーター
        /// </summary>
        public static string QcwebIdKey = "QcwebIdKey";

    }

}
