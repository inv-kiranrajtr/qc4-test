
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
    public partial class TScenarioQuerylistBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>シナリオ絞込み条件テーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TScenarioQuerylistDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TScenarioQuerylistBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SCENARIO_QUERYLIST"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TScenarioQuerylistDbm.GetInstance(); } }
        public TScenarioQuerylistDbm MyDBMeta { get { return TScenarioQuerylistDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TScenarioQuerylist NewMyEntity() { return new TScenarioQuerylist(); }
        public virtual TScenarioQuerylistCB NewMyConditionBean() { return new TScenarioQuerylistCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TScenarioQuerylistCB cb) {
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
        public virtual TScenarioQuerylist SelectEntity(TScenarioQuerylistCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TScenarioQuerylist> ls = null;
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
            return (TScenarioQuerylist)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TScenarioQuerylist SelectEntityWithDeletedCheck(TScenarioQuerylistCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TScenarioQuerylist entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TScenarioQuerylist SelectByPKValue(decimal? scenarioQuerylistId) {
            return SelectEntity(BuildPKCB(scenarioQuerylistId));
        }

        public virtual TScenarioQuerylist SelectByPKValueWithDeletedCheck(decimal? scenarioQuerylistId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(scenarioQuerylistId));
        }

        private TScenarioQuerylistCB BuildPKCB(decimal? scenarioQuerylistId) {
            AssertObjectNotNull("scenarioQuerylistId", scenarioQuerylistId);
            TScenarioQuerylistCB cb = NewMyConditionBean();
            cb.Query().SetScenarioQuerylistId_Equal(scenarioQuerylistId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TScenarioQuerylist> SelectList(TScenarioQuerylistCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TScenarioQuerylist>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TScenarioQuerylist> SelectPage(TScenarioQuerylistCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TScenarioQuerylist> invoker = new PagingInvoker<TScenarioQuerylist>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TScenarioQuerylist> {
            protected TScenarioQuerylistBhv _bhv; protected TScenarioQuerylistCB _cb;
            public InternalSelectPagingHandler(TScenarioQuerylistBhv bhv, TScenarioQuerylistCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TScenarioQuerylist> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TScenarioQuerylist myEntity = (TScenarioQuerylist)entity;
            myEntity.ScenarioQuerylistId = SelectNextVal();
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
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TScenarioQuerylist> tScenarioQuerylistList) {
            return HelpPulloutInternally<TScenarioQuerylist, TScenarioTotalization>(tScenarioQuerylistList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TScenarioQuerylist, TScenarioTotalization> {
            public TScenarioTotalization getFr(TScenarioQuerylist entity) { return entity.TScenarioTotalization; }
        }
        public IList<TItemInfo> PulloutTItemInfo(IList<TScenarioQuerylist> tScenarioQuerylistList) {
            return HelpPulloutInternally<TScenarioQuerylist, TItemInfo>(tScenarioQuerylistList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TScenarioQuerylist, TItemInfo> {
            public TItemInfo getFr(TScenarioQuerylist entity) { return entity.TItemInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TScenarioQuerylist entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TScenarioQuerylist entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TScenarioQuerylist entity) {
            HelpInsertOrUpdateInternally<TScenarioQuerylist, TScenarioQuerylistCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TScenarioQuerylist, TScenarioQuerylistCB> {
            protected TScenarioQuerylistBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TScenarioQuerylistBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TScenarioQuerylist entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TScenarioQuerylist entity) { _bhv.Update(entity); }
            public TScenarioQuerylistCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TScenarioQuerylistCB cb, TScenarioQuerylist entity) {
                cb.Query().SetScenarioQuerylistId_Equal(entity.ScenarioQuerylistId);
            }
            public int CallbackSelectCount(TScenarioQuerylistCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TScenarioQuerylist entity) {
            HelpDeleteInternally<TScenarioQuerylist>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TScenarioQuerylist> {
            protected TScenarioQuerylistBhv _bhv;
            public MyInternalDeleteCallback(TScenarioQuerylistBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TScenarioQuerylist entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TScenarioQuerylist tScenarioQuerylist, TScenarioQuerylistCB cb) {
            AssertObjectNotNull("tScenarioQuerylist", tScenarioQuerylist); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tScenarioQuerylist);
            FilterEntityOfUpdate(tScenarioQuerylist); AssertEntityOfUpdate(tScenarioQuerylist);
            return this.Dao.UpdateByQuery(cb, tScenarioQuerylist);
        }

        public int QueryDelete(TScenarioQuerylistCB cb) {
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
        protected int DelegateSelectCount(TScenarioQuerylistCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TScenarioQuerylist> DelegateSelectList(TScenarioQuerylistCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TScenarioQuerylist e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TScenarioQuerylist e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TScenarioQuerylist e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TScenarioQuerylist Downcast(Entity entity) {
            return (TScenarioQuerylist)entity;
        }

        protected TScenarioQuerylistCB Downcast(ConditionBean cb) {
            return (TScenarioQuerylistCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TScenarioQuerylistDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
