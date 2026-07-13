
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
    public partial class TGtMatrixChildBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TGtMatrixChildDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TGtMatrixChildBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_MATRIX_CHILD"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TGtMatrixChildDbm.GetInstance(); } }
        public TGtMatrixChildDbm MyDBMeta { get { return TGtMatrixChildDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TGtMatrixChild NewMyEntity() { return new TGtMatrixChild(); }
        public virtual TGtMatrixChildCB NewMyConditionBean() { return new TGtMatrixChildCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TGtMatrixChildCB cb) {
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
        public virtual TGtMatrixChild SelectEntity(TGtMatrixChildCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TGtMatrixChild> ls = null;
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
            return (TGtMatrixChild)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TGtMatrixChild SelectEntityWithDeletedCheck(TGtMatrixChildCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TGtMatrixChild entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TGtMatrixChild SelectByPKValue(decimal? gtMatrixChildid) {
            return SelectEntity(BuildPKCB(gtMatrixChildid));
        }

        public virtual TGtMatrixChild SelectByPKValueWithDeletedCheck(decimal? gtMatrixChildid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(gtMatrixChildid));
        }

        private TGtMatrixChildCB BuildPKCB(decimal? gtMatrixChildid) {
            AssertObjectNotNull("gtMatrixChildid", gtMatrixChildid);
            TGtMatrixChildCB cb = NewMyConditionBean();
            cb.Query().SetGtMatrixChildid_Equal(gtMatrixChildid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TGtMatrixChild> SelectList(TGtMatrixChildCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TGtMatrixChild>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TGtMatrixChild> SelectPage(TGtMatrixChildCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TGtMatrixChild> invoker = new PagingInvoker<TGtMatrixChild>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TGtMatrixChild> {
            protected TGtMatrixChildBhv _bhv; protected TGtMatrixChildCB _cb;
            public InternalSelectPagingHandler(TGtMatrixChildBhv bhv, TGtMatrixChildCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TGtMatrixChild> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TGtMatrixChild myEntity = (TGtMatrixChild)entity;
            myEntity.GtMatrixChildid = SelectNextVal();
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
        public IList<TGtMatrixInfo> PulloutTGtMatrixInfo(IList<TGtMatrixChild> tGtMatrixChildList) {
            return HelpPulloutInternally<TGtMatrixChild, TGtMatrixInfo>(tGtMatrixChildList, new MyInternalPulloutTGtMatrixInfoCallback());
        }
        protected class MyInternalPulloutTGtMatrixInfoCallback : InternalPulloutCallback<TGtMatrixChild, TGtMatrixInfo> {
            public TGtMatrixInfo getFr(TGtMatrixChild entity) { return entity.TGtMatrixInfo; }
        }
        public IList<TItemInfo> PulloutTItemInfo(IList<TGtMatrixChild> tGtMatrixChildList) {
            return HelpPulloutInternally<TGtMatrixChild, TItemInfo>(tGtMatrixChildList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TGtMatrixChild, TItemInfo> {
            public TItemInfo getFr(TGtMatrixChild entity) { return entity.TItemInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TGtMatrixChild entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TGtMatrixChild entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TGtMatrixChild entity) {
            HelpInsertOrUpdateInternally<TGtMatrixChild, TGtMatrixChildCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TGtMatrixChild, TGtMatrixChildCB> {
            protected TGtMatrixChildBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TGtMatrixChildBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TGtMatrixChild entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TGtMatrixChild entity) { _bhv.Update(entity); }
            public TGtMatrixChildCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TGtMatrixChildCB cb, TGtMatrixChild entity) {
                cb.Query().SetGtMatrixChildid_Equal(entity.GtMatrixChildid);
            }
            public int CallbackSelectCount(TGtMatrixChildCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TGtMatrixChild entity) {
            HelpDeleteInternally<TGtMatrixChild>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TGtMatrixChild> {
            protected TGtMatrixChildBhv _bhv;
            public MyInternalDeleteCallback(TGtMatrixChildBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TGtMatrixChild entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TGtMatrixChild tGtMatrixChild, TGtMatrixChildCB cb) {
            AssertObjectNotNull("tGtMatrixChild", tGtMatrixChild); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tGtMatrixChild);
            FilterEntityOfUpdate(tGtMatrixChild); AssertEntityOfUpdate(tGtMatrixChild);
            return this.Dao.UpdateByQuery(cb, tGtMatrixChild);
        }

        public int QueryDelete(TGtMatrixChildCB cb) {
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
        protected int DelegateSelectCount(TGtMatrixChildCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TGtMatrixChild> DelegateSelectList(TGtMatrixChildCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TGtMatrixChild e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TGtMatrixChild e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TGtMatrixChild e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TGtMatrixChild Downcast(Entity entity) {
            return (TGtMatrixChild)entity;
        }

        protected TGtMatrixChildCB Downcast(ConditionBean cb) {
            return (TGtMatrixChildCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TGtMatrixChildDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
