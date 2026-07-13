#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：AIRsCommunication.cs
 * バージョン：1.0.0
 * 概　　　要：AIRs連携を実施するクラス
 * 作　成　日：2012/03/13
 * 作　成　者：寺嶋　千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.Collections.Generic;
using Macromill.QCWeb.Common;
using Macromill.QCWeb.Exceptions;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Xml.Resolvers;

namespace Macromill.QCWeb.Common.OtherSystems {
    /// <summary>
    /// AIRs連携実装クラス
    /// </summary>
    [ComVisible(false), Guid("9B88B252-7D22-4CAC-849D-1E5A3C3CC66F")]
    public class AirsCommunication : IAirsCommunication {

        /// <summary>アプリケーション環境設定クラス</summary>
        protected ApplicationConfig applicationConfig = null;

        #region Const
        private const string NODE_NAME_RESULT = "result";
        private const string NODE_NAME_CAUSE = "cause";
        private const string NODE_NAME_QC_WEB_JOB_NO = "qcwebjobno";
        private const string NODE_NAME_RID = "rid";
        private const string NODE_NAME_PROC_KBN = "kbn";
        private const string NODE_NAME_FILE_NAME = "name";
        private const string NODE_NAME_FILE_MESSAGE = "message";
        private const string NODE_NAME_READ_FILE_PATH = "realfile";
        private const string NODE_NAME_QC_WEB_MNG_NO = "qcwebmngno";
        private const string NODE_NAME_DL_KBN = "dlkbn";
        private const string NODE_NAME_CID = "cid";
        private const string NODE_NAME_CORP_NAME = "corpname";
        private const string NODE_NAME_L_NAME = "lname";
        private const string NODE_NAME_F_NAME = "fname";
        private const string NODE_NAME_CID_KBN = "cidkbn";
        private const string NODE_NAME_GCUSERID = "gcuserid";
        private const string NODE_NAME_OWNERCID = "ownercid";

        private const string KBN_PARAM_NAME = "KBN";
        private const string RID_PARAM_NAME = "RID";
        private const string CID_PARAM_NAME = "CID";
        private const string GCUSERID_PARAM_NAME = "GCUSERID";
        private const string RESULT_PARAM_NAME = "RESULT";
        private const string JOBNO_PARAM_NAME = "JOBNO";
        private const string MNGNO_PARAM_NAME = "MNGNO";
        #endregion

        private string getPropertyName(XmlReader reader, out Type valueType) {

            valueType = typeof(string);
            switch (reader.Name.ToLower()) {
                case NODE_NAME_RESULT:
                    valueType = typeof(GlobalsCommonConstant.AIRS_RESULT_CODE);
                    return "Result";
                case NODE_NAME_CAUSE:
                    return "Cause";
                case NODE_NAME_QC_WEB_JOB_NO:
                    return "QcWebJobNo";
                case NODE_NAME_RID:
                    return "Rid";
                case NODE_NAME_PROC_KBN:
                    valueType = typeof(GlobalsCommonConstant.AIRS_PROC_KBN);
                    return "ProcKbn";
                case NODE_NAME_FILE_NAME:
                    return "FileName";
                case NODE_NAME_FILE_MESSAGE:
                    return "FileMessage";
                case NODE_NAME_READ_FILE_PATH:
                    return "RealFilePath";
                case NODE_NAME_QC_WEB_MNG_NO:
                    return "QcWebMngNo";
                case NODE_NAME_DL_KBN:
                    return "DlKbn";
                case NODE_NAME_CID:
                    return "Cid";
                case NODE_NAME_CORP_NAME:
                    return "CorpName";
                case NODE_NAME_L_NAME:
                    return "LName";
                case NODE_NAME_F_NAME:
                    return "FName";
                case NODE_NAME_CID_KBN:
                    return "CidKbn";
                case NODE_NAME_GCUSERID:
                    return "Gcuserid";
                case NODE_NAME_OWNERCID:
                    return "Ownercid";
            }
            return null;
        }

        /// <summary>
        /// AIRs連携APIの実行
        /// </summary>
        /// <param name="bean">AIRs連携WebAPIへの引数を格納したBean</param>
        /// <param name="loglevel">ログレベル</param>
        /// <returns>AIRs連携WebAPIからの連携結果を格納したBean</returns>
        public AirsResultBean ExecAIRsAPI(AirsParameterBean bean,GlobalsCommonConstant.LogLevel loglevel) {
            string url = applicationConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_WEBAPI_URL);
            if (String.IsNullOrWhiteSpace(url)) {
                throw new QCWebException(new Message(Constants.CommonMessageIndex.GetAIRsCooperateWebAPIURLFailedMessageIndex)
                                       , GlobalsCommonConstant.LogLevel.FATAL
                                       , GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_WEBAPI_URL);
            }

            string[] param = new string[] { string.Format("{0}={1}", KBN_PARAM_NAME, ((int)bean.Kbn).ToString()) };
            switch (bean.Kbn) {
                case GlobalsCommonConstant.AIRS_REQUEST_KIND.CLIENT_INFO:
                    Array.Resize<string>(ref param, 4);
                    param[1] = string.Format("{0}={1}", RID_PARAM_NAME, bean.Rid);
                    param[2] = string.Format("{0}={1}", CID_PARAM_NAME, bean.Cid);
                    param[3] = string.Format("{0}={1}", GCUSERID_PARAM_NAME, bean.Gcuserid);
                    break;
                case GlobalsCommonConstant.AIRS_REQUEST_KIND.PROC_END:
                    Array.Resize<string>(ref param, 5);
                    param[1] = string.Format("{0}={1}", RESULT_PARAM_NAME, bean.Result);
                    param[2] = string.Format("{0}={1}", JOBNO_PARAM_NAME, bean.Jobno);
                    param[3] = string.Format("{0}={1}", RID_PARAM_NAME, bean.Rid);
                    param[4] = string.Format("{0}={1}", MNGNO_PARAM_NAME, bean.Mngno);
                    break;
            }
            url += "?" + string.Join("&", param);

            Encoding enc = Encoding.UTF8;

            HttpWebResponse httpWResp = null;
            AirsResultBean resultBean = new AirsResultBean();
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
            httpWReq.Method = "GET";
            httpWReq.ReadWriteTimeout = 5 * 1000;
            httpWReq.Timeout = 5 * 1000;
            httpWReq.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            httpWReq.KeepAlive = true;

            try {
                httpWResp = (HttpWebResponse)httpWReq.GetResponse();
                using (Stream resStream = httpWResp.GetResponseStream()) {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    // DTD廃止に伴いコメント
                    //settings.DtdProcessing = DtdProcessing.Parse;
                    //settings.ValidationType = ValidationType.DTD;
                    //settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

                    XmlReader reader = XmlReader.Create(resStream, settings);
                    while (reader.Read()) {
                        if (reader.NodeType != XmlNodeType.Element) continue;

                        while (reader.MoveToNextAttribute()) {
                            Type valueType = null;
                            string propName = getPropertyName(reader, out valueType);
                            string value = reader.Value;
                            if (propName == null) continue;
                            if (string.IsNullOrEmpty(value)) continue;
                            COMOperate.LateBind.SetPropertyLateBind<AirsResultBean>(
                                    resultBean, propName
                                  , valueType == typeof(string) ? value : Enum.Parse(valueType, value));
                        }
                    }
                }
            } catch(Exception ex) {
                throw new QCWebException("QCCMN90000101", new string[] { string.Format("URL:[{0}] Detail:[{1}]", url, ex.Message) }
                                         , loglevel, null);
            } finally {
                if (httpWResp != null) {
                    httpWResp.Close();
                }
            }

            // 取得結果チェック
            string err = null;
            if (!IsAirsResult(bean.Kbn, resultBean, out err)) {
                throw new QCWebException("QCCMN90000101", new string[] { string.Format("{0} QCWebジョブNo:{1} 調査ID:{2} 追加データ管理:{3}", err,resultBean.QcWebJobNo,resultBean.Rid,resultBean.QcWebMngNo) }, GlobalsCommonConstant.LogLevel.FATAL, null);
            }

            // 整合性チェック
            // クライアント情報の取得IFの場合、引数と結果が一致していること
            string checkFlag = applicationConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_AIRS_COORDINATE_CHECK_FLAG);
            if (checkFlag != "0") {
                switch (bean.Kbn) {
                    case GlobalsCommonConstant.AIRS_REQUEST_KIND.CLIENT_INFO:
                        if (resultBean.Result == GlobalsCommonConstant.AIRS_RESULT_CODE.Success) {
                            #region クライアント情報の取得IF
                            // RID
                            if (bean.Rid != resultBean.Rid) {
                                // {0}が引数と処理結果で一致しません。引数:{1} 処理結果:{2}
                                throw new QCWebException("QCCMN90000117", new string[] { "RID", bean.Rid, resultBean.Rid }, GlobalsCommonConstant.LogLevel.FATAL, null);
                            }
                            // CID
                            if (!string.IsNullOrEmpty(resultBean.Cid)) {
                                if (bean.Cid != resultBean.Cid) {
                                    // {0}が引数と処理結果で一致しません。引数:{1} 処理結果:{2}
                                    throw new QCWebException("QCCMN90000117", new string[] { "CID", bean.Cid, resultBean.Cid }, GlobalsCommonConstant.LogLevel.FATAL, null);
                                }
                            }
                            // Gcuserid
                            if (!string.IsNullOrEmpty(bean.Gcuserid)) {
                                if (bean.Gcuserid != resultBean.Gcuserid) {
                                    // {0}が引数と処理結果で一致しません。引数:{1} 処理結果:{2}
                                    throw new QCWebException("QCCMN90000117", new string[] { "Gcuserid", bean.Gcuserid, resultBean.Gcuserid }, GlobalsCommonConstant.LogLevel.FATAL, null);
                                }
                            }
                        }
                        break;
                        #endregion
                }
            }

            resultBean.AccessUrl = url; 
            return resultBean;
        }

        /// <summary>
        /// WebAPI処理結果のチェック
        /// </summary>
        /// <param name="kbn"></param>
        /// <param name="resultBean"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private bool IsAirsResult(GlobalsCommonConstant.AIRS_REQUEST_KIND kbn, AirsResultBean resultBean, out string err) {
            err = "";
            // 処理結果
            if (!Enum.IsDefined(typeof(GlobalsCommonConstant.AIRS_RESULT_CODE), resultBean.Result)) {
                // 処理結果が不正です。{0}
                err = GetResource.GetLogMessage("QCCMN90000102", resultBean.Result.GetHashCode().ToString());
                return false;
            }
            if(resultBean.Result == GlobalsCommonConstant.AIRS_RESULT_CODE.None){
                // 処理結果が不正です。{0}
                err = GetResource.GetLogMessage("QCCMN90000102", resultBean.Result.GetHashCode().ToString());
                return false;
            }

            switch (kbn) {
                case GlobalsCommonConstant.AIRS_REQUEST_KIND.PROC_OBJECT:
                    #region 処理対象の要求受信IF
                    switch (resultBean.Result) {
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Success:
                            #region 成功
                            // QCWebJobNo
                            if (string.IsNullOrEmpty(resultBean.QcWebJobNo)) {
                                // QCWebJobNoが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000103");
                                return false;
                            }
                            // 調査ID
                            if (string.IsNullOrEmpty(resultBean.Rid)) {
                                // RIDが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000104");
                                return false;
                            }
                            // 処理区分
                            if (!Enum.IsDefined(typeof(GlobalsCommonConstant.AIRS_PROC_KBN), resultBean.ProcKbn)) {
                                // 処理区分が不正です。{0}
                                err = GetResource.GetLogMessage("QCCMN90000105", resultBean.ProcKbn.GetHashCode().ToString());
                                return false;
                            }
                            // ファイル名
                            if (string.IsNullOrEmpty(resultBean.FileName)) {
                                // ファイル名が未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000106");
                                return false;
                            }
                            // 実体ファイルパス
                            if (string.IsNullOrEmpty(resultBean.RealFilePath)) {
                                // 実体ファイルパスが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000107");
                                return false;
                            }
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Failure:
                            #region 失敗
                            // チェックなし
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.SuccessNone:
                            #region 成功(処理対象なし)
                            // チェックなし
                            break;
                            #endregion
                    }
                    break;
                    #endregion
                case GlobalsCommonConstant.AIRS_REQUEST_KIND.PROC_END:
                    #region 処理完了の通知IF
                    switch (resultBean.Result) {
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Success:
                            #region 成功
                            // QCWebJobNo
                            if (string.IsNullOrEmpty(resultBean.QcWebJobNo)) {
                                // QCWebJobNoが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000103");
                                return false;
                            }
                            // 調査ID
                            if (string.IsNullOrEmpty(resultBean.Rid)) {
                                // RIDが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000104");
                                return false;
                            }
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Failure:
                            #region 失敗
                            // チェックなし
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.SuccessNone:
                            #region 成功(処理対象なし)
                            // チェックなし
                            break;
                            #endregion
                    }
                    break;
                    #endregion
                case GlobalsCommonConstant.AIRS_REQUEST_KIND.DELETE_OBJECT:
                    #region 削除対象の要求受信IF
                    switch (resultBean.Result) {
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Success:
                            #region 成功
                            // QCWebJobNo
                            if (string.IsNullOrEmpty(resultBean.QcWebJobNo)) {
                                // QCWebJobNoが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000103");
                                return false;
                            }
                            // 調査ID
                            if (string.IsNullOrEmpty(resultBean.Rid)) {
                                // RIDが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000104");
                                return false;
                            }
                            // 追加データ管理No
                            if (string.IsNullOrEmpty(resultBean.QcWebMngNo)) {
                                // 追加データ管理Noが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000108");
                                return false;
                            }
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Failure:
                            #region 失敗
                            // チェックなし
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.SuccessNone:
                            #region 成功(処理対象なし)
                            // チェックなし
                            break;
                            #endregion
                    }
                    break;
                    #endregion
                case GlobalsCommonConstant.AIRS_REQUEST_KIND.CLIENT_INFO:
                    #region クライアント情報の取得IF
                    switch (resultBean.Result) {
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Success:
                            #region 成功
                            // 調査ID
                            if (string.IsNullOrEmpty(resultBean.Rid)) {
                                // RIDが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000104");
                                return false;
                            }
                            // 基本納品データダウンロード区分
                            if (string.IsNullOrEmpty(resultBean.DlKbn)) {
                                // 基本納品データダウンロード区分が未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000109");
                                return false;
                            }
                            //// クライアントID
                            //if (string.IsNullOrEmpty(resultBean.Cid)) {
                            //    // クライアントIDが未設定です。
                            //    err = GetResource.GetLogMessage("QCCMN90000110");
                            //    return false;
                            //}
                            //// 会社名
                            //if (string.IsNullOrEmpty(resultBean.CorpName)) {
                            //    // 会社名が未設定です。
                            //    err = GetResource.GetLogMessage("QCCMN90000111");
                            //    return false;
                            //}
                            //// 氏名_姓
                            //if (string.IsNullOrEmpty(resultBean.LName)) {
                            //    // 氏名_姓が未設定です。
                            //    err = GetResource.GetLogMessage("QCCMN90000112");
                            //    return false;
                            //}
                            //// 氏名_名
                            //if (string.IsNullOrEmpty(resultBean.FName)) {
                            //    // 氏名_名が未設定です。
                            //    err = GetResource.GetLogMessage("QCCMN90000113");
                            //    return false;
                            //}
                            // アクセス区分情報
                            if (string.IsNullOrEmpty(resultBean.CidKbn)) {
                                // アクセス区分情報が未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000114");
                                return false;
                            }
                            // オーナーCID
                            if (string.IsNullOrEmpty(resultBean.Ownercid)) {
                                // オーナーCIDが未設定です。
                                err = GetResource.GetLogMessage("QCCMN90000115");
                                return false;
                            }
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.Failure:
                            #region 失敗
                            // チェックなし
                            break;
                            #endregion
                        case GlobalsCommonConstant.AIRS_RESULT_CODE.SuccessNone:
                            #region 成功(処理対象なし)
                            // チェックなし
                            break;
                            #endregion
                    }
                    break;
                    #endregion
                default:
                    // 指定されたWebAPI区分はありません。{0}
                    throw new QCWebException("QCCMN90000116", new string[] { kbn.GetHashCode().ToString() }
                                             , GlobalsCommonConstant.LogLevel.FATAL, null);
            }
            return true;
        }

        /// <summary>
        /// Exceptionハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationCallBack(object sender, ValidationEventArgs e) {
            throw new QCWebException(new Message(Constants.CommonMessageIndex.DTDValidationErrorMessageIndex)
                                   , GlobalsCommonConstant.LogLevel.FATAL
                                   , e.Message);
        }
    }
}
