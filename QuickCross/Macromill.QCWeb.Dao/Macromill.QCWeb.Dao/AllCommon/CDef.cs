
using System;

using Macromill.QCWeb.Dao.AllCommon.JavaLike;

namespace Macromill.QCWeb.Dao.AllCommon {

    public static class CDef {

        /**
         * フラグを示す
         */
        public class Flag {
            /** はい: 有効を示す */
            public static readonly Flag True = new Flag("1", "True", "はい");
            /** いいえ: 無効を示す */
            public static readonly Flag False = new Flag("0", "False", "いいえ");
            private static readonly Map<String, Flag> _codeValueMap = new LinkedHashMap<String, Flag>();
            static Flag() {
                _codeValueMap.put(True.Code.ToLower(), True);
                _codeValueMap.put(False.Code.ToLower(), False);
            }
            protected String _code; protected String _name; protected String _alias;
            public Flag(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static Flag CodeOf(Object code) {
                if (code == null) { return null; } if (code is Flag) { return (Flag)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static Flag[] Values { get {
                Flag[] values = new Flag[_codeValueMap.size()];
                int index = 0;
                foreach (Flag flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is Flag)) { return false; }
                Flag cls = (Flag)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 削除フラグを示す
         */
        public class DeleteFlag {
            /** はい: 削除を示す */
            public static readonly DeleteFlag True = new DeleteFlag("1", "True", "はい");
            /** いいえ: 未削除を示す */
            public static readonly DeleteFlag False = new DeleteFlag("0", "False", "いいえ");
            private static readonly Map<String, DeleteFlag> _codeValueMap = new LinkedHashMap<String, DeleteFlag>();
            static DeleteFlag() {
                _codeValueMap.put(True.Code.ToLower(), True);
                _codeValueMap.put(False.Code.ToLower(), False);
            }
            protected String _code; protected String _name; protected String _alias;
            public DeleteFlag(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static DeleteFlag CodeOf(Object code) {
                if (code == null) { return null; } if (code is DeleteFlag) { return (DeleteFlag)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static DeleteFlag[] Values { get {
                DeleteFlag[] values = new DeleteFlag[_codeValueMap.size()];
                int index = 0;
                foreach (DeleteFlag flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is DeleteFlag)) { return false; }
                DeleteFlag cls = (DeleteFlag)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * Originalデータ区分
         */
        public class SourceDiv {
            /** Original: QC3から取り込まれたオリジナルデータを示す */
            public static readonly SourceDiv Original = new SourceDiv("0", "Original", "Original");
            /** 加工データ: データ加工で作成されたデータを示す */
            public static readonly SourceDiv DataEdit = new SourceDiv("1", "DataEdit", "加工データ");
            /** シナリオ加工データ: シナリオ内の操作で作成されたデータを示す */
            public static readonly SourceDiv ScenarioDataEdit = new SourceDiv("2", "ScenarioDataEdit", "シナリオ加工データ");
            private static readonly Map<String, SourceDiv> _codeValueMap = new LinkedHashMap<String, SourceDiv>();
            static SourceDiv() {
                _codeValueMap.put(Original.Code.ToLower(), Original);
                _codeValueMap.put(DataEdit.Code.ToLower(), DataEdit);
                _codeValueMap.put(ScenarioDataEdit.Code.ToLower(), ScenarioDataEdit);
            }
            protected String _code; protected String _name; protected String _alias;
            public SourceDiv(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static SourceDiv CodeOf(Object code) {
                if (code == null) { return null; } if (code is SourceDiv) { return (SourceDiv)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static SourceDiv[] Values { get {
                SourceDiv[] values = new SourceDiv[_codeValueMap.size()];
                int index = 0;
                foreach (SourceDiv flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is SourceDiv)) { return false; }
                SourceDiv cls = (SourceDiv)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 回答タイプ
         */
        public class AnswerType {
            /** SA: SAを示す */
            public static readonly AnswerType SA = new AnswerType("1", "SA", "SA");
            /** MA: MAを示す */
            public static readonly AnswerType MA = new AnswerType("2", "MA", "MA");
            /** N: Nを示す */
            public static readonly AnswerType N = new AnswerType("3", "N", "N");
            /** FA: FAを示す */
            public static readonly AnswerType FA = new AnswerType("4", "FA", "FA");
            /** D: Dを示す */
            public static readonly AnswerType D = new AnswerType("5", "D", "D");
            private static readonly Map<String, AnswerType> _codeValueMap = new LinkedHashMap<String, AnswerType>();
            static AnswerType() {
                _codeValueMap.put(SA.Code.ToLower(), SA);
                _codeValueMap.put(MA.Code.ToLower(), MA);
                _codeValueMap.put(N.Code.ToLower(), N);
                _codeValueMap.put(FA.Code.ToLower(), FA);
                _codeValueMap.put(D.Code.ToLower(), D);
            }
            protected String _code; protected String _name; protected String _alias;
            public AnswerType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static AnswerType CodeOf(Object code) {
                if (code == null) { return null; } if (code is AnswerType) { return (AnswerType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static AnswerType[] Values { get {
                AnswerType[] values = new AnswerType[_codeValueMap.size()];
                int index = 0;
                foreach (AnswerType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is AnswerType)) { return false; }
                AnswerType cls = (AnswerType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * マトリックス区分
         */
        public class MatrixType {
            /** 通常アイテム: 通常アイテムを示す */
            public static readonly MatrixType NormalItem = new MatrixType("0", "NormalItem", "通常アイテム");
            /** 親アイテム: 親アイテムを示す */
            public static readonly MatrixType MatrixParent = new MatrixType("1", "MatrixParent", "親アイテム");
            /** 子マトリックス（親作成元アイテム）: 子マトリックス（親作成元アイテム）を示す */
            public static readonly MatrixType FirstChild = new MatrixType("4", "FirstChild", "子マトリックス（親作成元アイテム）");
            /** 子マトリックス（通常子アイテム）: 子マトリックス（通常子アイテム）を示す */
            public static readonly MatrixType MatrixChild = new MatrixType("2", "MatrixChild", "子マトリックス（通常子アイテム）");
            /** 子マトリックス（付加FA）: 子マトリックス（付加FA）を示す */
            public static readonly MatrixType SubFA = new MatrixType("3", "SubFA", "子マトリックス（付加FA）");
            private static readonly Map<String, MatrixType> _codeValueMap = new LinkedHashMap<String, MatrixType>();
            static MatrixType() {
                _codeValueMap.put(NormalItem.Code.ToLower(), NormalItem);
                _codeValueMap.put(MatrixParent.Code.ToLower(), MatrixParent);
                _codeValueMap.put(FirstChild.Code.ToLower(), FirstChild);
                _codeValueMap.put(MatrixChild.Code.ToLower(), MatrixChild);
                _codeValueMap.put(SubFA.Code.ToLower(), SubFA);
            }
            protected String _code; protected String _name; protected String _alias;
            public MatrixType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static MatrixType CodeOf(Object code) {
                if (code == null) { return null; } if (code is MatrixType) { return (MatrixType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static MatrixType[] Values { get {
                MatrixType[] values = new MatrixType[_codeValueMap.size()];
                int index = 0;
                foreach (MatrixType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is MatrixType)) { return false; }
                MatrixType cls = (MatrixType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 絞り込み条件連結区分
         */
        public class ConditionDiv {
            /** &: ANDを示す */
            public static readonly ConditionDiv AND = new ConditionDiv("1", "AND", "&");
            /** |: ORを示す */
            public static readonly ConditionDiv OR = new ConditionDiv("2", "OR", "|");
            private static readonly Map<String, ConditionDiv> _codeValueMap = new LinkedHashMap<String, ConditionDiv>();
            static ConditionDiv() {
                _codeValueMap.put(AND.Code.ToLower(), AND);
                _codeValueMap.put(OR.Code.ToLower(), OR);
            }
            protected String _code; protected String _name; protected String _alias;
            public ConditionDiv(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ConditionDiv CodeOf(Object code) {
                if (code == null) { return null; } if (code is ConditionDiv) { return (ConditionDiv)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ConditionDiv[] Values { get {
                ConditionDiv[] values = new ConditionDiv[_codeValueMap.size()];
                int index = 0;
                foreach (ConditionDiv flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ConditionDiv)) { return false; }
                ConditionDiv cls = (ConditionDiv)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 演算子
         */
        public class OperationCode {
            /** =: Equal(=)を示す */
            public static readonly OperationCode Equal = new OperationCode("1", "Equal", "=");
            /** <>: NotEqual(<>)を示す */
            public static readonly OperationCode NotEqual = new OperationCode("2", "NotEqual", "<>");
            /** <: LessThan(<)を示す */
            public static readonly OperationCode LessThan = new OperationCode("3", "LessThan", "<");
            /** >: GreaterThan(>)を示す */
            public static readonly OperationCode GreaterThan = new OperationCode("4", "GreaterThan", ">");
            /** <=: LessEqual(<=)を示す */
            public static readonly OperationCode LessEqual = new OperationCode("5", "LessEqual", "<=");
            /** >=: GreaterEqual(>=)を示す */
            public static readonly OperationCode GreaterEqual = new OperationCode("6", "GreaterEqual", ">=");
            private static readonly Map<String, OperationCode> _codeValueMap = new LinkedHashMap<String, OperationCode>();
            static OperationCode() {
                _codeValueMap.put(Equal.Code.ToLower(), Equal);
                _codeValueMap.put(NotEqual.Code.ToLower(), NotEqual);
                _codeValueMap.put(LessThan.Code.ToLower(), LessThan);
                _codeValueMap.put(GreaterThan.Code.ToLower(), GreaterThan);
                _codeValueMap.put(LessEqual.Code.ToLower(), LessEqual);
                _codeValueMap.put(GreaterEqual.Code.ToLower(), GreaterEqual);
            }
            protected String _code; protected String _name; protected String _alias;
            public OperationCode(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static OperationCode CodeOf(Object code) {
                if (code == null) { return null; } if (code is OperationCode) { return (OperationCode)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static OperationCode[] Values { get {
                OperationCode[] values = new OperationCode[_codeValueMap.size()];
                int index = 0;
                foreach (OperationCode flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is OperationCode)) { return false; }
                OperationCode cls = (OperationCode)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 設問タイプ
         */
        public class ItemType {
            /** SAR: SARを示す */
            public static readonly ItemType SAR = new ItemType("SAR", "SAR", "SAR");
            /** SAS: SASを示す */
            public static readonly ItemType SAS = new ItemType("SAS", "SAS", "SAS");
            /** SAP: SAPを示す */
            public static readonly ItemType SAP = new ItemType("SAP", "SAP", "SAP");
            /** MAC: MACを示す */
            public static readonly ItemType MAC = new ItemType("MAC", "MAC", "MAC");
            /** MTS: MTSを示す */
            public static readonly ItemType MTS = new ItemType("MTS", "MTS", "MTS");
            /** MTM: MTSを示す */
            public static readonly ItemType MTM = new ItemType("MTM", "MTM", "MTM");
            /** MTT: MTTを示す */
            public static readonly ItemType MTT = new ItemType("MTT", "MTT", "MTT");
            /** RNK: RNKを示す */
            public static readonly ItemType RNK = new ItemType("RNK", "RNK", "RNK");
            /** RAT: RATを示す */
            public static readonly ItemType RAT = new ItemType("RAT", "RAT", "RAT");
            /** FAS: FASを示す */
            public static readonly ItemType FAS = new ItemType("FAS", "FAS", "FAS");
            /** FAL: FALを示す */
            public static readonly ItemType FAL = new ItemType("FAL", "FAL", "FAL");
            private static readonly Map<String, ItemType> _codeValueMap = new LinkedHashMap<String, ItemType>();
            static ItemType() {
                _codeValueMap.put(SAR.Code.ToLower(), SAR);
                _codeValueMap.put(SAS.Code.ToLower(), SAS);
                _codeValueMap.put(SAP.Code.ToLower(), SAP);
                _codeValueMap.put(MAC.Code.ToLower(), MAC);
                _codeValueMap.put(MTS.Code.ToLower(), MTS);
                _codeValueMap.put(MTM.Code.ToLower(), MTM);
                _codeValueMap.put(MTT.Code.ToLower(), MTT);
                _codeValueMap.put(RNK.Code.ToLower(), RNK);
                _codeValueMap.put(RAT.Code.ToLower(), RAT);
                _codeValueMap.put(FAS.Code.ToLower(), FAS);
                _codeValueMap.put(FAL.Code.ToLower(), FAL);
            }
            protected String _code; protected String _name; protected String _alias;
            public ItemType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ItemType CodeOf(Object code) {
                if (code == null) { return null; } if (code is ItemType) { return (ItemType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ItemType[] Values { get {
                ItemType[] values = new ItemType[_codeValueMap.size()];
                int index = 0;
                foreach (ItemType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ItemType)) { return false; }
                ItemType cls = (ItemType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * シナリオ区分
         */
        public class ScenarioType {
            /** GT: GTシナリオを示す */
            public static readonly ScenarioType GT = new ScenarioType("G", "GT", "GT");
            /** CROSS: クロスシナリオを示す */
            public static readonly ScenarioType CROSS = new ScenarioType("C", "CROSS", "CROSS");
            /** FA: FAシナリオを示す */
            public static readonly ScenarioType FA = new ScenarioType("F", "FA", "FA");
            private static readonly Map<String, ScenarioType> _codeValueMap = new LinkedHashMap<String, ScenarioType>();
            static ScenarioType() {
                _codeValueMap.put(GT.Code.ToLower(), GT);
                _codeValueMap.put(CROSS.Code.ToLower(), CROSS);
                _codeValueMap.put(FA.Code.ToLower(), FA);
            }
            protected String _code; protected String _name; protected String _alias;
            public ScenarioType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ScenarioType CodeOf(Object code) {
                if (code == null) { return null; } if (code is ScenarioType) { return (ScenarioType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ScenarioType[] Values { get {
                ScenarioType[] values = new ScenarioType[_codeValueMap.size()];
                int index = 0;
                foreach (ScenarioType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ScenarioType)) { return false; }
                ScenarioType cls = (ScenarioType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * PowerPoint出力形式
         */
        public class PowerPointType {
            /** 2003形式: 2003形式を示す */
            public static readonly PowerPointType PP2003 = new PowerPointType("1", "PP2003", "2003形式");
            /** 2007形式: 2007形式を示す */
            public static readonly PowerPointType PP2007 = new PowerPointType("24", "PP2007", "2007形式");
            private static readonly Map<String, PowerPointType> _codeValueMap = new LinkedHashMap<String, PowerPointType>();
            static PowerPointType() {
                _codeValueMap.put(PP2003.Code.ToLower(), PP2003);
                _codeValueMap.put(PP2007.Code.ToLower(), PP2007);
            }
            protected String _code; protected String _name; protected String _alias;
            public PowerPointType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static PowerPointType CodeOf(Object code) {
                if (code == null) { return null; } if (code is PowerPointType) { return (PowerPointType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static PowerPointType[] Values { get {
                PowerPointType[] values = new PowerPointType[_codeValueMap.size()];
                int index = 0;
                foreach (PowerPointType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is PowerPointType)) { return false; }
                PowerPointType cls = (PowerPointType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * Excelブック形式
         */
        public class ExcelbookType {
            /** 2003形式: 2003形式を示す */
            public static readonly ExcelbookType EXL2003 = new ExcelbookType("56", "EXL2003", "2003形式");
            /** 2007形式: 2007形式を示す */
            public static readonly ExcelbookType EXL2007 = new ExcelbookType("51", "EXL2007", "2007形式");
            private static readonly Map<String, ExcelbookType> _codeValueMap = new LinkedHashMap<String, ExcelbookType>();
            static ExcelbookType() {
                _codeValueMap.put(EXL2003.Code.ToLower(), EXL2003);
                _codeValueMap.put(EXL2007.Code.ToLower(), EXL2007);
            }
            protected String _code; protected String _name; protected String _alias;
            public ExcelbookType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ExcelbookType CodeOf(Object code) {
                if (code == null) { return null; } if (code is ExcelbookType) { return (ExcelbookType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ExcelbookType[] Values { get {
                ExcelbookType[] values = new ExcelbookType[_codeValueMap.size()];
                int index = 0;
                foreach (ExcelbookType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ExcelbookType)) { return false; }
                ExcelbookType cls = (ExcelbookType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 無回答表示コード
         */
        public class NoAnswerVisibleCode {
            /** 集計対象: 集計対象を示す */
            public static readonly NoAnswerVisibleCode SC = new NoAnswerVisibleCode("1", "SC", "集計対象");
            /** 集計軸: 集計軸を示す */
            public static readonly NoAnswerVisibleCode AXIS = new NoAnswerVisibleCode("2", "AXIS", "集計軸");
            private static readonly Map<String, NoAnswerVisibleCode> _codeValueMap = new LinkedHashMap<String, NoAnswerVisibleCode>();
            static NoAnswerVisibleCode() {
                _codeValueMap.put(SC.Code.ToLower(), SC);
                _codeValueMap.put(AXIS.Code.ToLower(), AXIS);
            }
            protected String _code; protected String _name; protected String _alias;
            public NoAnswerVisibleCode(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static NoAnswerVisibleCode CodeOf(Object code) {
                if (code == null) { return null; } if (code is NoAnswerVisibleCode) { return (NoAnswerVisibleCode)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static NoAnswerVisibleCode[] Values { get {
                NoAnswerVisibleCode[] values = new NoAnswerVisibleCode[_codeValueMap.size()];
                int index = 0;
                foreach (NoAnswerVisibleCode flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is NoAnswerVisibleCode)) { return false; }
                NoAnswerVisibleCode cls = (NoAnswerVisibleCode)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 非該当表示コード
         */
        public class UnmacthVisibleCode {
            /** 集計対象: 集計対象を示す */
            public static readonly UnmacthVisibleCode SC = new UnmacthVisibleCode("1", "SC", "集計対象");
            /** 集計軸: 集計軸を示す */
            public static readonly UnmacthVisibleCode AXIS = new UnmacthVisibleCode("2", "AXIS", "集計軸");
            private static readonly Map<String, UnmacthVisibleCode> _codeValueMap = new LinkedHashMap<String, UnmacthVisibleCode>();
            static UnmacthVisibleCode() {
                _codeValueMap.put(SC.Code.ToLower(), SC);
                _codeValueMap.put(AXIS.Code.ToLower(), AXIS);
            }
            protected String _code; protected String _name; protected String _alias;
            public UnmacthVisibleCode(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static UnmacthVisibleCode CodeOf(Object code) {
                if (code == null) { return null; } if (code is UnmacthVisibleCode) { return (UnmacthVisibleCode)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static UnmacthVisibleCode[] Values { get {
                UnmacthVisibleCode[] values = new UnmacthVisibleCode[_codeValueMap.size()];
                int index = 0;
                foreach (UnmacthVisibleCode flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is UnmacthVisibleCode)) { return false; }
                UnmacthVisibleCode cls = (UnmacthVisibleCode)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 処理重度
         */
        public class ProcWeightProcWeight {
            /** チェックリスト: チェックリストを示す */
            public static readonly ProcWeightProcWeight CHK_LIST = new ProcWeightProcWeight("0", "CHK_LIST", "チェックリスト");
            /** 極小: 極小を示す */
            public static readonly ProcWeightProcWeight SUPER_MINI = new ProcWeightProcWeight("1", "SUPER_MINI", "極小");
            /** 小: 小を示す */
            public static readonly ProcWeightProcWeight MIN = new ProcWeightProcWeight("2", "MIN", "小");
            /** 中: 中を示す */
            public static readonly ProcWeightProcWeight MIDDLE = new ProcWeightProcWeight("3", "MIDDLE", "中");
            /** 大: 大を示す */
            public static readonly ProcWeightProcWeight LARGE = new ProcWeightProcWeight("4", "LARGE", "大");
            /** 極大: 極大を示す */
            public static readonly ProcWeightProcWeight SUPER_LARGE = new ProcWeightProcWeight("5", "SUPER_LARGE", "極大");
            /** テンプレート: テンプレートを示す */
            public static readonly ProcWeightProcWeight TEMPLATE = new ProcWeightProcWeight("10", "TEMPLATE", "テンプレート");
            private static readonly Map<String, ProcWeightProcWeight> _codeValueMap = new LinkedHashMap<String, ProcWeightProcWeight>();
            static ProcWeightProcWeight() {
                _codeValueMap.put(CHK_LIST.Code.ToLower(), CHK_LIST);
                _codeValueMap.put(SUPER_MINI.Code.ToLower(), SUPER_MINI);
                _codeValueMap.put(MIN.Code.ToLower(), MIN);
                _codeValueMap.put(MIDDLE.Code.ToLower(), MIDDLE);
                _codeValueMap.put(LARGE.Code.ToLower(), LARGE);
                _codeValueMap.put(SUPER_LARGE.Code.ToLower(), SUPER_LARGE);
                _codeValueMap.put(TEMPLATE.Code.ToLower(), TEMPLATE);
            }
            protected String _code; protected String _name; protected String _alias;
            public ProcWeightProcWeight(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ProcWeightProcWeight CodeOf(Object code) {
                if (code == null) { return null; } if (code is ProcWeightProcWeight) { return (ProcWeightProcWeight)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ProcWeightProcWeight[] Values { get {
                ProcWeightProcWeight[] values = new ProcWeightProcWeight[_codeValueMap.size()];
                int index = 0;
                foreach (ProcWeightProcWeight flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ProcWeightProcWeight)) { return false; }
                ProcWeightProcWeight cls = (ProcWeightProcWeight)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 集計データ区分
         */
        public class SurveyDataType {
            /** 標準納品ファイル: 標準納品ファイルを示す */
            public static readonly SurveyDataType NORMAL = new SurveyDataType("0", "NORMAL", "標準納品ファイル");
            /** 追加納品ファイル: 追加納品ファイルを示す */
            public static readonly SurveyDataType ADD = new SurveyDataType("1", "ADD", "追加納品ファイル");
            private static readonly Map<String, SurveyDataType> _codeValueMap = new LinkedHashMap<String, SurveyDataType>();
            static SurveyDataType() {
                _codeValueMap.put(NORMAL.Code.ToLower(), NORMAL);
                _codeValueMap.put(ADD.Code.ToLower(), ADD);
            }
            protected String _code; protected String _name; protected String _alias;
            public SurveyDataType(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static SurveyDataType CodeOf(Object code) {
                if (code == null) { return null; } if (code is SurveyDataType) { return (SurveyDataType)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static SurveyDataType[] Values { get {
                SurveyDataType[] values = new SurveyDataType[_codeValueMap.size()];
                int index = 0;
                foreach (SurveyDataType flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is SurveyDataType)) { return false; }
                SurveyDataType cls = (SurveyDataType)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 取込ステータス
         */
        public class ImportStatus {
            /** 未取込: 未取込を示す */
            public static readonly ImportStatus NONE_IMPORT = new ImportStatus("0", "NONE_IMPORT", "未取込");
            /** 取込中: 取込中を示す */
            public static readonly ImportStatus IMPORT_EXEC = new ImportStatus("1", "IMPORT_EXEC", "取込中");
            /** 取込完(パスワード付きZIP): 取込完(パスワード付きZIP)を示す */
            public static readonly ImportStatus IMPORT_END_ZIP = new ImportStatus("2", "IMPORT_END_ZIP", "取込完(パスワード付きZIP)");
            /** 取込完: 取込完を示す */
            public static readonly ImportStatus IMPORT_END = new ImportStatus("3", "IMPORT_END", "取込完");
            /** エラー: エラーありを示す */
            public static readonly ImportStatus IMPORT_ERROR = new ImportStatus("-9", "IMPORT_ERROR", "エラー");
            /** スキップ: 処理をスキップしたことを示す */
            public static readonly ImportStatus IMPORT_SKIP = new ImportStatus("-1", "IMPORT_SKIP", "スキップ");
            /** 取込完(一部エラーあり): 取込完(一部エラーあり)を示す */
            public static readonly ImportStatus IMPORT_END_PART_ERROR = new ImportStatus("4", "IMPORT_END_PART_ERROR", "取込完(一部エラーあり)");
            private static readonly Map<String, ImportStatus> _codeValueMap = new LinkedHashMap<String, ImportStatus>();
            static ImportStatus() {
                _codeValueMap.put(NONE_IMPORT.Code.ToLower(), NONE_IMPORT);
                _codeValueMap.put(IMPORT_EXEC.Code.ToLower(), IMPORT_EXEC);
                _codeValueMap.put(IMPORT_END_ZIP.Code.ToLower(), IMPORT_END_ZIP);
                _codeValueMap.put(IMPORT_END.Code.ToLower(), IMPORT_END);
                _codeValueMap.put(IMPORT_ERROR.Code.ToLower(), IMPORT_ERROR);
                _codeValueMap.put(IMPORT_SKIP.Code.ToLower(), IMPORT_SKIP);
                _codeValueMap.put(IMPORT_END_PART_ERROR.Code.ToLower(), IMPORT_END_PART_ERROR);
            }
            protected String _code; protected String _name; protected String _alias;
            public ImportStatus(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ImportStatus CodeOf(Object code) {
                if (code == null) { return null; } if (code is ImportStatus) { return (ImportStatus)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ImportStatus[] Values { get {
                ImportStatus[] values = new ImportStatus[_codeValueMap.size()];
                int index = 0;
                foreach (ImportStatus flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ImportStatus)) { return false; }
                ImportStatus cls = (ImportStatus)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 削除ステータス
         */
        public class DeleteStatus {
            /** 未削除: 未削除を示す */
            public static readonly DeleteStatus NONE_DELETE = new DeleteStatus("0", "NONE_DELETE", "未削除");
            /** 削除中: 削除中を示す */
            public static readonly DeleteStatus DELETE_EXEC = new DeleteStatus("1", "DELETE_EXEC", "削除中");
            /** 削除完: 削除完を示す */
            public static readonly DeleteStatus DELETE_END = new DeleteStatus("2", "DELETE_END", "削除完");
            private static readonly Map<String, DeleteStatus> _codeValueMap = new LinkedHashMap<String, DeleteStatus>();
            static DeleteStatus() {
                _codeValueMap.put(NONE_DELETE.Code.ToLower(), NONE_DELETE);
                _codeValueMap.put(DELETE_EXEC.Code.ToLower(), DELETE_EXEC);
                _codeValueMap.put(DELETE_END.Code.ToLower(), DELETE_END);
            }
            protected String _code; protected String _name; protected String _alias;
            public DeleteStatus(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static DeleteStatus CodeOf(Object code) {
                if (code == null) { return null; } if (code is DeleteStatus) { return (DeleteStatus)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static DeleteStatus[] Values { get {
                DeleteStatus[] values = new DeleteStatus[_codeValueMap.size()];
                int index = 0;
                foreach (DeleteStatus flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is DeleteStatus)) { return false; }
                DeleteStatus cls = (DeleteStatus)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * アイテム情報ステータス
         */
        public class ItemStatus {
            /** 無効(論理削除): 無効を示す */
            public static readonly ItemStatus Invalid = new ItemStatus("0", "Invalid", "無効(論理削除)");
            /** 有効: 有効を示す */
            public static readonly ItemStatus Effective = new ItemStatus("1", "Effective", "有効");
            /** 仮登録: 仮登録を示す */
            public static readonly ItemStatus Temporary = new ItemStatus("2", "Temporary", "仮登録");
            private static readonly Map<String, ItemStatus> _codeValueMap = new LinkedHashMap<String, ItemStatus>();
            static ItemStatus() {
                _codeValueMap.put(Invalid.Code.ToLower(), Invalid);
                _codeValueMap.put(Effective.Code.ToLower(), Effective);
                _codeValueMap.put(Temporary.Code.ToLower(), Temporary);
            }
            protected String _code; protected String _name; protected String _alias;
            public ItemStatus(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static ItemStatus CodeOf(Object code) {
                if (code == null) { return null; } if (code is ItemStatus) { return (ItemStatus)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static ItemStatus[] Values { get {
                ItemStatus[] values = new ItemStatus[_codeValueMap.size()];
                int index = 0;
                foreach (ItemStatus flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is ItemStatus)) { return false; }
                ItemStatus cls = (ItemStatus)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 出力物重度判定の区分
         */
        public class OutputWPMasterID {
            /** GT: GTを示す */
            public static readonly OutputWPMasterID GT = new OutputWPMasterID("01", "GT", "GT");
            /** CROSS: CROSSを示す */
            public static readonly OutputWPMasterID CROSS = new OutputWPMasterID("02", "CROSS", "CROSS");
            /** FA: FAを示す */
            public static readonly OutputWPMasterID FA = new OutputWPMasterID("03", "FA", "FA");
            /** データ出力: データ出力を示す */
            public static readonly OutputWPMasterID DataOutput = new OutputWPMasterID("04", "DataOutput", "データ出力");
            private static readonly Map<String, OutputWPMasterID> _codeValueMap = new LinkedHashMap<String, OutputWPMasterID>();
            static OutputWPMasterID() {
                _codeValueMap.put(GT.Code.ToLower(), GT);
                _codeValueMap.put(CROSS.Code.ToLower(), CROSS);
                _codeValueMap.put(FA.Code.ToLower(), FA);
                _codeValueMap.put(DataOutput.Code.ToLower(), DataOutput);
            }
            protected String _code; protected String _name; protected String _alias;
            public OutputWPMasterID(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static OutputWPMasterID CodeOf(Object code) {
                if (code == null) { return null; } if (code is OutputWPMasterID) { return (OutputWPMasterID)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static OutputWPMasterID[] Values { get {
                OutputWPMasterID[] values = new OutputWPMasterID[_codeValueMap.size()];
                int index = 0;
                foreach (OutputWPMasterID flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is OutputWPMasterID)) { return false; }
                OutputWPMasterID cls = (OutputWPMasterID)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

        /**
         * 比率の差記号
         */
        public class RateSign {
            /** ±: プラス・マイナスを示す */
            public static readonly RateSign PlusAndMinus = new RateSign("1", "PlusAndMinus", "±");
            /** +: プラスを示す */
            public static readonly RateSign Plus = new RateSign("2", "Plus", "+");
            /** -: マイナスを示す */
            public static readonly RateSign Minus = new RateSign("3", "Minus", "-");
            private static readonly Map<String, RateSign> _codeValueMap = new LinkedHashMap<String, RateSign>();
            static RateSign() {
                _codeValueMap.put(PlusAndMinus.Code.ToLower(), PlusAndMinus);
                _codeValueMap.put(Plus.Code.ToLower(), Plus);
                _codeValueMap.put(Minus.Code.ToLower(), Minus);
            }
            protected String _code; protected String _name; protected String _alias;
            public RateSign(String code, String name, String alias) {
                _code = code; _name = name; _alias = alias;
            }
            public String Code { get { return _code; } }
            public String Name { get { return _name; } }
            public String Alias { get { return _alias; } }
            public static RateSign CodeOf(Object code) {
                if (code == null) { return null; } if (code is RateSign) { return (RateSign)code; }
                return _codeValueMap.get(code.ToString().ToLower());
            }
            public static RateSign[] Values { get {
                RateSign[] values = new RateSign[_codeValueMap.size()];
                int index = 0;
                foreach (RateSign flg in _codeValueMap.values()) {
                    values[index] = flg;
                    ++index;
                }
                return values;
            }}
            public override int GetHashCode() { return 7 + _code.GetHashCode(); }
            public override bool Equals(Object obj) {
                if (!(obj is RateSign)) { return false; }
                RateSign cls = (RateSign)obj;
                return _code.ToLower().Equals(cls.Code.ToLower());
            }
            public override String ToString() { return this.Code; }
        }

    }

}
