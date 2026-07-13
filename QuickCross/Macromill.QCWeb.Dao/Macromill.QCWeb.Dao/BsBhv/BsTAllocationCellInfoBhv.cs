
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
    public partial class TAllocationCellInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>割付セル情報 Delete </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TAllocationCellInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TAllocationCellInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_ALLOCATION_CELL_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TAllocationCellInfoDbm.GetInstance(); } }
        public TAllocationCellInfoDbm MyDBMeta { get { return TAllocationCellInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TAllocationCellInfo NewMyEntity() { return new TAllocationCellInfo(); }
        public virtual TAllocationCellInfoCB NewMyConditionBean() { return new TAllocationCellInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TAllocationCellInfoCB cb) {
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
        public virtual TAllocationCellInfo SelectEntity(TAllocationCellInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TAllocationCellInfo> ls = null;
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
            return (TAllocationCellInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TAllocationCellInfo SelectEntityWithDeletedCheck(TAllocationCellInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TAllocationCellInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TAllocationCellInfo SelectByPKValue(long? allocationCellId, decimal? qcwebid) {
            return SelectEntity(BuildPKCB(allocationCellId, qcwebid));
        }

        public virtual TAllocationCellInfo SelectByPKValueWithDeletedCheck(long? allocationCellId, decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(allocationCellId, qcwebid));
        }

        private TAllocationCellInfoCB BuildPKCB(long? allocationCellId, decimal? qcwebid) {
            AssertObjectNotNull("allocationCellId", allocationCellId);AssertObjectNotNull("qcwebid", qcwebid);
            TAllocationCellInfoCB cb = NewMyConditionBean();
            cb.Query().SetAllocationCellId_Equal(allocationCellId);cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TAllocationCellInfo> SelectList(TAllocationCellInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TAllocationCellInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TAllocationCellInfo> SelectPage(TAllocationCellInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TAllocationCellInfo> invoker = new PagingInvoker<TAllocationCellInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TAllocationCellInfo> {
            protected TAllocationCellInfoBhv _bhv; protected TAllocationCellInfoCB _cb;
            public InternalSelectPagingHandler(TAllocationCellInfoBhv bhv, TAllocationCellInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TAllocationCellInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion


        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TAllocationCellInfo> tAllocationCellInfoList) {
            return HelpPulloutInternally<TAllocationCellInfo, TQcwebSurveyInfo>(tAllocationCellInfoList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TAllocationCellInfo, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TAllocationCellInfo entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfoAsOne(IList<TAllocationCellInfo> tAllocationCellInfoList) {
            return HelpPulloutInternally<TAllocationCellInfo, TQcwebSurveyInfo>(tAllocationCellInfoList, new MyInternalPulloutTQcwebSurveyInfoListCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoListCallback : InternalPulloutCallback<TAllocationCellInfo, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TAllocationCellInfo entity) { return entity.TQcwebSurveyInfoAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TAllocationCellInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TAllocationCellInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TAllocationCellInfo entity) {
            HelpInsertOrUpdateInternally<TAllocationCellInfo, TAllocationCellInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TAllocationCellInfo, TAllocationCellInfoCB> {
            protected TAllocationCellInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TAllocationCellInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TAllocationCellInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TAllocationCellInfo entity) { _bhv.Update(entity); }
            public TAllocationCellInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TAllocationCellInfoCB cb, TAllocationCellInfo entity) {
                cb.Query().SetAllocationCellId_Equal(entity.AllocationCellId);
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TAllocationCellInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TAllocationCellInfo entity) {
            HelpDeleteInternally<TAllocationCellInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TAllocationCellInfo> {
            protected TAllocationCellInfoBhv _bhv;
            public MyInternalDeleteCallback(TAllocationCellInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TAllocationCellInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============

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
        protected int DelegateSelectCount(TAllocationCellInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TAllocationCellInfo> DelegateSelectList(TAllocationCellInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TAllocationCellInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TAllocationCellInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TAllocationCellInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TAllocationCellInfo Downcast(Entity entity) {
            return (TAllocationCellInfo)entity;
        }

        protected TAllocationCellInfoCB Downcast(ConditionBean cb) {
            return (TAllocationCellInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TAllocationCellInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
