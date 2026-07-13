
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
    public partial class TDefaultEnvBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>環境設定にデータ登録 </summary>
        public static readonly String PATH_insertBasicData = "insertBasicData";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDefaultEnvDbm.GetInstance(); } }
        public TDefaultEnvDbm MyDBMeta { get { return TDefaultEnvDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDefaultEnv NewMyEntity() { return new TDefaultEnv(); }
        public virtual TDefaultEnvCB NewMyConditionBean() { return new TDefaultEnvCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDefaultEnvCB cb) {
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
        public virtual TDefaultEnv SelectEntity(TDefaultEnvCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDefaultEnv> ls = null;
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
            return (TDefaultEnv)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDefaultEnv SelectEntityWithDeletedCheck(TDefaultEnvCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDefaultEnv entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDefaultEnv SelectByPKValue(decimal? qcwebid) {
            return SelectEntity(BuildPKCB(qcwebid));
        }

        public virtual TDefaultEnv SelectByPKValueWithDeletedCheck(decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebid));
        }

        private TDefaultEnvCB BuildPKCB(decimal? qcwebid) {
            AssertObjectNotNull("qcwebid", qcwebid);
            TDefaultEnvCB cb = NewMyConditionBean();
            cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDefaultEnv> SelectList(TDefaultEnvCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDefaultEnv>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDefaultEnv> SelectPage(TDefaultEnvCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDefaultEnv> invoker = new PagingInvoker<TDefaultEnv>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDefaultEnv> {
            protected TDefaultEnvBhv _bhv; protected TDefaultEnvCB _cb;
            public InternalSelectPagingHandler(TDefaultEnvBhv bhv, TDefaultEnvCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDefaultEnv> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDefaultEnvColorInfoList(TDefaultEnv tDefaultEnv, ConditionBeanSetupper<TDefaultEnvColorInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnv", tDefaultEnv); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorInfoList(xnewLRLs<TDefaultEnv>(tDefaultEnv), conditionBeanSetupper);
        }
        public virtual void LoadTDefaultEnvColorInfoList(IList<TDefaultEnv> tDefaultEnvList, ConditionBeanSetupper<TDefaultEnvColorInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvList", tDefaultEnvList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorInfoList(tDefaultEnvList, new LoadReferrerOption<TDefaultEnvColorInfoCB, TDefaultEnvColorInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDefaultEnvColorInfoList(TDefaultEnv tDefaultEnv, LoadReferrerOption<TDefaultEnvColorInfoCB, TDefaultEnvColorInfo> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnv", tDefaultEnv); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDefaultEnvColorInfoList(xnewLRLs<TDefaultEnv>(tDefaultEnv), loadReferrerOption);
        }
        public virtual void LoadTDefaultEnvColorInfoList(IList<TDefaultEnv> tDefaultEnvList, LoadReferrerOption<TDefaultEnvColorInfoCB, TDefaultEnvColorInfo> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvList", tDefaultEnvList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDefaultEnvList.Count == 0) { return; }
            TDefaultEnvColorInfoBhv referrerBhv = xgetBSFLR().Select<TDefaultEnvColorInfoBhv>();
            HelpLoadReferrerInternally<TDefaultEnv, decimal?, TDefaultEnvColorInfoCB, TDefaultEnvColorInfo>
                    (tDefaultEnvList, loadReferrerOption, new MyInternalLoadTDefaultEnvColorInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDefaultEnvColorInfoListCallback : InternalLoadReferrerCallback<TDefaultEnv, decimal?, TDefaultEnvColorInfoCB, TDefaultEnvColorInfo> {
            protected TDefaultEnvColorInfoBhv referrerBhv;
            public MyInternalLoadTDefaultEnvColorInfoListCallback(TDefaultEnvColorInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDefaultEnv e) { return e.Qcwebid; }
            public void setRfLs(TDefaultEnv e, IList<TDefaultEnvColorInfo> ls) { e.TDefaultEnvColorInfoList = ls; }
            public TDefaultEnvColorInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDefaultEnvColorInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TDefaultEnvColorInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TDefaultEnvColorInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TDefaultEnvColorInfo> selRfLs(TDefaultEnvColorInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDefaultEnvColorInfo e) { return e.Qcwebid; }
            public void setlcEt(TDefaultEnvColorInfo re, TDefaultEnv be) { re.TDefaultEnv = be; }
        }
        public virtual void LoadTScenarioTotalizationList(TDefaultEnv tDefaultEnv, ConditionBeanSetupper<TScenarioTotalizationCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnv", tDefaultEnv); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioTotalizationList(xnewLRLs<TDefaultEnv>(tDefaultEnv), conditionBeanSetupper);
        }
        public virtual void LoadTScenarioTotalizationList(IList<TDefaultEnv> tDefaultEnvList, ConditionBeanSetupper<TScenarioTotalizationCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvList", tDefaultEnvList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioTotalizationList(tDefaultEnvList, new LoadReferrerOption<TScenarioTotalizationCB, TScenarioTotalization>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTScenarioTotalizationList(TDefaultEnv tDefaultEnv, LoadReferrerOption<TScenarioTotalizationCB, TScenarioTotalization> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnv", tDefaultEnv); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTScenarioTotalizationList(xnewLRLs<TDefaultEnv>(tDefaultEnv), loadReferrerOption);
        }
        public virtual void LoadTScenarioTotalizationList(IList<TDefaultEnv> tDefaultEnvList, LoadReferrerOption<TScenarioTotalizationCB, TScenarioTotalization> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvList", tDefaultEnvList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDefaultEnvList.Count == 0) { return; }
            TScenarioTotalizationBhv referrerBhv = xgetBSFLR().Select<TScenarioTotalizationBhv>();
            HelpLoadReferrerInternally<TDefaultEnv, decimal?, TScenarioTotalizationCB, TScenarioTotalization>
                    (tDefaultEnvList, loadReferrerOption, new MyInternalLoadTScenarioTotalizationListCallback(referrerBhv));
        }
        protected class MyInternalLoadTScenarioTotalizationListCallback : InternalLoadReferrerCallback<TDefaultEnv, decimal?, TScenarioTotalizationCB, TScenarioTotalization> {
            protected TScenarioTotalizationBhv referrerBhv;
            public MyInternalLoadTScenarioTotalizationListCallback(TScenarioTotalizationBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDefaultEnv e) { return e.Qcwebid; }
            public void setRfLs(TDefaultEnv e, IList<TScenarioTotalization> ls) { e.TScenarioTotalizationList = ls; }
            public TScenarioTotalizationCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TScenarioTotalizationCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TScenarioTotalizationCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TScenarioTotalizationCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TScenarioTotalization> selRfLs(TScenarioTotalizationCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TScenarioTotalization e) { return e.Qcwebid; }
            public void setlcEt(TScenarioTotalization re, TDefaultEnv be) { re.TDefaultEnv = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfoAsOne(IList<TDefaultEnv> tDefaultEnvList) {
            return HelpPulloutInternally<TDefaultEnv, TQcwebSurveyInfo>(tDefaultEnvList, new MyInternalPulloutTQcwebSurveyInfoListCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoListCallback : InternalPulloutCallback<TDefaultEnv, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TDefaultEnv entity) { return entity.TQcwebSurveyInfoAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDefaultEnv entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDefaultEnv entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDefaultEnv entity) {
            HelpInsertOrUpdateInternally<TDefaultEnv, TDefaultEnvCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDefaultEnv, TDefaultEnvCB> {
            protected TDefaultEnvBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDefaultEnvBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDefaultEnv entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDefaultEnv entity) { _bhv.Update(entity); }
            public TDefaultEnvCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDefaultEnvCB cb, TDefaultEnv entity) {
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TDefaultEnvCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDefaultEnv entity) {
            HelpDeleteInternally<TDefaultEnv>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDefaultEnv> {
            protected TDefaultEnvBhv _bhv;
            public MyInternalDeleteCallback(TDefaultEnvBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDefaultEnv entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDefaultEnv tDefaultEnv, TDefaultEnvCB cb) {
            AssertObjectNotNull("tDefaultEnv", tDefaultEnv); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDefaultEnv);
            FilterEntityOfUpdate(tDefaultEnv); AssertEntityOfUpdate(tDefaultEnv);
            return this.Dao.UpdateByQuery(cb, tDefaultEnv);
        }

        public int QueryDelete(TDefaultEnvCB cb) {
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
        protected int DelegateSelectCount(TDefaultEnvCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDefaultEnv> DelegateSelectList(TDefaultEnvCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TDefaultEnv e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDefaultEnv e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDefaultEnv e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDefaultEnv Downcast(Entity entity) {
            return (TDefaultEnv)entity;
        }

        protected TDefaultEnvCB Downcast(ConditionBean cb) {
            return (TDefaultEnvCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDefaultEnvDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
