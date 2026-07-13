
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
    public partial class TOutputTemplateBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>テンプレートテーブルを検索し、削除対象ファイルを取得する </summary>
        public static readonly String PATH_Select = "Select";
        public static readonly String PATH_SelectCheckOKTemplateList = "SelectCheckOKTemplateList";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputTemplateDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputTemplateBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_TEMPLATE"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputTemplateDbm.GetInstance(); } }
        public TOutputTemplateDbm MyDBMeta { get { return TOutputTemplateDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputTemplate NewMyEntity() { return new TOutputTemplate(); }
        public virtual TOutputTemplateCB NewMyConditionBean() { return new TOutputTemplateCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputTemplateCB cb) {
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
        public virtual TOutputTemplate SelectEntity(TOutputTemplateCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputTemplate> ls = null;
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
            return (TOutputTemplate)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputTemplate SelectEntityWithDeletedCheck(TOutputTemplateCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputTemplate entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputTemplate SelectByPKValue(decimal? outputTemplateId) {
            return SelectEntity(BuildPKCB(outputTemplateId));
        }

        public virtual TOutputTemplate SelectByPKValueWithDeletedCheck(decimal? outputTemplateId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputTemplateId));
        }

        private TOutputTemplateCB BuildPKCB(decimal? outputTemplateId) {
            AssertObjectNotNull("outputTemplateId", outputTemplateId);
            TOutputTemplateCB cb = NewMyConditionBean();
            cb.Query().SetOutputTemplateId_Equal(outputTemplateId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputTemplate> SelectList(TOutputTemplateCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputTemplate>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputTemplate> SelectPage(TOutputTemplateCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputTemplate> invoker = new PagingInvoker<TOutputTemplate>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputTemplate> {
            protected TOutputTemplateBhv _bhv; protected TOutputTemplateCB _cb;
            public InternalSelectPagingHandler(TOutputTemplateBhv bhv, TOutputTemplateCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputTemplate> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputTemplate myEntity = (TOutputTemplate)entity;
            myEntity.OutputTemplateId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTOutputReportsetInfoList(TOutputTemplate tOutputTemplate, ConditionBeanSetupper<TOutputReportsetInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputTemplate", tOutputTemplate); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputReportsetInfoList(xnewLRLs<TOutputTemplate>(tOutputTemplate), conditionBeanSetupper);
        }
        public virtual void LoadTOutputReportsetInfoList(IList<TOutputTemplate> tOutputTemplateList, ConditionBeanSetupper<TOutputReportsetInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputTemplateList", tOutputTemplateList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputReportsetInfoList(tOutputTemplateList, new LoadReferrerOption<TOutputReportsetInfoCB, TOutputReportsetInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputReportsetInfoList(TOutputTemplate tOutputTemplate, LoadReferrerOption<TOutputReportsetInfoCB, TOutputReportsetInfo> loadReferrerOption) {
            AssertObjectNotNull("tOutputTemplate", tOutputTemplate); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputReportsetInfoList(xnewLRLs<TOutputTemplate>(tOutputTemplate), loadReferrerOption);
        }
        public virtual void LoadTOutputReportsetInfoList(IList<TOutputTemplate> tOutputTemplateList, LoadReferrerOption<TOutputReportsetInfoCB, TOutputReportsetInfo> loadReferrerOption) {
            AssertObjectNotNull("tOutputTemplateList", tOutputTemplateList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputTemplateList.Count == 0) { return; }
            TOutputReportsetInfoBhv referrerBhv = xgetBSFLR().Select<TOutputReportsetInfoBhv>();
            HelpLoadReferrerInternally<TOutputTemplate, decimal?, TOutputReportsetInfoCB, TOutputReportsetInfo>
                    (tOutputTemplateList, loadReferrerOption, new MyInternalLoadTOutputReportsetInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputReportsetInfoListCallback : InternalLoadReferrerCallback<TOutputTemplate, decimal?, TOutputReportsetInfoCB, TOutputReportsetInfo> {
            protected TOutputReportsetInfoBhv referrerBhv;
            public MyInternalLoadTOutputReportsetInfoListCallback(TOutputReportsetInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputTemplate e) { return e.OutputTemplateId; }
            public void setRfLs(TOutputTemplate e, IList<TOutputReportsetInfo> ls) { e.TOutputReportsetInfoList = ls; }
            public TOutputReportsetInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputReportsetInfoCB cb, IList<decimal?> ls) { cb.Query().SetOutputTemplateId_InScope(ls); }
            public void qyOdFKAsc(TOutputReportsetInfoCB cb) { cb.Query().AddOrderBy_OutputTemplateId_Asc(); }
            public void spFKCol(TOutputReportsetInfoCB cb) { cb.Specify().ColumnOutputTemplateId(); }
            public IList<TOutputReportsetInfo> selRfLs(TOutputReportsetInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputReportsetInfo e) { return e.OutputTemplateId; }
            public void setlcEt(TOutputReportsetInfo re, TOutputTemplate be) { re.TOutputTemplate = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TOutputTemplateMaster> PulloutTOutputTemplateMaster(IList<TOutputTemplate> tOutputTemplateList) {
            return HelpPulloutInternally<TOutputTemplate, TOutputTemplateMaster>(tOutputTemplateList, new MyInternalPulloutTOutputTemplateMasterCallback());
        }
        protected class MyInternalPulloutTOutputTemplateMasterCallback : InternalPulloutCallback<TOutputTemplate, TOutputTemplateMaster> {
            public TOutputTemplateMaster getFr(TOutputTemplate entity) { return entity.TOutputTemplateMaster; }
        }
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TOutputTemplate> tOutputTemplateList) {
            return HelpPulloutInternally<TOutputTemplate, TQcwebSurveyInfo>(tOutputTemplateList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TOutputTemplate, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TOutputTemplate entity) { return entity.TQcwebSurveyInfo; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TOutputTemplate entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputTemplate entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputTemplate entity) {
            HelpInsertOrUpdateInternally<TOutputTemplate, TOutputTemplateCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputTemplate, TOutputTemplateCB> {
            protected TOutputTemplateBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputTemplateBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputTemplate entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputTemplate entity) { _bhv.Update(entity); }
            public TOutputTemplateCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputTemplateCB cb, TOutputTemplate entity) {
                cb.Query().SetOutputTemplateId_Equal(entity.OutputTemplateId);
            }
            public int CallbackSelectCount(TOutputTemplateCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputTemplate entity) {
            HelpDeleteInternally<TOutputTemplate>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputTemplate> {
            protected TOutputTemplateBhv _bhv;
            public MyInternalDeleteCallback(TOutputTemplateBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputTemplate entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputTemplate tOutputTemplate, TOutputTemplateCB cb) {
            AssertObjectNotNull("tOutputTemplate", tOutputTemplate); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputTemplate);
            FilterEntityOfUpdate(tOutputTemplate); AssertEntityOfUpdate(tOutputTemplate);
            return this.Dao.UpdateByQuery(cb, tOutputTemplate);
        }

        public int QueryDelete(TOutputTemplateCB cb) {
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
        protected int DelegateSelectCount(TOutputTemplateCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputTemplate> DelegateSelectList(TOutputTemplateCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputTemplate e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputTemplate e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputTemplate e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputTemplate Downcast(Entity entity) {
            return (TOutputTemplate)entity;
        }

        protected TOutputTemplateCB Downcast(ConditionBean cb) {
            return (TOutputTemplateCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputTemplateDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
