
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
    public partial class TDeleteDataBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDeleteDataDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDeleteDataBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_DATA"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDeleteDataDbm.GetInstance(); } }
        public TDeleteDataDbm MyDBMeta { get { return TDeleteDataDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDeleteData NewMyEntity() { return new TDeleteData(); }
        public virtual TDeleteDataCB NewMyConditionBean() { return new TDeleteDataCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDeleteDataCB cb) {
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
        public virtual TDeleteData SelectEntity(TDeleteDataCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDeleteData> ls = null;
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
            return (TDeleteData)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDeleteData SelectEntityWithDeletedCheck(TDeleteDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDeleteData entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDeleteData SelectByPKValue(decimal? dataEditId) {
            return SelectEntity(BuildPKCB(dataEditId));
        }

        public virtual TDeleteData SelectByPKValueWithDeletedCheck(decimal? dataEditId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(dataEditId));
        }

        private TDeleteDataCB BuildPKCB(decimal? dataEditId) {
            AssertObjectNotNull("dataEditId", dataEditId);
            TDeleteDataCB cb = NewMyConditionBean();
            cb.Query().SetDataEditId_Equal(dataEditId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDeleteData> SelectList(TDeleteDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDeleteData>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDeleteData> SelectPage(TDeleteDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDeleteData> invoker = new PagingInvoker<TDeleteData>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDeleteData> {
            protected TDeleteDataBhv _bhv; protected TDeleteDataCB _cb;
            public InternalSelectPagingHandler(TDeleteDataBhv bhv, TDeleteDataCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDeleteData> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDeleteData myEntity = (TDeleteData)entity;
            myEntity.DataEditId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDeleteConditionList(TDeleteData tDeleteData, ConditionBeanSetupper<TDeleteConditionCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDeleteData", tDeleteData); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDeleteConditionList(xnewLRLs<TDeleteData>(tDeleteData), conditionBeanSetupper);
        }
        public virtual void LoadTDeleteConditionList(IList<TDeleteData> tDeleteDataList, ConditionBeanSetupper<TDeleteConditionCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDeleteDataList", tDeleteDataList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDeleteConditionList(tDeleteDataList, new LoadReferrerOption<TDeleteConditionCB, TDeleteCondition>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDeleteConditionList(TDeleteData tDeleteData, LoadReferrerOption<TDeleteConditionCB, TDeleteCondition> loadReferrerOption) {
            AssertObjectNotNull("tDeleteData", tDeleteData); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDeleteConditionList(xnewLRLs<TDeleteData>(tDeleteData), loadReferrerOption);
        }
        public virtual void LoadTDeleteConditionList(IList<TDeleteData> tDeleteDataList, LoadReferrerOption<TDeleteConditionCB, TDeleteCondition> loadReferrerOption) {
            AssertObjectNotNull("tDeleteDataList", tDeleteDataList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDeleteDataList.Count == 0) { return; }
            TDeleteConditionBhv referrerBhv = xgetBSFLR().Select<TDeleteConditionBhv>();
            HelpLoadReferrerInternally<TDeleteData, decimal?, TDeleteConditionCB, TDeleteCondition>
                    (tDeleteDataList, loadReferrerOption, new MyInternalLoadTDeleteConditionListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDeleteConditionListCallback : InternalLoadReferrerCallback<TDeleteData, decimal?, TDeleteConditionCB, TDeleteCondition> {
            protected TDeleteConditionBhv referrerBhv;
            public MyInternalLoadTDeleteConditionListCallback(TDeleteConditionBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDeleteData e) { return e.DataEditId; }
            public void setRfLs(TDeleteData e, IList<TDeleteCondition> ls) { e.TDeleteConditionList = ls; }
            public TDeleteConditionCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDeleteConditionCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TDeleteConditionCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TDeleteConditionCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TDeleteCondition> selRfLs(TDeleteConditionCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDeleteCondition e) { return e.DataEditId; }
            public void setlcEt(TDeleteCondition re, TDeleteData be) { re.TDeleteData = be; }
        }
        public virtual void LoadTDeleteSampleIdListList(TDeleteData tDeleteData, ConditionBeanSetupper<TDeleteSampleIdListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDeleteData", tDeleteData); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDeleteSampleIdListList(xnewLRLs<TDeleteData>(tDeleteData), conditionBeanSetupper);
        }
        public virtual void LoadTDeleteSampleIdListList(IList<TDeleteData> tDeleteDataList, ConditionBeanSetupper<TDeleteSampleIdListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDeleteDataList", tDeleteDataList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDeleteSampleIdListList(tDeleteDataList, new LoadReferrerOption<TDeleteSampleIdListCB, TDeleteSampleIdList>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDeleteSampleIdListList(TDeleteData tDeleteData, LoadReferrerOption<TDeleteSampleIdListCB, TDeleteSampleIdList> loadReferrerOption) {
            AssertObjectNotNull("tDeleteData", tDeleteData); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDeleteSampleIdListList(xnewLRLs<TDeleteData>(tDeleteData), loadReferrerOption);
        }
        public virtual void LoadTDeleteSampleIdListList(IList<TDeleteData> tDeleteDataList, LoadReferrerOption<TDeleteSampleIdListCB, TDeleteSampleIdList> loadReferrerOption) {
            AssertObjectNotNull("tDeleteDataList", tDeleteDataList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDeleteDataList.Count == 0) { return; }
            TDeleteSampleIdListBhv referrerBhv = xgetBSFLR().Select<TDeleteSampleIdListBhv>();
            HelpLoadReferrerInternally<TDeleteData, decimal?, TDeleteSampleIdListCB, TDeleteSampleIdList>
                    (tDeleteDataList, loadReferrerOption, new MyInternalLoadTDeleteSampleIdListListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDeleteSampleIdListListCallback : InternalLoadReferrerCallback<TDeleteData, decimal?, TDeleteSampleIdListCB, TDeleteSampleIdList> {
            protected TDeleteSampleIdListBhv referrerBhv;
            public MyInternalLoadTDeleteSampleIdListListCallback(TDeleteSampleIdListBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDeleteData e) { return e.DataEditId; }
            public void setRfLs(TDeleteData e, IList<TDeleteSampleIdList> ls) { e.TDeleteSampleIdListList = ls; }
            public TDeleteSampleIdListCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDeleteSampleIdListCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TDeleteSampleIdListCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TDeleteSampleIdListCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TDeleteSampleIdList> selRfLs(TDeleteSampleIdListCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDeleteSampleIdList e) { return e.DataEditId; }
            public void setlcEt(TDeleteSampleIdList re, TDeleteData be) { re.TDeleteData = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TDataEditList> PulloutTDataEditList(IList<TDeleteData> tDeleteDataList) {
            return HelpPulloutInternally<TDeleteData, TDataEditList>(tDeleteDataList, new MyInternalPulloutTDataEditListCallback());
        }
        protected class MyInternalPulloutTDataEditListCallback : InternalPulloutCallback<TDeleteData, TDataEditList> {
            public TDataEditList getFr(TDeleteData entity) { return entity.TDataEditList; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDeleteData entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDeleteData entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDeleteData entity) {
            HelpInsertOrUpdateInternally<TDeleteData, TDeleteDataCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDeleteData, TDeleteDataCB> {
            protected TDeleteDataBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDeleteDataBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDeleteData entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDeleteData entity) { _bhv.Update(entity); }
            public TDeleteDataCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDeleteDataCB cb, TDeleteData entity) {
                cb.Query().SetDataEditId_Equal(entity.DataEditId);
            }
            public int CallbackSelectCount(TDeleteDataCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDeleteData entity) {
            HelpDeleteInternally<TDeleteData>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDeleteData> {
            protected TDeleteDataBhv _bhv;
            public MyInternalDeleteCallback(TDeleteDataBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDeleteData entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDeleteData tDeleteData, TDeleteDataCB cb) {
            AssertObjectNotNull("tDeleteData", tDeleteData); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDeleteData);
            FilterEntityOfUpdate(tDeleteData); AssertEntityOfUpdate(tDeleteData);
            return this.Dao.UpdateByQuery(cb, tDeleteData);
        }

        public int QueryDelete(TDeleteDataCB cb) {
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
        protected int DelegateSelectCount(TDeleteDataCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDeleteData> DelegateSelectList(TDeleteDataCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDeleteData e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDeleteData e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDeleteData e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDeleteData Downcast(Entity entity) {
            return (TDeleteData)entity;
        }

        protected TDeleteDataCB Downcast(ConditionBean cb) {
            return (TDeleteDataCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDeleteDataDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
