#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：ApplicationConfig.cs
 * バージョン：1.0.0
 * 概　　　要：データ加工リストメニュークラス
 * 作　成　日：2012/07/24
 * 作　成　者：yzhengy
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
using Macromill.QCWeb.Dao.AllCommon.CBean;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common {
    /// <summary>
    /// データ加工リストメニュー共通クラス
    /// </summary>
    [Implementation, ComVisible(false), Guid("96B09B8F-8978-4f6d-A03D-591F1B3D88C0")]
    public class EditMenuMaster
    {
        /// <summary>加工メニュー情報報格納</summary>
        private Dictionary<int, TEditMenuMaster> map;
        /// <summary>加工メニュー情報アクセス</summary>
        protected TEditMenuMasterBhv _editMenuMasterBhv;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init() {
            ReadEditMenuMaster();
        }

        /// <summary>
        /// 指定したキーワードの情報を取得する
        /// </summary>
        /// <param name="keyword">データ加工メニューID</param>
        /// <returns></returns>
        public TEditMenuMaster GetValue(int keyword)
        {
            ReadEditMenuMaster();
            TEditMenuMaster val = null;
            try {
                val = map[keyword];
            } catch (Exception ex) {
                // TODO:適切なExceptionを
                throw new KeyNotFoundException("加工メニューマスタ情報取得でエラーが発生しました：" + ex.ToString());
            }
            return val;
        }

        /// <summary>
        /// 指定したキーワードの情報を取得する
        /// </summary>
        /// <returns></returns>
        public List<TEditMenuMaster> GetAllValues()
        {
            ReadEditMenuMaster();
            List<TEditMenuMaster> val = null;
            try {
                val = map.Values.ToList<TEditMenuMaster>();
            } catch (Exception ex) {
                // TODO:適切なExceptionを
                throw new KeyNotFoundException("加工メニューマスタ情報取得でエラーが発生しました：" + ex.ToString());
            }
            return val;
        }

        /// <summary>
        /// アプリ環境設定TBLの読み込み
        /// </summary>
        private void ReadEditMenuMaster() {
            if (map == null || map.Count <= 0) {
                TEditMenuMasterCB tEditMenuCB = new TEditMenuMasterCB();

                tEditMenuCB.Query().AddOrderBy_SortNo_Asc();
                ListResultBean<TEditMenuMaster> list = _editMenuMasterBhv.SelectList(tEditMenuCB);

                map = new Dictionary<int, TEditMenuMaster>();
                foreach (TEditMenuMaster entity in list)
                {
                    map[(int)entity.EditMenuMasterId] = entity;
                }
            }
        }
    }
}
