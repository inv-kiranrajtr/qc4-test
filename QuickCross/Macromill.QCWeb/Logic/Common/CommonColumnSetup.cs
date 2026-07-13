#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：CommonColumnSetup.cs
 * バージョン：1.0.0
 * 概　　　要：DB共通カラム自動定義クラス
 * 作　成　日：2012/03/21
 * 作　成　者：寺嶋 千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Dao.AllCommon;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// アクセスコンテキスト部分を設定する
    /// </summary>
    [ComVisible(false), Guid("49A36E3C-C0B7-4FBA-B6BE-98C8073E0129")]
    public static class CommonColumnSetup {
        /// <summary>
        /// アクセスコンテキストを設定
        /// </summary>
        /// <param name="user"></param>
        public static void SetAccessContext(String user) {
            AccessContext context = new AccessContext();
            context.AccessTimestamp = DateTime.Now;
            context.AccessUser = user;
            AccessContext.SetAccessContextOnThread(context);
        }

        /// <summary>
        /// アクセスコンテキストを破棄
        /// </summary>
        public static void ClearAccessContext() {
            AccessContext.ClearAccessContextOnThread();
        }

        /// <summary>
        /// アクセスコンテキストに登録されているユーザを
        /// 取得する
        /// </summary>
        /// <returns></returns>
        public static string GetAccessContextUser() {
            return AccessContext.GetAccessUserOnThread();
        }

        /// <summary>
        /// アクセスコンテキストに登録されているタイムスタンプを
        /// 取得する
        /// </summary>
        /// <returns></returns>
        public static DateTime GetAccessContextTimestamp() {
            return AccessContext.GetAccessTimestampOnThread() ?? new DateTime(0);
        }
    }
}
