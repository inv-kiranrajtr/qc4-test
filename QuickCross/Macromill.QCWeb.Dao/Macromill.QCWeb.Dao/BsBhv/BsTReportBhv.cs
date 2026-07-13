
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
    public partial class TReportBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TReportDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TReportBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_REPORT"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TReportDbm.GetInstance(); } }
        public TReportDbm MyDBMeta { get { return TReportDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TReport NewMyEntity() { return new TReport(); }
        public virtual TReportCB NewMyConditionBean() { return new TReportCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TReportCB cb) {
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
        public virtual TReport SelectEntity(TReportCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TReport> ls = null;
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
            return (TReport)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TReport SelectEntityWithDeletedCheck(TReportCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TReport entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TReport SelectByPKValue(decimal? reportId) {
            return SelectEntity(BuildPKCB(reportId));
        }

        public virtual TReport SelectByPKValueWithDeletedCheck(decimal? reportId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(reportId));
        }

        private TReportCB BuildPKCB(decimal? reportId) {
            AssertObjectNotNull("reportId", reportId);
            TReportCB cb = NewMyConditionBean();
            cb.Query().SetReportId_Equal(reportId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TReport> SelectList(TReportCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TReport>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TReport> SelectPage(TReportCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TReport> invoker = new PagingInvoker<TReport>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TReport> {
            protected TReportBhv _bhv; protected TReportCB _cb;
            public InternalSelectPagingHandler(TReportBhv bhv, TReportCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TReport> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TReport myEntity = (TReport)entity;
            myEntity.ReportId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTReportChildList(TReport tReport, ConditionBeanSetupper<TReportChildCB> conditionBeanSetupper) {
            AssertObjectNotNull("tReport", tReport); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTReportChildList(xnewLRLs<TReport>(tReport), conditionBeanSetupper);
        }
        public virtual void LoadTReportChildList(IList<TReport> tReportList, ConditionBeanSetupper<TReportChildCB> conditionBeanSetupper) {
            AssertObjectNotNull("tReportList", tReportList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTReportChildList(tReportList, new LoadReferrerOption<TReportChildCB, TReportChild>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTReportChildList(TReport tReport, LoadReferrerOption<TReportChildCB, TReportChild> loadReferrerOption) {
            AssertObjectNotNull("tReport", tReport); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTReportChildList(xnewLRLs<TReport>(tReport), loadReferrerOption);
        }
        public virtual void LoadTReportChildList(IList<TReport> tReportList, LoadReferrerOption<TReportChildCB, TReportChild> loadReferrerOption) {
            AssertObjectNotNull("tReportList", tReportList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tReportList.Count == 0) { return; }
            TReportChildBhv referrerBhv = xgetBSFLR().Select<TReportChildBhv>();
            HelpLoadReferrerInternally<TReport, decimal?, TReportChildCB, TReportChild>
                    (tReportList, loadReferrerOption, new MyInternalLoadTReportChildListCallback(referrerBhv));
        }
        protected class MyInternalLoadTReportChildListCallback : InternalLoadReferrerCallback<TReport, decimal?, TReportChildCB, TReportChild> {
            protected TReportChildBhv referrerBhv;
            public MyInternalLoadTReportChildListCallback(TReportChildBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TReport e) { return e.ReportId; }
            public void setRfLs(TReport e, IList<TReportChild> ls) { e.TReportChildList = ls; }
            public TReportChildCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TReportChildCB cb, IList<decimal?> ls) { cb.Query().SetParentReportId_InScope(ls); }
            public void qyOdFKAsc(TReportChildCB cb) { cb.Query().AddOrderBy_ParentReportId_Asc(); }
            public void spFKCol(TReportChildCB cb) { cb.Specify().ColumnParentReportId(); }
            public IList<TReportChild> selRfLs(TReportChildCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TReportChild e) { return e.ParentReportId; }
            public void setlcEt(TReportChild re, TReport be) { re.TReport = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TReportset> PulloutTReportset(IList<TReport> tReportList) {
            return HelpPulloutInternally<TReport, TReportset>(tReportList, new MyInternalPulloutTReportsetCallback());
        }
        protected class MyInternalPulloutTReportsetCallback : InternalPulloutCallback<TReport, TReportset> {
            public TReportset getFr(TReport entity) { return entity.TReportset; }
        }
        public IList<TReportChild> PulloutTReportChild(IList<TReport> tReportList) {
            return HelpPulloutInternally<TReport, TReportChild>(tReportList, new MyInternalPulloutTReportChildCallback());
        }
        protected class MyInternalPulloutTReportChildCallback : InternalPulloutCallback<TReport, TReportChild> {
            public TReportChild getFr(TReport entity) { return entity.TReportChild; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TReport entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TReport entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TReport entity) {
            HelpInsertOrUpdateInternally<TReport, TReportCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TReport, TReportCB> {
            protected TReportBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TReportBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TReport entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TReport entity) { _bhv.Update(entity); }
            public TReportCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TReportCB cb, TReport entity) {
                cb.Query().SetReportId_Equal(entity.ReportId);
            }
            public int CallbackSelectCount(TReportCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TReport entity) {
            HelpDeleteInternally<TReport>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TReport> {
            protected TReportBhv _bhv;
            public MyInternalDeleteCallback(TReportBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TReport entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TReport tReport, TReportCB cb) {
            AssertObjectNotNull("tReport", tReport); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tReport);
            FilterEntityOfUpdate(tReport); AssertEntityOfUpdate(tReport);
            return this.Dao.UpdateByQuery(cb, tReport);
        }

        public int QueryDelete(TReportCB cb) {
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
        protected int DelegateSelectCount(TReportCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TReport> DelegateSelectList(TReportCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TReport e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TReport e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TReport e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TReport Downcast(Entity entity) {
            return (TReport)entity;
        }

        protected TReportCB Downcast(ConditionBean cb) {
            return (TReportCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TReportDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
