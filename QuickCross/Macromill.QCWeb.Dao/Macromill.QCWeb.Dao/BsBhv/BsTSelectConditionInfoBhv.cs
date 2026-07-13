
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
    public partial class TSelectConditionInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>セレクト条件情報 Delete </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TSelectConditionInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TSelectConditionInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SELECT_CONDITION_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TSelectConditionInfoDbm.GetInstance(); } }
        public TSelectConditionInfoDbm MyDBMeta { get { return TSelectConditionInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TSelectConditionInfo NewMyEntity() { return new TSelectConditionInfo(); }
        public virtual TSelectConditionInfoCB NewMyConditionBean() { return new TSelectConditionInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TSelectConditionInfoCB cb) {
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
        public virtual TSelectConditionInfo SelectEntity(TSelectConditionInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TSelectConditionInfo> ls = null;
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
            return (TSelectConditionInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TSelectConditionInfo SelectEntityWithDeletedCheck(TSelectConditionInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TSelectConditionInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TSelectConditionInfo SelectByPKValue(long? selectNo, decimal? qcwebid) {
            return SelectEntity(BuildPKCB(selectNo, qcwebid));
        }

        public virtual TSelectConditionInfo SelectByPKValueWithDeletedCheck(long? selectNo, decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(selectNo, qcwebid));
        }

        private TSelectConditionInfoCB BuildPKCB(long? selectNo, decimal? qcwebid) {
            AssertObjectNotNull("selectNo", selectNo);AssertObjectNotNull("qcwebid", qcwebid);
            TSelectConditionInfoCB cb = NewMyConditionBean();
            cb.Query().SetSelectNo_Equal(selectNo);cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TSelectConditionInfo> SelectList(TSelectConditionInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TSelectConditionInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TSelectConditionInfo> SelectPage(TSelectConditionInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TSelectConditionInfo> invoker = new PagingInvoker<TSelectConditionInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TSelectConditionInfo> {
            protected TSelectConditionInfoBhv _bhv; protected TSelectConditionInfoCB _cb;
            public InternalSelectPagingHandler(TSelectConditionInfoBhv bhv, TSelectConditionInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TSelectConditionInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion


        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TSelectConditionInfo> tSelectConditionInfoList) {
            return HelpPulloutInternally<TSelectConditionInfo, TQcwebSurveyInfo>(tSelectConditionInfoList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TSelectConditionInfo, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TSelectConditionInfo entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfoAsOne(IList<TSelectConditionInfo> tSelectConditionInfoList) {
            return HelpPulloutInternally<TSelectConditionInfo, TQcwebSurveyInfo>(tSelectConditionInfoList, new MyInternalPulloutTQcwebSurveyInfoListCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoListCallback : InternalPulloutCallback<TSelectConditionInfo, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TSelectConditionInfo entity) { return entity.TQcwebSurveyInfoAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TSelectConditionInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TSelectConditionInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TSelectConditionInfo entity) {
            HelpInsertOrUpdateInternally<TSelectConditionInfo, TSelectConditionInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TSelectConditionInfo, TSelectConditionInfoCB> {
            protected TSelectConditionInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TSelectConditionInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TSelectConditionInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TSelectConditionInfo entity) { _bhv.Update(entity); }
            public TSelectConditionInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TSelectConditionInfoCB cb, TSelectConditionInfo entity) {
                cb.Query().SetSelectNo_Equal(entity.SelectNo);
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TSelectConditionInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TSelectConditionInfo entity) {
            HelpDeleteInternally<TSelectConditionInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TSelectConditionInfo> {
            protected TSelectConditionInfoBhv _bhv;
            public MyInternalDeleteCallback(TSelectConditionInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TSelectConditionInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============

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
        protected int DelegateSelectCount(TSelectConditionInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TSelectConditionInfo> DelegateSelectList(TSelectConditionInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TSelectConditionInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TSelectConditionInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TSelectConditionInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TSelectConditionInfo Downcast(Entity entity) {
            return (TSelectConditionInfo)entity;
        }

        protected TSelectConditionInfoCB Downcast(ConditionBean cb) {
            return (TSelectConditionInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TSelectConditionInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
