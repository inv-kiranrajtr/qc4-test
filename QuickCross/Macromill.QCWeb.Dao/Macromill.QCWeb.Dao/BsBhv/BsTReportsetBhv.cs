
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
    public partial class TReportsetBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TReportsetDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TReportsetBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORTSET"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TReportsetDbm.GetInstance(); } }
        public TReportsetDbm MyDBMeta { get { return TReportsetDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TReportset NewMyEntity() { return new TReportset(); }
        public virtual TReportsetCB NewMyConditionBean() { return new TReportsetCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TReportsetCB cb) {
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
        public virtual TReportset SelectEntity(TReportsetCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TReportset> ls = null;
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
            return (TReportset)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TReportset SelectEntityWithDeletedCheck(TReportsetCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TReportset entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TReportset SelectByPKValue(decimal? reportsetId) {
            return SelectEntity(BuildPKCB(reportsetId));
        }

        public virtual TReportset SelectByPKValueWithDeletedCheck(decimal? reportsetId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(reportsetId));
        }

        private TReportsetCB BuildPKCB(decimal? reportsetId) {
            AssertObjectNotNull("reportsetId", reportsetId);
            TReportsetCB cb = NewMyConditionBean();
            cb.Query().SetReportsetId_Equal(reportsetId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TReportset> SelectList(TReportsetCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TReportset>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TReportset> SelectPage(TReportsetCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TReportset> invoker = new PagingInvoker<TReportset>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TReportset> {
            protected TReportsetBhv _bhv; protected TReportsetCB _cb;
            public InternalSelectPagingHandler(TReportsetBhv bhv, TReportsetCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TReportset> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TReportset myEntity = (TReportset)entity;
            myEntity.ReportsetId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTReportList(TReportset tReportset, ConditionBeanSetupper<TReportCB> conditionBeanSetupper) {
            AssertObjectNotNull("tReportset", tReportset); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTReportList(xnewLRLs<TReportset>(tReportset), conditionBeanSetupper);
        }
        public virtual void LoadTReportList(IList<TReportset> tReportsetList, ConditionBeanSetupper<TReportCB> conditionBeanSetupper) {
            AssertObjectNotNull("tReportsetList", tReportsetList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTReportList(tReportsetList, new LoadReferrerOption<TReportCB, TReport>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTReportList(TReportset tReportset, LoadReferrerOption<TReportCB, TReport> loadReferrerOption) {
            AssertObjectNotNull("tReportset", tReportset); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTReportList(xnewLRLs<TReportset>(tReportset), loadReferrerOption);
        }
        public virtual void LoadTReportList(IList<TReportset> tReportsetList, LoadReferrerOption<TReportCB, TReport> loadReferrerOption) {
            AssertObjectNotNull("tReportsetList", tReportsetList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tReportsetList.Count == 0) { return; }
            TReportBhv referrerBhv = xgetBSFLR().Select<TReportBhv>();
            HelpLoadReferrerInternally<TReportset, decimal?, TReportCB, TReport>
                    (tReportsetList, loadReferrerOption, new MyInternalLoadTReportListCallback(referrerBhv));
        }
        protected class MyInternalLoadTReportListCallback : InternalLoadReferrerCallback<TReportset, decimal?, TReportCB, TReport> {
            protected TReportBhv referrerBhv;
            public MyInternalLoadTReportListCallback(TReportBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TReportset e) { return e.ReportsetId; }
            public void setRfLs(TReportset e, IList<TReport> ls) { e.TReportList = ls; }
            public TReportCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TReportCB cb, IList<decimal?> ls) { cb.Query().SetReportsetId_InScope(ls); }
            public void qyOdFKAsc(TReportCB cb) { cb.Query().AddOrderBy_ReportsetId_Asc(); }
            public void spFKCol(TReportCB cb) { cb.Specify().ColumnReportsetId(); }
            public IList<TReport> selRfLs(TReportCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TReport e) { return e.ReportsetId; }
            public void setlcEt(TReport re, TReportset be) { re.TReportset = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TReportset> tReportsetList) {
            return HelpPulloutInternally<TReportset, TQcwebSurveyInfo>(tReportsetList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TReportset, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TReportset entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TReport> PulloutTReport(IList<TReportset> tReportsetList) {
            return HelpPulloutInternally<TReportset, TReport>(tReportsetList, new MyInternalPulloutTReportCallback());
        }
        protected class MyInternalPulloutTReportCallback : InternalPulloutCallback<TReportset, TReport> {
            public TReport getFr(TReportset entity) { return entity.TReport; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TReportset entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TReportset entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TReportset entity) {
            HelpInsertOrUpdateInternally<TReportset, TReportsetCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TReportset, TReportsetCB> {
            protected TReportsetBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TReportsetBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TReportset entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TReportset entity) { _bhv.Update(entity); }
            public TReportsetCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TReportsetCB cb, TReportset entity) {
                cb.Query().SetReportsetId_Equal(entity.ReportsetId);
            }
            public int CallbackSelectCount(TReportsetCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TReportset entity) {
            HelpDeleteInternally<TReportset>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TReportset> {
            protected TReportsetBhv _bhv;
            public MyInternalDeleteCallback(TReportsetBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TReportset entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TReportset tReportset, TReportsetCB cb) {
            AssertObjectNotNull("tReportset", tReportset); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tReportset);
            FilterEntityOfUpdate(tReportset); AssertEntityOfUpdate(tReportset);
            return this.Dao.UpdateByQuery(cb, tReportset);
        }

        public int QueryDelete(TReportsetCB cb) {
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
        protected int DelegateSelectCount(TReportsetCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TReportset> DelegateSelectList(TReportsetCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TReportset e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TReportset e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TReportset e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TReportset Downcast(Entity entity) {
            return (TReportset)entity;
        }

        protected TReportsetCB Downcast(ConditionBean cb) {
            return (TReportsetCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TReportsetDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
