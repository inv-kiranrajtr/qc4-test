
using System;
using System.Reflection;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Macromill.QCWeb.Dao.AllCommon.JavaLike;
using Macromill.QCWeb.Dao.ExEntity;

using Macromill.QCWeb.Dao.ExDao;
using Macromill.QCWeb.Dao.CBean;

namespace Macromill.QCWeb.Dao.BsEntity.Dbm {

    public class TFaDataDbm : AbstractDBMeta {

        public static readonly Type ENTITY_TYPE = typeof(TFaData);

        private static readonly TFaDataDbm _instance = new TFaDataDbm();
        private TFaDataDbm() {
            InitializeColumnInfo();
            InitializeColumnInfoList();
            InitializeEntityPropertySetupper();
        }
        public static TFaDataDbm GetInstance() {
            return _instance;
        }

        // ===============================================================================
        //                                                                      Table Info
        //                                                                      ==========
        public override String TableDbName { get { return "T_FA_DATA"; } }
        public override String TablePropertyName { get { return "TFaData"; } }
        public override String TableSqlName { get { return "T_FA_DATA"; } }

        // ===============================================================================
        //                                                                     Column Info
        //                                                                     ===========
        protected ColumnInfo _columnSampleId;
        protected ColumnInfo _columnFa001;
        protected ColumnInfo _columnFa002;
        protected ColumnInfo _columnFa003;
        protected ColumnInfo _columnFa004;
        protected ColumnInfo _columnFa005;
        protected ColumnInfo _columnFa006;
        protected ColumnInfo _columnFa007;
        protected ColumnInfo _columnFa008;
        protected ColumnInfo _columnFa009;
        protected ColumnInfo _columnFa010;
        protected ColumnInfo _columnFa011;
        protected ColumnInfo _columnFa012;
        protected ColumnInfo _columnFa013;
        protected ColumnInfo _columnFa014;
        protected ColumnInfo _columnFa015;
        protected ColumnInfo _columnFa016;
        protected ColumnInfo _columnFa017;
        protected ColumnInfo _columnFa018;
        protected ColumnInfo _columnFa019;
        protected ColumnInfo _columnFa020;
        protected ColumnInfo _columnFa021;
        protected ColumnInfo _columnFa022;
        protected ColumnInfo _columnFa023;
        protected ColumnInfo _columnFa024;
        protected ColumnInfo _columnFa025;
        protected ColumnInfo _columnFa026;
        protected ColumnInfo _columnFa027;
        protected ColumnInfo _columnFa028;
        protected ColumnInfo _columnFa029;
        protected ColumnInfo _columnFa030;
        protected ColumnInfo _columnFa031;
        protected ColumnInfo _columnFa032;
        protected ColumnInfo _columnFa033;
        protected ColumnInfo _columnFa034;
        protected ColumnInfo _columnFa035;
        protected ColumnInfo _columnFa036;
        protected ColumnInfo _columnFa037;
        protected ColumnInfo _columnFa038;
        protected ColumnInfo _columnFa039;
        protected ColumnInfo _columnFa040;
        protected ColumnInfo _columnFa041;
        protected ColumnInfo _columnFa042;
        protected ColumnInfo _columnFa043;
        protected ColumnInfo _columnFa044;
        protected ColumnInfo _columnFa045;
        protected ColumnInfo _columnFa046;
        protected ColumnInfo _columnFa047;
        protected ColumnInfo _columnFa048;
        protected ColumnInfo _columnFa049;
        protected ColumnInfo _columnFa050;
        protected ColumnInfo _columnFa051;
        protected ColumnInfo _columnFa052;
        protected ColumnInfo _columnFa053;
        protected ColumnInfo _columnFa054;
        protected ColumnInfo _columnFa055;
        protected ColumnInfo _columnFa056;
        protected ColumnInfo _columnFa057;
        protected ColumnInfo _columnFa058;
        protected ColumnInfo _columnFa059;
        protected ColumnInfo _columnFa060;
        protected ColumnInfo _columnFa061;
        protected ColumnInfo _columnFa062;
        protected ColumnInfo _columnFa063;
        protected ColumnInfo _columnFa064;
        protected ColumnInfo _columnFa065;
        protected ColumnInfo _columnFa066;
        protected ColumnInfo _columnFa067;
        protected ColumnInfo _columnFa068;
        protected ColumnInfo _columnFa069;
        protected ColumnInfo _columnFa070;
        protected ColumnInfo _columnFa071;
        protected ColumnInfo _columnFa072;
        protected ColumnInfo _columnFa073;
        protected ColumnInfo _columnFa074;
        protected ColumnInfo _columnFa075;
        protected ColumnInfo _columnFa076;
        protected ColumnInfo _columnFa077;
        protected ColumnInfo _columnFa078;
        protected ColumnInfo _columnFa079;
        protected ColumnInfo _columnFa080;
        protected ColumnInfo _columnFa081;
        protected ColumnInfo _columnFa082;
        protected ColumnInfo _columnFa083;
        protected ColumnInfo _columnFa084;
        protected ColumnInfo _columnFa085;
        protected ColumnInfo _columnFa086;
        protected ColumnInfo _columnFa087;
        protected ColumnInfo _columnFa088;
        protected ColumnInfo _columnFa089;
        protected ColumnInfo _columnFa090;
        protected ColumnInfo _columnFa091;
        protected ColumnInfo _columnFa092;
        protected ColumnInfo _columnFa093;
        protected ColumnInfo _columnFa094;
        protected ColumnInfo _columnFa095;
        protected ColumnInfo _columnFa096;
        protected ColumnInfo _columnFa097;
        protected ColumnInfo _columnFa098;
        protected ColumnInfo _columnFa099;
        protected ColumnInfo _columnFa100;
        protected ColumnInfo _columnFa101;
        protected ColumnInfo _columnFa102;
        protected ColumnInfo _columnFa103;
        protected ColumnInfo _columnFa104;
        protected ColumnInfo _columnFa105;
        protected ColumnInfo _columnFa106;
        protected ColumnInfo _columnFa107;
        protected ColumnInfo _columnFa108;
        protected ColumnInfo _columnFa109;
        protected ColumnInfo _columnFa110;
        protected ColumnInfo _columnFa111;
        protected ColumnInfo _columnFa112;
        protected ColumnInfo _columnFa113;
        protected ColumnInfo _columnFa114;
        protected ColumnInfo _columnFa115;
        protected ColumnInfo _columnFa116;
        protected ColumnInfo _columnFa117;
        protected ColumnInfo _columnFa118;
        protected ColumnInfo _columnFa119;
        protected ColumnInfo _columnFa120;
        protected ColumnInfo _columnFa121;
        protected ColumnInfo _columnFa122;
        protected ColumnInfo _columnFa123;
        protected ColumnInfo _columnFa124;
        protected ColumnInfo _columnFa125;
        protected ColumnInfo _columnFa126;
        protected ColumnInfo _columnFa127;
        protected ColumnInfo _columnFa128;
        protected ColumnInfo _columnFa129;
        protected ColumnInfo _columnFa130;
        protected ColumnInfo _columnFa131;
        protected ColumnInfo _columnFa132;
        protected ColumnInfo _columnFa133;
        protected ColumnInfo _columnFa134;
        protected ColumnInfo _columnFa135;
        protected ColumnInfo _columnFa136;
        protected ColumnInfo _columnFa137;
        protected ColumnInfo _columnFa138;
        protected ColumnInfo _columnFa139;
        protected ColumnInfo _columnFa140;
        protected ColumnInfo _columnFa141;
        protected ColumnInfo _columnFa142;
        protected ColumnInfo _columnFa143;
        protected ColumnInfo _columnFa144;
        protected ColumnInfo _columnFa145;
        protected ColumnInfo _columnFa146;
        protected ColumnInfo _columnFa147;
        protected ColumnInfo _columnFa148;
        protected ColumnInfo _columnFa149;
        protected ColumnInfo _columnFa150;
        protected ColumnInfo _columnFa151;
        protected ColumnInfo _columnFa152;
        protected ColumnInfo _columnFa153;
        protected ColumnInfo _columnFa154;
        protected ColumnInfo _columnFa155;
        protected ColumnInfo _columnFa156;
        protected ColumnInfo _columnFa157;
        protected ColumnInfo _columnFa158;
        protected ColumnInfo _columnFa159;
        protected ColumnInfo _columnFa160;
        protected ColumnInfo _columnFa161;
        protected ColumnInfo _columnFa162;
        protected ColumnInfo _columnFa163;
        protected ColumnInfo _columnFa164;
        protected ColumnInfo _columnFa165;
        protected ColumnInfo _columnFa166;
        protected ColumnInfo _columnFa167;
        protected ColumnInfo _columnFa168;
        protected ColumnInfo _columnFa169;
        protected ColumnInfo _columnFa170;
        protected ColumnInfo _columnFa171;
        protected ColumnInfo _columnFa172;
        protected ColumnInfo _columnFa173;
        protected ColumnInfo _columnFa174;
        protected ColumnInfo _columnFa175;
        protected ColumnInfo _columnFa176;
        protected ColumnInfo _columnFa177;
        protected ColumnInfo _columnFa178;
        protected ColumnInfo _columnFa179;
        protected ColumnInfo _columnFa180;
        protected ColumnInfo _columnFa181;
        protected ColumnInfo _columnFa182;
        protected ColumnInfo _columnFa183;
        protected ColumnInfo _columnFa184;
        protected ColumnInfo _columnFa185;
        protected ColumnInfo _columnFa186;
        protected ColumnInfo _columnFa187;
        protected ColumnInfo _columnFa188;
        protected ColumnInfo _columnFa189;
        protected ColumnInfo _columnFa190;
        protected ColumnInfo _columnFa191;
        protected ColumnInfo _columnFa192;
        protected ColumnInfo _columnFa193;
        protected ColumnInfo _columnFa194;
        protected ColumnInfo _columnFa195;
        protected ColumnInfo _columnFa196;
        protected ColumnInfo _columnFa197;
        protected ColumnInfo _columnFa198;
        protected ColumnInfo _columnFa199;
        protected ColumnInfo _columnFa200;

        public ColumnInfo ColumnSampleId { get { return _columnSampleId; } }
        public ColumnInfo ColumnFa001 { get { return _columnFa001; } }
        public ColumnInfo ColumnFa002 { get { return _columnFa002; } }
        public ColumnInfo ColumnFa003 { get { return _columnFa003; } }
        public ColumnInfo ColumnFa004 { get { return _columnFa004; } }
        public ColumnInfo ColumnFa005 { get { return _columnFa005; } }
        public ColumnInfo ColumnFa006 { get { return _columnFa006; } }
        public ColumnInfo ColumnFa007 { get { return _columnFa007; } }
        public ColumnInfo ColumnFa008 { get { return _columnFa008; } }
        public ColumnInfo ColumnFa009 { get { return _columnFa009; } }
        public ColumnInfo ColumnFa010 { get { return _columnFa010; } }
        public ColumnInfo ColumnFa011 { get { return _columnFa011; } }
        public ColumnInfo ColumnFa012 { get { return _columnFa012; } }
        public ColumnInfo ColumnFa013 { get { return _columnFa013; } }
        public ColumnInfo ColumnFa014 { get { return _columnFa014; } }
        public ColumnInfo ColumnFa015 { get { return _columnFa015; } }
        public ColumnInfo ColumnFa016 { get { return _columnFa016; } }
        public ColumnInfo ColumnFa017 { get { return _columnFa017; } }
        public ColumnInfo ColumnFa018 { get { return _columnFa018; } }
        public ColumnInfo ColumnFa019 { get { return _columnFa019; } }
        public ColumnInfo ColumnFa020 { get { return _columnFa020; } }
        public ColumnInfo ColumnFa021 { get { return _columnFa021; } }
        public ColumnInfo ColumnFa022 { get { return _columnFa022; } }
        public ColumnInfo ColumnFa023 { get { return _columnFa023; } }
        public ColumnInfo ColumnFa024 { get { return _columnFa024; } }
        public ColumnInfo ColumnFa025 { get { return _columnFa025; } }
        public ColumnInfo ColumnFa026 { get { return _columnFa026; } }
        public ColumnInfo ColumnFa027 { get { return _columnFa027; } }
        public ColumnInfo ColumnFa028 { get { return _columnFa028; } }
        public ColumnInfo ColumnFa029 { get { return _columnFa029; } }
        public ColumnInfo ColumnFa030 { get { return _columnFa030; } }
        public ColumnInfo ColumnFa031 { get { return _columnFa031; } }
        public ColumnInfo ColumnFa032 { get { return _columnFa032; } }
        public ColumnInfo ColumnFa033 { get { return _columnFa033; } }
        public ColumnInfo ColumnFa034 { get { return _columnFa034; } }
        public ColumnInfo ColumnFa035 { get { return _columnFa035; } }
        public ColumnInfo ColumnFa036 { get { return _columnFa036; } }
        public ColumnInfo ColumnFa037 { get { return _columnFa037; } }
        public ColumnInfo ColumnFa038 { get { return _columnFa038; } }
        public ColumnInfo ColumnFa039 { get { return _columnFa039; } }
        public ColumnInfo ColumnFa040 { get { return _columnFa040; } }
        public ColumnInfo ColumnFa041 { get { return _columnFa041; } }
        public ColumnInfo ColumnFa042 { get { return _columnFa042; } }
        public ColumnInfo ColumnFa043 { get { return _columnFa043; } }
        public ColumnInfo ColumnFa044 { get { return _columnFa044; } }
        public ColumnInfo ColumnFa045 { get { return _columnFa045; } }
        public ColumnInfo ColumnFa046 { get { return _columnFa046; } }
        public ColumnInfo ColumnFa047 { get { return _columnFa047; } }
        public ColumnInfo ColumnFa048 { get { return _columnFa048; } }
        public ColumnInfo ColumnFa049 { get { return _columnFa049; } }
        public ColumnInfo ColumnFa050 { get { return _columnFa050; } }
        public ColumnInfo ColumnFa051 { get { return _columnFa051; } }
        public ColumnInfo ColumnFa052 { get { return _columnFa052; } }
        public ColumnInfo ColumnFa053 { get { return _columnFa053; } }
        public ColumnInfo ColumnFa054 { get { return _columnFa054; } }
        public ColumnInfo ColumnFa055 { get { return _columnFa055; } }
        public ColumnInfo ColumnFa056 { get { return _columnFa056; } }
        public ColumnInfo ColumnFa057 { get { return _columnFa057; } }
        public ColumnInfo ColumnFa058 { get { return _columnFa058; } }
        public ColumnInfo ColumnFa059 { get { return _columnFa059; } }
        public ColumnInfo ColumnFa060 { get { return _columnFa060; } }
        public ColumnInfo ColumnFa061 { get { return _columnFa061; } }
        public ColumnInfo ColumnFa062 { get { return _columnFa062; } }
        public ColumnInfo ColumnFa063 { get { return _columnFa063; } }
        public ColumnInfo ColumnFa064 { get { return _columnFa064; } }
        public ColumnInfo ColumnFa065 { get { return _columnFa065; } }
        public ColumnInfo ColumnFa066 { get { return _columnFa066; } }
        public ColumnInfo ColumnFa067 { get { return _columnFa067; } }
        public ColumnInfo ColumnFa068 { get { return _columnFa068; } }
        public ColumnInfo ColumnFa069 { get { return _columnFa069; } }
        public ColumnInfo ColumnFa070 { get { return _columnFa070; } }
        public ColumnInfo ColumnFa071 { get { return _columnFa071; } }
        public ColumnInfo ColumnFa072 { get { return _columnFa072; } }
        public ColumnInfo ColumnFa073 { get { return _columnFa073; } }
        public ColumnInfo ColumnFa074 { get { return _columnFa074; } }
        public ColumnInfo ColumnFa075 { get { return _columnFa075; } }
        public ColumnInfo ColumnFa076 { get { return _columnFa076; } }
        public ColumnInfo ColumnFa077 { get { return _columnFa077; } }
        public ColumnInfo ColumnFa078 { get { return _columnFa078; } }
        public ColumnInfo ColumnFa079 { get { return _columnFa079; } }
        public ColumnInfo ColumnFa080 { get { return _columnFa080; } }
        public ColumnInfo ColumnFa081 { get { return _columnFa081; } }
        public ColumnInfo ColumnFa082 { get { return _columnFa082; } }
        public ColumnInfo ColumnFa083 { get { return _columnFa083; } }
        public ColumnInfo ColumnFa084 { get { return _columnFa084; } }
        public ColumnInfo ColumnFa085 { get { return _columnFa085; } }
        public ColumnInfo ColumnFa086 { get { return _columnFa086; } }
        public ColumnInfo ColumnFa087 { get { return _columnFa087; } }
        public ColumnInfo ColumnFa088 { get { return _columnFa088; } }
        public ColumnInfo ColumnFa089 { get { return _columnFa089; } }
        public ColumnInfo ColumnFa090 { get { return _columnFa090; } }
        public ColumnInfo ColumnFa091 { get { return _columnFa091; } }
        public ColumnInfo ColumnFa092 { get { return _columnFa092; } }
        public ColumnInfo ColumnFa093 { get { return _columnFa093; } }
        public ColumnInfo ColumnFa094 { get { return _columnFa094; } }
        public ColumnInfo ColumnFa095 { get { return _columnFa095; } }
        public ColumnInfo ColumnFa096 { get { return _columnFa096; } }
        public ColumnInfo ColumnFa097 { get { return _columnFa097; } }
        public ColumnInfo ColumnFa098 { get { return _columnFa098; } }
        public ColumnInfo ColumnFa099 { get { return _columnFa099; } }
        public ColumnInfo ColumnFa100 { get { return _columnFa100; } }
        public ColumnInfo ColumnFa101 { get { return _columnFa101; } }
        public ColumnInfo ColumnFa102 { get { return _columnFa102; } }
        public ColumnInfo ColumnFa103 { get { return _columnFa103; } }
        public ColumnInfo ColumnFa104 { get { return _columnFa104; } }
        public ColumnInfo ColumnFa105 { get { return _columnFa105; } }
        public ColumnInfo ColumnFa106 { get { return _columnFa106; } }
        public ColumnInfo ColumnFa107 { get { return _columnFa107; } }
        public ColumnInfo ColumnFa108 { get { return _columnFa108; } }
        public ColumnInfo ColumnFa109 { get { return _columnFa109; } }
        public ColumnInfo ColumnFa110 { get { return _columnFa110; } }
        public ColumnInfo ColumnFa111 { get { return _columnFa111; } }
        public ColumnInfo ColumnFa112 { get { return _columnFa112; } }
        public ColumnInfo ColumnFa113 { get { return _columnFa113; } }
        public ColumnInfo ColumnFa114 { get { return _columnFa114; } }
        public ColumnInfo ColumnFa115 { get { return _columnFa115; } }
        public ColumnInfo ColumnFa116 { get { return _columnFa116; } }
        public ColumnInfo ColumnFa117 { get { return _columnFa117; } }
        public ColumnInfo ColumnFa118 { get { return _columnFa118; } }
        public ColumnInfo ColumnFa119 { get { return _columnFa119; } }
        public ColumnInfo ColumnFa120 { get { return _columnFa120; } }
        public ColumnInfo ColumnFa121 { get { return _columnFa121; } }
        public ColumnInfo ColumnFa122 { get { return _columnFa122; } }
        public ColumnInfo ColumnFa123 { get { return _columnFa123; } }
        public ColumnInfo ColumnFa124 { get { return _columnFa124; } }
        public ColumnInfo ColumnFa125 { get { return _columnFa125; } }
        public ColumnInfo ColumnFa126 { get { return _columnFa126; } }
        public ColumnInfo ColumnFa127 { get { return _columnFa127; } }
        public ColumnInfo ColumnFa128 { get { return _columnFa128; } }
        public ColumnInfo ColumnFa129 { get { return _columnFa129; } }
        public ColumnInfo ColumnFa130 { get { return _columnFa130; } }
        public ColumnInfo ColumnFa131 { get { return _columnFa131; } }
        public ColumnInfo ColumnFa132 { get { return _columnFa132; } }
        public ColumnInfo ColumnFa133 { get { return _columnFa133; } }
        public ColumnInfo ColumnFa134 { get { return _columnFa134; } }
        public ColumnInfo ColumnFa135 { get { return _columnFa135; } }
        public ColumnInfo ColumnFa136 { get { return _columnFa136; } }
        public ColumnInfo ColumnFa137 { get { return _columnFa137; } }
        public ColumnInfo ColumnFa138 { get { return _columnFa138; } }
        public ColumnInfo ColumnFa139 { get { return _columnFa139; } }
        public ColumnInfo ColumnFa140 { get { return _columnFa140; } }
        public ColumnInfo ColumnFa141 { get { return _columnFa141; } }
        public ColumnInfo ColumnFa142 { get { return _columnFa142; } }
        public ColumnInfo ColumnFa143 { get { return _columnFa143; } }
        public ColumnInfo ColumnFa144 { get { return _columnFa144; } }
        public ColumnInfo ColumnFa145 { get { return _columnFa145; } }
        public ColumnInfo ColumnFa146 { get { return _columnFa146; } }
        public ColumnInfo ColumnFa147 { get { return _columnFa147; } }
        public ColumnInfo ColumnFa148 { get { return _columnFa148; } }
        public ColumnInfo ColumnFa149 { get { return _columnFa149; } }
        public ColumnInfo ColumnFa150 { get { return _columnFa150; } }
        public ColumnInfo ColumnFa151 { get { return _columnFa151; } }
        public ColumnInfo ColumnFa152 { get { return _columnFa152; } }
        public ColumnInfo ColumnFa153 { get { return _columnFa153; } }
        public ColumnInfo ColumnFa154 { get { return _columnFa154; } }
        public ColumnInfo ColumnFa155 { get { return _columnFa155; } }
        public ColumnInfo ColumnFa156 { get { return _columnFa156; } }
        public ColumnInfo ColumnFa157 { get { return _columnFa157; } }
        public ColumnInfo ColumnFa158 { get { return _columnFa158; } }
        public ColumnInfo ColumnFa159 { get { return _columnFa159; } }
        public ColumnInfo ColumnFa160 { get { return _columnFa160; } }
        public ColumnInfo ColumnFa161 { get { return _columnFa161; } }
        public ColumnInfo ColumnFa162 { get { return _columnFa162; } }
        public ColumnInfo ColumnFa163 { get { return _columnFa163; } }
        public ColumnInfo ColumnFa164 { get { return _columnFa164; } }
        public ColumnInfo ColumnFa165 { get { return _columnFa165; } }
        public ColumnInfo ColumnFa166 { get { return _columnFa166; } }
        public ColumnInfo ColumnFa167 { get { return _columnFa167; } }
        public ColumnInfo ColumnFa168 { get { return _columnFa168; } }
        public ColumnInfo ColumnFa169 { get { return _columnFa169; } }
        public ColumnInfo ColumnFa170 { get { return _columnFa170; } }
        public ColumnInfo ColumnFa171 { get { return _columnFa171; } }
        public ColumnInfo ColumnFa172 { get { return _columnFa172; } }
        public ColumnInfo ColumnFa173 { get { return _columnFa173; } }
        public ColumnInfo ColumnFa174 { get { return _columnFa174; } }
        public ColumnInfo ColumnFa175 { get { return _columnFa175; } }
        public ColumnInfo ColumnFa176 { get { return _columnFa176; } }
        public ColumnInfo ColumnFa177 { get { return _columnFa177; } }
        public ColumnInfo ColumnFa178 { get { return _columnFa178; } }
        public ColumnInfo ColumnFa179 { get { return _columnFa179; } }
        public ColumnInfo ColumnFa180 { get { return _columnFa180; } }
        public ColumnInfo ColumnFa181 { get { return _columnFa181; } }
        public ColumnInfo ColumnFa182 { get { return _columnFa182; } }
        public ColumnInfo ColumnFa183 { get { return _columnFa183; } }
        public ColumnInfo ColumnFa184 { get { return _columnFa184; } }
        public ColumnInfo ColumnFa185 { get { return _columnFa185; } }
        public ColumnInfo ColumnFa186 { get { return _columnFa186; } }
        public ColumnInfo ColumnFa187 { get { return _columnFa187; } }
        public ColumnInfo ColumnFa188 { get { return _columnFa188; } }
        public ColumnInfo ColumnFa189 { get { return _columnFa189; } }
        public ColumnInfo ColumnFa190 { get { return _columnFa190; } }
        public ColumnInfo ColumnFa191 { get { return _columnFa191; } }
        public ColumnInfo ColumnFa192 { get { return _columnFa192; } }
        public ColumnInfo ColumnFa193 { get { return _columnFa193; } }
        public ColumnInfo ColumnFa194 { get { return _columnFa194; } }
        public ColumnInfo ColumnFa195 { get { return _columnFa195; } }
        public ColumnInfo ColumnFa196 { get { return _columnFa196; } }
        public ColumnInfo ColumnFa197 { get { return _columnFa197; } }
        public ColumnInfo ColumnFa198 { get { return _columnFa198; } }
        public ColumnInfo ColumnFa199 { get { return _columnFa199; } }
        public ColumnInfo ColumnFa200 { get { return _columnFa200; } }

        protected void InitializeColumnInfo() {
            _columnSampleId = cci("SAMPLE_ID", "SAMPLE_ID", null, null, true, "SampleId", typeof(String), true, "VARCHAR2", 10, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa001 = cci("FA001", "FA001", null, null, false, "Fa001", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa002 = cci("FA002", "FA002", null, null, false, "Fa002", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa003 = cci("FA003", "FA003", null, null, false, "Fa003", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa004 = cci("FA004", "FA004", null, null, false, "Fa004", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa005 = cci("FA005", "FA005", null, null, false, "Fa005", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa006 = cci("FA006", "FA006", null, null, false, "Fa006", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa007 = cci("FA007", "FA007", null, null, false, "Fa007", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa008 = cci("FA008", "FA008", null, null, false, "Fa008", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa009 = cci("FA009", "FA009", null, null, false, "Fa009", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa010 = cci("FA010", "FA010", null, null, false, "Fa010", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa011 = cci("FA011", "FA011", null, null, false, "Fa011", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa012 = cci("FA012", "FA012", null, null, false, "Fa012", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa013 = cci("FA013", "FA013", null, null, false, "Fa013", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa014 = cci("FA014", "FA014", null, null, false, "Fa014", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa015 = cci("FA015", "FA015", null, null, false, "Fa015", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa016 = cci("FA016", "FA016", null, null, false, "Fa016", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa017 = cci("FA017", "FA017", null, null, false, "Fa017", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa018 = cci("FA018", "FA018", null, null, false, "Fa018", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa019 = cci("FA019", "FA019", null, null, false, "Fa019", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa020 = cci("FA020", "FA020", null, null, false, "Fa020", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa021 = cci("FA021", "FA021", null, null, false, "Fa021", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa022 = cci("FA022", "FA022", null, null, false, "Fa022", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa023 = cci("FA023", "FA023", null, null, false, "Fa023", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa024 = cci("FA024", "FA024", null, null, false, "Fa024", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa025 = cci("FA025", "FA025", null, null, false, "Fa025", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa026 = cci("FA026", "FA026", null, null, false, "Fa026", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa027 = cci("FA027", "FA027", null, null, false, "Fa027", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa028 = cci("FA028", "FA028", null, null, false, "Fa028", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa029 = cci("FA029", "FA029", null, null, false, "Fa029", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa030 = cci("FA030", "FA030", null, null, false, "Fa030", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa031 = cci("FA031", "FA031", null, null, false, "Fa031", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa032 = cci("FA032", "FA032", null, null, false, "Fa032", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa033 = cci("FA033", "FA033", null, null, false, "Fa033", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa034 = cci("FA034", "FA034", null, null, false, "Fa034", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa035 = cci("FA035", "FA035", null, null, false, "Fa035", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa036 = cci("FA036", "FA036", null, null, false, "Fa036", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa037 = cci("FA037", "FA037", null, null, false, "Fa037", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa038 = cci("FA038", "FA038", null, null, false, "Fa038", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa039 = cci("FA039", "FA039", null, null, false, "Fa039", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa040 = cci("FA040", "FA040", null, null, false, "Fa040", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa041 = cci("FA041", "FA041", null, null, false, "Fa041", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa042 = cci("FA042", "FA042", null, null, false, "Fa042", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa043 = cci("FA043", "FA043", null, null, false, "Fa043", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa044 = cci("FA044", "FA044", null, null, false, "Fa044", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa045 = cci("FA045", "FA045", null, null, false, "Fa045", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa046 = cci("FA046", "FA046", null, null, false, "Fa046", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa047 = cci("FA047", "FA047", null, null, false, "Fa047", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa048 = cci("FA048", "FA048", null, null, false, "Fa048", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa049 = cci("FA049", "FA049", null, null, false, "Fa049", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa050 = cci("FA050", "FA050", null, null, false, "Fa050", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa051 = cci("FA051", "FA051", null, null, false, "Fa051", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa052 = cci("FA052", "FA052", null, null, false, "Fa052", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa053 = cci("FA053", "FA053", null, null, false, "Fa053", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa054 = cci("FA054", "FA054", null, null, false, "Fa054", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa055 = cci("FA055", "FA055", null, null, false, "Fa055", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa056 = cci("FA056", "FA056", null, null, false, "Fa056", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa057 = cci("FA057", "FA057", null, null, false, "Fa057", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa058 = cci("FA058", "FA058", null, null, false, "Fa058", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa059 = cci("FA059", "FA059", null, null, false, "Fa059", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa060 = cci("FA060", "FA060", null, null, false, "Fa060", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa061 = cci("FA061", "FA061", null, null, false, "Fa061", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa062 = cci("FA062", "FA062", null, null, false, "Fa062", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa063 = cci("FA063", "FA063", null, null, false, "Fa063", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa064 = cci("FA064", "FA064", null, null, false, "Fa064", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa065 = cci("FA065", "FA065", null, null, false, "Fa065", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa066 = cci("FA066", "FA066", null, null, false, "Fa066", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa067 = cci("FA067", "FA067", null, null, false, "Fa067", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa068 = cci("FA068", "FA068", null, null, false, "Fa068", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa069 = cci("FA069", "FA069", null, null, false, "Fa069", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa070 = cci("FA070", "FA070", null, null, false, "Fa070", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa071 = cci("FA071", "FA071", null, null, false, "Fa071", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa072 = cci("FA072", "FA072", null, null, false, "Fa072", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa073 = cci("FA073", "FA073", null, null, false, "Fa073", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa074 = cci("FA074", "FA074", null, null, false, "Fa074", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa075 = cci("FA075", "FA075", null, null, false, "Fa075", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa076 = cci("FA076", "FA076", null, null, false, "Fa076", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa077 = cci("FA077", "FA077", null, null, false, "Fa077", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa078 = cci("FA078", "FA078", null, null, false, "Fa078", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa079 = cci("FA079", "FA079", null, null, false, "Fa079", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa080 = cci("FA080", "FA080", null, null, false, "Fa080", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa081 = cci("FA081", "FA081", null, null, false, "Fa081", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa082 = cci("FA082", "FA082", null, null, false, "Fa082", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa083 = cci("FA083", "FA083", null, null, false, "Fa083", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa084 = cci("FA084", "FA084", null, null, false, "Fa084", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa085 = cci("FA085", "FA085", null, null, false, "Fa085", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa086 = cci("FA086", "FA086", null, null, false, "Fa086", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa087 = cci("FA087", "FA087", null, null, false, "Fa087", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa088 = cci("FA088", "FA088", null, null, false, "Fa088", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa089 = cci("FA089", "FA089", null, null, false, "Fa089", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa090 = cci("FA090", "FA090", null, null, false, "Fa090", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa091 = cci("FA091", "FA091", null, null, false, "Fa091", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa092 = cci("FA092", "FA092", null, null, false, "Fa092", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa093 = cci("FA093", "FA093", null, null, false, "Fa093", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa094 = cci("FA094", "FA094", null, null, false, "Fa094", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa095 = cci("FA095", "FA095", null, null, false, "Fa095", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa096 = cci("FA096", "FA096", null, null, false, "Fa096", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa097 = cci("FA097", "FA097", null, null, false, "Fa097", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa098 = cci("FA098", "FA098", null, null, false, "Fa098", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa099 = cci("FA099", "FA099", null, null, false, "Fa099", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa100 = cci("FA100", "FA100", null, null, false, "Fa100", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa101 = cci("FA101", "FA101", null, null, false, "Fa101", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa102 = cci("FA102", "FA102", null, null, false, "Fa102", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa103 = cci("FA103", "FA103", null, null, false, "Fa103", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa104 = cci("FA104", "FA104", null, null, false, "Fa104", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa105 = cci("FA105", "FA105", null, null, false, "Fa105", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa106 = cci("FA106", "FA106", null, null, false, "Fa106", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa107 = cci("FA107", "FA107", null, null, false, "Fa107", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa108 = cci("FA108", "FA108", null, null, false, "Fa108", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa109 = cci("FA109", "FA109", null, null, false, "Fa109", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa110 = cci("FA110", "FA110", null, null, false, "Fa110", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa111 = cci("FA111", "FA111", null, null, false, "Fa111", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa112 = cci("FA112", "FA112", null, null, false, "Fa112", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa113 = cci("FA113", "FA113", null, null, false, "Fa113", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa114 = cci("FA114", "FA114", null, null, false, "Fa114", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa115 = cci("FA115", "FA115", null, null, false, "Fa115", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa116 = cci("FA116", "FA116", null, null, false, "Fa116", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa117 = cci("FA117", "FA117", null, null, false, "Fa117", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa118 = cci("FA118", "FA118", null, null, false, "Fa118", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa119 = cci("FA119", "FA119", null, null, false, "Fa119", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa120 = cci("FA120", "FA120", null, null, false, "Fa120", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa121 = cci("FA121", "FA121", null, null, false, "Fa121", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa122 = cci("FA122", "FA122", null, null, false, "Fa122", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa123 = cci("FA123", "FA123", null, null, false, "Fa123", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa124 = cci("FA124", "FA124", null, null, false, "Fa124", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa125 = cci("FA125", "FA125", null, null, false, "Fa125", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa126 = cci("FA126", "FA126", null, null, false, "Fa126", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa127 = cci("FA127", "FA127", null, null, false, "Fa127", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa128 = cci("FA128", "FA128", null, null, false, "Fa128", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa129 = cci("FA129", "FA129", null, null, false, "Fa129", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa130 = cci("FA130", "FA130", null, null, false, "Fa130", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa131 = cci("FA131", "FA131", null, null, false, "Fa131", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa132 = cci("FA132", "FA132", null, null, false, "Fa132", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa133 = cci("FA133", "FA133", null, null, false, "Fa133", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa134 = cci("FA134", "FA134", null, null, false, "Fa134", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa135 = cci("FA135", "FA135", null, null, false, "Fa135", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa136 = cci("FA136", "FA136", null, null, false, "Fa136", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa137 = cci("FA137", "FA137", null, null, false, "Fa137", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa138 = cci("FA138", "FA138", null, null, false, "Fa138", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa139 = cci("FA139", "FA139", null, null, false, "Fa139", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa140 = cci("FA140", "FA140", null, null, false, "Fa140", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa141 = cci("FA141", "FA141", null, null, false, "Fa141", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa142 = cci("FA142", "FA142", null, null, false, "Fa142", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa143 = cci("FA143", "FA143", null, null, false, "Fa143", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa144 = cci("FA144", "FA144", null, null, false, "Fa144", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa145 = cci("FA145", "FA145", null, null, false, "Fa145", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa146 = cci("FA146", "FA146", null, null, false, "Fa146", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa147 = cci("FA147", "FA147", null, null, false, "Fa147", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa148 = cci("FA148", "FA148", null, null, false, "Fa148", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa149 = cci("FA149", "FA149", null, null, false, "Fa149", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa150 = cci("FA150", "FA150", null, null, false, "Fa150", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa151 = cci("FA151", "FA151", null, null, false, "Fa151", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa152 = cci("FA152", "FA152", null, null, false, "Fa152", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa153 = cci("FA153", "FA153", null, null, false, "Fa153", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa154 = cci("FA154", "FA154", null, null, false, "Fa154", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa155 = cci("FA155", "FA155", null, null, false, "Fa155", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa156 = cci("FA156", "FA156", null, null, false, "Fa156", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa157 = cci("FA157", "FA157", null, null, false, "Fa157", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa158 = cci("FA158", "FA158", null, null, false, "Fa158", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa159 = cci("FA159", "FA159", null, null, false, "Fa159", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa160 = cci("FA160", "FA160", null, null, false, "Fa160", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa161 = cci("FA161", "FA161", null, null, false, "Fa161", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa162 = cci("FA162", "FA162", null, null, false, "Fa162", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa163 = cci("FA163", "FA163", null, null, false, "Fa163", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa164 = cci("FA164", "FA164", null, null, false, "Fa164", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa165 = cci("FA165", "FA165", null, null, false, "Fa165", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa166 = cci("FA166", "FA166", null, null, false, "Fa166", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa167 = cci("FA167", "FA167", null, null, false, "Fa167", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa168 = cci("FA168", "FA168", null, null, false, "Fa168", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa169 = cci("FA169", "FA169", null, null, false, "Fa169", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa170 = cci("FA170", "FA170", null, null, false, "Fa170", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa171 = cci("FA171", "FA171", null, null, false, "Fa171", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa172 = cci("FA172", "FA172", null, null, false, "Fa172", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa173 = cci("FA173", "FA173", null, null, false, "Fa173", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa174 = cci("FA174", "FA174", null, null, false, "Fa174", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa175 = cci("FA175", "FA175", null, null, false, "Fa175", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa176 = cci("FA176", "FA176", null, null, false, "Fa176", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa177 = cci("FA177", "FA177", null, null, false, "Fa177", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa178 = cci("FA178", "FA178", null, null, false, "Fa178", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa179 = cci("FA179", "FA179", null, null, false, "Fa179", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa180 = cci("FA180", "FA180", null, null, false, "Fa180", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa181 = cci("FA181", "FA181", null, null, false, "Fa181", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa182 = cci("FA182", "FA182", null, null, false, "Fa182", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa183 = cci("FA183", "FA183", null, null, false, "Fa183", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa184 = cci("FA184", "FA184", null, null, false, "Fa184", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa185 = cci("FA185", "FA185", null, null, false, "Fa185", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa186 = cci("FA186", "FA186", null, null, false, "Fa186", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa187 = cci("FA187", "FA187", null, null, false, "Fa187", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa188 = cci("FA188", "FA188", null, null, false, "Fa188", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa189 = cci("FA189", "FA189", null, null, false, "Fa189", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa190 = cci("FA190", "FA190", null, null, false, "Fa190", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa191 = cci("FA191", "FA191", null, null, false, "Fa191", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa192 = cci("FA192", "FA192", null, null, false, "Fa192", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa193 = cci("FA193", "FA193", null, null, false, "Fa193", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa194 = cci("FA194", "FA194", null, null, false, "Fa194", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa195 = cci("FA195", "FA195", null, null, false, "Fa195", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa196 = cci("FA196", "FA196", null, null, false, "Fa196", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa197 = cci("FA197", "FA197", null, null, false, "Fa197", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa198 = cci("FA198", "FA198", null, null, false, "Fa198", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa199 = cci("FA199", "FA199", null, null, false, "Fa199", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
            _columnFa200 = cci("FA200", "FA200", null, null, false, "Fa200", typeof(String), false, "NVARCHAR2", 2000, 0, false, OptimisticLockType.NONE, null, null, null);
        }

        protected void InitializeColumnInfoList() {
            _columnInfoList = new ArrayList<ColumnInfo>();
            _columnInfoList.add(ColumnSampleId);
            _columnInfoList.add(ColumnFa001);
            _columnInfoList.add(ColumnFa002);
            _columnInfoList.add(ColumnFa003);
            _columnInfoList.add(ColumnFa004);
            _columnInfoList.add(ColumnFa005);
            _columnInfoList.add(ColumnFa006);
            _columnInfoList.add(ColumnFa007);
            _columnInfoList.add(ColumnFa008);
            _columnInfoList.add(ColumnFa009);
            _columnInfoList.add(ColumnFa010);
            _columnInfoList.add(ColumnFa011);
            _columnInfoList.add(ColumnFa012);
            _columnInfoList.add(ColumnFa013);
            _columnInfoList.add(ColumnFa014);
            _columnInfoList.add(ColumnFa015);
            _columnInfoList.add(ColumnFa016);
            _columnInfoList.add(ColumnFa017);
            _columnInfoList.add(ColumnFa018);
            _columnInfoList.add(ColumnFa019);
            _columnInfoList.add(ColumnFa020);
            _columnInfoList.add(ColumnFa021);
            _columnInfoList.add(ColumnFa022);
            _columnInfoList.add(ColumnFa023);
            _columnInfoList.add(ColumnFa024);
            _columnInfoList.add(ColumnFa025);
            _columnInfoList.add(ColumnFa026);
            _columnInfoList.add(ColumnFa027);
            _columnInfoList.add(ColumnFa028);
            _columnInfoList.add(ColumnFa029);
            _columnInfoList.add(ColumnFa030);
            _columnInfoList.add(ColumnFa031);
            _columnInfoList.add(ColumnFa032);
            _columnInfoList.add(ColumnFa033);
            _columnInfoList.add(ColumnFa034);
            _columnInfoList.add(ColumnFa035);
            _columnInfoList.add(ColumnFa036);
            _columnInfoList.add(ColumnFa037);
            _columnInfoList.add(ColumnFa038);
            _columnInfoList.add(ColumnFa039);
            _columnInfoList.add(ColumnFa040);
            _columnInfoList.add(ColumnFa041);
            _columnInfoList.add(ColumnFa042);
            _columnInfoList.add(ColumnFa043);
            _columnInfoList.add(ColumnFa044);
            _columnInfoList.add(ColumnFa045);
            _columnInfoList.add(ColumnFa046);
            _columnInfoList.add(ColumnFa047);
            _columnInfoList.add(ColumnFa048);
            _columnInfoList.add(ColumnFa049);
            _columnInfoList.add(ColumnFa050);
            _columnInfoList.add(ColumnFa051);
            _columnInfoList.add(ColumnFa052);
            _columnInfoList.add(ColumnFa053);
            _columnInfoList.add(ColumnFa054);
            _columnInfoList.add(ColumnFa055);
            _columnInfoList.add(ColumnFa056);
            _columnInfoList.add(ColumnFa057);
            _columnInfoList.add(ColumnFa058);
            _columnInfoList.add(ColumnFa059);
            _columnInfoList.add(ColumnFa060);
            _columnInfoList.add(ColumnFa061);
            _columnInfoList.add(ColumnFa062);
            _columnInfoList.add(ColumnFa063);
            _columnInfoList.add(ColumnFa064);
            _columnInfoList.add(ColumnFa065);
            _columnInfoList.add(ColumnFa066);
            _columnInfoList.add(ColumnFa067);
            _columnInfoList.add(ColumnFa068);
            _columnInfoList.add(ColumnFa069);
            _columnInfoList.add(ColumnFa070);
            _columnInfoList.add(ColumnFa071);
            _columnInfoList.add(ColumnFa072);
            _columnInfoList.add(ColumnFa073);
            _columnInfoList.add(ColumnFa074);
            _columnInfoList.add(ColumnFa075);
            _columnInfoList.add(ColumnFa076);
            _columnInfoList.add(ColumnFa077);
            _columnInfoList.add(ColumnFa078);
            _columnInfoList.add(ColumnFa079);
            _columnInfoList.add(ColumnFa080);
            _columnInfoList.add(ColumnFa081);
            _columnInfoList.add(ColumnFa082);
            _columnInfoList.add(ColumnFa083);
            _columnInfoList.add(ColumnFa084);
            _columnInfoList.add(ColumnFa085);
            _columnInfoList.add(ColumnFa086);
            _columnInfoList.add(ColumnFa087);
            _columnInfoList.add(ColumnFa088);
            _columnInfoList.add(ColumnFa089);
            _columnInfoList.add(ColumnFa090);
            _columnInfoList.add(ColumnFa091);
            _columnInfoList.add(ColumnFa092);
            _columnInfoList.add(ColumnFa093);
            _columnInfoList.add(ColumnFa094);
            _columnInfoList.add(ColumnFa095);
            _columnInfoList.add(ColumnFa096);
            _columnInfoList.add(ColumnFa097);
            _columnInfoList.add(ColumnFa098);
            _columnInfoList.add(ColumnFa099);
            _columnInfoList.add(ColumnFa100);
            _columnInfoList.add(ColumnFa101);
            _columnInfoList.add(ColumnFa102);
            _columnInfoList.add(ColumnFa103);
            _columnInfoList.add(ColumnFa104);
            _columnInfoList.add(ColumnFa105);
            _columnInfoList.add(ColumnFa106);
            _columnInfoList.add(ColumnFa107);
            _columnInfoList.add(ColumnFa108);
            _columnInfoList.add(ColumnFa109);
            _columnInfoList.add(ColumnFa110);
            _columnInfoList.add(ColumnFa111);
            _columnInfoList.add(ColumnFa112);
            _columnInfoList.add(ColumnFa113);
            _columnInfoList.add(ColumnFa114);
            _columnInfoList.add(ColumnFa115);
            _columnInfoList.add(ColumnFa116);
            _columnInfoList.add(ColumnFa117);
            _columnInfoList.add(ColumnFa118);
            _columnInfoList.add(ColumnFa119);
            _columnInfoList.add(ColumnFa120);
            _columnInfoList.add(ColumnFa121);
            _columnInfoList.add(ColumnFa122);
            _columnInfoList.add(ColumnFa123);
            _columnInfoList.add(ColumnFa124);
            _columnInfoList.add(ColumnFa125);
            _columnInfoList.add(ColumnFa126);
            _columnInfoList.add(ColumnFa127);
            _columnInfoList.add(ColumnFa128);
            _columnInfoList.add(ColumnFa129);
            _columnInfoList.add(ColumnFa130);
            _columnInfoList.add(ColumnFa131);
            _columnInfoList.add(ColumnFa132);
            _columnInfoList.add(ColumnFa133);
            _columnInfoList.add(ColumnFa134);
            _columnInfoList.add(ColumnFa135);
            _columnInfoList.add(ColumnFa136);
            _columnInfoList.add(ColumnFa137);
            _columnInfoList.add(ColumnFa138);
            _columnInfoList.add(ColumnFa139);
            _columnInfoList.add(ColumnFa140);
            _columnInfoList.add(ColumnFa141);
            _columnInfoList.add(ColumnFa142);
            _columnInfoList.add(ColumnFa143);
            _columnInfoList.add(ColumnFa144);
            _columnInfoList.add(ColumnFa145);
            _columnInfoList.add(ColumnFa146);
            _columnInfoList.add(ColumnFa147);
            _columnInfoList.add(ColumnFa148);
            _columnInfoList.add(ColumnFa149);
            _columnInfoList.add(ColumnFa150);
            _columnInfoList.add(ColumnFa151);
            _columnInfoList.add(ColumnFa152);
            _columnInfoList.add(ColumnFa153);
            _columnInfoList.add(ColumnFa154);
            _columnInfoList.add(ColumnFa155);
            _columnInfoList.add(ColumnFa156);
            _columnInfoList.add(ColumnFa157);
            _columnInfoList.add(ColumnFa158);
            _columnInfoList.add(ColumnFa159);
            _columnInfoList.add(ColumnFa160);
            _columnInfoList.add(ColumnFa161);
            _columnInfoList.add(ColumnFa162);
            _columnInfoList.add(ColumnFa163);
            _columnInfoList.add(ColumnFa164);
            _columnInfoList.add(ColumnFa165);
            _columnInfoList.add(ColumnFa166);
            _columnInfoList.add(ColumnFa167);
            _columnInfoList.add(ColumnFa168);
            _columnInfoList.add(ColumnFa169);
            _columnInfoList.add(ColumnFa170);
            _columnInfoList.add(ColumnFa171);
            _columnInfoList.add(ColumnFa172);
            _columnInfoList.add(ColumnFa173);
            _columnInfoList.add(ColumnFa174);
            _columnInfoList.add(ColumnFa175);
            _columnInfoList.add(ColumnFa176);
            _columnInfoList.add(ColumnFa177);
            _columnInfoList.add(ColumnFa178);
            _columnInfoList.add(ColumnFa179);
            _columnInfoList.add(ColumnFa180);
            _columnInfoList.add(ColumnFa181);
            _columnInfoList.add(ColumnFa182);
            _columnInfoList.add(ColumnFa183);
            _columnInfoList.add(ColumnFa184);
            _columnInfoList.add(ColumnFa185);
            _columnInfoList.add(ColumnFa186);
            _columnInfoList.add(ColumnFa187);
            _columnInfoList.add(ColumnFa188);
            _columnInfoList.add(ColumnFa189);
            _columnInfoList.add(ColumnFa190);
            _columnInfoList.add(ColumnFa191);
            _columnInfoList.add(ColumnFa192);
            _columnInfoList.add(ColumnFa193);
            _columnInfoList.add(ColumnFa194);
            _columnInfoList.add(ColumnFa195);
            _columnInfoList.add(ColumnFa196);
            _columnInfoList.add(ColumnFa197);
            _columnInfoList.add(ColumnFa198);
            _columnInfoList.add(ColumnFa199);
            _columnInfoList.add(ColumnFa200);
        }

        // ===============================================================================
        //                                                                     Unique Info
        //                                                                     ===========
        public override UniqueInfo PrimaryUniqueInfo { get {
            return cpui(ColumnSampleId);
        }}

        // -------------------------------------------------
        //                                   Primary Element
        //                                   ---------------
        public override bool HasPrimaryKey { get { return true; } }
        public override bool HasCompoundPrimaryKey { get { return false; } }

        // ===============================================================================
        //                                                                   Relation Info
        //                                                                   =============
        // -------------------------------------------------
        //                                   Foreign Element
        //                                   ---------------


        // -------------------------------------------------
        //                                  Referrer Element
        //                                  ----------------

        // ===============================================================================
        //                                                                    Various Info
        //                                                                    ============
        public override bool HasCommonColumn { get { return false; } }

        // ===============================================================================
        //                                                                 Name Definition
        //                                                                 ===============
        #region Name

        // -------------------------------------------------
        //                                             Table
        //                                             -----
        public static readonly String TABLE_DB_NAME = "T_FA_DATA";
        public static readonly String TABLE_PROPERTY_NAME = "TFaData";

        // -------------------------------------------------
        //                                    Column DB-Name
        //                                    --------------
        public static readonly String DB_NAME_SAMPLE_ID = "SAMPLE_ID";
        public static readonly String DB_NAME_FA001 = "FA001";
        public static readonly String DB_NAME_FA002 = "FA002";
        public static readonly String DB_NAME_FA003 = "FA003";
        public static readonly String DB_NAME_FA004 = "FA004";
        public static readonly String DB_NAME_FA005 = "FA005";
        public static readonly String DB_NAME_FA006 = "FA006";
        public static readonly String DB_NAME_FA007 = "FA007";
        public static readonly String DB_NAME_FA008 = "FA008";
        public static readonly String DB_NAME_FA009 = "FA009";
        public static readonly String DB_NAME_FA010 = "FA010";
        public static readonly String DB_NAME_FA011 = "FA011";
        public static readonly String DB_NAME_FA012 = "FA012";
        public static readonly String DB_NAME_FA013 = "FA013";
        public static readonly String DB_NAME_FA014 = "FA014";
        public static readonly String DB_NAME_FA015 = "FA015";
        public static readonly String DB_NAME_FA016 = "FA016";
        public static readonly String DB_NAME_FA017 = "FA017";
        public static readonly String DB_NAME_FA018 = "FA018";
        public static readonly String DB_NAME_FA019 = "FA019";
        public static readonly String DB_NAME_FA020 = "FA020";
        public static readonly String DB_NAME_FA021 = "FA021";
        public static readonly String DB_NAME_FA022 = "FA022";
        public static readonly String DB_NAME_FA023 = "FA023";
        public static readonly String DB_NAME_FA024 = "FA024";
        public static readonly String DB_NAME_FA025 = "FA025";
        public static readonly String DB_NAME_FA026 = "FA026";
        public static readonly String DB_NAME_FA027 = "FA027";
        public static readonly String DB_NAME_FA028 = "FA028";
        public static readonly String DB_NAME_FA029 = "FA029";
        public static readonly String DB_NAME_FA030 = "FA030";
        public static readonly String DB_NAME_FA031 = "FA031";
        public static readonly String DB_NAME_FA032 = "FA032";
        public static readonly String DB_NAME_FA033 = "FA033";
        public static readonly String DB_NAME_FA034 = "FA034";
        public static readonly String DB_NAME_FA035 = "FA035";
        public static readonly String DB_NAME_FA036 = "FA036";
        public static readonly String DB_NAME_FA037 = "FA037";
        public static readonly String DB_NAME_FA038 = "FA038";
        public static readonly String DB_NAME_FA039 = "FA039";
        public static readonly String DB_NAME_FA040 = "FA040";
        public static readonly String DB_NAME_FA041 = "FA041";
        public static readonly String DB_NAME_FA042 = "FA042";
        public static readonly String DB_NAME_FA043 = "FA043";
        public static readonly String DB_NAME_FA044 = "FA044";
        public static readonly String DB_NAME_FA045 = "FA045";
        public static readonly String DB_NAME_FA046 = "FA046";
        public static readonly String DB_NAME_FA047 = "FA047";
        public static readonly String DB_NAME_FA048 = "FA048";
        public static readonly String DB_NAME_FA049 = "FA049";
        public static readonly String DB_NAME_FA050 = "FA050";
        public static readonly String DB_NAME_FA051 = "FA051";
        public static readonly String DB_NAME_FA052 = "FA052";
        public static readonly String DB_NAME_FA053 = "FA053";
        public static readonly String DB_NAME_FA054 = "FA054";
        public static readonly String DB_NAME_FA055 = "FA055";
        public static readonly String DB_NAME_FA056 = "FA056";
        public static readonly String DB_NAME_FA057 = "FA057";
        public static readonly String DB_NAME_FA058 = "FA058";
        public static readonly String DB_NAME_FA059 = "FA059";
        public static readonly String DB_NAME_FA060 = "FA060";
        public static readonly String DB_NAME_FA061 = "FA061";
        public static readonly String DB_NAME_FA062 = "FA062";
        public static readonly String DB_NAME_FA063 = "FA063";
        public static readonly String DB_NAME_FA064 = "FA064";
        public static readonly String DB_NAME_FA065 = "FA065";
        public static readonly String DB_NAME_FA066 = "FA066";
        public static readonly String DB_NAME_FA067 = "FA067";
        public static readonly String DB_NAME_FA068 = "FA068";
        public static readonly String DB_NAME_FA069 = "FA069";
        public static readonly String DB_NAME_FA070 = "FA070";
        public static readonly String DB_NAME_FA071 = "FA071";
        public static readonly String DB_NAME_FA072 = "FA072";
        public static readonly String DB_NAME_FA073 = "FA073";
        public static readonly String DB_NAME_FA074 = "FA074";
        public static readonly String DB_NAME_FA075 = "FA075";
        public static readonly String DB_NAME_FA076 = "FA076";
        public static readonly String DB_NAME_FA077 = "FA077";
        public static readonly String DB_NAME_FA078 = "FA078";
        public static readonly String DB_NAME_FA079 = "FA079";
        public static readonly String DB_NAME_FA080 = "FA080";
        public static readonly String DB_NAME_FA081 = "FA081";
        public static readonly String DB_NAME_FA082 = "FA082";
        public static readonly String DB_NAME_FA083 = "FA083";
        public static readonly String DB_NAME_FA084 = "FA084";
        public static readonly String DB_NAME_FA085 = "FA085";
        public static readonly String DB_NAME_FA086 = "FA086";
        public static readonly String DB_NAME_FA087 = "FA087";
        public static readonly String DB_NAME_FA088 = "FA088";
        public static readonly String DB_NAME_FA089 = "FA089";
        public static readonly String DB_NAME_FA090 = "FA090";
        public static readonly String DB_NAME_FA091 = "FA091";
        public static readonly String DB_NAME_FA092 = "FA092";
        public static readonly String DB_NAME_FA093 = "FA093";
        public static readonly String DB_NAME_FA094 = "FA094";
        public static readonly String DB_NAME_FA095 = "FA095";
        public static readonly String DB_NAME_FA096 = "FA096";
        public static readonly String DB_NAME_FA097 = "FA097";
        public static readonly String DB_NAME_FA098 = "FA098";
        public static readonly String DB_NAME_FA099 = "FA099";
        public static readonly String DB_NAME_FA100 = "FA100";
        public static readonly String DB_NAME_FA101 = "FA101";
        public static readonly String DB_NAME_FA102 = "FA102";
        public static readonly String DB_NAME_FA103 = "FA103";
        public static readonly String DB_NAME_FA104 = "FA104";
        public static readonly String DB_NAME_FA105 = "FA105";
        public static readonly String DB_NAME_FA106 = "FA106";
        public static readonly String DB_NAME_FA107 = "FA107";
        public static readonly String DB_NAME_FA108 = "FA108";
        public static readonly String DB_NAME_FA109 = "FA109";
        public static readonly String DB_NAME_FA110 = "FA110";
        public static readonly String DB_NAME_FA111 = "FA111";
        public static readonly String DB_NAME_FA112 = "FA112";
        public static readonly String DB_NAME_FA113 = "FA113";
        public static readonly String DB_NAME_FA114 = "FA114";
        public static readonly String DB_NAME_FA115 = "FA115";
        public static readonly String DB_NAME_FA116 = "FA116";
        public static readonly String DB_NAME_FA117 = "FA117";
        public static readonly String DB_NAME_FA118 = "FA118";
        public static readonly String DB_NAME_FA119 = "FA119";
        public static readonly String DB_NAME_FA120 = "FA120";
        public static readonly String DB_NAME_FA121 = "FA121";
        public static readonly String DB_NAME_FA122 = "FA122";
        public static readonly String DB_NAME_FA123 = "FA123";
        public static readonly String DB_NAME_FA124 = "FA124";
        public static readonly String DB_NAME_FA125 = "FA125";
        public static readonly String DB_NAME_FA126 = "FA126";
        public static readonly String DB_NAME_FA127 = "FA127";
        public static readonly String DB_NAME_FA128 = "FA128";
        public static readonly String DB_NAME_FA129 = "FA129";
        public static readonly String DB_NAME_FA130 = "FA130";
        public static readonly String DB_NAME_FA131 = "FA131";
        public static readonly String DB_NAME_FA132 = "FA132";
        public static readonly String DB_NAME_FA133 = "FA133";
        public static readonly String DB_NAME_FA134 = "FA134";
        public static readonly String DB_NAME_FA135 = "FA135";
        public static readonly String DB_NAME_FA136 = "FA136";
        public static readonly String DB_NAME_FA137 = "FA137";
        public static readonly String DB_NAME_FA138 = "FA138";
        public static readonly String DB_NAME_FA139 = "FA139";
        public static readonly String DB_NAME_FA140 = "FA140";
        public static readonly String DB_NAME_FA141 = "FA141";
        public static readonly String DB_NAME_FA142 = "FA142";
        public static readonly String DB_NAME_FA143 = "FA143";
        public static readonly String DB_NAME_FA144 = "FA144";
        public static readonly String DB_NAME_FA145 = "FA145";
        public static readonly String DB_NAME_FA146 = "FA146";
        public static readonly String DB_NAME_FA147 = "FA147";
        public static readonly String DB_NAME_FA148 = "FA148";
        public static readonly String DB_NAME_FA149 = "FA149";
        public static readonly String DB_NAME_FA150 = "FA150";
        public static readonly String DB_NAME_FA151 = "FA151";
        public static readonly String DB_NAME_FA152 = "FA152";
        public static readonly String DB_NAME_FA153 = "FA153";
        public static readonly String DB_NAME_FA154 = "FA154";
        public static readonly String DB_NAME_FA155 = "FA155";
        public static readonly String DB_NAME_FA156 = "FA156";
        public static readonly String DB_NAME_FA157 = "FA157";
        public static readonly String DB_NAME_FA158 = "FA158";
        public static readonly String DB_NAME_FA159 = "FA159";
        public static readonly String DB_NAME_FA160 = "FA160";
        public static readonly String DB_NAME_FA161 = "FA161";
        public static readonly String DB_NAME_FA162 = "FA162";
        public static readonly String DB_NAME_FA163 = "FA163";
        public static readonly String DB_NAME_FA164 = "FA164";
        public static readonly String DB_NAME_FA165 = "FA165";
        public static readonly String DB_NAME_FA166 = "FA166";
        public static readonly String DB_NAME_FA167 = "FA167";
        public static readonly String DB_NAME_FA168 = "FA168";
        public static readonly String DB_NAME_FA169 = "FA169";
        public static readonly String DB_NAME_FA170 = "FA170";
        public static readonly String DB_NAME_FA171 = "FA171";
        public static readonly String DB_NAME_FA172 = "FA172";
        public static readonly String DB_NAME_FA173 = "FA173";
        public static readonly String DB_NAME_FA174 = "FA174";
        public static readonly String DB_NAME_FA175 = "FA175";
        public static readonly String DB_NAME_FA176 = "FA176";
        public static readonly String DB_NAME_FA177 = "FA177";
        public static readonly String DB_NAME_FA178 = "FA178";
        public static readonly String DB_NAME_FA179 = "FA179";
        public static readonly String DB_NAME_FA180 = "FA180";
        public static readonly String DB_NAME_FA181 = "FA181";
        public static readonly String DB_NAME_FA182 = "FA182";
        public static readonly String DB_NAME_FA183 = "FA183";
        public static readonly String DB_NAME_FA184 = "FA184";
        public static readonly String DB_NAME_FA185 = "FA185";
        public static readonly String DB_NAME_FA186 = "FA186";
        public static readonly String DB_NAME_FA187 = "FA187";
        public static readonly String DB_NAME_FA188 = "FA188";
        public static readonly String DB_NAME_FA189 = "FA189";
        public static readonly String DB_NAME_FA190 = "FA190";
        public static readonly String DB_NAME_FA191 = "FA191";
        public static readonly String DB_NAME_FA192 = "FA192";
        public static readonly String DB_NAME_FA193 = "FA193";
        public static readonly String DB_NAME_FA194 = "FA194";
        public static readonly String DB_NAME_FA195 = "FA195";
        public static readonly String DB_NAME_FA196 = "FA196";
        public static readonly String DB_NAME_FA197 = "FA197";
        public static readonly String DB_NAME_FA198 = "FA198";
        public static readonly String DB_NAME_FA199 = "FA199";
        public static readonly String DB_NAME_FA200 = "FA200";

        // -------------------------------------------------
        //                              Column Property-Name
        //                              --------------------
        public static readonly String PROPERTY_NAME_SAMPLE_ID = "SampleId";
        public static readonly String PROPERTY_NAME_FA001 = "Fa001";
        public static readonly String PROPERTY_NAME_FA002 = "Fa002";
        public static readonly String PROPERTY_NAME_FA003 = "Fa003";
        public static readonly String PROPERTY_NAME_FA004 = "Fa004";
        public static readonly String PROPERTY_NAME_FA005 = "Fa005";
        public static readonly String PROPERTY_NAME_FA006 = "Fa006";
        public static readonly String PROPERTY_NAME_FA007 = "Fa007";
        public static readonly String PROPERTY_NAME_FA008 = "Fa008";
        public static readonly String PROPERTY_NAME_FA009 = "Fa009";
        public static readonly String PROPERTY_NAME_FA010 = "Fa010";
        public static readonly String PROPERTY_NAME_FA011 = "Fa011";
        public static readonly String PROPERTY_NAME_FA012 = "Fa012";
        public static readonly String PROPERTY_NAME_FA013 = "Fa013";
        public static readonly String PROPERTY_NAME_FA014 = "Fa014";
        public static readonly String PROPERTY_NAME_FA015 = "Fa015";
        public static readonly String PROPERTY_NAME_FA016 = "Fa016";
        public static readonly String PROPERTY_NAME_FA017 = "Fa017";
        public static readonly String PROPERTY_NAME_FA018 = "Fa018";
        public static readonly String PROPERTY_NAME_FA019 = "Fa019";
        public static readonly String PROPERTY_NAME_FA020 = "Fa020";
        public static readonly String PROPERTY_NAME_FA021 = "Fa021";
        public static readonly String PROPERTY_NAME_FA022 = "Fa022";
        public static readonly String PROPERTY_NAME_FA023 = "Fa023";
        public static readonly String PROPERTY_NAME_FA024 = "Fa024";
        public static readonly String PROPERTY_NAME_FA025 = "Fa025";
        public static readonly String PROPERTY_NAME_FA026 = "Fa026";
        public static readonly String PROPERTY_NAME_FA027 = "Fa027";
        public static readonly String PROPERTY_NAME_FA028 = "Fa028";
        public static readonly String PROPERTY_NAME_FA029 = "Fa029";
        public static readonly String PROPERTY_NAME_FA030 = "Fa030";
        public static readonly String PROPERTY_NAME_FA031 = "Fa031";
        public static readonly String PROPERTY_NAME_FA032 = "Fa032";
        public static readonly String PROPERTY_NAME_FA033 = "Fa033";
        public static readonly String PROPERTY_NAME_FA034 = "Fa034";
        public static readonly String PROPERTY_NAME_FA035 = "Fa035";
        public static readonly String PROPERTY_NAME_FA036 = "Fa036";
        public static readonly String PROPERTY_NAME_FA037 = "Fa037";
        public static readonly String PROPERTY_NAME_FA038 = "Fa038";
        public static readonly String PROPERTY_NAME_FA039 = "Fa039";
        public static readonly String PROPERTY_NAME_FA040 = "Fa040";
        public static readonly String PROPERTY_NAME_FA041 = "Fa041";
        public static readonly String PROPERTY_NAME_FA042 = "Fa042";
        public static readonly String PROPERTY_NAME_FA043 = "Fa043";
        public static readonly String PROPERTY_NAME_FA044 = "Fa044";
        public static readonly String PROPERTY_NAME_FA045 = "Fa045";
        public static readonly String PROPERTY_NAME_FA046 = "Fa046";
        public static readonly String PROPERTY_NAME_FA047 = "Fa047";
        public static readonly String PROPERTY_NAME_FA048 = "Fa048";
        public static readonly String PROPERTY_NAME_FA049 = "Fa049";
        public static readonly String PROPERTY_NAME_FA050 = "Fa050";
        public static readonly String PROPERTY_NAME_FA051 = "Fa051";
        public static readonly String PROPERTY_NAME_FA052 = "Fa052";
        public static readonly String PROPERTY_NAME_FA053 = "Fa053";
        public static readonly String PROPERTY_NAME_FA054 = "Fa054";
        public static readonly String PROPERTY_NAME_FA055 = "Fa055";
        public static readonly String PROPERTY_NAME_FA056 = "Fa056";
        public static readonly String PROPERTY_NAME_FA057 = "Fa057";
        public static readonly String PROPERTY_NAME_FA058 = "Fa058";
        public static readonly String PROPERTY_NAME_FA059 = "Fa059";
        public static readonly String PROPERTY_NAME_FA060 = "Fa060";
        public static readonly String PROPERTY_NAME_FA061 = "Fa061";
        public static readonly String PROPERTY_NAME_FA062 = "Fa062";
        public static readonly String PROPERTY_NAME_FA063 = "Fa063";
        public static readonly String PROPERTY_NAME_FA064 = "Fa064";
        public static readonly String PROPERTY_NAME_FA065 = "Fa065";
        public static readonly String PROPERTY_NAME_FA066 = "Fa066";
        public static readonly String PROPERTY_NAME_FA067 = "Fa067";
        public static readonly String PROPERTY_NAME_FA068 = "Fa068";
        public static readonly String PROPERTY_NAME_FA069 = "Fa069";
        public static readonly String PROPERTY_NAME_FA070 = "Fa070";
        public static readonly String PROPERTY_NAME_FA071 = "Fa071";
        public static readonly String PROPERTY_NAME_FA072 = "Fa072";
        public static readonly String PROPERTY_NAME_FA073 = "Fa073";
        public static readonly String PROPERTY_NAME_FA074 = "Fa074";
        public static readonly String PROPERTY_NAME_FA075 = "Fa075";
        public static readonly String PROPERTY_NAME_FA076 = "Fa076";
        public static readonly String PROPERTY_NAME_FA077 = "Fa077";
        public static readonly String PROPERTY_NAME_FA078 = "Fa078";
        public static readonly String PROPERTY_NAME_FA079 = "Fa079";
        public static readonly String PROPERTY_NAME_FA080 = "Fa080";
        public static readonly String PROPERTY_NAME_FA081 = "Fa081";
        public static readonly String PROPERTY_NAME_FA082 = "Fa082";
        public static readonly String PROPERTY_NAME_FA083 = "Fa083";
        public static readonly String PROPERTY_NAME_FA084 = "Fa084";
        public static readonly String PROPERTY_NAME_FA085 = "Fa085";
        public static readonly String PROPERTY_NAME_FA086 = "Fa086";
        public static readonly String PROPERTY_NAME_FA087 = "Fa087";
        public static readonly String PROPERTY_NAME_FA088 = "Fa088";
        public static readonly String PROPERTY_NAME_FA089 = "Fa089";
        public static readonly String PROPERTY_NAME_FA090 = "Fa090";
        public static readonly String PROPERTY_NAME_FA091 = "Fa091";
        public static readonly String PROPERTY_NAME_FA092 = "Fa092";
        public static readonly String PROPERTY_NAME_FA093 = "Fa093";
        public static readonly String PROPERTY_NAME_FA094 = "Fa094";
        public static readonly String PROPERTY_NAME_FA095 = "Fa095";
        public static readonly String PROPERTY_NAME_FA096 = "Fa096";
        public static readonly String PROPERTY_NAME_FA097 = "Fa097";
        public static readonly String PROPERTY_NAME_FA098 = "Fa098";
        public static readonly String PROPERTY_NAME_FA099 = "Fa099";
        public static readonly String PROPERTY_NAME_FA100 = "Fa100";
        public static readonly String PROPERTY_NAME_FA101 = "Fa101";
        public static readonly String PROPERTY_NAME_FA102 = "Fa102";
        public static readonly String PROPERTY_NAME_FA103 = "Fa103";
        public static readonly String PROPERTY_NAME_FA104 = "Fa104";
        public static readonly String PROPERTY_NAME_FA105 = "Fa105";
        public static readonly String PROPERTY_NAME_FA106 = "Fa106";
        public static readonly String PROPERTY_NAME_FA107 = "Fa107";
        public static readonly String PROPERTY_NAME_FA108 = "Fa108";
        public static readonly String PROPERTY_NAME_FA109 = "Fa109";
        public static readonly String PROPERTY_NAME_FA110 = "Fa110";
        public static readonly String PROPERTY_NAME_FA111 = "Fa111";
        public static readonly String PROPERTY_NAME_FA112 = "Fa112";
        public static readonly String PROPERTY_NAME_FA113 = "Fa113";
        public static readonly String PROPERTY_NAME_FA114 = "Fa114";
        public static readonly String PROPERTY_NAME_FA115 = "Fa115";
        public static readonly String PROPERTY_NAME_FA116 = "Fa116";
        public static readonly String PROPERTY_NAME_FA117 = "Fa117";
        public static readonly String PROPERTY_NAME_FA118 = "Fa118";
        public static readonly String PROPERTY_NAME_FA119 = "Fa119";
        public static readonly String PROPERTY_NAME_FA120 = "Fa120";
        public static readonly String PROPERTY_NAME_FA121 = "Fa121";
        public static readonly String PROPERTY_NAME_FA122 = "Fa122";
        public static readonly String PROPERTY_NAME_FA123 = "Fa123";
        public static readonly String PROPERTY_NAME_FA124 = "Fa124";
        public static readonly String PROPERTY_NAME_FA125 = "Fa125";
        public static readonly String PROPERTY_NAME_FA126 = "Fa126";
        public static readonly String PROPERTY_NAME_FA127 = "Fa127";
        public static readonly String PROPERTY_NAME_FA128 = "Fa128";
        public static readonly String PROPERTY_NAME_FA129 = "Fa129";
        public static readonly String PROPERTY_NAME_FA130 = "Fa130";
        public static readonly String PROPERTY_NAME_FA131 = "Fa131";
        public static readonly String PROPERTY_NAME_FA132 = "Fa132";
        public static readonly String PROPERTY_NAME_FA133 = "Fa133";
        public static readonly String PROPERTY_NAME_FA134 = "Fa134";
        public static readonly String PROPERTY_NAME_FA135 = "Fa135";
        public static readonly String PROPERTY_NAME_FA136 = "Fa136";
        public static readonly String PROPERTY_NAME_FA137 = "Fa137";
        public static readonly String PROPERTY_NAME_FA138 = "Fa138";
        public static readonly String PROPERTY_NAME_FA139 = "Fa139";
        public static readonly String PROPERTY_NAME_FA140 = "Fa140";
        public static readonly String PROPERTY_NAME_FA141 = "Fa141";
        public static readonly String PROPERTY_NAME_FA142 = "Fa142";
        public static readonly String PROPERTY_NAME_FA143 = "Fa143";
        public static readonly String PROPERTY_NAME_FA144 = "Fa144";
        public static readonly String PROPERTY_NAME_FA145 = "Fa145";
        public static readonly String PROPERTY_NAME_FA146 = "Fa146";
        public static readonly String PROPERTY_NAME_FA147 = "Fa147";
        public static readonly String PROPERTY_NAME_FA148 = "Fa148";
        public static readonly String PROPERTY_NAME_FA149 = "Fa149";
        public static readonly String PROPERTY_NAME_FA150 = "Fa150";
        public static readonly String PROPERTY_NAME_FA151 = "Fa151";
        public static readonly String PROPERTY_NAME_FA152 = "Fa152";
        public static readonly String PROPERTY_NAME_FA153 = "Fa153";
        public static readonly String PROPERTY_NAME_FA154 = "Fa154";
        public static readonly String PROPERTY_NAME_FA155 = "Fa155";
        public static readonly String PROPERTY_NAME_FA156 = "Fa156";
        public static readonly String PROPERTY_NAME_FA157 = "Fa157";
        public static readonly String PROPERTY_NAME_FA158 = "Fa158";
        public static readonly String PROPERTY_NAME_FA159 = "Fa159";
        public static readonly String PROPERTY_NAME_FA160 = "Fa160";
        public static readonly String PROPERTY_NAME_FA161 = "Fa161";
        public static readonly String PROPERTY_NAME_FA162 = "Fa162";
        public static readonly String PROPERTY_NAME_FA163 = "Fa163";
        public static readonly String PROPERTY_NAME_FA164 = "Fa164";
        public static readonly String PROPERTY_NAME_FA165 = "Fa165";
        public static readonly String PROPERTY_NAME_FA166 = "Fa166";
        public static readonly String PROPERTY_NAME_FA167 = "Fa167";
        public static readonly String PROPERTY_NAME_FA168 = "Fa168";
        public static readonly String PROPERTY_NAME_FA169 = "Fa169";
        public static readonly String PROPERTY_NAME_FA170 = "Fa170";
        public static readonly String PROPERTY_NAME_FA171 = "Fa171";
        public static readonly String PROPERTY_NAME_FA172 = "Fa172";
        public static readonly String PROPERTY_NAME_FA173 = "Fa173";
        public static readonly String PROPERTY_NAME_FA174 = "Fa174";
        public static readonly String PROPERTY_NAME_FA175 = "Fa175";
        public static readonly String PROPERTY_NAME_FA176 = "Fa176";
        public static readonly String PROPERTY_NAME_FA177 = "Fa177";
        public static readonly String PROPERTY_NAME_FA178 = "Fa178";
        public static readonly String PROPERTY_NAME_FA179 = "Fa179";
        public static readonly String PROPERTY_NAME_FA180 = "Fa180";
        public static readonly String PROPERTY_NAME_FA181 = "Fa181";
        public static readonly String PROPERTY_NAME_FA182 = "Fa182";
        public static readonly String PROPERTY_NAME_FA183 = "Fa183";
        public static readonly String PROPERTY_NAME_FA184 = "Fa184";
        public static readonly String PROPERTY_NAME_FA185 = "Fa185";
        public static readonly String PROPERTY_NAME_FA186 = "Fa186";
        public static readonly String PROPERTY_NAME_FA187 = "Fa187";
        public static readonly String PROPERTY_NAME_FA188 = "Fa188";
        public static readonly String PROPERTY_NAME_FA189 = "Fa189";
        public static readonly String PROPERTY_NAME_FA190 = "Fa190";
        public static readonly String PROPERTY_NAME_FA191 = "Fa191";
        public static readonly String PROPERTY_NAME_FA192 = "Fa192";
        public static readonly String PROPERTY_NAME_FA193 = "Fa193";
        public static readonly String PROPERTY_NAME_FA194 = "Fa194";
        public static readonly String PROPERTY_NAME_FA195 = "Fa195";
        public static readonly String PROPERTY_NAME_FA196 = "Fa196";
        public static readonly String PROPERTY_NAME_FA197 = "Fa197";
        public static readonly String PROPERTY_NAME_FA198 = "Fa198";
        public static readonly String PROPERTY_NAME_FA199 = "Fa199";
        public static readonly String PROPERTY_NAME_FA200 = "Fa200";

        // -------------------------------------------------
        //                                      Foreign Name
        //                                      ------------
        // -------------------------------------------------
        //                                     Referrer Name
        //                                     -------------

        // -------------------------------------------------
        //                               DB-Property Mapping
        //                               -------------------
        protected static readonly Map<String, String> _dbNamePropertyNameKeyToLowerMap;
        protected static readonly Map<String, String> _propertyNameDbNameKeyToLowerMap;

        static TFaDataDbm() {
            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_DB_NAME.ToLower(), TABLE_PROPERTY_NAME);
                map.put(DB_NAME_SAMPLE_ID.ToLower(), PROPERTY_NAME_SAMPLE_ID);
                map.put(DB_NAME_FA001.ToLower(), PROPERTY_NAME_FA001);
                map.put(DB_NAME_FA002.ToLower(), PROPERTY_NAME_FA002);
                map.put(DB_NAME_FA003.ToLower(), PROPERTY_NAME_FA003);
                map.put(DB_NAME_FA004.ToLower(), PROPERTY_NAME_FA004);
                map.put(DB_NAME_FA005.ToLower(), PROPERTY_NAME_FA005);
                map.put(DB_NAME_FA006.ToLower(), PROPERTY_NAME_FA006);
                map.put(DB_NAME_FA007.ToLower(), PROPERTY_NAME_FA007);
                map.put(DB_NAME_FA008.ToLower(), PROPERTY_NAME_FA008);
                map.put(DB_NAME_FA009.ToLower(), PROPERTY_NAME_FA009);
                map.put(DB_NAME_FA010.ToLower(), PROPERTY_NAME_FA010);
                map.put(DB_NAME_FA011.ToLower(), PROPERTY_NAME_FA011);
                map.put(DB_NAME_FA012.ToLower(), PROPERTY_NAME_FA012);
                map.put(DB_NAME_FA013.ToLower(), PROPERTY_NAME_FA013);
                map.put(DB_NAME_FA014.ToLower(), PROPERTY_NAME_FA014);
                map.put(DB_NAME_FA015.ToLower(), PROPERTY_NAME_FA015);
                map.put(DB_NAME_FA016.ToLower(), PROPERTY_NAME_FA016);
                map.put(DB_NAME_FA017.ToLower(), PROPERTY_NAME_FA017);
                map.put(DB_NAME_FA018.ToLower(), PROPERTY_NAME_FA018);
                map.put(DB_NAME_FA019.ToLower(), PROPERTY_NAME_FA019);
                map.put(DB_NAME_FA020.ToLower(), PROPERTY_NAME_FA020);
                map.put(DB_NAME_FA021.ToLower(), PROPERTY_NAME_FA021);
                map.put(DB_NAME_FA022.ToLower(), PROPERTY_NAME_FA022);
                map.put(DB_NAME_FA023.ToLower(), PROPERTY_NAME_FA023);
                map.put(DB_NAME_FA024.ToLower(), PROPERTY_NAME_FA024);
                map.put(DB_NAME_FA025.ToLower(), PROPERTY_NAME_FA025);
                map.put(DB_NAME_FA026.ToLower(), PROPERTY_NAME_FA026);
                map.put(DB_NAME_FA027.ToLower(), PROPERTY_NAME_FA027);
                map.put(DB_NAME_FA028.ToLower(), PROPERTY_NAME_FA028);
                map.put(DB_NAME_FA029.ToLower(), PROPERTY_NAME_FA029);
                map.put(DB_NAME_FA030.ToLower(), PROPERTY_NAME_FA030);
                map.put(DB_NAME_FA031.ToLower(), PROPERTY_NAME_FA031);
                map.put(DB_NAME_FA032.ToLower(), PROPERTY_NAME_FA032);
                map.put(DB_NAME_FA033.ToLower(), PROPERTY_NAME_FA033);
                map.put(DB_NAME_FA034.ToLower(), PROPERTY_NAME_FA034);
                map.put(DB_NAME_FA035.ToLower(), PROPERTY_NAME_FA035);
                map.put(DB_NAME_FA036.ToLower(), PROPERTY_NAME_FA036);
                map.put(DB_NAME_FA037.ToLower(), PROPERTY_NAME_FA037);
                map.put(DB_NAME_FA038.ToLower(), PROPERTY_NAME_FA038);
                map.put(DB_NAME_FA039.ToLower(), PROPERTY_NAME_FA039);
                map.put(DB_NAME_FA040.ToLower(), PROPERTY_NAME_FA040);
                map.put(DB_NAME_FA041.ToLower(), PROPERTY_NAME_FA041);
                map.put(DB_NAME_FA042.ToLower(), PROPERTY_NAME_FA042);
                map.put(DB_NAME_FA043.ToLower(), PROPERTY_NAME_FA043);
                map.put(DB_NAME_FA044.ToLower(), PROPERTY_NAME_FA044);
                map.put(DB_NAME_FA045.ToLower(), PROPERTY_NAME_FA045);
                map.put(DB_NAME_FA046.ToLower(), PROPERTY_NAME_FA046);
                map.put(DB_NAME_FA047.ToLower(), PROPERTY_NAME_FA047);
                map.put(DB_NAME_FA048.ToLower(), PROPERTY_NAME_FA048);
                map.put(DB_NAME_FA049.ToLower(), PROPERTY_NAME_FA049);
                map.put(DB_NAME_FA050.ToLower(), PROPERTY_NAME_FA050);
                map.put(DB_NAME_FA051.ToLower(), PROPERTY_NAME_FA051);
                map.put(DB_NAME_FA052.ToLower(), PROPERTY_NAME_FA052);
                map.put(DB_NAME_FA053.ToLower(), PROPERTY_NAME_FA053);
                map.put(DB_NAME_FA054.ToLower(), PROPERTY_NAME_FA054);
                map.put(DB_NAME_FA055.ToLower(), PROPERTY_NAME_FA055);
                map.put(DB_NAME_FA056.ToLower(), PROPERTY_NAME_FA056);
                map.put(DB_NAME_FA057.ToLower(), PROPERTY_NAME_FA057);
                map.put(DB_NAME_FA058.ToLower(), PROPERTY_NAME_FA058);
                map.put(DB_NAME_FA059.ToLower(), PROPERTY_NAME_FA059);
                map.put(DB_NAME_FA060.ToLower(), PROPERTY_NAME_FA060);
                map.put(DB_NAME_FA061.ToLower(), PROPERTY_NAME_FA061);
                map.put(DB_NAME_FA062.ToLower(), PROPERTY_NAME_FA062);
                map.put(DB_NAME_FA063.ToLower(), PROPERTY_NAME_FA063);
                map.put(DB_NAME_FA064.ToLower(), PROPERTY_NAME_FA064);
                map.put(DB_NAME_FA065.ToLower(), PROPERTY_NAME_FA065);
                map.put(DB_NAME_FA066.ToLower(), PROPERTY_NAME_FA066);
                map.put(DB_NAME_FA067.ToLower(), PROPERTY_NAME_FA067);
                map.put(DB_NAME_FA068.ToLower(), PROPERTY_NAME_FA068);
                map.put(DB_NAME_FA069.ToLower(), PROPERTY_NAME_FA069);
                map.put(DB_NAME_FA070.ToLower(), PROPERTY_NAME_FA070);
                map.put(DB_NAME_FA071.ToLower(), PROPERTY_NAME_FA071);
                map.put(DB_NAME_FA072.ToLower(), PROPERTY_NAME_FA072);
                map.put(DB_NAME_FA073.ToLower(), PROPERTY_NAME_FA073);
                map.put(DB_NAME_FA074.ToLower(), PROPERTY_NAME_FA074);
                map.put(DB_NAME_FA075.ToLower(), PROPERTY_NAME_FA075);
                map.put(DB_NAME_FA076.ToLower(), PROPERTY_NAME_FA076);
                map.put(DB_NAME_FA077.ToLower(), PROPERTY_NAME_FA077);
                map.put(DB_NAME_FA078.ToLower(), PROPERTY_NAME_FA078);
                map.put(DB_NAME_FA079.ToLower(), PROPERTY_NAME_FA079);
                map.put(DB_NAME_FA080.ToLower(), PROPERTY_NAME_FA080);
                map.put(DB_NAME_FA081.ToLower(), PROPERTY_NAME_FA081);
                map.put(DB_NAME_FA082.ToLower(), PROPERTY_NAME_FA082);
                map.put(DB_NAME_FA083.ToLower(), PROPERTY_NAME_FA083);
                map.put(DB_NAME_FA084.ToLower(), PROPERTY_NAME_FA084);
                map.put(DB_NAME_FA085.ToLower(), PROPERTY_NAME_FA085);
                map.put(DB_NAME_FA086.ToLower(), PROPERTY_NAME_FA086);
                map.put(DB_NAME_FA087.ToLower(), PROPERTY_NAME_FA087);
                map.put(DB_NAME_FA088.ToLower(), PROPERTY_NAME_FA088);
                map.put(DB_NAME_FA089.ToLower(), PROPERTY_NAME_FA089);
                map.put(DB_NAME_FA090.ToLower(), PROPERTY_NAME_FA090);
                map.put(DB_NAME_FA091.ToLower(), PROPERTY_NAME_FA091);
                map.put(DB_NAME_FA092.ToLower(), PROPERTY_NAME_FA092);
                map.put(DB_NAME_FA093.ToLower(), PROPERTY_NAME_FA093);
                map.put(DB_NAME_FA094.ToLower(), PROPERTY_NAME_FA094);
                map.put(DB_NAME_FA095.ToLower(), PROPERTY_NAME_FA095);
                map.put(DB_NAME_FA096.ToLower(), PROPERTY_NAME_FA096);
                map.put(DB_NAME_FA097.ToLower(), PROPERTY_NAME_FA097);
                map.put(DB_NAME_FA098.ToLower(), PROPERTY_NAME_FA098);
                map.put(DB_NAME_FA099.ToLower(), PROPERTY_NAME_FA099);
                map.put(DB_NAME_FA100.ToLower(), PROPERTY_NAME_FA100);
                map.put(DB_NAME_FA101.ToLower(), PROPERTY_NAME_FA101);
                map.put(DB_NAME_FA102.ToLower(), PROPERTY_NAME_FA102);
                map.put(DB_NAME_FA103.ToLower(), PROPERTY_NAME_FA103);
                map.put(DB_NAME_FA104.ToLower(), PROPERTY_NAME_FA104);
                map.put(DB_NAME_FA105.ToLower(), PROPERTY_NAME_FA105);
                map.put(DB_NAME_FA106.ToLower(), PROPERTY_NAME_FA106);
                map.put(DB_NAME_FA107.ToLower(), PROPERTY_NAME_FA107);
                map.put(DB_NAME_FA108.ToLower(), PROPERTY_NAME_FA108);
                map.put(DB_NAME_FA109.ToLower(), PROPERTY_NAME_FA109);
                map.put(DB_NAME_FA110.ToLower(), PROPERTY_NAME_FA110);
                map.put(DB_NAME_FA111.ToLower(), PROPERTY_NAME_FA111);
                map.put(DB_NAME_FA112.ToLower(), PROPERTY_NAME_FA112);
                map.put(DB_NAME_FA113.ToLower(), PROPERTY_NAME_FA113);
                map.put(DB_NAME_FA114.ToLower(), PROPERTY_NAME_FA114);
                map.put(DB_NAME_FA115.ToLower(), PROPERTY_NAME_FA115);
                map.put(DB_NAME_FA116.ToLower(), PROPERTY_NAME_FA116);
                map.put(DB_NAME_FA117.ToLower(), PROPERTY_NAME_FA117);
                map.put(DB_NAME_FA118.ToLower(), PROPERTY_NAME_FA118);
                map.put(DB_NAME_FA119.ToLower(), PROPERTY_NAME_FA119);
                map.put(DB_NAME_FA120.ToLower(), PROPERTY_NAME_FA120);
                map.put(DB_NAME_FA121.ToLower(), PROPERTY_NAME_FA121);
                map.put(DB_NAME_FA122.ToLower(), PROPERTY_NAME_FA122);
                map.put(DB_NAME_FA123.ToLower(), PROPERTY_NAME_FA123);
                map.put(DB_NAME_FA124.ToLower(), PROPERTY_NAME_FA124);
                map.put(DB_NAME_FA125.ToLower(), PROPERTY_NAME_FA125);
                map.put(DB_NAME_FA126.ToLower(), PROPERTY_NAME_FA126);
                map.put(DB_NAME_FA127.ToLower(), PROPERTY_NAME_FA127);
                map.put(DB_NAME_FA128.ToLower(), PROPERTY_NAME_FA128);
                map.put(DB_NAME_FA129.ToLower(), PROPERTY_NAME_FA129);
                map.put(DB_NAME_FA130.ToLower(), PROPERTY_NAME_FA130);
                map.put(DB_NAME_FA131.ToLower(), PROPERTY_NAME_FA131);
                map.put(DB_NAME_FA132.ToLower(), PROPERTY_NAME_FA132);
                map.put(DB_NAME_FA133.ToLower(), PROPERTY_NAME_FA133);
                map.put(DB_NAME_FA134.ToLower(), PROPERTY_NAME_FA134);
                map.put(DB_NAME_FA135.ToLower(), PROPERTY_NAME_FA135);
                map.put(DB_NAME_FA136.ToLower(), PROPERTY_NAME_FA136);
                map.put(DB_NAME_FA137.ToLower(), PROPERTY_NAME_FA137);
                map.put(DB_NAME_FA138.ToLower(), PROPERTY_NAME_FA138);
                map.put(DB_NAME_FA139.ToLower(), PROPERTY_NAME_FA139);
                map.put(DB_NAME_FA140.ToLower(), PROPERTY_NAME_FA140);
                map.put(DB_NAME_FA141.ToLower(), PROPERTY_NAME_FA141);
                map.put(DB_NAME_FA142.ToLower(), PROPERTY_NAME_FA142);
                map.put(DB_NAME_FA143.ToLower(), PROPERTY_NAME_FA143);
                map.put(DB_NAME_FA144.ToLower(), PROPERTY_NAME_FA144);
                map.put(DB_NAME_FA145.ToLower(), PROPERTY_NAME_FA145);
                map.put(DB_NAME_FA146.ToLower(), PROPERTY_NAME_FA146);
                map.put(DB_NAME_FA147.ToLower(), PROPERTY_NAME_FA147);
                map.put(DB_NAME_FA148.ToLower(), PROPERTY_NAME_FA148);
                map.put(DB_NAME_FA149.ToLower(), PROPERTY_NAME_FA149);
                map.put(DB_NAME_FA150.ToLower(), PROPERTY_NAME_FA150);
                map.put(DB_NAME_FA151.ToLower(), PROPERTY_NAME_FA151);
                map.put(DB_NAME_FA152.ToLower(), PROPERTY_NAME_FA152);
                map.put(DB_NAME_FA153.ToLower(), PROPERTY_NAME_FA153);
                map.put(DB_NAME_FA154.ToLower(), PROPERTY_NAME_FA154);
                map.put(DB_NAME_FA155.ToLower(), PROPERTY_NAME_FA155);
                map.put(DB_NAME_FA156.ToLower(), PROPERTY_NAME_FA156);
                map.put(DB_NAME_FA157.ToLower(), PROPERTY_NAME_FA157);
                map.put(DB_NAME_FA158.ToLower(), PROPERTY_NAME_FA158);
                map.put(DB_NAME_FA159.ToLower(), PROPERTY_NAME_FA159);
                map.put(DB_NAME_FA160.ToLower(), PROPERTY_NAME_FA160);
                map.put(DB_NAME_FA161.ToLower(), PROPERTY_NAME_FA161);
                map.put(DB_NAME_FA162.ToLower(), PROPERTY_NAME_FA162);
                map.put(DB_NAME_FA163.ToLower(), PROPERTY_NAME_FA163);
                map.put(DB_NAME_FA164.ToLower(), PROPERTY_NAME_FA164);
                map.put(DB_NAME_FA165.ToLower(), PROPERTY_NAME_FA165);
                map.put(DB_NAME_FA166.ToLower(), PROPERTY_NAME_FA166);
                map.put(DB_NAME_FA167.ToLower(), PROPERTY_NAME_FA167);
                map.put(DB_NAME_FA168.ToLower(), PROPERTY_NAME_FA168);
                map.put(DB_NAME_FA169.ToLower(), PROPERTY_NAME_FA169);
                map.put(DB_NAME_FA170.ToLower(), PROPERTY_NAME_FA170);
                map.put(DB_NAME_FA171.ToLower(), PROPERTY_NAME_FA171);
                map.put(DB_NAME_FA172.ToLower(), PROPERTY_NAME_FA172);
                map.put(DB_NAME_FA173.ToLower(), PROPERTY_NAME_FA173);
                map.put(DB_NAME_FA174.ToLower(), PROPERTY_NAME_FA174);
                map.put(DB_NAME_FA175.ToLower(), PROPERTY_NAME_FA175);
                map.put(DB_NAME_FA176.ToLower(), PROPERTY_NAME_FA176);
                map.put(DB_NAME_FA177.ToLower(), PROPERTY_NAME_FA177);
                map.put(DB_NAME_FA178.ToLower(), PROPERTY_NAME_FA178);
                map.put(DB_NAME_FA179.ToLower(), PROPERTY_NAME_FA179);
                map.put(DB_NAME_FA180.ToLower(), PROPERTY_NAME_FA180);
                map.put(DB_NAME_FA181.ToLower(), PROPERTY_NAME_FA181);
                map.put(DB_NAME_FA182.ToLower(), PROPERTY_NAME_FA182);
                map.put(DB_NAME_FA183.ToLower(), PROPERTY_NAME_FA183);
                map.put(DB_NAME_FA184.ToLower(), PROPERTY_NAME_FA184);
                map.put(DB_NAME_FA185.ToLower(), PROPERTY_NAME_FA185);
                map.put(DB_NAME_FA186.ToLower(), PROPERTY_NAME_FA186);
                map.put(DB_NAME_FA187.ToLower(), PROPERTY_NAME_FA187);
                map.put(DB_NAME_FA188.ToLower(), PROPERTY_NAME_FA188);
                map.put(DB_NAME_FA189.ToLower(), PROPERTY_NAME_FA189);
                map.put(DB_NAME_FA190.ToLower(), PROPERTY_NAME_FA190);
                map.put(DB_NAME_FA191.ToLower(), PROPERTY_NAME_FA191);
                map.put(DB_NAME_FA192.ToLower(), PROPERTY_NAME_FA192);
                map.put(DB_NAME_FA193.ToLower(), PROPERTY_NAME_FA193);
                map.put(DB_NAME_FA194.ToLower(), PROPERTY_NAME_FA194);
                map.put(DB_NAME_FA195.ToLower(), PROPERTY_NAME_FA195);
                map.put(DB_NAME_FA196.ToLower(), PROPERTY_NAME_FA196);
                map.put(DB_NAME_FA197.ToLower(), PROPERTY_NAME_FA197);
                map.put(DB_NAME_FA198.ToLower(), PROPERTY_NAME_FA198);
                map.put(DB_NAME_FA199.ToLower(), PROPERTY_NAME_FA199);
                map.put(DB_NAME_FA200.ToLower(), PROPERTY_NAME_FA200);
                _dbNamePropertyNameKeyToLowerMap = map;
            }

            {
                Map<String, String> map = new LinkedHashMap<String, String>();
                map.put(TABLE_PROPERTY_NAME.ToLower(), TABLE_DB_NAME);
                map.put(PROPERTY_NAME_SAMPLE_ID.ToLower(), DB_NAME_SAMPLE_ID);
                map.put(PROPERTY_NAME_FA001.ToLower(), DB_NAME_FA001);
                map.put(PROPERTY_NAME_FA002.ToLower(), DB_NAME_FA002);
                map.put(PROPERTY_NAME_FA003.ToLower(), DB_NAME_FA003);
                map.put(PROPERTY_NAME_FA004.ToLower(), DB_NAME_FA004);
                map.put(PROPERTY_NAME_FA005.ToLower(), DB_NAME_FA005);
                map.put(PROPERTY_NAME_FA006.ToLower(), DB_NAME_FA006);
                map.put(PROPERTY_NAME_FA007.ToLower(), DB_NAME_FA007);
                map.put(PROPERTY_NAME_FA008.ToLower(), DB_NAME_FA008);
                map.put(PROPERTY_NAME_FA009.ToLower(), DB_NAME_FA009);
                map.put(PROPERTY_NAME_FA010.ToLower(), DB_NAME_FA010);
                map.put(PROPERTY_NAME_FA011.ToLower(), DB_NAME_FA011);
                map.put(PROPERTY_NAME_FA012.ToLower(), DB_NAME_FA012);
                map.put(PROPERTY_NAME_FA013.ToLower(), DB_NAME_FA013);
                map.put(PROPERTY_NAME_FA014.ToLower(), DB_NAME_FA014);
                map.put(PROPERTY_NAME_FA015.ToLower(), DB_NAME_FA015);
                map.put(PROPERTY_NAME_FA016.ToLower(), DB_NAME_FA016);
                map.put(PROPERTY_NAME_FA017.ToLower(), DB_NAME_FA017);
                map.put(PROPERTY_NAME_FA018.ToLower(), DB_NAME_FA018);
                map.put(PROPERTY_NAME_FA019.ToLower(), DB_NAME_FA019);
                map.put(PROPERTY_NAME_FA020.ToLower(), DB_NAME_FA020);
                map.put(PROPERTY_NAME_FA021.ToLower(), DB_NAME_FA021);
                map.put(PROPERTY_NAME_FA022.ToLower(), DB_NAME_FA022);
                map.put(PROPERTY_NAME_FA023.ToLower(), DB_NAME_FA023);
                map.put(PROPERTY_NAME_FA024.ToLower(), DB_NAME_FA024);
                map.put(PROPERTY_NAME_FA025.ToLower(), DB_NAME_FA025);
                map.put(PROPERTY_NAME_FA026.ToLower(), DB_NAME_FA026);
                map.put(PROPERTY_NAME_FA027.ToLower(), DB_NAME_FA027);
                map.put(PROPERTY_NAME_FA028.ToLower(), DB_NAME_FA028);
                map.put(PROPERTY_NAME_FA029.ToLower(), DB_NAME_FA029);
                map.put(PROPERTY_NAME_FA030.ToLower(), DB_NAME_FA030);
                map.put(PROPERTY_NAME_FA031.ToLower(), DB_NAME_FA031);
                map.put(PROPERTY_NAME_FA032.ToLower(), DB_NAME_FA032);
                map.put(PROPERTY_NAME_FA033.ToLower(), DB_NAME_FA033);
                map.put(PROPERTY_NAME_FA034.ToLower(), DB_NAME_FA034);
                map.put(PROPERTY_NAME_FA035.ToLower(), DB_NAME_FA035);
                map.put(PROPERTY_NAME_FA036.ToLower(), DB_NAME_FA036);
                map.put(PROPERTY_NAME_FA037.ToLower(), DB_NAME_FA037);
                map.put(PROPERTY_NAME_FA038.ToLower(), DB_NAME_FA038);
                map.put(PROPERTY_NAME_FA039.ToLower(), DB_NAME_FA039);
                map.put(PROPERTY_NAME_FA040.ToLower(), DB_NAME_FA040);
                map.put(PROPERTY_NAME_FA041.ToLower(), DB_NAME_FA041);
                map.put(PROPERTY_NAME_FA042.ToLower(), DB_NAME_FA042);
                map.put(PROPERTY_NAME_FA043.ToLower(), DB_NAME_FA043);
                map.put(PROPERTY_NAME_FA044.ToLower(), DB_NAME_FA044);
                map.put(PROPERTY_NAME_FA045.ToLower(), DB_NAME_FA045);
                map.put(PROPERTY_NAME_FA046.ToLower(), DB_NAME_FA046);
                map.put(PROPERTY_NAME_FA047.ToLower(), DB_NAME_FA047);
                map.put(PROPERTY_NAME_FA048.ToLower(), DB_NAME_FA048);
                map.put(PROPERTY_NAME_FA049.ToLower(), DB_NAME_FA049);
                map.put(PROPERTY_NAME_FA050.ToLower(), DB_NAME_FA050);
                map.put(PROPERTY_NAME_FA051.ToLower(), DB_NAME_FA051);
                map.put(PROPERTY_NAME_FA052.ToLower(), DB_NAME_FA052);
                map.put(PROPERTY_NAME_FA053.ToLower(), DB_NAME_FA053);
                map.put(PROPERTY_NAME_FA054.ToLower(), DB_NAME_FA054);
                map.put(PROPERTY_NAME_FA055.ToLower(), DB_NAME_FA055);
                map.put(PROPERTY_NAME_FA056.ToLower(), DB_NAME_FA056);
                map.put(PROPERTY_NAME_FA057.ToLower(), DB_NAME_FA057);
                map.put(PROPERTY_NAME_FA058.ToLower(), DB_NAME_FA058);
                map.put(PROPERTY_NAME_FA059.ToLower(), DB_NAME_FA059);
                map.put(PROPERTY_NAME_FA060.ToLower(), DB_NAME_FA060);
                map.put(PROPERTY_NAME_FA061.ToLower(), DB_NAME_FA061);
                map.put(PROPERTY_NAME_FA062.ToLower(), DB_NAME_FA062);
                map.put(PROPERTY_NAME_FA063.ToLower(), DB_NAME_FA063);
                map.put(PROPERTY_NAME_FA064.ToLower(), DB_NAME_FA064);
                map.put(PROPERTY_NAME_FA065.ToLower(), DB_NAME_FA065);
                map.put(PROPERTY_NAME_FA066.ToLower(), DB_NAME_FA066);
                map.put(PROPERTY_NAME_FA067.ToLower(), DB_NAME_FA067);
                map.put(PROPERTY_NAME_FA068.ToLower(), DB_NAME_FA068);
                map.put(PROPERTY_NAME_FA069.ToLower(), DB_NAME_FA069);
                map.put(PROPERTY_NAME_FA070.ToLower(), DB_NAME_FA070);
                map.put(PROPERTY_NAME_FA071.ToLower(), DB_NAME_FA071);
                map.put(PROPERTY_NAME_FA072.ToLower(), DB_NAME_FA072);
                map.put(PROPERTY_NAME_FA073.ToLower(), DB_NAME_FA073);
                map.put(PROPERTY_NAME_FA074.ToLower(), DB_NAME_FA074);
                map.put(PROPERTY_NAME_FA075.ToLower(), DB_NAME_FA075);
                map.put(PROPERTY_NAME_FA076.ToLower(), DB_NAME_FA076);
                map.put(PROPERTY_NAME_FA077.ToLower(), DB_NAME_FA077);
                map.put(PROPERTY_NAME_FA078.ToLower(), DB_NAME_FA078);
                map.put(PROPERTY_NAME_FA079.ToLower(), DB_NAME_FA079);
                map.put(PROPERTY_NAME_FA080.ToLower(), DB_NAME_FA080);
                map.put(PROPERTY_NAME_FA081.ToLower(), DB_NAME_FA081);
                map.put(PROPERTY_NAME_FA082.ToLower(), DB_NAME_FA082);
                map.put(PROPERTY_NAME_FA083.ToLower(), DB_NAME_FA083);
                map.put(PROPERTY_NAME_FA084.ToLower(), DB_NAME_FA084);
                map.put(PROPERTY_NAME_FA085.ToLower(), DB_NAME_FA085);
                map.put(PROPERTY_NAME_FA086.ToLower(), DB_NAME_FA086);
                map.put(PROPERTY_NAME_FA087.ToLower(), DB_NAME_FA087);
                map.put(PROPERTY_NAME_FA088.ToLower(), DB_NAME_FA088);
                map.put(PROPERTY_NAME_FA089.ToLower(), DB_NAME_FA089);
                map.put(PROPERTY_NAME_FA090.ToLower(), DB_NAME_FA090);
                map.put(PROPERTY_NAME_FA091.ToLower(), DB_NAME_FA091);
                map.put(PROPERTY_NAME_FA092.ToLower(), DB_NAME_FA092);
                map.put(PROPERTY_NAME_FA093.ToLower(), DB_NAME_FA093);
                map.put(PROPERTY_NAME_FA094.ToLower(), DB_NAME_FA094);
                map.put(PROPERTY_NAME_FA095.ToLower(), DB_NAME_FA095);
                map.put(PROPERTY_NAME_FA096.ToLower(), DB_NAME_FA096);
                map.put(PROPERTY_NAME_FA097.ToLower(), DB_NAME_FA097);
                map.put(PROPERTY_NAME_FA098.ToLower(), DB_NAME_FA098);
                map.put(PROPERTY_NAME_FA099.ToLower(), DB_NAME_FA099);
                map.put(PROPERTY_NAME_FA100.ToLower(), DB_NAME_FA100);
                map.put(PROPERTY_NAME_FA101.ToLower(), DB_NAME_FA101);
                map.put(PROPERTY_NAME_FA102.ToLower(), DB_NAME_FA102);
                map.put(PROPERTY_NAME_FA103.ToLower(), DB_NAME_FA103);
                map.put(PROPERTY_NAME_FA104.ToLower(), DB_NAME_FA104);
                map.put(PROPERTY_NAME_FA105.ToLower(), DB_NAME_FA105);
                map.put(PROPERTY_NAME_FA106.ToLower(), DB_NAME_FA106);
                map.put(PROPERTY_NAME_FA107.ToLower(), DB_NAME_FA107);
                map.put(PROPERTY_NAME_FA108.ToLower(), DB_NAME_FA108);
                map.put(PROPERTY_NAME_FA109.ToLower(), DB_NAME_FA109);
                map.put(PROPERTY_NAME_FA110.ToLower(), DB_NAME_FA110);
                map.put(PROPERTY_NAME_FA111.ToLower(), DB_NAME_FA111);
                map.put(PROPERTY_NAME_FA112.ToLower(), DB_NAME_FA112);
                map.put(PROPERTY_NAME_FA113.ToLower(), DB_NAME_FA113);
                map.put(PROPERTY_NAME_FA114.ToLower(), DB_NAME_FA114);
                map.put(PROPERTY_NAME_FA115.ToLower(), DB_NAME_FA115);
                map.put(PROPERTY_NAME_FA116.ToLower(), DB_NAME_FA116);
                map.put(PROPERTY_NAME_FA117.ToLower(), DB_NAME_FA117);
                map.put(PROPERTY_NAME_FA118.ToLower(), DB_NAME_FA118);
                map.put(PROPERTY_NAME_FA119.ToLower(), DB_NAME_FA119);
                map.put(PROPERTY_NAME_FA120.ToLower(), DB_NAME_FA120);
                map.put(PROPERTY_NAME_FA121.ToLower(), DB_NAME_FA121);
                map.put(PROPERTY_NAME_FA122.ToLower(), DB_NAME_FA122);
                map.put(PROPERTY_NAME_FA123.ToLower(), DB_NAME_FA123);
                map.put(PROPERTY_NAME_FA124.ToLower(), DB_NAME_FA124);
                map.put(PROPERTY_NAME_FA125.ToLower(), DB_NAME_FA125);
                map.put(PROPERTY_NAME_FA126.ToLower(), DB_NAME_FA126);
                map.put(PROPERTY_NAME_FA127.ToLower(), DB_NAME_FA127);
                map.put(PROPERTY_NAME_FA128.ToLower(), DB_NAME_FA128);
                map.put(PROPERTY_NAME_FA129.ToLower(), DB_NAME_FA129);
                map.put(PROPERTY_NAME_FA130.ToLower(), DB_NAME_FA130);
                map.put(PROPERTY_NAME_FA131.ToLower(), DB_NAME_FA131);
                map.put(PROPERTY_NAME_FA132.ToLower(), DB_NAME_FA132);
                map.put(PROPERTY_NAME_FA133.ToLower(), DB_NAME_FA133);
                map.put(PROPERTY_NAME_FA134.ToLower(), DB_NAME_FA134);
                map.put(PROPERTY_NAME_FA135.ToLower(), DB_NAME_FA135);
                map.put(PROPERTY_NAME_FA136.ToLower(), DB_NAME_FA136);
                map.put(PROPERTY_NAME_FA137.ToLower(), DB_NAME_FA137);
                map.put(PROPERTY_NAME_FA138.ToLower(), DB_NAME_FA138);
                map.put(PROPERTY_NAME_FA139.ToLower(), DB_NAME_FA139);
                map.put(PROPERTY_NAME_FA140.ToLower(), DB_NAME_FA140);
                map.put(PROPERTY_NAME_FA141.ToLower(), DB_NAME_FA141);
                map.put(PROPERTY_NAME_FA142.ToLower(), DB_NAME_FA142);
                map.put(PROPERTY_NAME_FA143.ToLower(), DB_NAME_FA143);
                map.put(PROPERTY_NAME_FA144.ToLower(), DB_NAME_FA144);
                map.put(PROPERTY_NAME_FA145.ToLower(), DB_NAME_FA145);
                map.put(PROPERTY_NAME_FA146.ToLower(), DB_NAME_FA146);
                map.put(PROPERTY_NAME_FA147.ToLower(), DB_NAME_FA147);
                map.put(PROPERTY_NAME_FA148.ToLower(), DB_NAME_FA148);
                map.put(PROPERTY_NAME_FA149.ToLower(), DB_NAME_FA149);
                map.put(PROPERTY_NAME_FA150.ToLower(), DB_NAME_FA150);
                map.put(PROPERTY_NAME_FA151.ToLower(), DB_NAME_FA151);
                map.put(PROPERTY_NAME_FA152.ToLower(), DB_NAME_FA152);
                map.put(PROPERTY_NAME_FA153.ToLower(), DB_NAME_FA153);
                map.put(PROPERTY_NAME_FA154.ToLower(), DB_NAME_FA154);
                map.put(PROPERTY_NAME_FA155.ToLower(), DB_NAME_FA155);
                map.put(PROPERTY_NAME_FA156.ToLower(), DB_NAME_FA156);
                map.put(PROPERTY_NAME_FA157.ToLower(), DB_NAME_FA157);
                map.put(PROPERTY_NAME_FA158.ToLower(), DB_NAME_FA158);
                map.put(PROPERTY_NAME_FA159.ToLower(), DB_NAME_FA159);
                map.put(PROPERTY_NAME_FA160.ToLower(), DB_NAME_FA160);
                map.put(PROPERTY_NAME_FA161.ToLower(), DB_NAME_FA161);
                map.put(PROPERTY_NAME_FA162.ToLower(), DB_NAME_FA162);
                map.put(PROPERTY_NAME_FA163.ToLower(), DB_NAME_FA163);
                map.put(PROPERTY_NAME_FA164.ToLower(), DB_NAME_FA164);
                map.put(PROPERTY_NAME_FA165.ToLower(), DB_NAME_FA165);
                map.put(PROPERTY_NAME_FA166.ToLower(), DB_NAME_FA166);
                map.put(PROPERTY_NAME_FA167.ToLower(), DB_NAME_FA167);
                map.put(PROPERTY_NAME_FA168.ToLower(), DB_NAME_FA168);
                map.put(PROPERTY_NAME_FA169.ToLower(), DB_NAME_FA169);
                map.put(PROPERTY_NAME_FA170.ToLower(), DB_NAME_FA170);
                map.put(PROPERTY_NAME_FA171.ToLower(), DB_NAME_FA171);
                map.put(PROPERTY_NAME_FA172.ToLower(), DB_NAME_FA172);
                map.put(PROPERTY_NAME_FA173.ToLower(), DB_NAME_FA173);
                map.put(PROPERTY_NAME_FA174.ToLower(), DB_NAME_FA174);
                map.put(PROPERTY_NAME_FA175.ToLower(), DB_NAME_FA175);
                map.put(PROPERTY_NAME_FA176.ToLower(), DB_NAME_FA176);
                map.put(PROPERTY_NAME_FA177.ToLower(), DB_NAME_FA177);
                map.put(PROPERTY_NAME_FA178.ToLower(), DB_NAME_FA178);
                map.put(PROPERTY_NAME_FA179.ToLower(), DB_NAME_FA179);
                map.put(PROPERTY_NAME_FA180.ToLower(), DB_NAME_FA180);
                map.put(PROPERTY_NAME_FA181.ToLower(), DB_NAME_FA181);
                map.put(PROPERTY_NAME_FA182.ToLower(), DB_NAME_FA182);
                map.put(PROPERTY_NAME_FA183.ToLower(), DB_NAME_FA183);
                map.put(PROPERTY_NAME_FA184.ToLower(), DB_NAME_FA184);
                map.put(PROPERTY_NAME_FA185.ToLower(), DB_NAME_FA185);
                map.put(PROPERTY_NAME_FA186.ToLower(), DB_NAME_FA186);
                map.put(PROPERTY_NAME_FA187.ToLower(), DB_NAME_FA187);
                map.put(PROPERTY_NAME_FA188.ToLower(), DB_NAME_FA188);
                map.put(PROPERTY_NAME_FA189.ToLower(), DB_NAME_FA189);
                map.put(PROPERTY_NAME_FA190.ToLower(), DB_NAME_FA190);
                map.put(PROPERTY_NAME_FA191.ToLower(), DB_NAME_FA191);
                map.put(PROPERTY_NAME_FA192.ToLower(), DB_NAME_FA192);
                map.put(PROPERTY_NAME_FA193.ToLower(), DB_NAME_FA193);
                map.put(PROPERTY_NAME_FA194.ToLower(), DB_NAME_FA194);
                map.put(PROPERTY_NAME_FA195.ToLower(), DB_NAME_FA195);
                map.put(PROPERTY_NAME_FA196.ToLower(), DB_NAME_FA196);
                map.put(PROPERTY_NAME_FA197.ToLower(), DB_NAME_FA197);
                map.put(PROPERTY_NAME_FA198.ToLower(), DB_NAME_FA198);
                map.put(PROPERTY_NAME_FA199.ToLower(), DB_NAME_FA199);
                map.put(PROPERTY_NAME_FA200.ToLower(), DB_NAME_FA200);
                _propertyNameDbNameKeyToLowerMap = map;
            }
        }

        #endregion

        // ===============================================================================
        //                                                                        Name Map
        //                                                                        ========
        #region Name Map
        public override Map<String, String> DbNamePropertyNameKeyToLowerMap { get { return _dbNamePropertyNameKeyToLowerMap; } }
        public override Map<String, String> PropertyNameDbNameKeyToLowerMap { get { return _propertyNameDbNameKeyToLowerMap; } }
        #endregion

        // ===============================================================================
        //                                                                       Type Name
        //                                                                       =========
        public override String EntityTypeName { get { return "Macromill.QCWeb.Dao.ExEntity.TFaData"; } }
        public override String DaoTypeName { get { return "Macromill.QCWeb.Dao.ExDao.TFaDataDao"; } }
        public override String ConditionBeanTypeName { get { return "Macromill.QCWeb.Dao.CBean.TFaDataCB"; } }
        public override String BehaviorTypeName { get { return "Macromill.QCWeb.Dao.ExBhv.TFaDataBhv"; } }

        // ===============================================================================
        //                                                                     Object Type
        //                                                                     ===========
        public override Type EntityType { get { return ENTITY_TYPE; } }

        // ===============================================================================
        //                                                                 Object Instance
        //                                                                 ===============
        public override Entity NewEntity() { return NewMyEntity(); }
        public TFaData NewMyEntity() { return new TFaData(); }
        public override ConditionBean NewConditionBean() { return NewMyConditionBean(); }
        public TFaDataCB NewMyConditionBean() { return new TFaDataCB(); }

        // ===============================================================================
        //                                                           Entity Property Setup
        //                                                           =====================
        protected Map<String, EntityPropertySetupper<TFaData>> _entityPropertySetupperMap = new LinkedHashMap<String, EntityPropertySetupper<TFaData>>();

        protected void InitializeEntityPropertySetupper() {
            RegisterEntityPropertySetupper("SAMPLE_ID", "SampleId", new EntityPropertySampleIdSetupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA001", "Fa001", new EntityPropertyFa001Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA002", "Fa002", new EntityPropertyFa002Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA003", "Fa003", new EntityPropertyFa003Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA004", "Fa004", new EntityPropertyFa004Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA005", "Fa005", new EntityPropertyFa005Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA006", "Fa006", new EntityPropertyFa006Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA007", "Fa007", new EntityPropertyFa007Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA008", "Fa008", new EntityPropertyFa008Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA009", "Fa009", new EntityPropertyFa009Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA010", "Fa010", new EntityPropertyFa010Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA011", "Fa011", new EntityPropertyFa011Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA012", "Fa012", new EntityPropertyFa012Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA013", "Fa013", new EntityPropertyFa013Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA014", "Fa014", new EntityPropertyFa014Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA015", "Fa015", new EntityPropertyFa015Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA016", "Fa016", new EntityPropertyFa016Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA017", "Fa017", new EntityPropertyFa017Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA018", "Fa018", new EntityPropertyFa018Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA019", "Fa019", new EntityPropertyFa019Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA020", "Fa020", new EntityPropertyFa020Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA021", "Fa021", new EntityPropertyFa021Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA022", "Fa022", new EntityPropertyFa022Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA023", "Fa023", new EntityPropertyFa023Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA024", "Fa024", new EntityPropertyFa024Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA025", "Fa025", new EntityPropertyFa025Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA026", "Fa026", new EntityPropertyFa026Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA027", "Fa027", new EntityPropertyFa027Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA028", "Fa028", new EntityPropertyFa028Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA029", "Fa029", new EntityPropertyFa029Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA030", "Fa030", new EntityPropertyFa030Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA031", "Fa031", new EntityPropertyFa031Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA032", "Fa032", new EntityPropertyFa032Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA033", "Fa033", new EntityPropertyFa033Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA034", "Fa034", new EntityPropertyFa034Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA035", "Fa035", new EntityPropertyFa035Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA036", "Fa036", new EntityPropertyFa036Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA037", "Fa037", new EntityPropertyFa037Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA038", "Fa038", new EntityPropertyFa038Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA039", "Fa039", new EntityPropertyFa039Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA040", "Fa040", new EntityPropertyFa040Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA041", "Fa041", new EntityPropertyFa041Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA042", "Fa042", new EntityPropertyFa042Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA043", "Fa043", new EntityPropertyFa043Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA044", "Fa044", new EntityPropertyFa044Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA045", "Fa045", new EntityPropertyFa045Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA046", "Fa046", new EntityPropertyFa046Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA047", "Fa047", new EntityPropertyFa047Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA048", "Fa048", new EntityPropertyFa048Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA049", "Fa049", new EntityPropertyFa049Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA050", "Fa050", new EntityPropertyFa050Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA051", "Fa051", new EntityPropertyFa051Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA052", "Fa052", new EntityPropertyFa052Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA053", "Fa053", new EntityPropertyFa053Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA054", "Fa054", new EntityPropertyFa054Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA055", "Fa055", new EntityPropertyFa055Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA056", "Fa056", new EntityPropertyFa056Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA057", "Fa057", new EntityPropertyFa057Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA058", "Fa058", new EntityPropertyFa058Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA059", "Fa059", new EntityPropertyFa059Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA060", "Fa060", new EntityPropertyFa060Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA061", "Fa061", new EntityPropertyFa061Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA062", "Fa062", new EntityPropertyFa062Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA063", "Fa063", new EntityPropertyFa063Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA064", "Fa064", new EntityPropertyFa064Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA065", "Fa065", new EntityPropertyFa065Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA066", "Fa066", new EntityPropertyFa066Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA067", "Fa067", new EntityPropertyFa067Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA068", "Fa068", new EntityPropertyFa068Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA069", "Fa069", new EntityPropertyFa069Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA070", "Fa070", new EntityPropertyFa070Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA071", "Fa071", new EntityPropertyFa071Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA072", "Fa072", new EntityPropertyFa072Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA073", "Fa073", new EntityPropertyFa073Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA074", "Fa074", new EntityPropertyFa074Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA075", "Fa075", new EntityPropertyFa075Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA076", "Fa076", new EntityPropertyFa076Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA077", "Fa077", new EntityPropertyFa077Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA078", "Fa078", new EntityPropertyFa078Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA079", "Fa079", new EntityPropertyFa079Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA080", "Fa080", new EntityPropertyFa080Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA081", "Fa081", new EntityPropertyFa081Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA082", "Fa082", new EntityPropertyFa082Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA083", "Fa083", new EntityPropertyFa083Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA084", "Fa084", new EntityPropertyFa084Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA085", "Fa085", new EntityPropertyFa085Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA086", "Fa086", new EntityPropertyFa086Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA087", "Fa087", new EntityPropertyFa087Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA088", "Fa088", new EntityPropertyFa088Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA089", "Fa089", new EntityPropertyFa089Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA090", "Fa090", new EntityPropertyFa090Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA091", "Fa091", new EntityPropertyFa091Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA092", "Fa092", new EntityPropertyFa092Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA093", "Fa093", new EntityPropertyFa093Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA094", "Fa094", new EntityPropertyFa094Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA095", "Fa095", new EntityPropertyFa095Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA096", "Fa096", new EntityPropertyFa096Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA097", "Fa097", new EntityPropertyFa097Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA098", "Fa098", new EntityPropertyFa098Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA099", "Fa099", new EntityPropertyFa099Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA100", "Fa100", new EntityPropertyFa100Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA101", "Fa101", new EntityPropertyFa101Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA102", "Fa102", new EntityPropertyFa102Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA103", "Fa103", new EntityPropertyFa103Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA104", "Fa104", new EntityPropertyFa104Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA105", "Fa105", new EntityPropertyFa105Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA106", "Fa106", new EntityPropertyFa106Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA107", "Fa107", new EntityPropertyFa107Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA108", "Fa108", new EntityPropertyFa108Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA109", "Fa109", new EntityPropertyFa109Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA110", "Fa110", new EntityPropertyFa110Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA111", "Fa111", new EntityPropertyFa111Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA112", "Fa112", new EntityPropertyFa112Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA113", "Fa113", new EntityPropertyFa113Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA114", "Fa114", new EntityPropertyFa114Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA115", "Fa115", new EntityPropertyFa115Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA116", "Fa116", new EntityPropertyFa116Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA117", "Fa117", new EntityPropertyFa117Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA118", "Fa118", new EntityPropertyFa118Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA119", "Fa119", new EntityPropertyFa119Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA120", "Fa120", new EntityPropertyFa120Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA121", "Fa121", new EntityPropertyFa121Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA122", "Fa122", new EntityPropertyFa122Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA123", "Fa123", new EntityPropertyFa123Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA124", "Fa124", new EntityPropertyFa124Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA125", "Fa125", new EntityPropertyFa125Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA126", "Fa126", new EntityPropertyFa126Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA127", "Fa127", new EntityPropertyFa127Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA128", "Fa128", new EntityPropertyFa128Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA129", "Fa129", new EntityPropertyFa129Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA130", "Fa130", new EntityPropertyFa130Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA131", "Fa131", new EntityPropertyFa131Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA132", "Fa132", new EntityPropertyFa132Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA133", "Fa133", new EntityPropertyFa133Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA134", "Fa134", new EntityPropertyFa134Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA135", "Fa135", new EntityPropertyFa135Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA136", "Fa136", new EntityPropertyFa136Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA137", "Fa137", new EntityPropertyFa137Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA138", "Fa138", new EntityPropertyFa138Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA139", "Fa139", new EntityPropertyFa139Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA140", "Fa140", new EntityPropertyFa140Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA141", "Fa141", new EntityPropertyFa141Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA142", "Fa142", new EntityPropertyFa142Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA143", "Fa143", new EntityPropertyFa143Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA144", "Fa144", new EntityPropertyFa144Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA145", "Fa145", new EntityPropertyFa145Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA146", "Fa146", new EntityPropertyFa146Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA147", "Fa147", new EntityPropertyFa147Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA148", "Fa148", new EntityPropertyFa148Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA149", "Fa149", new EntityPropertyFa149Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA150", "Fa150", new EntityPropertyFa150Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA151", "Fa151", new EntityPropertyFa151Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA152", "Fa152", new EntityPropertyFa152Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA153", "Fa153", new EntityPropertyFa153Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA154", "Fa154", new EntityPropertyFa154Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA155", "Fa155", new EntityPropertyFa155Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA156", "Fa156", new EntityPropertyFa156Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA157", "Fa157", new EntityPropertyFa157Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA158", "Fa158", new EntityPropertyFa158Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA159", "Fa159", new EntityPropertyFa159Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA160", "Fa160", new EntityPropertyFa160Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA161", "Fa161", new EntityPropertyFa161Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA162", "Fa162", new EntityPropertyFa162Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA163", "Fa163", new EntityPropertyFa163Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA164", "Fa164", new EntityPropertyFa164Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA165", "Fa165", new EntityPropertyFa165Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA166", "Fa166", new EntityPropertyFa166Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA167", "Fa167", new EntityPropertyFa167Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA168", "Fa168", new EntityPropertyFa168Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA169", "Fa169", new EntityPropertyFa169Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA170", "Fa170", new EntityPropertyFa170Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA171", "Fa171", new EntityPropertyFa171Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA172", "Fa172", new EntityPropertyFa172Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA173", "Fa173", new EntityPropertyFa173Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA174", "Fa174", new EntityPropertyFa174Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA175", "Fa175", new EntityPropertyFa175Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA176", "Fa176", new EntityPropertyFa176Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA177", "Fa177", new EntityPropertyFa177Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA178", "Fa178", new EntityPropertyFa178Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA179", "Fa179", new EntityPropertyFa179Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA180", "Fa180", new EntityPropertyFa180Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA181", "Fa181", new EntityPropertyFa181Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA182", "Fa182", new EntityPropertyFa182Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA183", "Fa183", new EntityPropertyFa183Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA184", "Fa184", new EntityPropertyFa184Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA185", "Fa185", new EntityPropertyFa185Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA186", "Fa186", new EntityPropertyFa186Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA187", "Fa187", new EntityPropertyFa187Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA188", "Fa188", new EntityPropertyFa188Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA189", "Fa189", new EntityPropertyFa189Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA190", "Fa190", new EntityPropertyFa190Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA191", "Fa191", new EntityPropertyFa191Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA192", "Fa192", new EntityPropertyFa192Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA193", "Fa193", new EntityPropertyFa193Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA194", "Fa194", new EntityPropertyFa194Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA195", "Fa195", new EntityPropertyFa195Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA196", "Fa196", new EntityPropertyFa196Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA197", "Fa197", new EntityPropertyFa197Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA198", "Fa198", new EntityPropertyFa198Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA199", "Fa199", new EntityPropertyFa199Setupper(), _entityPropertySetupperMap);
            RegisterEntityPropertySetupper("FA200", "Fa200", new EntityPropertyFa200Setupper(), _entityPropertySetupperMap);
        }

        public override bool HasEntityPropertySetupper(String propertyName) {
            return _entityPropertySetupperMap.containsKey(propertyName);
        }

        public override void SetupEntityProperty(String propertyName, Object entity, Object value) {
            EntityPropertySetupper<TFaData> callback = _entityPropertySetupperMap.get(propertyName);
            callback.Setup((TFaData)entity, value);
        }

        public class EntityPropertySampleIdSetupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.SampleId = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa001Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa001 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa002Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa002 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa003Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa003 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa004Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa004 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa005Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa005 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa006Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa006 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa007Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa007 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa008Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa008 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa009Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa009 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa010Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa010 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa011Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa011 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa012Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa012 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa013Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa013 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa014Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa014 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa015Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa015 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa016Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa016 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa017Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa017 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa018Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa018 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa019Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa019 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa020Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa020 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa021Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa021 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa022Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa022 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa023Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa023 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa024Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa024 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa025Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa025 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa026Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa026 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa027Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa027 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa028Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa028 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa029Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa029 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa030Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa030 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa031Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa031 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa032Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa032 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa033Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa033 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa034Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa034 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa035Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa035 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa036Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa036 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa037Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa037 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa038Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa038 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa039Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa039 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa040Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa040 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa041Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa041 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa042Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa042 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa043Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa043 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa044Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa044 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa045Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa045 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa046Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa046 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa047Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa047 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa048Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa048 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa049Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa049 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa050Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa050 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa051Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa051 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa052Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa052 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa053Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa053 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa054Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa054 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa055Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa055 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa056Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa056 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa057Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa057 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa058Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa058 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa059Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa059 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa060Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa060 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa061Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa061 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa062Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa062 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa063Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa063 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa064Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa064 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa065Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa065 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa066Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa066 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa067Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa067 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa068Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa068 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa069Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa069 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa070Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa070 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa071Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa071 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa072Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa072 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa073Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa073 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa074Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa074 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa075Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa075 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa076Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa076 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa077Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa077 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa078Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa078 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa079Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa079 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa080Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa080 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa081Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa081 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa082Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa082 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa083Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa083 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa084Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa084 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa085Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa085 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa086Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa086 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa087Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa087 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa088Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa088 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa089Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa089 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa090Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa090 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa091Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa091 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa092Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa092 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa093Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa093 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa094Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa094 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa095Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa095 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa096Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa096 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa097Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa097 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa098Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa098 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa099Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa099 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa100Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa100 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa101Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa101 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa102Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa102 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa103Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa103 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa104Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa104 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa105Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa105 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa106Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa106 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa107Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa107 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa108Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa108 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa109Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa109 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa110Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa110 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa111Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa111 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa112Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa112 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa113Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa113 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa114Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa114 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa115Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa115 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa116Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa116 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa117Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa117 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa118Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa118 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa119Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa119 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa120Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa120 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa121Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa121 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa122Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa122 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa123Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa123 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa124Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa124 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa125Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa125 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa126Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa126 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa127Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa127 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa128Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa128 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa129Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa129 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa130Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa130 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa131Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa131 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa132Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa132 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa133Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa133 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa134Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa134 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa135Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa135 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa136Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa136 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa137Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa137 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa138Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa138 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa139Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa139 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa140Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa140 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa141Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa141 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa142Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa142 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa143Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa143 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa144Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa144 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa145Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa145 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa146Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa146 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa147Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa147 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa148Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa148 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa149Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa149 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa150Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa150 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa151Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa151 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa152Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa152 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa153Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa153 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa154Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa154 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa155Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa155 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa156Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa156 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa157Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa157 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa158Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa158 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa159Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa159 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa160Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa160 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa161Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa161 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa162Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa162 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa163Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa163 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa164Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa164 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa165Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa165 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa166Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa166 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa167Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa167 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa168Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa168 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa169Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa169 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa170Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa170 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa171Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa171 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa172Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa172 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa173Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa173 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa174Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa174 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa175Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa175 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa176Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa176 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa177Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa177 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa178Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa178 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa179Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa179 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa180Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa180 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa181Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa181 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa182Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa182 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa183Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa183 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa184Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa184 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa185Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa185 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa186Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa186 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa187Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa187 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa188Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa188 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa189Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa189 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa190Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa190 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa191Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa191 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa192Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa192 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa193Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa193 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa194Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa194 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa195Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa195 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa196Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa196 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa197Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa197 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa198Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa198 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa199Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa199 = (value != null) ? (String)value : null; }
        }
        public class EntityPropertyFa200Setupper : EntityPropertySetupper<TFaData> {
            public void Setup(TFaData entity, Object value) { entity.Fa200 = (value != null) ? (String)value : null; }
        }
    }
}
