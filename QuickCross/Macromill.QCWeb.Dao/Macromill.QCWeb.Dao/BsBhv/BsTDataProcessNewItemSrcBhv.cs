
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
    public partial class TDataProcessNewItemSrcBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataProcessNewItemSrcDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataProcessNewItemSrcBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_ITEM_SRC"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDataProcessNewItemSrcDbm.GetInstance(); } }
        public TDataProcessNewItemSrcDbm MyDBMeta { get { return TDataProcessNewItemSrcDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDataProcessNewItemSrc NewMyEntity() { return new TDataProcessNewItemSrc(); }
        public virtual TDataProcessNewItemSrcCB NewMyConditionBean() { return new TDataProcessNewItemSrcCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDataProcessNewItemSrcCB cb) {
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
        public virtual TDataProcessNewItemSrc SelectEntity(TDataProcessNewItemSrcCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDataProcessNewItemSrc> ls = null;
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
            return (TDataProcessNewItemSrc)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDataProcessNewItemSrc SelectEntityWithDeletedCheck(TDataProcessNewItemSrcCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDataProcessNewItemSrc entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDataProcessNewItemSrc SelectByPKValue(decimal? dataProcessNewItemSrcId) {
            return SelectEntity(BuildPKCB(dataProcessNewItemSrcId));
        }

        public virtual TDataProcessNewItemSrc SelectByPKValueWithDeletedCheck(decimal? dataProcessNewItemSrcId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(dataProcessNewItemSrcId));
        }

        private TDataProcessNewItemSrcCB BuildPKCB(decimal? dataProcessNewItemSrcId) {
            AssertObjectNotNull("dataProcessNewItemSrcId", dataProcessNewItemSrcId);
            TDataProcessNewItemSrcCB cb = NewMyConditionBean();
            cb.Query().SetDataProcessNewItemSrcId_Equal(dataProcessNewItemSrcId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDataProcessNewItemSrc> SelectList(TDataProcessNewItemSrcCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDataProcessNewItemSrc>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDataProcessNewItemSrc> SelectPage(TDataProcessNewItemSrcCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDataProcessNewItemSrc> invoker = new PagingInvoker<TDataProcessNewItemSrc>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDataProcessNewItemSrc> {
            protected TDataProcessNewItemSrcBhv _bhv; protected TDataProcessNewItemSrcCB _cb;
            public InternalSelectPagingHandler(TDataProcessNewItemSrcBhv bhv, TDataProcessNewItemSrcCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDataProcessNewItemSrc> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDataProcessNewItemSrc myEntity = (TDataProcessNewItemSrc)entity;
            myEntity.DataProcessNewItemSrcId = SelectNextVal();
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
        public IList<TDataProcessNewItem> PulloutTDataProcessNewItem(IList<TDataProcessNewItemSrc> tDataProcessNewItemSrcList) {
            return HelpPulloutInternally<TDataProcessNewItemSrc, TDataProcessNewItem>(tDataProcessNewItemSrcList, new MyInternalPulloutTDataProcessNewItemCallback());
        }
        protected class MyInternalPulloutTDataProcessNewItemCallback : InternalPulloutCallback<TDataProcessNewItemSrc, TDataProcessNewItem> {
            public TDataProcessNewItem getFr(TDataProcessNewItemSrc entity) { return entity.TDataProcessNewItem; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDataProcessNewItemSrc entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDataProcessNewItemSrc entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDataProcessNewItemSrc entity) {
            HelpInsertOrUpdateInternally<TDataProcessNewItemSrc, TDataProcessNewItemSrcCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDataProcessNewItemSrc, TDataProcessNewItemSrcCB> {
            protected TDataProcessNewItemSrcBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDataProcessNewItemSrcBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDataProcessNewItemSrc entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDataProcessNewItemSrc entity) { _bhv.Update(entity); }
            public TDataProcessNewItemSrcCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDataProcessNewItemSrcCB cb, TDataProcessNewItemSrc entity) {
                cb.Query().SetDataProcessNewItemSrcId_Equal(entity.DataProcessNewItemSrcId);
            }
            public int CallbackSelectCount(TDataProcessNewItemSrcCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDataProcessNewItemSrc entity) {
            HelpDeleteInternally<TDataProcessNewItemSrc>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDataProcessNewItemSrc> {
            protected TDataProcessNewItemSrcBhv _bhv;
            public MyInternalDeleteCallback(TDataProcessNewItemSrcBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDataProcessNewItemSrc entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDataProcessNewItemSrc tDataProcessNewItemSrc, TDataProcessNewItemSrcCB cb) {
            AssertObjectNotNull("tDataProcessNewItemSrc", tDataProcessNewItemSrc); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDataProcessNewItemSrc);
            FilterEntityOfUpdate(tDataProcessNewItemSrc); AssertEntityOfUpdate(tDataProcessNewItemSrc);
            return this.Dao.UpdateByQuery(cb, tDataProcessNewItemSrc);
        }

        public int QueryDelete(TDataProcessNewItemSrcCB cb) {
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
        protected int DelegateSelectCount(TDataProcessNewItemSrcCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDataProcessNewItemSrc> DelegateSelectList(TDataProcessNewItemSrcCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDataProcessNewItemSrc e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDataProcessNewItemSrc e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDataProcessNewItemSrc e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDataProcessNewItemSrc Downcast(Entity entity) {
            return (TDataProcessNewItemSrc)entity;
        }

        protected TDataProcessNewItemSrcCB Downcast(ConditionBean cb) {
            return (TDataProcessNewItemSrcCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDataProcessNewItemSrcDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
