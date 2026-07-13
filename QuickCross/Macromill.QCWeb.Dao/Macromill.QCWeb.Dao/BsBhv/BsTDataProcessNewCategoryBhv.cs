
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
    public partial class TDataProcessNewCategoryBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataProcessNewCategoryDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataProcessNewCategoryBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_PROCESS_NEW_CATEGORY"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDataProcessNewCategoryDbm.GetInstance(); } }
        public TDataProcessNewCategoryDbm MyDBMeta { get { return TDataProcessNewCategoryDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDataProcessNewCategory NewMyEntity() { return new TDataProcessNewCategory(); }
        public virtual TDataProcessNewCategoryCB NewMyConditionBean() { return new TDataProcessNewCategoryCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDataProcessNewCategoryCB cb) {
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
        public virtual TDataProcessNewCategory SelectEntity(TDataProcessNewCategoryCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDataProcessNewCategory> ls = null;
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
            return (TDataProcessNewCategory)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDataProcessNewCategory SelectEntityWithDeletedCheck(TDataProcessNewCategoryCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDataProcessNewCategory entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDataProcessNewCategory SelectByPKValue(decimal? dataProcessNewCategoryId) {
            return SelectEntity(BuildPKCB(dataProcessNewCategoryId));
        }

        public virtual TDataProcessNewCategory SelectByPKValueWithDeletedCheck(decimal? dataProcessNewCategoryId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(dataProcessNewCategoryId));
        }

        private TDataProcessNewCategoryCB BuildPKCB(decimal? dataProcessNewCategoryId) {
            AssertObjectNotNull("dataProcessNewCategoryId", dataProcessNewCategoryId);
            TDataProcessNewCategoryCB cb = NewMyConditionBean();
            cb.Query().SetDataProcessNewCategoryId_Equal(dataProcessNewCategoryId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDataProcessNewCategory> SelectList(TDataProcessNewCategoryCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDataProcessNewCategory>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDataProcessNewCategory> SelectPage(TDataProcessNewCategoryCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDataProcessNewCategory> invoker = new PagingInvoker<TDataProcessNewCategory>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDataProcessNewCategory> {
            protected TDataProcessNewCategoryBhv _bhv; protected TDataProcessNewCategoryCB _cb;
            public InternalSelectPagingHandler(TDataProcessNewCategoryBhv bhv, TDataProcessNewCategoryCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDataProcessNewCategory> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDataProcessNewCategory myEntity = (TDataProcessNewCategory)entity;
            myEntity.DataProcessNewCategoryId = SelectNextVal();
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
        public IList<TDataProcessNewItem> PulloutTDataProcessNewItem(IList<TDataProcessNewCategory> tDataProcessNewCategoryList) {
            return HelpPulloutInternally<TDataProcessNewCategory, TDataProcessNewItem>(tDataProcessNewCategoryList, new MyInternalPulloutTDataProcessNewItemCallback());
        }
        protected class MyInternalPulloutTDataProcessNewItemCallback : InternalPulloutCallback<TDataProcessNewCategory, TDataProcessNewItem> {
            public TDataProcessNewItem getFr(TDataProcessNewCategory entity) { return entity.TDataProcessNewItem; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDataProcessNewCategory entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDataProcessNewCategory entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDataProcessNewCategory entity) {
            HelpInsertOrUpdateInternally<TDataProcessNewCategory, TDataProcessNewCategoryCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDataProcessNewCategory, TDataProcessNewCategoryCB> {
            protected TDataProcessNewCategoryBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDataProcessNewCategoryBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDataProcessNewCategory entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDataProcessNewCategory entity) { _bhv.Update(entity); }
            public TDataProcessNewCategoryCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDataProcessNewCategoryCB cb, TDataProcessNewCategory entity) {
                cb.Query().SetDataProcessNewCategoryId_Equal(entity.DataProcessNewCategoryId);
            }
            public int CallbackSelectCount(TDataProcessNewCategoryCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDataProcessNewCategory entity) {
            HelpDeleteInternally<TDataProcessNewCategory>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDataProcessNewCategory> {
            protected TDataProcessNewCategoryBhv _bhv;
            public MyInternalDeleteCallback(TDataProcessNewCategoryBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDataProcessNewCategory entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDataProcessNewCategory tDataProcessNewCategory, TDataProcessNewCategoryCB cb) {
            AssertObjectNotNull("tDataProcessNewCategory", tDataProcessNewCategory); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDataProcessNewCategory);
            FilterEntityOfUpdate(tDataProcessNewCategory); AssertEntityOfUpdate(tDataProcessNewCategory);
            return this.Dao.UpdateByQuery(cb, tDataProcessNewCategory);
        }

        public int QueryDelete(TDataProcessNewCategoryCB cb) {
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
        protected int DelegateSelectCount(TDataProcessNewCategoryCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDataProcessNewCategory> DelegateSelectList(TDataProcessNewCategoryCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDataProcessNewCategory e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDataProcessNewCategory e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDataProcessNewCategory e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDataProcessNewCategory Downcast(Entity entity) {
            return (TDataProcessNewCategory)entity;
        }

        protected TDataProcessNewCategoryCB Downcast(ConditionBean cb) {
            return (TDataProcessNewCategoryCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDataProcessNewCategoryDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
