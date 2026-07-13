
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
    public partial class TFaListAddItemBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>付加アイテムテーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TFaListAddItemDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TFaListAddItemBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_LIST_ADD_ITEM"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TFaListAddItemDbm.GetInstance(); } }
        public TFaListAddItemDbm MyDBMeta { get { return TFaListAddItemDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TFaListAddItem NewMyEntity() { return new TFaListAddItem(); }
        public virtual TFaListAddItemCB NewMyConditionBean() { return new TFaListAddItemCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TFaListAddItemCB cb) {
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
        public virtual TFaListAddItem SelectEntity(TFaListAddItemCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TFaListAddItem> ls = null;
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
            return (TFaListAddItem)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TFaListAddItem SelectEntityWithDeletedCheck(TFaListAddItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TFaListAddItem entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TFaListAddItem SelectByPKValue(decimal? faListAddItemId) {
            return SelectEntity(BuildPKCB(faListAddItemId));
        }

        public virtual TFaListAddItem SelectByPKValueWithDeletedCheck(decimal? faListAddItemId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(faListAddItemId));
        }

        private TFaListAddItemCB BuildPKCB(decimal? faListAddItemId) {
            AssertObjectNotNull("faListAddItemId", faListAddItemId);
            TFaListAddItemCB cb = NewMyConditionBean();
            cb.Query().SetFaListAddItemId_Equal(faListAddItemId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TFaListAddItem> SelectList(TFaListAddItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TFaListAddItem>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TFaListAddItem> SelectPage(TFaListAddItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TFaListAddItem> invoker = new PagingInvoker<TFaListAddItem>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TFaListAddItem> {
            protected TFaListAddItemBhv _bhv; protected TFaListAddItemCB _cb;
            public InternalSelectPagingHandler(TFaListAddItemBhv bhv, TFaListAddItemCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TFaListAddItem> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TFaListAddItem myEntity = (TFaListAddItem)entity;
            myEntity.FaListAddItemId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TFaScenarioHeader> PulloutTFaScenarioHeader(IList<TFaListAddItem> tFaListAddItemList) {
            return HelpPulloutInternally<TFaListAddItem, TFaScenarioHeader>(tFaListAddItemList, new MyInternalPulloutTFaScenarioHeaderCallback());
        }
        protected class MyInternalPulloutTFaScenarioHeaderCallback : InternalPulloutCallback<TFaListAddItem, TFaScenarioHeader> {
            public TFaScenarioHeader getFr(TFaListAddItem entity) { return entity.TFaScenarioHeader; }
        }
        public IList<TItemInfo> PulloutTItemInfo(IList<TFaListAddItem> tFaListAddItemList) {
            return HelpPulloutInternally<TFaListAddItem, TItemInfo>(tFaListAddItemList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TFaListAddItem, TItemInfo> {
            public TItemInfo getFr(TFaListAddItem entity) { return entity.TItemInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TFaListAddItem entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TFaListAddItem entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TFaListAddItem entity) {
            HelpInsertOrUpdateInternally<TFaListAddItem, TFaListAddItemCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TFaListAddItem, TFaListAddItemCB> {
            protected TFaListAddItemBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TFaListAddItemBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TFaListAddItem entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TFaListAddItem entity) { _bhv.Update(entity); }
            public TFaListAddItemCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TFaListAddItemCB cb, TFaListAddItem entity) {
                cb.Query().SetFaListAddItemId_Equal(entity.FaListAddItemId);
            }
            public int CallbackSelectCount(TFaListAddItemCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TFaListAddItem entity) {
            HelpDeleteInternally<TFaListAddItem>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TFaListAddItem> {
            protected TFaListAddItemBhv _bhv;
            public MyInternalDeleteCallback(TFaListAddItemBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TFaListAddItem entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TFaListAddItem tFaListAddItem, TFaListAddItemCB cb) {
            AssertObjectNotNull("tFaListAddItem", tFaListAddItem); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tFaListAddItem);
            FilterEntityOfUpdate(tFaListAddItem); AssertEntityOfUpdate(tFaListAddItem);
            return this.Dao.UpdateByQuery(cb, tFaListAddItem);
        }

        public int QueryDelete(TFaListAddItemCB cb) {
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
        protected int DelegateSelectCount(TFaListAddItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TFaListAddItem> DelegateSelectList(TFaListAddItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TFaListAddItem e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TFaListAddItem e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TFaListAddItem e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TFaListAddItem Downcast(Entity entity) {
            return (TFaListAddItem)entity;
        }

        protected TFaListAddItemCB Downcast(ConditionBean cb) {
            return (TFaListAddItemCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TFaListAddItemDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
