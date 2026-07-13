
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
    public partial class TSurveyDataBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>1.5.RawData非該当・無回答取得 </summary>
        public static readonly String PATH_abstractSelectCount = "abstractSelectCount";
        /// <summary>RAWDATA作成 </summary>
        public static readonly String PATH_CreateRawdata = "CreateRawdata";
        /// <summary>RAWDATA作成 </summary>
        public static readonly String PATH_CreateRawdataLine = "CreateRawdataLine";
        /// <summary>RawData、FAデータ Delete </summary>
        public static readonly String PATH_Delete = "Delete";
        /// <summary>RawDataのテーブルコピーを行います </summary>
        public static readonly String PATH_InitRawDataTable = "InitRawDataTable";
        /// <summary>RawData、FAデータ Insert </summary>
        public static readonly String PATH_Insert = "Insert";
        /// <summary>RawData、FAデータ InsertOrUpdate </summary>
        public static readonly String PATH_InsertOrUpdate = "InsertOrUpdate";
        /// <summary>RawData指定テーブル、カラムのRAWDATAを取得する </summary>
        public static readonly String PATH_SelectRawData = "SelectRawData";
        /// <summary>RawData指定テーブル、カラムのRAWDATAを取得する </summary>
        public static readonly String PATH_SelectRawDataItemViewFAList = "SelectRawDataItemViewFAList";
        /// <summary>RawData Update </summary>
        public static readonly String PATH_UpdateRawdata = "UpdateRawdata";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TSurveyDataDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TSurveyDataBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SURVEY_DATA"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TSurveyDataDbm.GetInstance(); } }
        public TSurveyDataDbm MyDBMeta { get { return TSurveyDataDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TSurveyData NewMyEntity() { return new TSurveyData(); }
        public virtual TSurveyDataCB NewMyConditionBean() { return new TSurveyDataCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TSurveyDataCB cb) {
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
        public virtual TSurveyData SelectEntity(TSurveyDataCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TSurveyData> ls = null;
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
            return (TSurveyData)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TSurveyData SelectEntityWithDeletedCheck(TSurveyDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TSurveyData entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TSurveyData SelectByPKValue(String sampleId) {
            return SelectEntity(BuildPKCB(sampleId));
        }

        public virtual TSurveyData SelectByPKValueWithDeletedCheck(String sampleId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(sampleId));
        }

        private TSurveyDataCB BuildPKCB(String sampleId) {
            AssertObjectNotNull("sampleId", sampleId);
            TSurveyDataCB cb = NewMyConditionBean();
            cb.Query().SetSampleId_Equal(sampleId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TSurveyData> SelectList(TSurveyDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TSurveyData>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TSurveyData> SelectPage(TSurveyDataCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TSurveyData> invoker = new PagingInvoker<TSurveyData>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TSurveyData> {
            protected TSurveyDataBhv _bhv; protected TSurveyDataCB _cb;
            public InternalSelectPagingHandler(TSurveyDataBhv bhv, TSurveyDataCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TSurveyData> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
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
        public virtual void Insert(TSurveyData entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TSurveyData entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TSurveyData entity) {
            HelpInsertOrUpdateInternally<TSurveyData, TSurveyDataCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TSurveyData, TSurveyDataCB> {
            protected TSurveyDataBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TSurveyDataBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TSurveyData entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TSurveyData entity) { _bhv.Update(entity); }
            public TSurveyDataCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TSurveyDataCB cb, TSurveyData entity) {
                cb.Query().SetSampleId_Equal(entity.SampleId);
            }
            public int CallbackSelectCount(TSurveyDataCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TSurveyData entity) {
            HelpDeleteInternally<TSurveyData>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TSurveyData> {
            protected TSurveyDataBhv _bhv;
            public MyInternalDeleteCallback(TSurveyDataBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TSurveyData entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TSurveyData tSurveyData, TSurveyDataCB cb) {
            AssertObjectNotNull("tSurveyData", tSurveyData); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tSurveyData);
            FilterEntityOfUpdate(tSurveyData); AssertEntityOfUpdate(tSurveyData);
            return this.Dao.UpdateByQuery(cb, tSurveyData);
        }

        public int QueryDelete(TSurveyDataCB cb) {
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
        protected int DelegateSelectCount(TSurveyDataCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TSurveyData> DelegateSelectList(TSurveyDataCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }

        protected int DelegateInsert(TSurveyData e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TSurveyData e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TSurveyData e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TSurveyData Downcast(Entity entity) {
            return (TSurveyData)entity;
        }

        protected TSurveyDataCB Downcast(ConditionBean cb) {
            return (TSurveyDataCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TSurveyDataDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
