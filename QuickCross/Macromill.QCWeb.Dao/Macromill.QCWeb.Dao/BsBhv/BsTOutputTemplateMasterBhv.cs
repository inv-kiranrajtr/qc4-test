
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
    public partial class TOutputTemplateMasterBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>PP テンプレートマスタテーブルの削除 </summary>
        public static readonly String PATH_Delete = "Delete";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TOutputTemplateMasterDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TOutputTemplateMasterBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_OUTPUT_TEMPLATE_MASTER"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TOutputTemplateMasterDbm.GetInstance(); } }
        public TOutputTemplateMasterDbm MyDBMeta { get { return TOutputTemplateMasterDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TOutputTemplateMaster NewMyEntity() { return new TOutputTemplateMaster(); }
        public virtual TOutputTemplateMasterCB NewMyConditionBean() { return new TOutputTemplateMasterCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TOutputTemplateMasterCB cb) {
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
        public virtual TOutputTemplateMaster SelectEntity(TOutputTemplateMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TOutputTemplateMaster> ls = null;
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
            return (TOutputTemplateMaster)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TOutputTemplateMaster SelectEntityWithDeletedCheck(TOutputTemplateMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TOutputTemplateMaster entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TOutputTemplateMaster SelectByPKValue(decimal? outputTemplateMasterId) {
            return SelectEntity(BuildPKCB(outputTemplateMasterId));
        }

        public virtual TOutputTemplateMaster SelectByPKValueWithDeletedCheck(decimal? outputTemplateMasterId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(outputTemplateMasterId));
        }

        private TOutputTemplateMasterCB BuildPKCB(decimal? outputTemplateMasterId) {
            AssertObjectNotNull("outputTemplateMasterId", outputTemplateMasterId);
            TOutputTemplateMasterCB cb = NewMyConditionBean();
            cb.Query().SetOutputTemplateMasterId_Equal(outputTemplateMasterId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TOutputTemplateMaster> SelectList(TOutputTemplateMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TOutputTemplateMaster>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TOutputTemplateMaster> SelectPage(TOutputTemplateMasterCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TOutputTemplateMaster> invoker = new PagingInvoker<TOutputTemplateMaster>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TOutputTemplateMaster> {
            protected TOutputTemplateMasterBhv _bhv; protected TOutputTemplateMasterCB _cb;
            public InternalSelectPagingHandler(TOutputTemplateMasterBhv bhv, TOutputTemplateMasterCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TOutputTemplateMaster> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TOutputTemplateMaster myEntity = (TOutputTemplateMaster)entity;
            myEntity.OutputTemplateMasterId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTOutputTemplateList(TOutputTemplateMaster tOutputTemplateMaster, ConditionBeanSetupper<TOutputTemplateCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputTemplateMaster", tOutputTemplateMaster); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputTemplateList(xnewLRLs<TOutputTemplateMaster>(tOutputTemplateMaster), conditionBeanSetupper);
        }
        public virtual void LoadTOutputTemplateList(IList<TOutputTemplateMaster> tOutputTemplateMasterList, ConditionBeanSetupper<TOutputTemplateCB> conditionBeanSetupper) {
            AssertObjectNotNull("tOutputTemplateMasterList", tOutputTemplateMasterList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputTemplateList(tOutputTemplateMasterList, new LoadReferrerOption<TOutputTemplateCB, TOutputTemplate>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputTemplateList(TOutputTemplateMaster tOutputTemplateMaster, LoadReferrerOption<TOutputTemplateCB, TOutputTemplate> loadReferrerOption) {
            AssertObjectNotNull("tOutputTemplateMaster", tOutputTemplateMaster); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputTemplateList(xnewLRLs<TOutputTemplateMaster>(tOutputTemplateMaster), loadReferrerOption);
        }
        public virtual void LoadTOutputTemplateList(IList<TOutputTemplateMaster> tOutputTemplateMasterList, LoadReferrerOption<TOutputTemplateCB, TOutputTemplate> loadReferrerOption) {
            AssertObjectNotNull("tOutputTemplateMasterList", tOutputTemplateMasterList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tOutputTemplateMasterList.Count == 0) { return; }
            TOutputTemplateBhv referrerBhv = xgetBSFLR().Select<TOutputTemplateBhv>();
            HelpLoadReferrerInternally<TOutputTemplateMaster, decimal?, TOutputTemplateCB, TOutputTemplate>
                    (tOutputTemplateMasterList, loadReferrerOption, new MyInternalLoadTOutputTemplateListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputTemplateListCallback : InternalLoadReferrerCallback<TOutputTemplateMaster, decimal?, TOutputTemplateCB, TOutputTemplate> {
            protected TOutputTemplateBhv referrerBhv;
            public MyInternalLoadTOutputTemplateListCallback(TOutputTemplateBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TOutputTemplateMaster e) { return e.OutputTemplateMasterId; }
            public void setRfLs(TOutputTemplateMaster e, IList<TOutputTemplate> ls) { e.TOutputTemplateList = ls; }
            public TOutputTemplateCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputTemplateCB cb, IList<decimal?> ls) { cb.Query().SetOutputTemplateMasterId_InScope(ls); }
            public void qyOdFKAsc(TOutputTemplateCB cb) { cb.Query().AddOrderBy_OutputTemplateMasterId_Asc(); }
            public void spFKCol(TOutputTemplateCB cb) { cb.Specify().ColumnOutputTemplateMasterId(); }
            public IList<TOutputTemplate> selRfLs(TOutputTemplateCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputTemplate e) { return e.OutputTemplateMasterId; }
            public void setlcEt(TOutputTemplate re, TOutputTemplateMaster be) { re.TOutputTemplateMaster = be; }
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
        public virtual void Insert(TOutputTemplateMaster entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TOutputTemplateMaster entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TOutputTemplateMaster entity) {
            HelpInsertOrUpdateInternally<TOutputTemplateMaster, TOutputTemplateMasterCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TOutputTemplateMaster, TOutputTemplateMasterCB> {
            protected TOutputTemplateMasterBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TOutputTemplateMasterBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TOutputTemplateMaster entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TOutputTemplateMaster entity) { _bhv.Update(entity); }
            public TOutputTemplateMasterCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TOutputTemplateMasterCB cb, TOutputTemplateMaster entity) {
                cb.Query().SetOutputTemplateMasterId_Equal(entity.OutputTemplateMasterId);
            }
            public int CallbackSelectCount(TOutputTemplateMasterCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TOutputTemplateMaster entity) {
            HelpDeleteInternally<TOutputTemplateMaster>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TOutputTemplateMaster> {
            protected TOutputTemplateMasterBhv _bhv;
            public MyInternalDeleteCallback(TOutputTemplateMasterBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TOutputTemplateMaster entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TOutputTemplateMaster tOutputTemplateMaster, TOutputTemplateMasterCB cb) {
            AssertObjectNotNull("tOutputTemplateMaster", tOutputTemplateMaster); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tOutputTemplateMaster);
            FilterEntityOfUpdate(tOutputTemplateMaster); AssertEntityOfUpdate(tOutputTemplateMaster);
            return this.Dao.UpdateByQuery(cb, tOutputTemplateMaster);
        }

        public int QueryDelete(TOutputTemplateMasterCB cb) {
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
        protected int DelegateSelectCount(TOutputTemplateMasterCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TOutputTemplateMaster> DelegateSelectList(TOutputTemplateMasterCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TOutputTemplateMaster e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TOutputTemplateMaster e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TOutputTemplateMaster e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TOutputTemplateMaster Downcast(Entity entity) {
            return (TOutputTemplateMaster)entity;
        }

        protected TOutputTemplateMasterCB Downcast(ConditionBean cb) {
            return (TOutputTemplateMasterCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TOutputTemplateMasterDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
