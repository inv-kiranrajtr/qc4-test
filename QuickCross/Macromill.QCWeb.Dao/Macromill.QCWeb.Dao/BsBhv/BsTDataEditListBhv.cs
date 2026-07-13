
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
    public partial class TDataEditListBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>データ加工メイン画面で利用するデータ加工リスト情報を取得する </summary>
        public static readonly String PATH_SelectData = "SelectData";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TDataEditListDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TDataEditListBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_DATA_EDIT_LIST"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TDataEditListDbm.GetInstance(); } }
        public TDataEditListDbm MyDBMeta { get { return TDataEditListDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TDataEditList NewMyEntity() { return new TDataEditList(); }
        public virtual TDataEditListCB NewMyConditionBean() { return new TDataEditListCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TDataEditListCB cb) {
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
        public virtual TDataEditList SelectEntity(TDataEditListCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TDataEditList> ls = null;
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
            return (TDataEditList)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TDataEditList SelectEntityWithDeletedCheck(TDataEditListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TDataEditList entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TDataEditList SelectByPKValue(decimal? dataEditId) {
            return SelectEntity(BuildPKCB(dataEditId));
        }

        public virtual TDataEditList SelectByPKValueWithDeletedCheck(decimal? dataEditId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(dataEditId));
        }

        private TDataEditListCB BuildPKCB(decimal? dataEditId) {
            AssertObjectNotNull("dataEditId", dataEditId);
            TDataEditListCB cb = NewMyConditionBean();
            cb.Query().SetDataEditId_Equal(dataEditId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TDataEditList> SelectList(TDataEditListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TDataEditList>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TDataEditList> SelectPage(TDataEditListCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TDataEditList> invoker = new PagingInvoker<TDataEditList>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TDataEditList> {
            protected TDataEditListBhv _bhv; protected TDataEditListCB _cb;
            public InternalSelectPagingHandler(TDataEditListBhv bhv, TDataEditListCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TDataEditList> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TDataEditList myEntity = (TDataEditList)entity;
            myEntity.DataEditId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTItemInfoList(TDataEditList tDataEditList, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataEditList", tDataEditList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(xnewLRLs<TDataEditList>(tDataEditList), conditionBeanSetupper);
        }
        public virtual void LoadTItemInfoList(IList<TDataEditList> tDataEditListList, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tDataEditListList", tDataEditListList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(tDataEditListList, new LoadReferrerOption<TItemInfoCB, TItemInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTItemInfoList(TDataEditList tDataEditList, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tDataEditList", tDataEditList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTItemInfoList(xnewLRLs<TDataEditList>(tDataEditList), loadReferrerOption);
        }
        public virtual void LoadTItemInfoList(IList<TDataEditList> tDataEditListList, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tDataEditListList", tDataEditListList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tDataEditListList.Count == 0) { return; }
            TItemInfoBhv referrerBhv = xgetBSFLR().Select<TItemInfoBhv>();
            HelpLoadReferrerInternally<TDataEditList, decimal?, TItemInfoCB, TItemInfo>
                    (tDataEditListList, loadReferrerOption, new MyInternalLoadTItemInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTItemInfoListCallback : InternalLoadReferrerCallback<TDataEditList, decimal?, TItemInfoCB, TItemInfo> {
            protected TItemInfoBhv referrerBhv;
            public MyInternalLoadTItemInfoListCallback(TItemInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TDataEditList e) { return e.DataEditId; }
            public void setRfLs(TDataEditList e, IList<TItemInfo> ls) { e.TItemInfoList = ls; }
            public TItemInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TItemInfoCB cb, IList<decimal?> ls) { cb.Query().SetDataEditId_InScope(ls); }
            public void qyOdFKAsc(TItemInfoCB cb) { cb.Query().AddOrderBy_DataEditId_Asc(); }
            public void spFKCol(TItemInfoCB cb) { cb.Specify().ColumnDataEditId(); }
            public IList<TItemInfo> selRfLs(TItemInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TItemInfo e) { return e.DataEditId; }
            public void setlcEt(TItemInfo re, TDataEditList be) { re.TDataEditList = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TDataEditList> tDataEditListList) {
            return HelpPulloutInternally<TDataEditList, TQcwebSurveyInfo>(tDataEditListList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TDataEditList, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TDataEditList entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TEditMenuMaster> PulloutTEditMenuMaster(IList<TDataEditList> tDataEditListList) {
            return HelpPulloutInternally<TDataEditList, TEditMenuMaster>(tDataEditListList, new MyInternalPulloutTEditMenuMasterCallback());
        }
        protected class MyInternalPulloutTEditMenuMasterCallback : InternalPulloutCallback<TDataEditList, TEditMenuMaster> {
            public TEditMenuMaster getFr(TDataEditList entity) { return entity.TEditMenuMaster; }
        }
        public IList<TDataProcessNewItem> PulloutTDataProcessNewItemAsOne(IList<TDataEditList> tDataEditListList) {
            return HelpPulloutInternally<TDataEditList, TDataProcessNewItem>(tDataEditListList, new MyInternalPulloutTDataProcessNewItemListCallback());
        }
        protected class MyInternalPulloutTDataProcessNewItemListCallback : InternalPulloutCallback<TDataEditList, TDataProcessNewItem> {
            public TDataProcessNewItem getFr(TDataEditList entity) { return entity.TDataProcessNewItemAsOne; }
        }
        public IList<TDeleteData> PulloutTDeleteDataAsOne(IList<TDataEditList> tDataEditListList) {
            return HelpPulloutInternally<TDataEditList, TDeleteData>(tDataEditListList, new MyInternalPulloutTDeleteDataListCallback());
        }
        protected class MyInternalPulloutTDeleteDataListCallback : InternalPulloutCallback<TDataEditList, TDeleteData> {
            public TDeleteData getFr(TDataEditList entity) { return entity.TDeleteDataAsOne; }
        }
        public IList<TEditData> PulloutTEditDataAsOne(IList<TDataEditList> tDataEditListList) {
            return HelpPulloutInternally<TDataEditList, TEditData>(tDataEditListList, new MyInternalPulloutTEditDataListCallback());
        }
        protected class MyInternalPulloutTEditDataListCallback : InternalPulloutCallback<TDataEditList, TEditData> {
            public TEditData getFr(TDataEditList entity) { return entity.TEditDataAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TDataEditList entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TDataEditList entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TDataEditList entity) {
            HelpInsertOrUpdateInternally<TDataEditList, TDataEditListCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TDataEditList, TDataEditListCB> {
            protected TDataEditListBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TDataEditListBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TDataEditList entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TDataEditList entity) { _bhv.Update(entity); }
            public TDataEditListCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TDataEditListCB cb, TDataEditList entity) {
                cb.Query().SetDataEditId_Equal(entity.DataEditId);
            }
            public int CallbackSelectCount(TDataEditListCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TDataEditList entity) {
            HelpDeleteInternally<TDataEditList>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TDataEditList> {
            protected TDataEditListBhv _bhv;
            public MyInternalDeleteCallback(TDataEditListBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TDataEditList entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TDataEditList tDataEditList, TDataEditListCB cb) {
            AssertObjectNotNull("tDataEditList", tDataEditList); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tDataEditList);
            FilterEntityOfUpdate(tDataEditList); AssertEntityOfUpdate(tDataEditList);
            return this.Dao.UpdateByQuery(cb, tDataEditList);
        }

        public int QueryDelete(TDataEditListCB cb) {
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
        protected int DelegateSelectCount(TDataEditListCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TDataEditList> DelegateSelectList(TDataEditListCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TDataEditList e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TDataEditList e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TDataEditList e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TDataEditList Downcast(Entity entity) {
            return (TDataEditList)entity;
        }

        protected TDataEditListCB Downcast(ConditionBean cb) {
            return (TDataEditListCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TDataEditListDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
