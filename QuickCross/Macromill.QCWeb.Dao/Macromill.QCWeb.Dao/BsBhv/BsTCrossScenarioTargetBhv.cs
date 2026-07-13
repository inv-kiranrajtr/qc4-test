
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
    public partial class TCrossScenarioTargetBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>クロス集計対象シナリオアイテムテーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCrossScenarioTargetDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCrossScenarioTargetBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CROSS_SCENARIO_TARGET"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TCrossScenarioTargetDbm.GetInstance(); } }
        public TCrossScenarioTargetDbm MyDBMeta { get { return TCrossScenarioTargetDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TCrossScenarioTarget NewMyEntity() { return new TCrossScenarioTarget(); }
        public virtual TCrossScenarioTargetCB NewMyConditionBean() { return new TCrossScenarioTargetCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TCrossScenarioTargetCB cb) {
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
        public virtual TCrossScenarioTarget SelectEntity(TCrossScenarioTargetCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TCrossScenarioTarget> ls = null;
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
            return (TCrossScenarioTarget)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TCrossScenarioTarget SelectEntityWithDeletedCheck(TCrossScenarioTargetCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TCrossScenarioTarget entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TCrossScenarioTarget SelectByPKValue(decimal? crossScenarioTargetId) {
            return SelectEntity(BuildPKCB(crossScenarioTargetId));
        }

        public virtual TCrossScenarioTarget SelectByPKValueWithDeletedCheck(decimal? crossScenarioTargetId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(crossScenarioTargetId));
        }

        private TCrossScenarioTargetCB BuildPKCB(decimal? crossScenarioTargetId) {
            AssertObjectNotNull("crossScenarioTargetId", crossScenarioTargetId);
            TCrossScenarioTargetCB cb = NewMyConditionBean();
            cb.Query().SetCrossScenarioTargetId_Equal(crossScenarioTargetId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TCrossScenarioTarget> SelectList(TCrossScenarioTargetCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TCrossScenarioTarget>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TCrossScenarioTarget> SelectPage(TCrossScenarioTargetCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TCrossScenarioTarget> invoker = new PagingInvoker<TCrossScenarioTarget>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TCrossScenarioTarget> {
            protected TCrossScenarioTargetBhv _bhv; protected TCrossScenarioTargetCB _cb;
            public InternalSelectPagingHandler(TCrossScenarioTargetBhv bhv, TCrossScenarioTargetCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TCrossScenarioTarget> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TCrossScenarioTarget myEntity = (TCrossScenarioTarget)entity;
            myEntity.CrossScenarioTargetId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTColorSetInfoCrossList(TCrossScenarioTarget tCrossScenarioTarget, ConditionBeanSetupper<TColorSetInfoCrossCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCrossScenarioTarget", tCrossScenarioTarget); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorSetInfoCrossList(xnewLRLs<TCrossScenarioTarget>(tCrossScenarioTarget), conditionBeanSetupper);
        }
        public virtual void LoadTColorSetInfoCrossList(IList<TCrossScenarioTarget> tCrossScenarioTargetList, ConditionBeanSetupper<TColorSetInfoCrossCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCrossScenarioTargetList", tCrossScenarioTargetList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorSetInfoCrossList(tCrossScenarioTargetList, new LoadReferrerOption<TColorSetInfoCrossCB, TColorSetInfoCross>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTColorSetInfoCrossList(TCrossScenarioTarget tCrossScenarioTarget, LoadReferrerOption<TColorSetInfoCrossCB, TColorSetInfoCross> loadReferrerOption) {
            AssertObjectNotNull("tCrossScenarioTarget", tCrossScenarioTarget); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTColorSetInfoCrossList(xnewLRLs<TCrossScenarioTarget>(tCrossScenarioTarget), loadReferrerOption);
        }
        public virtual void LoadTColorSetInfoCrossList(IList<TCrossScenarioTarget> tCrossScenarioTargetList, LoadReferrerOption<TColorSetInfoCrossCB, TColorSetInfoCross> loadReferrerOption) {
            AssertObjectNotNull("tCrossScenarioTargetList", tCrossScenarioTargetList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tCrossScenarioTargetList.Count == 0) { return; }
            TColorSetInfoCrossBhv referrerBhv = xgetBSFLR().Select<TColorSetInfoCrossBhv>();
            HelpLoadReferrerInternally<TCrossScenarioTarget, decimal?, TColorSetInfoCrossCB, TColorSetInfoCross>
                    (tCrossScenarioTargetList, loadReferrerOption, new MyInternalLoadTColorSetInfoCrossListCallback(referrerBhv));
        }
        protected class MyInternalLoadTColorSetInfoCrossListCallback : InternalLoadReferrerCallback<TCrossScenarioTarget, decimal?, TColorSetInfoCrossCB, TColorSetInfoCross> {
            protected TColorSetInfoCrossBhv referrerBhv;
            public MyInternalLoadTColorSetInfoCrossListCallback(TColorSetInfoCrossBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TCrossScenarioTarget e) { return e.CrossScenarioTargetId; }
            public void setRfLs(TCrossScenarioTarget e, IList<TColorSetInfoCross> ls) { e.TColorSetInfoCrossList = ls; }
            public TColorSetInfoCrossCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TColorSetInfoCrossCB cb, IList<decimal?> ls) { cb.Query().SetCrossScenarioTargetId_InScope(ls); }
            public void qyOdFKAsc(TColorSetInfoCrossCB cb) { cb.Query().AddOrderBy_CrossScenarioTargetId_Asc(); }
            public void spFKCol(TColorSetInfoCrossCB cb) { cb.Specify().ColumnCrossScenarioTargetId(); }
            public IList<TColorSetInfoCross> selRfLs(TColorSetInfoCrossCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TColorSetInfoCross e) { return e.CrossScenarioTargetId; }
            public void setlcEt(TColorSetInfoCross re, TCrossScenarioTarget be) { re.TCrossScenarioTarget = be; }
        }
        public virtual void LoadTCrossScenarioItemList(TCrossScenarioTarget tCrossScenarioTarget, ConditionBeanSetupper<TCrossScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCrossScenarioTarget", tCrossScenarioTarget); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCrossScenarioItemList(xnewLRLs<TCrossScenarioTarget>(tCrossScenarioTarget), conditionBeanSetupper);
        }
        public virtual void LoadTCrossScenarioItemList(IList<TCrossScenarioTarget> tCrossScenarioTargetList, ConditionBeanSetupper<TCrossScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCrossScenarioTargetList", tCrossScenarioTargetList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCrossScenarioItemList(tCrossScenarioTargetList, new LoadReferrerOption<TCrossScenarioItemCB, TCrossScenarioItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTCrossScenarioItemList(TCrossScenarioTarget tCrossScenarioTarget, LoadReferrerOption<TCrossScenarioItemCB, TCrossScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tCrossScenarioTarget", tCrossScenarioTarget); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTCrossScenarioItemList(xnewLRLs<TCrossScenarioTarget>(tCrossScenarioTarget), loadReferrerOption);
        }
        public virtual void LoadTCrossScenarioItemList(IList<TCrossScenarioTarget> tCrossScenarioTargetList, LoadReferrerOption<TCrossScenarioItemCB, TCrossScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tCrossScenarioTargetList", tCrossScenarioTargetList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tCrossScenarioTargetList.Count == 0) { return; }
            TCrossScenarioItemBhv referrerBhv = xgetBSFLR().Select<TCrossScenarioItemBhv>();
            HelpLoadReferrerInternally<TCrossScenarioTarget, decimal?, TCrossScenarioItemCB, TCrossScenarioItem>
                    (tCrossScenarioTargetList, loadReferrerOption, new MyInternalLoadTCrossScenarioItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTCrossScenarioItemListCallback : InternalLoadReferrerCallback<TCrossScenarioTarget, decimal?, TCrossScenarioItemCB, TCrossScenarioItem> {
            protected TCrossScenarioItemBhv referrerBhv;
            public MyInternalLoadTCrossScenarioItemListCallback(TCrossScenarioItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TCrossScenarioTarget e) { return e.CrossScenarioTargetId; }
            public void setRfLs(TCrossScenarioTarget e, IList<TCrossScenarioItem> ls) { e.TCrossScenarioItemList = ls; }
            public TCrossScenarioItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TCrossScenarioItemCB cb, IList<decimal?> ls) { cb.Query().SetCrossScenarioTargetId_InScope(ls); }
            public void qyOdFKAsc(TCrossScenarioItemCB cb) { cb.Query().AddOrderBy_CrossScenarioTargetId_Asc(); }
            public void spFKCol(TCrossScenarioItemCB cb) { cb.Specify().ColumnCrossScenarioTargetId(); }
            public IList<TCrossScenarioItem> selRfLs(TCrossScenarioItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TCrossScenarioItem e) { return e.CrossScenarioTargetId; }
            public void setlcEt(TCrossScenarioItem re, TCrossScenarioTarget be) { re.TCrossScenarioTarget = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TCrossScenarioTarget> tCrossScenarioTargetList) {
            return HelpPulloutInternally<TCrossScenarioTarget, TScenarioTotalization>(tCrossScenarioTargetList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TCrossScenarioTarget, TScenarioTotalization> {
            public TScenarioTotalization getFr(TCrossScenarioTarget entity) { return entity.TScenarioTotalization; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TCrossScenarioTarget entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TCrossScenarioTarget entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TCrossScenarioTarget entity) {
            HelpInsertOrUpdateInternally<TCrossScenarioTarget, TCrossScenarioTargetCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TCrossScenarioTarget, TCrossScenarioTargetCB> {
            protected TCrossScenarioTargetBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TCrossScenarioTargetBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TCrossScenarioTarget entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TCrossScenarioTarget entity) { _bhv.Update(entity); }
            public TCrossScenarioTargetCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TCrossScenarioTargetCB cb, TCrossScenarioTarget entity) {
                cb.Query().SetCrossScenarioTargetId_Equal(entity.CrossScenarioTargetId);
            }
            public int CallbackSelectCount(TCrossScenarioTargetCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TCrossScenarioTarget entity) {
            HelpDeleteInternally<TCrossScenarioTarget>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TCrossScenarioTarget> {
            protected TCrossScenarioTargetBhv _bhv;
            public MyInternalDeleteCallback(TCrossScenarioTargetBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TCrossScenarioTarget entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TCrossScenarioTarget tCrossScenarioTarget, TCrossScenarioTargetCB cb) {
            AssertObjectNotNull("tCrossScenarioTarget", tCrossScenarioTarget); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tCrossScenarioTarget);
            FilterEntityOfUpdate(tCrossScenarioTarget); AssertEntityOfUpdate(tCrossScenarioTarget);
            return this.Dao.UpdateByQuery(cb, tCrossScenarioTarget);
        }

        public int QueryDelete(TCrossScenarioTargetCB cb) {
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
        protected int DelegateSelectCount(TCrossScenarioTargetCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TCrossScenarioTarget> DelegateSelectList(TCrossScenarioTargetCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TCrossScenarioTarget e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TCrossScenarioTarget e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TCrossScenarioTarget e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TCrossScenarioTarget Downcast(Entity entity) {
            return (TCrossScenarioTarget)entity;
        }

        protected TCrossScenarioTargetCB Downcast(ConditionBean cb) {
            return (TCrossScenarioTargetCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TCrossScenarioTargetDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
