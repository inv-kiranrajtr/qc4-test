
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ;
using Macromill.QCWeb.Dao.CBean.CQ.Ciq;

namespace Macromill.QCWeb.Dao.CBean.CQ.BS {

    [System.Serializable]
    public class BsTFaDataCQ : AbstractBsTFaDataCQ {

        protected TFaDataCIQ _inlineQuery;

        public BsTFaDataCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        public TFaDataCIQ Inline() {
            if (_inlineQuery == null) {
                _inlineQuery = new TFaDataCIQ(xgetReferrerQuery(), xgetSqlClause(), xgetAliasName(), xgetNestLevel(), this);
            }
            _inlineQuery.xsetOnClause(false);
            return _inlineQuery;
        }
        
        public TFaDataCIQ On() {
            if (isBaseQuery()) { throw new UnsupportedOperationException("Unsupported onClause of Base Table!"); }
            TFaDataCIQ inlineQuery = Inline();
            inlineQuery.xsetOnClause(true);
            return inlineQuery;
        }


        protected ConditionValue _sampleId;
        public ConditionValue SampleId {
            get { if (_sampleId == null) { _sampleId = new ConditionValue(); } return _sampleId; }
        }
        protected override ConditionValue getCValueSampleId() { return this.SampleId; }


        public BsTFaDataCQ AddOrderBy_SampleId_Asc() { regOBA("SAMPLE_ID");return this; }
        public BsTFaDataCQ AddOrderBy_SampleId_Desc() { regOBD("SAMPLE_ID");return this; }

        protected ConditionValue _fa001;
        public ConditionValue Fa001 {
            get { if (_fa001 == null) { _fa001 = new ConditionValue(); } return _fa001; }
        }
        protected override ConditionValue getCValueFa001() { return this.Fa001; }


        public BsTFaDataCQ AddOrderBy_Fa001_Asc() { regOBA("FA001");return this; }
        public BsTFaDataCQ AddOrderBy_Fa001_Desc() { regOBD("FA001");return this; }

        protected ConditionValue _fa002;
        public ConditionValue Fa002 {
            get { if (_fa002 == null) { _fa002 = new ConditionValue(); } return _fa002; }
        }
        protected override ConditionValue getCValueFa002() { return this.Fa002; }


        public BsTFaDataCQ AddOrderBy_Fa002_Asc() { regOBA("FA002");return this; }
        public BsTFaDataCQ AddOrderBy_Fa002_Desc() { regOBD("FA002");return this; }

        protected ConditionValue _fa003;
        public ConditionValue Fa003 {
            get { if (_fa003 == null) { _fa003 = new ConditionValue(); } return _fa003; }
        }
        protected override ConditionValue getCValueFa003() { return this.Fa003; }


        public BsTFaDataCQ AddOrderBy_Fa003_Asc() { regOBA("FA003");return this; }
        public BsTFaDataCQ AddOrderBy_Fa003_Desc() { regOBD("FA003");return this; }

        protected ConditionValue _fa004;
        public ConditionValue Fa004 {
            get { if (_fa004 == null) { _fa004 = new ConditionValue(); } return _fa004; }
        }
        protected override ConditionValue getCValueFa004() { return this.Fa004; }


        public BsTFaDataCQ AddOrderBy_Fa004_Asc() { regOBA("FA004");return this; }
        public BsTFaDataCQ AddOrderBy_Fa004_Desc() { regOBD("FA004");return this; }

        protected ConditionValue _fa005;
        public ConditionValue Fa005 {
            get { if (_fa005 == null) { _fa005 = new ConditionValue(); } return _fa005; }
        }
        protected override ConditionValue getCValueFa005() { return this.Fa005; }


        public BsTFaDataCQ AddOrderBy_Fa005_Asc() { regOBA("FA005");return this; }
        public BsTFaDataCQ AddOrderBy_Fa005_Desc() { regOBD("FA005");return this; }

        protected ConditionValue _fa006;
        public ConditionValue Fa006 {
            get { if (_fa006 == null) { _fa006 = new ConditionValue(); } return _fa006; }
        }
        protected override ConditionValue getCValueFa006() { return this.Fa006; }


        public BsTFaDataCQ AddOrderBy_Fa006_Asc() { regOBA("FA006");return this; }
        public BsTFaDataCQ AddOrderBy_Fa006_Desc() { regOBD("FA006");return this; }

        protected ConditionValue _fa007;
        public ConditionValue Fa007 {
            get { if (_fa007 == null) { _fa007 = new ConditionValue(); } return _fa007; }
        }
        protected override ConditionValue getCValueFa007() { return this.Fa007; }


        public BsTFaDataCQ AddOrderBy_Fa007_Asc() { regOBA("FA007");return this; }
        public BsTFaDataCQ AddOrderBy_Fa007_Desc() { regOBD("FA007");return this; }

        protected ConditionValue _fa008;
        public ConditionValue Fa008 {
            get { if (_fa008 == null) { _fa008 = new ConditionValue(); } return _fa008; }
        }
        protected override ConditionValue getCValueFa008() { return this.Fa008; }


        public BsTFaDataCQ AddOrderBy_Fa008_Asc() { regOBA("FA008");return this; }
        public BsTFaDataCQ AddOrderBy_Fa008_Desc() { regOBD("FA008");return this; }

        protected ConditionValue _fa009;
        public ConditionValue Fa009 {
            get { if (_fa009 == null) { _fa009 = new ConditionValue(); } return _fa009; }
        }
        protected override ConditionValue getCValueFa009() { return this.Fa009; }


        public BsTFaDataCQ AddOrderBy_Fa009_Asc() { regOBA("FA009");return this; }
        public BsTFaDataCQ AddOrderBy_Fa009_Desc() { regOBD("FA009");return this; }

        protected ConditionValue _fa010;
        public ConditionValue Fa010 {
            get { if (_fa010 == null) { _fa010 = new ConditionValue(); } return _fa010; }
        }
        protected override ConditionValue getCValueFa010() { return this.Fa010; }


        public BsTFaDataCQ AddOrderBy_Fa010_Asc() { regOBA("FA010");return this; }
        public BsTFaDataCQ AddOrderBy_Fa010_Desc() { regOBD("FA010");return this; }

        protected ConditionValue _fa011;
        public ConditionValue Fa011 {
            get { if (_fa011 == null) { _fa011 = new ConditionValue(); } return _fa011; }
        }
        protected override ConditionValue getCValueFa011() { return this.Fa011; }


        public BsTFaDataCQ AddOrderBy_Fa011_Asc() { regOBA("FA011");return this; }
        public BsTFaDataCQ AddOrderBy_Fa011_Desc() { regOBD("FA011");return this; }

        protected ConditionValue _fa012;
        public ConditionValue Fa012 {
            get { if (_fa012 == null) { _fa012 = new ConditionValue(); } return _fa012; }
        }
        protected override ConditionValue getCValueFa012() { return this.Fa012; }


        public BsTFaDataCQ AddOrderBy_Fa012_Asc() { regOBA("FA012");return this; }
        public BsTFaDataCQ AddOrderBy_Fa012_Desc() { regOBD("FA012");return this; }

        protected ConditionValue _fa013;
        public ConditionValue Fa013 {
            get { if (_fa013 == null) { _fa013 = new ConditionValue(); } return _fa013; }
        }
        protected override ConditionValue getCValueFa013() { return this.Fa013; }


        public BsTFaDataCQ AddOrderBy_Fa013_Asc() { regOBA("FA013");return this; }
        public BsTFaDataCQ AddOrderBy_Fa013_Desc() { regOBD("FA013");return this; }

        protected ConditionValue _fa014;
        public ConditionValue Fa014 {
            get { if (_fa014 == null) { _fa014 = new ConditionValue(); } return _fa014; }
        }
        protected override ConditionValue getCValueFa014() { return this.Fa014; }


        public BsTFaDataCQ AddOrderBy_Fa014_Asc() { regOBA("FA014");return this; }
        public BsTFaDataCQ AddOrderBy_Fa014_Desc() { regOBD("FA014");return this; }

        protected ConditionValue _fa015;
        public ConditionValue Fa015 {
            get { if (_fa015 == null) { _fa015 = new ConditionValue(); } return _fa015; }
        }
        protected override ConditionValue getCValueFa015() { return this.Fa015; }


        public BsTFaDataCQ AddOrderBy_Fa015_Asc() { regOBA("FA015");return this; }
        public BsTFaDataCQ AddOrderBy_Fa015_Desc() { regOBD("FA015");return this; }

        protected ConditionValue _fa016;
        public ConditionValue Fa016 {
            get { if (_fa016 == null) { _fa016 = new ConditionValue(); } return _fa016; }
        }
        protected override ConditionValue getCValueFa016() { return this.Fa016; }


        public BsTFaDataCQ AddOrderBy_Fa016_Asc() { regOBA("FA016");return this; }
        public BsTFaDataCQ AddOrderBy_Fa016_Desc() { regOBD("FA016");return this; }

        protected ConditionValue _fa017;
        public ConditionValue Fa017 {
            get { if (_fa017 == null) { _fa017 = new ConditionValue(); } return _fa017; }
        }
        protected override ConditionValue getCValueFa017() { return this.Fa017; }


        public BsTFaDataCQ AddOrderBy_Fa017_Asc() { regOBA("FA017");return this; }
        public BsTFaDataCQ AddOrderBy_Fa017_Desc() { regOBD("FA017");return this; }

        protected ConditionValue _fa018;
        public ConditionValue Fa018 {
            get { if (_fa018 == null) { _fa018 = new ConditionValue(); } return _fa018; }
        }
        protected override ConditionValue getCValueFa018() { return this.Fa018; }


        public BsTFaDataCQ AddOrderBy_Fa018_Asc() { regOBA("FA018");return this; }
        public BsTFaDataCQ AddOrderBy_Fa018_Desc() { regOBD("FA018");return this; }

        protected ConditionValue _fa019;
        public ConditionValue Fa019 {
            get { if (_fa019 == null) { _fa019 = new ConditionValue(); } return _fa019; }
        }
        protected override ConditionValue getCValueFa019() { return this.Fa019; }


        public BsTFaDataCQ AddOrderBy_Fa019_Asc() { regOBA("FA019");return this; }
        public BsTFaDataCQ AddOrderBy_Fa019_Desc() { regOBD("FA019");return this; }

        protected ConditionValue _fa020;
        public ConditionValue Fa020 {
            get { if (_fa020 == null) { _fa020 = new ConditionValue(); } return _fa020; }
        }
        protected override ConditionValue getCValueFa020() { return this.Fa020; }


        public BsTFaDataCQ AddOrderBy_Fa020_Asc() { regOBA("FA020");return this; }
        public BsTFaDataCQ AddOrderBy_Fa020_Desc() { regOBD("FA020");return this; }

        protected ConditionValue _fa021;
        public ConditionValue Fa021 {
            get { if (_fa021 == null) { _fa021 = new ConditionValue(); } return _fa021; }
        }
        protected override ConditionValue getCValueFa021() { return this.Fa021; }


        public BsTFaDataCQ AddOrderBy_Fa021_Asc() { regOBA("FA021");return this; }
        public BsTFaDataCQ AddOrderBy_Fa021_Desc() { regOBD("FA021");return this; }

        protected ConditionValue _fa022;
        public ConditionValue Fa022 {
            get { if (_fa022 == null) { _fa022 = new ConditionValue(); } return _fa022; }
        }
        protected override ConditionValue getCValueFa022() { return this.Fa022; }


        public BsTFaDataCQ AddOrderBy_Fa022_Asc() { regOBA("FA022");return this; }
        public BsTFaDataCQ AddOrderBy_Fa022_Desc() { regOBD("FA022");return this; }

        protected ConditionValue _fa023;
        public ConditionValue Fa023 {
            get { if (_fa023 == null) { _fa023 = new ConditionValue(); } return _fa023; }
        }
        protected override ConditionValue getCValueFa023() { return this.Fa023; }


        public BsTFaDataCQ AddOrderBy_Fa023_Asc() { regOBA("FA023");return this; }
        public BsTFaDataCQ AddOrderBy_Fa023_Desc() { regOBD("FA023");return this; }

        protected ConditionValue _fa024;
        public ConditionValue Fa024 {
            get { if (_fa024 == null) { _fa024 = new ConditionValue(); } return _fa024; }
        }
        protected override ConditionValue getCValueFa024() { return this.Fa024; }


        public BsTFaDataCQ AddOrderBy_Fa024_Asc() { regOBA("FA024");return this; }
        public BsTFaDataCQ AddOrderBy_Fa024_Desc() { regOBD("FA024");return this; }

        protected ConditionValue _fa025;
        public ConditionValue Fa025 {
            get { if (_fa025 == null) { _fa025 = new ConditionValue(); } return _fa025; }
        }
        protected override ConditionValue getCValueFa025() { return this.Fa025; }


        public BsTFaDataCQ AddOrderBy_Fa025_Asc() { regOBA("FA025");return this; }
        public BsTFaDataCQ AddOrderBy_Fa025_Desc() { regOBD("FA025");return this; }

        protected ConditionValue _fa026;
        public ConditionValue Fa026 {
            get { if (_fa026 == null) { _fa026 = new ConditionValue(); } return _fa026; }
        }
        protected override ConditionValue getCValueFa026() { return this.Fa026; }


        public BsTFaDataCQ AddOrderBy_Fa026_Asc() { regOBA("FA026");return this; }
        public BsTFaDataCQ AddOrderBy_Fa026_Desc() { regOBD("FA026");return this; }

        protected ConditionValue _fa027;
        public ConditionValue Fa027 {
            get { if (_fa027 == null) { _fa027 = new ConditionValue(); } return _fa027; }
        }
        protected override ConditionValue getCValueFa027() { return this.Fa027; }


        public BsTFaDataCQ AddOrderBy_Fa027_Asc() { regOBA("FA027");return this; }
        public BsTFaDataCQ AddOrderBy_Fa027_Desc() { regOBD("FA027");return this; }

        protected ConditionValue _fa028;
        public ConditionValue Fa028 {
            get { if (_fa028 == null) { _fa028 = new ConditionValue(); } return _fa028; }
        }
        protected override ConditionValue getCValueFa028() { return this.Fa028; }


        public BsTFaDataCQ AddOrderBy_Fa028_Asc() { regOBA("FA028");return this; }
        public BsTFaDataCQ AddOrderBy_Fa028_Desc() { regOBD("FA028");return this; }

        protected ConditionValue _fa029;
        public ConditionValue Fa029 {
            get { if (_fa029 == null) { _fa029 = new ConditionValue(); } return _fa029; }
        }
        protected override ConditionValue getCValueFa029() { return this.Fa029; }


        public BsTFaDataCQ AddOrderBy_Fa029_Asc() { regOBA("FA029");return this; }
        public BsTFaDataCQ AddOrderBy_Fa029_Desc() { regOBD("FA029");return this; }

        protected ConditionValue _fa030;
        public ConditionValue Fa030 {
            get { if (_fa030 == null) { _fa030 = new ConditionValue(); } return _fa030; }
        }
        protected override ConditionValue getCValueFa030() { return this.Fa030; }


        public BsTFaDataCQ AddOrderBy_Fa030_Asc() { regOBA("FA030");return this; }
        public BsTFaDataCQ AddOrderBy_Fa030_Desc() { regOBD("FA030");return this; }

        protected ConditionValue _fa031;
        public ConditionValue Fa031 {
            get { if (_fa031 == null) { _fa031 = new ConditionValue(); } return _fa031; }
        }
        protected override ConditionValue getCValueFa031() { return this.Fa031; }


        public BsTFaDataCQ AddOrderBy_Fa031_Asc() { regOBA("FA031");return this; }
        public BsTFaDataCQ AddOrderBy_Fa031_Desc() { regOBD("FA031");return this; }

        protected ConditionValue _fa032;
        public ConditionValue Fa032 {
            get { if (_fa032 == null) { _fa032 = new ConditionValue(); } return _fa032; }
        }
        protected override ConditionValue getCValueFa032() { return this.Fa032; }


        public BsTFaDataCQ AddOrderBy_Fa032_Asc() { regOBA("FA032");return this; }
        public BsTFaDataCQ AddOrderBy_Fa032_Desc() { regOBD("FA032");return this; }

        protected ConditionValue _fa033;
        public ConditionValue Fa033 {
            get { if (_fa033 == null) { _fa033 = new ConditionValue(); } return _fa033; }
        }
        protected override ConditionValue getCValueFa033() { return this.Fa033; }


        public BsTFaDataCQ AddOrderBy_Fa033_Asc() { regOBA("FA033");return this; }
        public BsTFaDataCQ AddOrderBy_Fa033_Desc() { regOBD("FA033");return this; }

        protected ConditionValue _fa034;
        public ConditionValue Fa034 {
            get { if (_fa034 == null) { _fa034 = new ConditionValue(); } return _fa034; }
        }
        protected override ConditionValue getCValueFa034() { return this.Fa034; }


        public BsTFaDataCQ AddOrderBy_Fa034_Asc() { regOBA("FA034");return this; }
        public BsTFaDataCQ AddOrderBy_Fa034_Desc() { regOBD("FA034");return this; }

        protected ConditionValue _fa035;
        public ConditionValue Fa035 {
            get { if (_fa035 == null) { _fa035 = new ConditionValue(); } return _fa035; }
        }
        protected override ConditionValue getCValueFa035() { return this.Fa035; }


        public BsTFaDataCQ AddOrderBy_Fa035_Asc() { regOBA("FA035");return this; }
        public BsTFaDataCQ AddOrderBy_Fa035_Desc() { regOBD("FA035");return this; }

        protected ConditionValue _fa036;
        public ConditionValue Fa036 {
            get { if (_fa036 == null) { _fa036 = new ConditionValue(); } return _fa036; }
        }
        protected override ConditionValue getCValueFa036() { return this.Fa036; }


        public BsTFaDataCQ AddOrderBy_Fa036_Asc() { regOBA("FA036");return this; }
        public BsTFaDataCQ AddOrderBy_Fa036_Desc() { regOBD("FA036");return this; }

        protected ConditionValue _fa037;
        public ConditionValue Fa037 {
            get { if (_fa037 == null) { _fa037 = new ConditionValue(); } return _fa037; }
        }
        protected override ConditionValue getCValueFa037() { return this.Fa037; }


        public BsTFaDataCQ AddOrderBy_Fa037_Asc() { regOBA("FA037");return this; }
        public BsTFaDataCQ AddOrderBy_Fa037_Desc() { regOBD("FA037");return this; }

        protected ConditionValue _fa038;
        public ConditionValue Fa038 {
            get { if (_fa038 == null) { _fa038 = new ConditionValue(); } return _fa038; }
        }
        protected override ConditionValue getCValueFa038() { return this.Fa038; }


        public BsTFaDataCQ AddOrderBy_Fa038_Asc() { regOBA("FA038");return this; }
        public BsTFaDataCQ AddOrderBy_Fa038_Desc() { regOBD("FA038");return this; }

        protected ConditionValue _fa039;
        public ConditionValue Fa039 {
            get { if (_fa039 == null) { _fa039 = new ConditionValue(); } return _fa039; }
        }
        protected override ConditionValue getCValueFa039() { return this.Fa039; }


        public BsTFaDataCQ AddOrderBy_Fa039_Asc() { regOBA("FA039");return this; }
        public BsTFaDataCQ AddOrderBy_Fa039_Desc() { regOBD("FA039");return this; }

        protected ConditionValue _fa040;
        public ConditionValue Fa040 {
            get { if (_fa040 == null) { _fa040 = new ConditionValue(); } return _fa040; }
        }
        protected override ConditionValue getCValueFa040() { return this.Fa040; }


        public BsTFaDataCQ AddOrderBy_Fa040_Asc() { regOBA("FA040");return this; }
        public BsTFaDataCQ AddOrderBy_Fa040_Desc() { regOBD("FA040");return this; }

        protected ConditionValue _fa041;
        public ConditionValue Fa041 {
            get { if (_fa041 == null) { _fa041 = new ConditionValue(); } return _fa041; }
        }
        protected override ConditionValue getCValueFa041() { return this.Fa041; }


        public BsTFaDataCQ AddOrderBy_Fa041_Asc() { regOBA("FA041");return this; }
        public BsTFaDataCQ AddOrderBy_Fa041_Desc() { regOBD("FA041");return this; }

        protected ConditionValue _fa042;
        public ConditionValue Fa042 {
            get { if (_fa042 == null) { _fa042 = new ConditionValue(); } return _fa042; }
        }
        protected override ConditionValue getCValueFa042() { return this.Fa042; }


        public BsTFaDataCQ AddOrderBy_Fa042_Asc() { regOBA("FA042");return this; }
        public BsTFaDataCQ AddOrderBy_Fa042_Desc() { regOBD("FA042");return this; }

        protected ConditionValue _fa043;
        public ConditionValue Fa043 {
            get { if (_fa043 == null) { _fa043 = new ConditionValue(); } return _fa043; }
        }
        protected override ConditionValue getCValueFa043() { return this.Fa043; }


        public BsTFaDataCQ AddOrderBy_Fa043_Asc() { regOBA("FA043");return this; }
        public BsTFaDataCQ AddOrderBy_Fa043_Desc() { regOBD("FA043");return this; }

        protected ConditionValue _fa044;
        public ConditionValue Fa044 {
            get { if (_fa044 == null) { _fa044 = new ConditionValue(); } return _fa044; }
        }
        protected override ConditionValue getCValueFa044() { return this.Fa044; }


        public BsTFaDataCQ AddOrderBy_Fa044_Asc() { regOBA("FA044");return this; }
        public BsTFaDataCQ AddOrderBy_Fa044_Desc() { regOBD("FA044");return this; }

        protected ConditionValue _fa045;
        public ConditionValue Fa045 {
            get { if (_fa045 == null) { _fa045 = new ConditionValue(); } return _fa045; }
        }
        protected override ConditionValue getCValueFa045() { return this.Fa045; }


        public BsTFaDataCQ AddOrderBy_Fa045_Asc() { regOBA("FA045");return this; }
        public BsTFaDataCQ AddOrderBy_Fa045_Desc() { regOBD("FA045");return this; }

        protected ConditionValue _fa046;
        public ConditionValue Fa046 {
            get { if (_fa046 == null) { _fa046 = new ConditionValue(); } return _fa046; }
        }
        protected override ConditionValue getCValueFa046() { return this.Fa046; }


        public BsTFaDataCQ AddOrderBy_Fa046_Asc() { regOBA("FA046");return this; }
        public BsTFaDataCQ AddOrderBy_Fa046_Desc() { regOBD("FA046");return this; }

        protected ConditionValue _fa047;
        public ConditionValue Fa047 {
            get { if (_fa047 == null) { _fa047 = new ConditionValue(); } return _fa047; }
        }
        protected override ConditionValue getCValueFa047() { return this.Fa047; }


        public BsTFaDataCQ AddOrderBy_Fa047_Asc() { regOBA("FA047");return this; }
        public BsTFaDataCQ AddOrderBy_Fa047_Desc() { regOBD("FA047");return this; }

        protected ConditionValue _fa048;
        public ConditionValue Fa048 {
            get { if (_fa048 == null) { _fa048 = new ConditionValue(); } return _fa048; }
        }
        protected override ConditionValue getCValueFa048() { return this.Fa048; }


        public BsTFaDataCQ AddOrderBy_Fa048_Asc() { regOBA("FA048");return this; }
        public BsTFaDataCQ AddOrderBy_Fa048_Desc() { regOBD("FA048");return this; }

        protected ConditionValue _fa049;
        public ConditionValue Fa049 {
            get { if (_fa049 == null) { _fa049 = new ConditionValue(); } return _fa049; }
        }
        protected override ConditionValue getCValueFa049() { return this.Fa049; }


        public BsTFaDataCQ AddOrderBy_Fa049_Asc() { regOBA("FA049");return this; }
        public BsTFaDataCQ AddOrderBy_Fa049_Desc() { regOBD("FA049");return this; }

        protected ConditionValue _fa050;
        public ConditionValue Fa050 {
            get { if (_fa050 == null) { _fa050 = new ConditionValue(); } return _fa050; }
        }
        protected override ConditionValue getCValueFa050() { return this.Fa050; }


        public BsTFaDataCQ AddOrderBy_Fa050_Asc() { regOBA("FA050");return this; }
        public BsTFaDataCQ AddOrderBy_Fa050_Desc() { regOBD("FA050");return this; }

        protected ConditionValue _fa051;
        public ConditionValue Fa051 {
            get { if (_fa051 == null) { _fa051 = new ConditionValue(); } return _fa051; }
        }
        protected override ConditionValue getCValueFa051() { return this.Fa051; }


        public BsTFaDataCQ AddOrderBy_Fa051_Asc() { regOBA("FA051");return this; }
        public BsTFaDataCQ AddOrderBy_Fa051_Desc() { regOBD("FA051");return this; }

        protected ConditionValue _fa052;
        public ConditionValue Fa052 {
            get { if (_fa052 == null) { _fa052 = new ConditionValue(); } return _fa052; }
        }
        protected override ConditionValue getCValueFa052() { return this.Fa052; }


        public BsTFaDataCQ AddOrderBy_Fa052_Asc() { regOBA("FA052");return this; }
        public BsTFaDataCQ AddOrderBy_Fa052_Desc() { regOBD("FA052");return this; }

        protected ConditionValue _fa053;
        public ConditionValue Fa053 {
            get { if (_fa053 == null) { _fa053 = new ConditionValue(); } return _fa053; }
        }
        protected override ConditionValue getCValueFa053() { return this.Fa053; }


        public BsTFaDataCQ AddOrderBy_Fa053_Asc() { regOBA("FA053");return this; }
        public BsTFaDataCQ AddOrderBy_Fa053_Desc() { regOBD("FA053");return this; }

        protected ConditionValue _fa054;
        public ConditionValue Fa054 {
            get { if (_fa054 == null) { _fa054 = new ConditionValue(); } return _fa054; }
        }
        protected override ConditionValue getCValueFa054() { return this.Fa054; }


        public BsTFaDataCQ AddOrderBy_Fa054_Asc() { regOBA("FA054");return this; }
        public BsTFaDataCQ AddOrderBy_Fa054_Desc() { regOBD("FA054");return this; }

        protected ConditionValue _fa055;
        public ConditionValue Fa055 {
            get { if (_fa055 == null) { _fa055 = new ConditionValue(); } return _fa055; }
        }
        protected override ConditionValue getCValueFa055() { return this.Fa055; }


        public BsTFaDataCQ AddOrderBy_Fa055_Asc() { regOBA("FA055");return this; }
        public BsTFaDataCQ AddOrderBy_Fa055_Desc() { regOBD("FA055");return this; }

        protected ConditionValue _fa056;
        public ConditionValue Fa056 {
            get { if (_fa056 == null) { _fa056 = new ConditionValue(); } return _fa056; }
        }
        protected override ConditionValue getCValueFa056() { return this.Fa056; }


        public BsTFaDataCQ AddOrderBy_Fa056_Asc() { regOBA("FA056");return this; }
        public BsTFaDataCQ AddOrderBy_Fa056_Desc() { regOBD("FA056");return this; }

        protected ConditionValue _fa057;
        public ConditionValue Fa057 {
            get { if (_fa057 == null) { _fa057 = new ConditionValue(); } return _fa057; }
        }
        protected override ConditionValue getCValueFa057() { return this.Fa057; }


        public BsTFaDataCQ AddOrderBy_Fa057_Asc() { regOBA("FA057");return this; }
        public BsTFaDataCQ AddOrderBy_Fa057_Desc() { regOBD("FA057");return this; }

        protected ConditionValue _fa058;
        public ConditionValue Fa058 {
            get { if (_fa058 == null) { _fa058 = new ConditionValue(); } return _fa058; }
        }
        protected override ConditionValue getCValueFa058() { return this.Fa058; }


        public BsTFaDataCQ AddOrderBy_Fa058_Asc() { regOBA("FA058");return this; }
        public BsTFaDataCQ AddOrderBy_Fa058_Desc() { regOBD("FA058");return this; }

        protected ConditionValue _fa059;
        public ConditionValue Fa059 {
            get { if (_fa059 == null) { _fa059 = new ConditionValue(); } return _fa059; }
        }
        protected override ConditionValue getCValueFa059() { return this.Fa059; }


        public BsTFaDataCQ AddOrderBy_Fa059_Asc() { regOBA("FA059");return this; }
        public BsTFaDataCQ AddOrderBy_Fa059_Desc() { regOBD("FA059");return this; }

        protected ConditionValue _fa060;
        public ConditionValue Fa060 {
            get { if (_fa060 == null) { _fa060 = new ConditionValue(); } return _fa060; }
        }
        protected override ConditionValue getCValueFa060() { return this.Fa060; }


        public BsTFaDataCQ AddOrderBy_Fa060_Asc() { regOBA("FA060");return this; }
        public BsTFaDataCQ AddOrderBy_Fa060_Desc() { regOBD("FA060");return this; }

        protected ConditionValue _fa061;
        public ConditionValue Fa061 {
            get { if (_fa061 == null) { _fa061 = new ConditionValue(); } return _fa061; }
        }
        protected override ConditionValue getCValueFa061() { return this.Fa061; }


        public BsTFaDataCQ AddOrderBy_Fa061_Asc() { regOBA("FA061");return this; }
        public BsTFaDataCQ AddOrderBy_Fa061_Desc() { regOBD("FA061");return this; }

        protected ConditionValue _fa062;
        public ConditionValue Fa062 {
            get { if (_fa062 == null) { _fa062 = new ConditionValue(); } return _fa062; }
        }
        protected override ConditionValue getCValueFa062() { return this.Fa062; }


        public BsTFaDataCQ AddOrderBy_Fa062_Asc() { regOBA("FA062");return this; }
        public BsTFaDataCQ AddOrderBy_Fa062_Desc() { regOBD("FA062");return this; }

        protected ConditionValue _fa063;
        public ConditionValue Fa063 {
            get { if (_fa063 == null) { _fa063 = new ConditionValue(); } return _fa063; }
        }
        protected override ConditionValue getCValueFa063() { return this.Fa063; }


        public BsTFaDataCQ AddOrderBy_Fa063_Asc() { regOBA("FA063");return this; }
        public BsTFaDataCQ AddOrderBy_Fa063_Desc() { regOBD("FA063");return this; }

        protected ConditionValue _fa064;
        public ConditionValue Fa064 {
            get { if (_fa064 == null) { _fa064 = new ConditionValue(); } return _fa064; }
        }
        protected override ConditionValue getCValueFa064() { return this.Fa064; }


        public BsTFaDataCQ AddOrderBy_Fa064_Asc() { regOBA("FA064");return this; }
        public BsTFaDataCQ AddOrderBy_Fa064_Desc() { regOBD("FA064");return this; }

        protected ConditionValue _fa065;
        public ConditionValue Fa065 {
            get { if (_fa065 == null) { _fa065 = new ConditionValue(); } return _fa065; }
        }
        protected override ConditionValue getCValueFa065() { return this.Fa065; }


        public BsTFaDataCQ AddOrderBy_Fa065_Asc() { regOBA("FA065");return this; }
        public BsTFaDataCQ AddOrderBy_Fa065_Desc() { regOBD("FA065");return this; }

        protected ConditionValue _fa066;
        public ConditionValue Fa066 {
            get { if (_fa066 == null) { _fa066 = new ConditionValue(); } return _fa066; }
        }
        protected override ConditionValue getCValueFa066() { return this.Fa066; }


        public BsTFaDataCQ AddOrderBy_Fa066_Asc() { regOBA("FA066");return this; }
        public BsTFaDataCQ AddOrderBy_Fa066_Desc() { regOBD("FA066");return this; }

        protected ConditionValue _fa067;
        public ConditionValue Fa067 {
            get { if (_fa067 == null) { _fa067 = new ConditionValue(); } return _fa067; }
        }
        protected override ConditionValue getCValueFa067() { return this.Fa067; }


        public BsTFaDataCQ AddOrderBy_Fa067_Asc() { regOBA("FA067");return this; }
        public BsTFaDataCQ AddOrderBy_Fa067_Desc() { regOBD("FA067");return this; }

        protected ConditionValue _fa068;
        public ConditionValue Fa068 {
            get { if (_fa068 == null) { _fa068 = new ConditionValue(); } return _fa068; }
        }
        protected override ConditionValue getCValueFa068() { return this.Fa068; }


        public BsTFaDataCQ AddOrderBy_Fa068_Asc() { regOBA("FA068");return this; }
        public BsTFaDataCQ AddOrderBy_Fa068_Desc() { regOBD("FA068");return this; }

        protected ConditionValue _fa069;
        public ConditionValue Fa069 {
            get { if (_fa069 == null) { _fa069 = new ConditionValue(); } return _fa069; }
        }
        protected override ConditionValue getCValueFa069() { return this.Fa069; }


        public BsTFaDataCQ AddOrderBy_Fa069_Asc() { regOBA("FA069");return this; }
        public BsTFaDataCQ AddOrderBy_Fa069_Desc() { regOBD("FA069");return this; }

        protected ConditionValue _fa070;
        public ConditionValue Fa070 {
            get { if (_fa070 == null) { _fa070 = new ConditionValue(); } return _fa070; }
        }
        protected override ConditionValue getCValueFa070() { return this.Fa070; }


        public BsTFaDataCQ AddOrderBy_Fa070_Asc() { regOBA("FA070");return this; }
        public BsTFaDataCQ AddOrderBy_Fa070_Desc() { regOBD("FA070");return this; }

        protected ConditionValue _fa071;
        public ConditionValue Fa071 {
            get { if (_fa071 == null) { _fa071 = new ConditionValue(); } return _fa071; }
        }
        protected override ConditionValue getCValueFa071() { return this.Fa071; }


        public BsTFaDataCQ AddOrderBy_Fa071_Asc() { regOBA("FA071");return this; }
        public BsTFaDataCQ AddOrderBy_Fa071_Desc() { regOBD("FA071");return this; }

        protected ConditionValue _fa072;
        public ConditionValue Fa072 {
            get { if (_fa072 == null) { _fa072 = new ConditionValue(); } return _fa072; }
        }
        protected override ConditionValue getCValueFa072() { return this.Fa072; }


        public BsTFaDataCQ AddOrderBy_Fa072_Asc() { regOBA("FA072");return this; }
        public BsTFaDataCQ AddOrderBy_Fa072_Desc() { regOBD("FA072");return this; }

        protected ConditionValue _fa073;
        public ConditionValue Fa073 {
            get { if (_fa073 == null) { _fa073 = new ConditionValue(); } return _fa073; }
        }
        protected override ConditionValue getCValueFa073() { return this.Fa073; }


        public BsTFaDataCQ AddOrderBy_Fa073_Asc() { regOBA("FA073");return this; }
        public BsTFaDataCQ AddOrderBy_Fa073_Desc() { regOBD("FA073");return this; }

        protected ConditionValue _fa074;
        public ConditionValue Fa074 {
            get { if (_fa074 == null) { _fa074 = new ConditionValue(); } return _fa074; }
        }
        protected override ConditionValue getCValueFa074() { return this.Fa074; }


        public BsTFaDataCQ AddOrderBy_Fa074_Asc() { regOBA("FA074");return this; }
        public BsTFaDataCQ AddOrderBy_Fa074_Desc() { regOBD("FA074");return this; }

        protected ConditionValue _fa075;
        public ConditionValue Fa075 {
            get { if (_fa075 == null) { _fa075 = new ConditionValue(); } return _fa075; }
        }
        protected override ConditionValue getCValueFa075() { return this.Fa075; }


        public BsTFaDataCQ AddOrderBy_Fa075_Asc() { regOBA("FA075");return this; }
        public BsTFaDataCQ AddOrderBy_Fa075_Desc() { regOBD("FA075");return this; }

        protected ConditionValue _fa076;
        public ConditionValue Fa076 {
            get { if (_fa076 == null) { _fa076 = new ConditionValue(); } return _fa076; }
        }
        protected override ConditionValue getCValueFa076() { return this.Fa076; }


        public BsTFaDataCQ AddOrderBy_Fa076_Asc() { regOBA("FA076");return this; }
        public BsTFaDataCQ AddOrderBy_Fa076_Desc() { regOBD("FA076");return this; }

        protected ConditionValue _fa077;
        public ConditionValue Fa077 {
            get { if (_fa077 == null) { _fa077 = new ConditionValue(); } return _fa077; }
        }
        protected override ConditionValue getCValueFa077() { return this.Fa077; }


        public BsTFaDataCQ AddOrderBy_Fa077_Asc() { regOBA("FA077");return this; }
        public BsTFaDataCQ AddOrderBy_Fa077_Desc() { regOBD("FA077");return this; }

        protected ConditionValue _fa078;
        public ConditionValue Fa078 {
            get { if (_fa078 == null) { _fa078 = new ConditionValue(); } return _fa078; }
        }
        protected override ConditionValue getCValueFa078() { return this.Fa078; }


        public BsTFaDataCQ AddOrderBy_Fa078_Asc() { regOBA("FA078");return this; }
        public BsTFaDataCQ AddOrderBy_Fa078_Desc() { regOBD("FA078");return this; }

        protected ConditionValue _fa079;
        public ConditionValue Fa079 {
            get { if (_fa079 == null) { _fa079 = new ConditionValue(); } return _fa079; }
        }
        protected override ConditionValue getCValueFa079() { return this.Fa079; }


        public BsTFaDataCQ AddOrderBy_Fa079_Asc() { regOBA("FA079");return this; }
        public BsTFaDataCQ AddOrderBy_Fa079_Desc() { regOBD("FA079");return this; }

        protected ConditionValue _fa080;
        public ConditionValue Fa080 {
            get { if (_fa080 == null) { _fa080 = new ConditionValue(); } return _fa080; }
        }
        protected override ConditionValue getCValueFa080() { return this.Fa080; }


        public BsTFaDataCQ AddOrderBy_Fa080_Asc() { regOBA("FA080");return this; }
        public BsTFaDataCQ AddOrderBy_Fa080_Desc() { regOBD("FA080");return this; }

        protected ConditionValue _fa081;
        public ConditionValue Fa081 {
            get { if (_fa081 == null) { _fa081 = new ConditionValue(); } return _fa081; }
        }
        protected override ConditionValue getCValueFa081() { return this.Fa081; }


        public BsTFaDataCQ AddOrderBy_Fa081_Asc() { regOBA("FA081");return this; }
        public BsTFaDataCQ AddOrderBy_Fa081_Desc() { regOBD("FA081");return this; }

        protected ConditionValue _fa082;
        public ConditionValue Fa082 {
            get { if (_fa082 == null) { _fa082 = new ConditionValue(); } return _fa082; }
        }
        protected override ConditionValue getCValueFa082() { return this.Fa082; }


        public BsTFaDataCQ AddOrderBy_Fa082_Asc() { regOBA("FA082");return this; }
        public BsTFaDataCQ AddOrderBy_Fa082_Desc() { regOBD("FA082");return this; }

        protected ConditionValue _fa083;
        public ConditionValue Fa083 {
            get { if (_fa083 == null) { _fa083 = new ConditionValue(); } return _fa083; }
        }
        protected override ConditionValue getCValueFa083() { return this.Fa083; }


        public BsTFaDataCQ AddOrderBy_Fa083_Asc() { regOBA("FA083");return this; }
        public BsTFaDataCQ AddOrderBy_Fa083_Desc() { regOBD("FA083");return this; }

        protected ConditionValue _fa084;
        public ConditionValue Fa084 {
            get { if (_fa084 == null) { _fa084 = new ConditionValue(); } return _fa084; }
        }
        protected override ConditionValue getCValueFa084() { return this.Fa084; }


        public BsTFaDataCQ AddOrderBy_Fa084_Asc() { regOBA("FA084");return this; }
        public BsTFaDataCQ AddOrderBy_Fa084_Desc() { regOBD("FA084");return this; }

        protected ConditionValue _fa085;
        public ConditionValue Fa085 {
            get { if (_fa085 == null) { _fa085 = new ConditionValue(); } return _fa085; }
        }
        protected override ConditionValue getCValueFa085() { return this.Fa085; }


        public BsTFaDataCQ AddOrderBy_Fa085_Asc() { regOBA("FA085");return this; }
        public BsTFaDataCQ AddOrderBy_Fa085_Desc() { regOBD("FA085");return this; }

        protected ConditionValue _fa086;
        public ConditionValue Fa086 {
            get { if (_fa086 == null) { _fa086 = new ConditionValue(); } return _fa086; }
        }
        protected override ConditionValue getCValueFa086() { return this.Fa086; }


        public BsTFaDataCQ AddOrderBy_Fa086_Asc() { regOBA("FA086");return this; }
        public BsTFaDataCQ AddOrderBy_Fa086_Desc() { regOBD("FA086");return this; }

        protected ConditionValue _fa087;
        public ConditionValue Fa087 {
            get { if (_fa087 == null) { _fa087 = new ConditionValue(); } return _fa087; }
        }
        protected override ConditionValue getCValueFa087() { return this.Fa087; }


        public BsTFaDataCQ AddOrderBy_Fa087_Asc() { regOBA("FA087");return this; }
        public BsTFaDataCQ AddOrderBy_Fa087_Desc() { regOBD("FA087");return this; }

        protected ConditionValue _fa088;
        public ConditionValue Fa088 {
            get { if (_fa088 == null) { _fa088 = new ConditionValue(); } return _fa088; }
        }
        protected override ConditionValue getCValueFa088() { return this.Fa088; }


        public BsTFaDataCQ AddOrderBy_Fa088_Asc() { regOBA("FA088");return this; }
        public BsTFaDataCQ AddOrderBy_Fa088_Desc() { regOBD("FA088");return this; }

        protected ConditionValue _fa089;
        public ConditionValue Fa089 {
            get { if (_fa089 == null) { _fa089 = new ConditionValue(); } return _fa089; }
        }
        protected override ConditionValue getCValueFa089() { return this.Fa089; }


        public BsTFaDataCQ AddOrderBy_Fa089_Asc() { regOBA("FA089");return this; }
        public BsTFaDataCQ AddOrderBy_Fa089_Desc() { regOBD("FA089");return this; }

        protected ConditionValue _fa090;
        public ConditionValue Fa090 {
            get { if (_fa090 == null) { _fa090 = new ConditionValue(); } return _fa090; }
        }
        protected override ConditionValue getCValueFa090() { return this.Fa090; }


        public BsTFaDataCQ AddOrderBy_Fa090_Asc() { regOBA("FA090");return this; }
        public BsTFaDataCQ AddOrderBy_Fa090_Desc() { regOBD("FA090");return this; }

        protected ConditionValue _fa091;
        public ConditionValue Fa091 {
            get { if (_fa091 == null) { _fa091 = new ConditionValue(); } return _fa091; }
        }
        protected override ConditionValue getCValueFa091() { return this.Fa091; }


        public BsTFaDataCQ AddOrderBy_Fa091_Asc() { regOBA("FA091");return this; }
        public BsTFaDataCQ AddOrderBy_Fa091_Desc() { regOBD("FA091");return this; }

        protected ConditionValue _fa092;
        public ConditionValue Fa092 {
            get { if (_fa092 == null) { _fa092 = new ConditionValue(); } return _fa092; }
        }
        protected override ConditionValue getCValueFa092() { return this.Fa092; }


        public BsTFaDataCQ AddOrderBy_Fa092_Asc() { regOBA("FA092");return this; }
        public BsTFaDataCQ AddOrderBy_Fa092_Desc() { regOBD("FA092");return this; }

        protected ConditionValue _fa093;
        public ConditionValue Fa093 {
            get { if (_fa093 == null) { _fa093 = new ConditionValue(); } return _fa093; }
        }
        protected override ConditionValue getCValueFa093() { return this.Fa093; }


        public BsTFaDataCQ AddOrderBy_Fa093_Asc() { regOBA("FA093");return this; }
        public BsTFaDataCQ AddOrderBy_Fa093_Desc() { regOBD("FA093");return this; }

        protected ConditionValue _fa094;
        public ConditionValue Fa094 {
            get { if (_fa094 == null) { _fa094 = new ConditionValue(); } return _fa094; }
        }
        protected override ConditionValue getCValueFa094() { return this.Fa094; }


        public BsTFaDataCQ AddOrderBy_Fa094_Asc() { regOBA("FA094");return this; }
        public BsTFaDataCQ AddOrderBy_Fa094_Desc() { regOBD("FA094");return this; }

        protected ConditionValue _fa095;
        public ConditionValue Fa095 {
            get { if (_fa095 == null) { _fa095 = new ConditionValue(); } return _fa095; }
        }
        protected override ConditionValue getCValueFa095() { return this.Fa095; }


        public BsTFaDataCQ AddOrderBy_Fa095_Asc() { regOBA("FA095");return this; }
        public BsTFaDataCQ AddOrderBy_Fa095_Desc() { regOBD("FA095");return this; }

        protected ConditionValue _fa096;
        public ConditionValue Fa096 {
            get { if (_fa096 == null) { _fa096 = new ConditionValue(); } return _fa096; }
        }
        protected override ConditionValue getCValueFa096() { return this.Fa096; }


        public BsTFaDataCQ AddOrderBy_Fa096_Asc() { regOBA("FA096");return this; }
        public BsTFaDataCQ AddOrderBy_Fa096_Desc() { regOBD("FA096");return this; }

        protected ConditionValue _fa097;
        public ConditionValue Fa097 {
            get { if (_fa097 == null) { _fa097 = new ConditionValue(); } return _fa097; }
        }
        protected override ConditionValue getCValueFa097() { return this.Fa097; }


        public BsTFaDataCQ AddOrderBy_Fa097_Asc() { regOBA("FA097");return this; }
        public BsTFaDataCQ AddOrderBy_Fa097_Desc() { regOBD("FA097");return this; }

        protected ConditionValue _fa098;
        public ConditionValue Fa098 {
            get { if (_fa098 == null) { _fa098 = new ConditionValue(); } return _fa098; }
        }
        protected override ConditionValue getCValueFa098() { return this.Fa098; }


        public BsTFaDataCQ AddOrderBy_Fa098_Asc() { regOBA("FA098");return this; }
        public BsTFaDataCQ AddOrderBy_Fa098_Desc() { regOBD("FA098");return this; }

        protected ConditionValue _fa099;
        public ConditionValue Fa099 {
            get { if (_fa099 == null) { _fa099 = new ConditionValue(); } return _fa099; }
        }
        protected override ConditionValue getCValueFa099() { return this.Fa099; }


        public BsTFaDataCQ AddOrderBy_Fa099_Asc() { regOBA("FA099");return this; }
        public BsTFaDataCQ AddOrderBy_Fa099_Desc() { regOBD("FA099");return this; }

        protected ConditionValue _fa100;
        public ConditionValue Fa100 {
            get { if (_fa100 == null) { _fa100 = new ConditionValue(); } return _fa100; }
        }
        protected override ConditionValue getCValueFa100() { return this.Fa100; }


        public BsTFaDataCQ AddOrderBy_Fa100_Asc() { regOBA("FA100");return this; }
        public BsTFaDataCQ AddOrderBy_Fa100_Desc() { regOBD("FA100");return this; }

        protected ConditionValue _fa101;
        public ConditionValue Fa101 {
            get { if (_fa101 == null) { _fa101 = new ConditionValue(); } return _fa101; }
        }
        protected override ConditionValue getCValueFa101() { return this.Fa101; }


        public BsTFaDataCQ AddOrderBy_Fa101_Asc() { regOBA("FA101");return this; }
        public BsTFaDataCQ AddOrderBy_Fa101_Desc() { regOBD("FA101");return this; }

        protected ConditionValue _fa102;
        public ConditionValue Fa102 {
            get { if (_fa102 == null) { _fa102 = new ConditionValue(); } return _fa102; }
        }
        protected override ConditionValue getCValueFa102() { return this.Fa102; }


        public BsTFaDataCQ AddOrderBy_Fa102_Asc() { regOBA("FA102");return this; }
        public BsTFaDataCQ AddOrderBy_Fa102_Desc() { regOBD("FA102");return this; }

        protected ConditionValue _fa103;
        public ConditionValue Fa103 {
            get { if (_fa103 == null) { _fa103 = new ConditionValue(); } return _fa103; }
        }
        protected override ConditionValue getCValueFa103() { return this.Fa103; }


        public BsTFaDataCQ AddOrderBy_Fa103_Asc() { regOBA("FA103");return this; }
        public BsTFaDataCQ AddOrderBy_Fa103_Desc() { regOBD("FA103");return this; }

        protected ConditionValue _fa104;
        public ConditionValue Fa104 {
            get { if (_fa104 == null) { _fa104 = new ConditionValue(); } return _fa104; }
        }
        protected override ConditionValue getCValueFa104() { return this.Fa104; }


        public BsTFaDataCQ AddOrderBy_Fa104_Asc() { regOBA("FA104");return this; }
        public BsTFaDataCQ AddOrderBy_Fa104_Desc() { regOBD("FA104");return this; }

        protected ConditionValue _fa105;
        public ConditionValue Fa105 {
            get { if (_fa105 == null) { _fa105 = new ConditionValue(); } return _fa105; }
        }
        protected override ConditionValue getCValueFa105() { return this.Fa105; }


        public BsTFaDataCQ AddOrderBy_Fa105_Asc() { regOBA("FA105");return this; }
        public BsTFaDataCQ AddOrderBy_Fa105_Desc() { regOBD("FA105");return this; }

        protected ConditionValue _fa106;
        public ConditionValue Fa106 {
            get { if (_fa106 == null) { _fa106 = new ConditionValue(); } return _fa106; }
        }
        protected override ConditionValue getCValueFa106() { return this.Fa106; }


        public BsTFaDataCQ AddOrderBy_Fa106_Asc() { regOBA("FA106");return this; }
        public BsTFaDataCQ AddOrderBy_Fa106_Desc() { regOBD("FA106");return this; }

        protected ConditionValue _fa107;
        public ConditionValue Fa107 {
            get { if (_fa107 == null) { _fa107 = new ConditionValue(); } return _fa107; }
        }
        protected override ConditionValue getCValueFa107() { return this.Fa107; }


        public BsTFaDataCQ AddOrderBy_Fa107_Asc() { regOBA("FA107");return this; }
        public BsTFaDataCQ AddOrderBy_Fa107_Desc() { regOBD("FA107");return this; }

        protected ConditionValue _fa108;
        public ConditionValue Fa108 {
            get { if (_fa108 == null) { _fa108 = new ConditionValue(); } return _fa108; }
        }
        protected override ConditionValue getCValueFa108() { return this.Fa108; }


        public BsTFaDataCQ AddOrderBy_Fa108_Asc() { regOBA("FA108");return this; }
        public BsTFaDataCQ AddOrderBy_Fa108_Desc() { regOBD("FA108");return this; }

        protected ConditionValue _fa109;
        public ConditionValue Fa109 {
            get { if (_fa109 == null) { _fa109 = new ConditionValue(); } return _fa109; }
        }
        protected override ConditionValue getCValueFa109() { return this.Fa109; }


        public BsTFaDataCQ AddOrderBy_Fa109_Asc() { regOBA("FA109");return this; }
        public BsTFaDataCQ AddOrderBy_Fa109_Desc() { regOBD("FA109");return this; }

        protected ConditionValue _fa110;
        public ConditionValue Fa110 {
            get { if (_fa110 == null) { _fa110 = new ConditionValue(); } return _fa110; }
        }
        protected override ConditionValue getCValueFa110() { return this.Fa110; }


        public BsTFaDataCQ AddOrderBy_Fa110_Asc() { regOBA("FA110");return this; }
        public BsTFaDataCQ AddOrderBy_Fa110_Desc() { regOBD("FA110");return this; }

        protected ConditionValue _fa111;
        public ConditionValue Fa111 {
            get { if (_fa111 == null) { _fa111 = new ConditionValue(); } return _fa111; }
        }
        protected override ConditionValue getCValueFa111() { return this.Fa111; }


        public BsTFaDataCQ AddOrderBy_Fa111_Asc() { regOBA("FA111");return this; }
        public BsTFaDataCQ AddOrderBy_Fa111_Desc() { regOBD("FA111");return this; }

        protected ConditionValue _fa112;
        public ConditionValue Fa112 {
            get { if (_fa112 == null) { _fa112 = new ConditionValue(); } return _fa112; }
        }
        protected override ConditionValue getCValueFa112() { return this.Fa112; }


        public BsTFaDataCQ AddOrderBy_Fa112_Asc() { regOBA("FA112");return this; }
        public BsTFaDataCQ AddOrderBy_Fa112_Desc() { regOBD("FA112");return this; }

        protected ConditionValue _fa113;
        public ConditionValue Fa113 {
            get { if (_fa113 == null) { _fa113 = new ConditionValue(); } return _fa113; }
        }
        protected override ConditionValue getCValueFa113() { return this.Fa113; }


        public BsTFaDataCQ AddOrderBy_Fa113_Asc() { regOBA("FA113");return this; }
        public BsTFaDataCQ AddOrderBy_Fa113_Desc() { regOBD("FA113");return this; }

        protected ConditionValue _fa114;
        public ConditionValue Fa114 {
            get { if (_fa114 == null) { _fa114 = new ConditionValue(); } return _fa114; }
        }
        protected override ConditionValue getCValueFa114() { return this.Fa114; }


        public BsTFaDataCQ AddOrderBy_Fa114_Asc() { regOBA("FA114");return this; }
        public BsTFaDataCQ AddOrderBy_Fa114_Desc() { regOBD("FA114");return this; }

        protected ConditionValue _fa115;
        public ConditionValue Fa115 {
            get { if (_fa115 == null) { _fa115 = new ConditionValue(); } return _fa115; }
        }
        protected override ConditionValue getCValueFa115() { return this.Fa115; }


        public BsTFaDataCQ AddOrderBy_Fa115_Asc() { regOBA("FA115");return this; }
        public BsTFaDataCQ AddOrderBy_Fa115_Desc() { regOBD("FA115");return this; }

        protected ConditionValue _fa116;
        public ConditionValue Fa116 {
            get { if (_fa116 == null) { _fa116 = new ConditionValue(); } return _fa116; }
        }
        protected override ConditionValue getCValueFa116() { return this.Fa116; }


        public BsTFaDataCQ AddOrderBy_Fa116_Asc() { regOBA("FA116");return this; }
        public BsTFaDataCQ AddOrderBy_Fa116_Desc() { regOBD("FA116");return this; }

        protected ConditionValue _fa117;
        public ConditionValue Fa117 {
            get { if (_fa117 == null) { _fa117 = new ConditionValue(); } return _fa117; }
        }
        protected override ConditionValue getCValueFa117() { return this.Fa117; }


        public BsTFaDataCQ AddOrderBy_Fa117_Asc() { regOBA("FA117");return this; }
        public BsTFaDataCQ AddOrderBy_Fa117_Desc() { regOBD("FA117");return this; }

        protected ConditionValue _fa118;
        public ConditionValue Fa118 {
            get { if (_fa118 == null) { _fa118 = new ConditionValue(); } return _fa118; }
        }
        protected override ConditionValue getCValueFa118() { return this.Fa118; }


        public BsTFaDataCQ AddOrderBy_Fa118_Asc() { regOBA("FA118");return this; }
        public BsTFaDataCQ AddOrderBy_Fa118_Desc() { regOBD("FA118");return this; }

        protected ConditionValue _fa119;
        public ConditionValue Fa119 {
            get { if (_fa119 == null) { _fa119 = new ConditionValue(); } return _fa119; }
        }
        protected override ConditionValue getCValueFa119() { return this.Fa119; }


        public BsTFaDataCQ AddOrderBy_Fa119_Asc() { regOBA("FA119");return this; }
        public BsTFaDataCQ AddOrderBy_Fa119_Desc() { regOBD("FA119");return this; }

        protected ConditionValue _fa120;
        public ConditionValue Fa120 {
            get { if (_fa120 == null) { _fa120 = new ConditionValue(); } return _fa120; }
        }
        protected override ConditionValue getCValueFa120() { return this.Fa120; }


        public BsTFaDataCQ AddOrderBy_Fa120_Asc() { regOBA("FA120");return this; }
        public BsTFaDataCQ AddOrderBy_Fa120_Desc() { regOBD("FA120");return this; }

        protected ConditionValue _fa121;
        public ConditionValue Fa121 {
            get { if (_fa121 == null) { _fa121 = new ConditionValue(); } return _fa121; }
        }
        protected override ConditionValue getCValueFa121() { return this.Fa121; }


        public BsTFaDataCQ AddOrderBy_Fa121_Asc() { regOBA("FA121");return this; }
        public BsTFaDataCQ AddOrderBy_Fa121_Desc() { regOBD("FA121");return this; }

        protected ConditionValue _fa122;
        public ConditionValue Fa122 {
            get { if (_fa122 == null) { _fa122 = new ConditionValue(); } return _fa122; }
        }
        protected override ConditionValue getCValueFa122() { return this.Fa122; }


        public BsTFaDataCQ AddOrderBy_Fa122_Asc() { regOBA("FA122");return this; }
        public BsTFaDataCQ AddOrderBy_Fa122_Desc() { regOBD("FA122");return this; }

        protected ConditionValue _fa123;
        public ConditionValue Fa123 {
            get { if (_fa123 == null) { _fa123 = new ConditionValue(); } return _fa123; }
        }
        protected override ConditionValue getCValueFa123() { return this.Fa123; }


        public BsTFaDataCQ AddOrderBy_Fa123_Asc() { regOBA("FA123");return this; }
        public BsTFaDataCQ AddOrderBy_Fa123_Desc() { regOBD("FA123");return this; }

        protected ConditionValue _fa124;
        public ConditionValue Fa124 {
            get { if (_fa124 == null) { _fa124 = new ConditionValue(); } return _fa124; }
        }
        protected override ConditionValue getCValueFa124() { return this.Fa124; }


        public BsTFaDataCQ AddOrderBy_Fa124_Asc() { regOBA("FA124");return this; }
        public BsTFaDataCQ AddOrderBy_Fa124_Desc() { regOBD("FA124");return this; }

        protected ConditionValue _fa125;
        public ConditionValue Fa125 {
            get { if (_fa125 == null) { _fa125 = new ConditionValue(); } return _fa125; }
        }
        protected override ConditionValue getCValueFa125() { return this.Fa125; }


        public BsTFaDataCQ AddOrderBy_Fa125_Asc() { regOBA("FA125");return this; }
        public BsTFaDataCQ AddOrderBy_Fa125_Desc() { regOBD("FA125");return this; }

        protected ConditionValue _fa126;
        public ConditionValue Fa126 {
            get { if (_fa126 == null) { _fa126 = new ConditionValue(); } return _fa126; }
        }
        protected override ConditionValue getCValueFa126() { return this.Fa126; }


        public BsTFaDataCQ AddOrderBy_Fa126_Asc() { regOBA("FA126");return this; }
        public BsTFaDataCQ AddOrderBy_Fa126_Desc() { regOBD("FA126");return this; }

        protected ConditionValue _fa127;
        public ConditionValue Fa127 {
            get { if (_fa127 == null) { _fa127 = new ConditionValue(); } return _fa127; }
        }
        protected override ConditionValue getCValueFa127() { return this.Fa127; }


        public BsTFaDataCQ AddOrderBy_Fa127_Asc() { regOBA("FA127");return this; }
        public BsTFaDataCQ AddOrderBy_Fa127_Desc() { regOBD("FA127");return this; }

        protected ConditionValue _fa128;
        public ConditionValue Fa128 {
            get { if (_fa128 == null) { _fa128 = new ConditionValue(); } return _fa128; }
        }
        protected override ConditionValue getCValueFa128() { return this.Fa128; }


        public BsTFaDataCQ AddOrderBy_Fa128_Asc() { regOBA("FA128");return this; }
        public BsTFaDataCQ AddOrderBy_Fa128_Desc() { regOBD("FA128");return this; }

        protected ConditionValue _fa129;
        public ConditionValue Fa129 {
            get { if (_fa129 == null) { _fa129 = new ConditionValue(); } return _fa129; }
        }
        protected override ConditionValue getCValueFa129() { return this.Fa129; }


        public BsTFaDataCQ AddOrderBy_Fa129_Asc() { regOBA("FA129");return this; }
        public BsTFaDataCQ AddOrderBy_Fa129_Desc() { regOBD("FA129");return this; }

        protected ConditionValue _fa130;
        public ConditionValue Fa130 {
            get { if (_fa130 == null) { _fa130 = new ConditionValue(); } return _fa130; }
        }
        protected override ConditionValue getCValueFa130() { return this.Fa130; }


        public BsTFaDataCQ AddOrderBy_Fa130_Asc() { regOBA("FA130");return this; }
        public BsTFaDataCQ AddOrderBy_Fa130_Desc() { regOBD("FA130");return this; }

        protected ConditionValue _fa131;
        public ConditionValue Fa131 {
            get { if (_fa131 == null) { _fa131 = new ConditionValue(); } return _fa131; }
        }
        protected override ConditionValue getCValueFa131() { return this.Fa131; }


        public BsTFaDataCQ AddOrderBy_Fa131_Asc() { regOBA("FA131");return this; }
        public BsTFaDataCQ AddOrderBy_Fa131_Desc() { regOBD("FA131");return this; }

        protected ConditionValue _fa132;
        public ConditionValue Fa132 {
            get { if (_fa132 == null) { _fa132 = new ConditionValue(); } return _fa132; }
        }
        protected override ConditionValue getCValueFa132() { return this.Fa132; }


        public BsTFaDataCQ AddOrderBy_Fa132_Asc() { regOBA("FA132");return this; }
        public BsTFaDataCQ AddOrderBy_Fa132_Desc() { regOBD("FA132");return this; }

        protected ConditionValue _fa133;
        public ConditionValue Fa133 {
            get { if (_fa133 == null) { _fa133 = new ConditionValue(); } return _fa133; }
        }
        protected override ConditionValue getCValueFa133() { return this.Fa133; }


        public BsTFaDataCQ AddOrderBy_Fa133_Asc() { regOBA("FA133");return this; }
        public BsTFaDataCQ AddOrderBy_Fa133_Desc() { regOBD("FA133");return this; }

        protected ConditionValue _fa134;
        public ConditionValue Fa134 {
            get { if (_fa134 == null) { _fa134 = new ConditionValue(); } return _fa134; }
        }
        protected override ConditionValue getCValueFa134() { return this.Fa134; }


        public BsTFaDataCQ AddOrderBy_Fa134_Asc() { regOBA("FA134");return this; }
        public BsTFaDataCQ AddOrderBy_Fa134_Desc() { regOBD("FA134");return this; }

        protected ConditionValue _fa135;
        public ConditionValue Fa135 {
            get { if (_fa135 == null) { _fa135 = new ConditionValue(); } return _fa135; }
        }
        protected override ConditionValue getCValueFa135() { return this.Fa135; }


        public BsTFaDataCQ AddOrderBy_Fa135_Asc() { regOBA("FA135");return this; }
        public BsTFaDataCQ AddOrderBy_Fa135_Desc() { regOBD("FA135");return this; }

        protected ConditionValue _fa136;
        public ConditionValue Fa136 {
            get { if (_fa136 == null) { _fa136 = new ConditionValue(); } return _fa136; }
        }
        protected override ConditionValue getCValueFa136() { return this.Fa136; }


        public BsTFaDataCQ AddOrderBy_Fa136_Asc() { regOBA("FA136");return this; }
        public BsTFaDataCQ AddOrderBy_Fa136_Desc() { regOBD("FA136");return this; }

        protected ConditionValue _fa137;
        public ConditionValue Fa137 {
            get { if (_fa137 == null) { _fa137 = new ConditionValue(); } return _fa137; }
        }
        protected override ConditionValue getCValueFa137() { return this.Fa137; }


        public BsTFaDataCQ AddOrderBy_Fa137_Asc() { regOBA("FA137");return this; }
        public BsTFaDataCQ AddOrderBy_Fa137_Desc() { regOBD("FA137");return this; }

        protected ConditionValue _fa138;
        public ConditionValue Fa138 {
            get { if (_fa138 == null) { _fa138 = new ConditionValue(); } return _fa138; }
        }
        protected override ConditionValue getCValueFa138() { return this.Fa138; }


        public BsTFaDataCQ AddOrderBy_Fa138_Asc() { regOBA("FA138");return this; }
        public BsTFaDataCQ AddOrderBy_Fa138_Desc() { regOBD("FA138");return this; }

        protected ConditionValue _fa139;
        public ConditionValue Fa139 {
            get { if (_fa139 == null) { _fa139 = new ConditionValue(); } return _fa139; }
        }
        protected override ConditionValue getCValueFa139() { return this.Fa139; }


        public BsTFaDataCQ AddOrderBy_Fa139_Asc() { regOBA("FA139");return this; }
        public BsTFaDataCQ AddOrderBy_Fa139_Desc() { regOBD("FA139");return this; }

        protected ConditionValue _fa140;
        public ConditionValue Fa140 {
            get { if (_fa140 == null) { _fa140 = new ConditionValue(); } return _fa140; }
        }
        protected override ConditionValue getCValueFa140() { return this.Fa140; }


        public BsTFaDataCQ AddOrderBy_Fa140_Asc() { regOBA("FA140");return this; }
        public BsTFaDataCQ AddOrderBy_Fa140_Desc() { regOBD("FA140");return this; }

        protected ConditionValue _fa141;
        public ConditionValue Fa141 {
            get { if (_fa141 == null) { _fa141 = new ConditionValue(); } return _fa141; }
        }
        protected override ConditionValue getCValueFa141() { return this.Fa141; }


        public BsTFaDataCQ AddOrderBy_Fa141_Asc() { regOBA("FA141");return this; }
        public BsTFaDataCQ AddOrderBy_Fa141_Desc() { regOBD("FA141");return this; }

        protected ConditionValue _fa142;
        public ConditionValue Fa142 {
            get { if (_fa142 == null) { _fa142 = new ConditionValue(); } return _fa142; }
        }
        protected override ConditionValue getCValueFa142() { return this.Fa142; }


        public BsTFaDataCQ AddOrderBy_Fa142_Asc() { regOBA("FA142");return this; }
        public BsTFaDataCQ AddOrderBy_Fa142_Desc() { regOBD("FA142");return this; }

        protected ConditionValue _fa143;
        public ConditionValue Fa143 {
            get { if (_fa143 == null) { _fa143 = new ConditionValue(); } return _fa143; }
        }
        protected override ConditionValue getCValueFa143() { return this.Fa143; }


        public BsTFaDataCQ AddOrderBy_Fa143_Asc() { regOBA("FA143");return this; }
        public BsTFaDataCQ AddOrderBy_Fa143_Desc() { regOBD("FA143");return this; }

        protected ConditionValue _fa144;
        public ConditionValue Fa144 {
            get { if (_fa144 == null) { _fa144 = new ConditionValue(); } return _fa144; }
        }
        protected override ConditionValue getCValueFa144() { return this.Fa144; }


        public BsTFaDataCQ AddOrderBy_Fa144_Asc() { regOBA("FA144");return this; }
        public BsTFaDataCQ AddOrderBy_Fa144_Desc() { regOBD("FA144");return this; }

        protected ConditionValue _fa145;
        public ConditionValue Fa145 {
            get { if (_fa145 == null) { _fa145 = new ConditionValue(); } return _fa145; }
        }
        protected override ConditionValue getCValueFa145() { return this.Fa145; }


        public BsTFaDataCQ AddOrderBy_Fa145_Asc() { regOBA("FA145");return this; }
        public BsTFaDataCQ AddOrderBy_Fa145_Desc() { regOBD("FA145");return this; }

        protected ConditionValue _fa146;
        public ConditionValue Fa146 {
            get { if (_fa146 == null) { _fa146 = new ConditionValue(); } return _fa146; }
        }
        protected override ConditionValue getCValueFa146() { return this.Fa146; }


        public BsTFaDataCQ AddOrderBy_Fa146_Asc() { regOBA("FA146");return this; }
        public BsTFaDataCQ AddOrderBy_Fa146_Desc() { regOBD("FA146");return this; }

        protected ConditionValue _fa147;
        public ConditionValue Fa147 {
            get { if (_fa147 == null) { _fa147 = new ConditionValue(); } return _fa147; }
        }
        protected override ConditionValue getCValueFa147() { return this.Fa147; }


        public BsTFaDataCQ AddOrderBy_Fa147_Asc() { regOBA("FA147");return this; }
        public BsTFaDataCQ AddOrderBy_Fa147_Desc() { regOBD("FA147");return this; }

        protected ConditionValue _fa148;
        public ConditionValue Fa148 {
            get { if (_fa148 == null) { _fa148 = new ConditionValue(); } return _fa148; }
        }
        protected override ConditionValue getCValueFa148() { return this.Fa148; }


        public BsTFaDataCQ AddOrderBy_Fa148_Asc() { regOBA("FA148");return this; }
        public BsTFaDataCQ AddOrderBy_Fa148_Desc() { regOBD("FA148");return this; }

        protected ConditionValue _fa149;
        public ConditionValue Fa149 {
            get { if (_fa149 == null) { _fa149 = new ConditionValue(); } return _fa149; }
        }
        protected override ConditionValue getCValueFa149() { return this.Fa149; }


        public BsTFaDataCQ AddOrderBy_Fa149_Asc() { regOBA("FA149");return this; }
        public BsTFaDataCQ AddOrderBy_Fa149_Desc() { regOBD("FA149");return this; }

        protected ConditionValue _fa150;
        public ConditionValue Fa150 {
            get { if (_fa150 == null) { _fa150 = new ConditionValue(); } return _fa150; }
        }
        protected override ConditionValue getCValueFa150() { return this.Fa150; }


        public BsTFaDataCQ AddOrderBy_Fa150_Asc() { regOBA("FA150");return this; }
        public BsTFaDataCQ AddOrderBy_Fa150_Desc() { regOBD("FA150");return this; }

        protected ConditionValue _fa151;
        public ConditionValue Fa151 {
            get { if (_fa151 == null) { _fa151 = new ConditionValue(); } return _fa151; }
        }
        protected override ConditionValue getCValueFa151() { return this.Fa151; }


        public BsTFaDataCQ AddOrderBy_Fa151_Asc() { regOBA("FA151");return this; }
        public BsTFaDataCQ AddOrderBy_Fa151_Desc() { regOBD("FA151");return this; }

        protected ConditionValue _fa152;
        public ConditionValue Fa152 {
            get { if (_fa152 == null) { _fa152 = new ConditionValue(); } return _fa152; }
        }
        protected override ConditionValue getCValueFa152() { return this.Fa152; }


        public BsTFaDataCQ AddOrderBy_Fa152_Asc() { regOBA("FA152");return this; }
        public BsTFaDataCQ AddOrderBy_Fa152_Desc() { regOBD("FA152");return this; }

        protected ConditionValue _fa153;
        public ConditionValue Fa153 {
            get { if (_fa153 == null) { _fa153 = new ConditionValue(); } return _fa153; }
        }
        protected override ConditionValue getCValueFa153() { return this.Fa153; }


        public BsTFaDataCQ AddOrderBy_Fa153_Asc() { regOBA("FA153");return this; }
        public BsTFaDataCQ AddOrderBy_Fa153_Desc() { regOBD("FA153");return this; }

        protected ConditionValue _fa154;
        public ConditionValue Fa154 {
            get { if (_fa154 == null) { _fa154 = new ConditionValue(); } return _fa154; }
        }
        protected override ConditionValue getCValueFa154() { return this.Fa154; }


        public BsTFaDataCQ AddOrderBy_Fa154_Asc() { regOBA("FA154");return this; }
        public BsTFaDataCQ AddOrderBy_Fa154_Desc() { regOBD("FA154");return this; }

        protected ConditionValue _fa155;
        public ConditionValue Fa155 {
            get { if (_fa155 == null) { _fa155 = new ConditionValue(); } return _fa155; }
        }
        protected override ConditionValue getCValueFa155() { return this.Fa155; }


        public BsTFaDataCQ AddOrderBy_Fa155_Asc() { regOBA("FA155");return this; }
        public BsTFaDataCQ AddOrderBy_Fa155_Desc() { regOBD("FA155");return this; }

        protected ConditionValue _fa156;
        public ConditionValue Fa156 {
            get { if (_fa156 == null) { _fa156 = new ConditionValue(); } return _fa156; }
        }
        protected override ConditionValue getCValueFa156() { return this.Fa156; }


        public BsTFaDataCQ AddOrderBy_Fa156_Asc() { regOBA("FA156");return this; }
        public BsTFaDataCQ AddOrderBy_Fa156_Desc() { regOBD("FA156");return this; }

        protected ConditionValue _fa157;
        public ConditionValue Fa157 {
            get { if (_fa157 == null) { _fa157 = new ConditionValue(); } return _fa157; }
        }
        protected override ConditionValue getCValueFa157() { return this.Fa157; }


        public BsTFaDataCQ AddOrderBy_Fa157_Asc() { regOBA("FA157");return this; }
        public BsTFaDataCQ AddOrderBy_Fa157_Desc() { regOBD("FA157");return this; }

        protected ConditionValue _fa158;
        public ConditionValue Fa158 {
            get { if (_fa158 == null) { _fa158 = new ConditionValue(); } return _fa158; }
        }
        protected override ConditionValue getCValueFa158() { return this.Fa158; }


        public BsTFaDataCQ AddOrderBy_Fa158_Asc() { regOBA("FA158");return this; }
        public BsTFaDataCQ AddOrderBy_Fa158_Desc() { regOBD("FA158");return this; }

        protected ConditionValue _fa159;
        public ConditionValue Fa159 {
            get { if (_fa159 == null) { _fa159 = new ConditionValue(); } return _fa159; }
        }
        protected override ConditionValue getCValueFa159() { return this.Fa159; }


        public BsTFaDataCQ AddOrderBy_Fa159_Asc() { regOBA("FA159");return this; }
        public BsTFaDataCQ AddOrderBy_Fa159_Desc() { regOBD("FA159");return this; }

        protected ConditionValue _fa160;
        public ConditionValue Fa160 {
            get { if (_fa160 == null) { _fa160 = new ConditionValue(); } return _fa160; }
        }
        protected override ConditionValue getCValueFa160() { return this.Fa160; }


        public BsTFaDataCQ AddOrderBy_Fa160_Asc() { regOBA("FA160");return this; }
        public BsTFaDataCQ AddOrderBy_Fa160_Desc() { regOBD("FA160");return this; }

        protected ConditionValue _fa161;
        public ConditionValue Fa161 {
            get { if (_fa161 == null) { _fa161 = new ConditionValue(); } return _fa161; }
        }
        protected override ConditionValue getCValueFa161() { return this.Fa161; }


        public BsTFaDataCQ AddOrderBy_Fa161_Asc() { regOBA("FA161");return this; }
        public BsTFaDataCQ AddOrderBy_Fa161_Desc() { regOBD("FA161");return this; }

        protected ConditionValue _fa162;
        public ConditionValue Fa162 {
            get { if (_fa162 == null) { _fa162 = new ConditionValue(); } return _fa162; }
        }
        protected override ConditionValue getCValueFa162() { return this.Fa162; }


        public BsTFaDataCQ AddOrderBy_Fa162_Asc() { regOBA("FA162");return this; }
        public BsTFaDataCQ AddOrderBy_Fa162_Desc() { regOBD("FA162");return this; }

        protected ConditionValue _fa163;
        public ConditionValue Fa163 {
            get { if (_fa163 == null) { _fa163 = new ConditionValue(); } return _fa163; }
        }
        protected override ConditionValue getCValueFa163() { return this.Fa163; }


        public BsTFaDataCQ AddOrderBy_Fa163_Asc() { regOBA("FA163");return this; }
        public BsTFaDataCQ AddOrderBy_Fa163_Desc() { regOBD("FA163");return this; }

        protected ConditionValue _fa164;
        public ConditionValue Fa164 {
            get { if (_fa164 == null) { _fa164 = new ConditionValue(); } return _fa164; }
        }
        protected override ConditionValue getCValueFa164() { return this.Fa164; }


        public BsTFaDataCQ AddOrderBy_Fa164_Asc() { regOBA("FA164");return this; }
        public BsTFaDataCQ AddOrderBy_Fa164_Desc() { regOBD("FA164");return this; }

        protected ConditionValue _fa165;
        public ConditionValue Fa165 {
            get { if (_fa165 == null) { _fa165 = new ConditionValue(); } return _fa165; }
        }
        protected override ConditionValue getCValueFa165() { return this.Fa165; }


        public BsTFaDataCQ AddOrderBy_Fa165_Asc() { regOBA("FA165");return this; }
        public BsTFaDataCQ AddOrderBy_Fa165_Desc() { regOBD("FA165");return this; }

        protected ConditionValue _fa166;
        public ConditionValue Fa166 {
            get { if (_fa166 == null) { _fa166 = new ConditionValue(); } return _fa166; }
        }
        protected override ConditionValue getCValueFa166() { return this.Fa166; }


        public BsTFaDataCQ AddOrderBy_Fa166_Asc() { regOBA("FA166");return this; }
        public BsTFaDataCQ AddOrderBy_Fa166_Desc() { regOBD("FA166");return this; }

        protected ConditionValue _fa167;
        public ConditionValue Fa167 {
            get { if (_fa167 == null) { _fa167 = new ConditionValue(); } return _fa167; }
        }
        protected override ConditionValue getCValueFa167() { return this.Fa167; }


        public BsTFaDataCQ AddOrderBy_Fa167_Asc() { regOBA("FA167");return this; }
        public BsTFaDataCQ AddOrderBy_Fa167_Desc() { regOBD("FA167");return this; }

        protected ConditionValue _fa168;
        public ConditionValue Fa168 {
            get { if (_fa168 == null) { _fa168 = new ConditionValue(); } return _fa168; }
        }
        protected override ConditionValue getCValueFa168() { return this.Fa168; }


        public BsTFaDataCQ AddOrderBy_Fa168_Asc() { regOBA("FA168");return this; }
        public BsTFaDataCQ AddOrderBy_Fa168_Desc() { regOBD("FA168");return this; }

        protected ConditionValue _fa169;
        public ConditionValue Fa169 {
            get { if (_fa169 == null) { _fa169 = new ConditionValue(); } return _fa169; }
        }
        protected override ConditionValue getCValueFa169() { return this.Fa169; }


        public BsTFaDataCQ AddOrderBy_Fa169_Asc() { regOBA("FA169");return this; }
        public BsTFaDataCQ AddOrderBy_Fa169_Desc() { regOBD("FA169");return this; }

        protected ConditionValue _fa170;
        public ConditionValue Fa170 {
            get { if (_fa170 == null) { _fa170 = new ConditionValue(); } return _fa170; }
        }
        protected override ConditionValue getCValueFa170() { return this.Fa170; }


        public BsTFaDataCQ AddOrderBy_Fa170_Asc() { regOBA("FA170");return this; }
        public BsTFaDataCQ AddOrderBy_Fa170_Desc() { regOBD("FA170");return this; }

        protected ConditionValue _fa171;
        public ConditionValue Fa171 {
            get { if (_fa171 == null) { _fa171 = new ConditionValue(); } return _fa171; }
        }
        protected override ConditionValue getCValueFa171() { return this.Fa171; }


        public BsTFaDataCQ AddOrderBy_Fa171_Asc() { regOBA("FA171");return this; }
        public BsTFaDataCQ AddOrderBy_Fa171_Desc() { regOBD("FA171");return this; }

        protected ConditionValue _fa172;
        public ConditionValue Fa172 {
            get { if (_fa172 == null) { _fa172 = new ConditionValue(); } return _fa172; }
        }
        protected override ConditionValue getCValueFa172() { return this.Fa172; }


        public BsTFaDataCQ AddOrderBy_Fa172_Asc() { regOBA("FA172");return this; }
        public BsTFaDataCQ AddOrderBy_Fa172_Desc() { regOBD("FA172");return this; }

        protected ConditionValue _fa173;
        public ConditionValue Fa173 {
            get { if (_fa173 == null) { _fa173 = new ConditionValue(); } return _fa173; }
        }
        protected override ConditionValue getCValueFa173() { return this.Fa173; }


        public BsTFaDataCQ AddOrderBy_Fa173_Asc() { regOBA("FA173");return this; }
        public BsTFaDataCQ AddOrderBy_Fa173_Desc() { regOBD("FA173");return this; }

        protected ConditionValue _fa174;
        public ConditionValue Fa174 {
            get { if (_fa174 == null) { _fa174 = new ConditionValue(); } return _fa174; }
        }
        protected override ConditionValue getCValueFa174() { return this.Fa174; }


        public BsTFaDataCQ AddOrderBy_Fa174_Asc() { regOBA("FA174");return this; }
        public BsTFaDataCQ AddOrderBy_Fa174_Desc() { regOBD("FA174");return this; }

        protected ConditionValue _fa175;
        public ConditionValue Fa175 {
            get { if (_fa175 == null) { _fa175 = new ConditionValue(); } return _fa175; }
        }
        protected override ConditionValue getCValueFa175() { return this.Fa175; }


        public BsTFaDataCQ AddOrderBy_Fa175_Asc() { regOBA("FA175");return this; }
        public BsTFaDataCQ AddOrderBy_Fa175_Desc() { regOBD("FA175");return this; }

        protected ConditionValue _fa176;
        public ConditionValue Fa176 {
            get { if (_fa176 == null) { _fa176 = new ConditionValue(); } return _fa176; }
        }
        protected override ConditionValue getCValueFa176() { return this.Fa176; }


        public BsTFaDataCQ AddOrderBy_Fa176_Asc() { regOBA("FA176");return this; }
        public BsTFaDataCQ AddOrderBy_Fa176_Desc() { regOBD("FA176");return this; }

        protected ConditionValue _fa177;
        public ConditionValue Fa177 {
            get { if (_fa177 == null) { _fa177 = new ConditionValue(); } return _fa177; }
        }
        protected override ConditionValue getCValueFa177() { return this.Fa177; }


        public BsTFaDataCQ AddOrderBy_Fa177_Asc() { regOBA("FA177");return this; }
        public BsTFaDataCQ AddOrderBy_Fa177_Desc() { regOBD("FA177");return this; }

        protected ConditionValue _fa178;
        public ConditionValue Fa178 {
            get { if (_fa178 == null) { _fa178 = new ConditionValue(); } return _fa178; }
        }
        protected override ConditionValue getCValueFa178() { return this.Fa178; }


        public BsTFaDataCQ AddOrderBy_Fa178_Asc() { regOBA("FA178");return this; }
        public BsTFaDataCQ AddOrderBy_Fa178_Desc() { regOBD("FA178");return this; }

        protected ConditionValue _fa179;
        public ConditionValue Fa179 {
            get { if (_fa179 == null) { _fa179 = new ConditionValue(); } return _fa179; }
        }
        protected override ConditionValue getCValueFa179() { return this.Fa179; }


        public BsTFaDataCQ AddOrderBy_Fa179_Asc() { regOBA("FA179");return this; }
        public BsTFaDataCQ AddOrderBy_Fa179_Desc() { regOBD("FA179");return this; }

        protected ConditionValue _fa180;
        public ConditionValue Fa180 {
            get { if (_fa180 == null) { _fa180 = new ConditionValue(); } return _fa180; }
        }
        protected override ConditionValue getCValueFa180() { return this.Fa180; }


        public BsTFaDataCQ AddOrderBy_Fa180_Asc() { regOBA("FA180");return this; }
        public BsTFaDataCQ AddOrderBy_Fa180_Desc() { regOBD("FA180");return this; }

        protected ConditionValue _fa181;
        public ConditionValue Fa181 {
            get { if (_fa181 == null) { _fa181 = new ConditionValue(); } return _fa181; }
        }
        protected override ConditionValue getCValueFa181() { return this.Fa181; }


        public BsTFaDataCQ AddOrderBy_Fa181_Asc() { regOBA("FA181");return this; }
        public BsTFaDataCQ AddOrderBy_Fa181_Desc() { regOBD("FA181");return this; }

        protected ConditionValue _fa182;
        public ConditionValue Fa182 {
            get { if (_fa182 == null) { _fa182 = new ConditionValue(); } return _fa182; }
        }
        protected override ConditionValue getCValueFa182() { return this.Fa182; }


        public BsTFaDataCQ AddOrderBy_Fa182_Asc() { regOBA("FA182");return this; }
        public BsTFaDataCQ AddOrderBy_Fa182_Desc() { regOBD("FA182");return this; }

        protected ConditionValue _fa183;
        public ConditionValue Fa183 {
            get { if (_fa183 == null) { _fa183 = new ConditionValue(); } return _fa183; }
        }
        protected override ConditionValue getCValueFa183() { return this.Fa183; }


        public BsTFaDataCQ AddOrderBy_Fa183_Asc() { regOBA("FA183");return this; }
        public BsTFaDataCQ AddOrderBy_Fa183_Desc() { regOBD("FA183");return this; }

        protected ConditionValue _fa184;
        public ConditionValue Fa184 {
            get { if (_fa184 == null) { _fa184 = new ConditionValue(); } return _fa184; }
        }
        protected override ConditionValue getCValueFa184() { return this.Fa184; }


        public BsTFaDataCQ AddOrderBy_Fa184_Asc() { regOBA("FA184");return this; }
        public BsTFaDataCQ AddOrderBy_Fa184_Desc() { regOBD("FA184");return this; }

        protected ConditionValue _fa185;
        public ConditionValue Fa185 {
            get { if (_fa185 == null) { _fa185 = new ConditionValue(); } return _fa185; }
        }
        protected override ConditionValue getCValueFa185() { return this.Fa185; }


        public BsTFaDataCQ AddOrderBy_Fa185_Asc() { regOBA("FA185");return this; }
        public BsTFaDataCQ AddOrderBy_Fa185_Desc() { regOBD("FA185");return this; }

        protected ConditionValue _fa186;
        public ConditionValue Fa186 {
            get { if (_fa186 == null) { _fa186 = new ConditionValue(); } return _fa186; }
        }
        protected override ConditionValue getCValueFa186() { return this.Fa186; }


        public BsTFaDataCQ AddOrderBy_Fa186_Asc() { regOBA("FA186");return this; }
        public BsTFaDataCQ AddOrderBy_Fa186_Desc() { regOBD("FA186");return this; }

        protected ConditionValue _fa187;
        public ConditionValue Fa187 {
            get { if (_fa187 == null) { _fa187 = new ConditionValue(); } return _fa187; }
        }
        protected override ConditionValue getCValueFa187() { return this.Fa187; }


        public BsTFaDataCQ AddOrderBy_Fa187_Asc() { regOBA("FA187");return this; }
        public BsTFaDataCQ AddOrderBy_Fa187_Desc() { regOBD("FA187");return this; }

        protected ConditionValue _fa188;
        public ConditionValue Fa188 {
            get { if (_fa188 == null) { _fa188 = new ConditionValue(); } return _fa188; }
        }
        protected override ConditionValue getCValueFa188() { return this.Fa188; }


        public BsTFaDataCQ AddOrderBy_Fa188_Asc() { regOBA("FA188");return this; }
        public BsTFaDataCQ AddOrderBy_Fa188_Desc() { regOBD("FA188");return this; }

        protected ConditionValue _fa189;
        public ConditionValue Fa189 {
            get { if (_fa189 == null) { _fa189 = new ConditionValue(); } return _fa189; }
        }
        protected override ConditionValue getCValueFa189() { return this.Fa189; }


        public BsTFaDataCQ AddOrderBy_Fa189_Asc() { regOBA("FA189");return this; }
        public BsTFaDataCQ AddOrderBy_Fa189_Desc() { regOBD("FA189");return this; }

        protected ConditionValue _fa190;
        public ConditionValue Fa190 {
            get { if (_fa190 == null) { _fa190 = new ConditionValue(); } return _fa190; }
        }
        protected override ConditionValue getCValueFa190() { return this.Fa190; }


        public BsTFaDataCQ AddOrderBy_Fa190_Asc() { regOBA("FA190");return this; }
        public BsTFaDataCQ AddOrderBy_Fa190_Desc() { regOBD("FA190");return this; }

        protected ConditionValue _fa191;
        public ConditionValue Fa191 {
            get { if (_fa191 == null) { _fa191 = new ConditionValue(); } return _fa191; }
        }
        protected override ConditionValue getCValueFa191() { return this.Fa191; }


        public BsTFaDataCQ AddOrderBy_Fa191_Asc() { regOBA("FA191");return this; }
        public BsTFaDataCQ AddOrderBy_Fa191_Desc() { regOBD("FA191");return this; }

        protected ConditionValue _fa192;
        public ConditionValue Fa192 {
            get { if (_fa192 == null) { _fa192 = new ConditionValue(); } return _fa192; }
        }
        protected override ConditionValue getCValueFa192() { return this.Fa192; }


        public BsTFaDataCQ AddOrderBy_Fa192_Asc() { regOBA("FA192");return this; }
        public BsTFaDataCQ AddOrderBy_Fa192_Desc() { regOBD("FA192");return this; }

        protected ConditionValue _fa193;
        public ConditionValue Fa193 {
            get { if (_fa193 == null) { _fa193 = new ConditionValue(); } return _fa193; }
        }
        protected override ConditionValue getCValueFa193() { return this.Fa193; }


        public BsTFaDataCQ AddOrderBy_Fa193_Asc() { regOBA("FA193");return this; }
        public BsTFaDataCQ AddOrderBy_Fa193_Desc() { regOBD("FA193");return this; }

        protected ConditionValue _fa194;
        public ConditionValue Fa194 {
            get { if (_fa194 == null) { _fa194 = new ConditionValue(); } return _fa194; }
        }
        protected override ConditionValue getCValueFa194() { return this.Fa194; }


        public BsTFaDataCQ AddOrderBy_Fa194_Asc() { regOBA("FA194");return this; }
        public BsTFaDataCQ AddOrderBy_Fa194_Desc() { regOBD("FA194");return this; }

        protected ConditionValue _fa195;
        public ConditionValue Fa195 {
            get { if (_fa195 == null) { _fa195 = new ConditionValue(); } return _fa195; }
        }
        protected override ConditionValue getCValueFa195() { return this.Fa195; }


        public BsTFaDataCQ AddOrderBy_Fa195_Asc() { regOBA("FA195");return this; }
        public BsTFaDataCQ AddOrderBy_Fa195_Desc() { regOBD("FA195");return this; }

        protected ConditionValue _fa196;
        public ConditionValue Fa196 {
            get { if (_fa196 == null) { _fa196 = new ConditionValue(); } return _fa196; }
        }
        protected override ConditionValue getCValueFa196() { return this.Fa196; }


        public BsTFaDataCQ AddOrderBy_Fa196_Asc() { regOBA("FA196");return this; }
        public BsTFaDataCQ AddOrderBy_Fa196_Desc() { regOBD("FA196");return this; }

        protected ConditionValue _fa197;
        public ConditionValue Fa197 {
            get { if (_fa197 == null) { _fa197 = new ConditionValue(); } return _fa197; }
        }
        protected override ConditionValue getCValueFa197() { return this.Fa197; }


        public BsTFaDataCQ AddOrderBy_Fa197_Asc() { regOBA("FA197");return this; }
        public BsTFaDataCQ AddOrderBy_Fa197_Desc() { regOBD("FA197");return this; }

        protected ConditionValue _fa198;
        public ConditionValue Fa198 {
            get { if (_fa198 == null) { _fa198 = new ConditionValue(); } return _fa198; }
        }
        protected override ConditionValue getCValueFa198() { return this.Fa198; }


        public BsTFaDataCQ AddOrderBy_Fa198_Asc() { regOBA("FA198");return this; }
        public BsTFaDataCQ AddOrderBy_Fa198_Desc() { regOBD("FA198");return this; }

        protected ConditionValue _fa199;
        public ConditionValue Fa199 {
            get { if (_fa199 == null) { _fa199 = new ConditionValue(); } return _fa199; }
        }
        protected override ConditionValue getCValueFa199() { return this.Fa199; }


        public BsTFaDataCQ AddOrderBy_Fa199_Asc() { regOBA("FA199");return this; }
        public BsTFaDataCQ AddOrderBy_Fa199_Desc() { regOBD("FA199");return this; }

        protected ConditionValue _fa200;
        public ConditionValue Fa200 {
            get { if (_fa200 == null) { _fa200 = new ConditionValue(); } return _fa200; }
        }
        protected override ConditionValue getCValueFa200() { return this.Fa200; }


        public BsTFaDataCQ AddOrderBy_Fa200_Asc() { regOBA("FA200");return this; }
        public BsTFaDataCQ AddOrderBy_Fa200_Desc() { regOBD("FA200");return this; }

        public BsTFaDataCQ AddSpecifiedDerivedOrderBy_Asc(String aliasName) { registerSpecifiedDerivedOrderBy_Asc(aliasName); return this; }
        public BsTFaDataCQ AddSpecifiedDerivedOrderBy_Desc(String aliasName) { registerSpecifiedDerivedOrderBy_Desc(aliasName); return this; }

        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {

        }
    


	    // ===============================================================================
	    //                                                                 Scalar SubQuery
	    //                                                                 ===============
	    protected Map<String, TFaDataCQ> _scalarSubQueryMap;
	    public Map<String, TFaDataCQ> ScalarSubQuery { get { return _scalarSubQueryMap; } }
	    public override String keepScalarSubQuery(TFaDataCQ subQuery) {
	        if (_scalarSubQueryMap == null) { _scalarSubQueryMap = new LinkedHashMap<String, TFaDataCQ>(); }
	        String key = "subQueryMapKey" + (_scalarSubQueryMap.size() + 1);
	        _scalarSubQueryMap.put(key, subQuery); return "ScalarSubQuery." + key;
	    }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        protected Map<String, TFaDataCQ> _myselfInScopeSubQueryMap;
        public Map<String, TFaDataCQ> MyselfInScopeSubQuery { get { return _myselfInScopeSubQueryMap; } }
        public override String keepMyselfInScopeSubQuery(TFaDataCQ subQuery) {
            if (_myselfInScopeSubQueryMap == null) { _myselfInScopeSubQueryMap = new LinkedHashMap<String, TFaDataCQ>(); }
            String key = "subQueryMapKey" + (_myselfInScopeSubQueryMap.size() + 1);
            _myselfInScopeSubQueryMap.put(key, subQuery); return "MyselfInScopeSubQuery." + key;
        }
    }
}
