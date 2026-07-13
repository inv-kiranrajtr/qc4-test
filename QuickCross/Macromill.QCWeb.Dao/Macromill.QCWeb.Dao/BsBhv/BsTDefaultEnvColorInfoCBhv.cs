
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
    public partial class TDefaultEnvColorInfoCBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvColorInfoCDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvColorInfoCBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_COLOR_INFO_C"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDefaultEnvColorInfoCDbm.GetInstance(); } }
        public TDefaultEnvColorInfoCDbm MyDBMeta { get { return TDefaultEnvColorInfoCDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDefaultEnvColorInfoC NewMyEntity() { return new TDefaultEnvColorInfoC(); }
        public virtual TDefaultEnvColorInfoCCB NewMyConditionBean() { return new TDefaultEnvColorInfoCCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDefaultEnvColorInfoCCB cb) {
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
        public virtual TDefaultEnvColorInfoC SelectEntity(TDefaultEnvColorInfoCCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDefaultEnvColorInfoC> ls = null;
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
            return (TDefaultEnvColorInfoC)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDefaultEnvColorInfoC SelectEntityWithDeletedCheck(TDefaultEnvColorInfoCCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDefaultEnvColorInfoC entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDefaultEnvColorInfoC SelectByPKValue(int? defEnvColorInfoCId) {
            return SelectEntity(BuildPKCB(defEnvColorInfoCId));
        }

        public virtual TDefaultEnvColorInfoC SelectByPKValueWithDeletedCheck(int? defEnvColorInfoCId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(defEnvColorInfoCId));
        }

        private TDefaultEnvColorInfoCCB BuildPKCB(int? defEnvColorInfoCId) {
            AssertObjectNotNull("defEnvColorInfoCId", defEnvColorInfoCId);
            TDefaultEnvColorInfoCCB cb = NewMyConditionBean();
            cb.Query().SetDefEnvColorInfoCId_Equal(defEnvColorInfoCId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDefaultEnvColorInfoC> SelectList(TDefaultEnvColorInfoCCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDefaultEnvColorInfoC>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDefaultEnvColorInfoC> SelectPage(TDefaultEnvColorInfoCCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDefaultEnvColorInfoC> invoker = new PagingInvoker<TDefaultEnvColorInfoC>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDefaultEnvColorInfoC> {
            protected TDefaultEnvColorInfoCBhv _bhv; protected TDefaultEnvColorInfoCCB _cb;
            public InternalSelectPagingHandler(TDefaultEnvColorInfoCBhv bhv, TDefaultEnvColorInfoCCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDefaultEnvColorInfoC> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public int? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDefaultEnvColorInfoC myEntity = (TDefaultEnvColorInfoC)entity;
            myEntity.DefEnvColorInfoCId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDefaultEnvColorDtlCList(TDefaultEnvColorInfoC tDefaultEnvColorInfoC, ConditionBeanSetupper<TDefaultEnvColorDtlCCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvColorInfoC", tDefaultEnvColorInfoC); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorDtlCList(xnewLRLs<TDefaultEnvColorInfoC>(tDefaultEnvColorInfoC), conditionBeanSetupper);
        }
        public virtual void LoadTDefaultEnvColorDtlCList(IList<TDefaultEnvColorInfoC> tDefaultEnvColorInfoCList, ConditionBeanSetupper<TDefaultEnvColorDtlCCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvColorInfoCList", tDefaultEnvColorInfoCList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorDtlCList(tDefaultEnvColorInfoCList, new LoadReferrerOption<TDefaultEnvColorDtlCCB, TDefaultEnvColorDtlC>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDefaultEnvColorDtlCList(TDefaultEnvColorInfoC tDefaultEnvColorInfoC, LoadReferrerOption<TDefaultEnvColorDtlCCB, TDefaultEnvColorDtlC> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvColorInfoC", tDefaultEnvColorInfoC); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDefaultEnvColorDtlCList(xnewLRLs<TDefaultEnvColorInfoC>(tDefaultEnvColorInfoC), loadReferrerOption);
        }
        public virtual void LoadTDefaultEnvColorDtlCList(IList<TDefaultEnvColorInfoC> tDefaultEnvColorInfoCList, LoadReferrerOption<TDefaultEnvColorDtlCCB, TDefaultEnvColorDtlC> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvColorInfoCList", tDefaultEnvColorInfoCList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDefaultEnvColorInfoCList.Count == 0) { return; }
            TDefaultEnvColorDtlCBhv referrerBhv = xgetBSFLR().Select<TDefaultEnvColorDtlCBhv>();
            HelpLoadReferrerInternally<TDefaultEnvColorInfoC, int?, TDefaultEnvColorDtlCCB, TDefaultEnvColorDtlC>
                    (tDefaultEnvColorInfoCList, loadReferrerOption, new MyInternalLoadTDefaultEnvColorDtlCListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDefaultEnvColorDtlCListCallback : InternalLoadReferrerCallback<TDefaultEnvColorInfoC, int?, TDefaultEnvColorDtlCCB, TDefaultEnvColorDtlC> {
            protected TDefaultEnvColorDtlCBhv referrerBhv;
            public MyInternalLoadTDefaultEnvColorDtlCListCallback(TDefaultEnvColorDtlCBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public int? getPKVal(TDefaultEnvColorInfoC e) { return e.DefEnvColorInfoCId; }
            public void setRfLs(TDefaultEnvColorInfoC e, IList<TDefaultEnvColorDtlC> ls) { e.TDefaultEnvColorDtlCList = ls; }
            public TDefaultEnvColorDtlCCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDefaultEnvColorDtlCCB cb, IList<int?> ls) { cb.Query().SetDefEnvColorInfoCId_InScope(ls); }
            public void qyOdFKAsc(TDefaultEnvColorDtlCCB cb) { cb.Query().AddOrderBy_DefEnvColorInfoCId_Asc(); }
            public void spFKCol(TDefaultEnvColorDtlCCB cb) { cb.Specify().ColumnDefEnvColorInfoCId(); }
            public IList<TDefaultEnvColorDtlC> selRfLs(TDefaultEnvColorDtlCCB cb) { return referrerBhv.SelectList(cb); }
            public int? getFKVal(TDefaultEnvColorDtlC e) { return e.DefEnvColorInfoCId; }
            public void setlcEt(TDefaultEnvColorDtlC re, TDefaultEnvColorInfoC be) { re.TDefaultEnvColorInfoC = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TDefaultEnvBase> PulloutTDefaultEnvBase(IList<TDefaultEnvColorInfoC> tDefaultEnvColorInfoCList) {
            return HelpPulloutInternally<TDefaultEnvColorInfoC, TDefaultEnvBase>(tDefaultEnvColorInfoCList, new MyInternalPulloutTDefaultEnvBaseCallback());
        }
        protected class MyInternalPulloutTDefaultEnvBaseCallback : InternalPulloutCallback<TDefaultEnvColorInfoC, TDefaultEnvBase> {
            public TDefaultEnvBase getFr(TDefaultEnvColorInfoC entity) { return entity.TDefaultEnvBase; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDefaultEnvColorInfoC entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDefaultEnvColorInfoC entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDefaultEnvColorInfoC entity) {
            HelpInsertOrUpdateInternally<TDefaultEnvColorInfoC, TDefaultEnvColorInfoCCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDefaultEnvColorInfoC, TDefaultEnvColorInfoCCB> {
            protected TDefaultEnvColorInfoCBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDefaultEnvColorInfoCBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDefaultEnvColorInfoC entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDefaultEnvColorInfoC entity) { _bhv.Update(entity); }
            public TDefaultEnvColorInfoCCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDefaultEnvColorInfoCCB cb, TDefaultEnvColorInfoC entity) {
                cb.Query().SetDefEnvColorInfoCId_Equal(entity.DefEnvColorInfoCId);
            }
            public int CallbackSelectCount(TDefaultEnvColorInfoCCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDefaultEnvColorInfoC entity) {
            HelpDeleteInternally<TDefaultEnvColorInfoC>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDefaultEnvColorInfoC> {
            protected TDefaultEnvColorInfoCBhv _bhv;
            public MyInternalDeleteCallback(TDefaultEnvColorInfoCBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDefaultEnvColorInfoC entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDefaultEnvColorInfoC tDefaultEnvColorInfoC, TDefaultEnvColorInfoCCB cb) {
            AssertObjectNotNull("tDefaultEnvColorInfoC", tDefaultEnvColorInfoC); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDefaultEnvColorInfoC);
            FilterEntityOfUpdate(tDefaultEnvColorInfoC); AssertEntityOfUpdate(tDefaultEnvColorInfoC);
            return this.Dao.UpdateByQuery(cb, tDefaultEnvColorInfoC);
        }

        public int QueryDelete(TDefaultEnvColorInfoCCB cb) {
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
        protected int DelegateSelectCount(TDefaultEnvColorInfoCCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDefaultEnvColorInfoC> DelegateSelectList(TDefaultEnvColorInfoCCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected int? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDefaultEnvColorInfoC e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDefaultEnvColorInfoC e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDefaultEnvColorInfoC e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDefaultEnvColorInfoC Downcast(Entity entity) {
            return (TDefaultEnvColorInfoC)entity;
        }

        protected TDefaultEnvColorInfoCCB Downcast(ConditionBean cb) {
            return (TDefaultEnvColorInfoCCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDefaultEnvColorInfoCDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
