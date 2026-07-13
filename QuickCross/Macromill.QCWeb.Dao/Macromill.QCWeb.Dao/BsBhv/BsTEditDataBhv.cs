
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
    public partial class TEditDataBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TEditDataDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TEditDataBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_DATA"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TEditDataDbm.GetInstance(); } }
        public TEditDataDbm MyDBMeta { get { return TEditDataDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TEditData NewMyEntity() { return new TEditData(); }
        public virtual TEditDataCB NewMyConditionBean() { return new TEditDataCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TEditDataCB cb) {
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
        public virtual TEditData SelectEntity(TEditDataCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TEditData> ls = null;
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
            return (TEditData)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TEditData SelectEntityWithDeletedCheck(TEditDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TEditData entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TEditData SelectByPKValue(decimal? dataEditId) {
            return SelectEntity(BuildPKCB(dataEditId));
        }

        public virtual TEditData SelectByPKValueWithDeletedCheck(decimal? dataEditId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(dataEditId));
        }

        private TEditDataCB BuildPKCB(decimal? dataEditId) {
            AssertObjectNotNull("dataEditId", dataEditId);
            TEditDataCB cb = NewMyConditionBean();
            cb.Query().SetDataEditId_Equal(dataEditId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TEditData> SelectList(TEditDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TEditData>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TEditData> SelectPage(TEditDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TEditData> invoker = new PagingInvoker<TEditData>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TEditData> {
            protected TEditDataBhv _bhv; protected TEditDataCB _cb;
            public InternalSelectPagingHandler(TEditDataBhv bhv, TEditDataCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TEditData> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTEditConditionList(TEditData tEditData, ConditionBeanSetupper<TEditConditionCB> conditionBeanSetupper) {
            AssertObjectNotNull("tEditData", tEditData); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTEditConditionList(xnewLRLs<TEditData>(tEditData), conditionBeanSetupper);
        }
        public virtual void LoadTEditConditionList(IList<TEditData> tEditDataList, ConditionBeanSetupper<TEditConditionCB> conditionBeanSetupper) {
            AssertObjectNotNull("tEditDataList", tEditDataList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTEditConditionList(tEditDataList, new LoadReferrerOption<TEditConditionCB, TEditCondition>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTEditConditionList(TEditData tEditData, LoadReferrerOption<TEditConditionCB, TEditCondition> loadReferrerOption) {
            AssertObjectNotNull("tEditData", tEditData); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTEditConditionList(xnewLRLs<TEditData>(tEditData), loadReferrerOption);
        }
        public virtual void LoadTEditConditionList(IList<TEditData> tEditDataList, LoadReferrerOption<TEditConditionCB, TEditCondition> loadReferrerOption) {
            AssertObjectNotNull("tEditDataList", tEditDataList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tEditDataList.Count == 0) { return; }
            TEditConditionBhv referrerBhv = xgetBSFLR().Select<TEditConditionBhv>();
            HelpLoadReferrerInternally<TEditData, decimal?, TEditConditionCB, TEditCondition>
                    (tEditDataList, loadReferrerOption, new MyInternalLoadTEditConditionListCallback(referrerBhv));
        }
        protected class MyInternalLoadTEditConditionListCallback : InternalLoadReferrerCallback<TEditData, decimal?, TEditConditionCB, TEditCondition> {
            protected TEditConditionBhv referrerBhv;
            public MyInternalLoadTEditConditionListCallback(TEditConditionBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TEditData e) { return e.DataEditId; }
            public void setRfLs(TEditData e, IList<TEditCondition> ls) { e.TEditConditionList = ls; }
            public TEditConditionCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TEditConditionCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TEditConditionCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TEditConditionCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TEditCondition> selRfLs(TEditConditionCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TEditCondition e) { return e.DataEditId; }
            public void setlcEt(TEditCondition re, TEditData be) { re.TEditData = be; }
        }
        public virtual void LoadTEditTargetItemList(TEditData tEditData, ConditionBeanSetupper<TEditTargetItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tEditData", tEditData); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTEditTargetItemList(xnewLRLs<TEditData>(tEditData), conditionBeanSetupper);
        }
        public virtual void LoadTEditTargetItemList(IList<TEditData> tEditDataList, ConditionBeanSetupper<TEditTargetItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tEditDataList", tEditDataList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTEditTargetItemList(tEditDataList, new LoadReferrerOption<TEditTargetItemCB, TEditTargetItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTEditTargetItemList(TEditData tEditData, LoadReferrerOption<TEditTargetItemCB, TEditTargetItem> loadReferrerOption) {
            AssertObjectNotNull("tEditData", tEditData); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTEditTargetItemList(xnewLRLs<TEditData>(tEditData), loadReferrerOption);
        }
        public virtual void LoadTEditTargetItemList(IList<TEditData> tEditDataList, LoadReferrerOption<TEditTargetItemCB, TEditTargetItem> loadReferrerOption) {
            AssertObjectNotNull("tEditDataList", tEditDataList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tEditDataList.Count == 0) { return; }
            TEditTargetItemBhv referrerBhv = xgetBSFLR().Select<TEditTargetItemBhv>();
            HelpLoadReferrerInternally<TEditData, decimal?, TEditTargetItemCB, TEditTargetItem>
                    (tEditDataList, loadReferrerOption, new MyInternalLoadTEditTargetItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTEditTargetItemListCallback : InternalLoadReferrerCallback<TEditData, decimal?, TEditTargetItemCB, TEditTargetItem> {
            protected TEditTargetItemBhv referrerBhv;
            public MyInternalLoadTEditTargetItemListCallback(TEditTargetItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TEditData e) { return e.DataEditId; }
            public void setRfLs(TEditData e, IList<TEditTargetItem> ls) { e.TEditTargetItemList = ls; }
            public TEditTargetItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TEditTargetItemCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TEditTargetItemCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TEditTargetItemCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TEditTargetItem> selRfLs(TEditTargetItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TEditTargetItem e) { return e.DataEditId; }
            public void setlcEt(TEditTargetItem re, TEditData be) { re.TEditData = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TDataEditList> PulloutTDataEditList(IList<TEditData> tEditDataList) {
            return HelpPulloutInternally<TEditData, TDataEditList>(tEditDataList, new MyInternalPulloutTDataEditListCallback());
        }
        protected class MyInternalPulloutTDataEditListCallback : InternalPulloutCallback<TEditData, TDataEditList> {
            public TDataEditList getFr(TEditData entity) { return entity.TDataEditList; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TEditData entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TEditData entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TEditData entity) {
            HelpInsertOrUpdateInternally<TEditData, TEditDataCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TEditData, TEditDataCB> {
            protected TEditDataBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TEditDataBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TEditData entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TEditData entity) { _bhv.Update(entity); }
            public TEditDataCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TEditDataCB cb, TEditData entity) {
                cb.Query().SetDataEditId_Equal(entity.DataEditId);
            }
            public int CallbackSelectCount(TEditDataCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TEditData entity) {
            HelpDeleteInternally<TEditData>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TEditData> {
            protected TEditDataBhv _bhv;
            public MyInternalDeleteCallback(TEditDataBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TEditData entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TEditData tEditData, TEditDataCB cb) {
            AssertObjectNotNull("tEditData", tEditData); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tEditData);
            FilterEntityOfUpdate(tEditData); AssertEntityOfUpdate(tEditData);
            return this.Dao.UpdateByQuery(cb, tEditData);
        }

        public int QueryDelete(TEditDataCB cb) {
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
        protected int DelegateSelectCount(TEditDataCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TEditData> DelegateSelectList(TEditDataCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TEditData e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TEditData e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TEditData e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TEditData Downcast(Entity entity) {
            return (TEditData)entity;
        }

        protected TEditDataCB Downcast(ConditionBean cb) {
            return (TEditDataCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TEditDataDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
