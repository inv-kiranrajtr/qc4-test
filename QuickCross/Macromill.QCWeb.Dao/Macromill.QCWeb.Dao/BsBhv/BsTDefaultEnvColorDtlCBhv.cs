
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
    public partial class TDefaultEnvColorDtlCBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorDtlCDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvColorDtlCBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_DTL_C"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDefaultEnvColorDtlCDbm.GetInstance(); } }
        public TDefaultEnvColorDtlCDbm MyDBMeta { get { return TDefaultEnvColorDtlCDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDefaultEnvColorDtlC NewMyEntity() { return new TDefaultEnvColorDtlC(); }
        public virtual TDefaultEnvColorDtlCCB NewMyConditionBean() { return new TDefaultEnvColorDtlCCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDefaultEnvColorDtlCCB cb) {
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
        public virtual TDefaultEnvColorDtlC SelectEntity(TDefaultEnvColorDtlCCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDefaultEnvColorDtlC> ls = null;
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
            return (TDefaultEnvColorDtlC)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDefaultEnvColorDtlC SelectEntityWithDeletedCheck(TDefaultEnvColorDtlCCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDefaultEnvColorDtlC entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDefaultEnvColorDtlC SelectByPKValue(int? defEnvColorDtlCId) {
            return SelectEntity(BuildPKCB(defEnvColorDtlCId));
        }

        public virtual TDefaultEnvColorDtlC SelectByPKValueWithDeletedCheck(int? defEnvColorDtlCId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(defEnvColorDtlCId));
        }

        private TDefaultEnvColorDtlCCB BuildPKCB(int? defEnvColorDtlCId) {
            AssertObjectNotNull("defEnvColorDtlCId", defEnvColorDtlCId);
            TDefaultEnvColorDtlCCB cb = NewMyConditionBean();
            cb.Query().SetDefEnvColorDtlCId_Equal(defEnvColorDtlCId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDefaultEnvColorDtlC> SelectList(TDefaultEnvColorDtlCCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDefaultEnvColorDtlC>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDefaultEnvColorDtlC> SelectPage(TDefaultEnvColorDtlCCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDefaultEnvColorDtlC> invoker = new PagingInvoker<TDefaultEnvColorDtlC>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDefaultEnvColorDtlC> {
            protected TDefaultEnvColorDtlCBhv _bhv; protected TDefaultEnvColorDtlCCB _cb;
            public InternalSelectPagingHandler(TDefaultEnvColorDtlCBhv bhv, TDefaultEnvColorDtlCCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDefaultEnvColorDtlC> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public int? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDefaultEnvColorDtlC myEntity = (TDefaultEnvColorDtlC)entity;
            myEntity.DefEnvColorDtlCId = SelectNextVal();
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
        public IList<TDefaultEnvColorInfoC> PulloutTDefaultEnvColorInfoC(IList<TDefaultEnvColorDtlC> tDefaultEnvColorDtlCList) {
            return HelpPulloutInternally<TDefaultEnvColorDtlC, TDefaultEnvColorInfoC>(tDefaultEnvColorDtlCList, new MyInternalPulloutTDefaultEnvColorInfoCCallback());
        }
        protected class MyInternalPulloutTDefaultEnvColorInfoCCallback : InternalPulloutCallback<TDefaultEnvColorDtlC, TDefaultEnvColorInfoC> {
            public TDefaultEnvColorInfoC getFr(TDefaultEnvColorDtlC entity) { return entity.TDefaultEnvColorInfoC; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDefaultEnvColorDtlC entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDefaultEnvColorDtlC entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDefaultEnvColorDtlC entity) {
            HelpInsertOrUpdateInternally<TDefaultEnvColorDtlC, TDefaultEnvColorDtlCCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDefaultEnvColorDtlC, TDefaultEnvColorDtlCCB> {
            protected TDefaultEnvColorDtlCBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDefaultEnvColorDtlCBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDefaultEnvColorDtlC entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDefaultEnvColorDtlC entity) { _bhv.Update(entity); }
            public TDefaultEnvColorDtlCCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDefaultEnvColorDtlCCB cb, TDefaultEnvColorDtlC entity) {
                cb.Query().SetDefEnvColorDtlCId_Equal(entity.DefEnvColorDtlCId);
            }
            public int CallbackSelectCount(TDefaultEnvColorDtlCCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDefaultEnvColorDtlC entity) {
            HelpDeleteInternally<TDefaultEnvColorDtlC>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDefaultEnvColorDtlC> {
            protected TDefaultEnvColorDtlCBhv _bhv;
            public MyInternalDeleteCallback(TDefaultEnvColorDtlCBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDefaultEnvColorDtlC entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDefaultEnvColorDtlC tDefaultEnvColorDtlC, TDefaultEnvColorDtlCCB cb) {
            AssertObjectNotNull("tDefaultEnvColorDtlC", tDefaultEnvColorDtlC); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDefaultEnvColorDtlC);
            FilterEntityOfUpdate(tDefaultEnvColorDtlC); AssertEntityOfUpdate(tDefaultEnvColorDtlC);
            return this.Dao.UpdateByQuery(cb, tDefaultEnvColorDtlC);
        }

        public int QueryDelete(TDefaultEnvColorDtlCCB cb) {
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
        protected int DelegateSelectCount(TDefaultEnvColorDtlCCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDefaultEnvColorDtlC> DelegateSelectList(TDefaultEnvColorDtlCCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected int? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDefaultEnvColorDtlC e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDefaultEnvColorDtlC e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDefaultEnvColorDtlC e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDefaultEnvColorDtlC Downcast(Entity entity) {
            return (TDefaultEnvColorDtlC)entity;
        }

        protected TDefaultEnvColorDtlCCB Downcast(ConditionBean cb) {
            return (TDefaultEnvColorDtlCCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDefaultEnvColorDtlCDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
