
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
    public partial class TOutputSubFaBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubFaDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSubFaBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_FA"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputSubFaDbm.GetInstance(); } }
        public TOutputSubFaDbm MyDBMeta { get { return TOutputSubFaDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputSubFa NewMyEntity() { return new TOutputSubFa(); }
        public virtual TOutputSubFaCB NewMyConditionBean() { return new TOutputSubFaCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputSubFaCB cb) {
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
        public virtual TOutputSubFa SelectEntity(TOutputSubFaCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputSubFa> ls = null;
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
            return (TOutputSubFa)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputSubFa SelectEntityWithDeletedCheck(TOutputSubFaCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputSubFa entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputSubFa SelectByPKValue(decimal? outputSubFaId) {
            return SelectEntity(BuildPKCB(outputSubFaId));
        }

        public virtual TOutputSubFa SelectByPKValueWithDeletedCheck(decimal? outputSubFaId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputSubFaId));
        }

        private TOutputSubFaCB BuildPKCB(decimal? outputSubFaId) {
            AssertObjectNotNull("outputSubFaId", outputSubFaId);
            TOutputSubFaCB cb = NewMyConditionBean();
            cb.Query().SetOutputSubFaId_Equal(outputSubFaId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputSubFa> SelectList(TOutputSubFaCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputSubFa>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputSubFa> SelectPage(TOutputSubFaCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputSubFa> invoker = new PagingInvoker<TOutputSubFa>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputSubFa> {
            protected TOutputSubFaBhv _bhv; protected TOutputSubFaCB _cb;
            public InternalSelectPagingHandler(TOutputSubFaBhv bhv, TOutputSubFaCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputSubFa> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputSubFa myEntity = (TOutputSubFa)entity;
            myEntity.OutputSubFaId = SelectNextVal();
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
        public IList<TOutputCommon> PulloutTOutputCommon(IList<TOutputSubFa> tOutputSubFaList) {
            return HelpPulloutInternally<TOutputSubFa, TOutputCommon>(tOutputSubFaList, new MyInternalPulloutTOutputCommonCallback());
        }
        protected class MyInternalPulloutTOutputCommonCallback : InternalPulloutCallback<TOutputSubFa, TOutputCommon> {
            public TOutputCommon getFr(TOutputSubFa entity) { return entity.TOutputCommon; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputSubFa entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputSubFa entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputSubFa entity) {
            HelpInsertOrUpdateInternally<TOutputSubFa, TOutputSubFaCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputSubFa, TOutputSubFaCB> {
            protected TOutputSubFaBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputSubFaBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputSubFa entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputSubFa entity) { _bhv.Update(entity); }
            public TOutputSubFaCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputSubFaCB cb, TOutputSubFa entity) {
                cb.Query().SetOutputSubFaId_Equal(entity.OutputSubFaId);
            }
            public int CallbackSelectCount(TOutputSubFaCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputSubFa entity) {
            HelpDeleteInternally<TOutputSubFa>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputSubFa> {
            protected TOutputSubFaBhv _bhv;
            public MyInternalDeleteCallback(TOutputSubFaBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputSubFa entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputSubFa tOutputSubFa, TOutputSubFaCB cb) {
            AssertObjectNotNull("tOutputSubFa", tOutputSubFa); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputSubFa);
            FilterEntityOfUpdate(tOutputSubFa); AssertEntityOfUpdate(tOutputSubFa);
            return this.Dao.UpdateByQuery(cb, tOutputSubFa);
        }

        public int QueryDelete(TOutputSubFaCB cb) {
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
        protected int DelegateSelectCount(TOutputSubFaCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputSubFa> DelegateSelectList(TOutputSubFaCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputSubFa e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputSubFa e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputSubFa e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputSubFa Downcast(Entity entity) {
            return (TOutputSubFa)entity;
        }

        protected TOutputSubFaCB Downcast(ConditionBean cb) {
            return (TOutputSubFaCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputSubFaDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
