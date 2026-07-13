
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
    public partial class TOutputRequestBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>出力物削除処理に先立ち、出力物作成依頼情報を検索 </summary>
        public static readonly String PATH_selectTarget = "selectTarget";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputRequestDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputRequestBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_REQUEST"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputRequestDbm.GetInstance(); } }
        public TOutputRequestDbm MyDBMeta { get { return TOutputRequestDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputRequest NewMyEntity() { return new TOutputRequest(); }
        public virtual TOutputRequestCB NewMyConditionBean() { return new TOutputRequestCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputRequestCB cb) {
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
        public virtual TOutputRequest SelectEntity(TOutputRequestCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputRequest> ls = null;
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
            return (TOutputRequest)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputRequest SelectEntityWithDeletedCheck(TOutputRequestCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputRequest entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputRequest SelectByPKValue(decimal? outputRequestId) {
            return SelectEntity(BuildPKCB(outputRequestId));
        }

        public virtual TOutputRequest SelectByPKValueWithDeletedCheck(decimal? outputRequestId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputRequestId));
        }

        private TOutputRequestCB BuildPKCB(decimal? outputRequestId) {
            AssertObjectNotNull("outputRequestId", outputRequestId);
            TOutputRequestCB cb = NewMyConditionBean();
            cb.Query().SetOutputRequestId_Equal(outputRequestId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputRequest> SelectList(TOutputRequestCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputRequest>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputRequest> SelectPage(TOutputRequestCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputRequest> invoker = new PagingInvoker<TOutputRequest>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputRequest> {
            protected TOutputRequestBhv _bhv; protected TOutputRequestCB _cb;
            public InternalSelectPagingHandler(TOutputRequestBhv bhv, TOutputRequestCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputRequest> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputRequest myEntity = (TOutputRequest)entity;
            myEntity.OutputRequestId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTOutputCommonList(TOutputRequest tOutputRequest, ConditionBeanSetupper<TOutputCommonCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputRequest", tOutputRequest); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputCommonList(xnewLRLs<TOutputRequest>(tOutputRequest), conditionBeanSetupper);
        }
        public virtual void LoadTOutputCommonList(IList<TOutputRequest> tOutputRequestList, ConditionBeanSetupper<TOutputCommonCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputRequestList", tOutputRequestList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputCommonList(tOutputRequestList, new LoadReferrerOption<TOutputCommonCB, TOutputCommon>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputCommonList(TOutputRequest tOutputRequest, LoadReferrerOption<TOutputCommonCB, TOutputCommon> loadReferrerOption) {
            AssertObjectNotNull("tOutputRequest", tOutputRequest); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputCommonList(xnewLRLs<TOutputRequest>(tOutputRequest), loadReferrerOption);
        }
        public virtual void LoadTOutputCommonList(IList<TOutputRequest> tOutputRequestList, LoadReferrerOption<TOutputCommonCB, TOutputCommon> loadReferrerOption) {
            AssertObjectNotNull("tOutputRequestList", tOutputRequestList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputRequestList.Count == 0) { return; }
            TOutputCommonBhv referrerBhv = xgetBSFLR().Select<TOutputCommonBhv>();
            HelpLoadReferrerInternally<TOutputRequest, decimal?, TOutputCommonCB, TOutputCommon>
                    (tOutputRequestList, loadReferrerOption, new MyInternalLoadTOutputCommonListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputCommonListCallback : InternalLoadReferrerCallback<TOutputRequest, decimal?, TOutputCommonCB, TOutputCommon> {
            protected TOutputCommonBhv referrerBhv;
            public MyInternalLoadTOutputCommonListCallback(TOutputCommonBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputRequest e) { return e.OutputRequestId; }
            public void setRfLs(TOutputRequest e, IList<TOutputCommon> ls) { e.TOutputCommonList = ls; }
            public TOutputCommonCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputCommonCB cb, IList<decimal?> ls) { cb.Query().SetOutputRequestId_InScope(ls); }
            public void qyOdFKAsc(TOutputCommonCB cb) { cb.Query().AddOrderBy_OutputRequestId_Asc(); }
            public void spFKCol(TOutputCommonCB cb) { cb.Specify().ColumnOutputRequestId(); }
            public IList<TOutputCommon> selRfLs(TOutputCommonCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputCommon e) { return e.OutputRequestId; }
            public void setlcEt(TOutputCommon re, TOutputRequest be) { re.TOutputRequest = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TOutputRequest> tOutputRequestList) {
            return HelpPulloutInternally<TOutputRequest, TQcwebSurveyInfo>(tOutputRequestList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TOutputRequest, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TOutputRequest entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TOutputReportsetInfo> PulloutTOutputReportsetInfo(IList<TOutputRequest> tOutputRequestList) {
            return HelpPulloutInternally<TOutputRequest, TOutputReportsetInfo>(tOutputRequestList, new MyInternalPulloutTOutputReportsetInfoCallback());
        }
        protected class MyInternalPulloutTOutputReportsetInfoCallback : InternalPulloutCallback<TOutputRequest, TOutputReportsetInfo> {
            public TOutputReportsetInfo getFr(TOutputRequest entity) { return entity.TOutputReportsetInfo; }
        }
        public IList<TOutputCommon> PulloutTOutputCommon(IList<TOutputRequest> tOutputRequestList) {
            return HelpPulloutInternally<TOutputRequest, TOutputCommon>(tOutputRequestList, new MyInternalPulloutTOutputCommonCallback());
        }
        protected class MyInternalPulloutTOutputCommonCallback : InternalPulloutCallback<TOutputRequest, TOutputCommon> {
            public TOutputCommon getFr(TOutputRequest entity) { return entity.TOutputCommon; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputRequest entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputRequest entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputRequest entity) {
            HelpInsertOrUpdateInternally<TOutputRequest, TOutputRequestCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputRequest, TOutputRequestCB> {
            protected TOutputRequestBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputRequestBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputRequest entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputRequest entity) { _bhv.Update(entity); }
            public TOutputRequestCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputRequestCB cb, TOutputRequest entity) {
                cb.Query().SetOutputRequestId_Equal(entity.OutputRequestId);
            }
            public int CallbackSelectCount(TOutputRequestCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputRequest entity) {
            HelpDeleteInternally<TOutputRequest>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputRequest> {
            protected TOutputRequestBhv _bhv;
            public MyInternalDeleteCallback(TOutputRequestBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputRequest entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputRequest tOutputRequest, TOutputRequestCB cb) {
            AssertObjectNotNull("tOutputRequest", tOutputRequest); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputRequest);
            FilterEntityOfUpdate(tOutputRequest); AssertEntityOfUpdate(tOutputRequest);
            return this.Dao.UpdateByQuery(cb, tOutputRequest);
        }

        public int QueryDelete(TOutputRequestCB cb) {
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
        protected int DelegateSelectCount(TOutputRequestCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputRequest> DelegateSelectList(TOutputRequestCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputRequest e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputRequest e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputRequest e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputRequest Downcast(Entity entity) {
            return (TOutputRequest)entity;
        }

        protected TOutputRequestCB Downcast(ConditionBean cb) {
            return (TOutputRequestCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputRequestDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
