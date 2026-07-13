
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
    public partial class TQcwebSurveyInfoBhv : Macromill.QCWeb.Dao.AllCommon.Bhv.AbstractBehaviorWritable {

        // ===============================================================================
        //                                                                      Definition
        //                                                                      ==========
        /*df:beginQueryPath*/
        /// <summary>QCWeb調査管理の追加データ管理番号の採番 </summary>
        public static readonly String PATH_selectAddDataNoNextVal = "selectAddDataNoNextVal";
        /*df:endQueryPath*/

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TQcwebSurveyInfoDao _dao;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TQcwebSurveyInfoBhv() {
        }
        
        // ===============================================================================
        //                                                                Initialized Mark
        //                                                                ================
        public override bool IsInitialized { get { return _dao != null; } }

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_QCWEB_SURVEY_INFO"; } }

        // ===============================================================================
        //                                                                          DBMeta
        //                                                                          ======
        public override DBMeta DBMeta { get { return TQcwebSurveyInfoDbm.GetInstance(); } }
        public TQcwebSurveyInfoDbm MyDBMeta { get { return TQcwebSurveyInfoDbm.GetInstance(); } }

        // ===============================================================================
        //                                                                    New Instance
        //                                                                    ============
        #region New Instance
        public override Entity NewEntity() { return NewMyEntity(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public virtual TQcwebSurveyInfo NewMyEntity() { return new TQcwebSurveyInfo(); }
        public virtual TQcwebSurveyInfoCB NewMyConditionBean() { return new TQcwebSurveyInfoCB(); }
        #endregion

        // ===============================================================================
        //                                                                    Count Select
        //                                                                    ============
        #region Count Select
        public virtual int SelectCount(TQcwebSurveyInfoCB cb) {
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
        public virtual TQcwebSurveyInfo SelectEntity(TQcwebSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            if (!cb.HasWhereClause() && cb.FetchSize != 1) { // if no condition for one
                throwSelectEntityConditionNotFoundException(cb);
            }
            int preSafetyMaxResultSize = xcheckSafetyResultAsOne(cb);
            IList<TQcwebSurveyInfo> ls = null;
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
            return (TQcwebSurveyInfo)ls[0];
        }

        protected override Entity DoReadEntity(ConditionBean cb) {
            return SelectEntity(Downcast(cb));
        }

        public virtual TQcwebSurveyInfo SelectEntityWithDeletedCheck(TQcwebSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            TQcwebSurveyInfo entity = SelectEntity(cb);
            AssertEntityNotDeleted(entity, cb);
            return entity;
        }

        protected override Entity DoReadEntityWithDeletedCheck(ConditionBean cb) {
            return SelectEntityWithDeletedCheck(Downcast(cb));
        }

        public virtual TQcwebSurveyInfo SelectByPKValue(decimal? qcwebid) {
            return SelectEntity(BuildPKCB(qcwebid));
        }

        public virtual TQcwebSurveyInfo SelectByPKValueWithDeletedCheck(decimal? qcwebid) {
            return SelectEntityWithDeletedCheck(BuildPKCB(qcwebid));
        }

        private TQcwebSurveyInfoCB BuildPKCB(decimal? qcwebid) {
            AssertObjectNotNull("qcwebid", qcwebid);
            TQcwebSurveyInfoCB cb = NewMyConditionBean();
            cb.Query().SetQcwebid_Equal(qcwebid);
            return cb;            
        }
        #endregion

        // ===============================================================================
        //                                                                     List Select
        //                                                                     ===========
        #region List Select
        public virtual ListResultBean<TQcwebSurveyInfo> SelectList(TQcwebSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            return new ResultBeanBuilder<TQcwebSurveyInfo>(TableDbName).BuildListResultBean(cb, this.DelegateSelectList(cb));
        }
        #endregion

        // ===============================================================================
        //                                                                     Page Select
        //                                                                     ===========
        #region Page Select
        public virtual PagingResultBean<TQcwebSurveyInfo> SelectPage(TQcwebSurveyInfoCB cb) {
            AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            PagingInvoker<TQcwebSurveyInfo> invoker = new PagingInvoker<TQcwebSurveyInfo>(TableDbName);
            return invoker.InvokePaging(new InternalSelectPagingHandler(this, cb));
        }

        private class InternalSelectPagingHandler : PagingHandler<TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv _bhv; protected TQcwebSurveyInfoCB _cb;
            public InternalSelectPagingHandler(TQcwebSurveyInfoBhv bhv, TQcwebSurveyInfoCB cb) { _bhv = bhv; _cb = cb; }
            public PagingBean PagingBean { get { return _cb; } }
            public int Count() { return _bhv.SelectCount(_cb); }
            public IList<TQcwebSurveyInfo> Paging() { return _bhv.SelectList(_cb); }
        }
        #endregion

        // ===============================================================================
        //                                                                        Sequence
        //                                                                        ========
        public decimal? SelectNextVal() {
            return DelegateSelectNextVal();
        }
        protected override void SetupNextValueToPrimaryKey(Entity entity) {// Very Internal
            TQcwebSurveyInfo myEntity = (TQcwebSurveyInfo)entity;
            myEntity.Qcwebid = SelectNextVal();
        }

        // ===============================================================================
        //                                                                   Load Referrer
        //                                                                   =============
        #region Load Referrer
        public virtual void LoadTAllocationCellInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TAllocationCellInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTAllocationCellInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTAllocationCellInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TAllocationCellInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTAllocationCellInfoList(tQcwebSurveyInfoList, new LoadReferrerOption<TAllocationCellInfoCB, TAllocationCellInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTAllocationCellInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TAllocationCellInfoCB, TAllocationCellInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTAllocationCellInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTAllocationCellInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TAllocationCellInfoCB, TAllocationCellInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TAllocationCellInfoBhv referrerBhv = xgetBSFLR().Select<TAllocationCellInfoBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TAllocationCellInfoCB, TAllocationCellInfo>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTAllocationCellInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTAllocationCellInfoListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TAllocationCellInfoCB, TAllocationCellInfo> {
            protected TAllocationCellInfoBhv referrerBhv;
            public MyInternalLoadTAllocationCellInfoListCallback(TAllocationCellInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TAllocationCellInfo> ls) { e.TAllocationCellInfoList = ls; }
            public TAllocationCellInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TAllocationCellInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TAllocationCellInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TAllocationCellInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TAllocationCellInfo> selRfLs(TAllocationCellInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TAllocationCellInfo e) { return e.Qcwebid; }
            public void setlcEt(TAllocationCellInfo re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTDataEditListList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TDataEditListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataEditListList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTDataEditListList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TDataEditListCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTDataEditListList(tQcwebSurveyInfoList, new LoadReferrerOption<TDataEditListCB, TDataEditList>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTDataEditListList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TDataEditListCB, TDataEditList> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTDataEditListList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTDataEditListList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TDataEditListCB, TDataEditList> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TDataEditListBhv referrerBhv = xgetBSFLR().Select<TDataEditListBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TDataEditListCB, TDataEditList>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTDataEditListListCallback(referrerBhv));
        }
        protected class MyInternalLoadTDataEditListListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TDataEditListCB, TDataEditList> {
            protected TDataEditListBhv referrerBhv;
            public MyInternalLoadTDataEditListListCallback(TDataEditListBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TDataEditList> ls) { e.TDataEditListList = ls; }
            public TDataEditListCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TDataEditListCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TDataEditListCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TDataEditListCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TDataEditList> selRfLs(TDataEditListCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TDataEditList e) { return e.Qcwebid; }
            public void setlcEt(TDataEditList re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTItemInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTItemInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TItemInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTItemInfoList(tQcwebSurveyInfoList, new LoadReferrerOption<TItemInfoCB, TItemInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTItemInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTItemInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTItemInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TItemInfoCB, TItemInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TItemInfoBhv referrerBhv = xgetBSFLR().Select<TItemInfoBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TItemInfoCB, TItemInfo>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTItemInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTItemInfoListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TItemInfoCB, TItemInfo> {
            protected TItemInfoBhv referrerBhv;
            public MyInternalLoadTItemInfoListCallback(TItemInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TItemInfo> ls) { e.TItemInfoList = ls; }
            public TItemInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TItemInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TItemInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TItemInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TItemInfo> selRfLs(TItemInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TItemInfo e) { return e.Qcwebid; }
            public void setlcEt(TItemInfo re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTNoticeList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TNoticeCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTNoticeList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTNoticeList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TNoticeCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTNoticeList(tQcwebSurveyInfoList, new LoadReferrerOption<TNoticeCB, TNotice>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTNoticeList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TNoticeCB, TNotice> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTNoticeList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTNoticeList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TNoticeCB, TNotice> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TNoticeBhv referrerBhv = xgetBSFLR().Select<TNoticeBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TNoticeCB, TNotice>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTNoticeListCallback(referrerBhv));
        }
        protected class MyInternalLoadTNoticeListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TNoticeCB, TNotice> {
            protected TNoticeBhv referrerBhv;
            public MyInternalLoadTNoticeListCallback(TNoticeBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TNotice> ls) { e.TNoticeList = ls; }
            public TNoticeCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TNoticeCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TNoticeCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TNoticeCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TNotice> selRfLs(TNoticeCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TNotice e) { return e.Qcwebid; }
            public void setlcEt(TNotice re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTOutputRequestList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TOutputRequestCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputRequestList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTOutputRequestList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TOutputRequestCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputRequestList(tQcwebSurveyInfoList, new LoadReferrerOption<TOutputRequestCB, TOutputRequest>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputRequestList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TOutputRequestCB, TOutputRequest> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputRequestList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTOutputRequestList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TOutputRequestCB, TOutputRequest> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TOutputRequestBhv referrerBhv = xgetBSFLR().Select<TOutputRequestBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TOutputRequestCB, TOutputRequest>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTOutputRequestListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputRequestListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TOutputRequestCB, TOutputRequest> {
            protected TOutputRequestBhv referrerBhv;
            public MyInternalLoadTOutputRequestListCallback(TOutputRequestBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TOutputRequest> ls) { e.TOutputRequestList = ls; }
            public TOutputRequestCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputRequestCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TOutputRequestCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TOutputRequestCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TOutputRequest> selRfLs(TOutputRequestCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputRequest e) { return e.Qcwebid; }
            public void setlcEt(TOutputRequest re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTOutputTemplateList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TOutputTemplateCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputTemplateList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTOutputTemplateList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TOutputTemplateCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTOutputTemplateList(tQcwebSurveyInfoList, new LoadReferrerOption<TOutputTemplateCB, TOutputTemplate>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTOutputTemplateList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TOutputTemplateCB, TOutputTemplate> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTOutputTemplateList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTOutputTemplateList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TOutputTemplateCB, TOutputTemplate> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TOutputTemplateBhv referrerBhv = xgetBSFLR().Select<TOutputTemplateBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TOutputTemplateCB, TOutputTemplate>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTOutputTemplateListCallback(referrerBhv));
        }
        protected class MyInternalLoadTOutputTemplateListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TOutputTemplateCB, TOutputTemplate> {
            protected TOutputTemplateBhv referrerBhv;
            public MyInternalLoadTOutputTemplateListCallback(TOutputTemplateBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TOutputTemplate> ls) { e.TOutputTemplateList = ls; }
            public TOutputTemplateCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TOutputTemplateCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TOutputTemplateCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TOutputTemplateCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TOutputTemplate> selRfLs(TOutputTemplateCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TOutputTemplate e) { return e.Qcwebid; }
            public void setlcEt(TOutputTemplate re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTQcwebSurveyDetailList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TQcwebSurveyDetailCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTQcwebSurveyDetailList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTQcwebSurveyDetailList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TQcwebSurveyDetailCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTQcwebSurveyDetailList(tQcwebSurveyInfoList, new LoadReferrerOption<TQcwebSurveyDetailCB, TQcwebSurveyDetail>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTQcwebSurveyDetailList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TQcwebSurveyDetailCB, TQcwebSurveyDetail> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTQcwebSurveyDetailList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTQcwebSurveyDetailList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TQcwebSurveyDetailCB, TQcwebSurveyDetail> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TQcwebSurveyDetailBhv referrerBhv = xgetBSFLR().Select<TQcwebSurveyDetailBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TQcwebSurveyDetailCB, TQcwebSurveyDetail>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTQcwebSurveyDetailListCallback(referrerBhv));
        }
        protected class MyInternalLoadTQcwebSurveyDetailListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TQcwebSurveyDetailCB, TQcwebSurveyDetail> {
            protected TQcwebSurveyDetailBhv referrerBhv;
            public MyInternalLoadTQcwebSurveyDetailListCallback(TQcwebSurveyDetailBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TQcwebSurveyDetail> ls) { e.TQcwebSurveyDetailList = ls; }
            public TQcwebSurveyDetailCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TQcwebSurveyDetailCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TQcwebSurveyDetailCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TQcwebSurveyDetailCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TQcwebSurveyDetail> selRfLs(TQcwebSurveyDetailCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TQcwebSurveyDetail e) { return e.Qcwebid; }
            public void setlcEt(TQcwebSurveyDetail re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTRawdataImportQueInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TRawdataImportQueInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTRawdataImportQueInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTRawdataImportQueInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TRawdataImportQueInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTRawdataImportQueInfoList(tQcwebSurveyInfoList, new LoadReferrerOption<TRawdataImportQueInfoCB, TRawdataImportQueInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTRawdataImportQueInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TRawdataImportQueInfoCB, TRawdataImportQueInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTRawdataImportQueInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTRawdataImportQueInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TRawdataImportQueInfoCB, TRawdataImportQueInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TRawdataImportQueInfoBhv referrerBhv = xgetBSFLR().Select<TRawdataImportQueInfoBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TRawdataImportQueInfoCB, TRawdataImportQueInfo>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTRawdataImportQueInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTRawdataImportQueInfoListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TRawdataImportQueInfoCB, TRawdataImportQueInfo> {
            protected TRawdataImportQueInfoBhv referrerBhv;
            public MyInternalLoadTRawdataImportQueInfoListCallback(TRawdataImportQueInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TRawdataImportQueInfo> ls) { e.TRawdataImportQueInfoList = ls; }
            public TRawdataImportQueInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TRawdataImportQueInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TRawdataImportQueInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TRawdataImportQueInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TRawdataImportQueInfo> selRfLs(TRawdataImportQueInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TRawdataImportQueInfo e) { return e.Qcwebid; }
            public void setlcEt(TRawdataImportQueInfo re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTReportsetList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TReportsetCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTReportsetList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTReportsetList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TReportsetCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTReportsetList(tQcwebSurveyInfoList, new LoadReferrerOption<TReportsetCB, TReportset>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTReportsetList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TReportsetCB, TReportset> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTReportsetList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTReportsetList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TReportsetCB, TReportset> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TReportsetBhv referrerBhv = xgetBSFLR().Select<TReportsetBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TReportsetCB, TReportset>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTReportsetListCallback(referrerBhv));
        }
        protected class MyInternalLoadTReportsetListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TReportsetCB, TReportset> {
            protected TReportsetBhv referrerBhv;
            public MyInternalLoadTReportsetListCallback(TReportsetBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TReportset> ls) { e.TReportsetList = ls; }
            public TReportsetCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TReportsetCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TReportsetCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TReportsetCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TReportset> selRfLs(TReportsetCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TReportset e) { return e.Qcwebid; }
            public void setlcEt(TReportset re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTScenarioTotalizationList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TScenarioTotalizationCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioTotalizationList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTScenarioTotalizationList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TScenarioTotalizationCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTScenarioTotalizationList(tQcwebSurveyInfoList, new LoadReferrerOption<TScenarioTotalizationCB, TScenarioTotalization>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTScenarioTotalizationList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TScenarioTotalizationCB, TScenarioTotalization> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTScenarioTotalizationList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTScenarioTotalizationList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TScenarioTotalizationCB, TScenarioTotalization> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TScenarioTotalizationBhv referrerBhv = xgetBSFLR().Select<TScenarioTotalizationBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TScenarioTotalizationCB, TScenarioTotalization>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTScenarioTotalizationListCallback(referrerBhv));
        }
        protected class MyInternalLoadTScenarioTotalizationListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TScenarioTotalizationCB, TScenarioTotalization> {
            protected TScenarioTotalizationBhv referrerBhv;
            public MyInternalLoadTScenarioTotalizationListCallback(TScenarioTotalizationBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TScenarioTotalization> ls) { e.TScenarioTotalizationList = ls; }
            public TScenarioTotalizationCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TScenarioTotalizationCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TScenarioTotalizationCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TScenarioTotalizationCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TScenarioTotalization> selRfLs(TScenarioTotalizationCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TScenarioTotalization e) { return e.Qcwebid; }
            public void setlcEt(TScenarioTotalization re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTSelectConditionInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TSelectConditionInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTSelectConditionInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTSelectConditionInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TSelectConditionInfoCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTSelectConditionInfoList(tQcwebSurveyInfoList, new LoadReferrerOption<TSelectConditionInfoCB, TSelectConditionInfo>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTSelectConditionInfoList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TSelectConditionInfoCB, TSelectConditionInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTSelectConditionInfoList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTSelectConditionInfoList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TSelectConditionInfoCB, TSelectConditionInfo> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TSelectConditionInfoBhv referrerBhv = xgetBSFLR().Select<TSelectConditionInfoBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TSelectConditionInfoCB, TSelectConditionInfo>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTSelectConditionInfoListCallback(referrerBhv));
        }
        protected class MyInternalLoadTSelectConditionInfoListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TSelectConditionInfoCB, TSelectConditionInfo> {
            protected TSelectConditionInfoBhv referrerBhv;
            public MyInternalLoadTSelectConditionInfoListCallback(TSelectConditionInfoBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TSelectConditionInfo> ls) { e.TSelectConditionInfoList = ls; }
            public TSelectConditionInfoCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TSelectConditionInfoCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TSelectConditionInfoCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TSelectConditionInfoCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TSelectConditionInfo> selRfLs(TSelectConditionInfoCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TSelectConditionInfo e) { return e.Qcwebid; }
            public void setlcEt(TSelectConditionInfo re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTSessionControlerList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TSessionControlerCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTSessionControlerList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTSessionControlerList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TSessionControlerCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTSessionControlerList(tQcwebSurveyInfoList, new LoadReferrerOption<TSessionControlerCB, TSessionControler>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTSessionControlerList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TSessionControlerCB, TSessionControler> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTSessionControlerList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTSessionControlerList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TSessionControlerCB, TSessionControler> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TSessionControlerBhv referrerBhv = xgetBSFLR().Select<TSessionControlerBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TSessionControlerCB, TSessionControler>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTSessionControlerListCallback(referrerBhv));
        }
        protected class MyInternalLoadTSessionControlerListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TSessionControlerCB, TSessionControler> {
            protected TSessionControlerBhv referrerBhv;
            public MyInternalLoadTSessionControlerListCallback(TSessionControlerBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TSessionControler> ls) { e.TSessionControlerList = ls; }
            public TSessionControlerCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TSessionControlerCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TSessionControlerCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TSessionControlerCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TSessionControler> selRfLs(TSessionControlerCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TSessionControler e) { return e.Qcwebid; }
            public void setlcEt(TSessionControler re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        public virtual void LoadTWeightbackList(TQcwebSurveyInfo tQcwebSurveyInfo, ConditionBeanSetupper<TWeightbackCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTWeightbackList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), conditionBeanSetupper);
        }
        public virtual void LoadTWeightbackList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, ConditionBeanSetupper<TWeightbackCB> conditionBeanSetupper) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("conditionBeanSetupper", conditionBeanSetupper);
            LoadTWeightbackList(tQcwebSurveyInfoList, new LoadReferrerOption<TWeightbackCB, TWeightback>().xinit(conditionBeanSetupper));
        }
        public virtual void LoadTWeightbackList(TQcwebSurveyInfo tQcwebSurveyInfo, LoadReferrerOption<TWeightbackCB, TWeightback> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            LoadTWeightbackList(xnewLRLs<TQcwebSurveyInfo>(tQcwebSurveyInfo), loadReferrerOption);
        }
        public virtual void LoadTWeightbackList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList, LoadReferrerOption<TWeightbackCB, TWeightback> loadReferrerOption) {
            AssertObjectNotNull("tQcwebSurveyInfoList", tQcwebSurveyInfoList); AssertObjectNotNull("loadReferrerOption", loadReferrerOption);
            if (tQcwebSurveyInfoList.Count == 0) { return; }
            TWeightbackBhv referrerBhv = xgetBSFLR().Select<TWeightbackBhv>();
            HelpLoadReferrerInternally<TQcwebSurveyInfo, decimal?, TWeightbackCB, TWeightback>
                    (tQcwebSurveyInfoList, loadReferrerOption, new MyInternalLoadTWeightbackListCallback(referrerBhv));
        }
        protected class MyInternalLoadTWeightbackListCallback : InternalLoadReferrerCallback<TQcwebSurveyInfo, decimal?, TWeightbackCB, TWeightback> {
            protected TWeightbackBhv referrerBhv;
            public MyInternalLoadTWeightbackListCallback(TWeightbackBhv referrerBhv) { this.referrerBhv = referrerBhv; }
            public decimal? getPKVal(TQcwebSurveyInfo e) { return e.Qcwebid; }
            public void setRfLs(TQcwebSurveyInfo e, IList<TWeightback> ls) { e.TWeightbackList = ls; }
            public TWeightbackCB newMyCB() { return referrerBhv.NewMyConditionBean(); }
            public void qyFKIn(TWeightbackCB cb, IList<decimal?> ls) { cb.Query().SetQcwebid_InScope(ls); }
            public void qyOdFKAsc(TWeightbackCB cb) { cb.Query().AddOrderBy_Qcwebid_Asc(); }
            public void spFKCol(TWeightbackCB cb) { cb.Specify().ColumnQcwebid(); }
            public IList<TWeightback> selRfLs(TWeightbackCB cb) { return referrerBhv.SelectList(cb); }
            public decimal? getFKVal(TWeightback e) { return e.Qcwebid; }
            public void setlcEt(TWeightback re, TQcwebSurveyInfo be) { re.TQcwebSurveyInfo = be; }
        }
        #endregion

        // ===============================================================================
        //                                                                Pull out Foreign
        //                                                                ================
        #region Pullout Foreign
        public IList<TSurveyInfo> PulloutTSurveyInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TSurveyInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTSurveyInfoCallback());
        }
        protected class MyInternalPulloutTSurveyInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TSurveyInfo> {
            public TSurveyInfo getFr(TQcwebSurveyInfo entity) { return entity.TSurveyInfo; }
        }
        public IList<TRawdataImportQueInfo> PulloutTRawdataImportQueInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TRawdataImportQueInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTRawdataImportQueInfoCallback());
        }
        protected class MyInternalPulloutTRawdataImportQueInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TRawdataImportQueInfo> {
            public TRawdataImportQueInfo getFr(TQcwebSurveyInfo entity) { return entity.TRawdataImportQueInfo; }
        }
        public IList<TAllocationCellInfo> PulloutTAllocationCellInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TAllocationCellInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTAllocationCellInfoCallback());
        }
        protected class MyInternalPulloutTAllocationCellInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TAllocationCellInfo> {
            public TAllocationCellInfo getFr(TQcwebSurveyInfo entity) { return entity.TAllocationCellInfo; }
        }
        public IList<TSelectConditionInfo> PulloutTSelectConditionInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TSelectConditionInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTSelectConditionInfoCallback());
        }
        protected class MyInternalPulloutTSelectConditionInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TSelectConditionInfo> {
            public TSelectConditionInfo getFr(TQcwebSurveyInfo entity) { return entity.TSelectConditionInfo; }
        }
        public IList<TItemInfo> PulloutTItemInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TItemInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTItemInfoCallback());
        }
        protected class MyInternalPulloutTItemInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TItemInfo> {
            public TItemInfo getFr(TQcwebSurveyInfo entity) { return entity.TItemInfo; }
        }
        public IList<TTableControl> PulloutTTableControl(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TTableControl>(tQcwebSurveyInfoList, new MyInternalPulloutTTableControlCallback());
        }
        protected class MyInternalPulloutTTableControlCallback : InternalPulloutCallback<TQcwebSurveyInfo, TTableControl> {
            public TTableControl getFr(TQcwebSurveyInfo entity) { return entity.TTableControl; }
        }
        public IList<TDefaultEnv> PulloutTDefaultEnv(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TDefaultEnv>(tQcwebSurveyInfoList, new MyInternalPulloutTDefaultEnvCallback());
        }
        protected class MyInternalPulloutTDefaultEnvCallback : InternalPulloutCallback<TQcwebSurveyInfo, TDefaultEnv> {
            public TDefaultEnv getFr(TQcwebSurveyInfo entity) { return entity.TDefaultEnv; }
        }
        public IList<TDefaultEnvColorInfo> PulloutTDefaultEnvColorInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TDefaultEnvColorInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTDefaultEnvColorInfoCallback());
        }
        protected class MyInternalPulloutTDefaultEnvColorInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TDefaultEnvColorInfo> {
            public TDefaultEnvColorInfo getFr(TQcwebSurveyInfo entity) { return entity.TDefaultEnvColorInfo; }
        }
        public IList<TScenarioTotalization> PulloutTScenarioTotalization(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TScenarioTotalization>(tQcwebSurveyInfoList, new MyInternalPulloutTScenarioTotalizationCallback());
        }
        protected class MyInternalPulloutTScenarioTotalizationCallback : InternalPulloutCallback<TQcwebSurveyInfo, TScenarioTotalization> {
            public TScenarioTotalization getFr(TQcwebSurveyInfo entity) { return entity.TScenarioTotalization; }
        }
        public IList<TReportset> PulloutTReportset(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TReportset>(tQcwebSurveyInfoList, new MyInternalPulloutTReportsetCallback());
        }
        protected class MyInternalPulloutTReportsetCallback : InternalPulloutCallback<TQcwebSurveyInfo, TReportset> {
            public TReportset getFr(TQcwebSurveyInfo entity) { return entity.TReportset; }
        }
        public IList<TDataEditList> PulloutTDataEditList(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TDataEditList>(tQcwebSurveyInfoList, new MyInternalPulloutTDataEditListCallback());
        }
        protected class MyInternalPulloutTDataEditListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TDataEditList> {
            public TDataEditList getFr(TQcwebSurveyInfo entity) { return entity.TDataEditList; }
        }
        public IList<TOutputSetting> PulloutTOutputSetting(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSetting>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingCallback());
        }
        protected class MyInternalPulloutTOutputSettingCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSetting> {
            public TOutputSetting getFr(TQcwebSurveyInfo entity) { return entity.TOutputSetting; }
        }
        public IList<TOutputRequest> PulloutTOutputRequest(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputRequest>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputRequestCallback());
        }
        protected class MyInternalPulloutTOutputRequestCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputRequest> {
            public TOutputRequest getFr(TQcwebSurveyInfo entity) { return entity.TOutputRequest; }
        }
        public IList<TAccessPermissionsInfo> PulloutTAccessPermissionsInfo(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TAccessPermissionsInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTAccessPermissionsInfoCallback());
        }
        protected class MyInternalPulloutTAccessPermissionsInfoCallback : InternalPulloutCallback<TQcwebSurveyInfo, TAccessPermissionsInfo> {
            public TAccessPermissionsInfo getFr(TQcwebSurveyInfo entity) { return entity.TAccessPermissionsInfo; }
        }
        public IList<TSessionControler> PulloutTSessionControler(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TSessionControler>(tQcwebSurveyInfoList, new MyInternalPulloutTSessionControlerCallback());
        }
        protected class MyInternalPulloutTSessionControlerCallback : InternalPulloutCallback<TQcwebSurveyInfo, TSessionControler> {
            public TSessionControler getFr(TQcwebSurveyInfo entity) { return entity.TSessionControler; }
        }
        public IList<TNotice> PulloutTNotice(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TNotice>(tQcwebSurveyInfoList, new MyInternalPulloutTNoticeCallback());
        }
        protected class MyInternalPulloutTNoticeCallback : InternalPulloutCallback<TQcwebSurveyInfo, TNotice> {
            public TNotice getFr(TQcwebSurveyInfo entity) { return entity.TNotice; }
        }
        public IList<TOutputSettingGt> PulloutTOutputSettingGt(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingGt>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingGtCallback());
        }
        protected class MyInternalPulloutTOutputSettingGtCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingGt> {
            public TOutputSettingGt getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingGt; }
        }
        public IList<TOutputSettingCross> PulloutTOutputSettingCross(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingCross>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingCrossCallback());
        }
        protected class MyInternalPulloutTOutputSettingCrossCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingCross> {
            public TOutputSettingCross getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingCross; }
        }
        public IList<TOutputSettingFa> PulloutTOutputSettingFa(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingFa>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingFaCallback());
        }
        protected class MyInternalPulloutTOutputSettingFaCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingFa> {
            public TOutputSettingFa getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingFa; }
        }
        public IList<TOutputSettingReport> PulloutTOutputSettingReport(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingReport>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingReportCallback());
        }
        protected class MyInternalPulloutTOutputSettingReportCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingReport> {
            public TOutputSettingReport getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingReport; }
        }
        public IList<TQcwebSurveyDetail> PulloutTQcwebSurveyDetail(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TQcwebSurveyDetail>(tQcwebSurveyInfoList, new MyInternalPulloutTQcwebSurveyDetailCallback());
        }
        protected class MyInternalPulloutTQcwebSurveyDetailCallback : InternalPulloutCallback<TQcwebSurveyInfo, TQcwebSurveyDetail> {
            public TQcwebSurveyDetail getFr(TQcwebSurveyInfo entity) { return entity.TQcwebSurveyDetail; }
        }
        public IList<TAccessPermissionsInfo> PulloutTAccessPermissionsInfoAsOne(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TAccessPermissionsInfo>(tQcwebSurveyInfoList, new MyInternalPulloutTAccessPermissionsInfoListCallback());
        }
        protected class MyInternalPulloutTAccessPermissionsInfoListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TAccessPermissionsInfo> {
            public TAccessPermissionsInfo getFr(TQcwebSurveyInfo entity) { return entity.TAccessPermissionsInfoAsOne; }
        }
        public IList<TOutputSetting> PulloutTOutputSettingAsOne(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSetting>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingListCallback());
        }
        protected class MyInternalPulloutTOutputSettingListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSetting> {
            public TOutputSetting getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingAsOne; }
        }
        public IList<TOutputSettingCross> PulloutTOutputSettingCrossAsOne(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingCross>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingCrossListCallback());
        }
        protected class MyInternalPulloutTOutputSettingCrossListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingCross> {
            public TOutputSettingCross getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingCrossAsOne; }
        }
        public IList<TOutputSettingFa> PulloutTOutputSettingFaAsOne(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingFa>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingFaListCallback());
        }
        protected class MyInternalPulloutTOutputSettingFaListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingFa> {
            public TOutputSettingFa getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingFaAsOne; }
        }
        public IList<TOutputSettingGt> PulloutTOutputSettingGtAsOne(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingGt>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingGtListCallback());
        }
        protected class MyInternalPulloutTOutputSettingGtListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingGt> {
            public TOutputSettingGt getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingGtAsOne; }
        }
        public IList<TOutputSettingReport> PulloutTOutputSettingReportAsOne(IList<TQcwebSurveyInfo> tQcwebSurveyInfoList) {
            return HelpPulloutInternally<TQcwebSurveyInfo, TOutputSettingReport>(tQcwebSurveyInfoList, new MyInternalPulloutTOutputSettingReportListCallback());
        }
        protected class MyInternalPulloutTOutputSettingReportListCallback : InternalPulloutCallback<TQcwebSurveyInfo, TOutputSettingReport> {
            public TOutputSettingReport getFr(TQcwebSurveyInfo entity) { return entity.TOutputSettingReportAsOne; }
        }
        #endregion


        // ===============================================================================
        //                                                                   Entity Update
        //                                                                   =============
        #region Basic Entity Update
        public virtual void Insert(TQcwebSurveyInfo entity) {
            AssertEntityNotNull(entity);
            this.DelegateInsert(entity);
        }

        protected override void DoCreate(Entity entity) {
            Insert(Downcast(entity));
        }

        public virtual void Update(TQcwebSurveyInfo entity) {
            AssertEntityNotNull(entity);
            AssertEntityHasVersionNoValue(entity);
            AssertEntityHasUpdateDateValue(entity);
            int updatedCount = this.DelegateUpdate(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        protected override void DoModify(Entity entity) {
            Update(Downcast(entity));
        }

        public virtual void UpdateNonstrict(TQcwebSurveyInfo entity) {
            AssertEntityNotNull(entity);
            int updatedCount = this.DelegateUpdateNonstrict(entity);
            AssertUpdatedEntity(entity, updatedCount);
        }

        public void InsertOrUpdate(TQcwebSurveyInfo entity) {
            HelpInsertOrUpdateInternally<TQcwebSurveyInfo, TQcwebSurveyInfoCB>(entity, new MyInternalInsertOrUpdateCallback(this));
        }
        protected class MyInternalInsertOrUpdateCallback : InternalInsertOrUpdateCallback<TQcwebSurveyInfo, TQcwebSurveyInfoCB> {
            protected TQcwebSurveyInfoBhv _bhv;
            public MyInternalInsertOrUpdateCallback(TQcwebSurveyInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TQcwebSurveyInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdate(TQcwebSurveyInfo entity) { _bhv.Update(entity); }
            public TQcwebSurveyInfoCB CallbackNewMyConditionBean() { return _bhv.NewMyConditionBean(); }
            public void CallbackSetupPrimaryKeyCondition(TQcwebSurveyInfoCB cb, TQcwebSurveyInfo entity) {
                cb.Query().SetQcwebid_Equal(entity.Qcwebid);
            }
            public int CallbackSelectCount(TQcwebSurveyInfoCB cb) { return _bhv.SelectCount(cb); }
        }

        public void InsertOrUpdateNonstrict(TQcwebSurveyInfo entity) {
            HelpInsertOrUpdateInternally<TQcwebSurveyInfo>(entity, new MyInternalInsertOrUpdateNonstrictCallback(this));
        }
        protected class MyInternalInsertOrUpdateNonstrictCallback : InternalInsertOrUpdateNonstrictCallback<TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv _bhv;
            public MyInternalInsertOrUpdateNonstrictCallback(TQcwebSurveyInfoBhv bhv) { _bhv = bhv; }
            public void CallbackInsert(TQcwebSurveyInfo entity) { _bhv.Insert(entity); }
            public void CallbackUpdateNonstrict(TQcwebSurveyInfo entity) { _bhv.UpdateNonstrict(entity); }
        }

        public virtual void Delete(TQcwebSurveyInfo entity) {
            HelpDeleteInternally<TQcwebSurveyInfo>(entity, new MyInternalDeleteCallback(this));
        }

        protected override void DoRemove(Entity entity) {
            Remove(Downcast(entity));
        }

        protected class MyInternalDeleteCallback : InternalDeleteCallback<TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv _bhv;
            public MyInternalDeleteCallback(TQcwebSurveyInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDelete(TQcwebSurveyInfo entity) { return _bhv.DelegateDelete(entity); }
        }

        public virtual void DeleteNonstrict(TQcwebSurveyInfo entity) {
            HelpDeleteNonstrictInternally<TQcwebSurveyInfo>(entity, new MyInternalDeleteNonstrictCallback(this));
        }
        protected class MyInternalDeleteNonstrictCallback : InternalDeleteNonstrictCallback<TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv _bhv;
            public MyInternalDeleteNonstrictCallback(TQcwebSurveyInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDeleteNonstrict(TQcwebSurveyInfo entity) { return _bhv.DelegateDeleteNonstrict(entity); }
        }

        public virtual void DeleteNonstrictIgnoreDeleted(TQcwebSurveyInfo entity) {
            HelpDeleteNonstrictIgnoreDeletedInternally<TQcwebSurveyInfo>(entity, new MyInternalDeleteNonstrictIgnoreDeletedCallback(this));
        }
        protected class MyInternalDeleteNonstrictIgnoreDeletedCallback : InternalDeleteNonstrictIgnoreDeletedCallback<TQcwebSurveyInfo> {
            protected TQcwebSurveyInfoBhv _bhv;
            public MyInternalDeleteNonstrictIgnoreDeletedCallback(TQcwebSurveyInfoBhv bhv) { _bhv = bhv; }
            public int CallbackDelegateDeleteNonstrict(TQcwebSurveyInfo entity) { return _bhv.DelegateDeleteNonstrict(entity); }
        }
        #endregion

        // ===============================================================================
        //                                                                    Query Update
        //                                                                    ============
        public int QueryUpdate(TQcwebSurveyInfo tQcwebSurveyInfo, TQcwebSurveyInfoCB cb) {
            AssertObjectNotNull("tQcwebSurveyInfo", tQcwebSurveyInfo); AssertConditionBeanNotNull(cb);
            // 2013/02/21 cterash start
            if (cb.Query().WhereSetterFlag && !cb.HasWhereClause()) {
                throw new SelectEntityConditionNotFoundException("Where句Setterメソッドが呼び出されていますが無条件になっています");
            }
            // 2013/02/21 cterash end
            SetupCommonColumnOfUpdateIfNeeds(tQcwebSurveyInfo);
            FilterEntityOfUpdate(tQcwebSurveyInfo); AssertEntityOfUpdate(tQcwebSurveyInfo);
            return this.Dao.UpdateByQuery(cb, tQcwebSurveyInfo);
        }

        public int QueryDelete(TQcwebSurveyInfoCB cb) {
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
            return Downcast(entity).VersionNo != null;
        }

        protected override bool HasUpdateDateValue(Entity entity) {
            return false;
        }

        // ===============================================================================
        //                                                                 Delegate Method
        //                                                                 ===============
        #region Delegate Method
        protected int DelegateSelectCount(TQcwebSurveyInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectCount(cb); }
        protected IList<TQcwebSurveyInfo> DelegateSelectList(TQcwebSurveyInfoCB cb) { AssertConditionBeanNotNull(cb); return this.Dao.SelectList(cb); }
        protected decimal? DelegateSelectNextVal() { return this.Dao.SelectNextVal(); }

        protected int DelegateInsert(TQcwebSurveyInfo e) { if (!ProcessBeforeInsert(e)) { return 1; } return this.Dao.Insert(e); }
        protected int DelegateUpdate(TQcwebSurveyInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateModifiedOnly(e); }
        protected int DelegateUpdateNonstrict(TQcwebSurveyInfo e)
        { if (!ProcessBeforeUpdate(e)) { return 1; } return this.Dao.UpdateNonstrictModifiedOnly(e); }
        protected int DelegateDelete(TQcwebSurveyInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.Delete(e); }
        protected int DelegateDeleteNonstrict(TQcwebSurveyInfo e)
        { if (!ProcessBeforeDelete(e)) { return 1; } return this.Dao.DeleteNonstrict(e); }
        #endregion

        // ===============================================================================
        //                                                                 Downcast Helper
        //                                                                 ===============
        protected TQcwebSurveyInfo Downcast(Entity entity) {
            return (TQcwebSurveyInfo)entity;
        }

        protected TQcwebSurveyInfoCB Downcast(ConditionBean cb) {
            return (TQcwebSurveyInfoCB)cb;
        }

        // ===============================================================================
        //                                                                        Accessor
        //                                                                        ========
        public virtual TQcwebSurveyInfoDao Dao { get { return _dao; } set { _dao = value; } }
    }
}
