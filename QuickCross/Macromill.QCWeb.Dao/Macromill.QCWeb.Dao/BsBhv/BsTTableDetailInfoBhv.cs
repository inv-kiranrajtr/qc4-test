
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
    public partial class TTableDetailInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TTableDetailInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TTableDetailInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_TABLE_DETAIL_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TTableDetailInfoDbm.GetInstance(); } }
        public TTableDetailInfoDbm MyDBMeta { get { return TTableDetailInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TTableDetailInfo NewMyEntity() { return new TTableDetailInfo(); }
        public virtual TTableDetailInfoCB NewMyConditionBean() { return new TTableDetailInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TTableDetailInfoCB cb) {
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
        public virtual TTableDetailInfo SelectEntity(TTableDetailInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TTableDetailInfo> ls = null;
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
            return (TTableDetailInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TTableDetailInfo SelectEntityWithDeletedCheck(TTableDetailInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TTableDetailInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TTableDetailInfo SelectByPKValue(decimal? qcwebid, int? tableNo) {
            return SelectEntity(BuildPKCB(qcwebid, tableNo));
        }

        public virtual TTableDetailInfo SelectByPKValueWithDeletedCheck(decimal? qcwebid, int? tableNo) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebid, tableNo));
        }

        private TTableDetailInfoCB BuildPKCB(decimal? qcwebid, int? tableNo) {
            AssertObjectNotNull("qcwebid", qcwebid);AssertObjectNotNull("tableNo", tableNo);
            TTableDetailInfoCB cb = NewMyConditionBean();
            cb.Query().SetQcwebid_Equal(qcwebid);cb.Query().SetTableNo_Equal(tableNo);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TTableDetailInfo> SelectList(TTableDetailInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TTableDetailInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TTableDetailInfo> SelectPage(TTableDetailInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TTableDetailInfo> invoker = new PagingInvoker<TTableDetailInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TTableDetailInfo> {
            protected TTableDetailInfoBhv _bhv; protected TTableDetailInfoCB _cb;
            public InternalSelectPagingHandler(TTableDetailInfoBhv bhv, TTableDetailInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TTableDetailInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion


        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TTableControl> PulloutTTableControl(IList<TTableDetailInfo> tTableDetailInfoList) {
            return HelpPulloutInternally<TTableDetailInfo, TTableControl>(tTableDetailInfoList, new MyInternalPulloutTTableControlCallback());
        }
        protected class MyInternalPulloutTTableControlCallback : InternalPulloutCallback<TTableDetailInfo, TTableControl> {
            public TTableControl getFr(TTableDetailInfo entity) { return entity.TTableControl; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TTableDetailInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TTableDetailInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TTableDetailInfo entity) {
            HelpInsertOrUpdateInternally<TTableDetailInfo, TTableDetailInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TTableDetailInfo, TTableDetailInfoCB> {
            protected TTableDetailInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TTableDetailInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TTableDetailInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TTableDetailInfo entity) { _bhv.Update(entity); }
            public TTableDetailInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TTableDetailInfoCB cb, TTableDetailInfo entity) {
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
                cb.Query().SetTableNo_Equal(entity.TableNo);
            }
            public int CallbackSelectCount(TTableDetailInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TTableDetailInfo entity) {
            HelpDeleteInternally<TTableDetailInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TTableDetailInfo> {
            protected TTableDetailInfoBhv _bhv;
            public MyInternalDeleteCallback(TTableDetailInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TTableDetailInfo entity) { return _bhv.DelegateDelete(entity); }
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
        protected int DelegateSelectCount(TTableDetailInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TTableDetailInfo> DelegateSelectList(TTableDetailInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TTableDetailInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TTableDetailInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TTableDetailInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TTableDetailInfo Downcast(Entity entity) {
            return (TTableDetailInfo)entity;
        }

        protected TTableDetailInfoCB Downcast(ConditionBean cb) {
            return (TTableDetailInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TTableDetailInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
