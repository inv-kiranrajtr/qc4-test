#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：AirsResultBean.cs
 * バージョン：1.0.0
 * 概　　　要：AIRs連携処理結果格納Bean
 * 作　成　日：2012/08/17
 * 作　成　者：寺嶋 千晴
 * 更　新　日：
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macromill.QCWeb.Common.OtherSystems {
    /// <summary>
    /// AIRs連携結果格納
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false), System.Runtime.InteropServices.GuidAttribute("E89909A0-5806-4509-A540-A61B998167BF")]
    public class AirsResultBean {
        /// <summary>処理結果</summary>
        private GlobalsCommonConstant.AIRS_RESULT_CODE result = GlobalsCommonConstant.AIRS_RESULT_CODE.None;
        /// <summary>失敗理由</summary>
        private string cause = null;
        /// <summary>QCWebJOBNo</summary>
        private string qcWebJobNo = null;
        /// <summary>調査ID</summary>
        private string rid = null;
        /// <summary>処理区分</summary>
        private GlobalsCommonConstant.AIRS_PROC_KBN procKbn = GlobalsCommonConstant.AIRS_PROC_KBN.None;
        /// <summary>ファイル名</summary>
        private string fileName = null;
        /// <summary>メッセージ</summary>
        private string fileMassage = null;
        /// <summary>実体ファイル</summary>
        private string realFilePath = null;
        /// <summary>追加データ管理No</summary>
        private string qcWebMngNo = null;
        /// <summary>基本納品データダウンロード区分</summary>
        private string dlKbn = null;
        /// <summary>クライアントID</summary>
        private string cid = null;
        /// <summary>会社名</summary>
        private string corpName = null;
        /// <summary>氏名-姓</summary>
        private string lName = null;
        /// <summary>氏名-名</summary>
        private string fName = null;
        /// <summary>アクセス区分情報</summary>
        private string cidKbn = null;
        /// <summary>ゲストID</summary>
        private string gcuserid = null;
        /// <summary>オーナーCID</summary>
        private string ownercid = null;
        /// <summary>アクセスURL</summary>
        private string accessUrl = null;

        /// <summary>
        /// 処理結果を保持するプロパティ
        /// </summary>
        public GlobalsCommonConstant.AIRS_RESULT_CODE Result {
            get { return result; }
            set { result = value; }
        }

        /// <summary>
        /// 失敗理由を保持するプロパティ
        /// </summary>
        public string Cause {
            get { return cause; }
            set { cause = value; }
        }

        /// <summary>
        /// QCWebJOBNoを保持するプロパティ
        /// </summary>
        public string QcWebJobNo {
            get { return qcWebJobNo; }
            set { qcWebJobNo = value; }
        }

        /// <summary>
        /// 調査IDを保持するプロパティ
        /// </summary>
        public string Rid {
            get { return rid; }
            set { rid = value; }
        }

        /// <summary>
        /// 処理区分を保持するプロパティ
        /// </summary>
        public GlobalsCommonConstant.AIRS_PROC_KBN ProcKbn {
            get { return procKbn; }
            set { procKbn = value; }
        }

        /// <summary>
        /// ファイル名を保持するプロパティ
        /// </summary>
        public string FileName {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// メッセージを保持するプロパティ
        /// </summary>
        public string FileMessage {
            get { return fileMassage; }
            set { fileMassage = value; }
        }

        /// <summary>
        /// 実体ファイルを保持するプロパティ
        /// </summary>
        public string RealFilePath {
            get { return realFilePath; }
            set { realFilePath = value; }
        }

        /// <summary>
        /// 追加データ管理Noを保持するプロパティ
        /// </summary>
        public string QcWebMngNo {
            get { return qcWebMngNo; }
            set { qcWebMngNo = value; }
        }

        /// <summary>
        /// 基本納品データダウンロード区分を保持するプロパティ
        /// </summary>
        public string DlKbn {
            get { return dlKbn; }
            set { dlKbn = value; }
        }

        /// <summary>
        /// クライアントIDを保持するプロパティ
        /// </summary>
        public string Cid {
            get { return cid; }
            set { cid = value; }
        }

        /// <summary>
        /// 会社名を保持するプロパティ
        /// </summary>
        public string CorpName {
            get { return corpName; }
            set { corpName = value; }
        }

        /// <summary>
        /// 氏名-姓を保持するプロパティ
        /// </summary>
        public string LName {
            get { return lName; }
            set { lName = value; }
        }

        /// <summary>
        /// 氏名-名を保持するプロパティ
        /// </summary>
        public string FName {
            get { return fName; }
            set { fName = value; }
        }

        /// <summary>
        /// アクセス区分情報を保持するプロパティ
        /// </summary>
        public string CidKbn {
            get { return cidKbn; }
            set { cidKbn = value; }
        }

        /// <summary>
        /// ゲストIDを保持するプロパティ
        /// </summary>
        public string Gcuserid {
            get { return gcuserid; }
            set { gcuserid = value; }
        }

        /// <summary>
        /// オーナーCIDを保持するプロパティ
        /// </summary>
        public string Ownercid {
            get { return ownercid; }
            set { ownercid = value; }
        }

        /// <summary>
        /// アクセスURLを保持するプロパティ
        /// </summary>
        public string AccessUrl {
            get { return accessUrl; }
            set { accessUrl = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append(":Result=" + Result);
            sb.Append(":Cause=" + Cause);
            sb.Append(":QcWebJobNo=" + QcWebJobNo);
            sb.Append(":Rid=" + Rid);
            sb.Append(":ProcKbn=" + ProcKbn);
            sb.Append(":FileName=" + FileName);
            sb.Append(":FileMessage=" + FileMessage);
            sb.Append(":RealFilePath=" + RealFilePath);
            sb.Append(":QcWebMngNo=" + QcWebMngNo);
            sb.Append(":DlKbn=" + DlKbn);
            sb.Append(":Cid=" + Cid);
            sb.Append(":CorpName=" + CorpName);
            sb.Append(":LName=" + LName);
            sb.Append(":FName=" + FName);
            sb.Append(":CidKbn=" + CidKbn);
            sb.Append(":Gcuserid=" + Gcuserid);
            sb.Append(":Ownercid=" + Ownercid);
            sb.Append(":AccessUrl=" + AccessUrl);

            return sb.ToString();
        }
    }
}
