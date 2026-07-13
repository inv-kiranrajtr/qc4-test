
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
    public partial class TDataProcessNewItemBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataProcessNewItemDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataProcessNewItemBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDataProcessNewItemDbm.GetInstance(); } }
        public TDataProcessNewItemDbm MyDBMeta { get { return TDataProcessNewItemDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDataProcessNewItem NewMyEntity() { return new TDataProcessNewItem(); }
        public virtual TDataProcessNewItemCB NewMyConditionBean() { return new TDataProcessNewItemCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDataProcessNewItemCB cb) {
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
        public virtual TDataProcessNewItem SelectEntity(TDataProcessNewItemCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDataProcessNewItem> ls = null;
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
            return (TDataProcessNewItem)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDataProcessNewItem SelectEntityWithDeletedCheck(TDataProcessNewItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDataProcessNewItem entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDataProcessNewItem SelectByPKValue(decimal? dataEditId) {
            return SelectEntity(BuildPKCB(dataEditId));
        }

        public virtual TDataProcessNewItem SelectByPKValueWithDeletedCheck(decimal? dataEditId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(dataEditId));
        }

        private TDataProcessNewItemCB BuildPKCB(decimal? dataEditId) {
            AssertObjectNotNull("dataEditId", dataEditId);
            TDataProcessNewItemCB cb = NewMyConditionBean();
            cb.Query().SetDataEditId_Equal(dataEditId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDataProcessNewItem> SelectList(TDataProcessNewItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDataProcessNewItem>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDataProcessNewItem> SelectPage(TDataProcessNewItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDataProcessNewItem> invoker = new PagingInvoker<TDataProcessNewItem>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDataProcessNewItem> {
            protected TDataProcessNewItemBhv _bhv; protected TDataProcessNewItemCB _cb;
            public InternalSelectPagingHandler(TDataProcessNewItemBhv bhv, TDataProcessNewItemCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDataProcessNewItem> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDataProcessNewCategoryList(TDataProcessNewItem tDataProcessNewItem, ConditionBeanSetupper<TDataProcessNewCategoryCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataProcessNewCategoryList(xnewLRLs<TDataProcessNewItem>(tDataProcessNewItem), conditionBeanSetupper);
        }
        public virtual void LoadTDataProcessNewCategoryList(IList<TDataProcessNewItem> tDataProcessNewItemList, ConditionBeanSetupper<TDataProcessNewCategoryCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataProcessNewItemList", tDataProcessNewItemList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataProcessNewCategoryList(tDataProcessNewItemList, new LoadReferrerOption<TDataProcessNewCategoryCB, TDataProcessNewCategory>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDataProcessNewCategoryList(TDataProcessNewItem tDataProcessNewItem, LoadReferrerOption<TDataProcessNewCategoryCB, TDataProcessNewCategory> loadReferrerOption) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDataProcessNewCategoryList(xnewLRLs<TDataProcessNewItem>(tDataProcessNewItem), loadReferrerOption);
        }
        public virtual void LoadTDataProcessNewCategoryList(IList<TDataProcessNewItem> tDataProcessNewItemList, LoadReferrerOption<TDataProcessNewCategoryCB, TDataProcessNewCategory> loadReferrerOption) {
            AssertObjectNotNull("tDataProcessNewItemList", tDataProcessNewItemList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDataProcessNewItemList.Count == 0) { return; }
            TDataProcessNewCategoryBhv referrerBhv = xgetBSFLR().Select<TDataProcessNewCategoryBhv>();
            HelpLoadReferrerInternally<TDataProcessNewItem, decimal?, TDataProcessNewCategoryCB, TDataProcessNewCategory>
                    (tDataProcessNewItemList, loadReferrerOption, new MyInternalLoadTDataProcessNewCategoryListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDataProcessNewCategoryListCallback : InternalLoadReferrerCallback<TDataProcessNewItem, decimal?, TDataProcessNewCategoryCB, TDataProcessNewCategory> {
            protected TDataProcessNewCategoryBhv referrerBhv;
            public MyInternalLoadTDataProcessNewCategoryListCallback(TDataProcessNewCategoryBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDataProcessNewItem e) { return e.DataEditId; }
            public void setRfLs(TDataProcessNewItem e, IList<TDataProcessNewCategory> ls) { e.TDataProcessNewCategoryList = ls; }
            public TDataProcessNewCategoryCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDataProcessNewCategoryCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TDataProcessNewCategoryCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TDataProcessNewCategoryCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TDataProcessNewCategory> selRfLs(TDataProcessNewCategoryCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDataProcessNewCategory e) { return e.DataEditId; }
            public void setlcEt(TDataProcessNewCategory re, TDataProcessNewItem be) { re.TDataProcessNewItem = be; }
        }
        public virtual void LoadTDataProcessNewItemSrcList(TDataProcessNewItem tDataProcessNewItem, ConditionBeanSetupper<TDataProcessNewItemSrcCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataProcessNewItemSrcList(xnewLRLs<TDataProcessNewItem>(tDataProcessNewItem), conditionBeanSetupper);
        }
        public virtual void LoadTDataProcessNewItemSrcList(IList<TDataProcessNewItem> tDataProcessNewItemList, ConditionBeanSetupper<TDataProcessNewItemSrcCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataProcessNewItemList", tDataProcessNewItemList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataProcessNewItemSrcList(tDataProcessNewItemList, new LoadReferrerOption<TDataProcessNewItemSrcCB, TDataProcessNewItemSrc>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDataProcessNewItemSrcList(TDataProcessNewItem tDataProcessNewItem, LoadReferrerOption<TDataProcessNewItemSrcCB, TDataProcessNewItemSrc> loadReferrerOption) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDataProcessNewItemSrcList(xnewLRLs<TDataProcessNewItem>(tDataProcessNewItem), loadReferrerOption);
        }
        public virtual void LoadTDataProcessNewItemSrcList(IList<TDataProcessNewItem> tDataProcessNewItemList, LoadReferrerOption<TDataProcessNewItemSrcCB, TDataProcessNewItemSrc> loadReferrerOption) {
            AssertObjectNotNull("tDataProcessNewItemList", tDataProcessNewItemList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDataProcessNewItemList.Count == 0) { return; }
            TDataProcessNewItemSrcBhv referrerBhv = xgetBSFLR().Select<TDataProcessNewItemSrcBhv>();
            HelpLoadReferrerInternally<TDataProcessNewItem, decimal?, TDataProcessNewItemSrcCB, TDataProcessNewItemSrc>
                    (tDataProcessNewItemList, loadReferrerOption, new MyInternalLoadTDataProcessNewItemSrcListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDataProcessNewItemSrcListCallback : InternalLoadReferrerCallback<TDataProcessNewItem, decimal?, TDataProcessNewItemSrcCB, TDataProcessNewItemSrc> {
            protected TDataProcessNewItemSrcBhv referrerBhv;
            public MyInternalLoadTDataProcessNewItemSrcListCallback(TDataProcessNewItemSrcBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDataProcessNewItem e) { return e.DataEditId; }
            public void setRfLs(TDataProcessNewItem e, IList<TDataProcessNewItemSrc> ls) { e.TDataProcessNewItemSrcList = ls; }
            public TDataProcessNewItemSrcCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDataProcessNewItemSrcCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TDataProcessNewItemSrcCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TDataProcessNewItemSrcCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TDataProcessNewItemSrc> selRfLs(TDataProcessNewItemSrcCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDataProcessNewItemSrc e) { return e.DataEditId; }
            public void setlcEt(TDataProcessNewItemSrc re, TDataProcessNewItem be) { re.TDataProcessNewItem = be; }
        }
        public virtual void LoadTIntegConditionList(TDataProcessNewItem tDataProcessNewItem, ConditionBeanSetupper<TIntegConditionCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTIntegConditionList(xnewLRLs<TDataProcessNewItem>(tDataProcessNewItem), conditionBeanSetupper);
        }
        public virtual void LoadTIntegConditionList(IList<TDataProcessNewItem> tDataProcessNewItemList, ConditionBeanSetupper<TIntegConditionCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataProcessNewItemList", tDataProcessNewItemList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTIntegConditionList(tDataProcessNewItemList, new LoadReferrerOption<TIntegConditionCB, TIntegCondition>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTIntegConditionList(TDataProcessNewItem tDataProcessNewItem, LoadReferrerOption<TIntegConditionCB, TIntegCondition> loadReferrerOption) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTIntegConditionList(xnewLRLs<TDataProcessNewItem>(tDataProcessNewItem), loadReferrerOption);
        }
        public virtual void LoadTIntegConditionList(IList<TDataProcessNewItem> tDataProcessNewItemList, LoadReferrerOption<TIntegConditionCB, TIntegCondition> loadReferrerOption) {
            AssertObjectNotNull("tDataProcessNewItemList", tDataProcessNewItemList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDataProcessNewItemList.Count == 0) { return; }
            TIntegConditionBhv referrerBhv = xgetBSFLR().Select<TIntegConditionBhv>();
            HelpLoadReferrerInternally<TDataProcessNewItem, decimal?, TIntegConditionCB, TIntegCondition>
                    (tDataProcessNewItemList, loadReferrerOption, new MyInternalLoadTIntegConditionListCallback(referrerBhv));
        }
        protected class MyInternalLoadTIntegConditionListCallback : InternalLoadReferrerCallback<TDataProcessNewItem, decimal?, TIntegConditionCB, TIntegCondition> {
            protected TIntegConditionBhv referrerBhv;
            public MyInternalLoadTIntegConditionListCallback(TIntegConditionBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDataProcessNewItem e) { return e.DataEditId; }
            public void setRfLs(TDataProcessNewItem e, IList<TIntegCondition> ls) { e.TIntegConditionList = ls; }
            public TIntegConditionCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TIntegConditionCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TIntegConditionCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TIntegConditionCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TIntegCondition> selRfLs(TIntegConditionCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TIntegCondition e) { return e.DataEditId; }
            public void setlcEt(TIntegCondition re, TDataProcessNewItem be) { re.TDataProcessNewItem = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TDataEditList> PulloutTDataEditList(IList<TDataProcessNewItem> tDataProcessNewItemList) {
            return HelpPulloutInternally<TDataProcessNewItem, TDataEditList>(tDataProcessNewItemList, new MyInternalPulloutTDataEditListCallback());
        }
        protected class MyInternalPulloutTDataEditListCallback : InternalPulloutCallback<TDataProcessNewItem, TDataEditList> {
            public TDataEditList getFr(TDataProcessNewItem entity) { return entity.TDataEditList; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDataProcessNewItem entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDataProcessNewItem entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDataProcessNewItem entity) {
            HelpInsertOrUpdateInternally<TDataProcessNewItem, TDataProcessNewItemCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDataProcessNewItem, TDataProcessNewItemCB> {
            protected TDataProcessNewItemBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDataProcessNewItemBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDataProcessNewItem entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDataProcessNewItem entity) { _bhv.Update(entity); }
            public TDataProcessNewItemCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDataProcessNewItemCB cb, TDataProcessNewItem entity) {
                cb.Query().SetDataEditId_Equal(entity.DataEditId);
            }
            public int CallbackSelectCount(TDataProcessNewItemCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDataProcessNewItem entity) {
            HelpDeleteInternally<TDataProcessNewItem>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDataProcessNewItem> {
            protected TDataProcessNewItemBhv _bhv;
            public MyInternalDeleteCallback(TDataProcessNewItemBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDataProcessNewItem entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDataProcessNewItem tDataProcessNewItem, TDataProcessNewItemCB cb) {
            AssertObjectNotNull("tDataProcessNewItem", tDataProcessNewItem); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDataProcessNewItem);
            FilterEntityOfUpdate(tDataProcessNewItem); AssertEntityOfUpdate(tDataProcessNewItem);
            return this.Dao.UpdateByQuery(cb, tDataProcessNewItem);
        }

        public int QueryDelete(TDataProcessNewItemCB cb) {
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
        protected int DelegateSelectCount(TDataProcessNewItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDataProcessNewItem> DelegateSelectList(TDataProcessNewItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TDataProcessNewItem e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDataProcessNewItem e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDataProcessNewItem e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDataProcessNewItem Downcast(Entity entity) {
            return (TDataProcessNewItem)entity;
        }

        protected TDataProcessNewItemCB Downcast(ConditionBean cb) {
            return (TDataProcessNewItemCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDataProcessNewItemDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
