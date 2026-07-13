
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
    public partial class TOutputSubGtBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubGtDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSubGtBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_GT"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputSubGtDbm.GetInstance(); } }
        public TOutputSubGtDbm MyDBMeta { get { return TOutputSubGtDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputSubGt NewMyEntity() { return new TOutputSubGt(); }
        public virtual TOutputSubGtCB NewMyConditionBean() { return new TOutputSubGtCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputSubGtCB cb) {
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
        public virtual TOutputSubGt SelectEntity(TOutputSubGtCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputSubGt> ls = null;
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
            return (TOutputSubGt)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputSubGt SelectEntityWithDeletedCheck(TOutputSubGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputSubGt entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputSubGt SelectByPKValue(decimal? outputSubGtId) {
            return SelectEntity(BuildPKCB(outputSubGtId));
        }

        public virtual TOutputSubGt SelectByPKValueWithDeletedCheck(decimal? outputSubGtId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputSubGtId));
        }

        private TOutputSubGtCB BuildPKCB(decimal? outputSubGtId) {
            AssertObjectNotNull("outputSubGtId", outputSubGtId);
            TOutputSubGtCB cb = NewMyConditionBean();
            cb.Query().SetOutputSubGtId_Equal(outputSubGtId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputSubGt> SelectList(TOutputSubGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputSubGt>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputSubGt> SelectPage(TOutputSubGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputSubGt> invoker = new PagingInvoker<TOutputSubGt>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputSubGt> {
            protected TOutputSubGtBhv _bhv; protected TOutputSubGtCB _cb;
            public InternalSelectPagingHandler(TOutputSubGtBhv bhv, TOutputSubGtCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputSubGt> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputSubGt myEntity = (TOutputSubGt)entity;
            myEntity.OutputSubGtId = SelectNextVal();
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
        public IList<TOutputCommon> PulloutTOutputCommon(IList<TOutputSubGt> tOutputSubGtList) {
            return HelpPulloutInternally<TOutputSubGt, TOutputCommon>(tOutputSubGtList, new MyInternalPulloutTOutputCommonCallback());
        }
        protected class MyInternalPulloutTOutputCommonCallback : InternalPulloutCallback<TOutputSubGt, TOutputCommon> {
            public TOutputCommon getFr(TOutputSubGt entity) { return entity.TOutputCommon; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputSubGt entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputSubGt entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputSubGt entity) {
            HelpInsertOrUpdateInternally<TOutputSubGt, TOutputSubGtCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputSubGt, TOutputSubGtCB> {
            protected TOutputSubGtBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputSubGtBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputSubGt entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputSubGt entity) { _bhv.Update(entity); }
            public TOutputSubGtCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputSubGtCB cb, TOutputSubGt entity) {
                cb.Query().SetOutputSubGtId_Equal(entity.OutputSubGtId);
            }
            public int CallbackSelectCount(TOutputSubGtCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputSubGt entity) {
            HelpDeleteInternally<TOutputSubGt>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputSubGt> {
            protected TOutputSubGtBhv _bhv;
            public MyInternalDeleteCallback(TOutputSubGtBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputSubGt entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputSubGt tOutputSubGt, TOutputSubGtCB cb) {
            AssertObjectNotNull("tOutputSubGt", tOutputSubGt); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputSubGt);
            FilterEntityOfUpdate(tOutputSubGt); AssertEntityOfUpdate(tOutputSubGt);
            return this.Dao.UpdateByQuery(cb, tOutputSubGt);
        }

        public int QueryDelete(TOutputSubGtCB cb) {
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
        protected int DelegateSelectCount(TOutputSubGtCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputSubGt> DelegateSelectList(TOutputSubGtCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputSubGt e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputSubGt e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputSubGt e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputSubGt Downcast(Entity entity) {
            return (TOutputSubGt)entity;
        }

        protected TOutputSubGtCB Downcast(ConditionBean cb) {
            return (TOutputSubGtCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputSubGtDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
