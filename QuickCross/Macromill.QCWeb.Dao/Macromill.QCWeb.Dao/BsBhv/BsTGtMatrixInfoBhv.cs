
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
    public partial class TGtMatrixInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TGtMatrixInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TGtMatrixInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_GT_MATRIX_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TGtMatrixInfoDbm.GetInstance(); } }
        public TGtMatrixInfoDbm MyDBMeta { get { return TGtMatrixInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TGtMatrixInfo NewMyEntity() { return new TGtMatrixInfo(); }
        public virtual TGtMatrixInfoCB NewMyConditionBean() { return new TGtMatrixInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TGtMatrixInfoCB cb) {
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
        public virtual TGtMatrixInfo SelectEntity(TGtMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TGtMatrixInfo> ls = null;
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
            return (TGtMatrixInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TGtMatrixInfo SelectEntityWithDeletedCheck(TGtMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TGtMatrixInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TGtMatrixInfo SelectByPKValue(decimal? gtMatrixInfoId) {
            return SelectEntity(BuildPKCB(gtMatrixInfoId));
        }

        public virtual TGtMatrixInfo SelectByPKValueWithDeletedCheck(decimal? gtMatrixInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(gtMatrixInfoId));
        }

        private TGtMatrixInfoCB BuildPKCB(decimal? gtMatrixInfoId) {
            AssertObjectNotNull("gtMatrixInfoId", gtMatrixInfoId);
            TGtMatrixInfoCB cb = NewMyConditionBean();
            cb.Query().SetGtMatrixInfoId_Equal(gtMatrixInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TGtMatrixInfo> SelectList(TGtMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TGtMatrixInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TGtMatrixInfo> SelectPage(TGtMatrixInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TGtMatrixInfo> invoker = new PagingInvoker<TGtMatrixInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TGtMatrixInfo> {
            protected TGtMatrixInfoBhv _bhv; protected TGtMatrixInfoCB _cb;
            public InternalSelectPagingHandler(TGtMatrixInfoBhv bhv, TGtMatrixInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TGtMatrixInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TGtMatrixInfo myEntity = (TGtMatrixInfo)entity;
            myEntity.GtMatrixInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTGtMatrixChildList(TGtMatrixInfo tGtMatrixInfo, ConditionBeanSetupper<TGtMatrixChildCB> conditionBeanSetupper) {
            AssertObjectNotNull("tGtMatrixInfo", tGtMatrixInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtMatrixChildList(xnewLRLs<TGtMatrixInfo>(tGtMatrixInfo), conditionBeanSetupper);
        }
        public virtual void LoadTGtMatrixChildList(IList<TGtMatrixInfo> tGtMatrixInfoList, ConditionBeanSetupper<TGtMatrixChildCB> conditionBeanSetupper) {
            AssertObjectNotNull("tGtMatrixInfoList", tGtMatrixInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtMatrixChildList(tGtMatrixInfoList, new LoadReferrerOption<TGtMatrixChildCB, TGtMatrixChild>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTGtMatrixChildList(TGtMatrixInfo tGtMatrixInfo, LoadReferrerOption<TGtMatrixChildCB, TGtMatrixChild> loadReferrerOption) {
            AssertObjectNotNull("tGtMatrixInfo", tGtMatrixInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTGtMatrixChildList(xnewLRLs<TGtMatrixInfo>(tGtMatrixInfo), loadReferrerOption);
        }
        public virtual void LoadTGtMatrixChildList(IList<TGtMatrixInfo> tGtMatrixInfoList, LoadReferrerOption<TGtMatrixChildCB, TGtMatrixChild> loadReferrerOption) {
            AssertObjectNotNull("tGtMatrixInfoList", tGtMatrixInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tGtMatrixInfoList.Count == 0) { return; }
            TGtMatrixChildBhv referrerBhv = xgetBSFLR().Select<TGtMatrixChildBhv>();
            HelpLoadReferrerInternally<TGtMatrixInfo, decimal?, TGtMatrixChildCB, TGtMatrixChild>
                    (tGtMatrixInfoList, loadReferrerOption, new MyInternalLoadTGtMatrixChildListCallback(referrerBhv));
        }
        protected class MyInternalLoadTGtMatrixChildListCallback : InternalLoadReferrerCallback<TGtMatrixInfo, decimal?, TGtMatrixChildCB, TGtMatrixChild> {
            protected TGtMatrixChildBhv referrerBhv;
            public MyInternalLoadTGtMatrixChildListCallback(TGtMatrixChildBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TGtMatrixInfo e) { return e.GtMatrixInfoId; }
            public void setRfLs(TGtMatrixInfo e, IList<TGtMatrixChild> ls) { e.TGtMatrixChildList = ls; }
            public TGtMatrixChildCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TGtMatrixChildCB cb, IList<decimal?> ls) { cb.Query().SetGtMatrixInfoId_InScope(ls); }
            public void qyOdFKAsc(TGtMatrixChildCB cb) { cb.Query().AddOrderBy_GtMatrixInfoId_Asc(); }
            public void spFKCol(TGtMatrixChildCB cb) { cb.Specify().ColumnGtMatrixInfoId(); }
            public IList<TGtMatrixChild> selRfLs(TGtMatrixChildCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TGtMatrixChild e) { return e.GtMatrixInfoId; }
            public void setlcEt(TGtMatrixChild re, TGtMatrixInfo be) { re.TGtMatrixInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TGtMatrixInfo> tGtMatrixInfoList) {
            return HelpPulloutInternally<TGtMatrixInfo, TScenarioTotalization>(tGtMatrixInfoList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TGtMatrixInfo, TScenarioTotalization> {
            public TScenarioTotalization getFr(TGtMatrixInfo entity) { return entity.TScenarioTotalization; }
        }
        public IList<TGtMatrixChild> PulloutTGtMatrixChild(IList<TGtMatrixInfo> tGtMatrixInfoList) {
            return HelpPulloutInternally<TGtMatrixInfo, TGtMatrixChild>(tGtMatrixInfoList, new MyInternalPulloutTGtMatrixChildCallback());
        }
        protected class MyInternalPulloutTGtMatrixChildCallback : InternalPulloutCallback<TGtMatrixInfo, TGtMatrixChild> {
            public TGtMatrixChild getFr(TGtMatrixInfo entity) { return entity.TGtMatrixChild; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TGtMatrixInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TGtMatrixInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TGtMatrixInfo entity) {
            HelpInsertOrUpdateInternally<TGtMatrixInfo, TGtMatrixInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TGtMatrixInfo, TGtMatrixInfoCB> {
            protected TGtMatrixInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TGtMatrixInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TGtMatrixInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TGtMatrixInfo entity) { _bhv.Update(entity); }
            public TGtMatrixInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TGtMatrixInfoCB cb, TGtMatrixInfo entity) {
                cb.Query().SetGtMatrixInfoId_Equal(entity.GtMatrixInfoId);
            }
            public int CallbackSelectCount(TGtMatrixInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TGtMatrixInfo entity) {
            HelpDeleteInternally<TGtMatrixInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TGtMatrixInfo> {
            protected TGtMatrixInfoBhv _bhv;
            public MyInternalDeleteCallback(TGtMatrixInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TGtMatrixInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TGtMatrixInfo tGtMatrixInfo, TGtMatrixInfoCB cb) {
            AssertObjectNotNull("tGtMatrixInfo", tGtMatrixInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tGtMatrixInfo);
            FilterEntityOfUpdate(tGtMatrixInfo); AssertEntityOfUpdate(tGtMatrixInfo);
            return this.Dao.UpdateByQuery(cb, tGtMatrixInfo);
        }

        public int QueryDelete(TGtMatrixInfoCB cb) {
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
        protected int DelegateSelectCount(TGtMatrixInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TGtMatrixInfo> DelegateSelectList(TGtMatrixInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TGtMatrixInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TGtMatrixInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TGtMatrixInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TGtMatrixInfo Downcast(Entity entity) {
            return (TGtMatrixInfo)entity;
        }

        protected TGtMatrixInfoCB Downcast(ConditionBean cb) {
            return (TGtMatrixInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TGtMatrixInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
