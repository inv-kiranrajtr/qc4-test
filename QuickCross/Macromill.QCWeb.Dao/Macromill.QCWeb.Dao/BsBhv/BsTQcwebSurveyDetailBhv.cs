
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
    public partial class TQcwebSurveyDetailBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TQcwebSurveyDetailDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TQcwebSurveyDetailBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_QCWEB_SURVEY_DETAIL"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TQcwebSurveyDetailDbm.GetInstance(); } }
        public TQcwebSurveyDetailDbm MyDBMeta { get { return TQcwebSurveyDetailDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TQcwebSurveyDetail NewMyEntity() { return new TQcwebSurveyDetail(); }
        public virtual TQcwebSurveyDetailCB NewMyConditionBean() { return new TQcwebSurveyDetailCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TQcwebSurveyDetailCB cb) {
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
        public virtual TQcwebSurveyDetail SelectEntity(TQcwebSurveyDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TQcwebSurveyDetail> ls = null;
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
            return (TQcwebSurveyDetail)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TQcwebSurveyDetail SelectEntityWithDeletedCheck(TQcwebSurveyDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TQcwebSurveyDetail entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TQcwebSurveyDetail SelectByPKValue(decimal? qcwebDetailId) {
            return SelectEntity(BuildPKCB(qcwebDetailId));
        }

        public virtual TQcwebSurveyDetail SelectByPKValueWithDeletedCheck(decimal? qcwebDetailId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebDetailId));
        }

        private TQcwebSurveyDetailCB BuildPKCB(decimal? qcwebDetailId) {
            AssertObjectNotNull("qcwebDetailId", qcwebDetailId);
            TQcwebSurveyDetailCB cb = NewMyConditionBean();
            cb.Query().SetQcwebDetailId_Equal(qcwebDetailId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TQcwebSurveyDetail> SelectList(TQcwebSurveyDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TQcwebSurveyDetail>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TQcwebSurveyDetail> SelectPage(TQcwebSurveyDetailCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TQcwebSurveyDetail> invoker = new PagingInvoker<TQcwebSurveyDetail>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TQcwebSurveyDetail> {
            protected TQcwebSurveyDetailBhv _bhv; protected TQcwebSurveyDetailCB _cb;
            public InternalSelectPagingHandler(TQcwebSurveyDetailBhv bhv, TQcwebSurveyDetailCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TQcwebSurveyDetail> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TQcwebSurveyDetail myEntity = (TQcwebSurveyDetail)entity;
            myEntity.QcwebDetailId = SelectNextVal();
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
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TQcwebSurveyDetail> tQcwebSurveyDetailList) {
            return HelpPulloutInternally<TQcwebSurveyDetail, TQcwebSurveyInfo>(tQcwebSurveyDetailList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TQcwebSurveyDetail, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TQcwebSurveyDetail entity) { return entity.TQcwebSurveyInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TQcwebSurveyDetail entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TQcwebSurveyDetail entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TQcwebSurveyDetail entity) {
            HelpInsertOrUpdateInternally<TQcwebSurveyDetail, TQcwebSurveyDetailCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TQcwebSurveyDetail, TQcwebSurveyDetailCB> {
            protected TQcwebSurveyDetailBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TQcwebSurveyDetailBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TQcwebSurveyDetail entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TQcwebSurveyDetail entity) { _bhv.Update(entity); }
            public TQcwebSurveyDetailCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TQcwebSurveyDetailCB cb, TQcwebSurveyDetail entity) {
                cb.Query().SetQcwebDetailId_Equal(entity.QcwebDetailId);
            }
            public int CallbackSelectCount(TQcwebSurveyDetailCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TQcwebSurveyDetail entity) {
            HelpDeleteInternally<TQcwebSurveyDetail>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TQcwebSurveyDetail> {
            protected TQcwebSurveyDetailBhv _bhv;
            public MyInternalDeleteCallback(TQcwebSurveyDetailBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TQcwebSurveyDetail entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TQcwebSurveyDetail tQcwebSurveyDetail, TQcwebSurveyDetailCB cb) {
            AssertObjectNotNull("tQcwebSurveyDetail", tQcwebSurveyDetail); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tQcwebSurveyDetail);
            FilterEntityOfUpdate(tQcwebSurveyDetail); AssertEntityOfUpdate(tQcwebSurveyDetail);
            return this.Dao.UpdateByQuery(cb, tQcwebSurveyDetail);
        }

        public int QueryDelete(TQcwebSurveyDetailCB cb) {
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
        protected int DelegateSelectCount(TQcwebSurveyDetailCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TQcwebSurveyDetail> DelegateSelectList(TQcwebSurveyDetailCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TQcwebSurveyDetail e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TQcwebSurveyDetail e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TQcwebSurveyDetail e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TQcwebSurveyDetail Downcast(Entity entity) {
            return (TQcwebSurveyDetail)entity;
        }

        protected TQcwebSurveyDetailCB Downcast(ConditionBean cb) {
            return (TQcwebSurveyDetailCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TQcwebSurveyDetailDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
