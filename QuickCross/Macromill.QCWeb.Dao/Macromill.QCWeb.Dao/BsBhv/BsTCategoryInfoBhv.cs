
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
    public partial class TCategoryInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCategoryInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCategoryInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TCategoryInfoDbm.GetInstance(); } }
        public TCategoryInfoDbm MyDBMeta { get { return TCategoryInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TCategoryInfo NewMyEntity() { return new TCategoryInfo(); }
        public virtual TCategoryInfoCB NewMyConditionBean() { return new TCategoryInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TCategoryInfoCB cb) {
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
        public virtual TCategoryInfo SelectEntity(TCategoryInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TCategoryInfo> ls = null;
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
            return (TCategoryInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TCategoryInfo SelectEntityWithDeletedCheck(TCategoryInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TCategoryInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TCategoryInfo SelectByPKValue(decimal? categoryInfoId) {
            return SelectEntity(BuildPKCB(categoryInfoId));
        }

        public virtual TCategoryInfo SelectByPKValueWithDeletedCheck(decimal? categoryInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(categoryInfoId));
        }

        private TCategoryInfoCB BuildPKCB(decimal? categoryInfoId) {
            AssertObjectNotNull("categoryInfoId", categoryInfoId);
            TCategoryInfoCB cb = NewMyConditionBean();
            cb.Query().SetCategoryInfoId_Equal(categoryInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TCategoryInfo> SelectList(TCategoryInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TCategoryInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TCategoryInfo> SelectPage(TCategoryInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TCategoryInfo> invoker = new PagingInvoker<TCategoryInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TCategoryInfo> {
            protected TCategoryInfoBhv _bhv; protected TCategoryInfoCB _cb;
            public InternalSelectPagingHandler(TCategoryInfoBhv bhv, TCategoryInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TCategoryInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TCategoryInfo myEntity = (TCategoryInfo)entity;
            myEntity.CategoryInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTMatrixInfoList(TCategoryInfo tCategoryInfo, ConditionBeanSetupper<TMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCategoryInfo", tCategoryInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTMatrixInfoList(xnewLRLs<TCategoryInfo>(tCategoryInfo), conditionBeanSetupper);
        }
        public virtual void LoadTMatrixInfoList(IList<TCategoryInfo> tCategoryInfoList, ConditionBeanSetupper<TMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCategoryInfoList", tCategoryInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTMatrixInfoList(tCategoryInfoList, new LoadReferrerOption<TMatrixInfoCB, TMatrixInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTMatrixInfoList(TCategoryInfo tCategoryInfo, LoadReferrerOption<TMatrixInfoCB, TMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tCategoryInfo", tCategoryInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTMatrixInfoList(xnewLRLs<TCategoryInfo>(tCategoryInfo), loadReferrerOption);
        }
        public virtual void LoadTMatrixInfoList(IList<TCategoryInfo> tCategoryInfoList, LoadReferrerOption<TMatrixInfoCB, TMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tCategoryInfoList", tCategoryInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tCategoryInfoList.Count == 0) { return; }
            TMatrixInfoBhv referrerBhv = xgetBSFLR().Select<TMatrixInfoBhv>();
            HelpLoadReferrerInternally<TCategoryInfo, decimal?, TMatrixInfoCB, TMatrixInfo>
                    (tCategoryInfoList, loadReferrerOption, new MyInternalLoadTMatrixInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTMatrixInfoListCallback : InternalLoadReferrerCallback<TCategoryInfo, decimal?, TMatrixInfoCB, TMatrixInfo> {
            protected TMatrixInfoBhv referrerBhv;
            public MyInternalLoadTMatrixInfoListCallback(TMatrixInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TCategoryInfo e) { return e.CategoryInfoId; }
            public void setRfLs(TCategoryInfo e, IList<TMatrixInfo> ls) { e.TMatrixInfoList = ls; }
            public TMatrixInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TMatrixInfoCB cb, IList<decimal?> ls) { cb.Query().SetAddFaCategoryInfoId_InScope(ls); }
            public void qyOdFKAsc(TMatrixInfoCB cb) { cb.Query().AddOrderBy_AddFaCategoryInfoId_Asc(); }
            public void spFKCol(TMatrixInfoCB cb) { cb.Specify().ColumnAddFaCategoryInfoId(); }
            public IList<TMatrixInfo> selRfLs(TMatrixInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TMatrixInfo e) { return e.AddFaCategoryInfoId; }
            public void setlcEt(TMatrixInfo re, TCategoryInfo be) { re.TCategoryInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TItemInfo> PulloutTItemInfo(IList<TCategoryInfo> tCategoryInfoList) {
            return HelpPulloutInternally<TCategoryInfo, TItemInfo>(tCategoryInfoList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TCategoryInfo, TItemInfo> {
            public TItemInfo getFr(TCategoryInfo entity) { return entity.TItemInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TCategoryInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TCategoryInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TCategoryInfo entity) {
            HelpInsertOrUpdateInternally<TCategoryInfo, TCategoryInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TCategoryInfo, TCategoryInfoCB> {
            protected TCategoryInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TCategoryInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TCategoryInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TCategoryInfo entity) { _bhv.Update(entity); }
            public TCategoryInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TCategoryInfoCB cb, TCategoryInfo entity) {
                cb.Query().SetCategoryInfoId_Equal(entity.CategoryInfoId);
            }
            public int CallbackSelectCount(TCategoryInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TCategoryInfo entity) {
            HelpDeleteInternally<TCategoryInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TCategoryInfo> {
            protected TCategoryInfoBhv _bhv;
            public MyInternalDeleteCallback(TCategoryInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TCategoryInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TCategoryInfo tCategoryInfo, TCategoryInfoCB cb) {
            AssertObjectNotNull("tCategoryInfo", tCategoryInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tCategoryInfo);
            FilterEntityOfUpdate(tCategoryInfo); AssertEntityOfUpdate(tCategoryInfo);
            return this.Dao.UpdateByQuery(cb, tCategoryInfo);
        }

        public int QueryDelete(TCategoryInfoCB cb) {
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
        protected int DelegateSelectCount(TCategoryInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TCategoryInfo> DelegateSelectList(TCategoryInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TCategoryInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TCategoryInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TCategoryInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TCategoryInfo Downcast(Entity entity) {
            return (TCategoryInfo)entity;
        }

        protected TCategoryInfoCB Downcast(ConditionBean cb) {
            return (TCategoryInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TCategoryInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
