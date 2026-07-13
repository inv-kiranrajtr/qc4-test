
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
    public partial class TEditMenuMasterBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TEditMenuMasterDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TEditMenuMasterBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_EDIT_MENU_MASTER"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TEditMenuMasterDbm.GetInstance(); } }
        public TEditMenuMasterDbm MyDBMeta { get { return TEditMenuMasterDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TEditMenuMaster NewMyEntity() { return new TEditMenuMaster(); }
        public virtual TEditMenuMasterCB NewMyConditionBean() { return new TEditMenuMasterCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TEditMenuMasterCB cb) {
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
        public virtual TEditMenuMaster SelectEntity(TEditMenuMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TEditMenuMaster> ls = null;
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
            return (TEditMenuMaster)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TEditMenuMaster SelectEntityWithDeletedCheck(TEditMenuMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TEditMenuMaster entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TEditMenuMaster SelectByPKValue(int? editMenuMasterId) {
            return SelectEntity(BuildPKCB(editMenuMasterId));
        }

        public virtual TEditMenuMaster SelectByPKValueWithDeletedCheck(int? editMenuMasterId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(editMenuMasterId));
        }

        private TEditMenuMasterCB BuildPKCB(int? editMenuMasterId) {
            AssertObjectNotNull("editMenuMasterId", editMenuMasterId);
            TEditMenuMasterCB cb = NewMyConditionBean();
            cb.Query().SetEditMenuMasterId_Equal(editMenuMasterId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TEditMenuMaster> SelectList(TEditMenuMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TEditMenuMaster>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TEditMenuMaster> SelectPage(TEditMenuMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TEditMenuMaster> invoker = new PagingInvoker<TEditMenuMaster>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TEditMenuMaster> {
            protected TEditMenuMasterBhv _bhv; protected TEditMenuMasterCB _cb;
            public InternalSelectPagingHandler(TEditMenuMasterBhv bhv, TEditMenuMasterCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TEditMenuMaster> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTDataEditListList(TEditMenuMaster tEditMenuMaster, ConditionBeanSetupper<TDataEditListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tEditMenuMaster", tEditMenuMaster); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataEditListList(xnewLRLs<TEditMenuMaster>(tEditMenuMaster), conditionBeanSetupper);
        }
        public virtual void LoadTDataEditListList(IList<TEditMenuMaster> tEditMenuMasterList, ConditionBeanSetupper<TDataEditListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tEditMenuMasterList", tEditMenuMasterList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataEditListList(tEditMenuMasterList, new LoadReferrerOption<TDataEditListCB, TDataEditList>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDataEditListList(TEditMenuMaster tEditMenuMaster, LoadReferrerOption<TDataEditListCB, TDataEditList> loadReferrerOption) {
            AssertObjectNotNull("tEditMenuMaster", tEditMenuMaster); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDataEditListList(xnewLRLs<TEditMenuMaster>(tEditMenuMaster), loadReferrerOption);
        }
        public virtual void LoadTDataEditListList(IList<TEditMenuMaster> tEditMenuMasterList, LoadReferrerOption<TDataEditListCB, TDataEditList> loadReferrerOption) {
            AssertObjectNotNull("tEditMenuMasterList", tEditMenuMasterList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tEditMenuMasterList.Count == 0) { return; }
            TDataEditListBhv referrerBhv = xgetBSFLR().Select<TDataEditListBhv>();
            HelpLoadReferrerInternally<TEditMenuMaster, int?, TDataEditListCB, TDataEditList>
                    (tEditMenuMasterList, loadReferrerOption, new MyInternalLoadTDataEditListListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDataEditListListCallback : InternalLoadReferrerCallback<TEditMenuMaster, int?, TDataEditListCB, TDataEditList> {
            protected TDataEditListBhv referrerBhv;
            public MyInternalLoadTDataEditListListCallback(TDataEditListBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public int? getPKVal(TEditMenuMaster e) { return e.EditMenuMasterId; }
            public void setRfLs(TEditMenuMaster e, IList<TDataEditList> ls) { e.TDataEditListList = ls; }
            public TDataEditListCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDataEditListCB cb, IList<int?> ls) { cb.Query().SetEditMenuMasterId_InScope(ls); }
            public void qyOdFKAsc(TDataEditListCB cb) { cb.Query().AddOrderBy_EditMenuMasterId_Asc(); }
            public void spFKCol(TDataEditListCB cb) { cb.Specify().ColumnEditMenuMasterId(); }
            public IList<TDataEditList> selRfLs(TDataEditListCB cb) { return referrerBhv.SelectList(cb); }
            public int? getFKVal(TDataEditList e) { return e.EditMenuMasterId; }
            public void setlcEt(TDataEditList re, TEditMenuMaster be) { re.TEditMenuMaster = be; }
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
        public virtual void Insert(TEditMenuMaster entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TEditMenuMaster entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TEditMenuMaster entity) {
            HelpInsertOrUpdateInternally<TEditMenuMaster, TEditMenuMasterCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TEditMenuMaster, TEditMenuMasterCB> {
            protected TEditMenuMasterBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TEditMenuMasterBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TEditMenuMaster entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TEditMenuMaster entity) { _bhv.Update(entity); }
            public TEditMenuMasterCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TEditMenuMasterCB cb, TEditMenuMaster entity) {
                cb.Query().SetEditMenuMasterId_Equal(entity.EditMenuMasterId);
            }
            public int CallbackSelectCount(TEditMenuMasterCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TEditMenuMaster entity) {
            HelpDeleteInternally<TEditMenuMaster>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TEditMenuMaster> {
            protected TEditMenuMasterBhv _bhv;
            public MyInternalDeleteCallback(TEditMenuMasterBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TEditMenuMaster entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TEditMenuMaster tEditMenuMaster, TEditMenuMasterCB cb) {
            AssertObjectNotNull("tEditMenuMaster", tEditMenuMaster); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tEditMenuMaster);
            FilterEntityOfUpdate(tEditMenuMaster); AssertEntityOfUpdate(tEditMenuMaster);
            return this.Dao.UpdateByQuery(cb, tEditMenuMaster);
        }

        public int QueryDelete(TEditMenuMasterCB cb) {
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
        protected int DelegateSelectCount(TEditMenuMasterCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TEditMenuMaster> DelegateSelectList(TEditMenuMasterCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TEditMenuMaster e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TEditMenuMaster e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TEditMenuMaster e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TEditMenuMaster Downcast(Entity entity) {
            return (TEditMenuMaster)entity;
        }

        protected TEditMenuMasterCB Downcast(ConditionBean cb) {
            return (TEditMenuMasterCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TEditMenuMasterDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
