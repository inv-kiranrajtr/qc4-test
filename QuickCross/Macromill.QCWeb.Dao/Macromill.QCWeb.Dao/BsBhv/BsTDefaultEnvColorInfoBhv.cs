
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
    public partial class TDefaultEnvColorInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvColorInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDefaultEnvColorInfoDbm.GetInstance(); } }
        public TDefaultEnvColorInfoDbm MyDBMeta { get { return TDefaultEnvColorInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDefaultEnvColorInfo NewMyEntity() { return new TDefaultEnvColorInfo(); }
        public virtual TDefaultEnvColorInfoCB NewMyConditionBean() { return new TDefaultEnvColorInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDefaultEnvColorInfoCB cb) {
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
        public virtual TDefaultEnvColorInfo SelectEntity(TDefaultEnvColorInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDefaultEnvColorInfo> ls = null;
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
            return (TDefaultEnvColorInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDefaultEnvColorInfo SelectEntityWithDeletedCheck(TDefaultEnvColorInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDefaultEnvColorInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDefaultEnvColorInfo SelectByPKValue(decimal? defEnvColorInfoId) {
            return SelectEntity(BuildPKCB(defEnvColorInfoId));
        }

        public virtual TDefaultEnvColorInfo SelectByPKValueWithDeletedCheck(decimal? defEnvColorInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(defEnvColorInfoId));
        }

        private TDefaultEnvColorInfoCB BuildPKCB(decimal? defEnvColorInfoId) {
            AssertObjectNotNull("defEnvColorInfoId", defEnvColorInfoId);
            TDefaultEnvColorInfoCB cb = NewMyConditionBean();
            cb.Query().SetDefEnvColorInfoId_Equal(defEnvColorInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDefaultEnvColorInfo> SelectList(TDefaultEnvColorInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDefaultEnvColorInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDefaultEnvColorInfo> SelectPage(TDefaultEnvColorInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDefaultEnvColorInfo> invoker = new PagingInvoker<TDefaultEnvColorInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDefaultEnvColorInfo> {
            protected TDefaultEnvColorInfoBhv _bhv; protected TDefaultEnvColorInfoCB _cb;
            public InternalSelectPagingHandler(TDefaultEnvColorInfoBhv bhv, TDefaultEnvColorInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDefaultEnvColorInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDefaultEnvColorInfo myEntity = (TDefaultEnvColorInfo)entity;
            myEntity.DefEnvColorInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDefaultEnvColorDtlList(TDefaultEnvColorInfo tDefaultEnvColorInfo, ConditionBeanSetupper<TDefaultEnvColorDtlCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvColorInfo", tDefaultEnvColorInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorDtlList(xnewLRLs<TDefaultEnvColorInfo>(tDefaultEnvColorInfo), conditionBeanSetupper);
        }
        public virtual void LoadTDefaultEnvColorDtlList(IList<TDefaultEnvColorInfo> tDefaultEnvColorInfoList, ConditionBeanSetupper<TDefaultEnvColorDtlCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvColorInfoList", tDefaultEnvColorInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorDtlList(tDefaultEnvColorInfoList, new LoadReferrerOption<TDefaultEnvColorDtlCB, TDefaultEnvColorDtl>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDefaultEnvColorDtlList(TDefaultEnvColorInfo tDefaultEnvColorInfo, LoadReferrerOption<TDefaultEnvColorDtlCB, TDefaultEnvColorDtl> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvColorInfo", tDefaultEnvColorInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDefaultEnvColorDtlList(xnewLRLs<TDefaultEnvColorInfo>(tDefaultEnvColorInfo), loadReferrerOption);
        }
        public virtual void LoadTDefaultEnvColorDtlList(IList<TDefaultEnvColorInfo> tDefaultEnvColorInfoList, LoadReferrerOption<TDefaultEnvColorDtlCB, TDefaultEnvColorDtl> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvColorInfoList", tDefaultEnvColorInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDefaultEnvColorInfoList.Count == 0) { return; }
            TDefaultEnvColorDtlBhv referrerBhv = xgetBSFLR().Select<TDefaultEnvColorDtlBhv>();
            HelpLoadReferrerInternally<TDefaultEnvColorInfo, decimal?, TDefaultEnvColorDtlCB, TDefaultEnvColorDtl>
                    (tDefaultEnvColorInfoList, loadReferrerOption, new MyInternalLoadTDefaultEnvColorDtlListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDefaultEnvColorDtlListCallback : InternalLoadReferrerCallback<TDefaultEnvColorInfo, decimal?, TDefaultEnvColorDtlCB, TDefaultEnvColorDtl> {
            protected TDefaultEnvColorDtlBhv referrerBhv;
            public MyInternalLoadTDefaultEnvColorDtlListCallback(TDefaultEnvColorDtlBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDefaultEnvColorInfo e) { return e.DefEnvColorInfoId; }
            public void setRfLs(TDefaultEnvColorInfo e, IList<TDefaultEnvColorDtl> ls) { e.TDefaultEnvColorDtlList = ls; }
            public TDefaultEnvColorDtlCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDefaultEnvColorDtlCB cb, IList<decimal?> ls) { cb.Query().SetDefEnvColorInfoId_InScope(ls); }
            public void qyOdFKAsc(TDefaultEnvColorDtlCB cb) { cb.Query().AddOrderBy_DefEnvColorInfoId_Asc(); }
            public void spFKCol(TDefaultEnvColorDtlCB cb) { cb.Specify().ColumnDefEnvColorInfoId(); }
            public IList<TDefaultEnvColorDtl> selRfLs(TDefaultEnvColorDtlCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDefaultEnvColorDtl e) { return e.DefEnvColorInfoId; }
            public void setlcEt(TDefaultEnvColorDtl re, TDefaultEnvColorInfo be) { re.TDefaultEnvColorInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TDefaultEnv> PulloutTDefaultEnv(IList<TDefaultEnvColorInfo> tDefaultEnvColorInfoList) {
            return HelpPulloutInternally<TDefaultEnvColorInfo, TDefaultEnv>(tDefaultEnvColorInfoList, new MyInternalPulloutTDefaultEnvCallback());
        }
        protected class MyInternalPulloutTDefaultEnvCallback : InternalPulloutCallback<TDefaultEnvColorInfo, TDefaultEnv> {
            public TDefaultEnv getFr(TDefaultEnvColorInfo entity) { return entity.TDefaultEnv; }
        }
        public IList<TDefaultEnvColorDtl> PulloutTDefaultEnvColorDtl(IList<TDefaultEnvColorInfo> tDefaultEnvColorInfoList) {
            return HelpPulloutInternally<TDefaultEnvColorInfo, TDefaultEnvColorDtl>(tDefaultEnvColorInfoList, new MyInternalPulloutTDefaultEnvColorDtlCallback());
        }
        protected class MyInternalPulloutTDefaultEnvColorDtlCallback : InternalPulloutCallback<TDefaultEnvColorInfo, TDefaultEnvColorDtl> {
            public TDefaultEnvColorDtl getFr(TDefaultEnvColorInfo entity) { return entity.TDefaultEnvColorDtl; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDefaultEnvColorInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDefaultEnvColorInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDefaultEnvColorInfo entity) {
            HelpInsertOrUpdateInternally<TDefaultEnvColorInfo, TDefaultEnvColorInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDefaultEnvColorInfo, TDefaultEnvColorInfoCB> {
            protected TDefaultEnvColorInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDefaultEnvColorInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDefaultEnvColorInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDefaultEnvColorInfo entity) { _bhv.Update(entity); }
            public TDefaultEnvColorInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDefaultEnvColorInfoCB cb, TDefaultEnvColorInfo entity) {
                cb.Query().SetDefEnvColorInfoId_Equal(entity.DefEnvColorInfoId);
            }
            public int CallbackSelectCount(TDefaultEnvColorInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDefaultEnvColorInfo entity) {
            HelpDeleteInternally<TDefaultEnvColorInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDefaultEnvColorInfo> {
            protected TDefaultEnvColorInfoBhv _bhv;
            public MyInternalDeleteCallback(TDefaultEnvColorInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDefaultEnvColorInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDefaultEnvColorInfo tDefaultEnvColorInfo, TDefaultEnvColorInfoCB cb) {
            AssertObjectNotNull("tDefaultEnvColorInfo", tDefaultEnvColorInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDefaultEnvColorInfo);
            FilterEntityOfUpdate(tDefaultEnvColorInfo); AssertEntityOfUpdate(tDefaultEnvColorInfo);
            return this.Dao.UpdateByQuery(cb, tDefaultEnvColorInfo);
        }

        public int QueryDelete(TDefaultEnvColorInfoCB cb) {
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
        protected int DelegateSelectCount(TDefaultEnvColorInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDefaultEnvColorInfo> DelegateSelectList(TDefaultEnvColorInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDefaultEnvColorInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDefaultEnvColorInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDefaultEnvColorInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDefaultEnvColorInfo Downcast(Entity entity) {
            return (TDefaultEnvColorInfo)entity;
        }

        protected TDefaultEnvColorInfoCB Downcast(ConditionBean cb) {
            return (TDefaultEnvColorInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDefaultEnvColorInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
