
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
    public partial class TColorInfoDetailCrossBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TColorInfoDetailCrossDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TColorInfoDetailCrossBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_COLOR_INFO_DETAIL_CROSS"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TColorInfoDetailCrossDbm.GetInstance(); } }
        public TColorInfoDetailCrossDbm MyDBMeta { get { return TColorInfoDetailCrossDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TColorInfoDetailCross NewMyEntity() { return new TColorInfoDetailCross(); }
        public virtual TColorInfoDetailCrossCB NewMyConditionBean() { return new TColorInfoDetailCrossCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TColorInfoDetailCrossCB cb) {
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
        public virtual TColorInfoDetailCross SelectEntity(TColorInfoDetailCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TColorInfoDetailCross> ls = null;
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
            return (TColorInfoDetailCross)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TColorInfoDetailCross SelectEntityWithDeletedCheck(TColorInfoDetailCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TColorInfoDetailCross entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TColorInfoDetailCross SelectByPKValue(decimal? colorInfoDetailCrossId) {
            return SelectEntity(BuildPKCB(colorInfoDetailCrossId));
        }

        public virtual TColorInfoDetailCross SelectByPKValueWithDeletedCheck(decimal? colorInfoDetailCrossId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(colorInfoDetailCrossId));
        }

        private TColorInfoDetailCrossCB BuildPKCB(decimal? colorInfoDetailCrossId) {
            AssertObjectNotNull("colorInfoDetailCrossId", colorInfoDetailCrossId);
            TColorInfoDetailCrossCB cb = NewMyConditionBean();
            cb.Query().SetColorInfoDetailCrossId_Equal(colorInfoDetailCrossId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TColorInfoDetailCross> SelectList(TColorInfoDetailCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TColorInfoDetailCross>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TColorInfoDetailCross> SelectPage(TColorInfoDetailCrossCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TColorInfoDetailCross> invoker = new PagingInvoker<TColorInfoDetailCross>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TColorInfoDetailCross> {
            protected TColorInfoDetailCrossBhv _bhv; protected TColorInfoDetailCrossCB _cb;
            public InternalSelectPagingHandler(TColorInfoDetailCrossBhv bhv, TColorInfoDetailCrossCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TColorInfoDetailCross> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TColorInfoDetailCross myEntity = (TColorInfoDetailCross)entity;
            myEntity.ColorInfoDetailCrossId = SelectNextVal();
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
        public IList<TColorSetInfoCross> PulloutTColorSetInfoCross(IList<TColorInfoDetailCross> tColorInfoDetailCrossList) {
            return HelpPulloutInternally<TColorInfoDetailCross, TColorSetInfoCross>(tColorInfoDetailCrossList, new MyInternalPulloutTColorSetInfoCrossCallback());
        }
        protected class MyInternalPulloutTColorSetInfoCrossCallback : InternalPulloutCallback<TColorInfoDetailCross, TColorSetInfoCross> {
            public TColorSetInfoCross getFr(TColorInfoDetailCross entity) { return entity.TColorSetInfoCross; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TColorInfoDetailCross entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TColorInfoDetailCross entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TColorInfoDetailCross entity) {
            HelpInsertOrUpdateInternally<TColorInfoDetailCross, TColorInfoDetailCrossCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TColorInfoDetailCross, TColorInfoDetailCrossCB> {
            protected TColorInfoDetailCrossBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TColorInfoDetailCrossBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TColorInfoDetailCross entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TColorInfoDetailCross entity) { _bhv.Update(entity); }
            public TColorInfoDetailCrossCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TColorInfoDetailCrossCB cb, TColorInfoDetailCross entity) {
                cb.Query().SetColorInfoDetailCrossId_Equal(entity.ColorInfoDetailCrossId);
            }
            public int CallbackSelectCount(TColorInfoDetailCrossCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TColorInfoDetailCross entity) {
            HelpDeleteInternally<TColorInfoDetailCross>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TColorInfoDetailCross> {
            protected TColorInfoDetailCrossBhv _bhv;
            public MyInternalDeleteCallback(TColorInfoDetailCrossBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TColorInfoDetailCross entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TColorInfoDetailCross tColorInfoDetailCross, TColorInfoDetailCrossCB cb) {
            AssertObjectNotNull("tColorInfoDetailCross", tColorInfoDetailCross); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tColorInfoDetailCross);
            FilterEntityOfUpdate(tColorInfoDetailCross); AssertEntityOfUpdate(tColorInfoDetailCross);
            return this.Dao.UpdateByQuery(cb, tColorInfoDetailCross);
        }

        public int QueryDelete(TColorInfoDetailCrossCB cb) {
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
        protected int DelegateSelectCount(TColorInfoDetailCrossCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TColorInfoDetailCross> DelegateSelectList(TColorInfoDetailCrossCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TColorInfoDetailCross e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TColorInfoDetailCross e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TColorInfoDetailCross e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TColorInfoDetailCross Downcast(Entity entity) {
            return (TColorInfoDetailCross)entity;
        }

        protected TColorInfoDetailCrossCB Downcast(ConditionBean cb) {
            return (TColorInfoDetailCrossCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TColorInfoDetailCrossDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
