
using System;

using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.CKey;
using Macromill.QCWeb.Dao.AllCommon.CBean.COption;
using Macromill.QCWeb.Dao.AllCommon.CBean.CValue;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.CBean.CQ.BS;
using Macromill.QCWeb.Dao.CBean.CQ;

namespace Macromill.QCWeb.Dao.CBean.CQ.Ciq {

    [System.Serializable]
    public class TFaDataCIQ : AbstractBsTFaDataCQ {

        // ===============================================================================
        //                                                                       Attribute
        //                                                                       =========
        protected BsTFaDataCQ _myCQ;

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        public TFaDataCIQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel, BsTFaDataCQ myCQ)
            : base(childQuery, sqlClause, aliasName, nestLevel) {
            _myCQ = myCQ;
            _foreignPropertyName = _myCQ.xgetForeignPropertyName();// Accept foreign property name.
            _relationPath = _myCQ.xgetRelationPath();// Accept relation path.
        }

        // ===================================================================================
        //                                                             Override about Register
        //                                                             =======================
        public override void reflectRelationOnUnionQuery(ConditionQuery baseQueryAsSuper, ConditionQuery unionQueryAsSuper) {
            throw new UnsupportedOperationException("InlineQuery must not need UNION method: " + baseQueryAsSuper + " : " + unionQueryAsSuper);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue, String colName) {
            regIQ(key, value, cvalue, colName);
        }
    
        protected override void setupConditionValueAndRegisterWhereClause(ConditionKey key, Object value, ConditionValue cvalue
                                                                        , String colName, ConditionOption option) {
            regIQ(key, value, cvalue, colName, option);
        }
    
        protected override void registerWhereClause(String whereClause) {
            registerInlineWhereClause(whereClause);
        }
    
        protected override String getInScopeSubQueryRealColumnName(String columnName) {
            if (_onClause) {
                throw new UnsupportedOperationException("InScopeSubQuery of on-clause is unsupported");
            }
            return _onClause ? xgetAliasName() + "." + columnName : columnName;
        }
    
        protected override void registerExistsSubQuery(ConditionQuery subQuery
                                     , String columnName, String relatedColumnName, String propertyName) {
            throw new UnsupportedOperationException("Sorry! ExistsSubQuery at inline view is unsupported. So please use InScopeSubQyery.");
        }


        protected override ConditionValue getCValueSampleId() {
            return _myCQ.SampleId;
        }


        protected override ConditionValue getCValueFa001() {
            return _myCQ.Fa001;
        }


        protected override ConditionValue getCValueFa002() {
            return _myCQ.Fa002;
        }


        protected override ConditionValue getCValueFa003() {
            return _myCQ.Fa003;
        }


        protected override ConditionValue getCValueFa004() {
            return _myCQ.Fa004;
        }


        protected override ConditionValue getCValueFa005() {
            return _myCQ.Fa005;
        }


        protected override ConditionValue getCValueFa006() {
            return _myCQ.Fa006;
        }


        protected override ConditionValue getCValueFa007() {
            return _myCQ.Fa007;
        }


        protected override ConditionValue getCValueFa008() {
            return _myCQ.Fa008;
        }


        protected override ConditionValue getCValueFa009() {
            return _myCQ.Fa009;
        }


        protected override ConditionValue getCValueFa010() {
            return _myCQ.Fa010;
        }


        protected override ConditionValue getCValueFa011() {
            return _myCQ.Fa011;
        }


        protected override ConditionValue getCValueFa012() {
            return _myCQ.Fa012;
        }


        protected override ConditionValue getCValueFa013() {
            return _myCQ.Fa013;
        }


        protected override ConditionValue getCValueFa014() {
            return _myCQ.Fa014;
        }


        protected override ConditionValue getCValueFa015() {
            return _myCQ.Fa015;
        }


        protected override ConditionValue getCValueFa016() {
            return _myCQ.Fa016;
        }


        protected override ConditionValue getCValueFa017() {
            return _myCQ.Fa017;
        }


        protected override ConditionValue getCValueFa018() {
            return _myCQ.Fa018;
        }


        protected override ConditionValue getCValueFa019() {
            return _myCQ.Fa019;
        }


        protected override ConditionValue getCValueFa020() {
            return _myCQ.Fa020;
        }


        protected override ConditionValue getCValueFa021() {
            return _myCQ.Fa021;
        }


        protected override ConditionValue getCValueFa022() {
            return _myCQ.Fa022;
        }


        protected override ConditionValue getCValueFa023() {
            return _myCQ.Fa023;
        }


        protected override ConditionValue getCValueFa024() {
            return _myCQ.Fa024;
        }


        protected override ConditionValue getCValueFa025() {
            return _myCQ.Fa025;
        }


        protected override ConditionValue getCValueFa026() {
            return _myCQ.Fa026;
        }


        protected override ConditionValue getCValueFa027() {
            return _myCQ.Fa027;
        }


        protected override ConditionValue getCValueFa028() {
            return _myCQ.Fa028;
        }


        protected override ConditionValue getCValueFa029() {
            return _myCQ.Fa029;
        }


        protected override ConditionValue getCValueFa030() {
            return _myCQ.Fa030;
        }


        protected override ConditionValue getCValueFa031() {
            return _myCQ.Fa031;
        }


        protected override ConditionValue getCValueFa032() {
            return _myCQ.Fa032;
        }


        protected override ConditionValue getCValueFa033() {
            return _myCQ.Fa033;
        }


        protected override ConditionValue getCValueFa034() {
            return _myCQ.Fa034;
        }


        protected override ConditionValue getCValueFa035() {
            return _myCQ.Fa035;
        }


        protected override ConditionValue getCValueFa036() {
            return _myCQ.Fa036;
        }


        protected override ConditionValue getCValueFa037() {
            return _myCQ.Fa037;
        }


        protected override ConditionValue getCValueFa038() {
            return _myCQ.Fa038;
        }


        protected override ConditionValue getCValueFa039() {
            return _myCQ.Fa039;
        }


        protected override ConditionValue getCValueFa040() {
            return _myCQ.Fa040;
        }


        protected override ConditionValue getCValueFa041() {
            return _myCQ.Fa041;
        }


        protected override ConditionValue getCValueFa042() {
            return _myCQ.Fa042;
        }


        protected override ConditionValue getCValueFa043() {
            return _myCQ.Fa043;
        }


        protected override ConditionValue getCValueFa044() {
            return _myCQ.Fa044;
        }


        protected override ConditionValue getCValueFa045() {
            return _myCQ.Fa045;
        }


        protected override ConditionValue getCValueFa046() {
            return _myCQ.Fa046;
        }


        protected override ConditionValue getCValueFa047() {
            return _myCQ.Fa047;
        }


        protected override ConditionValue getCValueFa048() {
            return _myCQ.Fa048;
        }


        protected override ConditionValue getCValueFa049() {
            return _myCQ.Fa049;
        }


        protected override ConditionValue getCValueFa050() {
            return _myCQ.Fa050;
        }


        protected override ConditionValue getCValueFa051() {
            return _myCQ.Fa051;
        }


        protected override ConditionValue getCValueFa052() {
            return _myCQ.Fa052;
        }


        protected override ConditionValue getCValueFa053() {
            return _myCQ.Fa053;
        }


        protected override ConditionValue getCValueFa054() {
            return _myCQ.Fa054;
        }


        protected override ConditionValue getCValueFa055() {
            return _myCQ.Fa055;
        }


        protected override ConditionValue getCValueFa056() {
            return _myCQ.Fa056;
        }


        protected override ConditionValue getCValueFa057() {
            return _myCQ.Fa057;
        }


        protected override ConditionValue getCValueFa058() {
            return _myCQ.Fa058;
        }


        protected override ConditionValue getCValueFa059() {
            return _myCQ.Fa059;
        }


        protected override ConditionValue getCValueFa060() {
            return _myCQ.Fa060;
        }


        protected override ConditionValue getCValueFa061() {
            return _myCQ.Fa061;
        }


        protected override ConditionValue getCValueFa062() {
            return _myCQ.Fa062;
        }


        protected override ConditionValue getCValueFa063() {
            return _myCQ.Fa063;
        }


        protected override ConditionValue getCValueFa064() {
            return _myCQ.Fa064;
        }


        protected override ConditionValue getCValueFa065() {
            return _myCQ.Fa065;
        }


        protected override ConditionValue getCValueFa066() {
            return _myCQ.Fa066;
        }


        protected override ConditionValue getCValueFa067() {
            return _myCQ.Fa067;
        }


        protected override ConditionValue getCValueFa068() {
            return _myCQ.Fa068;
        }


        protected override ConditionValue getCValueFa069() {
            return _myCQ.Fa069;
        }


        protected override ConditionValue getCValueFa070() {
            return _myCQ.Fa070;
        }


        protected override ConditionValue getCValueFa071() {
            return _myCQ.Fa071;
        }


        protected override ConditionValue getCValueFa072() {
            return _myCQ.Fa072;
        }


        protected override ConditionValue getCValueFa073() {
            return _myCQ.Fa073;
        }


        protected override ConditionValue getCValueFa074() {
            return _myCQ.Fa074;
        }


        protected override ConditionValue getCValueFa075() {
            return _myCQ.Fa075;
        }


        protected override ConditionValue getCValueFa076() {
            return _myCQ.Fa076;
        }


        protected override ConditionValue getCValueFa077() {
            return _myCQ.Fa077;
        }


        protected override ConditionValue getCValueFa078() {
            return _myCQ.Fa078;
        }


        protected override ConditionValue getCValueFa079() {
            return _myCQ.Fa079;
        }


        protected override ConditionValue getCValueFa080() {
            return _myCQ.Fa080;
        }


        protected override ConditionValue getCValueFa081() {
            return _myCQ.Fa081;
        }


        protected override ConditionValue getCValueFa082() {
            return _myCQ.Fa082;
        }


        protected override ConditionValue getCValueFa083() {
            return _myCQ.Fa083;
        }


        protected override ConditionValue getCValueFa084() {
            return _myCQ.Fa084;
        }


        protected override ConditionValue getCValueFa085() {
            return _myCQ.Fa085;
        }


        protected override ConditionValue getCValueFa086() {
            return _myCQ.Fa086;
        }


        protected override ConditionValue getCValueFa087() {
            return _myCQ.Fa087;
        }


        protected override ConditionValue getCValueFa088() {
            return _myCQ.Fa088;
        }


        protected override ConditionValue getCValueFa089() {
            return _myCQ.Fa089;
        }


        protected override ConditionValue getCValueFa090() {
            return _myCQ.Fa090;
        }


        protected override ConditionValue getCValueFa091() {
            return _myCQ.Fa091;
        }


        protected override ConditionValue getCValueFa092() {
            return _myCQ.Fa092;
        }


        protected override ConditionValue getCValueFa093() {
            return _myCQ.Fa093;
        }


        protected override ConditionValue getCValueFa094() {
            return _myCQ.Fa094;
        }


        protected override ConditionValue getCValueFa095() {
            return _myCQ.Fa095;
        }


        protected override ConditionValue getCValueFa096() {
            return _myCQ.Fa096;
        }


        protected override ConditionValue getCValueFa097() {
            return _myCQ.Fa097;
        }


        protected override ConditionValue getCValueFa098() {
            return _myCQ.Fa098;
        }


        protected override ConditionValue getCValueFa099() {
            return _myCQ.Fa099;
        }


        protected override ConditionValue getCValueFa100() {
            return _myCQ.Fa100;
        }


        protected override ConditionValue getCValueFa101() {
            return _myCQ.Fa101;
        }


        protected override ConditionValue getCValueFa102() {
            return _myCQ.Fa102;
        }


        protected override ConditionValue getCValueFa103() {
            return _myCQ.Fa103;
        }


        protected override ConditionValue getCValueFa104() {
            return _myCQ.Fa104;
        }


        protected override ConditionValue getCValueFa105() {
            return _myCQ.Fa105;
        }


        protected override ConditionValue getCValueFa106() {
            return _myCQ.Fa106;
        }


        protected override ConditionValue getCValueFa107() {
            return _myCQ.Fa107;
        }


        protected override ConditionValue getCValueFa108() {
            return _myCQ.Fa108;
        }


        protected override ConditionValue getCValueFa109() {
            return _myCQ.Fa109;
        }


        protected override ConditionValue getCValueFa110() {
            return _myCQ.Fa110;
        }


        protected override ConditionValue getCValueFa111() {
            return _myCQ.Fa111;
        }


        protected override ConditionValue getCValueFa112() {
            return _myCQ.Fa112;
        }


        protected override ConditionValue getCValueFa113() {
            return _myCQ.Fa113;
        }


        protected override ConditionValue getCValueFa114() {
            return _myCQ.Fa114;
        }


        protected override ConditionValue getCValueFa115() {
            return _myCQ.Fa115;
        }


        protected override ConditionValue getCValueFa116() {
            return _myCQ.Fa116;
        }


        protected override ConditionValue getCValueFa117() {
            return _myCQ.Fa117;
        }


        protected override ConditionValue getCValueFa118() {
            return _myCQ.Fa118;
        }


        protected override ConditionValue getCValueFa119() {
            return _myCQ.Fa119;
        }


        protected override ConditionValue getCValueFa120() {
            return _myCQ.Fa120;
        }


        protected override ConditionValue getCValueFa121() {
            return _myCQ.Fa121;
        }


        protected override ConditionValue getCValueFa122() {
            return _myCQ.Fa122;
        }


        protected override ConditionValue getCValueFa123() {
            return _myCQ.Fa123;
        }


        protected override ConditionValue getCValueFa124() {
            return _myCQ.Fa124;
        }


        protected override ConditionValue getCValueFa125() {
            return _myCQ.Fa125;
        }


        protected override ConditionValue getCValueFa126() {
            return _myCQ.Fa126;
        }


        protected override ConditionValue getCValueFa127() {
            return _myCQ.Fa127;
        }


        protected override ConditionValue getCValueFa128() {
            return _myCQ.Fa128;
        }


        protected override ConditionValue getCValueFa129() {
            return _myCQ.Fa129;
        }


        protected override ConditionValue getCValueFa130() {
            return _myCQ.Fa130;
        }


        protected override ConditionValue getCValueFa131() {
            return _myCQ.Fa131;
        }


        protected override ConditionValue getCValueFa132() {
            return _myCQ.Fa132;
        }


        protected override ConditionValue getCValueFa133() {
            return _myCQ.Fa133;
        }


        protected override ConditionValue getCValueFa134() {
            return _myCQ.Fa134;
        }


        protected override ConditionValue getCValueFa135() {
            return _myCQ.Fa135;
        }


        protected override ConditionValue getCValueFa136() {
            return _myCQ.Fa136;
        }


        protected override ConditionValue getCValueFa137() {
            return _myCQ.Fa137;
        }


        protected override ConditionValue getCValueFa138() {
            return _myCQ.Fa138;
        }


        protected override ConditionValue getCValueFa139() {
            return _myCQ.Fa139;
        }


        protected override ConditionValue getCValueFa140() {
            return _myCQ.Fa140;
        }


        protected override ConditionValue getCValueFa141() {
            return _myCQ.Fa141;
        }


        protected override ConditionValue getCValueFa142() {
            return _myCQ.Fa142;
        }


        protected override ConditionValue getCValueFa143() {
            return _myCQ.Fa143;
        }


        protected override ConditionValue getCValueFa144() {
            return _myCQ.Fa144;
        }


        protected override ConditionValue getCValueFa145() {
            return _myCQ.Fa145;
        }


        protected override ConditionValue getCValueFa146() {
            return _myCQ.Fa146;
        }


        protected override ConditionValue getCValueFa147() {
            return _myCQ.Fa147;
        }


        protected override ConditionValue getCValueFa148() {
            return _myCQ.Fa148;
        }


        protected override ConditionValue getCValueFa149() {
            return _myCQ.Fa149;
        }


        protected override ConditionValue getCValueFa150() {
            return _myCQ.Fa150;
        }


        protected override ConditionValue getCValueFa151() {
            return _myCQ.Fa151;
        }


        protected override ConditionValue getCValueFa152() {
            return _myCQ.Fa152;
        }


        protected override ConditionValue getCValueFa153() {
            return _myCQ.Fa153;
        }


        protected override ConditionValue getCValueFa154() {
            return _myCQ.Fa154;
        }


        protected override ConditionValue getCValueFa155() {
            return _myCQ.Fa155;
        }


        protected override ConditionValue getCValueFa156() {
            return _myCQ.Fa156;
        }


        protected override ConditionValue getCValueFa157() {
            return _myCQ.Fa157;
        }


        protected override ConditionValue getCValueFa158() {
            return _myCQ.Fa158;
        }


        protected override ConditionValue getCValueFa159() {
            return _myCQ.Fa159;
        }


        protected override ConditionValue getCValueFa160() {
            return _myCQ.Fa160;
        }


        protected override ConditionValue getCValueFa161() {
            return _myCQ.Fa161;
        }


        protected override ConditionValue getCValueFa162() {
            return _myCQ.Fa162;
        }


        protected override ConditionValue getCValueFa163() {
            return _myCQ.Fa163;
        }


        protected override ConditionValue getCValueFa164() {
            return _myCQ.Fa164;
        }


        protected override ConditionValue getCValueFa165() {
            return _myCQ.Fa165;
        }


        protected override ConditionValue getCValueFa166() {
            return _myCQ.Fa166;
        }


        protected override ConditionValue getCValueFa167() {
            return _myCQ.Fa167;
        }


        protected override ConditionValue getCValueFa168() {
            return _myCQ.Fa168;
        }


        protected override ConditionValue getCValueFa169() {
            return _myCQ.Fa169;
        }


        protected override ConditionValue getCValueFa170() {
            return _myCQ.Fa170;
        }


        protected override ConditionValue getCValueFa171() {
            return _myCQ.Fa171;
        }


        protected override ConditionValue getCValueFa172() {
            return _myCQ.Fa172;
        }


        protected override ConditionValue getCValueFa173() {
            return _myCQ.Fa173;
        }


        protected override ConditionValue getCValueFa174() {
            return _myCQ.Fa174;
        }


        protected override ConditionValue getCValueFa175() {
            return _myCQ.Fa175;
        }


        protected override ConditionValue getCValueFa176() {
            return _myCQ.Fa176;
        }


        protected override ConditionValue getCValueFa177() {
            return _myCQ.Fa177;
        }


        protected override ConditionValue getCValueFa178() {
            return _myCQ.Fa178;
        }


        protected override ConditionValue getCValueFa179() {
            return _myCQ.Fa179;
        }


        protected override ConditionValue getCValueFa180() {
            return _myCQ.Fa180;
        }


        protected override ConditionValue getCValueFa181() {
            return _myCQ.Fa181;
        }


        protected override ConditionValue getCValueFa182() {
            return _myCQ.Fa182;
        }


        protected override ConditionValue getCValueFa183() {
            return _myCQ.Fa183;
        }


        protected override ConditionValue getCValueFa184() {
            return _myCQ.Fa184;
        }


        protected override ConditionValue getCValueFa185() {
            return _myCQ.Fa185;
        }


        protected override ConditionValue getCValueFa186() {
            return _myCQ.Fa186;
        }


        protected override ConditionValue getCValueFa187() {
            return _myCQ.Fa187;
        }


        protected override ConditionValue getCValueFa188() {
            return _myCQ.Fa188;
        }


        protected override ConditionValue getCValueFa189() {
            return _myCQ.Fa189;
        }


        protected override ConditionValue getCValueFa190() {
            return _myCQ.Fa190;
        }


        protected override ConditionValue getCValueFa191() {
            return _myCQ.Fa191;
        }


        protected override ConditionValue getCValueFa192() {
            return _myCQ.Fa192;
        }


        protected override ConditionValue getCValueFa193() {
            return _myCQ.Fa193;
        }


        protected override ConditionValue getCValueFa194() {
            return _myCQ.Fa194;
        }


        protected override ConditionValue getCValueFa195() {
            return _myCQ.Fa195;
        }


        protected override ConditionValue getCValueFa196() {
            return _myCQ.Fa196;
        }


        protected override ConditionValue getCValueFa197() {
            return _myCQ.Fa197;
        }


        protected override ConditionValue getCValueFa198() {
            return _myCQ.Fa198;
        }


        protected override ConditionValue getCValueFa199() {
            return _myCQ.Fa199;
        }


        protected override ConditionValue getCValueFa200() {
            return _myCQ.Fa200;
        }


        // ===================================================================================
        //                                                                     Scalar SubQuery
        //                                                                     ===============
        public override String keepScalarSubQuery(TFaDataCQ subQuery) {
            throw new UnsupportedOperationException("ScalarSubQuery at inline() is unsupported! Sorry!");
        }

        // ===============================================================================
        //                                                         Myself InScope SubQuery
        //                                                         =======================
        public override String keepMyselfInScopeSubQuery(TFaDataCQ subQuery) {
            throw new UnsupportedOperationException("MyselfInScopeSubQuery at inline() is unsupported! Sorry!");
        }
    }
}
