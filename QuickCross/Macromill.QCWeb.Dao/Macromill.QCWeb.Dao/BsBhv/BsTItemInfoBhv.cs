
using System;
using System.Collections.Generic;

using Seasar.Quill;
using Seasar.Quill.Attrs;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.Bhv;
using Macromill.QCWeb.Dao.AllCommon.Bhv.Load;
using Macromill.QCWeb.Dao.AllCommon.Bhv.Setup;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Exp;
using Macromill.QCWeb.Dao.BsEntity.Dbm;
using Macromill.QCWeb.Dao.ExDao;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.CBean;


namespace Macromill.QCWeb.Dao.ExBhv {

    [Implementation]
    public partial class TItemInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>利用可能なカラム番号を返す </summary>
        public static readonly String PATH_AvailableNo = "AvailableNo";
        /// <summary>アイテム情報(マトリクスの子アイテム数)を取得する </summary>
        public static readonly String PATH_SelectFindByQCWebID = "SelectFindByQCWebID";
        /// <summary>アイテム名を全角・半角、大文字・小文字を同値として検索するSQL </summary>
        public static readonly String PATH_SelectItemNameCaseNonsensitive = "SelectItemNameCaseNonsensitive";
        /// <summary>アイテム情報、カテゴリ情報、RawDataコントロール情報を取得する </summary>
        public static readonly String PATH_SelectQuestionsCategoryInfo = "SelectQuestionsCategoryInfo";
        /// <summary>データ加工系画面で利用するアイテム一覧情報を取得する </summary>
        public static readonly String PATH_SelectQuestionsForDataProcess = "SelectQuestionsForDataProcess";
        /// <summary>アイテム情報、RawDataコントロール情報を取得する </summary>
        public static readonly String PATH_SelectQuestionsInfo = "SelectQuestionsInfo";
        /// <summary>アイテム情報を取得する </summary>
        public static readonly String PATH_SelectSimpleInfo = "SelectSimpleInfo";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TItemInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TItemInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_ITEM_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TItemInfoDbm.GetInstance(); } }
        public TItemInfoDbm MyDBMeta { get { return TItemInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TItemInfo NewMyEntity() { return new TItemInfo(); }
        public virtual TItemInfoCB NewMyConditionBean() { return new TItemInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TItemInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            return this.DelegateSelectCount(cb);
        }

        protected override int DoReadCount(ConditionBean cb) {
            return SelectCount(Downcast(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                   Entity Select
        //                                                                   =============
        #region Entity Select
        public virtual TItemInfo SelectEntity(TItemInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TItemInfo> ls = null;
            try {
                ls = this.DelegateSelectList(cb);
            } catch (DangerousResultSizeException e) {
                ThrowEntityDuplicatedException("{over safetyMaxResultSize '1'}", cb, e);
                return null; // unreachable
            } finally {
                xrestoreSafetyResult(cb, preSafetyMaxResultSize);
            }
            if (ls.Count == 0) { return null; }
            AssertEntitySelectedAsOne(ls, cb);
            return (TItemInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TItemInfo SelectEntityWithDeletedCheck(TItemInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TItemInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TItemInfo SelectByPKValue(decimal? itemInfoId) {
            return SelectEntity(BuildPKCB(itemInfoId));
        }

        public virtual TItemInfo SelectByPKValueWithDeletedCheck(decimal? itemInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(itemInfoId));
        }

        private TItemInfoCB BuildPKCB(decimal? itemInfoId) {
            AssertObjectNotNull("itemInfoId", itemInfoId);
            TItemInfoCB cb = NewMyConditionBean();
            cb.Query().SetItemInfoId_Equal(itemInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TItemInfo> SelectList(TItemInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TItemInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TItemInfo> SelectPage(TItemInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TItemInfo> invoker = new PagingInvoker<TItemInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TItemInfo> {
            protected TItemInfoBhv _bhv; protected TItemInfoCB _cb;
            public InternalSelectPagingHandler(TItemInfoBhv bhv, TItemInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TItemInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TItemInfo myEntity = (TItemInfo)entity;
            myEntity.ItemInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTCategoryInfoList(TItemInfo tItemInfo, ConditionBeanSetupper<TCategoryInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCategoryInfoList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTCategoryInfoList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TCategoryInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCategoryInfoList(tItemInfoList, new LoadReferrerOption<TCategoryInfoCB, TCategoryInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTCategoryInfoList(TItemInfo tItemInfo, LoadReferrerOption<TCategoryInfoCB, TCategoryInfo> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTCategoryInfoList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTCategoryInfoList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TCategoryInfoCB, TCategoryInfo> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TCategoryInfoBhv referrerBhv = xgetBSFLR().Select<TCategoryInfoBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TCategoryInfoCB, TCategoryInfo>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTCategoryInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTCategoryInfoListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TCategoryInfoCB, TCategoryInfo> {
            protected TCategoryInfoBhv referrerBhv;
            public MyInternalLoadTCategoryInfoListCallback(TCategoryInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TCategoryInfo> ls) { e.TCategoryInfoList = ls; }
            public TCategoryInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TCategoryInfoCB cb, IList<decimal?> ls) { cb.Query().SetItemInfoId_InScope(ls); }
            public void qyOdFKAsc(TCategoryInfoCB cb) { cb.Query().AddOrderBy_ItemInfoId_Asc(); }
            public void spFKCol(TCategoryInfoCB cb) { cb.Specify().ColumnItemInfoId(); }
            public IList<TCategoryInfo> selRfLs(TCategoryInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TCategoryInfo e) { return e.ItemInfoId; }
            public void setlcEt(TCategoryInfo re, TItemInfo be) { re.TItemInfo = be; }
        }
        public virtual void LoadTMatrixInfoByItemInfoIdList(TItemInfo tItemInfo, ConditionBeanSetupper<TMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTMatrixInfoByItemInfoIdList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTMatrixInfoByItemInfoIdList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTMatrixInfoByItemInfoIdList(tItemInfoList, new LoadReferrerOption<TMatrixInfoCB, TMatrixInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTMatrixInfoByItemInfoIdList(TItemInfo tItemInfo, LoadReferrerOption<TMatrixInfoCB, TMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTMatrixInfoByItemInfoIdList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTMatrixInfoByItemInfoIdList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TMatrixInfoCB, TMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TMatrixInfoBhv referrerBhv = xgetBSFLR().Select<TMatrixInfoBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TMatrixInfoCB, TMatrixInfo>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTMatrixInfoByItemInfoIdListCallback(referrerBhv));
        }
        protected class MyInternalLoadTMatrixInfoByItemInfoIdListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TMatrixInfoCB, TMatrixInfo> {
            protected TMatrixInfoBhv referrerBhv;
            public MyInternalLoadTMatrixInfoByItemInfoIdListCallback(TMatrixInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TMatrixInfo> ls) { e.TMatrixInfoByItemInfoIdList = ls; }
            public TMatrixInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TMatrixInfoCB cb, IList<decimal?> ls) { cb.Query().SetItemInfoId_InScope(ls); }
            public void qyOdFKAsc(TMatrixInfoCB cb) { cb.Query().AddOrderBy_ItemInfoId_Asc(); }
            public void spFKCol(TMatrixInfoCB cb) { cb.Specify().ColumnItemInfoId(); }
            public IList<TMatrixInfo> selRfLs(TMatrixInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TMatrixInfo e) { return e.ItemInfoId; }
            public void setlcEt(TMatrixInfo re, TItemInfo be) { re.TItemInfoByItemInfoId = be; }
        }
        public virtual void LoadTMatrixInfoByChildItemInfoIdList(TItemInfo tItemInfo, ConditionBeanSetupper<TMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTMatrixInfoByChildItemInfoIdList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTMatrixInfoByChildItemInfoIdList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTMatrixInfoByChildItemInfoIdList(tItemInfoList, new LoadReferrerOption<TMatrixInfoCB, TMatrixInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTMatrixInfoByChildItemInfoIdList(TItemInfo tItemInfo, LoadReferrerOption<TMatrixInfoCB, TMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTMatrixInfoByChildItemInfoIdList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTMatrixInfoByChildItemInfoIdList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TMatrixInfoCB, TMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TMatrixInfoBhv referrerBhv = xgetBSFLR().Select<TMatrixInfoBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TMatrixInfoCB, TMatrixInfo>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTMatrixInfoByChildItemInfoIdListCallback(referrerBhv));
        }
        protected class MyInternalLoadTMatrixInfoByChildItemInfoIdListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TMatrixInfoCB, TMatrixInfo> {
            protected TMatrixInfoBhv referrerBhv;
            public MyInternalLoadTMatrixInfoByChildItemInfoIdListCallback(TMatrixInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TMatrixInfo> ls) { e.TMatrixInfoByChildItemInfoIdList = ls; }
            public TMatrixInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TMatrixInfoCB cb, IList<decimal?> ls) { cb.Query().SetChildItemInfoId_InScope(ls); }
            public void qyOdFKAsc(TMatrixInfoCB cb) { cb.Query().AddOrderBy_ChildItemInfoId_Asc(); }
            public void spFKCol(TMatrixInfoCB cb) { cb.Specify().ColumnChildItemInfoId(); }
            public IList<TMatrixInfo> selRfLs(TMatrixInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TMatrixInfo e) { return e.ChildItemInfoId; }
            public void setlcEt(TMatrixInfo re, TItemInfo be) { re.TItemInfoByChildItemInfoId = be; }
        }
        public virtual void LoadTScenarioQuerylistList(TItemInfo tItemInfo, ConditionBeanSetupper<TScenarioQuerylistCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioQuerylistList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTScenarioQuerylistList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TScenarioQuerylistCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioQuerylistList(tItemInfoList, new LoadReferrerOption<TScenarioQuerylistCB, TScenarioQuerylist>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTScenarioQuerylistList(TItemInfo tItemInfo, LoadReferrerOption<TScenarioQuerylistCB, TScenarioQuerylist> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTScenarioQuerylistList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTScenarioQuerylistList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TScenarioQuerylistCB, TScenarioQuerylist> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TScenarioQuerylistBhv referrerBhv = xgetBSFLR().Select<TScenarioQuerylistBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TScenarioQuerylistCB, TScenarioQuerylist>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTScenarioQuerylistListCallback(referrerBhv));
        }
        protected class MyInternalLoadTScenarioQuerylistListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TScenarioQuerylistCB, TScenarioQuerylist> {
            protected TScenarioQuerylistBhv referrerBhv;
            public MyInternalLoadTScenarioQuerylistListCallback(TScenarioQuerylistBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TScenarioQuerylist> ls) { e.TScenarioQuerylistList = ls; }
            public TScenarioQuerylistCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TScenarioQuerylistCB cb, IList<decimal?> ls) { cb.Query().SetItemInfoId_InScope(ls); }
            public void qyOdFKAsc(TScenarioQuerylistCB cb) { cb.Query().AddOrderBy_ItemInfoId_Asc(); }
            public void spFKCol(TScenarioQuerylistCB cb) { cb.Specify().ColumnItemInfoId(); }
            public IList<TScenarioQuerylist> selRfLs(TScenarioQuerylistCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TScenarioQuerylist e) { return e.ItemInfoId; }
            public void setlcEt(TScenarioQuerylist re, TItemInfo be) { re.TItemInfo = be; }
        }
        public virtual void LoadTGtScenarioItemList(TItemInfo tItemInfo, ConditionBeanSetupper<TGtScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtScenarioItemList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTGtScenarioItemList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TGtScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtScenarioItemList(tItemInfoList, new LoadReferrerOption<TGtScenarioItemCB, TGtScenarioItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTGtScenarioItemList(TItemInfo tItemInfo, LoadReferrerOption<TGtScenarioItemCB, TGtScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTGtScenarioItemList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTGtScenarioItemList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TGtScenarioItemCB, TGtScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TGtScenarioItemBhv referrerBhv = xgetBSFLR().Select<TGtScenarioItemBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TGtScenarioItemCB, TGtScenarioItem>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTGtScenarioItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTGtScenarioItemListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TGtScenarioItemCB, TGtScenarioItem> {
            protected TGtScenarioItemBhv referrerBhv;
            public MyInternalLoadTGtScenarioItemListCallback(TGtScenarioItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TGtScenarioItem> ls) { e.TGtScenarioItemList = ls; }
            public TGtScenarioItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TGtScenarioItemCB cb, IList<decimal?> ls) { cb.Query().SetItemInfoId_InScope(ls); }
            public void qyOdFKAsc(TGtScenarioItemCB cb) { cb.Query().AddOrderBy_ItemInfoId_Asc(); }
            public void spFKCol(TGtScenarioItemCB cb) { cb.Specify().ColumnItemInfoId(); }
            public IList<TGtScenarioItem> selRfLs(TGtScenarioItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TGtScenarioItem e) { return e.ItemInfoId; }
            public void setlcEt(TGtScenarioItem re, TItemInfo be) { re.TItemInfo = be; }
        }
        public virtual void LoadTFaScenarioItemList(TItemInfo tItemInfo, ConditionBeanSetupper<TFaScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaScenarioItemList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTFaScenarioItemList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TFaScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaScenarioItemList(tItemInfoList, new LoadReferrerOption<TFaScenarioItemCB, TFaScenarioItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTFaScenarioItemList(TItemInfo tItemInfo, LoadReferrerOption<TFaScenarioItemCB, TFaScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTFaScenarioItemList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTFaScenarioItemList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TFaScenarioItemCB, TFaScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TFaScenarioItemBhv referrerBhv = xgetBSFLR().Select<TFaScenarioItemBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TFaScenarioItemCB, TFaScenarioItem>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTFaScenarioItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTFaScenarioItemListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TFaScenarioItemCB, TFaScenarioItem> {
            protected TFaScenarioItemBhv referrerBhv;
            public MyInternalLoadTFaScenarioItemListCallback(TFaScenarioItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TFaScenarioItem> ls) { e.TFaScenarioItemList = ls; }
            public TFaScenarioItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TFaScenarioItemCB cb, IList<decimal?> ls) { cb.Query().SetFaTargetItemId_InScope(ls); }
            public void qyOdFKAsc(TFaScenarioItemCB cb) { cb.Query().AddOrderBy_FaTargetItemId_Asc(); }
            public void spFKCol(TFaScenarioItemCB cb) { cb.Specify().ColumnFaTargetItemId(); }
            public IList<TFaScenarioItem> selRfLs(TFaScenarioItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TFaScenarioItem e) { return e.FaTargetItemId; }
            public void setlcEt(TFaScenarioItem re, TItemInfo be) { re.TItemInfo = be; }
        }
        public virtual void LoadTFaListAddItemList(TItemInfo tItemInfo, ConditionBeanSetupper<TFaListAddItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaListAddItemList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTFaListAddItemList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TFaListAddItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaListAddItemList(tItemInfoList, new LoadReferrerOption<TFaListAddItemCB, TFaListAddItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTFaListAddItemList(TItemInfo tItemInfo, LoadReferrerOption<TFaListAddItemCB, TFaListAddItem> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTFaListAddItemList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTFaListAddItemList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TFaListAddItemCB, TFaListAddItem> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TFaListAddItemBhv referrerBhv = xgetBSFLR().Select<TFaListAddItemBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TFaListAddItemCB, TFaListAddItem>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTFaListAddItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTFaListAddItemListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TFaListAddItemCB, TFaListAddItem> {
            protected TFaListAddItemBhv referrerBhv;
            public MyInternalLoadTFaListAddItemListCallback(TFaListAddItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TFaListAddItem> ls) { e.TFaListAddItemList = ls; }
            public TFaListAddItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TFaListAddItemCB cb, IList<decimal?> ls) { cb.Query().SetItemInfoId_InScope(ls); }
            public void qyOdFKAsc(TFaListAddItemCB cb) { cb.Query().AddOrderBy_ItemInfoId_Asc(); }
            public void spFKCol(TFaListAddItemCB cb) { cb.Specify().ColumnItemInfoId(); }
            public IList<TFaListAddItem> selRfLs(TFaListAddItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TFaListAddItem e) { return e.ItemInfoId; }
            public void setlcEt(TFaListAddItem re, TItemInfo be) { re.TItemInfo = be; }
        }
        public virtual void LoadTGtMatrixChildList(TItemInfo tItemInfo, ConditionBeanSetupper<TGtMatrixChildCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtMatrixChildList(xnewLRLs<TItemInfo>(tItemInfo), conditionBeanSetupper);
        }
        public virtual void LoadTGtMatrixChildList(IList<TItemInfo> tItemInfoList, ConditionBeanSetupper<TGtMatrixChildCB> conditionBeanSetupper) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtMatrixChildList(tItemInfoList, new LoadReferrerOption<TGtMatrixChildCB, TGtMatrixChild>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTGtMatrixChildList(TItemInfo tItemInfo, LoadReferrerOption<TGtMatrixChildCB, TGtMatrixChild> loadReferrerOption) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTGtMatrixChildList(xnewLRLs<TItemInfo>(tItemInfo), loadReferrerOption);
        }
        public virtual void LoadTGtMatrixChildList(IList<TItemInfo> tItemInfoList, LoadReferrerOption<TGtMatrixChildCB, TGtMatrixChild> loadReferrerOption) {
            AssertObjectNotNull("tItemInfoList", tItemInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tItemInfoList.Count == 0) { return; }
            TGtMatrixChildBhv referrerBhv = xgetBSFLR().Select<TGtMatrixChildBhv>();
            HelpLoadReferrerInternally<TItemInfo, decimal?, TGtMatrixChildCB, TGtMatrixChild>
                    (tItemInfoList, loadReferrerOption, new MyInternalLoadTGtMatrixChildListCallback(referrerBhv));
        }
        protected class MyInternalLoadTGtMatrixChildListCallback : InternalLoadReferrerCallback<TItemInfo, decimal?, TGtMatrixChildCB, TGtMatrixChild> {
            protected TGtMatrixChildBhv referrerBhv;
            public MyInternalLoadTGtMatrixChildListCallback(TGtMatrixChildBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TItemInfo e) { return e.ItemInfoId; }
            public void setRfLs(TItemInfo e, IList<TGtMatrixChild> ls) { e.TGtMatrixChildList = ls; }
            public TGtMatrixChildCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TGtMatrixChildCB cb, IList<decimal?> ls) { cb.Query().SetChildItemId_InScope(ls); }
            public void qyOdFKAsc(TGtMatrixChildCB cb) { cb.Query().AddOrderBy_ChildItemId_Asc(); }
            public void spFKCol(TGtMatrixChildCB cb) { cb.Specify().ColumnChildItemId(); }
            public IList<TGtMatrixChild> selRfLs(TGtMatrixChildCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TGtMatrixChild e) { return e.ChildItemId; }
            public void setlcEt(TGtMatrixChild re, TItemInfo be) { re.TItemInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TQcwebSurveyInfo>(tItemInfoList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TItemInfo, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TItemInfo entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TMatrixInfo> PulloutTMatrixInfo(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TMatrixInfo>(tItemInfoList, new MyInternalPulloutTMatrixInfoCallback());
        }
        protected class MyInternalPulloutTMatrixInfoCallback : InternalPulloutCallback<TItemInfo, TMatrixInfo> {
            public TMatrixInfo getFr(TItemInfo entity) { return entity.TMatrixInfo; }
        }
        public IList<TFaListAddItem> PulloutTFaListAddItem(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TFaListAddItem>(tItemInfoList, new MyInternalPulloutTFaListAddItemCallback());
        }
        protected class MyInternalPulloutTFaListAddItemCallback : InternalPulloutCallback<TItemInfo, TFaListAddItem> {
            public TFaListAddItem getFr(TItemInfo entity) { return entity.TFaListAddItem; }
        }
        public IList<TFaScenarioItem> PulloutTFaScenarioItem(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TFaScenarioItem>(tItemInfoList, new MyInternalPulloutTFaScenarioItemCallback());
        }
        protected class MyInternalPulloutTFaScenarioItemCallback : InternalPulloutCallback<TItemInfo, TFaScenarioItem> {
            public TFaScenarioItem getFr(TItemInfo entity) { return entity.TFaScenarioItem; }
        }
        public IList<TTableControl> PulloutTTableControl(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TTableControl>(tItemInfoList, new MyInternalPulloutTTableControlCallback());
        }
        protected class MyInternalPulloutTTableControlCallback : InternalPulloutCallback<TItemInfo, TTableControl> {
            public TTableControl getFr(TItemInfo entity) { return entity.TTableControl; }
        }
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TScenarioTotalization>(tItemInfoList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TItemInfo, TScenarioTotalization> {
            public TScenarioTotalization getFr(TItemInfo entity) { return entity.TScenarioTotalization; }
        }
        public IList<TDataEditList> PulloutTDataEditList(IList<TItemInfo> tItemInfoList) {
            return HelpPulloutInternally<TItemInfo, TDataEditList>(tItemInfoList, new MyInternalPulloutTDataEditListCallback());
        }
        protected class MyInternalPulloutTDataEditListCallback : InternalPulloutCallback<TItemInfo, TDataEditList> {
            public TDataEditList getFr(TItemInfo entity) { return entity.TDataEditList; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TItemInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TItemInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TItemInfo entity) {
            HelpInsertOrUpdateInternally<TItemInfo, TItemInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TItemInfo, TItemInfoCB> {
            protected TItemInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TItemInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TItemInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TItemInfo entity) { _bhv.Update(entity); }
            public TItemInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TItemInfoCB cb, TItemInfo entity) {
                cb.Query().SetItemInfoId_Equal(entity.ItemInfoId);
            }
            public int CallbackSelectCount(TItemInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TItemInfo entity) {
            HelpDeleteInternally<TItemInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TItemInfo> {
            protected TItemInfoBhv _bhv;
            public MyInternalDeleteCallback(TItemInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TItemInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TItemInfo tItemInfo, TItemInfoCB cb) {
            AssertObjectNotNull("tItemInfo", tItemInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tItemInfo);
            FilterEntityOfUpdate(tItemInfo); AssertEntityOfUpdate(tItemInfo);
            return this.Dao.UpdateByQuery(cb, tItemInfo);
        }

        public int QueryDelete(TItemInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return this.Dao.DeleteByQuery(cb);
        }

        // ===============================================================================
        //                                                            Optimistic Lock Info
        //                                                            ====================
        protected override bool HasVersionNoValue(Entity entity) {
            return false;
        }

        protected override bool HasUpdateDateValue(Entity entity) {
            return false;
        }

        // ===============================================================================
        //                                                                 Delegate Method
        //                                                                 ===============
        #region Delegate Method
        protected int DelegateSelectCount(TItemInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TItemInfo> DelegateSelectList(TItemInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TItemInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TItemInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TItemInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TItemInfo Downcast(Entity entity) {
            return (TItemInfo)entity;
        }

        protected TItemInfoCB Downcast(ConditionBean cb) {
            return (TItemInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TItemInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
