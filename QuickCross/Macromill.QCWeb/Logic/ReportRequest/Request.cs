#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：Request.cs
 * バージョン：1.0.0
 * 概　　　要： 
 * 作　成　日：2012/3/23
 * 作　成　者：井川はるき
 * 更　新　日：2012/4/8
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
/*
using Excel = Microsoft.Office.Interop.Excel;
*/
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Exceptions;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using System.Collections.Generic;

namespace Macromill.QCWeb.ReportRequest
{
    /// <summary>
    /// 出力命令側から扱う出力リクエストクラス
    /// </summary>
    [ComVisible(false), Guid("5C660C17-DDC6-41bd-8118-E0C91866854F"), Implementation]
    public class Request : IRequest
    {
        private decimal id = 0; // ID
        private decimal qcwebid = 0;    // 調査ID
        private string reqsvcd = null;    // リクエストサーバコード
        private string dlpath = null; // ダウンロードパス
        private string title = null;    // 調査タイトル
        // private Excel.XlFileFormat xlfmt = Excel.XlFileFormat.xlOpenXMLWorkbook; // Excelブック形式
        private Common.XlFileFormat xlfmt = Common.XlFileFormat.xlOpenXMLWorkbook; // Excelブック形式
        private ZeroNAIVShowCode zeroshowcd = (ZeroNAIVShowCode)0;  // 0件表示コード
        private NumericContentsCode showncd = NumericContentsCode.All;  // 数値回答表示項目コード
        private bool mergeaxis = true;  // 集計軸セル結合フラグ
        private int sumnumdigits = 2;   // 合計小数点以下桁数
        private int avgnumdigits = 2;   // 平均小数点以下桁数
        private int stdevnumdigits = 2;   // 標準偏差小数点以下桁数
        private int minnumdigits = 2;   // 最小値小数点以下桁数
        private int maxnumdigits = 2;   // 最大値小数点以下桁数
        private int mednumdigits = 2;   // 中央値小数点以下桁数
        private int wtnumdigits = 1;   // ウエイト値小数点以下桁数
        private int wtavgnumdigits = 1;   // 加重平均小数点以下桁数
        private string lccd = "ja_JP";     // ロケーションコード
        private string requestUserId = null;    // リクエストユーザID
        private Common.PpSaveAsFileType ppfmt = Common.PpSaveAsFileType.ppSaveAsOpenXMLPresentation;   // PowerPoint出力形式
        private string scenarioName = null;        // シナリオ名

        private Reportsets reportsets = null;   // レポートセットコレクションへの参照

        private OutputZIPType outputZipType = (OutputZIPType)0;     // 出力物ZIPファイルタイプ
        private string dlfilename = null;       // ダウンロードファイル名

        #region ファイルサイズ
        /// <summary>GT集計結果TSVファイルサイズ</summary>
        private long GTFileSize = 0L;
        /// <summary>クロス集計結果TSVファイルサイズ</summary>
        private long CrossFileSize = 0L;
        /// <summary>FA集計結果TSVファイルサイズ</summary>
        private long FAFileSize = 0L;
        /// <summary>データ出力集計結果TSVファイルサイズ</summary>
        private long DataOutputFileSize = 0L;
        #endregion

        #region ビヘイビア
        /// <summary>リクエストBhv</summary>
        protected TOutputRequestBhv tOutputRequestBhv = null;
        internal TOutputRequestBhv TOutputRequestBhv
        {
            get
            {
                return tOutputRequestBhv;
            }
        }
        /// <summary>出力物重度ポイントマスタBhv</summary>
        protected TOutputWpMasterBhv tOutputWpMasterBhv = null;
        internal TOutputWpMasterBhv TOutputWpMasterBhv
        {
            get
            {
                return tOutputWpMasterBhv;
            }
        }
        /// <summary>レポートセットBhv</summary>
        protected TOutputReportsetInfoBhv tOutputReportsetInfoBhv = null;
        internal TOutputReportsetInfoBhv TOutputReportsetInfoBhv
        {
            get
            {
                return tOutputReportsetInfoBhv;
            }
        }
        /// <summary>出力物共通Bhv</summary>
        protected TOutputCommonBhv tOutputCommonBhv = null;
        internal TOutputCommonBhv TOutputCommonBhv
        {
            get
            {
                return tOutputCommonBhv;
            }
        }
        /// <summary>出力物サブGTBhv</summary>
        protected TOutputSubGtBhv tOutputSubGtBhv = null;
        internal TOutputSubGtBhv TOutputSubGtBhv
        {
            get
            {
                return tOutputSubGtBhv;
            }
        }
        /// <summary>出力物サブクロスBhv</summary>
        protected TOutputSubCrossBhv tOutputSubCrossBhv = null;
        internal TOutputSubCrossBhv TOutputSubCrossBhv
        {
            get
            {
                return tOutputSubCrossBhv;
            }
        }
        /// <summary>出力物サブFABhv</summary>
        protected TOutputSubFaBhv tOutputSubFaBhv = null;
        internal TOutputSubFaBhv TOutputSubFaBhv
        {
            get
            {
                return tOutputSubFaBhv;
            }
        }
        /// <summary>出力物サブチェックリストBhv</summary>
        protected TOutputSubCklistBhv tOutputSubCklistBhv = null;
        internal TOutputSubCklistBhv TOutputSubCklistBhv
        {
            get
            {
                return tOutputSubCklistBhv;
            }
        }

        /// <summary>アプリケーション環境設定クラス</summary>
        protected ApplicationConfig appConfig = null;
        internal ApplicationConfig AppConfig
        {
            get
            {
                return appConfig;
            }
        }

        /// <summary>QCWeb調査管理詳細Bhv</summary>
        protected TQcwebSurveyDetailBhv tQcwebSurveyDetailBhv = null;
        internal TQcwebSurveyDetailBhv TQcwebSurveyDetailBhv
        {
            get { return tQcwebSurveyDetailBhv; }
        }
        #endregion

        public Request()
        {
            this.LocationCode = lccd;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="qcwebid">調査ID</param>
        public Request(decimal qcwebid)
        {
            this.qcwebid = qcwebid;
            reportsets = new Reportsets(this);
            QuillInjector.GetInstance().Inject(this);
        }


        public Request(long id, List<string> divNameList, int groupingSectorCount, string title, string companyName, ZeroNAIVShowCode zeroshowcd, bool mergeaxis
            , string reportPrefix, string xlbooknameprefix, TableType tabletype, TableOrientation tableorientation, TableType pagesetuptabletype
            , int minsamplescountonmarking, MarkingType markingtype, SignificanceTestLevel significancetestlevel, XlPaperSize papersize, XlPageOrientation paperorientation
            , TablesOnOneSheet tablesononesheet, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex
            , int level2lowcolorindex, int level1percent, int level2percent, ShowCode ShowNACode, ShowCode ShowIVCode
            , WBSettingCode WBOn, string FilteringExpression, bool PreWbBase, bool isChart)
        {
            appConfig = new ApplicationConfig();
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            reportsets = new Reportsets(this);
            foreach (string divName in divNameList)
            {
                Reportsets.Reportset reportset = reportsets.Add(
                    (decimal)id
                    // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                    , isChart ? (FileType.Excel | FileType.Report) : FileType.Excel
                    , null
                    , reportPrefix
                    , (Common.PpSaveAsFileType)0
                    , false
                    , ""                                            // 調査目的
                    , ""                     // 調査方法
                    , ""                       // 調査時期
                    , ""                     // 割付有効回答数
                    , companyName                                   // 調査実施機関
                );
                id++;

                for (int j = 0; j < groupingSectorCount; j++)
                {
                    Outputs.OutputCross cross = (Outputs.OutputCross)(reportset.Outputs as Outputs).Add(xlbooknameprefix
                        , isChart ? TableType.Per : tabletype
                        , tableorientation
                        , isChart ? TablesOnOneSheet.Single : tablesononesheet
                        , pagesetuptabletype
                        , papersize
                        , paperorientation
                        , significancetestlevel
                        , markingtype
                        , minsamplescountonmarking
                        , level2highcolorindex
                        , level1highcolorindex
                        , level1lowcolorindex
                        , level2lowcolorindex
                        , level1percent
                        , level2percent
                    );
                    cross.ShowNACode = ShowNACode;            // 無回答表示コード
                    cross.ShowIVCode = ShowIVCode;             // 非該当表示コード
                    cross.WBOn = false;          // WB設定コード
                    cross.WBOn = (WBOn & WBSettingCode.WBOn) == WBSettingCode.WBOn;
                    cross.ShowPreWBTotal = (WBOn & WBSettingCode.ShowPreWB) == WBSettingCode.ShowPreWB;
                    cross.FilteringExpression = FilteringExpression;    // 絞込み条件
                    cross.PreWbBase = PreWbBase;
                }
            }
        }

        public Request(long id, int groupingSectorCount, string title, string companyName, ZeroNAIVShowCode zeroshowcd, bool mergeaxis
        , string reportPrefix, string xlbooknameprefix, TableType tabletype, TableOrientation tableorientation, TableType pagesetuptabletype
        , int minsamplescountonmarking, MarkingType markingtype, SignificanceTestLevel significancetestlevel, XlPaperSize papersize, XlPageOrientation paperorientation
        , TablesOnOneSheet tablesononesheet, ShowCode ShowNACode, ShowCode ShowIVCode, WBSettingCode WBOn, string FilteringExpression, bool PreWbBase
        //, int level2highcolorindex, int level1highcolorindex, int level1lowcolorindex, int level2lowcolorindex, int level1percent, int level2percent
        )
        {
            appConfig = new ApplicationConfig();
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;

            this.showncd = NumericContentsCode.All;

            //this.ShowMedian = false;
            //this.ShowZeroNAIV = false;
            //this.ShowMaximum = true;

            reportsets = new Reportsets(this);
            Reportsets.Reportset reportset = reportsets.Add(
                (decimal)id
                // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , FileType.Excel
                , null
                , reportPrefix
                , (Common.PpSaveAsFileType)0
                , false
                , ""                                            // 調査目的
                , ""                     // 調査方法
                , ""                       // 調査時期
                , ""                     // 割付有効回答数
                , companyName                                   // 調査実施機関
            );

            for (int j = 0; j < groupingSectorCount; j++)
            {
                Outputs.OutputGT outputGT = (Outputs.OutputGT)(reportset.Outputs as Outputs).AddGT(xlbooknameprefix
                , tabletype
                , tableorientation
                , tablesononesheet
                , pagesetuptabletype
                , papersize
                , paperorientation
                , significancetestlevel
                , markingtype
                , minsamplescountonmarking
                //, level2highcolorindex
                //, level1highcolorindex
                //, level1lowcolorindex
                //, level2lowcolorindex
                //, level1percent
                //, level2percent
                );
                outputGT.ShowNACode = ShowNACode;       // 無回答表示コード
                outputGT.ShowIVCode = ShowIVCode;        // 非該当表示コード
                outputGT.FilteringExpression = FilteringExpression;  // 絞込み条件
                outputGT.WBOn = (WBOn & WBSettingCode.WBOn) == WBSettingCode.WBOn;
                outputGT.ShowPreWBTotal = (WBOn & WBSettingCode.ShowPreWB) == WBSettingCode.ShowPreWB;
                outputGT.PreWbBase = PreWbBase;
            }


        }

        public Request(long id, string title, string companyName, ZeroNAIVShowCode zeroshowcd, bool mergeaxis, string reportPrefix, string xlbooknameprefix, int totalCount)
        {
            appConfig = new ApplicationConfig();
            this.id = id;
            this.qcwebid = id;
            this.title = title;
            this.mergeaxis = mergeaxis;
            this.zeroshowcd = zeroshowcd;
            this.showncd = NumericContentsCode.All;

            reportsets = new Reportsets(this);
            Reportsets.Reportset reportset = reportsets.Add(
                (decimal)id
                // , (FileType)Enum.Parse(typeof(FileType), request.OutputFileTypeCode.ToString())
                , FileType.Excel
                , null
                , reportPrefix
                , (Common.PpSaveAsFileType)0
                , false
                , ""                                            // 調査目的
                , ""                     // 調査方法
                , ""                       // 調査時期
                , ""                     // 割付有効回答数
                , companyName                                   // 調査実施機関
            );

            Outputs.OutputCheckList outputCheckList = (Outputs.OutputCheckList)(reportset.Outputs as Outputs).AddCheckList(xlbooknameprefix, totalCount);
        }


        private void resetFileSizeTotal()
        {
            GTFileSize = 0L;
            CrossFileSize = 0L;
            FAFileSize = 0L;
            DataOutputFileSize = 0L;
        }

        internal void PushFileSize(long fileSize, OutputType outputtype)
        {
            switch (outputtype)
            {
                case OutputType.GT:
                case OutputType.Questionnaire:
                    GTFileSize += fileSize;
                    break;
                case OutputType.Cross:
                    CrossFileSize += fileSize;
                    break;
                case OutputType.FAList:
                    FAFileSize += fileSize;
                    break;
                case OutputType.QC3:
                    CrossFileSize += fileSize * 2L;
                    break;
                case OutputType.RawData:
                    DataOutputFileSize += fileSize;
                    break;
                case OutputType.CheckList:
                    break;
                default:
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.UnjustOutputTypeMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL
                                           , outputtype.ToString());
            }
        }

        /// <summary>
        /// リクエスト内容をDBに新規書き込みするメソッド
        /// </summary>
        public void Commit()
        {
            try
            {
                if (this.id != 0)
                    return;    // 書き込み済み
                if (reportsets.Count == 0 || reportsets[0].Outputs.Count == 0 || reportsets[0].Outputs[0].Tables.Count == 0)
                {
                    // エラースロー
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.NotExistWriteDataMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL);
                }

                TOutputRequest entity = new TOutputRequest();
                entity.RequestServerCode = reqsvcd; // リクエストサーバコード
                entity.RequestUserId = requestUserId;  // リクエストユーザID
                entity.Qcwebid = qcwebid;   // QCWeb管理ID
                entity.RequestDatetime = DateTime.Now;  // リクエスト日時
                entity.DownloadPath = dlpath;   // ダウンロードパス
                entity.StatusCode = 0;  // ステータスコード
                entity.ExcelbookType = (int)xlfmt;  // Excelブック形式
                //entity.NoanswerVisibleCode = (int)shownacd; // 無回答表示コード
                //entity.UnmacthVisibleCode = (int)showivcd;  // 非該当表示コード
                entity.NumericAnswerViewCode = (int)showncd;    // 数値回答表示項目コード
                //entity.WbSettingCode = (int)wbcd;   // WB集計表示コード
                entity.DpTotal = sumnumdigits;  // 合計小数点以下表示桁数
                entity.DpAverage = avgnumdigits;    // 平均小数点以下桁数
                entity.DpStandardDiv = stdevnumdigits;  // 標準偏差小数点以下桁数
                entity.DpMin = minnumdigits;    // 最小値小数点以下桁数
                entity.DpMax = maxnumdigits;    // 最大値小数点以下桁数
                entity.DpMedian = mednumdigits; // 中央値小数点以下桁数
                entity.DpWeight = wtnumdigits;  // ウエイト値小数点以下桁数
                entity.DpWeightavr = wtavgnumdigits;    // 加重平均小数点以下桁数
                entity.SetDeleteFlag_False();   // 論理削除フラグ
                entity.ViewSurveyName = title;  // 調査名
                entity.Language = lccd; // 対象言語
                entity.ShowZeroNaIvCode = (int)zeroshowcd;  // 0件表示コード
                entity.MergeAxisCellsFlag = mergeaxis ? "1" : "0";  // 集計軸セル結合フラグ
                entity.ScenarioName = scenarioName; // シナリオ名称

                // リクエスト情報の書き込み
                tOutputRequestBhv.Insert(entity);
                // 新規レコードのID取得
                this.id = (decimal)entity.OutputRequestId;

                // 集計結果TSVファイルサイズクリア
                resetFileSizeTotal();

                reportsets.InsertToDB();

                entity.OutputReportsetInfoId = reportsets[0].ID;   // レポートセット情報ID
                TQcwebSurveyDetailCB cb = new TQcwebSurveyDetailCB();
                cb.Query().SetQcwebid_Equal(qcwebid);
                cb.Query().AddOrderBy_SurveyNo_Asc();
                ListResultBean<TQcwebSurveyDetail> detailList = tQcwebSurveyDetailBhv.SelectList(cb);
                dlfilename = string.Format("{0}_{1}_{2}.zip", this.id.ToString(), DateTime.Now.ToString("yyMMddHHmmssfff"), OutputZipTypeSuffix());
                entity.DownloadPath = string.Format("{0}{1}/{2}", dlpath, detailList[0].Qc3uniqueId.ToString(), dlfilename);

                // 処理重度の計算
                TOutputWpMasterCB tOutputWpMasterCB = new TOutputWpMasterCB();
                ListResultBean<TOutputWpMaster> pointMasterList = tOutputWpMasterBhv.SelectList(tOutputWpMasterCB);
                if (pointMasterList.Count == 0)
                {
                    // エラースロー
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.GetProcessWeightPointMasterFailedMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL);
                }

                double weightPoint = 0.0;
                foreach (TOutputWpMaster pointEntity in pointMasterList)
                {
                    double point = (double)pointEntity.Point;
                    if (point <= 0.0)
                    {
                        weightPoint = -1.0;
                        break;
                    }
                    string code = pointEntity.OutputWpMasterIdAsOutputWPMasterID.Code;
                    long size = 0L;
                    if (CDef.OutputWPMasterID.GT.Code.Equals(code))
                    {
                        entity.TsvFileSizeGt = GTFileSize;
                        size = GTFileSize;
                    }
                    else if (CDef.OutputWPMasterID.CROSS.Code.Equals(code))
                    {
                        entity.TsvFileSizeCross = CrossFileSize;
                        size = CrossFileSize;
                    }
                    else if (CDef.OutputWPMasterID.FA.Code.Equals(code))
                    {
                        entity.TsvFileSizeFa = FAFileSize;
                        size = FAFileSize;
                    }
                    else if (CDef.OutputWPMasterID.DataOutput.Code.Equals(code))
                    {
                        entity.TsvFileSizeDataOutput = DataOutputFileSize;
                        size = DataOutputFileSize;
                    }
                    if (size < 0L)
                    {
                        weightPoint = -1.0;
                        break;
                    }
                    weightPoint += (double)size / point;
                }

                if (weightPoint < 0.0)
                {
                    // エラースロー
                    throw new QCWebException(new Message(Constants.CommonMessageIndex.JudgeProcessWeightFailedMessageIndex)
                                           , GlobalsCommonConstant.LogLevel.FATAL);
                }

                string[] judgementPointStr = appConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_DATAOUTPUT_WAIT_JUDGMENT_POINT, '|');
                double[] judgementPoint = Array.ConvertAll<string, double>(judgementPointStr, x => double.Parse(x));
                int weight = -1;
                if (weightPoint > 0.0)
                {
                    for (weight = 0; weight < judgementPoint.Length - 1; ++weight)
                    {
                        if (weightPoint <= judgementPoint[weight])
                            break;
                    }
                }
                entity.ProcWeight = ++weight;

                // リクエスト情報の書き込み
                tOutputRequestBhv.Update(entity);
            }
            catch (Exception e)
            {
                Debug.WriteLine("StackTrace:{0}", e.StackTrace);
                Debug.Indent();
                Debug.WriteLine("Type:{0}", e.GetType().ToString());
                Debug.WriteLine("Description:{0}", e.Message);
                Debug.Unindent();
                this.id = 0;

                // エラースロー
                // throw new QCWebException("システムエラーが発生しました。:" + e.Message, e);
                throw new QCWebException(new Message(Constants.CommonMessageIndex.RaisedSystemErrorMessageIndex)
                                       , e
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , e.Message);
            }
        }

        /// <summary>
        /// 出力種別に応じたZIPファイル名の接尾辞を返します
        /// </summary>
        /// <returns>ZIPファイル名の接尾辞</returns>
        /// <remarks>MANTIS#0002328</remarks>
        private String OutputZipTypeSuffix()
        {

            string openingWb = GlobalsCommonConstant.OutputTypeSign.OpeningWb;
            string closingWb = GlobalsCommonConstant.OutputTypeSign.ClosingWb;
            string openingFileExtensions = GlobalsCommonConstant.OutputTypeSign.OpeningFileExtensions;
            string closingFileExtensions = GlobalsCommonConstant.OutputTypeSign.ClosingFileExtensions;
            string separatorFileExtensions = GlobalsCommonConstant.OutputTypeSign.SeparatorFileExtensions;
            string wbSymbol = openingWb + GlobalsCommonConstant.OutputTypeSign.Wb + closingWb;
            string xls = GlobalsCommonConstant.OutputTypeSign.FileExtensions.xls.ToString();
            string ppt = GlobalsCommonConstant.OutputTypeSign.FileExtensions.ppt.ToString();
            string pdf = GlobalsCommonConstant.OutputTypeSign.FileExtensions.pdf.ToString();
            string qc3 = GlobalsCommonConstant.OutputTypeSign.FileExtensions.qc3.ToString();
            string zipname = this.outputZipType.ToString();
            string symbol = string.Empty;

            switch (this.outputZipType)
            {
                // GT表の出力を表す
                case OutputZIPType.GT:
                    Outputs.OutputGT outputGt = (Outputs.OutputGT)reportsets[0].Outputs[0];
                    symbol = outputGt.WBOn ? wbSymbol : String.Empty;
                    break;
                // クロス表の出力を表す
                case OutputZIPType.Cross:
                    Outputs.OutputCross outputCross = (Outputs.OutputCross)reportsets[0].Outputs[0];
                    symbol = outputCross.WBOn ? wbSymbol : String.Empty;
                    break;
                // Reportの出力を表す
                case OutputZIPType.Report:
                    System.Collections.Generic.IList<string> reportSymbols = new System.Collections.Generic.List<string>() { };
                    FileType fileType = reportsets[0].FileType;
                    bool selectedExcel = (fileType & FileType.Excel) == FileType.Excel;
                    bool selectedPowerPoint = (fileType & FileType.PowerPoint) == FileType.PowerPoint;
                    bool selectedPDF = (fileType & FileType.PDF) == FileType.PDF;
                    if (selectedExcel) { reportSymbols.Add(xls); }
                    if (selectedPowerPoint) { reportSymbols.Add(ppt); }
                    if (selectedPDF) { reportSymbols.Add(pdf); }
                    symbol = openingFileExtensions + String.Join(separatorFileExtensions, reportSymbols) + closingFileExtensions;
                    break;
                // データ出力を表す
                case OutputZIPType.Data:
                    OutputType outputType = reportsets[0].Outputs[0].OutputType;
                    bool selectedQC3 = (int)outputType == (int)OutputType.QC3;
                    string dataSymbol = selectedQC3 ? qc3 : xls;
                    symbol = openingFileExtensions + dataSymbol + closingFileExtensions;
                    break;
            }

            return zipname = String.Format("{0}{1}", zipname, symbol);
        }


        /// <summary>
        /// リクエストに紐づくレポートセットコレクションへの参照を返す読み取り専用プロパティ
        /// </summary>
        public IReportsets Reportsets
        {
            get
            {
                return reportsets;
            }
        }

        /// <summary>
        /// リクエストIDを返す読み取り専用プロパティ
        /// </summary>
        public decimal ID
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// 調査IDを返す読み取り専用プロパティ
        /// </summary>
        public decimal QCWebID
        {
            get
            {
                return qcwebid;
            }
        }

        /// <summary>
        /// リクエストを発行したサーバのサーバコードを取得/設定するプロパティ<br />
        /// 設定は1度のみ可能
        /// </summary>
        public string RequestServerCode
        {
            get
            {
                return reqsvcd;
            }
            set
            {
                if (reqsvcd == null)
                    reqsvcd = value;
            }
        }

        /// <summary>
        /// 出力物のダウンロードパスを取得/設定するプロパティ<br />
        /// 設定は1度のみ可能
        /// </summary>
        public string DownloadPath
        {
            get
            {
                return dlpath;
            }
            set
            {
                if (dlpath == null)
                    dlpath = value;
            }
        }

        /// <summary>
        /// 調査タイトルを取得/設定するプロパティ<br />
        /// 設定は1度のみ可能
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title == null)
                    title = value;
            }
        }

        /// <summary>
        /// 出力するExcelブックのファイル形式を表すXlFileFormat列挙型の値を取得/設定するプロパティ<br />
        /// 設定できる値は、XlFileFormat.xlOpenXMLWorkbook (Excel 2007形式を表す)、XlFileFormat.xlExcel8 (Excel 2003形式を表す)のいずれか
        /// </summary>
        public Common.XlFileFormat ExcelFileFormat
        {
            get
            {
                // return (Common.XlFileFormat)xlfmt;
                return xlfmt;
            }
            set
            {
                switch (value)
                {
                    case Common.XlFileFormat.xlOpenXMLWorkbook:
                    case Common.XlFileFormat.xlExcel8:
                        // xlfmt = (Excel.XlFileFormat)value;
                        xlfmt = value;
                        break;
                }
            }
        }

        /// <summary>
        /// 数値回答の集計表示項目コードを表すNumericContentsCode列挙型の値を直接設定する書き込み専用プロパティ
        /// </summary>
        public NumericContentsCode ShowNCode
        {
            set
            {
                showncd = value & NumericContentsCode.All;
            }
        }

        private void setShowNCode(NumericContentsCode code, bool value)
        {
            if (value)
            {
                showncd |= code;
            }
            else
            {
                showncd &= ~code;
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、統計量母数を表示するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool ShowParameter
        {
            get
            {
                return (showncd & NumericContentsCode.Parameter) == NumericContentsCode.Parameter;
            }
            set
            {
                setShowNCode(NumericContentsCode.Parameter, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、合計を表示するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool ShowSummary
        {
            get
            {
                return (showncd & NumericContentsCode.Summary) == NumericContentsCode.Summary;
            }
            set
            {
                setShowNCode(NumericContentsCode.Summary, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、平均を表示するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool ShowAverage
        {
            get
            {
                return (showncd & NumericContentsCode.Average) == NumericContentsCode.Average;
            }
            set
            {
                setShowNCode(NumericContentsCode.Average, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、標準偏差を表示するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool ShowStdev
        {
            get
            {
                return (showncd & NumericContentsCode.Stdev) == NumericContentsCode.Stdev;
            }
            set
            {
                setShowNCode(NumericContentsCode.Stdev, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、最小値を表示するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool ShowMinimum
        {
            get
            {
                return (showncd & NumericContentsCode.Minimum) == NumericContentsCode.Minimum;
            }
            set
            {
                setShowNCode(NumericContentsCode.Minimum, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、最大値を表示するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool ShowMaximum
        {
            get
            {
                return (showncd & NumericContentsCode.Maximum) == NumericContentsCode.Maximum;
            }
            set
            {
                setShowNCode(NumericContentsCode.Maximum, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計時に、中央値を表示するかどうかを取得/設定するプロパティ
        /// <note>WB集計時には無視される</note>
        /// </summary>
        public bool ShowMedian
        {
            get
            {
                return (showncd & NumericContentsCode.Median) == NumericContentsCode.Median;
            }
            set
            {
                setShowNCode(NumericContentsCode.Median, value);
            }
        }

        /// <summary>
        /// 数値回答質問の集計項目において、表示する小数点以下の桁数を返すメソッド<br />
        /// </summary>
        /// <param name="ncontentscode">
        /// 数値回答質問の集計項目を表すNumericContentsCode列挙型の以下の値のいずれか
        /// <list type="bullet">
        /// <item>
        /// <description>NumericContentsCode.Summary</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Average</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Stdev</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Minimum</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Maximum</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Median</description>
        /// </item>
        /// </list>
        /// </param>
        /// <returns>ncontentscodeが表す数値回答質問の集計項目で、表示する小数点以下の桁数</returns>
        public int NumDigitsAfterDecimal(NumericContentsCode ncontentscode)
        {
            switch (ncontentscode)
            {
                case NumericContentsCode.Summary:
                    return sumnumdigits;
                case NumericContentsCode.Average:
                    return avgnumdigits;
                case NumericContentsCode.Stdev:
                    return stdevnumdigits;
                case NumericContentsCode.Minimum:
                    return minnumdigits;
                case NumericContentsCode.Maximum:
                    return maxnumdigits;
                case NumericContentsCode.Median:
                    return mednumdigits;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 数値回答質問の集計項目において、表示する小数点以下の桁数を設定するメソッド
        /// </summary>
        /// <param name="ncontentscode">
        /// 数値回答質問の集計項目を表すNumericContentsCode列挙型の以下の値のいずれか、またはその組み合わせ
        /// <list type="bullet">
        /// <item>
        /// <description>NumericContentsCode.Summary</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Average</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Stdev</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Minimum</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Maximum</description>
        /// </item>
        /// <item>
        /// <description>NumericContentsCode.Median</description>
        /// </item>
        /// </list>
        /// ncontentscodeに含まれるすべての集計項目に同じ値を設定する
        /// </param>
        /// <param name="value">
        /// 設定する値<br />
        /// 設定できる値は0～5の整数値
        /// </param>
        public void SetNumDigitsAfterDecimal(NumericContentsCode ncontentscode, int value)
        {
            int min = int.Parse(AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_NUMERIC_CONTENTS_MIN));
            int max = int.Parse(AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_NUMERIC_CONTENTS_MAX));

            if (value < min || value > max)
                return;
            if ((ncontentscode & NumericContentsCode.Summary) == NumericContentsCode.Summary)
            {
                sumnumdigits = value;
            }
            if ((ncontentscode & NumericContentsCode.Average) == NumericContentsCode.Average)
            {
                avgnumdigits = value;
            }
            if ((ncontentscode & NumericContentsCode.Stdev) == NumericContentsCode.Stdev)
            {
                stdevnumdigits = value;
            }
            if ((ncontentscode & NumericContentsCode.Minimum) == NumericContentsCode.Minimum)
            {
                minnumdigits = value;
            }
            if ((ncontentscode & NumericContentsCode.Maximum) == NumericContentsCode.Maximum)
            {
                maxnumdigits = value;
            }
            if ((ncontentscode & NumericContentsCode.Median) == NumericContentsCode.Median)
            {
                mednumdigits = value;
            }
        }

        /// <summary>
        /// ウエイト値設定時に、表示する小数点以下の桁数を取得/設定するプロパティ
        /// </summary>
        public int WeightNumDigitsAfterDecimal
        {
            get
            {
                return wtnumdigits;
            }
            set
            {
                int min = int.Parse(AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_NUMERIC_CONTENTS_MIN));
                int max = int.Parse(AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_NUMERIC_CONTENTS_MAX));

                if (value < min || value > max)
                    return;
                wtnumdigits = value;
            }
        }

        /// <summary>
        /// 加重平均算出時に、表示する小数点以下の桁数を取得/設定するプロパティ
        /// </summary>
        public int WeightAverageNumDigitsAfterDecimal
        {
            get
            {
                return wtavgnumdigits;
            }
            set
            {
                int min = int.Parse(AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_NUMERIC_CONTENTS_MIN));
                int max = int.Parse(AppConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_NUMERIC_CONTENTS_MAX));

                if (value < min || value > max)
                    return;
                wtavgnumdigits = value;
            }
        }

        /// <summary>
        /// Disposeメソッドの実装
        /// </summary>
        public void Dispose()
        {
            reportsets.Dispose();
        }

        /// <summary>
        /// 0件の無回答/非該当を表示するかどうかを取得/設定するプロパティ
        /// <note>現行仕様では無回答と非該当の個別設定はなし</note>
        /// </summary>
        public bool ShowZeroNAIV
        {
            get
            {
                return zeroshowcd == (ZeroNAIVShowCode.NA | ZeroNAIVShowCode.IV);
            }
            set
            {
                if (value)
                {
                    zeroshowcd = ZeroNAIVShowCode.NA | ZeroNAIVShowCode.IV;
                }
                else
                {
                    zeroshowcd = (ZeroNAIVShowCode)0;
                }
            }
        }

        /// <summary>
        /// 集計軸のセルを結合するかどうかを取得/設定するプロパティ
        /// </summary>
        public bool MergeAxis
        {
            get
            {
                return mergeaxis;
            }
            set
            {
                mergeaxis = value;
            }
        }

        /// <summary>
        /// ロケーションを表すコードを取得/設定するプロパティ
        /// <note>設定時はxx_YYの形またはzz/ZZ/Zz/zZの形のみを受け付ける</note>
        /// </summary>
        public string LocationCode
        {
            get
            {
                return lccd;
            }
            set
            {
                if (value == null)
                    return;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-z]{2}_[A-Z]{2}$|^[a-zA-Z]{2}$");
                if (regex.IsMatch(value))
                    lccd = value;
            }
        }

        // これはいらないけど、リファクタリング漏れを防ぐため、残しておく
        /// <summary>
        /// 言語を取得／設定するプロパティ
        /// </summary>
        public string Language
        {
            get
            {
                return LocationCode;
            }
            set
            {
                LocationCode = value;
            }
        }

        /// <summary>
        /// リクエストユーザIDを取得／設定するプロパティ
        /// </summary>
        public string RequestUserId
        {
            get
            {
                return requestUserId;
            }
            set
            {
                requestUserId = value;
            }
        }

        /// <summary>
        /// 出力するPowerPointのファイル形式を表すPpSaveAsFileType列挙型の値を取得/設定するプロパティ<br />
        /// 設定できる値は、PpSaveAsFileType.ppSaveAsPresentation (PowerPoint 2003形式を表す)、.PpSaveAsFileType.ppSaveAsOpenXMLPresentation (PowerPoint 2007形式を表す)のいずれか
        /// </summary>
        public Common.PpSaveAsFileType PptFileFormat
        {
            get
            {
                return ppfmt;
            }
            set
            {
                switch (value)
                {
                    case Common.PpSaveAsFileType.ppSaveAsPresentation:
                    case Common.PpSaveAsFileType.ppSaveAsOpenXMLPresentation:
                        ppfmt = value;
                        break;
                }
            }
        }

        /// <summary>
        /// シナリオ名を取得／設定するプロパティ
        /// </summary>
        public string ScenarioName
        {
            get
            {
                return this.scenarioName;
            }
            set
            {
                this.scenarioName = value;
            }
        }

        private string statusdescription = null;
        /// <summary>
        /// ステータスコードの説明を取得/設定するプロパティ
        /// </summary>
        public string StatusDescription
        {
            get
            {
                return statusdescription;
            }
            set
            {
                statusdescription = value;
            }
        }

        /// <summary>
        /// 出力物ZIPタイプを設定するプロパティ
        /// </summary>
        public OutputZIPType OutputZipType
        {
            set { this.outputZipType = value; }
        }

        /// <summary>
        /// ダウンロードファイル名を取得するプロパティ
        /// </summary>
        public string DlFileName
        {
            get { return dlfilename; }
        }
    }
}
