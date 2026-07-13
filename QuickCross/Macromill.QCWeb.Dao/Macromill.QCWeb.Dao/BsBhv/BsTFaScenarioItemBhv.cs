
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
    public partial class TFaScenarioItemBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>FA シナリオアイテムテーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TFaScenarioItemDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TFaScenarioItemBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_SCENARIO_ITEM"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TFaScenarioItemDbm.GetInstance(); } }
        public TFaScenarioItemDbm MyDBMeta { get { return TFaScenarioItemDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TFaScenarioItem NewMyEntity() { return new TFaScenarioItem(); }
        public virtual TFaScenarioItemCB NewMyConditionBean() { return new TFaScenarioItemCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TFaScenarioItemCB cb) {
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
        public virtual TFaScenarioItem SelectEntity(TFaScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TFaScenarioItem> ls = null;
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
            return (TFaScenarioItem)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TFaScenarioItem SelectEntityWithDeletedCheck(TFaScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TFaScenarioItem entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TFaScenarioItem SelectByPKValue(decimal? faScenarioItemId) {
            return SelectEntity(BuildPKCB(faScenarioItemId));
        }

        public virtual TFaScenarioItem SelectByPKValueWithDeletedCheck(decimal? faScenarioItemId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(faScenarioItemId));
        }

        private TFaScenarioItemCB BuildPKCB(decimal? faScenarioItemId) {
            AssertObjectNotNull("faScenarioItemId", faScenarioItemId);
            TFaScenarioItemCB cb = NewMyConditionBean();
            cb.Query().SetFaScenarioItemId_Equal(faScenarioItemId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TFaScenarioItem> SelectList(TFaScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TFaScenarioItem>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TFaScenarioItem> SelectPage(TFaScenarioItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TFaScenarioItem> invoker = new PagingInvoker<TFaScenarioItem>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TFaScenarioItem> {
            protected TFaScenarioItemBhv _bhv; protected TFaScenarioItemCB _cb;
            public InternalSelectPagingHandler(TFaScenarioItemBhv bhv, TFaScenarioItemCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TFaScenarioItem> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TFaScenarioItem myEntity = (TFaScenarioItem)entity;
            myEntity.FaScenarioItemId = SelectNextVal();
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
        public IList<TFaScenarioHeader> PulloutTFaScenarioHeader(IList<TFaScenarioItem> tFaScenarioItemList) {
            return HelpPulloutInternally<TFaScenarioItem, TFaScenarioHeader>(tFaScenarioItemList, new MyInternalPulloutTFaScenarioHeaderCallback());
        }
        protected class MyInternalPulloutTFaScenarioHeaderCallback : InternalPulloutCallback<TFaScenarioItem, TFaScenarioHeader> {
            public TFaScenarioHeader getFr(TFaScenarioItem entity) { return entity.TFaScenarioHeader; }
        }
        public IList<TItemInfo> PulloutTItemInfo(IList<TFaScenarioItem> tFaScenarioItemList) {
            return HelpPulloutInternally<TFaScenarioItem, TItemInfo>(tFaScenarioItemList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TFaScenarioItem, TItemInfo> {
            public TItemInfo getFr(TFaScenarioItem entity) { return entity.TItemInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TFaScenarioItem entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TFaScenarioItem entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TFaScenarioItem entity) {
            HelpInsertOrUpdateInternally<TFaScenarioItem, TFaScenarioItemCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TFaScenarioItem, TFaScenarioItemCB> {
            protected TFaScenarioItemBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TFaScenarioItemBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TFaScenarioItem entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TFaScenarioItem entity) { _bhv.Update(entity); }
            public TFaScenarioItemCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TFaScenarioItemCB cb, TFaScenarioItem entity) {
                cb.Query().SetFaScenarioItemId_Equal(entity.FaScenarioItemId);
            }
            public int CallbackSelectCount(TFaScenarioItemCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TFaScenarioItem entity) {
            HelpDeleteInternally<TFaScenarioItem>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TFaScenarioItem> {
            protected TFaScenarioItemBhv _bhv;
            public MyInternalDeleteCallback(TFaScenarioItemBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TFaScenarioItem entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TFaScenarioItem tFaScenarioItem, TFaScenarioItemCB cb) {
            AssertObjectNotNull("tFaScenarioItem", tFaScenarioItem); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tFaScenarioItem);
            FilterEntityOfUpdate(tFaScenarioItem); AssertEntityOfUpdate(tFaScenarioItem);
            return this.Dao.UpdateByQuery(cb, tFaScenarioItem);
        }

        public int QueryDelete(TFaScenarioItemCB cb) {
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
        protected int DelegateSelectCount(TFaScenarioItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TFaScenarioItem> DelegateSelectList(TFaScenarioItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TFaScenarioItem e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TFaScenarioItem e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TFaScenarioItem e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TFaScenarioItem Downcast(Entity entity) {
            return (TFaScenarioItem)entity;
        }

        protected TFaScenarioItemCB Downcast(ConditionBean cb) {
            return (TFaScenarioItemCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TFaScenarioItemDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
