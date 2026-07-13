
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
    public partial class TDeleteSampleIdListBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDeleteSampleIdListDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDeleteSampleIdListBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DELETE_SAMPLE_ID_LIST"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDeleteSampleIdListDbm.GetInstance(); } }
        public TDeleteSampleIdListDbm MyDBMeta { get { return TDeleteSampleIdListDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDeleteSampleIdList NewMyEntity() { return new TDeleteSampleIdList(); }
        public virtual TDeleteSampleIdListCB NewMyConditionBean() { return new TDeleteSampleIdListCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDeleteSampleIdListCB cb) {
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
        public virtual TDeleteSampleIdList SelectEntity(TDeleteSampleIdListCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDeleteSampleIdList> ls = null;
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
            return (TDeleteSampleIdList)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDeleteSampleIdList SelectEntityWithDeletedCheck(TDeleteSampleIdListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDeleteSampleIdList entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDeleteSampleIdList SelectByPKValue(decimal? deleteSampleId) {
            return SelectEntity(BuildPKCB(deleteSampleId));
        }

        public virtual TDeleteSampleIdList SelectByPKValueWithDeletedCheck(decimal? deleteSampleId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(deleteSampleId));
        }

        private TDeleteSampleIdListCB BuildPKCB(decimal? deleteSampleId) {
            AssertObjectNotNull("deleteSampleId", deleteSampleId);
            TDeleteSampleIdListCB cb = NewMyConditionBean();
            cb.Query().SetDeleteSampleId_Equal(deleteSampleId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDeleteSampleIdList> SelectList(TDeleteSampleIdListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDeleteSampleIdList>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDeleteSampleIdList> SelectPage(TDeleteSampleIdListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDeleteSampleIdList> invoker = new PagingInvoker<TDeleteSampleIdList>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDeleteSampleIdList> {
            protected TDeleteSampleIdListBhv _bhv; protected TDeleteSampleIdListCB _cb;
            public InternalSelectPagingHandler(TDeleteSampleIdListBhv bhv, TDeleteSampleIdListCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDeleteSampleIdList> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDeleteSampleIdList myEntity = (TDeleteSampleIdList)entity;
            myEntity.DeleteSampleId = SelectNextVal();
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
        public IList<TDeleteData> PulloutTDeleteData(IList<TDeleteSampleIdList> tDeleteSampleIdListList) {
            return HelpPulloutInternally<TDeleteSampleIdList, TDeleteData>(tDeleteSampleIdListList, new MyInternalPulloutTDeleteDataCallback());
        }
        protected class MyInternalPulloutTDeleteDataCallback : InternalPulloutCallback<TDeleteSampleIdList, TDeleteData> {
            public TDeleteData getFr(TDeleteSampleIdList entity) { return entity.TDeleteData; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDeleteSampleIdList entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDeleteSampleIdList entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDeleteSampleIdList entity) {
            HelpInsertOrUpdateInternally<TDeleteSampleIdList, TDeleteSampleIdListCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDeleteSampleIdList, TDeleteSampleIdListCB> {
            protected TDeleteSampleIdListBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDeleteSampleIdListBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDeleteSampleIdList entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDeleteSampleIdList entity) { _bhv.Update(entity); }
            public TDeleteSampleIdListCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDeleteSampleIdListCB cb, TDeleteSampleIdList entity) {
                cb.Query().SetDeleteSampleId_Equal(entity.DeleteSampleId);
            }
            public int CallbackSelectCount(TDeleteSampleIdListCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDeleteSampleIdList entity) {
            HelpDeleteInternally<TDeleteSampleIdList>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDeleteSampleIdList> {
            protected TDeleteSampleIdListBhv _bhv;
            public MyInternalDeleteCallback(TDeleteSampleIdListBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDeleteSampleIdList entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDeleteSampleIdList tDeleteSampleIdList, TDeleteSampleIdListCB cb) {
            AssertObjectNotNull("tDeleteSampleIdList", tDeleteSampleIdList); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDeleteSampleIdList);
            FilterEntityOfUpdate(tDeleteSampleIdList); AssertEntityOfUpdate(tDeleteSampleIdList);
            return this.Dao.UpdateByQuery(cb, tDeleteSampleIdList);
        }

        public int QueryDelete(TDeleteSampleIdListCB cb) {
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
        protected int DelegateSelectCount(TDeleteSampleIdListCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDeleteSampleIdList> DelegateSelectList(TDeleteSampleIdListCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDeleteSampleIdList e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDeleteSampleIdList e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDeleteSampleIdList e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDeleteSampleIdList Downcast(Entity entity) {
            return (TDeleteSampleIdList)entity;
        }

        protected TDeleteSampleIdListCB Downcast(ConditionBean cb) {
            return (TDeleteSampleIdListCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDeleteSampleIdListDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
