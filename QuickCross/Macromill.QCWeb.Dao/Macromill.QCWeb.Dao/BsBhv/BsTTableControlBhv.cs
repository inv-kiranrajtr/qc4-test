
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
    public partial class TTableControlBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TTableControlDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TTableControlBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_TABLE_CONTROL"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TTableControlDbm.GetInstance(); } }
        public TTableControlDbm MyDBMeta { get { return TTableControlDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TTableControl NewMyEntity() { return new TTableControl(); }
        public virtual TTableControlCB NewMyConditionBean() { return new TTableControlCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TTableControlCB cb) {
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
        public virtual TTableControl SelectEntity(TTableControlCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TTableControl> ls = null;
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
            return (TTableControl)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TTableControl SelectEntityWithDeletedCheck(TTableControlCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TTableControl entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TTableControl SelectByPKValue(decimal? qcwebid) {
            return SelectEntity(BuildPKCB(qcwebid));
        }

        public virtual TTableControl SelectByPKValueWithDeletedCheck(decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebid));
        }

        private TTableControlCB BuildPKCB(decimal? qcwebid) {
            AssertObjectNotNull("qcwebid", qcwebid);
            TTableControlCB cb = NewMyConditionBean();
            cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TTableControl> SelectList(TTableControlCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TTableControl>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TTableControl> SelectPage(TTableControlCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TTableControl> invoker = new PagingInvoker<TTableControl>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TTableControl> {
            protected TTableControlBhv _bhv; protected TTableControlCB _cb;
            public InternalSelectPagingHandler(TTableControlBhv bhv, TTableControlCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TTableControl> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTTableDetailInfoList(TTableControl tTableControl, ConditionBeanSetupper<TTableDetailInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tTableControl", tTableControl); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTTableDetailInfoList(xnewLRLs<TTableControl>(tTableControl), conditionBeanSetupper);
        }
        public virtual void LoadTTableDetailInfoList(IList<TTableControl> tTableControlList, ConditionBeanSetupper<TTableDetailInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tTableControlList", tTableControlList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTTableDetailInfoList(tTableControlList, new LoadReferrerOption<TTableDetailInfoCB, TTableDetailInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTTableDetailInfoList(TTableControl tTableControl, LoadReferrerOption<TTableDetailInfoCB, TTableDetailInfo> loadReferrerOption) {
            AssertObjectNotNull("tTableControl", tTableControl); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTTableDetailInfoList(xnewLRLs<TTableControl>(tTableControl), loadReferrerOption);
        }
        public virtual void LoadTTableDetailInfoList(IList<TTableControl> tTableControlList, LoadReferrerOption<TTableDetailInfoCB, TTableDetailInfo> loadReferrerOption) {
            AssertObjectNotNull("tTableControlList", tTableControlList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tTableControlList.Count == 0) { return; }
            TTableDetailInfoBhv referrerBhv = xgetBSFLR().Select<TTableDetailInfoBhv>();
            HelpLoadReferrerInternally<TTableControl, decimal?, TTableDetailInfoCB, TTableDetailInfo>
                    (tTableControlList, loadReferrerOption, new MyInternalLoadTTableDetailInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTTableDetailInfoListCallback : InternalLoadReferrerCallback<TTableControl, decimal?, TTableDetailInfoCB, TTableDetailInfo> {
            protected TTableDetailInfoBhv referrerBhv;
            public MyInternalLoadTTableDetailInfoListCallback(TTableDetailInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TTableControl e) { return e.Qcwebid; }
            public void setRfLs(TTableControl e, IList<TTableDetailInfo> ls) { e.TTableDetailInfoList = ls; }
            public TTableDetailInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TTableDetailInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TTableDetailInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TTableDetailInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TTableDetailInfo> selRfLs(TTableDetailInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TTableDetailInfo e) { return e.Qcwebid; }
            public void setlcEt(TTableDetailInfo re, TTableControl be) { re.TTableControl = be; }
        }
        public virtual void LoadTItemInfoList(TTableControl tTableControl, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tTableControl", tTableControl); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(xnewLRLs<TTableControl>(tTableControl), conditionBeanSetupper);
        }
        public virtual void LoadTItemInfoList(IList<TTableControl> tTableControlList, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tTableControlList", tTableControlList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(tTableControlList, new LoadReferrerOption<TItemInfoCB, TItemInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTItemInfoList(TTableControl tTableControl, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tTableControl", tTableControl); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTItemInfoList(xnewLRLs<TTableControl>(tTableControl), loadReferrerOption);
        }
        public virtual void LoadTItemInfoList(IList<TTableControl> tTableControlList, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tTableControlList", tTableControlList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tTableControlList.Count == 0) { return; }
            TItemInfoBhv referrerBhv = xgetBSFLR().Select<TItemInfoBhv>();
            HelpLoadReferrerInternally<TTableControl, decimal?, TItemInfoCB, TItemInfo>
                    (tTableControlList, loadReferrerOption, new MyInternalLoadTItemInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTItemInfoListCallback : InternalLoadReferrerCallback<TTableControl, decimal?, TItemInfoCB, TItemInfo> {
            protected TItemInfoBhv referrerBhv;
            public MyInternalLoadTItemInfoListCallback(TItemInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TTableControl e) { return e.Qcwebid; }
            public void setRfLs(TTableControl e, IList<TItemInfo> ls) { e.TItemInfoList = ls; }
            public TItemInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TItemInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TItemInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TItemInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TItemInfo> selRfLs(TItemInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TItemInfo e) { return e.Qcwebid; }
            public void setlcEt(TItemInfo re, TTableControl be) { re.TTableControl = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfoAsOne(IList<TTableControl> tTableControlList) {
            return HelpPulloutInternally<TTableControl, TQcwebSurveyInfo>(tTableControlList, new MyInternalPulloutTQcwebSurveyInfoListCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoListCallback : InternalPulloutCallback<TTableControl, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TTableControl entity) { return entity.TQcwebSurveyInfoAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TTableControl entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TTableControl entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TTableControl entity) {
            HelpInsertOrUpdateInternally<TTableControl, TTableControlCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TTableControl, TTableControlCB> {
            protected TTableControlBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TTableControlBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TTableControl entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TTableControl entity) { _bhv.Update(entity); }
            public TTableControlCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TTableControlCB cb, TTableControl entity) {
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TTableControlCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TTableControl entity) {
            HelpDeleteInternally<TTableControl>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TTableControl> {
            protected TTableControlBhv _bhv;
            public MyInternalDeleteCallback(TTableControlBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TTableControl entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TTableControl tTableControl, TTableControlCB cb) {
            AssertObjectNotNull("tTableControl", tTableControl); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tTableControl);
            FilterEntityOfUpdate(tTableControl); AssertEntityOfUpdate(tTableControl);
            return this.Dao.UpdateByQuery(cb, tTableControl);
        }

        public int QueryDelete(TTableControlCB cb) {
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
        protected int DelegateSelectCount(TTableControlCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TTableControl> DelegateSelectList(TTableControlCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TTableControl e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TTableControl e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TTableControl e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TTableControl Downcast(Entity entity) {
            return (TTableControl)entity;
        }

        protected TTableControlCB Downcast(ConditionBean cb) {
            return (TTableControlCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TTableControlDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
