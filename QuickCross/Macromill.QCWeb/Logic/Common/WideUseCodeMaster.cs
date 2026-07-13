#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：CodeMaster.cs
 * バージョン：1.0.0
 * 概　　　要：汎用コードマスタ－共通クラス
 * 作　成　日：2012/7/24
 * 作　成　者：寺嶋 千晴
 * 更　新　日：2012/7/24
 * $Id: WideUseCodeMaster.cs 7854 2013-01-11 02:09:11Z cterash $ / $Date: 2013-01-11 11:09:11 +0900 (2013/01/11 (金)) $ / $Rev: 7854 $ / $Author: cterash $
 ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Exceptions;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// 汎用コードマスタ共通クラス
    /// </summary>
    [Implementation, ComVisible(false), Guid("67EEF3C2-B1FE-4293-BE48-E8C75CA15EF1")]
    public class WideUseCodeMaster {
        private Dictionary<string, List<TCodeMaster>> master = null;

        /// <summary>汎用コードマスタDBアクセスクラス</summary>
        protected TCodeMasterBhv tCodeMasterBhv = null;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init() {
            ReadCodeMaster();
        }

        /// <summary>
        /// グループキー指定で汎用コードマスタリストを取得する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TCodeMaster> GetGroupKey(string key) {
            if (master == null) ReadCodeMaster();
            if (!master.ContainsKey(key.ToLower())) {
                // 汎用コードマスタに指定された{0}は存在しません。
                throw new KeyNotFoundException(GetResource.GetLogMessage("QCCMN16001000", key));
            }
            return master[key.ToLower()];
        }

        /// <summary>
        /// グループキー、コード値指定で汎用コードマスタを取得する
        /// </summary>
        /// <param name="groupKey"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public TCodeMaster GetGroupKeyCodeValue(string groupKey, string code) {
            List<TCodeMaster> list = GetGroupKey(groupKey);
            foreach (TCodeMaster entity in list) {
                if (code.ToLower().Equals(entity.CodeValue.ToLower())) {
                    return entity;
                }
            }
            // 汎用コードマスタに指定された{0}:{1}は存在しません。
            throw new KeyNotFoundException(GetResource.GetLogMessage("QCCMN16001001", groupKey, code));
        }

        /// <summary>
        /// 汎用コードマスタを読み込んでメモリに保持する
        /// </summary>
        private void ReadCodeMaster() {
            TCodeMasterCB cb = new TCodeMasterCB();
            cb.Query().AddOrderBy_GroupKey_Asc();
            cb.Query().AddOrderBy_SortNo_Asc();
            ListResultBean<TCodeMaster> list = tCodeMasterBhv.SelectList(cb);
            if (list.Count <= 0) {
                // 汎用コードマスタにデータが登録されていません。
                throw new QCWebException("QCCMN16001002", GlobalsCommonConstant.LogLevel.FATAL, null);
            }

            master = new Dictionary<string, List<TCodeMaster>>();
            foreach (TCodeMaster entity in list) {
                string groupKey = entity.GroupKey.ToLower();
                List<TCodeMaster> codeList = null;
                if (master.ContainsKey(groupKey)) {
                    codeList = master[groupKey];
                } else {
                    codeList = new List<TCodeMaster>();
                }

                codeList.Add(entity);
                master[groupKey] = codeList;
            }
        }
    }
}
