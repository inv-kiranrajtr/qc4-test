
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
    public partial class TSurveyInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TSurveyInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TSurveyInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SURVEY_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TSurveyInfoDbm.GetInstance(); } }
        public TSurveyInfoDbm MyDBMeta { get { return TSurveyInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TSurveyInfo NewMyEntity() { return new TSurveyInfo(); }
        public virtual TSurveyInfoCB NewMyConditionBean() { return new TSurveyInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TSurveyInfoCB cb) {
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
        public virtual TSurveyInfo SelectEntity(TSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TSurveyInfo> ls = null;
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
            return (TSurveyInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TSurveyInfo SelectEntityWithDeletedCheck(TSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TSurveyInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TSurveyInfo SelectByPKValue(decimal? surveyInfoId) {
            return SelectEntity(BuildPKCB(surveyInfoId));
        }

        public virtual TSurveyInfo SelectByPKValueWithDeletedCheck(decimal? surveyInfoId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(surveyInfoId));
        }

        private TSurveyInfoCB BuildPKCB(decimal? surveyInfoId) {
            AssertObjectNotNull("surveyInfoId", surveyInfoId);
            TSurveyInfoCB cb = NewMyConditionBean();
            cb.Query().SetSurveyInfoId_Equal(surveyInfoId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TSurveyInfo> SelectList(TSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TSurveyInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TSurveyInfo> SelectPage(TSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TSurveyInfo> invoker = new PagingInvoker<TSurveyInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TSurveyInfo> {
            protected TSurveyInfoBhv _bhv; protected TSurveyInfoCB _cb;
            public InternalSelectPagingHandler(TSurveyInfoBhv bhv, TSurveyInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TSurveyInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TSurveyInfo myEntity = (TSurveyInfo)entity;
            myEntity.SurveyInfoId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTQcwebSurveyInfoList(TSurveyInfo tSurveyInfo, ConditionBeanSetupper<TQcwebSurveyInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tSurveyInfo", tSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTQcwebSurveyInfoList(xnewLRLs<TSurveyInfo>(tSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTQcwebSurveyInfoList(IList<TSurveyInfo> tSurveyInfoList, ConditionBeanSetupper<TQcwebSurveyInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tSurveyInfoList", tSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTQcwebSurveyInfoList(tSurveyInfoList, new LoadReferrerOption<TQcwebSurveyInfoCB, TQcwebSurveyInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTQcwebSurveyInfoList(TSurveyInfo tSurveyInfo, LoadReferrerOption<TQcwebSurveyInfoCB, TQcwebSurveyInfo> loadReferrerOption) {
            AssertObjectNotNull("tSurveyInfo", tSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTQcwebSurveyInfoList(xnewLRLs<TSurveyInfo>(tSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTQcwebSurveyInfoList(IList<TSurveyInfo> tSurveyInfoList, LoadReferrerOption<TQcwebSurveyInfoCB, TQcwebSurveyInfo> loadReferrerOption) {
            AssertObjectNotNull("tSurveyInfoList", tSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tSurveyInfoList.Count == 0) { return; }
            TQcwebSurveyInfoBhv referrerBhv = xgetBSFLR().Select<TQcwebSurveyInfoBhv>();
            HelpLoadReferrerInternally<TSurveyInfo, decimal?, TQcwebSurveyInfoCB, TQcwebSurveyInfo>
                    (tSurveyInfoList, loadReferrerOption, new MyInternalLoadTQcwebSurveyInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTQcwebSurveyInfoListCallback : InternalLoadReferrerCallback<TSurveyInfo, decimal?, TQcwebSurveyInfoCB, TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv referrerBhv;
            public MyInternalLoadTQcwebSurveyInfoListCallback(TQcwebSurveyInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TSurveyInfo e) { return e.SurveyInfoId; }
            public void setRfLs(TSurveyInfo e, IList<TQcwebSurveyInfo> ls) { e.TQcwebSurveyInfoList = ls; }
            public TQcwebSurveyInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TQcwebSurveyInfoCB cb, IList<decimal?> ls) { cb.Query().SetSurveyInfoId_InScope(ls); }
            public void qyOdFKAsc(TQcwebSurveyInfoCB cb) { cb.Query().AddOrderBy_SurveyInfoId_Asc(); }
            public void spFKCol(TQcwebSurveyInfoCB cb) { cb.Specify().ColumnSurveyInfoId(); }
            public IList<TQcwebSurveyInfo> selRfLs(TQcwebSurveyInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TQcwebSurveyInfo e) { return e.SurveyInfoId; }
            public void setlcEt(TQcwebSurveyInfo re, TSurveyInfo be) { re.TSurveyInfo = be; }
        }
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
        public virtual void Insert(TSurveyInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TSurveyInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TSurveyInfo entity) {
            HelpInsertOrUpdateInternally<TSurveyInfo, TSurveyInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TSurveyInfo, TSurveyInfoCB> {
            protected TSurveyInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TSurveyInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TSurveyInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TSurveyInfo entity) { _bhv.Update(entity); }
            public TSurveyInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TSurveyInfoCB cb, TSurveyInfo entity) {
                cb.Query().SetSurveyInfoId_Equal(entity.SurveyInfoId);
            }
            public int CallbackSelectCount(TSurveyInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TSurveyInfo entity) {
            HelpDeleteInternally<TSurveyInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TSurveyInfo> {
            protected TSurveyInfoBhv _bhv;
            public MyInternalDeleteCallback(TSurveyInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TSurveyInfo entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TSurveyInfo tSurveyInfo, TSurveyInfoCB cb) {
            AssertObjectNotNull("tSurveyInfo", tSurveyInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tSurveyInfo);
            FilterEntityOfUpdate(tSurveyInfo); AssertEntityOfUpdate(tSurveyInfo);
            return this.Dao.UpdateByQuery(cb, tSurveyInfo);
        }

        public int QueryDelete(TSurveyInfoCB cb) {
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
        protected int DelegateSelectCount(TSurveyInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TSurveyInfo> DelegateSelectList(TSurveyInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TSurveyInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TSurveyInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TSurveyInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TSurveyInfo Downcast(Entity entity) {
            return (TSurveyInfo)entity;
        }

        protected TSurveyInfoCB Downcast(ConditionBean cb) {
            return (TSurveyInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TSurveyInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
