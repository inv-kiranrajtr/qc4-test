
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
    public partial class TPolylineCategoryListBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TPolylineCategoryListDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TPolylineCategoryListBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_POLYLINE_CATEGORY_LIST"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TPolylineCategoryListDbm.GetInstance(); } }
        public TPolylineCategoryListDbm MyDBMeta { get { return TPolylineCategoryListDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TPolylineCategoryList NewMyEntity() { return new TPolylineCategoryList(); }
        public virtual TPolylineCategoryListCB NewMyConditionBean() { return new TPolylineCategoryListCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TPolylineCategoryListCB cb) {
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
        public virtual TPolylineCategoryList SelectEntity(TPolylineCategoryListCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TPolylineCategoryList> ls = null;
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
            return (TPolylineCategoryList)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TPolylineCategoryList SelectEntityWithDeletedCheck(TPolylineCategoryListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TPolylineCategoryList entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TPolylineCategoryList SelectByPKValue(decimal? polylineCategoryListId) {
            return SelectEntity(BuildPKCB(polylineCategoryListId));
        }

        public virtual TPolylineCategoryList SelectByPKValueWithDeletedCheck(decimal? polylineCategoryListId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(polylineCategoryListId));
        }

        private TPolylineCategoryListCB BuildPKCB(decimal? polylineCategoryListId) {
            AssertObjectNotNull("polylineCategoryListId", polylineCategoryListId);
            TPolylineCategoryListCB cb = NewMyConditionBean();
            cb.Query().SetPolylineCategoryListId_Equal(polylineCategoryListId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TPolylineCategoryList> SelectList(TPolylineCategoryListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TPolylineCategoryList>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TPolylineCategoryList> SelectPage(TPolylineCategoryListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TPolylineCategoryList> invoker = new PagingInvoker<TPolylineCategoryList>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TPolylineCategoryList> {
            protected TPolylineCategoryListBhv _bhv; protected TPolylineCategoryListCB _cb;
            public InternalSelectPagingHandler(TPolylineCategoryListBhv bhv, TPolylineCategoryListCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TPolylineCategoryList> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TPolylineCategoryList myEntity = (TPolylineCategoryList)entity;
            myEntity.PolylineCategoryListId = SelectNextVal();
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
        public IList<TCrossScenarioItem> PulloutTCrossScenarioItem(IList<TPolylineCategoryList> tPolylineCategoryListList) {
            return HelpPulloutInternally<TPolylineCategoryList, TCrossScenarioItem>(tPolylineCategoryListList, new MyInternalPulloutTCrossScenarioItemCallback());
        }
        protected class MyInternalPulloutTCrossScenarioItemCallback : InternalPulloutCallback<TPolylineCategoryList, TCrossScenarioItem> {
            public TCrossScenarioItem getFr(TPolylineCategoryList entity) { return entity.TCrossScenarioItem; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TPolylineCategoryList entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TPolylineCategoryList entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TPolylineCategoryList entity) {
            HelpInsertOrUpdateInternally<TPolylineCategoryList, TPolylineCategoryListCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TPolylineCategoryList, TPolylineCategoryListCB> {
            protected TPolylineCategoryListBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TPolylineCategoryListBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TPolylineCategoryList entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TPolylineCategoryList entity) { _bhv.Update(entity); }
            public TPolylineCategoryListCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TPolylineCategoryListCB cb, TPolylineCategoryList entity) {
                cb.Query().SetPolylineCategoryListId_Equal(entity.PolylineCategoryListId);
            }
            public int CallbackSelectCount(TPolylineCategoryListCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TPolylineCategoryList entity) {
            HelpDeleteInternally<TPolylineCategoryList>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TPolylineCategoryList> {
            protected TPolylineCategoryListBhv _bhv;
            public MyInternalDeleteCallback(TPolylineCategoryListBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TPolylineCategoryList entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TPolylineCategoryList tPolylineCategoryList, TPolylineCategoryListCB cb) {
            AssertObjectNotNull("tPolylineCategoryList", tPolylineCategoryList); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tPolylineCategoryList);
            FilterEntityOfUpdate(tPolylineCategoryList); AssertEntityOfUpdate(tPolylineCategoryList);
            return this.Dao.UpdateByQuery(cb, tPolylineCategoryList);
        }

        public int QueryDelete(TPolylineCategoryListCB cb) {
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
        protected int DelegateSelectCount(TPolylineCategoryListCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TPolylineCategoryList> DelegateSelectList(TPolylineCategoryListCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TPolylineCategoryList e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TPolylineCategoryList e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TPolylineCategoryList e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TPolylineCategoryList Downcast(Entity entity) {
            return (TPolylineCategoryList)entity;
        }

        protected TPolylineCategoryListCB Downcast(ConditionBean cb) {
            return (TPolylineCategoryListCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TPolylineCategoryListDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
