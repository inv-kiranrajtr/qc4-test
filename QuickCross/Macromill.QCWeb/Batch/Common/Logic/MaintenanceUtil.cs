#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：MaintenanceUtil.cs
 * バージョン：1.0.0
 * 概　　　要：サーバメンテナンス判定クラス
 * 作　成　日：2012/03/27
 * 作　成　者：寺嶋　千晴
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Common;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Batch.Common.Logic {
    /// <summary>
    /// サーバメンテナンス判定クラス
    /// </summary>
    [Implementation, ComVisible(false), Guid("75CA9144-8857-4ebc-993B-3C362C51E548")]
    public class MaintenanceUtil : IMaintenanceUtil {

        /// <summary>メンテナンスTBLアクセス</summary>
        protected TMaintenanceBhv tMaintenanceBhv = null;

        /// <summary>
        /// メンテナンスTBLを参照し現在メンテナンス中であるか確認する
        /// </summary>
        /// <returns>メンテナンス中：true メンテナンス中ではない：false</returns>
        public bool isMaintenance() {
            return IsMaintenance(DateTime.Now);
        }

        /// <summary>
        /// メンテナンスTBLを参照し現在メンテナンス中であるか確認する
        /// ※極大、大については、メンテナンス日の１日前から処理を行わない
        /// </summary>
        /// <param name="lv"></param>
        /// <returns>メンテナンス中：true メンテナンス中ではない：false</returns>
        public bool isMaintenance(GlobalsCommonConstant.ReportProcLevel lv) {
            bool flg = true;
            switch (lv) {
                case GlobalsCommonConstant.ReportProcLevel.LARGE:
                case GlobalsCommonConstant.ReportProcLevel.EXTRA_LARGE:
                    flg = IsMaintenance(DateTime.Now.AddDays(1));
                    break;
                default:
                    flg = isMaintenance();
                    break;
            }
            return flg;
        }

        /// <summary>
        /// 実行可能な処理重度を取得する
        /// </summary>
        /// <returns></returns>
        public GlobalsCommonConstant.ReportProcLevel GetMaxProcLevel(GlobalsCommonConstant.ReportProcLevel lv) {
            bool flg = isMaintenance(lv);
            if (flg) {
                return GlobalsCommonConstant.ReportProcLevel.MEDIUM;
            }
            return lv;
        }

        /// <summary>
        /// メンテナンスTBLを参照し現在メンテナンス中であるか確認する
        /// </summary>
        /// <param name="time">現在のシステム日時</param>
        /// <returns>メンテナンス中：true メンテナンス中ではない：false</returns>
        private bool IsMaintenance(DateTime time) {
            TMaintenanceCB cb = new TMaintenanceCB();
            cb.Query().SetLimitTime_LessEqual(time);
            int count = tMaintenanceBhv.SelectCount(cb);

            return count > 0 ? true : false;
        }
    }
}
