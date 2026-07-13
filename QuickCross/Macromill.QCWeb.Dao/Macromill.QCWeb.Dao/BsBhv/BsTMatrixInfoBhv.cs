
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
    public partial class TMatrixInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TMatrixInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TMatrixInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_MATRIX_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TMatrixInfoDbm.GetInstance(); } }
        public TMatrixInfoDbm MyDBMeta { get { return TMatrixInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TMatrixInfo NewMyEntity() { return new TMatrixInfo(); }
        public virtual TMatrixInfoCB NewMyConditionBean() { return new TMatrixInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TMatrixInfoCB cb) {
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
        public virtual TMatrixInfo SelectEntity(TMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TMatrixInfo> ls = null;
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
            return (TMatrixInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TMatrixInfo SelectEntityWithDeletedCheck(TMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TMatrixInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TMatrixInfo SelectByPKValue(decimal? matrixInfoId) {
            return SelectEntity(BuildPKCB(matrixInfoId));
        }

        public virtual TMatrixInfo SelectByPKValueWithDeletedCheck(decimal? matrixInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(matrixInfoId));
        }

        private TMatrixInfoCB BuildPKCB(decimal? matrixInfoId) {
            AssertObjectNotNull("matrixInfoId", matrixInfoId);
            TMatrixInfoCB cb = NewMyConditionBean();
            cb.Query().SetMatrixInfoId_Equal(matrixInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TMatrixInfo> SelectList(TMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TMatrixInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TMatrixInfo> SelectPage(TMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TMatrixInfo> invoker = new PagingInvoker<TMatrixInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TMatrixInfo> {
            protected TMatrixInfoBhv _bhv; protected TMatrixInfoCB _cb;
            public InternalSelectPagingHandler(TMatrixInfoBhv bhv, TMatrixInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TMatrixInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TMatrixInfo myEntity = (TMatrixInfo)entity;
            myEntity.MatrixInfoId = SelectNextVal();
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
        public IList<TItemInfo> PulloutTItemInfoByItemInfoId(IList<TMatrixInfo> tMatrixInfoList) {
            return HelpPulloutInternally<TMatrixInfo, TItemInfo>(tMatrixInfoList, new MyInternalPulloutTItemInfoByItemInfoIdCallback());
        }
        protected class MyInternalPulloutTItemInfoByItemInfoIdCallback : InternalPulloutCallback<TMatrixInfo, TItemInfo> {
            public TItemInfo getFr(TMatrixInfo entity) { return entity.TItemInfoByItemInfoId; }
        }
        public IList<TItemInfo> PulloutTItemInfoByChildItemInfoId(IList<TMatrixInfo> tMatrixInfoList) {
            return HelpPulloutInternally<TMatrixInfo, TItemInfo>(tMatrixInfoList, new MyInternalPulloutTItemInfoByChildItemInfoIdCallback());
        }
        protected class MyInternalPulloutTItemInfoByChildItemInfoIdCallback : InternalPulloutCallback<TMatrixInfo, TItemInfo> {
            public TItemInfo getFr(TMatrixInfo entity) { return entity.TItemInfoByChildItemInfoId; }
        }
        public IList<TCategoryInfo> PulloutTCategoryInfo(IList<TMatrixInfo> tMatrixInfoList) {
            return HelpPulloutInternally<TMatrixInfo, TCategoryInfo>(tMatrixInfoList, new MyInternalPulloutTCategoryInfoCallback());
        }
        protected class MyInternalPulloutTCategoryInfoCallback : InternalPulloutCallback<TMatrixInfo, TCategoryInfo> {
            public TCategoryInfo getFr(TMatrixInfo entity) { return entity.TCategoryInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TMatrixInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TMatrixInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TMatrixInfo entity) {
            HelpInsertOrUpdateInternally<TMatrixInfo, TMatrixInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TMatrixInfo, TMatrixInfoCB> {
            protected TMatrixInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TMatrixInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TMatrixInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TMatrixInfo entity) { _bhv.Update(entity); }
            public TMatrixInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TMatrixInfoCB cb, TMatrixInfo entity) {
                cb.Query().SetMatrixInfoId_Equal(entity.MatrixInfoId);
            }
            public int CallbackSelectCount(TMatrixInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TMatrixInfo entity) {
            HelpDeleteInternally<TMatrixInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TMatrixInfo> {
            protected TMatrixInfoBhv _bhv;
            public MyInternalDeleteCallback(TMatrixInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TMatrixInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TMatrixInfo tMatrixInfo, TMatrixInfoCB cb) {
            AssertObjectNotNull("tMatrixInfo", tMatrixInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tMatrixInfo);
            FilterEntityOfUpdate(tMatrixInfo); AssertEntityOfUpdate(tMatrixInfo);
            return this.Dao.UpdateByQuery(cb, tMatrixInfo);
        }

        public int QueryDelete(TMatrixInfoCB cb) {
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
        protected int DelegateSelectCount(TMatrixInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TMatrixInfo> DelegateSelectList(TMatrixInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TMatrixInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TMatrixInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TMatrixInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TMatrixInfo Downcast(Entity entity) {
            return (TMatrixInfo)entity;
        }

        protected TMatrixInfoCB Downcast(ConditionBean cb) {
            return (TMatrixInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TMatrixInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
