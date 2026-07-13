#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：MockMaintenanceUtil.cs
 * バージョン：1.0.0
 * 概　　　要：サーバメンテナンス判定モッククラス
 * 作　成　日：2012/6/18
 * 作　成　者：寺嶋千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Macromill.QCWeb.Common;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Batch.Common.Logic {
    /// <summary>
    /// サーバメンテナンス判定モッククラス
    /// </summary>
    [ComVisible(false), Guid("5020A474-3B1C-49ab-A959-B68AF4439B01")]
    public class MockMaintenanceUtil : IMaintenanceUtil {
        /// <summary>
        /// メンテナンスTBLを参照し現在メンテナンス中であるか確認する
        /// </summary>
        /// <returns>メンテナンス中：true メンテナンス中ではない：false</returns>
        public bool isMaintenance() {
            return false;
        }

        /// <summary>
        /// メンテナンスTBLを参照し現在メンテナンス中であるか確認する
        /// ※極大、大については、メンテナンス日の１日前から処理を行わない
        /// </summary>
        /// <param name="lv">レポートバッチ処理レベル</param>
        /// <returns>メンテナンス中：true メンテナンス中ではない：false</returns>
        public bool isMaintenance(GlobalsCommonConstant.ReportProcLevel lv) {
            return false;
        }
    }
}
