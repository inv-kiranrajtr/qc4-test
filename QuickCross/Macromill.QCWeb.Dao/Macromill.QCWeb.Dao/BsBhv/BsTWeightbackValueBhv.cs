
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
    public partial class TWeightbackValueBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TWeightbackValueDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TWeightbackValueBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_WEIGHTBACK_VALUE"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TWeightbackValueDbm.GetInstance(); } }
        public TWeightbackValueDbm MyDBMeta { get { return TWeightbackValueDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TWeightbackValue NewMyEntity() { return new TWeightbackValue(); }
        public virtual TWeightbackValueCB NewMyConditionBean() { return new TWeightbackValueCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TWeightbackValueCB cb) {
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
        public virtual TWeightbackValue SelectEntity(TWeightbackValueCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TWeightbackValue> ls = null;
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
            return (TWeightbackValue)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TWeightbackValue SelectEntityWithDeletedCheck(TWeightbackValueCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TWeightbackValue entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TWeightbackValue SelectByPKValue(decimal? weightbackValueId) {
            return SelectEntity(BuildPKCB(weightbackValueId));
        }

        public virtual TWeightbackValue SelectByPKValueWithDeletedCheck(decimal? weightbackValueId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(weightbackValueId));
        }

        private TWeightbackValueCB BuildPKCB(decimal? weightbackValueId) {
            AssertObjectNotNull("weightbackValueId", weightbackValueId);
            TWeightbackValueCB cb = NewMyConditionBean();
            cb.Query().SetWeightbackValueId_Equal(weightbackValueId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TWeightbackValue> SelectList(TWeightbackValueCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TWeightbackValue>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TWeightbackValue> SelectPage(TWeightbackValueCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TWeightbackValue> invoker = new PagingInvoker<TWeightbackValue>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TWeightbackValue> {
            protected TWeightbackValueBhv _bhv; protected TWeightbackValueCB _cb;
            public InternalSelectPagingHandler(TWeightbackValueBhv bhv, TWeightbackValueCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TWeightbackValue> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TWeightbackValue myEntity = (TWeightbackValue)entity;
            myEntity.WeightbackValueId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TWeightback> PulloutTWeightback(IList<TWeightbackValue> tWeightbackValueList) {
            return HelpPulloutInternally<TWeightbackValue, TWeightback>(tWeightbackValueList, new MyInternalPulloutTWeightbackCallback());
        }
        protected class MyInternalPulloutTWeightbackCallback : InternalPulloutCallback<TWeightbackValue, TWeightback> {
            public TWeightback getFr(TWeightbackValue entity) { return entity.TWeightback; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TWeightbackValue entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TWeightbackValue entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TWeightbackValue entity) {
            HelpInsertOrUpdateInternally<TWeightbackValue, TWeightbackValueCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TWeightbackValue, TWeightbackValueCB> {
            protected TWeightbackValueBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TWeightbackValueBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TWeightbackValue entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TWeightbackValue entity) { _bhv.Update(entity); }
            public TWeightbackValueCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TWeightbackValueCB cb, TWeightbackValue entity) {
                cb.Query().SetWeightbackValueId_Equal(entity.WeightbackValueId);
            }
            public int CallbackSelectCount(TWeightbackValueCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TWeightbackValue entity) {
            HelpDeleteInternally<TWeightbackValue>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TWeightbackValue> {
            protected TWeightbackValueBhv _bhv;
            public MyInternalDeleteCallback(TWeightbackValueBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TWeightbackValue entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TWeightbackValue tWeightbackValue, TWeightbackValueCB cb) {
            AssertObjectNotNull("tWeightbackValue", tWeightbackValue); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tWeightbackValue);
            FilterEntityOfUpdate(tWeightbackValue); AssertEntityOfUpdate(tWeightbackValue);
            return this.Dao.UpdateByQuery(cb, tWeightbackValue);
        }

        public int QueryDelete(TWeightbackValueCB cb) {
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
        protected int DelegateSelectCount(TWeightbackValueCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TWeightbackValue> DelegateSelectList(TWeightbackValueCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TWeightbackValue e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TWeightbackValue e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TWeightbackValue e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TWeightbackValue Downcast(Entity entity) {
            return (TWeightbackValue)entity;
        }

        protected TWeightbackValueCB Downcast(ConditionBean cb) {
            return (TWeightbackValueCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TWeightbackValueDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
