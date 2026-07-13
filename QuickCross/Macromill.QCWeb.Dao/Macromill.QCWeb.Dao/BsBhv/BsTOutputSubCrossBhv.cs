
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
    public partial class TOutputSubCrossBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSubCrossDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSubCrossBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SUB_CROSS"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputSubCrossDbm.GetInstance(); } }
        public TOutputSubCrossDbm MyDBMeta { get { return TOutputSubCrossDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputSubCross NewMyEntity() { return new TOutputSubCross(); }
        public virtual TOutputSubCrossCB NewMyConditionBean() { return new TOutputSubCrossCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputSubCrossCB cb) {
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
        public virtual TOutputSubCross SelectEntity(TOutputSubCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputSubCross> ls = null;
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
            return (TOutputSubCross)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputSubCross SelectEntityWithDeletedCheck(TOutputSubCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputSubCross entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputSubCross SelectByPKValue(decimal? outputSubCrossId) {
            return SelectEntity(BuildPKCB(outputSubCrossId));
        }

        public virtual TOutputSubCross SelectByPKValueWithDeletedCheck(decimal? outputSubCrossId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputSubCrossId));
        }

        private TOutputSubCrossCB BuildPKCB(decimal? outputSubCrossId) {
            AssertObjectNotNull("outputSubCrossId", outputSubCrossId);
            TOutputSubCrossCB cb = NewMyConditionBean();
            cb.Query().SetOutputSubCrossId_Equal(outputSubCrossId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputSubCross> SelectList(TOutputSubCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputSubCross>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputSubCross> SelectPage(TOutputSubCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputSubCross> invoker = new PagingInvoker<TOutputSubCross>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputSubCross> {
            protected TOutputSubCrossBhv _bhv; protected TOutputSubCrossCB _cb;
            public InternalSelectPagingHandler(TOutputSubCrossBhv bhv, TOutputSubCrossCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputSubCross> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputSubCross myEntity = (TOutputSubCross)entity;
            myEntity.OutputSubCrossId = SelectNextVal();
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
        public IList<TOutputCommon> PulloutTOutputCommon(IList<TOutputSubCross> tOutputSubCrossList) {
            return HelpPulloutInternally<TOutputSubCross, TOutputCommon>(tOutputSubCrossList, new MyInternalPulloutTOutputCommonCallback());
        }
        protected class MyInternalPulloutTOutputCommonCallback : InternalPulloutCallback<TOutputSubCross, TOutputCommon> {
            public TOutputCommon getFr(TOutputSubCross entity) { return entity.TOutputCommon; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputSubCross entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputSubCross entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputSubCross entity) {
            HelpInsertOrUpdateInternally<TOutputSubCross, TOutputSubCrossCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputSubCross, TOutputSubCrossCB> {
            protected TOutputSubCrossBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputSubCrossBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputSubCross entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputSubCross entity) { _bhv.Update(entity); }
            public TOutputSubCrossCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputSubCrossCB cb, TOutputSubCross entity) {
                cb.Query().SetOutputSubCrossId_Equal(entity.OutputSubCrossId);
            }
            public int CallbackSelectCount(TOutputSubCrossCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputSubCross entity) {
            HelpDeleteInternally<TOutputSubCross>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputSubCross> {
            protected TOutputSubCrossBhv _bhv;
            public MyInternalDeleteCallback(TOutputSubCrossBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputSubCross entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputSubCross tOutputSubCross, TOutputSubCrossCB cb) {
            AssertObjectNotNull("tOutputSubCross", tOutputSubCross); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputSubCross);
            FilterEntityOfUpdate(tOutputSubCross); AssertEntityOfUpdate(tOutputSubCross);
            return this.Dao.UpdateByQuery(cb, tOutputSubCross);
        }

        public int QueryDelete(TOutputSubCrossCB cb) {
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
        protected int DelegateSelectCount(TOutputSubCrossCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputSubCross> DelegateSelectList(TOutputSubCrossCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputSubCross e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputSubCross e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputSubCross e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputSubCross Downcast(Entity entity) {
            return (TOutputSubCross)entity;
        }

        protected TOutputSubCrossCB Downcast(ConditionBean cb) {
            return (TOutputSubCrossCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputSubCrossDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
