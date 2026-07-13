
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
    public partial class TGtScenarioItemBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>GT シナリオアイテムテーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TGtScenarioItemDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TGtScenarioItemBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TGtScenarioItemDbm.GetInstance(); } }
        public TGtScenarioItemDbm MyDBMeta { get { return TGtScenarioItemDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TGtScenarioItem NewMyEntity() { return new TGtScenarioItem(); }
        public virtual TGtScenarioItemCB NewMyConditionBean() { return new TGtScenarioItemCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TGtScenarioItemCB cb) {
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
        public virtual TGtScenarioItem SelectEntity(TGtScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TGtScenarioItem> ls = null;
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
            return (TGtScenarioItem)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TGtScenarioItem SelectEntityWithDeletedCheck(TGtScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TGtScenarioItem entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TGtScenarioItem SelectByPKValue(decimal? gtScenarioItemId) {
            return SelectEntity(BuildPKCB(gtScenarioItemId));
        }

        public virtual TGtScenarioItem SelectByPKValueWithDeletedCheck(decimal? gtScenarioItemId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(gtScenarioItemId));
        }

        private TGtScenarioItemCB BuildPKCB(decimal? gtScenarioItemId) {
            AssertObjectNotNull("gtScenarioItemId", gtScenarioItemId);
            TGtScenarioItemCB cb = NewMyConditionBean();
            cb.Query().SetGtScenarioItemId_Equal(gtScenarioItemId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TGtScenarioItem> SelectList(TGtScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TGtScenarioItem>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TGtScenarioItem> SelectPage(TGtScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TGtScenarioItem> invoker = new PagingInvoker<TGtScenarioItem>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TGtScenarioItem> {
            protected TGtScenarioItemBhv _bhv; protected TGtScenarioItemCB _cb;
            public InternalSelectPagingHandler(TGtScenarioItemBhv bhv, TGtScenarioItemCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TGtScenarioItem> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TGtScenarioItem myEntity = (TGtScenarioItem)entity;
            myEntity.GtScenarioItemId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTColorSetInfoGtList(TGtScenarioItem tGtScenarioItem, ConditionBeanSetupper<TColorSetInfoGtCB> conditionBeanSetupper) {
            AssertObjectNotNull("tGtScenarioItem", tGtScenarioItem); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorSetInfoGtList(xnewLRLs<TGtScenarioItem>(tGtScenarioItem), conditionBeanSetupper);
        }
        public virtual void LoadTColorSetInfoGtList(IList<TGtScenarioItem> tGtScenarioItemList, ConditionBeanSetupper<TColorSetInfoGtCB> conditionBeanSetupper) {
            AssertObjectNotNull("tGtScenarioItemList", tGtScenarioItemList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorSetInfoGtList(tGtScenarioItemList, new LoadReferrerOption<TColorSetInfoGtCB, TColorSetInfoGt>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTColorSetInfoGtList(TGtScenarioItem tGtScenarioItem, LoadReferrerOption<TColorSetInfoGtCB, TColorSetInfoGt> loadReferrerOption) {
            AssertObjectNotNull("tGtScenarioItem", tGtScenarioItem); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTColorSetInfoGtList(xnewLRLs<TGtScenarioItem>(tGtScenarioItem), loadReferrerOption);
        }
        public virtual void LoadTColorSetInfoGtList(IList<TGtScenarioItem> tGtScenarioItemList, LoadReferrerOption<TColorSetInfoGtCB, TColorSetInfoGt> loadReferrerOption) {
            AssertObjectNotNull("tGtScenarioItemList", tGtScenarioItemList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tGtScenarioItemList.Count == 0) { return; }
            TColorSetInfoGtBhv referrerBhv = xgetBSFLR().Select<TColorSetInfoGtBhv>();
            HelpLoadReferrerInternally<TGtScenarioItem, decimal?, TColorSetInfoGtCB, TColorSetInfoGt>
                    (tGtScenarioItemList, loadReferrerOption, new MyInternalLoadTColorSetInfoGtListCallback(referrerBhv));
        }
        protected class MyInternalLoadTColorSetInfoGtListCallback : InternalLoadReferrerCallback<TGtScenarioItem, decimal?, TColorSetInfoGtCB, TColorSetInfoGt> {
            protected TColorSetInfoGtBhv referrerBhv;
            public MyInternalLoadTColorSetInfoGtListCallback(TColorSetInfoGtBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TGtScenarioItem e) { return e.GtScenarioItemId; }
            public void setRfLs(TGtScenarioItem e, IList<TColorSetInfoGt> ls) { e.TColorSetInfoGtList = ls; }
            public TColorSetInfoGtCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TColorSetInfoGtCB cb, IList<decimal?> ls) { cb.Query().SetGtScenarioItemId_InScope(ls); }
            public void qyOdFKAsc(TColorSetInfoGtCB cb) { cb.Query().AddOrderBy_GtScenarioItemId_Asc(); }
            public void spFKCol(TColorSetInfoGtCB cb) { cb.Specify().ColumnGtScenarioItemId(); }
            public IList<TColorSetInfoGt> selRfLs(TColorSetInfoGtCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TColorSetInfoGt e) { return e.GtScenarioItemId; }
            public void setlcEt(TColorSetInfoGt re, TGtScenarioItem be) { re.TGtScenarioItem = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TGtScenarioItem> tGtScenarioItemList) {
            return HelpPulloutInternally<TGtScenarioItem, TScenarioTotalization>(tGtScenarioItemList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TGtScenarioItem, TScenarioTotalization> {
            public TScenarioTotalization getFr(TGtScenarioItem entity) { return entity.TScenarioTotalization; }
        }
        public IList<TItemInfo> PulloutTItemInfo(IList<TGtScenarioItem> tGtScenarioItemList) {
            return HelpPulloutInternally<TGtScenarioItem, TItemInfo>(tGtScenarioItemList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TGtScenarioItem, TItemInfo> {
            public TItemInfo getFr(TGtScenarioItem entity) { return entity.TItemInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TGtScenarioItem entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TGtScenarioItem entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TGtScenarioItem entity) {
            HelpInsertOrUpdateInternally<TGtScenarioItem, TGtScenarioItemCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TGtScenarioItem, TGtScenarioItemCB> {
            protected TGtScenarioItemBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TGtScenarioItemBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TGtScenarioItem entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TGtScenarioItem entity) { _bhv.Update(entity); }
            public TGtScenarioItemCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TGtScenarioItemCB cb, TGtScenarioItem entity) {
                cb.Query().SetGtScenarioItemId_Equal(entity.GtScenarioItemId);
            }
            public int CallbackSelectCount(TGtScenarioItemCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TGtScenarioItem entity) {
            HelpDeleteInternally<TGtScenarioItem>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TGtScenarioItem> {
            protected TGtScenarioItemBhv _bhv;
            public MyInternalDeleteCallback(TGtScenarioItemBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TGtScenarioItem entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TGtScenarioItem tGtScenarioItem, TGtScenarioItemCB cb) {
            AssertObjectNotNull("tGtScenarioItem", tGtScenarioItem); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tGtScenarioItem);
            FilterEntityOfUpdate(tGtScenarioItem); AssertEntityOfUpdate(tGtScenarioItem);
            return this.Dao.UpdateByQuery(cb, tGtScenarioItem);
        }

        public int QueryDelete(TGtScenarioItemCB cb) {
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
        protected int DelegateSelectCount(TGtScenarioItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TGtScenarioItem> DelegateSelectList(TGtScenarioItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TGtScenarioItem e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TGtScenarioItem e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TGtScenarioItem e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TGtScenarioItem Downcast(Entity entity) {
            return (TGtScenarioItem)entity;
        }

        protected TGtScenarioItemCB Downcast(ConditionBean cb) {
            return (TGtScenarioItemCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TGtScenarioItemDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
