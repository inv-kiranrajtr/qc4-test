 #region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：MockAIRsCommunication.cs
 * バージョン：1.0.0
 * 概　　　要：AIRs連携モッククラス
 * 作　成　日：2012/6/16
 * 作　成　者：寺嶋　千晴
 * $Id: MockAirsCommunication.cs 11166 2013-12-19 06:52:02Z kousaka $ / $Date: 2013-12-19 15:52:02 +0900 (2013/12/19 (木)) $ / $Rev: 11166 $ / $Author: kousaka $
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Exceptions;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common.OtherSystems {
    /// <summary>
    /// AIRs連携モッククラス
    /// </summary>
    [ComVisible(false), Guid("5020A474-3B1C-49ab-A959-B68AF4439B01")]
    public class MockAirsCommunication : IAirsCommunication {

        private int execCnt1 = 0;
        private int execCnt2 = 0;
        private int execCnt3 = 0;
        //private int execCnt4 = 0;

        /// <summary>アプリケーション環境設定クラス</summary>
        protected ApplicationConfig applicationConfig;

        /// <summary>
        /// AIRs連携APIの実行(Mock)
        /// </summary>
        /// <param name="bean">AIRs連携WebAPIへの引数を格納したBean</param>
        /// <param name="loglevel">ログレベル</param>
        /// <returns>AIRs連携WebAPIからの連携結果を格納したBean</returns>
        public AirsResultBean ExecAIRsAPI(AirsParameterBean bean,GlobalsCommonConstant.LogLevel loglevel) {

            string url = applicationConfig.GetValue(GlobalsCommonConstant.APP_CONFIG_COMMON_AIRS_WEBAPI_URL);
            if (String.IsNullOrWhiteSpace(url))
            {
                throw new QCWebException("AIRs連携WebAPIのURL取得に失敗しました。:" + url, GlobalsCommonConstant.LogLevel.FATAL, null);
            }

            AirsResultBean[] resultBeanArray = null;

            // 処理対象の要求受信IF
            if (bean.Kbn == GlobalsCommonConstant.AIRS_REQUEST_KIND.PROC_OBJECT)
            {
                // 連携結果７パターン
                resultBeanArray = new AirsResultBean[7];

                // 標準納品（過去に取込なし）
                resultBeanArray[0] = new AirsResultBean();
                resultBeanArray[0].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[0].Cause = "";
                resultBeanArray[0].QcWebJobNo = "JOB-020001";
                resultBeanArray[0].Rid = "20001";
                resultBeanArray[0].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.Normal;
                resultBeanArray[0].FileName = "テスト20001.qc3";
                resultBeanArray[0].FileMessage = "";
                resultBeanArray[0].RealFilePath = "/home/qcweb/share/batch/test20001";

                // 標準納品（過去に取込あり。本調査ID=10001で本調査管理、QCWeb調査管理レコード存在。）
                resultBeanArray[1] = new AirsResultBean();
                resultBeanArray[1].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[1].Cause = "";
                resultBeanArray[1].QcWebJobNo = "JOB-020002";
                resultBeanArray[1].Rid = "10001";
                resultBeanArray[1].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.Normal;
                resultBeanArray[1].FileName = "テスト20002.qc3";
                resultBeanArray[1].FileMessage = "";
                resultBeanArray[1].RealFilePath = "/home/qcweb/share/batch/test20002";

                // 追加納品
                resultBeanArray[2] = new AirsResultBean();
                resultBeanArray[2].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[2].Cause = "";
                resultBeanArray[2].QcWebJobNo = "JOB-020003";
                resultBeanArray[2].Rid = "20003";
                resultBeanArray[2].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.ADD;
                resultBeanArray[2].FileName = "テスト20003.zip";
                resultBeanArray[2].FileMessage = "メッセージ２０００３";
                resultBeanArray[2].RealFilePath = "/home/qcweb/share/batch/test20003";

                // QcWebJobNo重複
                resultBeanArray[3] = new AirsResultBean();
                resultBeanArray[3].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[3].Cause = "";
                resultBeanArray[3].QcWebJobNo = "JOB-020003";
                resultBeanArray[3].Rid = "20004";
                resultBeanArray[3].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.ADD;
                resultBeanArray[3].FileName = "テスト20004.zip";
                resultBeanArray[3].FileMessage = "メッセージ２０００４";
                resultBeanArray[3].RealFilePath = "/home/qcweb/share/batch/test20004";

                // QcWebJobNoカラムサイズオーバー
                resultBeanArray[4] = new AirsResultBean();
                resultBeanArray[4].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[4].Cause = "";
                resultBeanArray[4].QcWebJobNo = "JOB-9999999999";
                resultBeanArray[4].Rid = "20005";
                resultBeanArray[4].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.ADD;
                resultBeanArray[4].FileName = "テスト20005.zip";
                resultBeanArray[4].FileMessage = "メッセージ２０００５";
                resultBeanArray[4].RealFilePath = "/home/qcweb/share/batch/test20005";

                // 取込対象なし
                resultBeanArray[5] = new AirsResultBean();
                resultBeanArray[5].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.SuccessNone;
                resultBeanArray[5].Cause = "";
                resultBeanArray[5].QcWebJobNo = "";
                resultBeanArray[5].Rid = "";
                resultBeanArray[5].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.None;
                resultBeanArray[5].FileName = "";
                resultBeanArray[5].FileMessage = "";
                resultBeanArray[5].RealFilePath = "";

                // AIRsエラー
                resultBeanArray[6] = new AirsResultBean();
                resultBeanArray[6].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Failure;
                resultBeanArray[6].Cause = "ＡＩＲｓ処理対象の要求エラー";
                resultBeanArray[6].QcWebJobNo = "";
                resultBeanArray[6].Rid = "";
                resultBeanArray[6].ProcKbn = GlobalsCommonConstant.AIRS_PROC_KBN.None;
                resultBeanArray[6].FileName = "";
                resultBeanArray[6].FileMessage = "";
                resultBeanArray[6].RealFilePath = "";

                if (execCnt1 < resultBeanArray.Length) execCnt1++;
                return resultBeanArray[execCnt1 - 1];
            }

            // 処理完了の通知IF
            if (bean.Kbn == GlobalsCommonConstant.AIRS_REQUEST_KIND.PROC_END)
            {
                // 連携結果２パターン
                resultBeanArray = new AirsResultBean[2];

                // 正常終了
                resultBeanArray[0] = new AirsResultBean();
                resultBeanArray[0].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[0].Cause = "";
                resultBeanArray[0].QcWebJobNo = bean.Jobno;
                resultBeanArray[0].Rid = bean.Rid;
                resultBeanArray[0].QcWebMngNo = bean.Mngno;

                // AIRsエラー
                resultBeanArray[1] = new AirsResultBean();
                resultBeanArray[1].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Failure;
                resultBeanArray[1].Cause = "ＡＩＲｓ処理完了の通知エラー";
                resultBeanArray[1].QcWebJobNo = bean.Jobno;
                resultBeanArray[1].Rid = bean.Rid;
                resultBeanArray[1].QcWebMngNo = bean.Mngno;

                if (execCnt2 < resultBeanArray.Length) execCnt2++;
                return resultBeanArray[execCnt2 - 1];
            }

            // 削除対象の要求受信IF
            if (bean.Kbn == GlobalsCommonConstant.AIRS_REQUEST_KIND.DELETE_OBJECT)
            {
                Random cRandom = new System.Random();

                // 連携結果５パターン
                resultBeanArray = new AirsResultBean[5];

                // 正常終了
                resultBeanArray[0] = new AirsResultBean();
                resultBeanArray[0].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[0].Cause = "";
                resultBeanArray[0].QcWebJobNo = cRandom.Next(10000).ToString();
                resultBeanArray[0].Rid = "20001";
                resultBeanArray[0].QcWebMngNo = "2001";

                // QcWebJobNo重複
                resultBeanArray[1] = new AirsResultBean();
                resultBeanArray[1].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[1].Cause = "";
                resultBeanArray[1].QcWebJobNo = cRandom.Next(10000).ToString();
                resultBeanArray[1].Rid = "20002";
                resultBeanArray[1].QcWebMngNo = "2002";

                // QcWebJobNoカラムサイズオーバー
                resultBeanArray[2] = new AirsResultBean();
                resultBeanArray[2].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Success;
                resultBeanArray[2].Cause = "";
                resultBeanArray[2].QcWebJobNo = cRandom.Next(10000).ToString();
                resultBeanArray[2].Rid = "20003";
                resultBeanArray[2].QcWebMngNo = "2003";

                // 削除対象なし
                resultBeanArray[3] = new AirsResultBean();
                resultBeanArray[3].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.SuccessNone;
                resultBeanArray[3].Cause = "";
                resultBeanArray[3].QcWebJobNo = "";
                resultBeanArray[3].Rid = "";
                resultBeanArray[3].QcWebMngNo = "";

                // AIRsエラー
                resultBeanArray[4] = new AirsResultBean();
                resultBeanArray[4].Result = GlobalsCommonConstant.AIRS_RESULT_CODE.Failure;
                resultBeanArray[4].Cause = "ＡＩＲｓ削除対象の要求エラー";
                resultBeanArray[4].QcWebJobNo = "";
                resultBeanArray[4].Rid = "";
                resultBeanArray[4].QcWebMngNo = "";

                //if (execCnt3 < resultBeanArray.Length) execCnt3++;
                return resultBeanArray[cRandom.Next(0, 4)];
            }

            return null;
        }
    }
}
