
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
    public partial class TRawdataImportQueInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>取込管理情報を本調査ID単位で検索 </summary>
        public static readonly String PATH_SelectGroupByMainSurveyId = "SelectGroupByMainSurveyId";
        /// <summary>取込管理情報を本調査ID、ステータス：取込中で検索 </summary>
        public static readonly String PATH_SelectImportDataUnionAll = "SelectImportDataUnionAll";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TRawdataImportQueInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TRawdataImportQueInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_RAWDATA_IMPORT_QUE_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TRawdataImportQueInfoDbm.GetInstance(); } }
        public TRawdataImportQueInfoDbm MyDBMeta { get { return TRawdataImportQueInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TRawdataImportQueInfo NewMyEntity() { return new TRawdataImportQueInfo(); }
        public virtual TRawdataImportQueInfoCB NewMyConditionBean() { return new TRawdataImportQueInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TRawdataImportQueInfoCB cb) {
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
        public virtual TRawdataImportQueInfo SelectEntity(TRawdataImportQueInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TRawdataImportQueInfo> ls = null;
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
            return (TRawdataImportQueInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TRawdataImportQueInfo SelectEntityWithDeletedCheck(TRawdataImportQueInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TRawdataImportQueInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TRawdataImportQueInfo SelectByPKValue(decimal? rawdataImportQueInfoId) {
            return SelectEntity(BuildPKCB(rawdataImportQueInfoId));
        }

        public virtual TRawdataImportQueInfo SelectByPKValueWithDeletedCheck(decimal? rawdataImportQueInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(rawdataImportQueInfoId));
        }

        private TRawdataImportQueInfoCB BuildPKCB(decimal? rawdataImportQueInfoId) {
            AssertObjectNotNull("rawdataImportQueInfoId", rawdataImportQueInfoId);
            TRawdataImportQueInfoCB cb = NewMyConditionBean();
            cb.Query().SetRawdataImportQueInfoId_Equal(rawdataImportQueInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TRawdataImportQueInfo> SelectList(TRawdataImportQueInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TRawdataImportQueInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TRawdataImportQueInfo> SelectPage(TRawdataImportQueInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TRawdataImportQueInfo> invoker = new PagingInvoker<TRawdataImportQueInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TRawdataImportQueInfo> {
            protected TRawdataImportQueInfoBhv _bhv; protected TRawdataImportQueInfoCB _cb;
            public InternalSelectPagingHandler(TRawdataImportQueInfoBhv bhv, TRawdataImportQueInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TRawdataImportQueInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TRawdataImportQueInfo myEntity = (TRawdataImportQueInfo)entity;
            myEntity.RawdataImportQueInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTQcwebSurveyInfoList(TRawdataImportQueInfo tRawdataImportQueInfo, ConditionBeanSetupper<TQcwebSurveyInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tRawdataImportQueInfo", tRawdataImportQueInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTQcwebSurveyInfoList(xnewLRLs<TRawdataImportQueInfo>(tRawdataImportQueInfo), conditionBeanSetupper);
        }
        public virtual void LoadTQcwebSurveyInfoList(IList<TRawdataImportQueInfo> tRawdataImportQueInfoList, ConditionBeanSetupper<TQcwebSurveyInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tRawdataImportQueInfoList", tRawdataImportQueInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTQcwebSurveyInfoList(tRawdataImportQueInfoList, new LoadReferrerOption<TQcwebSurveyInfoCB, TQcwebSurveyInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTQcwebSurveyInfoList(TRawdataImportQueInfo tRawdataImportQueInfo, LoadReferrerOption<TQcwebSurveyInfoCB, TQcwebSurveyInfo> loadReferrerOption) {
            AssertObjectNotNull("tRawdataImportQueInfo", tRawdataImportQueInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTQcwebSurveyInfoList(xnewLRLs<TRawdataImportQueInfo>(tRawdataImportQueInfo), loadReferrerOption);
        }
        public virtual void LoadTQcwebSurveyInfoList(IList<TRawdataImportQueInfo> tRawdataImportQueInfoList, LoadReferrerOption<TQcwebSurveyInfoCB, TQcwebSurveyInfo> loadReferrerOption) {
            AssertObjectNotNull("tRawdataImportQueInfoList", tRawdataImportQueInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tRawdataImportQueInfoList.Count == 0) { return; }
            TQcwebSurveyInfoBhv referrerBhv = xgetBSFLR().Select<TQcwebSurveyInfoBhv>();
            HelpLoadReferrerInternally<TRawdataImportQueInfo, decimal?, TQcwebSurveyInfoCB, TQcwebSurveyInfo>
                    (tRawdataImportQueInfoList, loadReferrerOption, new MyInternalLoadTQcwebSurveyInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTQcwebSurveyInfoListCallback : InternalLoadReferrerCallback<TRawdataImportQueInfo, decimal?, TQcwebSurveyInfoCB, TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv referrerBhv;
            public MyInternalLoadTQcwebSurveyInfoListCallback(TQcwebSurveyInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TRawdataImportQueInfo e) { return e.RawdataImportQueInfoId; }
            public void setRfLs(TRawdataImportQueInfo e, IList<TQcwebSurveyInfo> ls) { e.TQcwebSurveyInfoList = ls; }
            public TQcwebSurveyInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TQcwebSurveyInfoCB cb, IList<decimal?> ls) { cb.Query().SetRawdataImportQueInfoId_InScope(ls); }
            public void qyOdFKAsc(TQcwebSurveyInfoCB cb) { cb.Query().AddOrderBy_RawdataImportQueInfoId_Asc(); }
            public void spFKCol(TQcwebSurveyInfoCB cb) { cb.Specify().ColumnRawdataImportQueInfoId(); }
            public IList<TQcwebSurveyInfo> selRfLs(TQcwebSurveyInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TQcwebSurveyInfo e) { return e.RawdataImportQueInfoId; }
            public void setlcEt(TQcwebSurveyInfo re, TRawdataImportQueInfo be) { re.TRawdataImportQueInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TRawdataImportQueInfo> tRawdataImportQueInfoList) {
            return HelpPulloutInternally<TRawdataImportQueInfo, TQcwebSurveyInfo>(tRawdataImportQueInfoList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TRawdataImportQueInfo, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TRawdataImportQueInfo entity) { return entity.TQcwebSurveyInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TRawdataImportQueInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TRawdataImportQueInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TRawdataImportQueInfo entity) {
            HelpInsertOrUpdateInternally<TRawdataImportQueInfo, TRawdataImportQueInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TRawdataImportQueInfo, TRawdataImportQueInfoCB> {
            protected TRawdataImportQueInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TRawdataImportQueInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TRawdataImportQueInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TRawdataImportQueInfo entity) { _bhv.Update(entity); }
            public TRawdataImportQueInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TRawdataImportQueInfoCB cb, TRawdataImportQueInfo entity) {
                cb.Query().SetRawdataImportQueInfoId_Equal(entity.RawdataImportQueInfoId);
            }
            public int CallbackSelectCount(TRawdataImportQueInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TRawdataImportQueInfo entity) {
            HelpDeleteInternally<TRawdataImportQueInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TRawdataImportQueInfo> {
            protected TRawdataImportQueInfoBhv _bhv;
            public MyInternalDeleteCallback(TRawdataImportQueInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TRawdataImportQueInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TRawdataImportQueInfo tRawdataImportQueInfo, TRawdataImportQueInfoCB cb) {
            AssertObjectNotNull("tRawdataImportQueInfo", tRawdataImportQueInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tRawdataImportQueInfo);
            FilterEntityOfUpdate(tRawdataImportQueInfo); AssertEntityOfUpdate(tRawdataImportQueInfo);
            return this.Dao.UpdateByQuery(cb, tRawdataImportQueInfo);
        }

        public int QueryDelete(TRawdataImportQueInfoCB cb) {
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
        protected int DelegateSelectCount(TRawdataImportQueInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TRawdataImportQueInfo> DelegateSelectList(TRawdataImportQueInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TRawdataImportQueInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TRawdataImportQueInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TRawdataImportQueInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TRawdataImportQueInfo Downcast(Entity entity) {
            return (TRawdataImportQueInfo)entity;
        }

        protected TRawdataImportQueInfoCB Downcast(ConditionBean cb) {
            return (TRawdataImportQueInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TRawdataImportQueInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
