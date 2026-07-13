
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
    public class BsTFaDataCB : AbstractConditionBean {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected TFaDataCQ _conditionQuery;

        // ===============================================================================
        //                                                                      Table Name
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_DATA"; } }

        // ===============================================================================
        //                                                             PrimaryKey Handling
        //                                                             ===================
        public void AcceptPrimaryKey(String sampleId) {
            assertObjectNotNull("sampleId", sampleId);
            BsTFaDataCB cb = this;
            cb.Query().SetSampleId_Equal(sampleId);
        }

        public override ConditionBean AddOrderBy_PK_Asc() {
            Query().AddOrderBy_SampleId_Asc();
            return this;
        }

        public override ConditionBean AddOrderBy_PK_Desc() {
            Query().AddOrderBy_SampleId_Desc();
            return this;
        }

        // ===============================================================================
        //                                                                           Query
        //                                                                           =====
        public TFaDataCQ Query() {
            return this.ConditionQuery;
        }

        public TFaDataCQ ConditionQuery {
            get {
                if (_conditionQuery == null) {
                    _conditionQuery = CreateLocalCQ();
                }
                return _conditionQuery;
            }
        }

        protected virtual TFaDataCQ CreateLocalCQ() {
            return xcreateCQ(null, this.SqlClause, this.SqlClause.getBasePointAliasName(), 0);
        }

        protected virtual TFaDataCQ xcreateCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel) {
            return new TFaDataCQ(childQuery, sqlClause, aliasName, nestLevel);
        }

        public override ConditionQuery LocalCQ {
            get { return this.ConditionQuery; }
        }

        // ===============================================================================
        //                                                                           Union
        //                                                                           =====
	    public virtual void Union(UnionQuery<TFaDataCB> unionQuery) {
            TFaDataCB cb = new TFaDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TFaDataCQ cq = cb.Query(); Query().xsetUnionQuery(cq);
        }

	    public virtual void UnionAll(UnionQuery<TFaDataCB> unionQuery) {
            TFaDataCB cb = new TFaDataCB();
            cb.xsetupForUnion(this); xsyncUQ(cb); unionQuery.Invoke(cb);
		    TFaDataCQ cq = cb.Query(); Query().xsetUnionAllQuery(cq);
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

        // [DBFlute-0.7.4]
        // ===============================================================================
        //                                                                         Specify
        //                                                                         =======
        protected TFaDataCBSpecification _specification;
        public TFaDataCBSpecification Specify() {
            if (_specification == null) { _specification = new TFaDataCBSpecification(this, new MySpQyCall(this), _forDerivedReferrer, _forScalarSelect, _forScalarCondition, _forColumnQuery); }
            return _specification;
        }
        protected bool HasSpecifiedColumn { get {
            return _specification != null && _specification.IsAlreadySpecifiedRequiredColumn;
        }}
        protected class MySpQyCall : HpSpQyCall<TFaDataCQ> {
			protected BsTFaDataCB _myCB;
			public MySpQyCall(BsTFaDataCB myCB) { _myCB = myCB; }
    		public bool has() { return true; } public TFaDataCQ qy() { return _myCB.Query(); }
    	}

        // [DBFlute-0.8.9.18]
        // ===============================================================================
        //                                                                     ColumnQuery
        //                                                                     ===========
        public HpColQyOperand<TFaDataCB> ColumnQuery(SpecifyQuery<TFaDataCB> leftSpecifyQuery) {
            return new HpColQyOperand<TFaDataCB>(delegate(SpecifyQuery<TFaDataCB> rightSp, String operand) {
                xcolqy(xcreateColumnQueryCB(), xcreateColumnQueryCB(), leftSpecifyQuery, rightSp, operand);
            });
        }

        protected TFaDataCB xcreateColumnQueryCB() {
            TFaDataCB cb = new TFaDataCB();
            cb.xsetupForColumnQuery((TFaDataCB)this);
            return cb;
        }

        // [DBFlute-0.8.9.9]
        // ===============================================================================
        //                                                                    OrScopeQuery
        //                                                                    ============
        public void OrScopeQuery(OrQuery<TFaDataCB> orQuery) {
            xorQ((TFaDataCB)this, orQuery);
        }

        // ===============================================================================
        //                                                                    Purpose Type
        //                                                                    ============
        public void xsetupForColumnQuery(TFaDataCB mainCB) {
            xinheritSubQueryInfo(mainCB.LocalCQ);
            //xchangePurposeSqlClause(HpCBPurpose.COLUMN_QUERY);
            _forColumnQuery = true; // old style

            // inherits a parent query to synchronize real name
            // (and also for suppressing query check) 
            Specify().xsetSyncQyCall(new TFaDataCBColQySpQyCall(mainCB));
        }
    }

    public class TFaDataCBColQySpQyCall : HpSpQyCall<TFaDataCQ> {
        protected TFaDataCB _mainCB;
        public TFaDataCBColQySpQyCall(TFaDataCB mainCB) {
            _mainCB = mainCB;
        }
        public bool has() { return true; } 
        public TFaDataCQ qy() { return _mainCB.Query(); } 
    }

    public class TFaDataCBSpecification : AbstractSpecification<TFaDataCQ> {
        public TFaDataCBSpecification(ConditionBean baseCB, HpSpQyCall<TFaDataCQ> qyCall
                                                      , bool forDerivedReferrer, bool forScalarSelect, bool forScalarSubQuery, bool forColumnQuery)
        : base(baseCB, qyCall, forDerivedReferrer, forScalarSelect, forScalarSubQuery, forColumnQuery) { }
        public void ColumnSampleId() { doColumn("SAMPLE_ID"); }
        public void ColumnFa001() { doColumn("FA001"); }
        public void ColumnFa002() { doColumn("FA002"); }
        public void ColumnFa003() { doColumn("FA003"); }
        public void ColumnFa004() { doColumn("FA004"); }
        public void ColumnFa005() { doColumn("FA005"); }
        public void ColumnFa006() { doColumn("FA006"); }
        public void ColumnFa007() { doColumn("FA007"); }
        public void ColumnFa008() { doColumn("FA008"); }
        public void ColumnFa009() { doColumn("FA009"); }
        public void ColumnFa010() { doColumn("FA010"); }
        public void ColumnFa011() { doColumn("FA011"); }
        public void ColumnFa012() { doColumn("FA012"); }
        public void ColumnFa013() { doColumn("FA013"); }
        public void ColumnFa014() { doColumn("FA014"); }
        public void ColumnFa015() { doColumn("FA015"); }
        public void ColumnFa016() { doColumn("FA016"); }
        public void ColumnFa017() { doColumn("FA017"); }
        public void ColumnFa018() { doColumn("FA018"); }
        public void ColumnFa019() { doColumn("FA019"); }
        public void ColumnFa020() { doColumn("FA020"); }
        public void ColumnFa021() { doColumn("FA021"); }
        public void ColumnFa022() { doColumn("FA022"); }
        public void ColumnFa023() { doColumn("FA023"); }
        public void ColumnFa024() { doColumn("FA024"); }
        public void ColumnFa025() { doColumn("FA025"); }
        public void ColumnFa026() { doColumn("FA026"); }
        public void ColumnFa027() { doColumn("FA027"); }
        public void ColumnFa028() { doColumn("FA028"); }
        public void ColumnFa029() { doColumn("FA029"); }
        public void ColumnFa030() { doColumn("FA030"); }
        public void ColumnFa031() { doColumn("FA031"); }
        public void ColumnFa032() { doColumn("FA032"); }
        public void ColumnFa033() { doColumn("FA033"); }
        public void ColumnFa034() { doColumn("FA034"); }
        public void ColumnFa035() { doColumn("FA035"); }
        public void ColumnFa036() { doColumn("FA036"); }
        public void ColumnFa037() { doColumn("FA037"); }
        public void ColumnFa038() { doColumn("FA038"); }
        public void ColumnFa039() { doColumn("FA039"); }
        public void ColumnFa040() { doColumn("FA040"); }
        public void ColumnFa041() { doColumn("FA041"); }
        public void ColumnFa042() { doColumn("FA042"); }
        public void ColumnFa043() { doColumn("FA043"); }
        public void ColumnFa044() { doColumn("FA044"); }
        public void ColumnFa045() { doColumn("FA045"); }
        public void ColumnFa046() { doColumn("FA046"); }
        public void ColumnFa047() { doColumn("FA047"); }
        public void ColumnFa048() { doColumn("FA048"); }
        public void ColumnFa049() { doColumn("FA049"); }
        public void ColumnFa050() { doColumn("FA050"); }
        public void ColumnFa051() { doColumn("FA051"); }
        public void ColumnFa052() { doColumn("FA052"); }
        public void ColumnFa053() { doColumn("FA053"); }
        public void ColumnFa054() { doColumn("FA054"); }
        public void ColumnFa055() { doColumn("FA055"); }
        public void ColumnFa056() { doColumn("FA056"); }
        public void ColumnFa057() { doColumn("FA057"); }
        public void ColumnFa058() { doColumn("FA058"); }
        public void ColumnFa059() { doColumn("FA059"); }
        public void ColumnFa060() { doColumn("FA060"); }
        public void ColumnFa061() { doColumn("FA061"); }
        public void ColumnFa062() { doColumn("FA062"); }
        public void ColumnFa063() { doColumn("FA063"); }
        public void ColumnFa064() { doColumn("FA064"); }
        public void ColumnFa065() { doColumn("FA065"); }
        public void ColumnFa066() { doColumn("FA066"); }
        public void ColumnFa067() { doColumn("FA067"); }
        public void ColumnFa068() { doColumn("FA068"); }
        public void ColumnFa069() { doColumn("FA069"); }
        public void ColumnFa070() { doColumn("FA070"); }
        public void ColumnFa071() { doColumn("FA071"); }
        public void ColumnFa072() { doColumn("FA072"); }
        public void ColumnFa073() { doColumn("FA073"); }
        public void ColumnFa074() { doColumn("FA074"); }
        public void ColumnFa075() { doColumn("FA075"); }
        public void ColumnFa076() { doColumn("FA076"); }
        public void ColumnFa077() { doColumn("FA077"); }
        public void ColumnFa078() { doColumn("FA078"); }
        public void ColumnFa079() { doColumn("FA079"); }
        public void ColumnFa080() { doColumn("FA080"); }
        public void ColumnFa081() { doColumn("FA081"); }
        public void ColumnFa082() { doColumn("FA082"); }
        public void ColumnFa083() { doColumn("FA083"); }
        public void ColumnFa084() { doColumn("FA084"); }
        public void ColumnFa085() { doColumn("FA085"); }
        public void ColumnFa086() { doColumn("FA086"); }
        public void ColumnFa087() { doColumn("FA087"); }
        public void ColumnFa088() { doColumn("FA088"); }
        public void ColumnFa089() { doColumn("FA089"); }
        public void ColumnFa090() { doColumn("FA090"); }
        public void ColumnFa091() { doColumn("FA091"); }
        public void ColumnFa092() { doColumn("FA092"); }
        public void ColumnFa093() { doColumn("FA093"); }
        public void ColumnFa094() { doColumn("FA094"); }
        public void ColumnFa095() { doColumn("FA095"); }
        public void ColumnFa096() { doColumn("FA096"); }
        public void ColumnFa097() { doColumn("FA097"); }
        public void ColumnFa098() { doColumn("FA098"); }
        public void ColumnFa099() { doColumn("FA099"); }
        public void ColumnFa100() { doColumn("FA100"); }
        public void ColumnFa101() { doColumn("FA101"); }
        public void ColumnFa102() { doColumn("FA102"); }
        public void ColumnFa103() { doColumn("FA103"); }
        public void ColumnFa104() { doColumn("FA104"); }
        public void ColumnFa105() { doColumn("FA105"); }
        public void ColumnFa106() { doColumn("FA106"); }
        public void ColumnFa107() { doColumn("FA107"); }
        public void ColumnFa108() { doColumn("FA108"); }
        public void ColumnFa109() { doColumn("FA109"); }
        public void ColumnFa110() { doColumn("FA110"); }
        public void ColumnFa111() { doColumn("FA111"); }
        public void ColumnFa112() { doColumn("FA112"); }
        public void ColumnFa113() { doColumn("FA113"); }
        public void ColumnFa114() { doColumn("FA114"); }
        public void ColumnFa115() { doColumn("FA115"); }
        public void ColumnFa116() { doColumn("FA116"); }
        public void ColumnFa117() { doColumn("FA117"); }
        public void ColumnFa118() { doColumn("FA118"); }
        public void ColumnFa119() { doColumn("FA119"); }
        public void ColumnFa120() { doColumn("FA120"); }
        public void ColumnFa121() { doColumn("FA121"); }
        public void ColumnFa122() { doColumn("FA122"); }
        public void ColumnFa123() { doColumn("FA123"); }
        public void ColumnFa124() { doColumn("FA124"); }
        public void ColumnFa125() { doColumn("FA125"); }
        public void ColumnFa126() { doColumn("FA126"); }
        public void ColumnFa127() { doColumn("FA127"); }
        public void ColumnFa128() { doColumn("FA128"); }
        public void ColumnFa129() { doColumn("FA129"); }
        public void ColumnFa130() { doColumn("FA130"); }
        public void ColumnFa131() { doColumn("FA131"); }
        public void ColumnFa132() { doColumn("FA132"); }
        public void ColumnFa133() { doColumn("FA133"); }
        public void ColumnFa134() { doColumn("FA134"); }
        public void ColumnFa135() { doColumn("FA135"); }
        public void ColumnFa136() { doColumn("FA136"); }
        public void ColumnFa137() { doColumn("FA137"); }
        public void ColumnFa138() { doColumn("FA138"); }
        public void ColumnFa139() { doColumn("FA139"); }
        public void ColumnFa140() { doColumn("FA140"); }
        public void ColumnFa141() { doColumn("FA141"); }
        public void ColumnFa142() { doColumn("FA142"); }
        public void ColumnFa143() { doColumn("FA143"); }
        public void ColumnFa144() { doColumn("FA144"); }
        public void ColumnFa145() { doColumn("FA145"); }
        public void ColumnFa146() { doColumn("FA146"); }
        public void ColumnFa147() { doColumn("FA147"); }
        public void ColumnFa148() { doColumn("FA148"); }
        public void ColumnFa149() { doColumn("FA149"); }
        public void ColumnFa150() { doColumn("FA150"); }
        public void ColumnFa151() { doColumn("FA151"); }
        public void ColumnFa152() { doColumn("FA152"); }
        public void ColumnFa153() { doColumn("FA153"); }
        public void ColumnFa154() { doColumn("FA154"); }
        public void ColumnFa155() { doColumn("FA155"); }
        public void ColumnFa156() { doColumn("FA156"); }
        public void ColumnFa157() { doColumn("FA157"); }
        public void ColumnFa158() { doColumn("FA158"); }
        public void ColumnFa159() { doColumn("FA159"); }
        public void ColumnFa160() { doColumn("FA160"); }
        public void ColumnFa161() { doColumn("FA161"); }
        public void ColumnFa162() { doColumn("FA162"); }
        public void ColumnFa163() { doColumn("FA163"); }
        public void ColumnFa164() { doColumn("FA164"); }
        public void ColumnFa165() { doColumn("FA165"); }
        public void ColumnFa166() { doColumn("FA166"); }
        public void ColumnFa167() { doColumn("FA167"); }
        public void ColumnFa168() { doColumn("FA168"); }
        public void ColumnFa169() { doColumn("FA169"); }
        public void ColumnFa170() { doColumn("FA170"); }
        public void ColumnFa171() { doColumn("FA171"); }
        public void ColumnFa172() { doColumn("FA172"); }
        public void ColumnFa173() { doColumn("FA173"); }
        public void ColumnFa174() { doColumn("FA174"); }
        public void ColumnFa175() { doColumn("FA175"); }
        public void ColumnFa176() { doColumn("FA176"); }
        public void ColumnFa177() { doColumn("FA177"); }
        public void ColumnFa178() { doColumn("FA178"); }
        public void ColumnFa179() { doColumn("FA179"); }
        public void ColumnFa180() { doColumn("FA180"); }
        public void ColumnFa181() { doColumn("FA181"); }
        public void ColumnFa182() { doColumn("FA182"); }
        public void ColumnFa183() { doColumn("FA183"); }
        public void ColumnFa184() { doColumn("FA184"); }
        public void ColumnFa185() { doColumn("FA185"); }
        public void ColumnFa186() { doColumn("FA186"); }
        public void ColumnFa187() { doColumn("FA187"); }
        public void ColumnFa188() { doColumn("FA188"); }
        public void ColumnFa189() { doColumn("FA189"); }
        public void ColumnFa190() { doColumn("FA190"); }
        public void ColumnFa191() { doColumn("FA191"); }
        public void ColumnFa192() { doColumn("FA192"); }
        public void ColumnFa193() { doColumn("FA193"); }
        public void ColumnFa194() { doColumn("FA194"); }
        public void ColumnFa195() { doColumn("FA195"); }
        public void ColumnFa196() { doColumn("FA196"); }
        public void ColumnFa197() { doColumn("FA197"); }
        public void ColumnFa198() { doColumn("FA198"); }
        public void ColumnFa199() { doColumn("FA199"); }
        public void ColumnFa200() { doColumn("FA200"); }
        protected override void doSpecifyRequiredColumn() {
            ColumnSampleId(); // PK
        }
        protected override String getTableDbName() { return "T_FA_DATA"; }
    }
}
