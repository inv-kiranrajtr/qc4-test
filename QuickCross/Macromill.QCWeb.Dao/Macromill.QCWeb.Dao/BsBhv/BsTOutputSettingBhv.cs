
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
    public partial class TOutputSettingBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputSettingDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputSettingBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_SETTING"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputSettingDbm.GetInstance(); } }
        public TOutputSettingDbm MyDBMeta { get { return TOutputSettingDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputSetting NewMyEntity() { return new TOutputSetting(); }
        public virtual TOutputSettingCB NewMyConditionBean() { return new TOutputSettingCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputSettingCB cb) {
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
        public virtual TOutputSetting SelectEntity(TOutputSettingCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputSetting> ls = null;
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
            return (TOutputSetting)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputSetting SelectEntityWithDeletedCheck(TOutputSettingCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputSetting entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputSetting SelectByPKValue(decimal? qcwebid) {
            return SelectEntity(BuildPKCB(qcwebid));
        }

        public virtual TOutputSetting SelectByPKValueWithDeletedCheck(decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebid));
        }

        private TOutputSettingCB BuildPKCB(decimal? qcwebid) {
            AssertObjectNotNull("qcwebid", qcwebid);
            TOutputSettingCB cb = NewMyConditionBean();
            cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputSetting> SelectList(TOutputSettingCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputSetting>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputSetting> SelectPage(TOutputSettingCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputSetting> invoker = new PagingInvoker<TOutputSetting>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputSetting> {
            protected TOutputSettingBhv _bhv; protected TOutputSettingCB _cb;
            public InternalSelectPagingHandler(TOutputSettingBhv bhv, TOutputSettingCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputSetting> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTOutputHistoryItemList(TOutputSetting tOutputSetting, ConditionBeanSetupper<TOutputHistoryItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputSetting", tOutputSetting); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputHistoryItemList(xnewLRLs<TOutputSetting>(tOutputSetting), conditionBeanSetupper);
        }
        public virtual void LoadTOutputHistoryItemList(IList<TOutputSetting> tOutputSettingList, ConditionBeanSetupper<TOutputHistoryItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputSettingList", tOutputSettingList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputHistoryItemList(tOutputSettingList, new LoadReferrerOption<TOutputHistoryItemCB, TOutputHistoryItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputHistoryItemList(TOutputSetting tOutputSetting, LoadReferrerOption<TOutputHistoryItemCB, TOutputHistoryItem> loadReferrerOption) {
            AssertObjectNotNull("tOutputSetting", tOutputSetting); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputHistoryItemList(xnewLRLs<TOutputSetting>(tOutputSetting), loadReferrerOption);
        }
        public virtual void LoadTOutputHistoryItemList(IList<TOutputSetting> tOutputSettingList, LoadReferrerOption<TOutputHistoryItemCB, TOutputHistoryItem> loadReferrerOption) {
            AssertObjectNotNull("tOutputSettingList", tOutputSettingList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputSettingList.Count == 0) { return; }
            TOutputHistoryItemBhv referrerBhv = xgetBSFLR().Select<TOutputHistoryItemBhv>();
            HelpLoadReferrerInternally<TOutputSetting, decimal?, TOutputHistoryItemCB, TOutputHistoryItem>
                    (tOutputSettingList, loadReferrerOption, new MyInternalLoadTOutputHistoryItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputHistoryItemListCallback : InternalLoadReferrerCallback<TOutputSetting, decimal?, TOutputHistoryItemCB, TOutputHistoryItem> {
            protected TOutputHistoryItemBhv referrerBhv;
            public MyInternalLoadTOutputHistoryItemListCallback(TOutputHistoryItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputSetting e) { return e.Qcwebid; }
            public void setRfLs(TOutputSetting e, IList<TOutputHistoryItem> ls) { e.TOutputHistoryItemList = ls; }
            public TOutputHistoryItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputHistoryItemCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TOutputHistoryItemCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TOutputHistoryItemCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TOutputHistoryItem> selRfLs(TOutputHistoryItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputHistoryItem e) { return e.Qcwebid; }
            public void setlcEt(TOutputHistoryItem re, TOutputSetting be) { re.TOutputSetting = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TOutputSetting> tOutputSettingList) {
            return HelpPulloutInternally<TOutputSetting, TQcwebSurveyInfo>(tOutputSettingList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TOutputSetting, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TOutputSetting entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TOutputHistoryItem> PulloutTOutputHistoryItem(IList<TOutputSetting> tOutputSettingList) {
            return HelpPulloutInternally<TOutputSetting, TOutputHistoryItem>(tOutputSettingList, new MyInternalPulloutTOutputHistoryItemCallback());
        }
        protected class MyInternalPulloutTOutputHistoryItemCallback : InternalPulloutCallback<TOutputSetting, TOutputHistoryItem> {
            public TOutputHistoryItem getFr(TOutputSetting entity) { return entity.TOutputHistoryItem; }
        }
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfoAsOne(IList<TOutputSetting> tOutputSettingList) {
            return HelpPulloutInternally<TOutputSetting, TQcwebSurveyInfo>(tOutputSettingList, new MyInternalPulloutTQcwebSurveyInfoListCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoListCallback : InternalPulloutCallback<TOutputSetting, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TOutputSetting entity) { return entity.TQcwebSurveyInfoAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputSetting entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputSetting entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputSetting entity) {
            HelpInsertOrUpdateInternally<TOutputSetting, TOutputSettingCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputSetting, TOutputSettingCB> {
            protected TOutputSettingBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputSettingBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputSetting entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputSetting entity) { _bhv.Update(entity); }
            public TOutputSettingCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputSettingCB cb, TOutputSetting entity) {
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TOutputSettingCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputSetting entity) {
            HelpDeleteInternally<TOutputSetting>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputSetting> {
            protected TOutputSettingBhv _bhv;
            public MyInternalDeleteCallback(TOutputSettingBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputSetting entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputSetting tOutputSetting, TOutputSettingCB cb) {
            AssertObjectNotNull("tOutputSetting", tOutputSetting); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputSetting);
            FilterEntityOfUpdate(tOutputSetting); AssertEntityOfUpdate(tOutputSetting);
            return this.Dao.UpdateByQuery(cb, tOutputSetting);
        }

        public int QueryDelete(TOutputSettingCB cb) {
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
        protected int DelegateSelectCount(TOutputSettingCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputSetting> DelegateSelectList(TOutputSettingCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TOutputSetting e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputSetting e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputSetting e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputSetting Downcast(Entity entity) {
            return (TOutputSetting)entity;
        }

        protected TOutputSettingCB Downcast(ConditionBean cb) {
            return (TOutputSettingCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputSettingDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
