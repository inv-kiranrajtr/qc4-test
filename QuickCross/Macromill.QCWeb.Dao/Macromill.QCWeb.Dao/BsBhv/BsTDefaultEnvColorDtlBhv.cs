
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
    public partial class TDefaultEnvColorDtlBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorDtlDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvColorDtlBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDefaultEnvColorDtlDbm.GetInstance(); } }
        public TDefaultEnvColorDtlDbm MyDBMeta { get { return TDefaultEnvColorDtlDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDefaultEnvColorDtl NewMyEntity() { return new TDefaultEnvColorDtl(); }
        public virtual TDefaultEnvColorDtlCB NewMyConditionBean() { return new TDefaultEnvColorDtlCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDefaultEnvColorDtlCB cb) {
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
        public virtual TDefaultEnvColorDtl SelectEntity(TDefaultEnvColorDtlCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDefaultEnvColorDtl> ls = null;
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
            return (TDefaultEnvColorDtl)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDefaultEnvColorDtl SelectEntityWithDeletedCheck(TDefaultEnvColorDtlCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDefaultEnvColorDtl entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDefaultEnvColorDtl SelectByPKValue(decimal? defEnvColorDtlId) {
            return SelectEntity(BuildPKCB(defEnvColorDtlId));
        }

        public virtual TDefaultEnvColorDtl SelectByPKValueWithDeletedCheck(decimal? defEnvColorDtlId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(defEnvColorDtlId));
        }

        private TDefaultEnvColorDtlCB BuildPKCB(decimal? defEnvColorDtlId) {
            AssertObjectNotNull("defEnvColorDtlId", defEnvColorDtlId);
            TDefaultEnvColorDtlCB cb = NewMyConditionBean();
            cb.Query().SetDefEnvColorDtlId_Equal(defEnvColorDtlId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDefaultEnvColorDtl> SelectList(TDefaultEnvColorDtlCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDefaultEnvColorDtl>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDefaultEnvColorDtl> SelectPage(TDefaultEnvColorDtlCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDefaultEnvColorDtl> invoker = new PagingInvoker<TDefaultEnvColorDtl>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDefaultEnvColorDtl> {
            protected TDefaultEnvColorDtlBhv _bhv; protected TDefaultEnvColorDtlCB _cb;
            public InternalSelectPagingHandler(TDefaultEnvColorDtlBhv bhv, TDefaultEnvColorDtlCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDefaultEnvColorDtl> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDefaultEnvColorDtl myEntity = (TDefaultEnvColorDtl)entity;
            myEntity.DefEnvColorDtlId = SelectNextVal();
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
        public IList<TDefaultEnvColorInfo> PulloutTDefaultEnvColorInfo(IList<TDefaultEnvColorDtl> tDefaultEnvColorDtlList) {
            return HelpPulloutInternally<TDefaultEnvColorDtl, TDefaultEnvColorInfo>(tDefaultEnvColorDtlList, new MyInternalPulloutTDefaultEnvColorInfoCallback());
        }
        protected class MyInternalPulloutTDefaultEnvColorInfoCallback : InternalPulloutCallback<TDefaultEnvColorDtl, TDefaultEnvColorInfo> {
            public TDefaultEnvColorInfo getFr(TDefaultEnvColorDtl entity) { return entity.TDefaultEnvColorInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDefaultEnvColorDtl entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDefaultEnvColorDtl entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDefaultEnvColorDtl entity) {
            HelpInsertOrUpdateInternally<TDefaultEnvColorDtl, TDefaultEnvColorDtlCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDefaultEnvColorDtl, TDefaultEnvColorDtlCB> {
            protected TDefaultEnvColorDtlBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDefaultEnvColorDtlBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDefaultEnvColorDtl entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDefaultEnvColorDtl entity) { _bhv.Update(entity); }
            public TDefaultEnvColorDtlCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDefaultEnvColorDtlCB cb, TDefaultEnvColorDtl entity) {
                cb.Query().SetDefEnvColorDtlId_Equal(entity.DefEnvColorDtlId);
            }
            public int CallbackSelectCount(TDefaultEnvColorDtlCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDefaultEnvColorDtl entity) {
            HelpDeleteInternally<TDefaultEnvColorDtl>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDefaultEnvColorDtl> {
            protected TDefaultEnvColorDtlBhv _bhv;
            public MyInternalDeleteCallback(TDefaultEnvColorDtlBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDefaultEnvColorDtl entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDefaultEnvColorDtl tDefaultEnvColorDtl, TDefaultEnvColorDtlCB cb) {
            AssertObjectNotNull("tDefaultEnvColorDtl", tDefaultEnvColorDtl); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDefaultEnvColorDtl);
            FilterEntityOfUpdate(tDefaultEnvColorDtl); AssertEntityOfUpdate(tDefaultEnvColorDtl);
            return this.Dao.UpdateByQuery(cb, tDefaultEnvColorDtl);
        }

        public int QueryDelete(TDefaultEnvColorDtlCB cb) {
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
        protected int DelegateSelectCount(TDefaultEnvColorDtlCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDefaultEnvColorDtl> DelegateSelectList(TDefaultEnvColorDtlCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDefaultEnvColorDtl e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDefaultEnvColorDtl e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDefaultEnvColorDtl e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDefaultEnvColorDtl Downcast(Entity entity) {
            return (TDefaultEnvColorDtl)entity;
        }

        protected TDefaultEnvColorDtlCB Downcast(ConditionBean cb) {
            return (TDefaultEnvColorDtlCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDefaultEnvColorDtlDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
