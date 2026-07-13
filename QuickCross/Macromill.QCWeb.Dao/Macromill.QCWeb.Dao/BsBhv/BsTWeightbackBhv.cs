
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
    public partial class TWeightbackBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>ウェイトバックヘッダーテーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TWeightbackDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TWeightbackBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_WEIGHTBACK"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TWeightbackDbm.GetInstance(); } }
        public TWeightbackDbm MyDBMeta { get { return TWeightbackDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TWeightback NewMyEntity() { return new TWeightback(); }
        public virtual TWeightbackCB NewMyConditionBean() { return new TWeightbackCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TWeightbackCB cb) {
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
        public virtual TWeightback SelectEntity(TWeightbackCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TWeightback> ls = null;
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
            return (TWeightback)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TWeightback SelectEntityWithDeletedCheck(TWeightbackCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TWeightback entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TWeightback SelectByPKValue(decimal? weightbackId) {
            return SelectEntity(BuildPKCB(weightbackId));
        }

        public virtual TWeightback SelectByPKValueWithDeletedCheck(decimal? weightbackId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(weightbackId));
        }

        private TWeightbackCB BuildPKCB(decimal? weightbackId) {
            AssertObjectNotNull("weightbackId", weightbackId);
            TWeightbackCB cb = NewMyConditionBean();
            cb.Query().SetWeightbackId_Equal(weightbackId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TWeightback> SelectList(TWeightbackCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TWeightback>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TWeightback> SelectPage(TWeightbackCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TWeightback> invoker = new PagingInvoker<TWeightback>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TWeightback> {
            protected TWeightbackBhv _bhv; protected TWeightbackCB _cb;
            public InternalSelectPagingHandler(TWeightbackBhv bhv, TWeightbackCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TWeightback> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TWeightback myEntity = (TWeightback)entity;
            myEntity.WeightbackId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTWeightbackValueList(TWeightback tWeightback, ConditionBeanSetupper<TWeightbackValueCB> conditionBeanSetupper) {
            AssertObjectNotNull("tWeightback", tWeightback); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTWeightbackValueList(xnewLRLs<TWeightback>(tWeightback), conditionBeanSetupper);
        }
        public virtual void LoadTWeightbackValueList(IList<TWeightback> tWeightbackList, ConditionBeanSetupper<TWeightbackValueCB> conditionBeanSetupper) {
            AssertObjectNotNull("tWeightbackList", tWeightbackList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTWeightbackValueList(tWeightbackList, new LoadReferrerOption<TWeightbackValueCB, TWeightbackValue>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTWeightbackValueList(TWeightback tWeightback, LoadReferrerOption<TWeightbackValueCB, TWeightbackValue> loadReferrerOption) {
            AssertObjectNotNull("tWeightback", tWeightback); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTWeightbackValueList(xnewLRLs<TWeightback>(tWeightback), loadReferrerOption);
        }
        public virtual void LoadTWeightbackValueList(IList<TWeightback> tWeightbackList, LoadReferrerOption<TWeightbackValueCB, TWeightbackValue> loadReferrerOption) {
            AssertObjectNotNull("tWeightbackList", tWeightbackList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tWeightbackList.Count == 0) { return; }
            TWeightbackValueBhv referrerBhv = xgetBSFLR().Select<TWeightbackValueBhv>();
            HelpLoadReferrerInternally<TWeightback, decimal?, TWeightbackValueCB, TWeightbackValue>
                    (tWeightbackList, loadReferrerOption, new MyInternalLoadTWeightbackValueListCallback(referrerBhv));
        }
        protected class MyInternalLoadTWeightbackValueListCallback : InternalLoadReferrerCallback<TWeightback, decimal?, TWeightbackValueCB, TWeightbackValue> {
            protected TWeightbackValueBhv referrerBhv;
            public MyInternalLoadTWeightbackValueListCallback(TWeightbackValueBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TWeightback e) { return e.WeightbackId; }
            public void setRfLs(TWeightback e, IList<TWeightbackValue> ls) { e.TWeightbackValueList = ls; }
            public TWeightbackValueCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TWeightbackValueCB cb, IList<decimal?> ls) { cb.Query().SetWeightbackId_InScope(ls); }
            public void qyOdFKAsc(TWeightbackValueCB cb) { cb.Query().AddOrderBy_WeightbackId_Asc(); }
            public void spFKCol(TWeightbackValueCB cb) { cb.Specify().ColumnWeightbackId(); }
            public IList<TWeightbackValue> selRfLs(TWeightbackValueCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TWeightbackValue e) { return e.WeightbackId; }
            public void setlcEt(TWeightbackValue re, TWeightback be) { re.TWeightback = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TWeightback> tWeightbackList) {
            return HelpPulloutInternally<TWeightback, TQcwebSurveyInfo>(tWeightbackList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TWeightback, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TWeightback entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TWeightbackValue> PulloutTWeightbackValue(IList<TWeightback> tWeightbackList) {
            return HelpPulloutInternally<TWeightback, TWeightbackValue>(tWeightbackList, new MyInternalPulloutTWeightbackValueCallback());
        }
        protected class MyInternalPulloutTWeightbackValueCallback : InternalPulloutCallback<TWeightback, TWeightbackValue> {
            public TWeightbackValue getFr(TWeightback entity) { return entity.TWeightbackValue; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TWeightback entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TWeightback entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TWeightback entity) {
            HelpInsertOrUpdateInternally<TWeightback, TWeightbackCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TWeightback, TWeightbackCB> {
            protected TWeightbackBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TWeightbackBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TWeightback entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TWeightback entity) { _bhv.Update(entity); }
            public TWeightbackCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TWeightbackCB cb, TWeightback entity) {
                cb.Query().SetWeightbackId_Equal(entity.WeightbackId);
            }
            public int CallbackSelectCount(TWeightbackCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TWeightback entity) {
            HelpDeleteInternally<TWeightback>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TWeightback> {
            protected TWeightbackBhv _bhv;
            public MyInternalDeleteCallback(TWeightbackBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TWeightback entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TWeightback tWeightback, TWeightbackCB cb) {
            AssertObjectNotNull("tWeightback", tWeightback); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tWeightback);
            FilterEntityOfUpdate(tWeightback); AssertEntityOfUpdate(tWeightback);
            return this.Dao.UpdateByQuery(cb, tWeightback);
        }

        public int QueryDelete(TWeightbackCB cb) {
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
        protected int DelegateSelectCount(TWeightbackCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TWeightback> DelegateSelectList(TWeightbackCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TWeightback e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TWeightback e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TWeightback e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TWeightback Downcast(Entity entity) {
            return (TWeightback)entity;
        }

        protected TWeightbackCB Downcast(ConditionBean cb) {
            return (TWeightbackCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TWeightbackDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
