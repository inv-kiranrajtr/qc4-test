
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
    public partial class TRawdataDeleteQueBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>物理削除対象を検索 </summary>
        public static readonly String PATH_selectPhysicalDelete = "selectPhysicalDelete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TRawdataDeleteQueDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TRawdataDeleteQueBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_RAWDATA_DELETE_QUE"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TRawdataDeleteQueDbm.GetInstance(); } }
        public TRawdataDeleteQueDbm MyDBMeta { get { return TRawdataDeleteQueDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TRawdataDeleteQue NewMyEntity() { return new TRawdataDeleteQue(); }
        public virtual TRawdataDeleteQueCB NewMyConditionBean() { return new TRawdataDeleteQueCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TRawdataDeleteQueCB cb) {
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
        public virtual TRawdataDeleteQue SelectEntity(TRawdataDeleteQueCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TRawdataDeleteQue> ls = null;
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
            return (TRawdataDeleteQue)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TRawdataDeleteQue SelectEntityWithDeletedCheck(TRawdataDeleteQueCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TRawdataDeleteQue entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TRawdataDeleteQue SelectByPKValue(decimal? rawdataDeleteQueId) {
            return SelectEntity(BuildPKCB(rawdataDeleteQueId));
        }

        public virtual TRawdataDeleteQue SelectByPKValueWithDeletedCheck(decimal? rawdataDeleteQueId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(rawdataDeleteQueId));
        }

        private TRawdataDeleteQueCB BuildPKCB(decimal? rawdataDeleteQueId) {
            AssertObjectNotNull("rawdataDeleteQueId", rawdataDeleteQueId);
            TRawdataDeleteQueCB cb = NewMyConditionBean();
            cb.Query().SetRawdataDeleteQueId_Equal(rawdataDeleteQueId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TRawdataDeleteQue> SelectList(TRawdataDeleteQueCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TRawdataDeleteQue>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TRawdataDeleteQue> SelectPage(TRawdataDeleteQueCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TRawdataDeleteQue> invoker = new PagingInvoker<TRawdataDeleteQue>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TRawdataDeleteQue> {
            protected TRawdataDeleteQueBhv _bhv; protected TRawdataDeleteQueCB _cb;
            public InternalSelectPagingHandler(TRawdataDeleteQueBhv bhv, TRawdataDeleteQueCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TRawdataDeleteQue> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TRawdataDeleteQue myEntity = (TRawdataDeleteQue)entity;
            myEntity.RawdataDeleteQueId = SelectNextVal();
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
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TRawdataDeleteQue entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TRawdataDeleteQue entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TRawdataDeleteQue entity) {
            HelpInsertOrUpdateInternally<TRawdataDeleteQue, TRawdataDeleteQueCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TRawdataDeleteQue, TRawdataDeleteQueCB> {
            protected TRawdataDeleteQueBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TRawdataDeleteQueBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TRawdataDeleteQue entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TRawdataDeleteQue entity) { _bhv.Update(entity); }
            public TRawdataDeleteQueCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TRawdataDeleteQueCB cb, TRawdataDeleteQue entity) {
                cb.Query().SetRawdataDeleteQueId_Equal(entity.RawdataDeleteQueId);
            }
            public int CallbackSelectCount(TRawdataDeleteQueCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TRawdataDeleteQue entity) {
            HelpDeleteInternally<TRawdataDeleteQue>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TRawdataDeleteQue> {
            protected TRawdataDeleteQueBhv _bhv;
            public MyInternalDeleteCallback(TRawdataDeleteQueBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TRawdataDeleteQue entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TRawdataDeleteQue tRawdataDeleteQue, TRawdataDeleteQueCB cb) {
            AssertObjectNotNull("tRawdataDeleteQue", tRawdataDeleteQue); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tRawdataDeleteQue);
            FilterEntityOfUpdate(tRawdataDeleteQue); AssertEntityOfUpdate(tRawdataDeleteQue);
            return this.Dao.UpdateByQuery(cb, tRawdataDeleteQue);
        }

        public int QueryDelete(TRawdataDeleteQueCB cb) {
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
        protected int DelegateSelectCount(TRawdataDeleteQueCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TRawdataDeleteQue> DelegateSelectList(TRawdataDeleteQueCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TRawdataDeleteQue e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TRawdataDeleteQue e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TRawdataDeleteQue e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TRawdataDeleteQue Downcast(Entity entity) {
            return (TRawdataDeleteQue)entity;
        }

        protected TRawdataDeleteQueCB Downcast(ConditionBean cb) {
            return (TRawdataDeleteQueCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TRawdataDeleteQueDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
