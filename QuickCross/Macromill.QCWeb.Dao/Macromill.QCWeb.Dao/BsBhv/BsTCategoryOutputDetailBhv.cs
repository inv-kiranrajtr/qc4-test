
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
    public partial class TCategoryOutputDetailBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCategoryOutputDetailDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCategoryOutputDetailBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_OUTPUT_DETAIL"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TCategoryOutputDetailDbm.GetInstance(); } }
        public TCategoryOutputDetailDbm MyDBMeta { get { return TCategoryOutputDetailDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TCategoryOutputDetail NewMyEntity() { return new TCategoryOutputDetail(); }
        public virtual TCategoryOutputDetailCB NewMyConditionBean() { return new TCategoryOutputDetailCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TCategoryOutputDetailCB cb) {
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
        public virtual TCategoryOutputDetail SelectEntity(TCategoryOutputDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TCategoryOutputDetail> ls = null;
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
            return (TCategoryOutputDetail)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TCategoryOutputDetail SelectEntityWithDeletedCheck(TCategoryOutputDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TCategoryOutputDetail entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TCategoryOutputDetail SelectByPKValue(decimal? categoryOutputEditDetailId) {
            return SelectEntity(BuildPKCB(categoryOutputEditDetailId));
        }

        public virtual TCategoryOutputDetail SelectByPKValueWithDeletedCheck(decimal? categoryOutputEditDetailId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(categoryOutputEditDetailId));
        }

        private TCategoryOutputDetailCB BuildPKCB(decimal? categoryOutputEditDetailId) {
            AssertObjectNotNull("categoryOutputEditDetailId", categoryOutputEditDetailId);
            TCategoryOutputDetailCB cb = NewMyConditionBean();
            cb.Query().SetCategoryOutputEditDetailId_Equal(categoryOutputEditDetailId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TCategoryOutputDetail> SelectList(TCategoryOutputDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TCategoryOutputDetail>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TCategoryOutputDetail> SelectPage(TCategoryOutputDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TCategoryOutputDetail> invoker = new PagingInvoker<TCategoryOutputDetail>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TCategoryOutputDetail> {
            protected TCategoryOutputDetailBhv _bhv; protected TCategoryOutputDetailCB _cb;
            public InternalSelectPagingHandler(TCategoryOutputDetailBhv bhv, TCategoryOutputDetailCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TCategoryOutputDetail> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TCategoryOutputDetail myEntity = (TCategoryOutputDetail)entity;
            myEntity.CategoryOutputEditDetailId = SelectNextVal();
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
        public IList<TCategoryOutputEdit> PulloutTCategoryOutputEdit(IList<TCategoryOutputDetail> tCategoryOutputDetailList) {
            return HelpPulloutInternally<TCategoryOutputDetail, TCategoryOutputEdit>(tCategoryOutputDetailList, new MyInternalPulloutTCategoryOutputEditCallback());
        }
        protected class MyInternalPulloutTCategoryOutputEditCallback : InternalPulloutCallback<TCategoryOutputDetail, TCategoryOutputEdit> {
            public TCategoryOutputEdit getFr(TCategoryOutputDetail entity) { return entity.TCategoryOutputEdit; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TCategoryOutputDetail entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TCategoryOutputDetail entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TCategoryOutputDetail entity) {
            HelpInsertOrUpdateInternally<TCategoryOutputDetail, TCategoryOutputDetailCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TCategoryOutputDetail, TCategoryOutputDetailCB> {
            protected TCategoryOutputDetailBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TCategoryOutputDetailBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TCategoryOutputDetail entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TCategoryOutputDetail entity) { _bhv.Update(entity); }
            public TCategoryOutputDetailCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TCategoryOutputDetailCB cb, TCategoryOutputDetail entity) {
                cb.Query().SetCategoryOutputEditDetailId_Equal(entity.CategoryOutputEditDetailId);
            }
            public int CallbackSelectCount(TCategoryOutputDetailCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TCategoryOutputDetail entity) {
            HelpDeleteInternally<TCategoryOutputDetail>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TCategoryOutputDetail> {
            protected TCategoryOutputDetailBhv _bhv;
            public MyInternalDeleteCallback(TCategoryOutputDetailBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TCategoryOutputDetail entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TCategoryOutputDetail tCategoryOutputDetail, TCategoryOutputDetailCB cb) {
            AssertObjectNotNull("tCategoryOutputDetail", tCategoryOutputDetail); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tCategoryOutputDetail);
            FilterEntityOfUpdate(tCategoryOutputDetail); AssertEntityOfUpdate(tCategoryOutputDetail);
            return this.Dao.UpdateByQuery(cb, tCategoryOutputDetail);
        }

        public int QueryDelete(TCategoryOutputDetailCB cb) {
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
        protected int DelegateSelectCount(TCategoryOutputDetailCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TCategoryOutputDetail> DelegateSelectList(TCategoryOutputDetailCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TCategoryOutputDetail e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TCategoryOutputDetail e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TCategoryOutputDetail e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TCategoryOutputDetail Downcast(Entity entity) {
            return (TCategoryOutputDetail)entity;
        }

        protected TCategoryOutputDetailCB Downcast(ConditionBean cb) {
            return (TCategoryOutputDetailCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TCategoryOutputDetailDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
