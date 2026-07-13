
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
    public partial class TDefaultEnvBaseBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDefaultEnvBaseDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDefaultEnvBaseBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DEFAULT_ENV_BASE"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDefaultEnvBaseDbm.GetInstance(); } }
        public TDefaultEnvBaseDbm MyDBMeta { get { return TDefaultEnvBaseDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDefaultEnvBase NewMyEntity() { return new TDefaultEnvBase(); }
        public virtual TDefaultEnvBaseCB NewMyConditionBean() { return new TDefaultEnvBaseCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDefaultEnvBaseCB cb) {
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
        public virtual TDefaultEnvBase SelectEntity(TDefaultEnvBaseCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDefaultEnvBase> ls = null;
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
            return (TDefaultEnvBase)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDefaultEnvBase SelectEntityWithDeletedCheck(TDefaultEnvBaseCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDefaultEnvBase entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDefaultEnvBase SelectByPKValue(String language) {
            return SelectEntity(BuildPKCB(language));
        }

        public virtual TDefaultEnvBase SelectByPKValueWithDeletedCheck(String language) {
            return SelectEntityWithDeletedCheck(BuildPKCB(language));
        }

        private TDefaultEnvBaseCB BuildPKCB(String language) {
            AssertObjectNotNull("language", language);
            TDefaultEnvBaseCB cb = NewMyConditionBean();
            cb.Query().SetLanguage_Equal(language);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDefaultEnvBase> SelectList(TDefaultEnvBaseCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDefaultEnvBase>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDefaultEnvBase> SelectPage(TDefaultEnvBaseCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDefaultEnvBase> invoker = new PagingInvoker<TDefaultEnvBase>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDefaultEnvBase> {
            protected TDefaultEnvBaseBhv _bhv; protected TDefaultEnvBaseCB _cb;
            public InternalSelectPagingHandler(TDefaultEnvBaseBhv bhv, TDefaultEnvBaseCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDefaultEnvBase> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDefaultEnvColorInfoCList(TDefaultEnvBase tDefaultEnvBase, ConditionBeanSetupper<TDefaultEnvColorInfoCCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvBase", tDefaultEnvBase); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorInfoCList(xnewLRLs<TDefaultEnvBase>(tDefaultEnvBase), conditionBeanSetupper);
        }
        public virtual void LoadTDefaultEnvColorInfoCList(IList<TDefaultEnvBase> tDefaultEnvBaseList, ConditionBeanSetupper<TDefaultEnvColorInfoCCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDefaultEnvBaseList", tDefaultEnvBaseList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDefaultEnvColorInfoCList(tDefaultEnvBaseList, new LoadReferrerOption<TDefaultEnvColorInfoCCB, TDefaultEnvColorInfoC>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDefaultEnvColorInfoCList(TDefaultEnvBase tDefaultEnvBase, LoadReferrerOption<TDefaultEnvColorInfoCCB, TDefaultEnvColorInfoC> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvBase", tDefaultEnvBase); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDefaultEnvColorInfoCList(xnewLRLs<TDefaultEnvBase>(tDefaultEnvBase), loadReferrerOption);
        }
        public virtual void LoadTDefaultEnvColorInfoCList(IList<TDefaultEnvBase> tDefaultEnvBaseList, LoadReferrerOption<TDefaultEnvColorInfoCCB, TDefaultEnvColorInfoC> loadReferrerOption) {
            AssertObjectNotNull("tDefaultEnvBaseList", tDefaultEnvBaseList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDefaultEnvBaseList.Count == 0) { return; }
            TDefaultEnvColorInfoCBhv referrerBhv = xgetBSFLR().Select<TDefaultEnvColorInfoCBhv>();
            HelpLoadReferrerInternally<TDefaultEnvBase, String, TDefaultEnvColorInfoCCB, TDefaultEnvColorInfoC>
                    (tDefaultEnvBaseList, loadReferrerOption, new MyInternalLoadTDefaultEnvColorInfoCListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDefaultEnvColorInfoCListCallback : InternalLoadReferrerCallback<TDefaultEnvBase, String, TDefaultEnvColorInfoCCB, TDefaultEnvColorInfoC> {
            protected TDefaultEnvColorInfoCBhv referrerBhv;
            public MyInternalLoadTDefaultEnvColorInfoCListCallback(TDefaultEnvColorInfoCBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public String getPKVal(TDefaultEnvBase e) { return e.Language; }
            public void setRfLs(TDefaultEnvBase e, IList<TDefaultEnvColorInfoC> ls) { e.TDefaultEnvColorInfoCList = ls; }
            public TDefaultEnvColorInfoCCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDefaultEnvColorInfoCCB cb, IList<String> ls) { cb.Query().SetLanguage_InScope(ls); }
            public void qyOdFKAsc(TDefaultEnvColorInfoCCB cb) { cb.Query().AddOrderBy_Language_Asc(); }
            public void spFKCol(TDefaultEnvColorInfoCCB cb) { cb.Specify().ColumnLanguage(); }
            public IList<TDefaultEnvColorInfoC> selRfLs(TDefaultEnvColorInfoCCB cb) { return referrerBhv.SelectList(cb); }
            public String getFKVal(TDefaultEnvColorInfoC e) { return e.Language; }
            public void setlcEt(TDefaultEnvColorInfoC re, TDefaultEnvBase be) { re.TDefaultEnvBase = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDefaultEnvBase entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDefaultEnvBase entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDefaultEnvBase entity) {
            HelpInsertOrUpdateInternally<TDefaultEnvBase, TDefaultEnvBaseCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDefaultEnvBase, TDefaultEnvBaseCB> {
            protected TDefaultEnvBaseBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDefaultEnvBaseBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDefaultEnvBase entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDefaultEnvBase entity) { _bhv.Update(entity); }
            public TDefaultEnvBaseCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDefaultEnvBaseCB cb, TDefaultEnvBase entity) {
                cb.Query().SetLanguage_Equal(entity.Language);
            }
            public int CallbackSelectCount(TDefaultEnvBaseCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDefaultEnvBase entity) {
            HelpDeleteInternally<TDefaultEnvBase>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDefaultEnvBase> {
            protected TDefaultEnvBaseBhv _bhv;
            public MyInternalDeleteCallback(TDefaultEnvBaseBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDefaultEnvBase entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDefaultEnvBase tDefaultEnvBase, TDefaultEnvBaseCB cb) {
            AssertObjectNotNull("tDefaultEnvBase", tDefaultEnvBase); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDefaultEnvBase);
            FilterEntityOfUpdate(tDefaultEnvBase); AssertEntityOfUpdate(tDefaultEnvBase);
            return this.Dao.UpdateByQuery(cb, tDefaultEnvBase);
        }

        public int QueryDelete(TDefaultEnvBaseCB cb) {
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
        protected int DelegateSelectCount(TDefaultEnvBaseCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDefaultEnvBase> DelegateSelectList(TDefaultEnvBaseCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TDefaultEnvBase e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDefaultEnvBase e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDefaultEnvBase e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDefaultEnvBase Downcast(Entity entity) {
            return (TDefaultEnvBase)entity;
        }

        protected TDefaultEnvBaseCB Downcast(ConditionBean cb) {
            return (TDefaultEnvBaseCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDefaultEnvBaseDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
