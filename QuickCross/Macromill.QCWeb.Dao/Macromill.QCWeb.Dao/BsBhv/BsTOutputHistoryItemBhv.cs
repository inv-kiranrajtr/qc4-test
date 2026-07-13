
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
    public partial class TOutputHistoryItemBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputHistoryItemDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputHistoryItemBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_HISTORY_ITEM"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputHistoryItemDbm.GetInstance(); } }
        public TOutputHistoryItemDbm MyDBMeta { get { return TOutputHistoryItemDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputHistoryItem NewMyEntity() { return new TOutputHistoryItem(); }
        public virtual TOutputHistoryItemCB NewMyConditionBean() { return new TOutputHistoryItemCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputHistoryItemCB cb) {
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
        public virtual TOutputHistoryItem SelectEntity(TOutputHistoryItemCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputHistoryItem> ls = null;
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
            return (TOutputHistoryItem)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputHistoryItem SelectEntityWithDeletedCheck(TOutputHistoryItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputHistoryItem entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputHistoryItem SelectByPKValue(decimal? outputHistoryItemId) {
            return SelectEntity(BuildPKCB(outputHistoryItemId));
        }

        public virtual TOutputHistoryItem SelectByPKValueWithDeletedCheck(decimal? outputHistoryItemId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputHistoryItemId));
        }

        private TOutputHistoryItemCB BuildPKCB(decimal? outputHistoryItemId) {
            AssertObjectNotNull("outputHistoryItemId", outputHistoryItemId);
            TOutputHistoryItemCB cb = NewMyConditionBean();
            cb.Query().SetOutputHistoryItemId_Equal(outputHistoryItemId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputHistoryItem> SelectList(TOutputHistoryItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputHistoryItem>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputHistoryItem> SelectPage(TOutputHistoryItemCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputHistoryItem> invoker = new PagingInvoker<TOutputHistoryItem>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputHistoryItem> {
            protected TOutputHistoryItemBhv _bhv; protected TOutputHistoryItemCB _cb;
            public InternalSelectPagingHandler(TOutputHistoryItemBhv bhv, TOutputHistoryItemCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputHistoryItem> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputHistoryItem myEntity = (TOutputHistoryItem)entity;
            myEntity.OutputHistoryItemId = SelectNextVal();
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
        public IList<TOutputSetting> PulloutTOutputSetting(IList<TOutputHistoryItem> tOutputHistoryItemList) {
            return HelpPulloutInternally<TOutputHistoryItem, TOutputSetting>(tOutputHistoryItemList, new MyInternalPulloutTOutputSettingCallback());
        }
        protected class MyInternalPulloutTOutputSettingCallback : InternalPulloutCallback<TOutputHistoryItem, TOutputSetting> {
            public TOutputSetting getFr(TOutputHistoryItem entity) { return entity.TOutputSetting; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputHistoryItem entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputHistoryItem entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputHistoryItem entity) {
            HelpInsertOrUpdateInternally<TOutputHistoryItem, TOutputHistoryItemCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputHistoryItem, TOutputHistoryItemCB> {
            protected TOutputHistoryItemBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputHistoryItemBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputHistoryItem entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputHistoryItem entity) { _bhv.Update(entity); }
            public TOutputHistoryItemCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputHistoryItemCB cb, TOutputHistoryItem entity) {
                cb.Query().SetOutputHistoryItemId_Equal(entity.OutputHistoryItemId);
            }
            public int CallbackSelectCount(TOutputHistoryItemCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputHistoryItem entity) {
            HelpDeleteInternally<TOutputHistoryItem>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputHistoryItem> {
            protected TOutputHistoryItemBhv _bhv;
            public MyInternalDeleteCallback(TOutputHistoryItemBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputHistoryItem entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputHistoryItem tOutputHistoryItem, TOutputHistoryItemCB cb) {
            AssertObjectNotNull("tOutputHistoryItem", tOutputHistoryItem); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputHistoryItem);
            FilterEntityOfUpdate(tOutputHistoryItem); AssertEntityOfUpdate(tOutputHistoryItem);
            return this.Dao.UpdateByQuery(cb, tOutputHistoryItem);
        }

        public int QueryDelete(TOutputHistoryItemCB cb) {
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
        protected int DelegateSelectCount(TOutputHistoryItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputHistoryItem> DelegateSelectList(TOutputHistoryItemCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputHistoryItem e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputHistoryItem e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputHistoryItem e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputHistoryItem Downcast(Entity entity) {
            return (TOutputHistoryItem)entity;
        }

        protected TOutputHistoryItemCB Downcast(ConditionBean cb) {
            return (TOutputHistoryItemCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputHistoryItemDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
