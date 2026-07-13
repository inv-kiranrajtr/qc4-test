
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
    public partial class TScenarioTotalizationBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>QCWEB管理IDをキーにシナリオ情報のみを取得する（シナリオ区分での絞り込みが可能） </summary>
        public static readonly String PATH_FindScenarioNodesByQCWebID = "FindScenarioNodesByQCWebID";
        /// <summary>シナリオにパラメータのアイテムが使用されているか調査する（データ加工の整合性チェック用） </summary>
        public static readonly String PATH_SelectItemBeingUsed = "SelectItemBeingUsed";
        /// <summary>カテゴリ編集及びGT集計設定追加に追加されたデータ数を取得する </summary>
        public static readonly String PATH_SelectScenarioAppendCount = "SelectScenarioAppendCount";
        /// <summary>シナリオテーブルのウエイトバック設定有無フラグ更新（Loadバッチの途中集計特殊処理用） </summary>
        public static readonly String PATH_UpdateWeightbackFlag = "UpdateWeightbackFlag";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TScenarioTotalizationDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TScenarioTotalizationBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_SCENARIO_TOTALIZATION"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TScenarioTotalizationDbm.GetInstance(); } }
        public TScenarioTotalizationDbm MyDBMeta { get { return TScenarioTotalizationDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TScenarioTotalization NewMyEntity() { return new TScenarioTotalization(); }
        public virtual TScenarioTotalizationCB NewMyConditionBean() { return new TScenarioTotalizationCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TScenarioTotalizationCB cb) {
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
        public virtual TScenarioTotalization SelectEntity(TScenarioTotalizationCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TScenarioTotalization> ls = null;
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
            return (TScenarioTotalization)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TScenarioTotalization SelectEntityWithDeletedCheck(TScenarioTotalizationCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TScenarioTotalization entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TScenarioTotalization SelectByPKValue(decimal? scenarioTotalizationId) {
            return SelectEntity(BuildPKCB(scenarioTotalizationId));
        }

        public virtual TScenarioTotalization SelectByPKValueWithDeletedCheck(decimal? scenarioTotalizationId) {
            return SelectEntityWithDeletedCheck(BuildPKCB(scenarioTotalizationId));
        }

        private TScenarioTotalizationCB BuildPKCB(decimal? scenarioTotalizationId) {
            AssertObjectNotNull("scenarioTotalizationId", scenarioTotalizationId);
            TScenarioTotalizationCB cb = NewMyConditionBean();
            cb.Query().SetScenarioTotalizationId_Equal(scenarioTotalizationId);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TScenarioTotalization> SelectList(TScenarioTotalizationCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TScenarioTotalization>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TScenarioTotalization> SelectPage(TScenarioTotalizationCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TScenarioTotalization> invoker = new PagingInvoker<TScenarioTotalization>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TScenarioTotalization> {
            protected TScenarioTotalizationBhv _bhv; protected TScenarioTotalizationCB _cb;
            public InternalSelectPagingHandler(TScenarioTotalizationBhv bhv, TScenarioTotalizationCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TScenarioTotalization> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TScenarioTotalization myEntity = (TScenarioTotalization)entity;
            myEntity.ScenarioTotalizationId = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTCategoryOutputEditList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TCategoryOutputEditCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCategoryOutputEditList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTCategoryOutputEditList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TCategoryOutputEditCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCategoryOutputEditList(tScenarioTotalizationList, new LoadReferrerOption<TCategoryOutputEditCB, TCategoryOutputEdit>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTCategoryOutputEditList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TCategoryOutputEditCB, TCategoryOutputEdit> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTCategoryOutputEditList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTCategoryOutputEditList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TCategoryOutputEditCB, TCategoryOutputEdit> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TCategoryOutputEditBhv referrerBhv = xgetBSFLR().Select<TCategoryOutputEditBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TCategoryOutputEditCB, TCategoryOutputEdit>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTCategoryOutputEditListCallback(referrerBhv));
        }
        protected class MyInternalLoadTCategoryOutputEditListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TCategoryOutputEditCB, TCategoryOutputEdit> {
            protected TCategoryOutputEditBhv referrerBhv;
            public MyInternalLoadTCategoryOutputEditListCallback(TCategoryOutputEditBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TCategoryOutputEdit> ls) { e.TCategoryOutputEditList = ls; }
            public TCategoryOutputEditCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TCategoryOutputEditCB cb, IList<decimal?> ls) { cb.Query().SetScenarioTotalizationId_InScope(ls); }
            public void qyOdFKAsc(TCategoryOutputEditCB cb) { cb.Query().AddOrderBy_ScenarioTotalizationId_Asc(); }
            public void spFKCol(TCategoryOutputEditCB cb) { cb.Specify().ColumnScenarioTotalizationId(); }
            public IList<TCategoryOutputEdit> selRfLs(TCategoryOutputEditCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TCategoryOutputEdit e) { return e.ScenarioTotalizationId; }
            public void setlcEt(TCategoryOutputEdit re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        public virtual void LoadTCrossScenarioTargetList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TCrossScenarioTargetCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCrossScenarioTargetList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTCrossScenarioTargetList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TCrossScenarioTargetCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTCrossScenarioTargetList(tScenarioTotalizationList, new LoadReferrerOption<TCrossScenarioTargetCB, TCrossScenarioTarget>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTCrossScenarioTargetList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TCrossScenarioTargetCB, TCrossScenarioTarget> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTCrossScenarioTargetList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTCrossScenarioTargetList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TCrossScenarioTargetCB, TCrossScenarioTarget> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TCrossScenarioTargetBhv referrerBhv = xgetBSFLR().Select<TCrossScenarioTargetBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TCrossScenarioTargetCB, TCrossScenarioTarget>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTCrossScenarioTargetListCallback(referrerBhv));
        }
        protected class MyInternalLoadTCrossScenarioTargetListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TCrossScenarioTargetCB, TCrossScenarioTarget> {
            protected TCrossScenarioTargetBhv referrerBhv;
            public MyInternalLoadTCrossScenarioTargetListCallback(TCrossScenarioTargetBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TCrossScenarioTarget> ls) { e.TCrossScenarioTargetList = ls; }
            public TCrossScenarioTargetCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TCrossScenarioTargetCB cb, IList<decimal?> ls) { cb.Query().SetScenarioTotalizationId_InScope(ls); }
            public void qyOdFKAsc(TCrossScenarioTargetCB cb) { cb.Query().AddOrderBy_ScenarioTotalizationId_Asc(); }
            public void spFKCol(TCrossScenarioTargetCB cb) { cb.Specify().ColumnScenarioTotalizationId(); }
            public IList<TCrossScenarioTarget> selRfLs(TCrossScenarioTargetCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TCrossScenarioTarget e) { return e.ScenarioTotalizationId; }
            public void setlcEt(TCrossScenarioTarget re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        public virtual void LoadTFaScenarioHeaderList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TFaScenarioHeaderCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaScenarioHeaderList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTFaScenarioHeaderList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TFaScenarioHeaderCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTFaScenarioHeaderList(tScenarioTotalizationList, new LoadReferrerOption<TFaScenarioHeaderCB, TFaScenarioHeader>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTFaScenarioHeaderList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TFaScenarioHeaderCB, TFaScenarioHeader> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTFaScenarioHeaderList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTFaScenarioHeaderList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TFaScenarioHeaderCB, TFaScenarioHeader> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TFaScenarioHeaderBhv referrerBhv = xgetBSFLR().Select<TFaScenarioHeaderBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TFaScenarioHeaderCB, TFaScenarioHeader>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTFaScenarioHeaderListCallback(referrerBhv));
        }
        protected class MyInternalLoadTFaScenarioHeaderListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TFaScenarioHeaderCB, TFaScenarioHeader> {
            protected TFaScenarioHeaderBhv referrerBhv;
            public MyInternalLoadTFaScenarioHeaderListCallback(TFaScenarioHeaderBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TFaScenarioHeader> ls) { e.TFaScenarioHeaderList = ls; }
            public TFaScenarioHeaderCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TFaScenarioHeaderCB cb, IList<decimal?> ls) { cb.Query().SetScenarioTotalizationId_InScope(ls); }
            public void qyOdFKAsc(TFaScenarioHeaderCB cb) { cb.Query().AddOrderBy_ScenarioTotalizationId_Asc(); }
            public void spFKCol(TFaScenarioHeaderCB cb) { cb.Specify().ColumnScenarioTotalizationId(); }
            public IList<TFaScenarioHeader> selRfLs(TFaScenarioHeaderCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TFaScenarioHeader e) { return e.ScenarioTotalizationId; }
            public void setlcEt(TFaScenarioHeader re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        public virtual void LoadTGtMatrixInfoList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TGtMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtMatrixInfoList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTGtMatrixInfoList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TGtMatrixInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtMatrixInfoList(tScenarioTotalizationList, new LoadReferrerOption<TGtMatrixInfoCB, TGtMatrixInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTGtMatrixInfoList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TGtMatrixInfoCB, TGtMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTGtMatrixInfoList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTGtMatrixInfoList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TGtMatrixInfoCB, TGtMatrixInfo> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TGtMatrixInfoBhv referrerBhv = xgetBSFLR().Select<TGtMatrixInfoBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TGtMatrixInfoCB, TGtMatrixInfo>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTGtMatrixInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTGtMatrixInfoListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TGtMatrixInfoCB, TGtMatrixInfo> {
            protected TGtMatrixInfoBhv referrerBhv;
            public MyInternalLoadTGtMatrixInfoListCallback(TGtMatrixInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TGtMatrixInfo> ls) { e.TGtMatrixInfoList = ls; }
            public TGtMatrixInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TGtMatrixInfoCB cb, IList<decimal?> ls) { cb.Query().SetScenarioTotalizationId_InScope(ls); }
            public void qyOdFKAsc(TGtMatrixInfoCB cb) { cb.Query().AddOrderBy_ScenarioTotalizationId_Asc(); }
            public void spFKCol(TGtMatrixInfoCB cb) { cb.Specify().ColumnScenarioTotalizationId(); }
            public IList<TGtMatrixInfo> selRfLs(TGtMatrixInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TGtMatrixInfo e) { return e.ScenarioTotalizationId; }
            public void setlcEt(TGtMatrixInfo re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        public virtual void LoadTGtScenarioItemList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TGtScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtScenarioItemList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTGtScenarioItemList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TGtScenarioItemCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTGtScenarioItemList(tScenarioTotalizationList, new LoadReferrerOption<TGtScenarioItemCB, TGtScenarioItem>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTGtScenarioItemList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TGtScenarioItemCB, TGtScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTGtScenarioItemList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTGtScenarioItemList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TGtScenarioItemCB, TGtScenarioItem> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TGtScenarioItemBhv referrerBhv = xgetBSFLR().Select<TGtScenarioItemBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TGtScenarioItemCB, TGtScenarioItem>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTGtScenarioItemListCallback(referrerBhv));
        }
        protected class MyInternalLoadTGtScenarioItemListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TGtScenarioItemCB, TGtScenarioItem> {
            protected TGtScenarioItemBhv referrerBhv;
            public MyInternalLoadTGtScenarioItemListCallback(TGtScenarioItemBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TGtScenarioItem> ls) { e.TGtScenarioItemList = ls; }
            public TGtScenarioItemCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TGtScenarioItemCB cb, IList<decimal?> ls) { cb.Query().SetScenarioTotalizationId_InScope(ls); }
            public void qyOdFKAsc(TGtScenarioItemCB cb) { cb.Query().AddOrderBy_ScenarioTotalizationId_Asc(); }
            public void spFKCol(TGtScenarioItemCB cb) { cb.Specify().ColumnScenarioTotalizationId(); }
            public IList<TGtScenarioItem> selRfLs(TGtScenarioItemCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TGtScenarioItem e) { return e.ScenarioTotalizationId; }
            public void setlcEt(TGtScenarioItem re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        public virtual void LoadTScenarioQuerylistList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TScenarioQuerylistCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioQuerylistList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTScenarioQuerylistList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TScenarioQuerylistCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioQuerylistList(tScenarioTotalizationList, new LoadReferrerOption<TScenarioQuerylistCB, TScenarioQuerylist>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTScenarioQuerylistList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TScenarioQuerylistCB, TScenarioQuerylist> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTScenarioQuerylistList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTScenarioQuerylistList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TScenarioQuerylistCB, TScenarioQuerylist> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TScenarioQuerylistBhv referrerBhv = xgetBSFLR().Select<TScenarioQuerylistBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TScenarioQuerylistCB, TScenarioQuerylist>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTScenarioQuerylistListCallback(referrerBhv));
        }
        protected class MyInternalLoadTScenarioQuerylistListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TScenarioQuerylistCB, TScenarioQuerylist> {
            protected TScenarioQuerylistBhv referrerBhv;
            public MyInternalLoadTScenarioQuerylistListCallback(TScenarioQuerylistBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TScenarioQuerylist> ls) { e.TScenarioQuerylistList = ls; }
            public TScenarioQuerylistCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TScenarioQuerylistCB cb, IList<decimal?> ls) { cb.Query().SetScenarioTotalizationId_InScope(ls); }
            public void qyOdFKAsc(TScenarioQuerylistCB cb) { cb.Query().AddOrderBy_ScenarioTotalizationId_Asc(); }
            public void spFKCol(TScenarioQuerylistCB cb) { cb.Specify().ColumnScenarioTotalizationId(); }
            public IList<TScenarioQuerylist> selRfLs(TScenarioQuerylistCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TScenarioQuerylist e) { return e.ScenarioTotalizationId; }
            public void setlcEt(TScenarioQuerylist re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        public virtual void LoadTItemInfoList(TScenarioTotalization tScenarioTotalization, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), conditionBeanSetupper);
        }
        public virtual void LoadTItemInfoList(IList<TScenarioTotalization> tScenarioTotalizationList, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(tScenarioTotalizationList, new LoadReferrerOption<TItemInfoCB, TItemInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTItemInfoList(TScenarioTotalization tScenarioTotalization, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTItemInfoList(xnewLRLs<TScenarioTotalization>(tScenarioTotalization), loadReferrerOption);
        }
        public virtual void LoadTItemInfoList(IList<TScenarioTotalization> tScenarioTotalizationList, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tScenarioTotalizationList", tScenarioTotalizationList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tScenarioTotalizationList.Count == 0) { return; }
            TItemInfoBhv referrerBhv = xgetBSFLR().Select<TItemInfoBhv>();
            HelpLoadReferrerInternally<TScenarioTotalization, decimal?, TItemInfoCB, TItemInfo>
                    (tScenarioTotalizationList, loadReferrerOption, new MyInternalLoadTItemInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTItemInfoListCallback : InternalLoadReferrerCallback<TScenarioTotalization, decimal?, TItemInfoCB, TItemInfo> {
            protected TItemInfoBhv referrerBhv;
            public MyInternalLoadTItemInfoListCallback(TItemInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TScenarioTotalization e) { return e.ScenarioTotalizationId; }
            public void setRfLs(TScenarioTotalization e, IList<TItemInfo> ls) { e.TItemInfoList = ls; }
            public TItemInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TItemInfoCB cb, IList<decimal?> ls) { cb.Query().SetCategoryEditId_InScope(ls); }
            public void qyOdFKAsc(TItemInfoCB cb) { cb.Query().AddOrderBy_CategoryEditId_Asc(); }
            public void spFKCol(TItemInfoCB cb) { cb.Specify().ColumnCategoryEditId(); }
            public IList<TItemInfo> selRfLs(TItemInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TItemInfo e) { return e.CategoryEditId; }
            public void setlcEt(TItemInfo re, TScenarioTotalization be) { re.TScenarioTotalization = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TQcwebSurveyInfo> PulloutTQcwebSurveyInfo(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TQcwebSurveyInfo>(tScenarioTotalizationList, new MyInternalPulloutTQcwebSurveyInfoCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyInfoCallback : InternalPulloutCallback<TScenarioTotalization, TQcwebSurveyInfo> {
            public TQcwebSurveyInfo getFr(TScenarioTotalization entity) { return entity.TQcwebSurveyInfo; }
        }
        public IList<TGtScenarioItem> PulloutTGtScenarioItem(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TGtScenarioItem>(tScenarioTotalizationList, new MyInternalPulloutTGtScenarioItemCallback());
        }
        protected class MyInternalPulloutTGtScenarioItemCallback : InternalPulloutCallback<TScenarioTotalization, TGtScenarioItem> {
            public TGtScenarioItem getFr(TScenarioTotalization entity) { return entity.TGtScenarioItem; }
        }
        public IList<TCrossScenarioTarget> PulloutTCrossScenarioTarget(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TCrossScenarioTarget>(tScenarioTotalizationList, new MyInternalPulloutTCrossScenarioTargetCallback());
        }
        protected class MyInternalPulloutTCrossScenarioTargetCallback : InternalPulloutCallback<TScenarioTotalization, TCrossScenarioTarget> {
            public TCrossScenarioTarget getFr(TScenarioTotalization entity) { return entity.TCrossScenarioTarget; }
        }
        public IList<TFaScenarioHeader> PulloutTFaScenarioHeader(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TFaScenarioHeader>(tScenarioTotalizationList, new MyInternalPulloutTFaScenarioHeaderCallback());
        }
        protected class MyInternalPulloutTFaScenarioHeaderCallback : InternalPulloutCallback<TScenarioTotalization, TFaScenarioHeader> {
            public TFaScenarioHeader getFr(TScenarioTotalization entity) { return entity.TFaScenarioHeader; }
        }
        public IList<TScenarioQuerylist> PulloutTScenarioQuerylist(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TScenarioQuerylist>(tScenarioTotalizationList, new MyInternalPulloutTScenarioQuerylistCallback());
        }
        protected class MyInternalPulloutTScenarioQuerylistCallback : InternalPulloutCallback<TScenarioTotalization, TScenarioQuerylist> {
            public TScenarioQuerylist getFr(TScenarioTotalization entity) { return entity.TScenarioQuerylist; }
        }
        public IList<TCategoryOutputEdit> PulloutTCategoryOutputEdit(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TCategoryOutputEdit>(tScenarioTotalizationList, new MyInternalPulloutTCategoryOutputEditCallback());
        }
        protected class MyInternalPulloutTCategoryOutputEditCallback : InternalPulloutCallback<TScenarioTotalization, TCategoryOutputEdit> {
            public TCategoryOutputEdit getFr(TScenarioTotalization entity) { return entity.TCategoryOutputEdit; }
        }
        public IList<TGtMatrixInfo> PulloutTGtMatrixInfo(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TGtMatrixInfo>(tScenarioTotalizationList, new MyInternalPulloutTGtMatrixInfoCallback());
        }
        protected class MyInternalPulloutTGtMatrixInfoCallback : InternalPulloutCallback<TScenarioTotalization, TGtMatrixInfo> {
            public TGtMatrixInfo getFr(TScenarioTotalization entity) { return entity.TGtMatrixInfo; }
        }
        public IList<TDefaultEnv> PulloutTDefaultEnv(IList<TScenarioTotalization> tScenarioTotalizationList) {
            return HelpPulloutInternally<TScenarioTotalization, TDefaultEnv>(tScenarioTotalizationList, new MyInternalPulloutTDefaultEnvCallback());
        }
        protected class MyInternalPulloutTDefaultEnvCallback : InternalPulloutCallback<TScenarioTotalization, TDefaultEnv> {
            public TDefaultEnv getFr(TScenarioTotalization entity) { return entity.TDefaultEnv; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TScenarioTotalization entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TScenarioTotalization entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public void InsertOrUpdate(TScenarioTotalization entity) {
            HelpInsertOrUpdateInternally<TScenarioTotalization, TScenarioTotalizationCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TScenarioTotalization, TScenarioTotalizationCB> {
            protected TScenarioTotalizationBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TScenarioTotalizationBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TScenarioTotalization entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TScenarioTotalization entity) { _bhv.Update(entity); }
            public TScenarioTotalizationCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TScenarioTotalizationCB cb, TScenarioTotalization entity) {
                cb.Query().SetScenarioTotalizationId_Equal(entity.ScenarioTotalizationId);
            }
            public int CallbackSelectCount(TScenarioTotalizationCB cb) { return _bhv.SelectCount(cb); }
        }

        public virtual void Delete(TScenarioTotalization entity) {
            HelpDeleteInternally<TScenarioTotalization>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TScenarioTotalization> {
            protected TScenarioTotalizationBhv _bhv;
            public MyInternalDeleteCallback(TScenarioTotalizationBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TScenarioTotalization entity) { return _bhv.DelegateDelete(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TScenarioTotalization tScenarioTotalization, TScenarioTotalizationCB cb) {
            AssertObjectNotNull("tScenarioTotalization", tScenarioTotalization); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tScenarioTotalization);
            FilterEntityOfUpdate(tScenarioTotalization); AssertEntityOfUpdate(tScenarioTotalization);
            return this.Dao.UpdateByQuery(cb, tScenarioTotalization);
        }

        public int QueryDelete(TScenarioTotalizationCB cb) {
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
        protected int DelegateSelectCount(TScenarioTotalizationCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TScenarioTotalization> DelegateSelectList(TScenarioTotalizationCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TScenarioTotalization e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TScenarioTotalization e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TScenarioTotalization e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TScenarioTotalization Downcast(Entity entity) {
            return (TScenarioTotalization)entity;
        }

        protected TScenarioTotalizationCB Downcast(ConditionBean cb) {
            return (TScenarioTotalizationCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TScenarioTotalizationDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
