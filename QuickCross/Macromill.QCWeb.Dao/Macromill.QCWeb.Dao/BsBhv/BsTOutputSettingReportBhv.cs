
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
    public partial class TOutputSettingReportBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSettingReportDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSettingReportBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING_REPORT"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputSettingReportDbm.GetInstance(); } }
        public TOutputSettingReportDbm MyDBMeta { get { return TOutputSettingReportDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputSettingReport NewMyEntity() { return new TOutputSettingReport(); }
        public virtual TOutputSettingReportCB NewMyConditionBean() { return new TOutputSettingReportCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputSettingReportCB cb) {
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
        public virtual TOutputSettingReport SelectEntity(TOutputSettingReportCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputSettingReport> ls = null;
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
            return (TOutputSettingReport)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputSettingReport SelectEntityWithDeletedCheck(TOutputSettingReportCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputSettingReport entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputSettingReport SelectByPKValue(decimal? qcwebid) {
            return SelectEntity(BuildPKCB(qcwebid));
        }

        public virtual TOutputSettingReport SelectByPKValueWithDeletedCheck(decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebid));
        }

        private TOutputSettingReportCB BuildPKCB(decimal? qcwebid) {
            AssertObjectNotNull("qcwebid", qcwebid);
            TOutputSettingReportCB cb = NewMyConditionBean();
            cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputSettingReport> SelectList(TOutputSettingReportCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputSettingReport>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputSettingReport> SelectPage(TOutputSettingReportCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputSettingReport> invoker = new PagingInvoker<TOutputSettingReport>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputSettingReport> {
            protected TOutputSettingReportBhv _bhv; protected TOutputSettingReportCB _cb;
            public InternalSelectPagingHandler(TOutputSettingReportBhv bhv, TOutputSettingReportCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputSettingReport> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TOutputSettingReport> tOutputSettingReportList) {
            return HelpPulloutInternally<TOutputSettingReport, TQcwebSurveyInfo>(tOutputSettingReportList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TOutputSettingReport, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TOutputSettingReport entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfoAsOne(IList<TOutputSettingReport> tOutputSettingReportList) {
            return HelpPulloutInternally<TOutputSettingReport, TQcwebSurveyInfo>(tOutputSettingReportList, new MyInternalPulloutTQcwebSurveyInfoListCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoListCallback : InternalPulloutCallback<TOutputSettingReport, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TOutputSettingReport entity) { return entity.TQcwebSurveyInfoAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputSettingReport entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputSettingReport entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputSettingReport entity) {
            HelpInsertOrUpdateInternally<TOutputSettingReport, TOutputSettingReportCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputSettingReport, TOutputSettingReportCB> {
            protected TOutputSettingReportBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputSettingReportBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputSettingReport entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputSettingReport entity) { _bhv.Update(entity); }
            public TOutputSettingReportCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputSettingReportCB cb, TOutputSettingReport entity) {
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TOutputSettingReportCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputSettingReport entity) {
            HelpDeleteInternally<TOutputSettingReport>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputSettingReport> {
            protected TOutputSettingReportBhv _bhv;
            public MyInternalDeleteCallback(TOutputSettingReportBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputSettingReport entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputSettingReport tOutputSettingReport, TOutputSettingReportCB cb) {
            AssertObjectNotNull("tOutputSettingReport", tOutputSettingReport); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputSettingReport);
            FilterEntityOfUpdate(tOutputSettingReport); AssertEntityOfUpdate(tOutputSettingReport);
            return this.Dao.UpdateByQuery(cb, tOutputSettingReport);
        }

        public int QueryDelete(TOutputSettingReportCB cb) {
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
        protected int DelegateSelectCount(TOutputSettingReportCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputSettingReport> DelegateSelectList(TOutputSettingReportCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TOutputSettingReport e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputSettingReport e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputSettingReport e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputSettingReport Downcast(Entity entity) {
            return (TOutputSettingReport)entity;
        }

        protected TOutputSettingReportCB Downcast(ConditionBean cb) {
            return (TOutputSettingReportCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputSettingReportDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
