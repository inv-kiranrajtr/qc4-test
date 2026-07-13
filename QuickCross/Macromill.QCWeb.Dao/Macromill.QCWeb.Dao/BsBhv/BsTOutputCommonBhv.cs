
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
    public partial class TOutputCommonBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputCommonDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputCommonBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_COMMON"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputCommonDbm.GetInstance(); } }
        public TOutputCommonDbm MyDBMeta { get { return TOutputCommonDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputCommon NewMyEntity() { return new TOutputCommon(); }
        public virtual TOutputCommonCB NewMyConditionBean() { return new TOutputCommonCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputCommonCB cb) {
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
        public virtual TOutputCommon SelectEntity(TOutputCommonCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputCommon> ls = null;
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
            return (TOutputCommon)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputCommon SelectEntityWithDeletedCheck(TOutputCommonCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputCommon entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputCommon SelectByPKValue(decimal? outputCommonId) {
            return SelectEntity(BuildPKCB(outputCommonId));
        }

        public virtual TOutputCommon SelectByPKValueWithDeletedCheck(decimal? outputCommonId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputCommonId));
        }

        private TOutputCommonCB BuildPKCB(decimal? outputCommonId) {
            AssertObjectNotNull("outputCommonId", outputCommonId);
            TOutputCommonCB cb = NewMyConditionBean();
            cb.Query().SetOutputCommonId_Equal(outputCommonId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputCommon> SelectList(TOutputCommonCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputCommon>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputCommon> SelectPage(TOutputCommonCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputCommon> invoker = new PagingInvoker<TOutputCommon>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputCommon> {
            protected TOutputCommonBhv _bhv; protected TOutputCommonCB _cb;
            public InternalSelectPagingHandler(TOutputCommonBhv bhv, TOutputCommonCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputCommon> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputCommon myEntity = (TOutputCommon)entity;
            myEntity.OutputCommonId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTOutputSubCklistList(TOutputCommon tOutputCommon, ConditionBeanSetupper<TOutputSubCklistCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubCklistList(xnewLRLs<TOutputCommon>(tOutputCommon), conditionBeanSetupper);
        }
        public virtual void LoadTOutputSubCklistList(IList<TOutputCommon> tOutputCommonList, ConditionBeanSetupper<TOutputSubCklistCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubCklistList(tOutputCommonList, new LoadReferrerOption<TOutputSubCklistCB, TOutputSubCklist>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputSubCklistList(TOutputCommon tOutputCommon, LoadReferrerOption<TOutputSubCklistCB, TOutputSubCklist> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputSubCklistList(xnewLRLs<TOutputCommon>(tOutputCommon), loadReferrerOption);
        }
        public virtual void LoadTOutputSubCklistList(IList<TOutputCommon> tOutputCommonList, LoadReferrerOption<TOutputSubCklistCB, TOutputSubCklist> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputCommonList.Count == 0) { return; }
            TOutputSubCklistBhv referrerBhv = xgetBSFLR().Select<TOutputSubCklistBhv>();
            HelpLoadReferrerInternally<TOutputCommon, decimal?, TOutputSubCklistCB, TOutputSubCklist>
                    (tOutputCommonList, loadReferrerOption, new MyInternalLoadTOutputSubCklistListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputSubCklistListCallback : InternalLoadReferrerCallback<TOutputCommon, decimal?, TOutputSubCklistCB, TOutputSubCklist> {
            protected TOutputSubCklistBhv referrerBhv;
            public MyInternalLoadTOutputSubCklistListCallback(TOutputSubCklistBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputCommon e) { return e.OutputCommonId; }
            public void setRfLs(TOutputCommon e, IList<TOutputSubCklist> ls) { e.TOutputSubCklistList = ls; }
            public TOutputSubCklistCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputSubCklistCB cb, IList<decimal?> ls) { cb.Query().SetOutputCommonId_InScope(ls); }
            public void qyOdFKAsc(TOutputSubCklistCB cb) { cb.Query().AddOrderBy_OutputCommonId_Asc(); }
            public void spFKCol(TOutputSubCklistCB cb) { cb.Specify().ColumnOutputCommonId(); }
            public IList<TOutputSubCklist> selRfLs(TOutputSubCklistCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputSubCklist e) { return e.OutputCommonId; }
            public void setlcEt(TOutputSubCklist re, TOutputCommon be) { re.TOutputCommon = be; }
        }
        public virtual void LoadTOutputSubCrossList(TOutputCommon tOutputCommon, ConditionBeanSetupper<TOutputSubCrossCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubCrossList(xnewLRLs<TOutputCommon>(tOutputCommon), conditionBeanSetupper);
        }
        public virtual void LoadTOutputSubCrossList(IList<TOutputCommon> tOutputCommonList, ConditionBeanSetupper<TOutputSubCrossCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubCrossList(tOutputCommonList, new LoadReferrerOption<TOutputSubCrossCB, TOutputSubCross>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputSubCrossList(TOutputCommon tOutputCommon, LoadReferrerOption<TOutputSubCrossCB, TOutputSubCross> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputSubCrossList(xnewLRLs<TOutputCommon>(tOutputCommon), loadReferrerOption);
        }
        public virtual void LoadTOutputSubCrossList(IList<TOutputCommon> tOutputCommonList, LoadReferrerOption<TOutputSubCrossCB, TOutputSubCross> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputCommonList.Count == 0) { return; }
            TOutputSubCrossBhv referrerBhv = xgetBSFLR().Select<TOutputSubCrossBhv>();
            HelpLoadReferrerInternally<TOutputCommon, decimal?, TOutputSubCrossCB, TOutputSubCross>
                    (tOutputCommonList, loadReferrerOption, new MyInternalLoadTOutputSubCrossListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputSubCrossListCallback : InternalLoadReferrerCallback<TOutputCommon, decimal?, TOutputSubCrossCB, TOutputSubCross> {
            protected TOutputSubCrossBhv referrerBhv;
            public MyInternalLoadTOutputSubCrossListCallback(TOutputSubCrossBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputCommon e) { return e.OutputCommonId; }
            public void setRfLs(TOutputCommon e, IList<TOutputSubCross> ls) { e.TOutputSubCrossList = ls; }
            public TOutputSubCrossCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputSubCrossCB cb, IList<decimal?> ls) { cb.Query().SetOutputCommonId_InScope(ls); }
            public void qyOdFKAsc(TOutputSubCrossCB cb) { cb.Query().AddOrderBy_OutputCommonId_Asc(); }
            public void spFKCol(TOutputSubCrossCB cb) { cb.Specify().ColumnOutputCommonId(); }
            public IList<TOutputSubCross> selRfLs(TOutputSubCrossCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputSubCross e) { return e.OutputCommonId; }
            public void setlcEt(TOutputSubCross re, TOutputCommon be) { re.TOutputCommon = be; }
        }
        public virtual void LoadTOutputSubFaList(TOutputCommon tOutputCommon, ConditionBeanSetupper<TOutputSubFaCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubFaList(xnewLRLs<TOutputCommon>(tOutputCommon), conditionBeanSetupper);
        }
        public virtual void LoadTOutputSubFaList(IList<TOutputCommon> tOutputCommonList, ConditionBeanSetupper<TOutputSubFaCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubFaList(tOutputCommonList, new LoadReferrerOption<TOutputSubFaCB, TOutputSubFa>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputSubFaList(TOutputCommon tOutputCommon, LoadReferrerOption<TOutputSubFaCB, TOutputSubFa> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputSubFaList(xnewLRLs<TOutputCommon>(tOutputCommon), loadReferrerOption);
        }
        public virtual void LoadTOutputSubFaList(IList<TOutputCommon> tOutputCommonList, LoadReferrerOption<TOutputSubFaCB, TOutputSubFa> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputCommonList.Count == 0) { return; }
            TOutputSubFaBhv referrerBhv = xgetBSFLR().Select<TOutputSubFaBhv>();
            HelpLoadReferrerInternally<TOutputCommon, decimal?, TOutputSubFaCB, TOutputSubFa>
                    (tOutputCommonList, loadReferrerOption, new MyInternalLoadTOutputSubFaListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputSubFaListCallback : InternalLoadReferrerCallback<TOutputCommon, decimal?, TOutputSubFaCB, TOutputSubFa> {
            protected TOutputSubFaBhv referrerBhv;
            public MyInternalLoadTOutputSubFaListCallback(TOutputSubFaBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputCommon e) { return e.OutputCommonId; }
            public void setRfLs(TOutputCommon e, IList<TOutputSubFa> ls) { e.TOutputSubFaList = ls; }
            public TOutputSubFaCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputSubFaCB cb, IList<decimal?> ls) { cb.Query().SetOutputCommonId_InScope(ls); }
            public void qyOdFKAsc(TOutputSubFaCB cb) { cb.Query().AddOrderBy_OutputCommonId_Asc(); }
            public void spFKCol(TOutputSubFaCB cb) { cb.Specify().ColumnOutputCommonId(); }
            public IList<TOutputSubFa> selRfLs(TOutputSubFaCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputSubFa e) { return e.OutputCommonId; }
            public void setlcEt(TOutputSubFa re, TOutputCommon be) { re.TOutputCommon = be; }
        }
        public virtual void LoadTOutputSubGtList(TOutputCommon tOutputCommon, ConditionBeanSetupper<TOutputSubGtCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubGtList(xnewLRLs<TOutputCommon>(tOutputCommon), conditionBeanSetupper);
        }
        public virtual void LoadTOutputSubGtList(IList<TOutputCommon> tOutputCommonList, ConditionBeanSetupper<TOutputSubGtCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputSubGtList(tOutputCommonList, new LoadReferrerOption<TOutputSubGtCB, TOutputSubGt>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputSubGtList(TOutputCommon tOutputCommon, LoadReferrerOption<TOutputSubGtCB, TOutputSubGt> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputSubGtList(xnewLRLs<TOutputCommon>(tOutputCommon), loadReferrerOption);
        }
        public virtual void LoadTOutputSubGtList(IList<TOutputCommon> tOutputCommonList, LoadReferrerOption<TOutputSubGtCB, TOutputSubGt> loadReferrerOption) {
            AssertObjectNotNull("tOutputCommonList", tOutputCommonList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputCommonList.Count == 0) { return; }
            TOutputSubGtBhv referrerBhv = xgetBSFLR().Select<TOutputSubGtBhv>();
            HelpLoadReferrerInternally<TOutputCommon, decimal?, TOutputSubGtCB, TOutputSubGt>
                    (tOutputCommonList, loadReferrerOption, new MyInternalLoadTOutputSubGtListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputSubGtListCallback : InternalLoadReferrerCallback<TOutputCommon, decimal?, TOutputSubGtCB, TOutputSubGt> {
            protected TOutputSubGtBhv referrerBhv;
            public MyInternalLoadTOutputSubGtListCallback(TOutputSubGtBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputCommon e) { return e.OutputCommonId; }
            public void setRfLs(TOutputCommon e, IList<TOutputSubGt> ls) { e.TOutputSubGtList = ls; }
            public TOutputSubGtCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputSubGtCB cb, IList<decimal?> ls) { cb.Query().SetOutputCommonId_InScope(ls); }
            public void qyOdFKAsc(TOutputSubGtCB cb) { cb.Query().AddOrderBy_OutputCommonId_Asc(); }
            public void spFKCol(TOutputSubGtCB cb) { cb.Specify().ColumnOutputCommonId(); }
            public IList<TOutputSubGt> selRfLs(TOutputSubGtCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputSubGt e) { return e.OutputCommonId; }
            public void setlcEt(TOutputSubGt re, TOutputCommon be) { re.TOutputCommon = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TOutputRequest> PulloutTOutputRequest(IList<TOutputCommon> tOutputCommonList) {
            return HelpPulloutInternally<TOutputCommon, TOutputRequest>(tOutputCommonList, new MyInternalPulloutTOutputRequestCallback());
        }
        protected class MyInternalPulloutTOutputRequestCallback : InternalPulloutCallback<TOutputCommon, TOutputRequest> {
            public TOutputRequest getFr(TOutputCommon entity) { return entity.TOutputRequest; }
        }
        public IList<TOutputSubGt> PulloutTOutputSubGt(IList<TOutputCommon> tOutputCommonList) {
            return HelpPulloutInternally<TOutputCommon, TOutputSubGt>(tOutputCommonList, new MyInternalPulloutTOutputSubGtCallback());
        }
        protected class MyInternalPulloutTOutputSubGtCallback : InternalPulloutCallback<TOutputCommon, TOutputSubGt> {
            public TOutputSubGt getFr(TOutputCommon entity) { return entity.TOutputSubGt; }
        }
        public IList<TOutputSubCross> PulloutTOutputSubCross(IList<TOutputCommon> tOutputCommonList) {
            return HelpPulloutInternally<TOutputCommon, TOutputSubCross>(tOutputCommonList, new MyInternalPulloutTOutputSubCrossCallback());
        }
        protected class MyInternalPulloutTOutputSubCrossCallback : InternalPulloutCallback<TOutputCommon, TOutputSubCross> {
            public TOutputSubCross getFr(TOutputCommon entity) { return entity.TOutputSubCross; }
        }
        public IList<TOutputSubFa> PulloutTOutputSubFa(IList<TOutputCommon> tOutputCommonList) {
            return HelpPulloutInternally<TOutputCommon, TOutputSubFa>(tOutputCommonList, new MyInternalPulloutTOutputSubFaCallback());
        }
        protected class MyInternalPulloutTOutputSubFaCallback : InternalPulloutCallback<TOutputCommon, TOutputSubFa> {
            public TOutputSubFa getFr(TOutputCommon entity) { return entity.TOutputSubFa; }
        }
        public IList<TOutputSubCklist> PulloutTOutputSubCklist(IList<TOutputCommon> tOutputCommonList) {
            return HelpPulloutInternally<TOutputCommon, TOutputSubCklist>(tOutputCommonList, new MyInternalPulloutTOutputSubCklistCallback());
        }
        protected class MyInternalPulloutTOutputSubCklistCallback : InternalPulloutCallback<TOutputCommon, TOutputSubCklist> {
            public TOutputSubCklist getFr(TOutputCommon entity) { return entity.TOutputSubCklist; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputCommon entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputCommon entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputCommon entity) {
            HelpInsertOrUpdateInternally<TOutputCommon, TOutputCommonCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputCommon, TOutputCommonCB> {
            protected TOutputCommonBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputCommonBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputCommon entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputCommon entity) { _bhv.Update(entity); }
            public TOutputCommonCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputCommonCB cb, TOutputCommon entity) {
                cb.Query().SetOutputCommonId_Equal(entity.OutputCommonId);
            }
            public int CallbackSelectCount(TOutputCommonCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputCommon entity) {
            HelpDeleteInternally<TOutputCommon>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputCommon> {
            protected TOutputCommonBhv _bhv;
            public MyInternalDeleteCallback(TOutputCommonBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputCommon entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputCommon tOutputCommon, TOutputCommonCB cb) {
            AssertObjectNotNull("tOutputCommon", tOutputCommon); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputCommon);
            FilterEntityOfUpdate(tOutputCommon); AssertEntityOfUpdate(tOutputCommon);
            return this.Dao.UpdateByQuery(cb, tOutputCommon);
        }

        public int QueryDelete(TOutputCommonCB cb) {
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
        protected int DelegateSelectCount(TOutputCommonCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputCommon> DelegateSelectList(TOutputCommonCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputCommon e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputCommon e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputCommon e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputCommon Downcast(Entity entity) {
            return (TOutputCommon)entity;
        }

        protected TOutputCommonCB Downcast(ConditionBean cb) {
            return (TOutputCommonCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputCommonDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
