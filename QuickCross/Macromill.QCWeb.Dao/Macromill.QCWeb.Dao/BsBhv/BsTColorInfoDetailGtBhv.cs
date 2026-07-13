
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
    public partial class TColorInfoDetailGtBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorInfoDetailGtDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorInfoDetailGtBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_INFO_DETAIL_GT"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TColorInfoDetailGtDbm.GetInstance(); } }
        public TColorInfoDetailGtDbm MyDBMeta { get { return TColorInfoDetailGtDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TColorInfoDetailGt NewMyEntity() { return new TColorInfoDetailGt(); }
        public virtual TColorInfoDetailGtCB NewMyConditionBean() { return new TColorInfoDetailGtCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TColorInfoDetailGtCB cb) {
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
        public virtual TColorInfoDetailGt SelectEntity(TColorInfoDetailGtCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TColorInfoDetailGt> ls = null;
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
            return (TColorInfoDetailGt)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TColorInfoDetailGt SelectEntityWithDeletedCheck(TColorInfoDetailGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TColorInfoDetailGt entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TColorInfoDetailGt SelectByPKValue(decimal? colorInfoDetailGtId) {
            return SelectEntity(BuildPKCB(colorInfoDetailGtId));
        }

        public virtual TColorInfoDetailGt SelectByPKValueWithDeletedCheck(decimal? colorInfoDetailGtId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(colorInfoDetailGtId));
        }

        private TColorInfoDetailGtCB BuildPKCB(decimal? colorInfoDetailGtId) {
            AssertObjectNotNull("colorInfoDetailGtId", colorInfoDetailGtId);
            TColorInfoDetailGtCB cb = NewMyConditionBean();
            cb.Query().SetColorInfoDetailGtId_Equal(colorInfoDetailGtId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TColorInfoDetailGt> SelectList(TColorInfoDetailGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TColorInfoDetailGt>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TColorInfoDetailGt> SelectPage(TColorInfoDetailGtCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TColorInfoDetailGt> invoker = new PagingInvoker<TColorInfoDetailGt>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TColorInfoDetailGt> {
            protected TColorInfoDetailGtBhv _bhv; protected TColorInfoDetailGtCB _cb;
            public InternalSelectPagingHandler(TColorInfoDetailGtBhv bhv, TColorInfoDetailGtCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TColorInfoDetailGt> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TColorInfoDetailGt myEntity = (TColorInfoDetailGt)entity;
            myEntity.ColorInfoDetailGtId = SelectNextVal();
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
        public IList<TColorSetInfoGt> PulloutTColorSetInfoGt(IList<TColorInfoDetailGt> tColorInfoDetailGtList) {
            return HelpPulloutInternally<TColorInfoDetailGt, TColorSetInfoGt>(tColorInfoDetailGtList, new MyInternalPulloutTColorSetInfoGtCallback());
        }
        protected class MyInternalPulloutTColorSetInfoGtCallback : InternalPulloutCallback<TColorInfoDetailGt, TColorSetInfoGt> {
            public TColorSetInfoGt getFr(TColorInfoDetailGt entity) { return entity.TColorSetInfoGt; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TColorInfoDetailGt entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TColorInfoDetailGt entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TColorInfoDetailGt entity) {
            HelpInsertOrUpdateInternally<TColorInfoDetailGt, TColorInfoDetailGtCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TColorInfoDetailGt, TColorInfoDetailGtCB> {
            protected TColorInfoDetailGtBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TColorInfoDetailGtBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TColorInfoDetailGt entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TColorInfoDetailGt entity) { _bhv.Update(entity); }
            public TColorInfoDetailGtCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TColorInfoDetailGtCB cb, TColorInfoDetailGt entity) {
                cb.Query().SetColorInfoDetailGtId_Equal(entity.ColorInfoDetailGtId);
            }
            public int CallbackSelectCount(TColorInfoDetailGtCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TColorInfoDetailGt entity) {
            HelpDeleteInternally<TColorInfoDetailGt>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TColorInfoDetailGt> {
            protected TColorInfoDetailGtBhv _bhv;
            public MyInternalDeleteCallback(TColorInfoDetailGtBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TColorInfoDetailGt entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TColorInfoDetailGt tColorInfoDetailGt, TColorInfoDetailGtCB cb) {
            AssertObjectNotNull("tColorInfoDetailGt", tColorInfoDetailGt); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tColorInfoDetailGt);
            FilterEntityOfUpdate(tColorInfoDetailGt); AssertEntityOfUpdate(tColorInfoDetailGt);
            return this.Dao.UpdateByQuery(cb, tColorInfoDetailGt);
        }

        public int QueryDelete(TColorInfoDetailGtCB cb) {
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
        protected int DelegateSelectCount(TColorInfoDetailGtCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TColorInfoDetailGt> DelegateSelectList(TColorInfoDetailGtCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TColorInfoDetailGt e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TColorInfoDetailGt e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TColorInfoDetailGt e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TColorInfoDetailGt Downcast(Entity entity) {
            return (TColorInfoDetailGt)entity;
        }

        protected TColorInfoDetailGtCB Downcast(ConditionBean cb) {
            return (TColorInfoDetailGtCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TColorInfoDetailGtDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
