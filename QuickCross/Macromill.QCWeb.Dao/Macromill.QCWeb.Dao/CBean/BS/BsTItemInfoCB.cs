
using System;
using System.Collections;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.Helper;

using Macromill.QCWeb.Dao.CBean;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.Nss;

namespace Macromill.QCWeb.Dao.CBean.BS {

    [System.Serializable]
    public class BsTItemInfoCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TItemInfoCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_ITEM_INFO"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(decimal? itemInfoId) {
            assertObjectNotNull("itemInfoId", itemInfoId);
            BsTItemInfoCB cb = this;
            cb.Query().SetItemInfoId_Equal(itemInfoId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_ItemInfoId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_ItemInfoId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TItemInfoCQ Query() {
            return this.ConditionQuery;
        }

        public TItemInfoCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TItemInfoCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TItemInfoCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TItemInfoCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TItemInfoCB> unionQuery) {
            TItemInfoCB cb = new TItemInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TItemInfoCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TItemInfoCB> unionQuery) {
            TItemInfoCB cb = new TItemInfoCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TItemInfoCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
	    }

        public override bool HasUnionQueryOrUnionAllQuery() {
            return Query().hasUnionQueryOrUnionAllQuery();
        }

        // ===============================================================================
        //                                                                       Lock Wait
        //                                                                       =========
        public virtual ConditionBean LockForUpdateNoWait()
        { if (xhelpIsSqlClauseOracle()) { xhelpGettingSqlClauseOracle().lockForUpdateNoWait(); } return this; }
        public virtual ConditionBean LockForUpdateWait(int waitSec)
        { if (xhelpIsSqlClauseOracle()) { xhelpGettingSqlClauseOracle().lockForUpdateWait(waitSec); } return this; }

        protected virtual bool xhelpIsSqlClauseOracle() {
            return this.SqlClause is Macromill.QCWeb.Dao.AllCommon.CBean.SClause.SqlClauseOracle;
        }

        protected virtual Macromill.QCWeb.Dao.AllCommon.CBean.SClause.SqlClauseOracle xhelpGettingSqlClauseOracle() {
            return (Macromill.QCWeb.Dao.AllCommon.CBean.SClause.SqlClauseOracle)this.SqlClause;
        }

        // ===============================================================================
        //                                                                    Setup Select
        //                                                                    ============
        protected TQcwebSurveyInfoNss _nssTQcwebSurveyInfo;
        public TQcwebSurveyInfoNss NssTQcwebSurveyInfo { get {
            if (_nssTQcwebSurveyInfo == null) { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(null); }
            return _nssTQcwebSurveyInfo;
        }}
        public TQcwebSurveyInfoNss SetupSelect_TQcwebSurveyInfo() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTQcwebSurveyInfo(); });
            if (_nssTQcwebSurveyInfo == null || !_nssTQcwebSurveyInfo.HasConditionQuery)
            { _nssTQcwebSurveyInfo = new TQcwebSurveyInfoNss(Query().QueryTQcwebSurveyInfo()); }
            return _nssTQcwebSurveyInfo;
        }
        protected TMatrixInfoNss _nssTMatrixInfo;
        public TMatrixInfoNss NssTMatrixInfo { get {
            if (_nssTMatrixInfo == null) { _nssTMatrixInfo = new TMatrixInfoNss(null); }
            return _nssTMatrixInfo;
        }}
        public TMatrixInfoNss SetupSelect_TMatrixInfo() {
            doSetupSelect(delegate { return Query().QueryTMatrixInfo(); });
            if (_nssTMatrixInfo == null || !_nssTMatrixInfo.HasConditionQuery)
            { _nssTMatrixInfo = new TMatrixInfoNss(Query().QueryTMatrixInfo()); }
            return _nssTMatrixInfo;
        }
        protected TFaListAddItemNss _nssTFaListAddItem;
        public TFaListAddItemNss NssTFaListAddItem { get {
            if (_nssTFaListAddItem == null) { _nssTFaListAddItem = new TFaListAddItemNss(null); }
            return _nssTFaListAddItem;
        }}
        public TFaListAddItemNss SetupSelect_TFaListAddItem() {
            doSetupSelect(delegate { return Query().QueryTFaListAddItem(); });
            if (_nssTFaListAddItem == null || !_nssTFaListAddItem.HasConditionQuery)
            { _nssTFaListAddItem = new TFaListAddItemNss(Query().QueryTFaListAddItem()); }
            return _nssTFaListAddItem;
        }
        protected TFaScenarioItemNss _nssTFaScenarioItem;
        public TFaScenarioItemNss NssTFaScenarioItem { get {
            if (_nssTFaScenarioItem == null) { _nssTFaScenarioItem = new TFaScenarioItemNss(null); }
            return _nssTFaScenarioItem;
        }}
        public TFaScenarioItemNss SetupSelect_TFaScenarioItem() {
            doSetupSelect(delegate { return Query().QueryTFaScenarioItem(); });
            if (_nssTFaScenarioItem == null || !_nssTFaScenarioItem.HasConditionQuery)
            { _nssTFaScenarioItem = new TFaScenarioItemNss(Query().QueryTFaScenarioItem()); }
            return _nssTFaScenarioItem;
        }
        protected TTableControlNss _nssTTableControl;
        public TTableControlNss NssTTableControl { get {
            if (_nssTTableControl == null) { _nssTTableControl = new TTableControlNss(null); }
            return _nssTTableControl;
        }}
        public TTableControlNss SetupSelect_TTableControl() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnQcwebid();
            }
            doSetupSelect(delegate { return Query().QueryTTableControl(); });
            if (_nssTTableControl == null || !_nssTTableControl.HasConditionQuery)
            { _nssTTableControl = new TTableControlNss(Query().QueryTTableControl()); }
            return _nssTTableControl;
        }
        protected TScenarioTotalizationNss _nssTScenarioTotalization;
        public TScenarioTotalizationNss NssTScenarioTotalization { get {
            if (_nssTScenarioTotalization == null) { _nssTScenarioTotalization = new TScenarioTotalizationNss(null); }
            return _nssTScenarioTotalization;
        }}
        public TScenarioTotalizationNss SetupSelect_TScenarioTotalization() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnCategoryEditId();
            }
            doSetupSelect(delegate { return Query().QueryTScenarioTotalization(); });
            if (_nssTScenarioTotalization == null || !_nssTScenarioTotalization.HasConditionQuery)
            { _nssTScenarioTotalization = new TScenarioTotalizationNss(Query().QueryTScenarioTotalization()); }
            return _nssTScenarioTotalization;
        }
        protected TDataEditListNss _nssTDataEditList;
        public TDataEditListNss NssTDataEditList { get {
            if (_nssTDataEditList == null) { _nssTDataEditList = new TDataEditListNss(null); }
            return _nssTDataEditList;
        }}
        public TDataEditListNss SetupSelect_TDataEditList() {
            if (HasSpecifiedColumn) { // if reverse call
                Specify().ColumnDataEditId();
            }
            doSetupSelect(delegate { return Query().QueryTDataEditList(); });
            if (_nssTDataEditList == null || !_nssTDataEditList.HasConditionQuery)
            { _nssTDataEditList = new TDataEditListNss(Query().QueryTDataEditList()); }
            return _nssTDataEditList;
        }

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TItemInfoCBSpecification _specification;
        public TItemInfoCBSpecification Specify() {
            if (_specification == null) { _specification = new TItemInfoCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TItemInfoCQ> {
			protected BsTItemInfoCB _myCB;
			public MySpQyCall(BsTItemInfoCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TItemInfoCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TItemInfoCB> ColumnQuery(SpecifyQuery<TItemInfoCB> leftSpecifyQuery) {
            return new HpColQyOperand<TItemInfoCB>(delegate(SpecifyQuery<TItemInfoCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TItemInfoCB xcreateColumnQueryCB() {
            TItemInfoCB cb = new TItemInfoCB();
            cb.xsetupForColumnQuery((TItemInfoCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TItemInfoCB> orQuery) {
            xorQ((TItemInfoCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TItemInfoCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TItemInfoCBColQySpQyCall(mainCB));
        }
    }

    public class TItemInfoCBColQySpQyCall : HpSpQyCall<TItemInfoCQ> {
        protected TItemInfoCB _mainCB;
        public TItemInfoCBColQySpQyCall(TItemInfoCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TItemInfoCQ qy() { return _mainCB.Query(); } 
    }

    public class TItemInfoCBSpecification : AbstractSpecification<TItemInfoCQ> {
        protected TQcwebSurveyInfoCBSpecification _tQcwebSurveyInfo;
        protected TMatrixInfoCBSpecification _tMatrixInfo;
        protected TFaListAddItemCBSpecification _tFaListAddItem;
        protected TFaScenarioItemCBSpecification _tFaScenarioItem;
        protected TTableControlCBSpecification _tTableControl;
        protected TScenarioTotalizationCBSpecification _tScenarioTotalization;
        protected TDataEditListCBSpecification _tDataEditList;
        public TItemInfoCBSpecification(ConditionBean baseCB, HpSpQyCall<TItemInfoCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnItemInfoId() { doColumn("ITEM_INFO_ID"); }
        public void ColumnQcwebid() { doColumn("QCWEBID"); }
        public void ColumnItemName() { doColumn("ITEM_NAME"); }
        public void ColumnSourceDiv() { doColumn("SOURCE_DIV"); }
        public void ColumnItemno() { doColumn("ITEMNO"); }
        public void ColumnItemType() { doColumn("ITEM_TYPE"); }
        public void ColumnAnswerType() { doColumn("ANSWER_TYPE"); }
        public void ColumnSortNumber() { doColumn("SORT_NUMBER"); }
        public void ColumnMatrixDiv() { doColumn("MATRIX_DIV"); }
        public void ColumnLv1title() { doColumn("LV1TITLE"); }
        public void ColumnLv2title() { doColumn("LV2TITLE"); }
        public void ColumnOriginalLv1title() { doColumn("ORIGINAL_LV1TITLE"); }
        public void ColumnOriginalLv2title() { doColumn("ORIGINAL_LV2TITLE"); }
        public void ColumnTableName() { doColumn("TABLE_NAME"); }
        public void ColumnColumnName() { doColumn("COLUMN_NAME"); }
        public void ColumnCategoryEditId() { doColumn("CATEGORY_EDIT_ID"); }
        public void ColumnDataEditId() { doColumn("DATA_EDIT_ID"); }
        public void ColumnStatus() { doColumn("STATUS"); }
        public void ColumnTableNameOrg() { doColumn("TABLE_NAME_ORG"); }
        public void ColumnColumnNameOrg() { doColumn("COLUMN_NAME_ORG"); }
        public void ColumnCompelItemChangeFlag() { doColumn("COMPEL_ITEM_CHANGE_FLAG"); }
        public void ColumnSortFlag() { doColumn("SORT_FLAG"); }
        public void ColumnSortRange() { doColumn("SORT_RANGE"); }
        public void ColumnMultivariateFlag() { doColumn("MULTIVARIATE_FLAG"); }
        public void ColumnTableNo() { doColumn("TABLE_NO"); }
        public void ColumnColumnNo() { doColumn("COLUMN_NO"); }
        public void ColumnTableNoOrg() { doColumn("TABLE_NO_ORG"); }
        public void ColumnColumnNoOrg() { doColumn("COLUMN_NO_ORG"); }
        public void ColumnLastUpdateUser() { doColumn("LAST_UPDATE_USER"); }
        public void ColumnLastUpdateDatetime() { doColumn("LAST_UPDATE_DATETIME"); }
        public void ColumnNewAtQc3Flag() { doColumn("NEW_AT_QC3_FLAG"); }
        public void ColumnSortRangeOrg() { doColumn("SORT_RANGE_ORG"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnItemInfoId(); // PK
            if (qyCall().qy().hasConditionQueryTQcwebSurveyInfo()
                    || qyCall().qy().xgetReferrerQuery() is TQcwebSurveyInfoCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTTableControl()
                    || qyCall().qy().xgetReferrerQuery() is TTableControlCQ) {
                ColumnQcwebid(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTScenarioTotalization()
                    || qyCall().qy().xgetReferrerQuery() is TScenarioTotalizationCQ) {
                ColumnCategoryEditId(); // FK or one-to-one referrer
            }
            if (qyCall().qy().hasConditionQueryTDataEditList()
                    || qyCall().qy().xgetReferrerQuery() is TDataEditListCQ) {
                ColumnDataEditId(); // FK or one-to-one referrer
            }
        }
        protected override String getTableDbName() { return "T_ITEM_INFO"; }
        public TQcwebSurveyInfoCBSpecification SpecifyTQcwebSurveyInfo() {
            assertForeign("tQcwebSurveyInfo");
            if (_tQcwebSurveyInfo == null) {
                _tQcwebSurveyInfo = new TQcwebSurveyInfoCBSpecification(_baseCB, new TQcwebSurveyInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tQcwebSurveyInfo.xsetSyncQyCall(new TQcwebSurveyInfoSpQyCall(xsyncQyCall())); }
            }
            return _tQcwebSurveyInfo;
        }
		public class TQcwebSurveyInfoSpQyCall : HpSpQyCall<TQcwebSurveyInfoCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TQcwebSurveyInfoSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTQcwebSurveyInfo(); }
			public TQcwebSurveyInfoCQ qy() { return _qyCall.qy().QueryTQcwebSurveyInfo(); }
		}
        public TMatrixInfoCBSpecification SpecifyTMatrixInfo() {
            assertForeign("tMatrixInfo");
            if (_tMatrixInfo == null) {
                _tMatrixInfo = new TMatrixInfoCBSpecification(_baseCB, new TMatrixInfoSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tMatrixInfo.xsetSyncQyCall(new TMatrixInfoSpQyCall(xsyncQyCall())); }
            }
            return _tMatrixInfo;
        }
		public class TMatrixInfoSpQyCall : HpSpQyCall<TMatrixInfoCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TMatrixInfoSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTMatrixInfo(); }
			public TMatrixInfoCQ qy() { return _qyCall.qy().QueryTMatrixInfo(); }
		}
        public TFaListAddItemCBSpecification SpecifyTFaListAddItem() {
            assertForeign("tFaListAddItem");
            if (_tFaListAddItem == null) {
                _tFaListAddItem = new TFaListAddItemCBSpecification(_baseCB, new TFaListAddItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tFaListAddItem.xsetSyncQyCall(new TFaListAddItemSpQyCall(xsyncQyCall())); }
            }
            return _tFaListAddItem;
        }
		public class TFaListAddItemSpQyCall : HpSpQyCall<TFaListAddItemCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TFaListAddItemSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTFaListAddItem(); }
			public TFaListAddItemCQ qy() { return _qyCall.qy().QueryTFaListAddItem(); }
		}
        public TFaScenarioItemCBSpecification SpecifyTFaScenarioItem() {
            assertForeign("tFaScenarioItem");
            if (_tFaScenarioItem == null) {
                _tFaScenarioItem = new TFaScenarioItemCBSpecification(_baseCB, new TFaScenarioItemSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tFaScenarioItem.xsetSyncQyCall(new TFaScenarioItemSpQyCall(xsyncQyCall())); }
            }
            return _tFaScenarioItem;
        }
		public class TFaScenarioItemSpQyCall : HpSpQyCall<TFaScenarioItemCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TFaScenarioItemSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTFaScenarioItem(); }
			public TFaScenarioItemCQ qy() { return _qyCall.qy().QueryTFaScenarioItem(); }
		}
        public TTableControlCBSpecification SpecifyTTableControl() {
            assertForeign("tTableControl");
            if (_tTableControl == null) {
                _tTableControl = new TTableControlCBSpecification(_baseCB, new TTableControlSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tTableControl.xsetSyncQyCall(new TTableControlSpQyCall(xsyncQyCall())); }
            }
            return _tTableControl;
        }
		public class TTableControlSpQyCall : HpSpQyCall<TTableControlCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TTableControlSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTTableControl(); }
			public TTableControlCQ qy() { return _qyCall.qy().QueryTTableControl(); }
		}
        public TScenarioTotalizationCBSpecification SpecifyTScenarioTotalization() {
            assertForeign("tScenarioTotalization");
            if (_tScenarioTotalization == null) {
                _tScenarioTotalization = new TScenarioTotalizationCBSpecification(_baseCB, new TScenarioTotalizationSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tScenarioTotalization.xsetSyncQyCall(new TScenarioTotalizationSpQyCall(xsyncQyCall())); }
            }
            return _tScenarioTotalization;
        }
		public class TScenarioTotalizationSpQyCall : HpSpQyCall<TScenarioTotalizationCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TScenarioTotalizationSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTScenarioTotalization(); }
			public TScenarioTotalizationCQ qy() { return _qyCall.qy().QueryTScenarioTotalization(); }
		}
        public TDataEditListCBSpecification SpecifyTDataEditList() {
            assertForeign("tDataEditList");
            if (_tDataEditList == null) {
                _tDataEditList = new TDataEditListCBSpecification(_baseCB, new TDataEditListSpQyCall(_qyCall), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery);
                if (xhasSyncQyCall()) // inherits it
                { _tDataEditList.xsetSyncQyCall(new TDataEditListSpQyCall(xsyncQyCall())); }
            }
            return _tDataEditList;
        }
		public class TDataEditListSpQyCall : HpSpQyCall<TDataEditListCQ> {
		    protected HpSpQyCall<TItemInfoCQ> _qyCall;
		    public TDataEditListSpQyCall(HpSpQyCall<TItemInfoCQ> myQyCall) { _qyCall = myQyCall; }
		    public bool has() { return _qyCall.has() && _qyCall.qy().hasConditionQueryTDataEditList(); }
			public TDataEditListCQ qy() { return _qyCall.qy().QueryTDataEditList(); }
		}
        public RAFunction<TCategoryInfoCB, TItemInfoCQ> DerivedTCategoryInfoList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TCategoryInfoCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TCategoryInfoCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTCategoryInfoList(function, subQuery, aliasName); });
        }
        public RAFunction<TMatrixInfoCB, TItemInfoCQ> DerivedTMatrixInfoByItemInfoIdList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TMatrixInfoCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TMatrixInfoCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTMatrixInfoByItemInfoIdList(function, subQuery, aliasName); });
        }
        public RAFunction<TMatrixInfoCB, TItemInfoCQ> DerivedTMatrixInfoByChildItemInfoIdList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TMatrixInfoCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TMatrixInfoCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTMatrixInfoByChildItemInfoIdList(function, subQuery, aliasName); });
        }
        public RAFunction<TScenarioQuerylistCB, TItemInfoCQ> DerivedTScenarioQuerylistList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TScenarioQuerylistCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TScenarioQuerylistCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTScenarioQuerylistList(function, subQuery, aliasName); });
        }
        public RAFunction<TGtScenarioItemCB, TItemInfoCQ> DerivedTGtScenarioItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TGtScenarioItemCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TGtScenarioItemCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTGtScenarioItemList(function, subQuery, aliasName); });
        }
        public RAFunction<TFaScenarioItemCB, TItemInfoCQ> DerivedTFaScenarioItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TFaScenarioItemCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TFaScenarioItemCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTFaScenarioItemList(function, subQuery, aliasName); });
        }
        public RAFunction<TFaListAddItemCB, TItemInfoCQ> DerivedTFaListAddItemList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TFaListAddItemCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TFaListAddItemCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTFaListAddItemList(function, subQuery, aliasName); });
        }
        public RAFunction<TGtMatrixChildCB, TItemInfoCQ> DerivedTGtMatrixChildList() {
            if (xhasSyncQyCall()) { xsyncQyCall().qy(); } // for sync (for example, this in ColumnQuery)
            return new RAFunction<TGtMatrixChildCB, TItemInfoCQ>(_baseCB, _qyCall.qy(), delegate(String function, SubQuery<TGtMatrixChildCB> subQuery, TItemInfoCQ cq, String aliasName)
                { cq.xsderiveTGtMatrixChildList(function, subQuery, aliasName); });
        }
    }
}
