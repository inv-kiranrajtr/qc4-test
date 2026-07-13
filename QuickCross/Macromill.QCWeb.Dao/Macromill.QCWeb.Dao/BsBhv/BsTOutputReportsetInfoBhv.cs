
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
    public partial class TOutputReportsetInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>出力物レポートセット情報テーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /// <summary>出力物レポートセット情報テーブルの削除 </summary>
        public static readonly String PATH_OutputDelete = "OutputDelete";
        /// <summary>出力物の作成依頼キューの取得 </summary>
        public static readonly String PATH_SelectRequestInfo = "SelectRequestInfo";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputReportsetInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputReportsetInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_REPORTSET_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputReportsetInfoDbm.GetInstance(); } }
        public TOutputReportsetInfoDbm MyDBMeta { get { return TOutputReportsetInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputReportsetInfo NewMyEntity() { return new TOutputReportsetInfo(); }
        public virtual TOutputReportsetInfoCB NewMyConditionBean() { return new TOutputReportsetInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputReportsetInfoCB cb) {
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
        public virtual TOutputReportsetInfo SelectEntity(TOutputReportsetInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputReportsetInfo> ls = null;
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
            return (TOutputReportsetInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputReportsetInfo SelectEntityWithDeletedCheck(TOutputReportsetInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputReportsetInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputReportsetInfo SelectByPKValue(decimal? outputReportsetInfoId) {
            return SelectEntity(BuildPKCB(outputReportsetInfoId));
        }

        public virtual TOutputReportsetInfo SelectByPKValueWithDeletedCheck(decimal? outputReportsetInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputReportsetInfoId));
        }

        private TOutputReportsetInfoCB BuildPKCB(decimal? outputReportsetInfoId) {
            AssertObjectNotNull("outputReportsetInfoId", outputReportsetInfoId);
            TOutputReportsetInfoCB cb = NewMyConditionBean();
            cb.Query().SetOutputReportsetInfoId_Equal(outputReportsetInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputReportsetInfo> SelectList(TOutputReportsetInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputReportsetInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputReportsetInfo> SelectPage(TOutputReportsetInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputReportsetInfo> invoker = new PagingInvoker<TOutputReportsetInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputReportsetInfo> {
            protected TOutputReportsetInfoBhv _bhv; protected TOutputReportsetInfoCB _cb;
            public InternalSelectPagingHandler(TOutputReportsetInfoBhv bhv, TOutputReportsetInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputReportsetInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputReportsetInfo myEntity = (TOutputReportsetInfo)entity;
            myEntity.OutputReportsetInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTOutputRequestList(TOutputReportsetInfo tOutputReportsetInfo, ConditionBeanSetupper<TOutputRequestCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputReportsetInfo", tOutputReportsetInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputRequestList(xnewLRLs<TOutputReportsetInfo>(tOutputReportsetInfo), conditionBeanSetupper);
        }
        public virtual void LoadTOutputRequestList(IList<TOutputReportsetInfo> tOutputReportsetInfoList, ConditionBeanSetupper<TOutputRequestCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputReportsetInfoList", tOutputReportsetInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputRequestList(tOutputReportsetInfoList, new LoadReferrerOption<TOutputRequestCB, TOutputRequest>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputRequestList(TOutputReportsetInfo tOutputReportsetInfo, LoadReferrerOption<TOutputRequestCB, TOutputRequest> loadReferrerOption) {
            AssertObjectNotNull("tOutputReportsetInfo", tOutputReportsetInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputRequestList(xnewLRLs<TOutputReportsetInfo>(tOutputReportsetInfo), loadReferrerOption);
        }
        public virtual void LoadTOutputRequestList(IList<TOutputReportsetInfo> tOutputReportsetInfoList, LoadReferrerOption<TOutputRequestCB, TOutputRequest> loadReferrerOption) {
            AssertObjectNotNull("tOutputReportsetInfoList", tOutputReportsetInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputReportsetInfoList.Count == 0) { return; }
            TOutputRequestBhv referrerBhv = xgetBSFLR().Select<TOutputRequestBhv>();
            HelpLoadReferrerInternally<TOutputReportsetInfo, decimal?, TOutputRequestCB, TOutputRequest>
                    (tOutputReportsetInfoList, loadReferrerOption, new MyInternalLoadTOutputRequestListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputRequestListCallback : InternalLoadReferrerCallback<TOutputReportsetInfo, decimal?, TOutputRequestCB, TOutputRequest> {
            protected TOutputRequestBhv referrerBhv;
            public MyInternalLoadTOutputRequestListCallback(TOutputRequestBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputReportsetInfo e) { return e.OutputReportsetInfoId; }
            public void setRfLs(TOutputReportsetInfo e, IList<TOutputRequest> ls) { e.TOutputRequestList = ls; }
            public TOutputRequestCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputRequestCB cb, IList<decimal?> ls) { cb.Query().SetOutputReportsetInfoId_InScope(ls); }
            public void qyOdFKAsc(TOutputRequestCB cb) { cb.Query().AddOrderBy_OutputReportsetInfoId_Asc(); }
            public void spFKCol(TOutputRequestCB cb) { cb.Specify().ColumnOutputReportsetInfoId(); }
            public IList<TOutputRequest> selRfLs(TOutputRequestCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputRequest e) { return e.OutputReportsetInfoId; }
            public void setlcEt(TOutputRequest re, TOutputReportsetInfo be) { re.TOutputReportsetInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TOutputTemplate> PulloutTOutputTemplate(IList<TOutputReportsetInfo> tOutputReportsetInfoList) {
            return HelpPulloutInternally<TOutputReportsetInfo, TOutputTemplate>(tOutputReportsetInfoList, new MyInternalPulloutTOutputTemplateCallback());
        }
        protected class MyInternalPulloutTOutputTemplateCallback : InternalPulloutCallback<TOutputReportsetInfo, TOutputTemplate> {
            public TOutputTemplate getFr(TOutputReportsetInfo entity) { return entity.TOutputTemplate; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputReportsetInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputReportsetInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputReportsetInfo entity) {
            HelpInsertOrUpdateInternally<TOutputReportsetInfo, TOutputReportsetInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputReportsetInfo, TOutputReportsetInfoCB> {
            protected TOutputReportsetInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputReportsetInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputReportsetInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputReportsetInfo entity) { _bhv.Update(entity); }
            public TOutputReportsetInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputReportsetInfoCB cb, TOutputReportsetInfo entity) {
                cb.Query().SetOutputReportsetInfoId_Equal(entity.OutputReportsetInfoId);
            }
            public int CallbackSelectCount(TOutputReportsetInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputReportsetInfo entity) {
            HelpDeleteInternally<TOutputReportsetInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputReportsetInfo> {
            protected TOutputReportsetInfoBhv _bhv;
            public MyInternalDeleteCallback(TOutputReportsetInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputReportsetInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputReportsetInfo tOutputReportsetInfo, TOutputReportsetInfoCB cb) {
            AssertObjectNotNull("tOutputReportsetInfo", tOutputReportsetInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputReportsetInfo);
            FilterEntityOfUpdate(tOutputReportsetInfo); AssertEntityOfUpdate(tOutputReportsetInfo);
            return this.Dao.UpdateByQuery(cb, tOutputReportsetInfo);
        }

        public int QueryDelete(TOutputReportsetInfoCB cb) {
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
        protected int DelegateSelectCount(TOutputReportsetInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputReportsetInfo> DelegateSelectList(TOutputReportsetInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputReportsetInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputReportsetInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputReportsetInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputReportsetInfo Downcast(Entity entity) {
            return (TOutputReportsetInfo)entity;
        }

        protected TOutputReportsetInfoCB Downcast(ConditionBean cb) {
            return (TOutputReportsetInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputReportsetInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
