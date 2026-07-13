
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
    public partial class TCrossScenarioItemBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCrossScenarioItemDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCrossScenarioItemBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CROSS_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TCrossScenarioItemDbm.GetInstance(); } }
        public TCrossScenarioItemDbm MyDBMeta { get { return TCrossScenarioItemDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TCrossScenarioItem NewMyEntity() { return new TCrossScenarioItem(); }
        public virtual TCrossScenarioItemCB NewMyConditionBean() { return new TCrossScenarioItemCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TCrossScenarioItemCB cb) {
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
        public virtual TCrossScenarioItem SelectEntity(TCrossScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TCrossScenarioItem> ls = null;
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
            return (TCrossScenarioItem)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TCrossScenarioItem SelectEntityWithDeletedCheck(TCrossScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TCrossScenarioItem entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TCrossScenarioItem SelectByPKValue(decimal? crossScenarioItemId) {
            return SelectEntity(BuildPKCB(crossScenarioItemId));
        }

        public virtual TCrossScenarioItem SelectByPKValueWithDeletedCheck(decimal? crossScenarioItemId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(crossScenarioItemId));
        }

        private TCrossScenarioItemCB BuildPKCB(decimal? crossScenarioItemId) {
            AssertObjectNotNull("crossScenarioItemId", crossScenarioItemId);
            TCrossScenarioItemCB cb = NewMyConditionBean();
            cb.Query().SetCrossScenarioItemId_Equal(crossScenarioItemId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TCrossScenarioItem> SelectList(TCrossScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TCrossScenarioItem>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TCrossScenarioItem> SelectPage(TCrossScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TCrossScenarioItem> invoker = new PagingInvoker<TCrossScenarioItem>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TCrossScenarioItem> {
            protected TCrossScenarioItemBhv _bhv; protected TCrossScenarioItemCB _cb;
            public InternalSelectPagingHandler(TCrossScenarioItemBhv bhv, TCrossScenarioItemCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TCrossScenarioItem> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TCrossScenarioItem myEntity = (TCrossScenarioItem)entity;
            myEntity.CrossScenarioItemId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTPolylineCategoryListList(TCrossScenarioItem tCrossScenarioItem, ConditionBeanSetupper<TPolylineCategoryListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCrossScenarioItem", tCrossScenarioItem); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTPolylineCategoryListList(xnewLRLs<TCrossScenarioItem>(tCrossScenarioItem), conditionBeanSetupper);
        }
        public virtual void LoadTPolylineCategoryListList(IList<TCrossScenarioItem> tCrossScenarioItemList, ConditionBeanSetupper<TPolylineCategoryListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCrossScenarioItemList", tCrossScenarioItemList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTPolylineCategoryListList(tCrossScenarioItemList, new LoadReferrerOption<TPolylineCategoryListCB, TPolylineCategoryList>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTPolylineCategoryListList(TCrossScenarioItem tCrossScenarioItem, LoadReferrerOption<TPolylineCategoryListCB, TPolylineCategoryList> loadReferrerOption) {
            AssertObjectNotNull("tCrossScenarioItem", tCrossScenarioItem); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTPolylineCategoryListList(xnewLRLs<TCrossScenarioItem>(tCrossScenarioItem), loadReferrerOption);
        }
        public virtual void LoadTPolylineCategoryListList(IList<TCrossScenarioItem> tCrossScenarioItemList, LoadReferrerOption<TPolylineCategoryListCB, TPolylineCategoryList> loadReferrerOption) {
            AssertObjectNotNull("tCrossScenarioItemList", tCrossScenarioItemList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tCrossScenarioItemList.Count == 0) { return; }
            TPolylineCategoryListBhv referrerBhv = xgetBSFLR().Select<TPolylineCategoryListBhv>();
            HelpLoadReferrerInternally<TCrossScenarioItem, decimal?, TPolylineCategoryListCB, TPolylineCategoryList>
                    (tCrossScenarioItemList, loadReferrerOption, new MyInternalLoadTPolylineCategoryListListCallback(referrerBhv));
        }
        protected class MyInternalLoadTPolylineCategoryListListCallback : InternalLoadReferrerCallback<TCrossScenarioItem, decimal?, TPolylineCategoryListCB, TPolylineCategoryList> {
            protected TPolylineCategoryListBhv referrerBhv;
            public MyInternalLoadTPolylineCategoryListListCallback(TPolylineCategoryListBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TCrossScenarioItem e) { return e.CrossScenarioItemId; }
            public void setRfLs(TCrossScenarioItem e, IList<TPolylineCategoryList> ls) { e.TPolylineCategoryListList = ls; }
            public TPolylineCategoryListCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TPolylineCategoryListCB cb, IList<decimal?> ls) { cb.Query().SetCrossScenarioItemId_InScope(ls); }
            public void qyOdFKAsc(TPolylineCategoryListCB cb) { cb.Query().AddOrderBy_CrossScenarioItemId_Asc(); }
            public void spFKCol(TPolylineCategoryListCB cb) { cb.Specify().ColumnCrossScenarioItemId(); }
            public IList<TPolylineCategoryList> selRfLs(TPolylineCategoryListCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TPolylineCategoryList e) { return e.CrossScenarioItemId; }
            public void setlcEt(TPolylineCategoryList re, TCrossScenarioItem be) { re.TCrossScenarioItem = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TCrossScenarioTarget> PulloutTCrossScenarioTarget(IList<TCrossScenarioItem> tCrossScenarioItemList) {
            return HelpPulloutInternally<TCrossScenarioItem, TCrossScenarioTarget>(tCrossScenarioItemList, new MyInternalPulloutTCrossScenarioTargetCallback());
        }
        protected class MyInternalPulloutTCrossScenarioTargetCallback : InternalPulloutCallback<TCrossScenarioItem, TCrossScenarioTarget> {
            public TCrossScenarioTarget getFr(TCrossScenarioItem entity) { return entity.TCrossScenarioTarget; }
        }
        public IList<TPolylineCategoryList> PulloutTPolylineCategoryList(IList<TCrossScenarioItem> tCrossScenarioItemList) {
            return HelpPulloutInternally<TCrossScenarioItem, TPolylineCategoryList>(tCrossScenarioItemList, new MyInternalPulloutTPolylineCategoryListCallback());
        }
        protected class MyInternalPulloutTPolylineCategoryListCallback : InternalPulloutCallback<TCrossScenarioItem, TPolylineCategoryList> {
            public TPolylineCategoryList getFr(TCrossScenarioItem entity) { return entity.TPolylineCategoryList; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TCrossScenarioItem entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TCrossScenarioItem entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TCrossScenarioItem entity) {
            HelpInsertOrUpdateInternally<TCrossScenarioItem, TCrossScenarioItemCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TCrossScenarioItem, TCrossScenarioItemCB> {
            protected TCrossScenarioItemBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TCrossScenarioItemBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TCrossScenarioItem entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TCrossScenarioItem entity) { _bhv.Update(entity); }
            public TCrossScenarioItemCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TCrossScenarioItemCB cb, TCrossScenarioItem entity) {
                cb.Query().SetCrossScenarioItemId_Equal(entity.CrossScenarioItemId);
            }
            public int CallbackSelectCount(TCrossScenarioItemCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TCrossScenarioItem entity) {
            HelpDeleteInternally<TCrossScenarioItem>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TCrossScenarioItem> {
            protected TCrossScenarioItemBhv _bhv;
            public MyInternalDeleteCallback(TCrossScenarioItemBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TCrossScenarioItem entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TCrossScenarioItem tCrossScenarioItem, TCrossScenarioItemCB cb) {
            AssertObjectNotNull("tCrossScenarioItem", tCrossScenarioItem); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tCrossScenarioItem);
            FilterEntityOfUpdate(tCrossScenarioItem); AssertEntityOfUpdate(tCrossScenarioItem);
            return this.Dao.UpdateByQuery(cb, tCrossScenarioItem);
        }

        public int QueryDelete(TCrossScenarioItemCB cb) {
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
        protected int DelegateSelectCount(TCrossScenarioItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TCrossScenarioItem> DelegateSelectList(TCrossScenarioItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TCrossScenarioItem e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TCrossScenarioItem e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TCrossScenarioItem e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TCrossScenarioItem Downcast(Entity entity) {
            return (TCrossScenarioItem)entity;
        }

        protected TCrossScenarioItemCB Downcast(ConditionBean cb) {
            return (TCrossScenarioItemCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TCrossScenarioItemDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
