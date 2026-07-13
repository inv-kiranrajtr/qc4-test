#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：GTSettingCommonImpl.cs
 * バージョン：1.0.0
 * 概　　　要：GT集計設定追加（共通処理）
 * 作　成　日：2012/08/03
 * 作　成　者：鈴木　孝明
 * $Id$ / $Date$ / $Rev$ / $Author$
  ***************************************************************/
#endregion
using System.Collections.Generic;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.ExBhv;
using Macromill.QCWeb.Dao.ExEntity;
using Seasar.Quill.Attrs;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Common {

    /// <summary>
    /// GT集計設定追加（共通処理）
    /// </summary>
    [Implementation, ComVisible(false), Guid("6E003456-7B19-412b-94CE-159D20971FA3")]
    public class GTSettingCommonImpl : IGTSettingCommon
    {
        /// <summary>アイテム情報TBLアクセス</summary>
        protected TItemInfoBhv _itemInfoBhv;
        /// <summary>カテゴリ情報TBLアクセス</summary>
        protected TCategoryInfoBhv _categoryInfoBhv;
        /// <summary>マトリクス情報TBLアクセス</summary>
        protected TMatrixInfoBhv _matrixInfoBhv;

        /// <summary>
        /// 基準アイテムから加工後アイテムへカテゴリをコピーする
        /// </summary>
        /// <param name="baseItemId">基準アイテムID</param>
        /// <param name="newItemId">加工後アイテムID</param>
        public virtual void CategoryDataCopy(decimal baseItemId, decimal newItemId)
        {
            // 基準アイテム情報取得
            TItemInfo itemEntity = _itemInfoBhv.SelectByPKValue(baseItemId);

            // 基準アイテムのカテゴリを取得
            IList<TCategoryInfo> baseCategoryList = FindCategoryByItemInfo(baseItemId, (int)itemEntity.MatrixDiv);

            // カテゴリが存在しない場合は抜ける
            if (baseCategoryList == null || baseCategoryList.Count == 0)
            {
                return;
            }

            // 基準アイテムのカテゴリ件数分LOOP
            TCategoryInfoCB tCategoryInfoCB = new TCategoryInfoCB();
            tCategoryInfoCB.Query().SetItemInfoId_Equal(newItemId);
            var list = _categoryInfoBhv.SelectList(tCategoryInfoCB);
            List<TCategoryInfo> insertnewCategoryList = new List<TCategoryInfo>();
            List<TCategoryInfo> updatenewCategoryList = new List<TCategoryInfo>();
            foreach (TCategoryInfo baseCategory in baseCategoryList)
            {
                TCategoryInfo newCategory = null;

                foreach (var data in list)
                {
                    if (data.CategoryNo == baseCategory.CategoryNo)
                    {
                        newCategory = data;
                        break;
                    }
                }
                // 存在しない場合は新規登録
                if (newCategory == null)
                {
                    newCategory = new TCategoryInfo();
                    newCategory.ItemInfoId = newItemId;
                    newCategory.CategoryNo = baseCategory.CategoryNo;
                }

                newCategory.CategoryName = baseCategory.CategoryName;
                newCategory.WeightValue = baseCategory.WeightValue;
                newCategory.OriginalCategoryName = baseCategory.OriginalCategoryName;

                // 新規登録または更新
                if (newCategory.CategoryInfoId == null)
                {
                    insertnewCategoryList.Add(newCategory);
                }
                else
                {
                    updatenewCategoryList.Add(newCategory);
                }
            }

            using (var bind = new Dao.Dao.ArrayBindInsert())
            {
                if (insertnewCategoryList.Count > 0) bind.ExecuteInsert<TCategoryInfo>(insertnewCategoryList, true);
                if (updatenewCategoryList.Count > 0) bind.ExecuteUpdate<TCategoryInfo>(updatenewCategoryList);
            }

            return;
        }

        /// <summary>
        /// 紐づくカテゴリ情報を取得する
        /// </summary>
        /// <param name="itemId">対象アイテムID</param>
        /// <param name="matrixDiv">マトリクス区分</param>
        /// <returns>カテゴリ情報リスト</returns>
        public virtual IList<TCategoryInfo> FindCategoryByItemInfo(decimal itemId, int matrixDiv)
        {
            decimal categorySelectItemId = 0;

            // マトリクス区分が「0：通常アイテム」または「1：親マトリクス」の場合
            // 紐づくカテゴリを返す
            if (int.Parse(CDef.MatrixType.NormalItem.Code) == matrixDiv ||
                int.Parse(CDef.MatrixType.MatrixParent.Code) == matrixDiv)
            {
                categorySelectItemId = itemId;
            }

            // マトリクス区分が「2：子マトリクス」または「4：子マトリクス（親作成元アイテム）」の場合
            // マトリクス親に紐づくカテゴリを返す
            if (int.Parse(CDef.MatrixType.MatrixChild.Code) == matrixDiv ||
                int.Parse(CDef.MatrixType.FirstChild.Code) == matrixDiv)
            {
                TMatrixInfoCB matrixCB = new TMatrixInfoCB();
                matrixCB.Query().SetChildItemInfoId_Equal(itemId);
                matrixCB.AddOrderBy_PK_Asc();
                matrixCB.FetchFirst(1);
                TMatrixInfo matrix = _matrixInfoBhv.SelectEntity(matrixCB);

                if (matrix != null)
                {
                    categorySelectItemId = (decimal)matrix.ItemInfoId;
                }
            }

            if (categorySelectItemId == 0)
            {
                return null;
            }

            // 関連カテゴリIDを取得する
            TCategoryInfoCB conditionBean = new TCategoryInfoCB();
            conditionBean.Query().SetItemInfoId_Equal(categorySelectItemId);
            conditionBean.Query().AddOrderBy_CategoryNo_Asc();
            IList<TCategoryInfo> itemInfoList = _categoryInfoBhv.SelectList(conditionBean);
            return itemInfoList;
        }
    }

}
