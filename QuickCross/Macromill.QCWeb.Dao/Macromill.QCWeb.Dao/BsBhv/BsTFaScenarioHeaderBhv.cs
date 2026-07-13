
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
    public partial class TFaScenarioHeaderBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TFaScenarioHeaderDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TFaScenarioHeaderBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_SCENARIO_HEADER"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TFaScenarioHeaderDbm.GetInstance(); } }
        public TFaScenarioHeaderDbm MyDBMeta { get { return TFaScenarioHeaderDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TFaScenarioHeader NewMyEntity() { return new TFaScenarioHeader(); }
        public virtual TFaScenarioHeaderCB NewMyConditionBean() { return new TFaScenarioHeaderCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TFaScenarioHeaderCB cb) {
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
        public virtual TFaScenarioHeader SelectEntity(TFaScenarioHeaderCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TFaScenarioHeader> ls = null;
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
            return (TFaScenarioHeader)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TFaScenarioHeader SelectEntityWithDeletedCheck(TFaScenarioHeaderCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TFaScenarioHeader entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TFaScenarioHeader SelectByPKValue(decimal? faScenarioHeaderId) {
            return SelectEntity(BuildPKCB(faScenarioHeaderId));
        }

        public virtual TFaScenarioHeader SelectByPKValueWithDeletedCheck(decimal? faScenarioHeaderId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(faScenarioHeaderId));
        }

        private TFaScenarioHeaderCB BuildPKCB(decimal? faScenarioHeaderId) {
            AssertObjectNotNull("faScenarioHeaderId", faScenarioHeaderId);
            TFaScenarioHeaderCB cb = NewMyConditionBean();
            cb.Query().SetFaScenarioHeaderId_Equal(faScenarioHeaderId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TFaScenarioHeader> SelectList(TFaScenarioHeaderCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TFaScenarioHeader>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TFaScenarioHeader> SelectPage(TFaScenarioHeaderCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TFaScenarioHeader> invoker = new PagingInvoker<TFaScenarioHeader>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TFaScenarioHeader> {
            protected TFaScenarioHeaderBhv _bhv; protected TFaScenarioHeaderCB _cb;
            public InternalSelectPagingHandler(TFaScenarioHeaderBhv bhv, TFaScenarioHeaderCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TFaScenarioHeader> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TFaScenarioHeader myEntity = (TFaScenarioHeader)entity;
            myEntity.FaScenarioHeaderId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTFaListAddItemList(TFaScenarioHeader tFaScenarioHeader, ConditionBeanSetupper<TFaListAddItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tFaScenarioHeader", tFaScenarioHeader); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaListAddItemList(xnewLRLs<TFaScenarioHeader>(tFaScenarioHeader), conditionBeanSetupper);
        }
        public virtual void LoadTFaListAddItemList(IList<TFaScenarioHeader> tFaScenarioHeaderList, ConditionBeanSetupper<TFaListAddItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tFaScenarioHeaderList", tFaScenarioHeaderList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaListAddItemList(tFaScenarioHeaderList, new LoadReferrerOption<TFaListAddItemCB, TFaListAddItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTFaListAddItemList(TFaScenarioHeader tFaScenarioHeader, LoadReferrerOption<TFaListAddItemCB, TFaListAddItem> loadReferrerOption) {
            AssertObjectNotNull("tFaScenarioHeader", tFaScenarioHeader); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTFaListAddItemList(xnewLRLs<TFaScenarioHeader>(tFaScenarioHeader), loadReferrerOption);
        }
        public virtual void LoadTFaListAddItemList(IList<TFaScenarioHeader> tFaScenarioHeaderList, LoadReferrerOption<TFaListAddItemCB, TFaListAddItem> loadReferrerOption) {
            AssertObjectNotNull("tFaScenarioHeaderList", tFaScenarioHeaderList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tFaScenarioHeaderList.Count == 0) { return; }
            TFaListAddItemBhv referrerBhv = xgetBSFLR().Select<TFaListAddItemBhv>();
            HelpLoadReferrerInternally<TFaScenarioHeader, decimal?, TFaListAddItemCB, TFaListAddItem>
                    (tFaScenarioHeaderList, loadReferrerOption, new MyInternalLoadTFaListAddItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTFaListAddItemListCallback : InternalLoadReferrerCallback<TFaScenarioHeader, decimal?, TFaListAddItemCB, TFaListAddItem> {
            protected TFaListAddItemBhv referrerBhv;
            public MyInternalLoadTFaListAddItemListCallback(TFaListAddItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TFaScenarioHeader e) { return e.FaScenarioHeaderId; }
            public void setRfLs(TFaScenarioHeader e, IList<TFaListAddItem> ls) { e.TFaListAddItemList = ls; }
            public TFaListAddItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TFaListAddItemCB cb, IList<decimal?> ls) { cb.Query().SetFaScenarioHeaderId_InScope(ls); }
            public void qyOdFKAsc(TFaListAddItemCB cb) { cb.Query().AddOrderBy_FaScenarioHeaderId_Asc(); }
            public void spFKCol(TFaListAddItemCB cb) { cb.Specify().ColumnFaScenarioHeaderId(); }
            public IList<TFaListAddItem> selRfLs(TFaListAddItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TFaListAddItem e) { return e.FaScenarioHeaderId; }
            public void setlcEt(TFaListAddItem re, TFaScenarioHeader be) { re.TFaScenarioHeader = be; }
        }
        public virtual void LoadTFaScenarioItemList(TFaScenarioHeader tFaScenarioHeader, ConditionBeanSetupper<TFaScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tFaScenarioHeader", tFaScenarioHeader); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaScenarioItemList(xnewLRLs<TFaScenarioHeader>(tFaScenarioHeader), conditionBeanSetupper);
        }
        public virtual void LoadTFaScenarioItemList(IList<TFaScenarioHeader> tFaScenarioHeaderList, ConditionBeanSetupper<TFaScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tFaScenarioHeaderList", tFaScenarioHeaderList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaScenarioItemList(tFaScenarioHeaderList, new LoadReferrerOption<TFaScenarioItemCB, TFaScenarioItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTFaScenarioItemList(TFaScenarioHeader tFaScenarioHeader, LoadReferrerOption<TFaScenarioItemCB, TFaScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tFaScenarioHeader", tFaScenarioHeader); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTFaScenarioItemList(xnewLRLs<TFaScenarioHeader>(tFaScenarioHeader), loadReferrerOption);
        }
        public virtual void LoadTFaScenarioItemList(IList<TFaScenarioHeader> tFaScenarioHeaderList, LoadReferrerOption<TFaScenarioItemCB, TFaScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tFaScenarioHeaderList", tFaScenarioHeaderList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tFaScenarioHeaderList.Count == 0) { return; }
            TFaScenarioItemBhv referrerBhv = xgetBSFLR().Select<TFaScenarioItemBhv>();
            HelpLoadReferrerInternally<TFaScenarioHeader, decimal?, TFaScenarioItemCB, TFaScenarioItem>
                    (tFaScenarioHeaderList, loadReferrerOption, new MyInternalLoadTFaScenarioItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTFaScenarioItemListCallback : InternalLoadReferrerCallback<TFaScenarioHeader, decimal?, TFaScenarioItemCB, TFaScenarioItem> {
            protected TFaScenarioItemBhv referrerBhv;
            public MyInternalLoadTFaScenarioItemListCallback(TFaScenarioItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TFaScenarioHeader e) { return e.FaScenarioHeaderId; }
            public void setRfLs(TFaScenarioHeader e, IList<TFaScenarioItem> ls) { e.TFaScenarioItemList = ls; }
            public TFaScenarioItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TFaScenarioItemCB cb, IList<decimal?> ls) { cb.Query().SetFaScenarioHeaderId_InScope(ls); }
            public void qyOdFKAsc(TFaScenarioItemCB cb) { cb.Query().AddOrderBy_FaScenarioHeaderId_Asc(); }
            public void spFKCol(TFaScenarioItemCB cb) { cb.Specify().ColumnFaScenarioHeaderId(); }
            public IList<TFaScenarioItem> selRfLs(TFaScenarioItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TFaScenarioItem e) { return e.FaScenarioHeaderId; }
            public void setlcEt(TFaScenarioItem re, TFaScenarioHeader be) { re.TFaScenarioHeader = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TFaScenarioHeader> tFaScenarioHeaderList) {
            return HelpPulloutInternally<TFaScenarioHeader, TScenarioTotalization>(tFaScenarioHeaderList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TFaScenarioHeader, TScenarioTotalization> {
            public TScenarioTotalization getFr(TFaScenarioHeader entity) { return entity.TScenarioTotalization; }
        }
        public IList<TFaScenarioItem> PulloutTFaScenarioItem(IList<TFaScenarioHeader> tFaScenarioHeaderList) {
            return HelpPulloutInternally<TFaScenarioHeader, TFaScenarioItem>(tFaScenarioHeaderList, new MyInternalPulloutTFaScenarioItemCallback());
        }
        protected class MyInternalPulloutTFaScenarioItemCallback : InternalPulloutCallback<TFaScenarioHeader, TFaScenarioItem> {
            public TFaScenarioItem getFr(TFaScenarioHeader entity) { return entity.TFaScenarioItem; }
        }
        public IList<TFaListAddItem> PulloutTFaListAddItem(IList<TFaScenarioHeader> tFaScenarioHeaderList) {
            return HelpPulloutInternally<TFaScenarioHeader, TFaListAddItem>(tFaScenarioHeaderList, new MyInternalPulloutTFaListAddItemCallback());
        }
        protected class MyInternalPulloutTFaListAddItemCallback : InternalPulloutCallback<TFaScenarioHeader, TFaListAddItem> {
            public TFaListAddItem getFr(TFaScenarioHeader entity) { return entity.TFaListAddItem; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TFaScenarioHeader entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TFaScenarioHeader entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TFaScenarioHeader entity) {
            HelpInsertOrUpdateInternally<TFaScenarioHeader, TFaScenarioHeaderCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TFaScenarioHeader, TFaScenarioHeaderCB> {
            protected TFaScenarioHeaderBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TFaScenarioHeaderBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TFaScenarioHeader entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TFaScenarioHeader entity) { _bhv.Update(entity); }
            public TFaScenarioHeaderCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TFaScenarioHeaderCB cb, TFaScenarioHeader entity) {
                cb.Query().SetFaScenarioHeaderId_Equal(entity.FaScenarioHeaderId);
            }
            public int CallbackSelectCount(TFaScenarioHeaderCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TFaScenarioHeader entity) {
            HelpDeleteInternally<TFaScenarioHeader>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TFaScenarioHeader> {
            protected TFaScenarioHeaderBhv _bhv;
            public MyInternalDeleteCallback(TFaScenarioHeaderBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TFaScenarioHeader entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TFaScenarioHeader tFaScenarioHeader, TFaScenarioHeaderCB cb) {
            AssertObjectNotNull("tFaScenarioHeader", tFaScenarioHeader); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tFaScenarioHeader);
            FilterEntityOfUpdate(tFaScenarioHeader); AssertEntityOfUpdate(tFaScenarioHeader);
            return this.Dao.UpdateByQuery(cb, tFaScenarioHeader);
        }

        public int QueryDelete(TFaScenarioHeaderCB cb) {
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
        protected int DelegateSelectCount(TFaScenarioHeaderCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TFaScenarioHeader> DelegateSelectList(TFaScenarioHeaderCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TFaScenarioHeader e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TFaScenarioHeader e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TFaScenarioHeader e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TFaScenarioHeader Downcast(Entity entity) {
            return (TFaScenarioHeader)entity;
        }

        protected TFaScenarioHeaderCB Downcast(ConditionBean cb) {
            return (TFaScenarioHeaderCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TFaScenarioHeaderDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
