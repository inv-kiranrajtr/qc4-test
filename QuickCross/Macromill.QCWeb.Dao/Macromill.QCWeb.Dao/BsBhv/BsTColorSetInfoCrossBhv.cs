
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
    public partial class TColorSetInfoCrossBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorSetInfoCrossDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorSetInfoCrossBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_SET_INFO_CROSS"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TColorSetInfoCrossDbm.GetInstance(); } }
        public TColorSetInfoCrossDbm MyDBMeta { get { return TColorSetInfoCrossDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TColorSetInfoCross NewMyEntity() { return new TColorSetInfoCross(); }
        public virtual TColorSetInfoCrossCB NewMyConditionBean() { return new TColorSetInfoCrossCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TColorSetInfoCrossCB cb) {
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
        public virtual TColorSetInfoCross SelectEntity(TColorSetInfoCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TColorSetInfoCross> ls = null;
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
            return (TColorSetInfoCross)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TColorSetInfoCross SelectEntityWithDeletedCheck(TColorSetInfoCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TColorSetInfoCross entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TColorSetInfoCross SelectByPKValue(decimal? colorSetInfoCrossId) {
            return SelectEntity(BuildPKCB(colorSetInfoCrossId));
        }

        public virtual TColorSetInfoCross SelectByPKValueWithDeletedCheck(decimal? colorSetInfoCrossId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(colorSetInfoCrossId));
        }

        private TColorSetInfoCrossCB BuildPKCB(decimal? colorSetInfoCrossId) {
            AssertObjectNotNull("colorSetInfoCrossId", colorSetInfoCrossId);
            TColorSetInfoCrossCB cb = NewMyConditionBean();
            cb.Query().SetColorSetInfoCrossId_Equal(colorSetInfoCrossId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TColorSetInfoCross> SelectList(TColorSetInfoCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TColorSetInfoCross>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TColorSetInfoCross> SelectPage(TColorSetInfoCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TColorSetInfoCross> invoker = new PagingInvoker<TColorSetInfoCross>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TColorSetInfoCross> {
            protected TColorSetInfoCrossBhv _bhv; protected TColorSetInfoCrossCB _cb;
            public InternalSelectPagingHandler(TColorSetInfoCrossBhv bhv, TColorSetInfoCrossCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TColorSetInfoCross> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TColorSetInfoCross myEntity = (TColorSetInfoCross)entity;
            myEntity.ColorSetInfoCrossId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTColorInfoDetailCrossList(TColorSetInfoCross tColorSetInfoCross, ConditionBeanSetupper<TColorInfoDetailCrossCB> conditionBeanSetupper) {
            AssertObjectNotNull("tColorSetInfoCross", tColorSetInfoCross); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorInfoDetailCrossList(xnewLRLs<TColorSetInfoCross>(tColorSetInfoCross), conditionBeanSetupper);
        }
        public virtual void LoadTColorInfoDetailCrossList(IList<TColorSetInfoCross> tColorSetInfoCrossList, ConditionBeanSetupper<TColorInfoDetailCrossCB> conditionBeanSetupper) {
            AssertObjectNotNull("tColorSetInfoCrossList", tColorSetInfoCrossList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTColorInfoDetailCrossList(tColorSetInfoCrossList, new LoadReferrerOption<TColorInfoDetailCrossCB, TColorInfoDetailCross>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTColorInfoDetailCrossList(TColorSetInfoCross tColorSetInfoCross, LoadReferrerOption<TColorInfoDetailCrossCB, TColorInfoDetailCross> loadReferrerOption) {
            AssertObjectNotNull("tColorSetInfoCross", tColorSetInfoCross); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTColorInfoDetailCrossList(xnewLRLs<TColorSetInfoCross>(tColorSetInfoCross), loadReferrerOption);
        }
        public virtual void LoadTColorInfoDetailCrossList(IList<TColorSetInfoCross> tColorSetInfoCrossList, LoadReferrerOption<TColorInfoDetailCrossCB, TColorInfoDetailCross> loadReferrerOption) {
            AssertObjectNotNull("tColorSetInfoCrossList", tColorSetInfoCrossList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tColorSetInfoCrossList.Count == 0) { return; }
            TColorInfoDetailCrossBhv referrerBhv = xgetBSFLR().Select<TColorInfoDetailCrossBhv>();
            HelpLoadReferrerInternally<TColorSetInfoCross, decimal?, TColorInfoDetailCrossCB, TColorInfoDetailCross>
                    (tColorSetInfoCrossList, loadReferrerOption, new MyInternalLoadTColorInfoDetailCrossListCallback(referrerBhv));
        }
        protected class MyInternalLoadTColorInfoDetailCrossListCallback : InternalLoadReferrerCallback<TColorSetInfoCross, decimal?, TColorInfoDetailCrossCB, TColorInfoDetailCross> {
            protected TColorInfoDetailCrossBhv referrerBhv;
            public MyInternalLoadTColorInfoDetailCrossListCallback(TColorInfoDetailCrossBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TColorSetInfoCross e) { return e.ColorSetInfoCrossId; }
            public void setRfLs(TColorSetInfoCross e, IList<TColorInfoDetailCross> ls) { e.TColorInfoDetailCrossList = ls; }
            public TColorInfoDetailCrossCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TColorInfoDetailCrossCB cb, IList<decimal?> ls) { cb.Query().SetColorSetInfoCrossId_InScope(ls); }
            public void qyOdFKAsc(TColorInfoDetailCrossCB cb) { cb.Query().AddOrderBy_ColorSetInfoCrossId_Asc(); }
            public void spFKCol(TColorInfoDetailCrossCB cb) { cb.Specify().ColumnColorSetInfoCrossId(); }
            public IList<TColorInfoDetailCross> selRfLs(TColorInfoDetailCrossCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TColorInfoDetailCross e) { return e.ColorSetInfoCrossId; }
            public void setlcEt(TColorInfoDetailCross re, TColorSetInfoCross be) { re.TColorSetInfoCross = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TCrossScenarioTarget> PulloutTCrossScenarioTarget(IList<TColorSetInfoCross> tColorSetInfoCrossList) {
            return HelpPulloutInternally<TColorSetInfoCross, TCrossScenarioTarget>(tColorSetInfoCrossList, new MyInternalPulloutTCrossScenarioTargetCallback());
        }
        protected class MyInternalPulloutTCrossScenarioTargetCallback : InternalPulloutCallback<TColorSetInfoCross, TCrossScenarioTarget> {
            public TCrossScenarioTarget getFr(TColorSetInfoCross entity) { return entity.TCrossScenarioTarget; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TColorSetInfoCross entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TColorSetInfoCross entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TColorSetInfoCross entity) {
            HelpInsertOrUpdateInternally<TColorSetInfoCross, TColorSetInfoCrossCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TColorSetInfoCross, TColorSetInfoCrossCB> {
            protected TColorSetInfoCrossBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TColorSetInfoCrossBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TColorSetInfoCross entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TColorSetInfoCross entity) { _bhv.Update(entity); }
            public TColorSetInfoCrossCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TColorSetInfoCrossCB cb, TColorSetInfoCross entity) {
                cb.Query().SetColorSetInfoCrossId_Equal(entity.ColorSetInfoCrossId);
            }
            public int CallbackSelectCount(TColorSetInfoCrossCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TColorSetInfoCross entity) {
            HelpDeleteInternally<TColorSetInfoCross>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TColorSetInfoCross> {
            protected TColorSetInfoCrossBhv _bhv;
            public MyInternalDeleteCallback(TColorSetInfoCrossBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TColorSetInfoCross entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TColorSetInfoCross tColorSetInfoCross, TColorSetInfoCrossCB cb) {
            AssertObjectNotNull("tColorSetInfoCross", tColorSetInfoCross); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tColorSetInfoCross);
            FilterEntityOfUpdate(tColorSetInfoCross); AssertEntityOfUpdate(tColorSetInfoCross);
            return this.Dao.UpdateByQuery(cb, tColorSetInfoCross);
        }

        public int QueryDelete(TColorSetInfoCrossCB cb) {
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
        protected int DelegateSelectCount(TColorSetInfoCrossCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TColorSetInfoCross> DelegateSelectList(TColorSetInfoCrossCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TColorSetInfoCross e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TColorSetInfoCross e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TColorSetInfoCross e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TColorSetInfoCross Downcast(Entity entity) {
            return (TColorSetInfoCross)entity;
        }

        protected TColorSetInfoCrossCB Downcast(ConditionBean cb) {
            return (TColorSetInfoCrossCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TColorSetInfoCrossDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
