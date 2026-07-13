
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
    public partial class TColorSetInfoGtBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorSetInfoGtDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorSetInfoGtBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_SET_INFO_GT"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TColorSetInfoGtDbm.GetInstance(); } }
        public TColorSetInfoGtDbm MyDBMeta { get { return TColorSetInfoGtDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TColorSetInfoGt NewMyEntity() { return new TColorSetInfoGt(); }
        public virtual TColorSetInfoGtCB NewMyConditionBean() { return new TColorSetInfoGtCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TColorSetInfoGtCB cb) {
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
        public virtual TColorSetInfoGt SelectEntity(TColorSetInfoGtCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TColorSetInfoGt> ls = null;
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
            return (TColorSetInfoGt)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TColorSetInfoGt SelectEntityWithDeletedCheck(TColorSetInfoGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TColorSetInfoGt entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TColorSetInfoGt SelectByPKValue(decimal? colorSetInfoGtId) {
            return SelectEntity(BuildPKCB(colorSetInfoGtId));
        }

        public virtual TColorSetInfoGt SelectByPKValueWithDeletedCheck(decimal? colorSetInfoGtId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(colorSetInfoGtId));
        }

        private TColorSetInfoGtCB BuildPKCB(decimal? colorSetInfoGtId) {
            AssertObjectNotNull("colorSetInfoGtId", colorSetInfoGtId);
            TColorSetInfoGtCB cb = NewMyConditionBean();
            cb.Query().SetColorSetInfoGtId_Equal(colorSetInfoGtId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TColorSetInfoGt> SelectList(TColorSetInfoGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TColorSetInfoGt>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TColorSetInfoGt> SelectPage(TColorSetInfoGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TColorSetInfoGt> invoker = new PagingInvoker<TColorSetInfoGt>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TColorSetInfoGt> {
            protected TColorSetInfoGtBhv _bhv; protected TColorSetInfoGtCB _cb;
            public InternalSelectPagingHandler(TColorSetInfoGtBhv bhv, TColorSetInfoGtCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TColorSetInfoGt> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TColorSetInfoGt myEntity = (TColorSetInfoGt)entity;
            myEntity.ColorSetInfoGtId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTColorInfoDetailGtList(TColorSetInfoGt tColorSetInfoGt, ConditionBeanSetupper<TColorInfoDetailGtCB> conditionBeanSetupper) {
            AssertObjectNotNull("tColorSetInfoGt", tColorSetInfoGt); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorInfoDetailGtList(xnewLRLs<TColorSetInfoGt>(tColorSetInfoGt), conditionBeanSetupper);
        }
        public virtual void LoadTColorInfoDetailGtList(IList<TColorSetInfoGt> tColorSetInfoGtList, ConditionBeanSetupper<TColorInfoDetailGtCB> conditionBeanSetupper) {
            AssertObjectNotNull("tColorSetInfoGtList", tColorSetInfoGtList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorInfoDetailGtList(tColorSetInfoGtList, new LoadReferrerOption<TColorInfoDetailGtCB, TColorInfoDetailGt>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTColorInfoDetailGtList(TColorSetInfoGt tColorSetInfoGt, LoadReferrerOption<TColorInfoDetailGtCB, TColorInfoDetailGt> loadReferrerOption) {
            AssertObjectNotNull("tColorSetInfoGt", tColorSetInfoGt); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTColorInfoDetailGtList(xnewLRLs<TColorSetInfoGt>(tColorSetInfoGt), loadReferrerOption);
        }
        public virtual void LoadTColorInfoDetailGtList(IList<TColorSetInfoGt> tColorSetInfoGtList, LoadReferrerOption<TColorInfoDetailGtCB, TColorInfoDetailGt> loadReferrerOption) {
            AssertObjectNotNull("tColorSetInfoGtList", tColorSetInfoGtList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tColorSetInfoGtList.Count == 0) { return; }
            TColorInfoDetailGtBhv referrerBhv = xgetBSFLR().Select<TColorInfoDetailGtBhv>();
            HelpLoadReferrerInternally<TColorSetInfoGt, decimal?, TColorInfoDetailGtCB, TColorInfoDetailGt>
                    (tColorSetInfoGtList, loadReferrerOption, new MyInternalLoadTColorInfoDetailGtListCallback(referrerBhv));
        }
        protected class MyInternalLoadTColorInfoDetailGtListCallback : InternalLoadReferrerCallback<TColorSetInfoGt, decimal?, TColorInfoDetailGtCB, TColorInfoDetailGt> {
            protected TColorInfoDetailGtBhv referrerBhv;
            public MyInternalLoadTColorInfoDetailGtListCallback(TColorInfoDetailGtBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TColorSetInfoGt e) { return e.ColorSetInfoGtId; }
            public void setRfLs(TColorSetInfoGt e, IList<TColorInfoDetailGt> ls) { e.TColorInfoDetailGtList = ls; }
            public TColorInfoDetailGtCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TColorInfoDetailGtCB cb, IList<decimal?> ls) { cb.Query().SetColorSetInfoGtId_InScope(ls); }
            public void qyOdFKAsc(TColorInfoDetailGtCB cb) { cb.Query().AddOrderBy_ColorSetInfoGtId_Asc(); }
            public void spFKCol(TColorInfoDetailGtCB cb) { cb.Specify().ColumnColorSetInfoGtId(); }
            public IList<TColorInfoDetailGt> selRfLs(TColorInfoDetailGtCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TColorInfoDetailGt e) { return e.ColorSetInfoGtId; }
            public void setlcEt(TColorInfoDetailGt re, TColorSetInfoGt be) { re.TColorSetInfoGt = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TGtScenarioItem> PulloutTGtScenarioItem(IList<TColorSetInfoGt> tColorSetInfoGtList) {
            return HelpPulloutInternally<TColorSetInfoGt, TGtScenarioItem>(tColorSetInfoGtList, new MyInternalPulloutTGtScenarioItemCallback());
        }
        protected class MyInternalPulloutTGtScenarioItemCallback : InternalPulloutCallback<TColorSetInfoGt, TGtScenarioItem> {
            public TGtScenarioItem getFr(TColorSetInfoGt entity) { return entity.TGtScenarioItem; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TColorSetInfoGt entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TColorSetInfoGt entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TColorSetInfoGt entity) {
            HelpInsertOrUpdateInternally<TColorSetInfoGt, TColorSetInfoGtCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TColorSetInfoGt, TColorSetInfoGtCB> {
            protected TColorSetInfoGtBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TColorSetInfoGtBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TColorSetInfoGt entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TColorSetInfoGt entity) { _bhv.Update(entity); }
            public TColorSetInfoGtCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TColorSetInfoGtCB cb, TColorSetInfoGt entity) {
                cb.Query().SetColorSetInfoGtId_Equal(entity.ColorSetInfoGtId);
            }
            public int CallbackSelectCount(TColorSetInfoGtCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TColorSetInfoGt entity) {
            HelpDeleteInternally<TColorSetInfoGt>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TColorSetInfoGt> {
            protected TColorSetInfoGtBhv _bhv;
            public MyInternalDeleteCallback(TColorSetInfoGtBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TColorSetInfoGt entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TColorSetInfoGt tColorSetInfoGt, TColorSetInfoGtCB cb) {
            AssertObjectNotNull("tColorSetInfoGt", tColorSetInfoGt); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tColorSetInfoGt);
            FilterEntityOfUpdate(tColorSetInfoGt); AssertEntityOfUpdate(tColorSetInfoGt);
            return this.Dao.UpdateByQuery(cb, tColorSetInfoGt);
        }

        public int QueryDelete(TColorSetInfoGtCB cb) {
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
        protected int DelegateSelectCount(TColorSetInfoGtCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TColorSetInfoGt> DelegateSelectList(TColorSetInfoGtCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TColorSetInfoGt e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TColorSetInfoGt e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TColorSetInfoGt e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TColorSetInfoGt Downcast(Entity entity) {
            return (TColorSetInfoGt)entity;
        }

        protected TColorSetInfoGtCB Downcast(ConditionBean cb) {
            return (TColorSetInfoGtCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TColorSetInfoGtDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
