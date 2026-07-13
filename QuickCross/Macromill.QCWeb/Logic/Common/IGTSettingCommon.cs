#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：IGTSettingCommon.cs
 * バージョン：1.0.0
 * 概　　　要：GT集計設定追加（共通処理）インターフェース
 * 作　成　日：2012/08/03
 * 作　成　者：鈴木　孝明
 * $Id$ / $Date$ / $Rev$ / $Author$
  ***************************************************************/
#endregion
using Seasar.Quill.Attrs;
using Macromill.QCWeb.Dao.ExEntity;
using System.Collections.Generic;

namespace Macromill.QCWeb.Common {

    /// <summary>
    /// GT集計設定追加（共通処理）インターフェース
    /// </summary>
    [Implementation(typeof(GTSettingCommonImpl)), System.Runtime.InteropServices.ComVisible(false)]
    public interface IGTSettingCommon {
        /// <summary>
        /// 基準アイテムから加工後アイテムへカテゴリをコピーする
        /// </summary>
        /// <param name="baseItemId">基準アイテムID</param>
        /// <param name="newItemId">加工後アイテムID</param>
        void CategoryDataCopy(decimal baseItemId, decimal newItemId);

        /// <summary>
        /// 指定アイテムID,マトリクス区分のアイテムのカテゴリ情報を取得する
        /// </summary>
        /// <param name="itemId">対象アイテムID</param>
        /// <param name="Matrix_div">対象アイテムのマトリクス区分</param>
        /// <returns></returns>
        IList<TCategoryInfo> FindCategoryByItemInfo(decimal itemId, int Matrix_div);
    }

}
