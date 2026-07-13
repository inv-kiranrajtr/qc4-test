
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
    public partial class TCategoryOutputEditBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>カテゴリ出力編集テーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TCategoryOutputEditDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TCategoryOutputEditBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_CATEGORY_OUTPUT_EDIT"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TCategoryOutputEditDbm.GetInstance(); } }
        public TCategoryOutputEditDbm MyDBMeta { get { return TCategoryOutputEditDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TCategoryOutputEdit NewMyEntity() { return new TCategoryOutputEdit(); }
        public virtual TCategoryOutputEditCB NewMyConditionBean() { return new TCategoryOutputEditCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TCategoryOutputEditCB cb) {
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
        public virtual TCategoryOutputEdit SelectEntity(TCategoryOutputEditCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TCategoryOutputEdit> ls = null;
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
            return (TCategoryOutputEdit)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TCategoryOutputEdit SelectEntityWithDeletedCheck(TCategoryOutputEditCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TCategoryOutputEdit entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TCategoryOutputEdit SelectByPKValue(decimal? categoryOutputEditId) {
            return SelectEntity(BuildPKCB(categoryOutputEditId));
        }

        public virtual TCategoryOutputEdit SelectByPKValueWithDeletedCheck(decimal? categoryOutputEditId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(categoryOutputEditId));
        }

        private TCategoryOutputEditCB BuildPKCB(decimal? categoryOutputEditId) {
            AssertObjectNotNull("categoryOutputEditId", categoryOutputEditId);
            TCategoryOutputEditCB cb = NewMyConditionBean();
            cb.Query().SetCategoryOutputEditId_Equal(categoryOutputEditId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TCategoryOutputEdit> SelectList(TCategoryOutputEditCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TCategoryOutputEdit>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TCategoryOutputEdit> SelectPage(TCategoryOutputEditCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TCategoryOutputEdit> invoker = new PagingInvoker<TCategoryOutputEdit>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TCategoryOutputEdit> {
            protected TCategoryOutputEditBhv _bhv; protected TCategoryOutputEditCB _cb;
            public InternalSelectPagingHandler(TCategoryOutputEditBhv bhv, TCategoryOutputEditCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TCategoryOutputEdit> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TCategoryOutputEdit myEntity = (TCategoryOutputEdit)entity;
            myEntity.CategoryOutputEditId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTCategoryOutputDetailList(TCategoryOutputEdit tCategoryOutputEdit, ConditionBeanSetupper<TCategoryOutputDetailCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCategoryOutputEdit", tCategoryOutputEdit); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCategoryOutputDetailList(xnewLRLs<TCategoryOutputEdit>(tCategoryOutputEdit), conditionBeanSetupper);
        }
        public virtual void LoadTCategoryOutputDetailList(IList<TCategoryOutputEdit> tCategoryOutputEditList, ConditionBeanSetupper<TCategoryOutputDetailCB> conditionBeanSetupper) {
            AssertObjectNotNull("tCategoryOutputEditList", tCategoryOutputEditList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCategoryOutputDetailList(tCategoryOutputEditList, new LoadReferrerOption<TCategoryOutputDetailCB, TCategoryOutputDetail>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTCategoryOutputDetailList(TCategoryOutputEdit tCategoryOutputEdit, LoadReferrerOption<TCategoryOutputDetailCB, TCategoryOutputDetail> loadReferrerOption) {
            AssertObjectNotNull("tCategoryOutputEdit", tCategoryOutputEdit); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTCategoryOutputDetailList(xnewLRLs<TCategoryOutputEdit>(tCategoryOutputEdit), loadReferrerOption);
        }
        public virtual void LoadTCategoryOutputDetailList(IList<TCategoryOutputEdit> tCategoryOutputEditList, LoadReferrerOption<TCategoryOutputDetailCB, TCategoryOutputDetail> loadReferrerOption) {
            AssertObjectNotNull("tCategoryOutputEditList", tCategoryOutputEditList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tCategoryOutputEditList.Count == 0) { return; }
            TCategoryOutputDetailBhv referrerBhv = xgetBSFLR().Select<TCategoryOutputDetailBhv>();
            HelpLoadReferrerInternally<TCategoryOutputEdit, decimal?, TCategoryOutputDetailCB, TCategoryOutputDetail>
                    (tCategoryOutputEditList, loadReferrerOption, new MyInternalLoadTCategoryOutputDetailListCallback(referrerBhv));
        }
        protected class MyInternalLoadTCategoryOutputDetailListCallback : InternalLoadReferrerCallback<TCategoryOutputEdit, decimal?, TCategoryOutputDetailCB, TCategoryOutputDetail> {
            protected TCategoryOutputDetailBhv referrerBhv;
            public MyInternalLoadTCategoryOutputDetailListCallback(TCategoryOutputDetailBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TCategoryOutputEdit e) { return e.CategoryOutputEditId; }
            public void setRfLs(TCategoryOutputEdit e, IList<TCategoryOutputDetail> ls) { e.TCategoryOutputDetailList = ls; }
            public TCategoryOutputDetailCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TCategoryOutputDetailCB cb, IList<decimal?> ls) { cb.Query().SetCategoryOutputEditId_InScope(ls); }
            public void qyOdFKAsc(TCategoryOutputDetailCB cb) { cb.Query().AddOrderBy_CategoryOutputEditId_Asc(); }
            public void spFKCol(TCategoryOutputDetailCB cb) { cb.Specify().ColumnCategoryOutputEditId(); }
            public IList<TCategoryOutputDetail> selRfLs(TCategoryOutputDetailCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TCategoryOutputDetail e) { return e.CategoryOutputEditId; }
            public void setlcEt(TCategoryOutputDetail re, TCategoryOutputEdit be) { re.TCategoryOutputEdit = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TCategoryOutputEdit> tCategoryOutputEditList) {
            return HelpPulloutInternally<TCategoryOutputEdit, TScenarioTotalization>(tCategoryOutputEditList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TCategoryOutputEdit, TScenarioTotalization> {
            public TScenarioTotalization getFr(TCategoryOutputEdit entity) { return entity.TScenarioTotalization; }
        }
        public IList<TCategoryOutputDetail> PulloutTCategoryOutputDetail(IList<TCategoryOutputEdit> tCategoryOutputEditList) {
            return HelpPulloutInternally<TCategoryOutputEdit, TCategoryOutputDetail>(tCategoryOutputEditList, new MyInternalPulloutTCategoryOutputDetailCallback());
        }
        protected class MyInternalPulloutTCategoryOutputDetailCallback : InternalPulloutCallback<TCategoryOutputEdit, TCategoryOutputDetail> {
            public TCategoryOutputDetail getFr(TCategoryOutputEdit entity) { return entity.TCategoryOutputDetail; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TCategoryOutputEdit entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TCategoryOutputEdit entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TCategoryOutputEdit entity) {
            HelpInsertOrUpdateInternally<TCategoryOutputEdit, TCategoryOutputEditCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TCategoryOutputEdit, TCategoryOutputEditCB> {
            protected TCategoryOutputEditBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TCategoryOutputEditBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TCategoryOutputEdit entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TCategoryOutputEdit entity) { _bhv.Update(entity); }
            public TCategoryOutputEditCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TCategoryOutputEditCB cb, TCategoryOutputEdit entity) {
                cb.Query().SetCategoryOutputEditId_Equal(entity.CategoryOutputEditId);
            }
            public int CallbackSelectCount(TCategoryOutputEditCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TCategoryOutputEdit entity) {
            HelpDeleteInternally<TCategoryOutputEdit>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TCategoryOutputEdit> {
            protected TCategoryOutputEditBhv _bhv;
            public MyInternalDeleteCallback(TCategoryOutputEditBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TCategoryOutputEdit entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TCategoryOutputEdit tCategoryOutputEdit, TCategoryOutputEditCB cb) {
            AssertObjectNotNull("tCategoryOutputEdit", tCategoryOutputEdit); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tCategoryOutputEdit);
            FilterEntityOfUpdate(tCategoryOutputEdit); AssertEntityOfUpdate(tCategoryOutputEdit);
            return this.Dao.UpdateByQuery(cb, tCategoryOutputEdit);
        }

        public int QueryDelete(TCategoryOutputEditCB cb) {
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
        protected int DelegateSelectCount(TCategoryOutputEditCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TCategoryOutputEdit> DelegateSelectList(TCategoryOutputEditCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TCategoryOutputEdit e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TCategoryOutputEdit e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TCategoryOutputEdit e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TCategoryOutputEdit Downcast(Entity entity) {
            return (TCategoryOutputEdit)entity;
        }

        protected TCategoryOutputEditCB Downcast(ConditionBean cb) {
            return (TCategoryOutputEditCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TCategoryOutputEditDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
