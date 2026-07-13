#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：ArrayBindInsert.cs
 * バージョン：1.0.0
 * 概　　　要：ODP.NETのArrayBindでINSERT
 * 作　成　日：2012/12/14
 * 作　成　者：中山大介
 * 更　新　日：
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Seasar.Extension.Tx;
using Seasar.Quill;
using Seasar.Quill.Database.Tx;
using Seasar.Quill.Database.Tx.Impl;
using Seasar.Quill.Util;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.Dbm;
using Macromill.QCWeb.Dao.AllCommon.Dbm.Info;
using Oracle.DataAccess.Client;

namespace Macromill.QCWeb.Dao.Dao
{
    /// <summary>
    /// 基本となるSQL文（INSERT,DELETE,UPDATE）を返す
    /// </summary>
    internal static class DefaultSql
    {
        /// <summary>
        /// INSERT文を返す
        /// </summary>
        /// <param name="columns">カラム名のリスト</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="surrogateKeyName">サロゲートキー名</param>
        /// <param name="sequenceName">シーケンス名</param>
        /// <returns>INSERTのSQL文</returns>
        public static string InsertSql(List<string> columns, string tableName, string surrogateKeyName, string sequenceName)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlValue = new StringBuilder();
            sql.Append(string.Format("INSERT INTO {0}(", tableName));
            for (int i = 0; i < columns.Count; ++i)
            {
                var columnName = columns[i];
                //surrogateKeyNameがi=0であることを大前提としている
                if (columnName == surrogateKeyName)
                {
                    sql.Append(string.Format(" {0}", columnName));
                    sqlValue.Append(string.Format("{0}.NEXTVAL", sequenceName));
                }
                else
                {
                    sql.Append(string.Format(",{0}", columnName));
                    sqlValue.Append(string.Format(",:{0}", columnName));
                }
            }
            sql.Append(") VALUES (");
            sql.Append(sqlValue.ToString());
            sql.Append(")");
            return sql.ToString();
        }
        /// <summary>
        /// DELETE文を返す
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <param name="surrogateKeyName">サロゲートキー名</param>
        /// <returns>DELETEのSQL文</returns>
        public static string DeleteSql(string tableName, string surrogateKeyName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("DELETE FROM {0} ", tableName));
            sql.Append(string.Format("WHERE {0} = :{0}", surrogateKeyName));
            return sql.ToString();
        }
        /// <summary>
        /// UPDATE文を返す
        /// </summary>
        /// <param name="updateTargetColumns">カラム名のリスト</param>
        /// <param name="tableName">テーブル名</param>
        /// <param name="surrogateKeyName">サロゲートキー名</param>
        /// <returns>UPDATEのSQL文</returns>
        public static string UpdateSql(List<string> updateTargetColumns, string tableName, string surrogateKeyName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("UPDATE {0} ", tableName));
            sql.Append(string.Format("SET "));
            for (int i = 0; i < updateTargetColumns.Count; ++i)
            {
                if (i != 0) sql.Append(",");
                sql.Append(string.Format("{0} = :{0} ", updateTargetColumns[i]));
            }
            sql.Append(string.Format("WHERE {0} = :{0} ", surrogateKeyName));
            return sql.ToString();
        }

    }

    #region 1.3.カテゴリ情報
    /// <summary>
    /// 1.3.カテゴリ情報のSQL生成に利用する文字列情報
    /// </summary>
    public static class CategoryInfoSql
    {
        #region プロパティ
        /// <summary>
        /// カテゴリ情報のテーブルの名称
        /// </summary>
        public static string TABLE_NAME = "T_CATEGORY_INFO";
        /// <summary>
        /// カテゴリ情報のシーケンスの名称
        /// </summary>
        public static string SEQUENCE = "T_CATEGORY_INFO_SEQ_01";
        /// <summary>
        /// カテゴリ情報.カテゴリIDの名称
        /// </summary>
        public static string CATEGORY_INFO_ID = "CATEGORY_INFO_ID";
        /// <summary>
        /// カテゴリ情報.アイテムIDの名称
        /// </summary>
        public static string ITEM_INFO_ID = "ITEM_INFO_ID";
        /// <summary>
        /// カテゴリ情報.カテゴリNoの名称
        /// </summary>
        public static string CATEGORY_NO = "CATEGORY_NO";
        /// <summary>
        /// カテゴリ情報.カテゴリ名の名称
        /// </summary>
        public static string CATEGORY_NAME = "CATEGORY_NAME";
        /// <summary>
        /// カテゴリ情報.ウエイト値の名称
        /// </summary>
        public static string WEIGHT_VALUE = "WEIGHT_VALUE";
        /// <summary>
        /// カテゴリ情報.カテゴリ名（原文）の名称
        /// </summary>
        public static string ORIGINAL_CATEGORY_NAME = "ORIGINAL_CATEGORY_NAME";
        #endregion

        /// <summary>
        /// カテゴリ情報の項目をList形式で返す
        /// </summary>
        /// <returns></returns>
        public static List<string> GetColumns()
        {
            List<string> columns = new List<string>();
            columns.Add(CATEGORY_INFO_ID);
            columns.Add(ITEM_INFO_ID);
            columns.Add(CATEGORY_NO);
            columns.Add(CATEGORY_NAME);
            columns.Add(WEIGHT_VALUE);
            columns.Add(ORIGINAL_CATEGORY_NAME);
            return columns;
        }
        /// <summary>
        /// カテゴリ情報の項目に対応するOracleDbTypeをDictionary形式で返す
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, OracleDbType> GetTypes()
        {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(CATEGORY_INFO_ID, OracleDbType.Decimal);
            types.Add(ITEM_INFO_ID, OracleDbType.Decimal);
            types.Add(CATEGORY_NO, OracleDbType.Int32);
            types.Add(CATEGORY_NAME, OracleDbType.NVarchar2);
            types.Add(WEIGHT_VALUE, OracleDbType.Varchar2);
            types.Add(ORIGINAL_CATEGORY_NAME, OracleDbType.NVarchar2);
            return types;
        }
        /// <summary>
        /// INSERT文を返す
        /// </summary>
        /// <returns>INSERT文の文字列</returns>
        public static string GetInsertSql()
        {
            return DefaultSql.InsertSql(GetColumns(), TABLE_NAME, CATEGORY_INFO_ID, SEQUENCE);
        }
        /// <summary>
        /// DELETE文を返す
        /// </summary>
        /// <returns>DELETE文の文字列</returns>
        public static string GetDeleteSql()
        {
            return DefaultSql.DeleteSql(TABLE_NAME, CATEGORY_INFO_ID);
        }
        /// <summary>
        /// UPDATE文を返す
        /// </summary>
        /// <param name="columns">更新対象カラムのリスト</param>
        /// <returns>UPDATE文の文字列</returns>
        public static string GetUpdateSql(List<string> columns)
        {
            return DefaultSql.UpdateSql(columns, TABLE_NAME, CATEGORY_INFO_ID);
        }
        /// <summary>
        /// 1.3.カテゴリ情報のINSERT文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="values">カラムの値のDictionaryコレクション</param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public static int ExecuteInsert(IDictionary<string, object> values, int recordCount)
        {
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert())
            {
                executeCount = logic.ExecuteNonQuery(GetInsertSql(), GetTypes(), values, recordCount);
            }
            return executeCount;
        }
        /// <summary>
        /// 1.3.カテゴリ情報のUPDATE文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="columns">カラム名のリスト</param>
        /// <param name="values">カラムの値のDictionaryコレクション</param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public static int ExecuteUpdate(List<string> columns, Dictionary<string, object> values, int recordCount)
        {
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert())
            {
                executeCount = logic.ExecuteNonQuery(GetUpdateSql(columns), GetTypes(), values, recordCount);
            }
            return executeCount;
        }
        /// <summary>
        /// 1.3.カテゴリ情報のUPDATE文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="values"></param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public static int ExecuteDelete(Dictionary<string, object> values, int recordCount)
        {
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert())
            {
                executeCount = logic.ExecuteNonQuery(GetDeleteSql(), GetTypes(), values, recordCount);
            }
            return executeCount;
        }
    }
    #endregion

    #region 2.1.GTシナリオアイテム
    /// <summary>
    /// 2.1.GTシナリオアイテムのSQL生成に利用する文字列情報
    /// </summary>
    public static class GtScenarioItemSql
    {
        #region プロパティ
        /// <summary>
        /// テーブルの名称
        /// </summary>
        public static string TABLE_NAME = "T_GT_SCENARIO_ITEM";
        /// <summary>
        /// シーケンスの名称
        /// </summary>
        public static string SEQUENCE = "T_GT_SCENARIO_ITEM_SEQ_01";
        /// <summary>
        /// GTシナリオアイテムIDの名称
        /// </summary>
        public static string GT_SCENARIO_ITEM_ID = "GT_SCENARIO_ITEM_ID";
        /// <summary>
        /// グラフ種別レポートの名称
        /// </summary>
        public static string GRAPH_TYPE_REPORT = "GRAPH_TYPE_REPORT";
        #endregion
        /// <summary>
        /// テーブル項目をList形式で返す
        /// </summary>
        /// <returns></returns>
        public static List<string> GetColumns()
        {
            List<string> columns = new List<string>();
            columns.Add(GT_SCENARIO_ITEM_ID);
            columns.Add(GRAPH_TYPE_REPORT);
            return columns;
        }
        /// <summary>
        /// テーブル項目に対応するOracleDbTypeをDictionary形式で返す
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, OracleDbType> GetTypes()
        {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(GT_SCENARIO_ITEM_ID, OracleDbType.Decimal);
            types.Add(GRAPH_TYPE_REPORT, OracleDbType.Varchar2);
            return types;
        }
        /// <summary>
        /// UPDATE文を返す
        /// </summary>
        /// <param name="columns">更新対象カラムのリスト</param>
        /// <returns>UPDATE文の文字列</returns>
        public static string GetUpdateSql(List<string> columns)
        {
            return DefaultSql.UpdateSql(columns, TABLE_NAME, GT_SCENARIO_ITEM_ID);
        }
        /// <summary>
        /// UPDATE文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="columns">カラム名のリスト</param>
        /// <param name="values">カラムの値のDictionaryコレクション</param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public static int ExecuteUpdate(List<string> columns, Dictionary<string, object> values, int recordCount)
        {
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert())
            {
                executeCount = logic.ExecuteNonQuery(GetUpdateSql(columns), GetTypes(), values, recordCount);
            }
            return executeCount;
        }
    }
    #endregion

    #region 2.2.クロス集計対象シナリオアイテム
    public static class CrossScenarioTargetSql
    {
        #region プロパティ
        /// <summary>
        /// テーブルの名称
        /// </summary>
        public static string TABLE_NAME = "T_CROSS_SCENARIO_TARGET";
        /// <summary>
        /// シーケンスの名称
        /// </summary>
        public static string SEQUENCE = "T_CROSS_SCENARIO_TARGET_SEQ_01";
        /// <summary>
        /// クロス集計対象シナリオアイテムIDの名称
        /// </summary>
        public static string CROSS_SCENARIO_TARGET_ID = "CROSS_SCENARIO_TARGET_ID";
        /// <summary>
        /// グラフ種別レポートの名称
        /// </summary>
        public static string GRAPH_TYPE_REPORT = "GRAPH_TYPE_REPORT";
        #endregion
        /// <summary>
        /// テーブル項目をList形式で返す
        /// </summary>
        /// <returns></returns>
        public static List<string> GetColumns()
        {
            List<string> columns = new List<string>();
            columns.Add(CROSS_SCENARIO_TARGET_ID);
            columns.Add(GRAPH_TYPE_REPORT);
            return columns;
        }
        /// <summary>
        /// テーブル項目に対応するOracleDbTypeをDictionary形式で返す
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, OracleDbType> GetTypes()
        {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(CROSS_SCENARIO_TARGET_ID, OracleDbType.Decimal);
            types.Add(GRAPH_TYPE_REPORT, OracleDbType.Varchar2);
            return types;
        }
        /// <summary>
        /// UPDATE文を返す
        /// </summary>
        /// <param name="columns">更新対象カラムのリスト</param>
        /// <returns>UPDATE文の文字列</returns>
        public static string GetUpdateSql(List<string> columns)
        {
            return DefaultSql.UpdateSql(columns, TABLE_NAME, CROSS_SCENARIO_TARGET_ID);
        }
        /// <summary>
        /// UPDATE文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="columns">カラム名のリスト</param>
        /// <param name="values">カラムの値のDictionaryコレクション</param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public static int ExecuteUpdate(List<string> columns, Dictionary<string, object> values, int recordCount)
        {
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert())
            {
                executeCount = logic.ExecuteNonQuery(GetUpdateSql(columns), GetTypes(), values, recordCount);
            }
            return executeCount;
        }
    }
    #endregion

    #region 3.1.レポート
    /// <summary>
    /// 3.1.レポート
    /// </summary>
    public static class ReportSql {
        #region プロパティ
        /// <summary>レポートのテーブルの名称</summary>
        public static string TABLE_NAME = "T_Report";
        /// <summary>レポートのシーケンスの名称</summary>
        public static string SEQUENCE = "T_REPORT_SEQ_01";

        /// <summary>レポートTBL.レポートIDのカラム名</summary>
        public static string REPORT_ID = "REPORT_ID";
        /// <summary>レポートTBL.レポートセットIDのカラム名</summary>
        public static string REPORTSET_ID = "REPORTSET_ID";
        /// <summary>レポートTBL.対象シナリオアイテムIDのカラム名</summary>
        public static string TARGET_SCENARIO_ITEM_ID = "TARGET_SCENARIO_ITEM_ID";
        /// <summary>レポートTBL.表示順のカラム名</summary>
        public static string SORT_NO = "SORT_NO";
        /// <summary>レポートTBL.子状態のカラム名</summary>
        public static string CHILD_DIV = "CHILD_DIV";
        /// <summary>レポートTBL.シナリオ区分のカラム名</summary>
        public static string SCENARIO_TYPE = "SCENARIO_TYPE";
        #endregion

        /// <summary>
        /// カテゴリ情報の項目に対応するOracleDbTypeをDictionary形式で返す
        /// </summary>
        public static Dictionary<string, OracleDbType> GetTypes() {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(REPORT_ID, OracleDbType.Decimal);
            types.Add(REPORTSET_ID, OracleDbType.Decimal);
            types.Add(TARGET_SCENARIO_ITEM_ID, OracleDbType.Decimal);
            types.Add(SORT_NO, OracleDbType.Int32);
            types.Add(CHILD_DIV, OracleDbType.Int32);
            types.Add(SCENARIO_TYPE, OracleDbType.Char);
            return types;
        }

        /// <summary>
        /// DELETE文を返す
        /// </summary>
        /// <returns>DELETE文の文字列</returns>
        public static string GetDeleteSql() {
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("DELETE FROM {0} ", TABLE_NAME));
            sql.Append(string.Format("WHERE {0} = :{0} ", REPORTSET_ID));
            sql.Append(string.Format("AND {0} = :{0} ", TARGET_SCENARIO_ITEM_ID));
            sql.Append(string.Format("AND {0} = :{0}", SCENARIO_TYPE));
            return sql.ToString();
        }

        /// <summary>
        /// 3.1.レポートのDELETE文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="values"></param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public static int ExecuteDelete(Dictionary<string, object> values, int recordCount) {
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert()) {
                executeCount = logic.ExecuteNonQuery(GetDeleteSql(), GetTypes(), values, recordCount);
            }
            return executeCount;
        }
    }
    #endregion

    #region ローデータ
    /// <summary>
    /// ローデータ
    /// </summary>
    public static class RawdataBindSql {
        #region プロパティ
        /// <summary>繰り返しカラム数(FA)</summary>
        public static int RAWDATA_FA_COLUMNS_NUM = 200;

        /// <summary>ローデータTBLサンプルID</summary>
        public static string SAMPLE_ID = "SAMPLE_ID";
        /// <summary>ローデータTBLソート順</summary>
        public static string SORT_NO = "SORT_NO";
        /// <summary>ローデータTBL削除フラグ</summary>
        public static string DELETE_FLAG = "DELETE_FLAG";
        #endregion

        #region WithOne
        public static Dictionary<string, OracleDbType> GetTypesWithOne(int ColumnNum) {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(SAMPLE_ID, OracleDbType.Varchar2);
            types.Add(SORT_NO, OracleDbType.Int64);
            types.Add(DELETE_FLAG, OracleDbType.Int64);
            for (int i = 4; i < ColumnNum + 1; ++i) {
                types.Add(string.Format("Q{0}", i.ToString(new string('0', 3))), OracleDbType.NVarchar2);
            }
            return types;
        }

        public static List<string> GetColumnsWithOne(int ColumnNum) {
            List<string> columns = new List<string>();
            columns.Add(SAMPLE_ID);
            columns.Add(SORT_NO);
            columns.Add(DELETE_FLAG);
            for (int i = 4; i < ColumnNum + 1; ++i) {
                columns.Add(string.Format("Q{0}", i.ToString(new string('0', 3))));
            }
            return columns;
        }

        public static string GetInsertSqlWithOne(string tableName, int ColumnNum) {
            return DefaultSql.InsertSql(GetColumnsWithOne(ColumnNum), tableName, null, null);
        }

        public static int ExecuteInsertWithOne(string tableName, IDictionary<string, object> values, int recordCount) {
            if (values == null || values.Count == 0)
                return 0;
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert()) {
                executeCount = logic.ExecuteNonQuery(GetInsertSqlWithOne(tableName, values.Count).Replace("(,", "(")
                                                    , GetTypesWithOne(values.Count), values, recordCount);
            }
            return executeCount;
        }
        #endregion

        #region WithOther
        public static Dictionary<string, OracleDbType> GetTypesWithOther(int ColumnNum) {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(SAMPLE_ID, OracleDbType.Varchar2);
            types.Add(SORT_NO, OracleDbType.Int64);
            for (int i = 2; i < ColumnNum; ++i) {
                types.Add(string.Format("Q{0}", i.ToString(new string('0', 3))), OracleDbType.NVarchar2);
            }
            return types;
        }

        public static List<string> GetColumnsWithOther(int ColumnNum) {
            List<string> columns = new List<string>();
            columns.Add(SAMPLE_ID);
            columns.Add(SORT_NO);
            for (int i = 2; i < ColumnNum; ++i) {
                columns.Add(string.Format("Q{0}", i.ToString(new string('0', 3))));
            }
            return columns;
        }

        public static string GetInsertSqlWithOther(string tableName, int ColumnNum) {
            return DefaultSql.InsertSql(GetColumnsWithOther(ColumnNum), tableName, null, null);
        }

        public static int ExecuteInsertWithOther(string tableName, IDictionary<string, object> values, int recordCount) {
            if (values == null || values.Count == 0)
                return 0;
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert()) {
                executeCount = logic.ExecuteNonQuery(GetInsertSqlWithOther(tableName, values.Count).Replace("(,", "(")
                                                    , GetTypesWithOther(values.Count), values, recordCount);
            }
            return executeCount;
        }
        #endregion

        /// <summary>
        /// ローデータTBL(FA)の項目に対応するOracleDbTypeをDictionary形式で返す
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, OracleDbType> GetTypesFA(int ColumnNum = 200) {
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            types.Add(SAMPLE_ID, OracleDbType.Varchar2);
            types.Add(SORT_NO, OracleDbType.Int64);
            for (int i = 1; i <= ColumnNum - 2; ++i) {
                types.Add(string.Format("FA{0}", i.ToString(new string('0', 3))), OracleDbType.NVarchar2);
            }
            return types;
        }

        /// <summary>
        /// ローデータTBL(FA)の項目をList形式で返す
        /// </summary>
        /// <returns></returns>
        public static List<string> GetColumnsFA(int ColumnNum = 200) {
            List<string> columns = new List<string>();
            columns.Add(SAMPLE_ID);
            columns.Add(SORT_NO);
            for (int i = 1; i <= ColumnNum - 2; ++i) {
                columns.Add(string.Format("FA{0}", i.ToString(new string('0', 3))));
            }
            return columns;
        }

        /// <summary>
        /// INSERT文(FA)を返す
        /// </summary>
        /// <returns>INSERT文の文字列</returns>
        public static string GetInsertSqlFA(string tableName, int ColumnNum) {
            return DefaultSql.InsertSql(GetColumnsFA(ColumnNum), tableName, null, null);
        }

        /// <summary>
        /// ローデータTBL(FA)のINSERT文を実行するメソッド
        /// SQL文に登場するバインド変数の順番と、パラメータの登録する順番とを一致させること
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static int ExecuteInsertFA(string tableName, IDictionary<string, object> values, int recordCount) {
            if (values == null || values.Count == 0) return 0;
            int executeCount = 0;
            using (ArrayBindInsert logic = new ArrayBindInsert()) {
                executeCount = logic.ExecuteNonQuery(GetInsertSqlFA(tableName, values.Count).Replace("(,", "(")
                                                    , GetTypesFA(values.Count), values, recordCount);
            }
            return executeCount;
        }
    }
    #endregion

    /// <summary>
    /// ODP.NETのArrayBindでINSERTするクラス
    /// </summary>
    public class ArrayBindInsert : IDisposable
    {
        #region メンバ変数
        private OracleConnection conn = null;
        #endregion コンストラクタ

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// トランザクション制御を呼び出し元で行わない場合
        /// </summary>
        /// <param name="current">ITransactionContext</param>
        public ArrayBindInsert()
        {
            QuillInjector injector = QuillInjector.GetInstance();
            ITransactionSetting s =
                (ITransactionSetting)ComponentUtil.GetComponent(injector.Container, typeof(TypicalTransactionSetting));
            ITransactionContext txc = s.TransactionContext;
            ITransactionContext current = null;
            if (txc.Current == null) current = txc.Create();
            else current = txc.Current;
            conn = (OracleConnection)current.Connection;
        }
        /// <summary>
        /// コンストラクタ
        /// トランザクション制御を呼び出し元で行う場合
        /// </summary>
        /// <param name="current">ITransactionContext</param>
        public ArrayBindInsert(ITransactionContext current)
        {
            conn = (OracleConnection)current.Connection;
        }
        #endregion コンストラクタ

        /// <summary>
        /// ArrayBindによるINSERT/UPDATE/DELETE実行
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="columns">バインドするカラム名のList</param>
        /// <param name="types">カラムの型のDictionaryコレクション</param>
        /// <param name="values">カラムの値のDictionaryコレクション</param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>
        /// 処理された件数
        /// </returns>
        public int ExecuteNonQuery(String sql, List<string> columns, Dictionary<string, OracleDbType> types, Dictionary<string, object> values, int recordCount)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandText = sql;
            cmd.ArrayBindCount = recordCount;

            for (int i = 0; i < columns.Count; i++)
            {
                OracleParameter prm = new OracleParameter();
                prm.ParameterName = columns[i];
                prm.OracleDbType = types[columns[i]];
                prm.Value = values[columns[i]];
                prm.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(prm);
            }
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// ArrayBindによるINSERT/UPDATE/DELETE実行
        /// valuesに設定されたkeyでパラメータを設定し、SQL文を実行する
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="types">カラムの型のDictionaryコレクション</param>
        /// <param name="values">カラムの値のDictionaryコレクション</param>
        /// <param name="recordCount">実行するレコード数</param>
        /// <returns>処理された件数</returns>
        public int ExecuteNonQuery(String sql, IDictionary<string, OracleDbType> types, IDictionary<string, object> values, int recordCount)
        {
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandText = sql;
            cmd.ArrayBindCount = recordCount;
            cmd.BindByName = true;

            foreach(string key in values.Keys)
            {
                OracleParameter prm = new OracleParameter();
                prm.ParameterName = key;
                prm.OracleDbType = types[key];
                prm.Value = values[key];
                prm.Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add(prm);
            }
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// ArrayBindによるINSERT
        /// </summary>
        /// <param name="insertList">登録対象データリスト</param>
        /// <param name="autoSeqFlag">サロゲートキーの採番を自動で行う(シーケンスを利用する)場合true、自動で行わない場合false　(省略時、false)</param>
        /// <returns>Insert件数</returns>
        public int ExecuteInsert<ENTITY>(IList<ENTITY> insertList, bool autoSeqFlag = false) where ENTITY : Entity
        {
            if (insertList != null && insertList.Count <= 0) return 0;

            string tableName = insertList[0].TableDbName;
            DBMeta metaData = insertList[0].DBMeta;

            // SQL文の生成、バインドするカラム名と型を設定する
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlValue = new StringBuilder();
            //List<string> columns = new List<string>();
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            List<ColumnInfo> columnInfoList = (List<ColumnInfo>)metaData.ColumnInfoList.getList();

            sql.Append(string.Format("INSERT INTO {0}", tableName));
            sql.Append("(");
            for (int i = 0; i < columnInfoList.Count; ++i)
            {
                var columnInfo = columnInfoList[i];
                //columns.Add(columnInfo.ColumnDbName);
                //OracleDbType ora = (OracleDbType)Enum.Parse(typeof(OracleDbType), columnInfoList[i].ColumnDbType);
                types.Add(columnInfo.ColumnDbName, GetOracleDbType(columnInfo));

                if (i != 0)
                {
                    sql.Append(",");
                    sqlValue.Append(",");
                }

                sql.Append(string.Format(" {0}", columnInfo.ColumnDbName));

                if (autoSeqFlag && columnInfo.IsPrimary)
                {
                    sqlValue.Append(string.Format("{0}.NEXTVAL", columnInfo.DBMeta.SequenceName));
                }
                else
                {
                    sqlValue.Append(string.Format(" :{0}", columnInfo.ColumnDbName));
                }
            }
            sql.Append(") VALUES (");
            sql.Append(sqlValue.ToString());
            sql.Append(")");

            System.Diagnostics.Debug.WriteLine(string.Format("ExecuteInsert sql=[{0}]", sql.ToString()));

            Dictionary<string, object> values = new Dictionary<string, object>();
            Type t = insertList[0].GetType();
            for (int i = 0; i < columnInfoList.Count; ++i)
            {
                var columnInfo = columnInfoList[i];
                //シーケンスを利用する場合はバインド変数を利用しないので、valuesからも省く
                if (autoSeqFlag && columnInfo.IsPrimary) continue;

                List<object> columnDatas = new List<object>();
                for (int j = 0; j < insertList.Count; ++j)
                {
                    object colunData = t.InvokeMember(columnInfo.PropertyName, BindingFlags.GetProperty, null, insertList[j], null);
                    columnDatas.Add(colunData);
                }
                values.Add(columnInfo.ColumnDbName, columnDatas.ToArray());
            }
            //return ExecuteInsert(sql.ToString(), columns, types, values, insertList.Count);
            return ExecuteNonQuery(sql.ToString(), types, values, insertList.Count);
        }

        /// <summary>
        /// ArrayBindによるDELETE
        /// <note>
        /// 注意事項
        /// 1. PKを条件にした更新のみサポート
        /// </note>
        /// </summary>
        /// <typeparam name="ENTITY">DBFlute.netのEntityクラスを指定すること</typeparam>
        /// <param name="deleteList">削除対象データリスト</param>
        /// <returns>処理された件数</returns>
        public int ExecuteDelete<ENTITY>(IList<ENTITY> deleteList) where ENTITY : Entity {
            if (deleteList != null && deleteList.Count <= 0) return 0;

            string tableName = deleteList[0].TableDbName;
            DBMeta metaData = deleteList[0].DBMeta;
            List<ColumnInfo> columnInfoList = (List<ColumnInfo>)metaData.ColumnInfoList.getList();
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();

            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("DELETE FROM {0} WHERE ",tableName));
            foreach (ColumnInfo columnInfo in columnInfoList) {
                if (columnInfo.IsPrimary) {
                    sql.Append(string.Format("{0} = :{0}", columnInfo.ColumnDbName));
                    types.Add(columnInfo.ColumnDbName, GetOracleDbType(columnInfo));
                    break;
                }
            }

            Dictionary<string, object> values = new Dictionary<string, object>();
            Type t = deleteList[0].GetType();
            for (int i = 0; i < columnInfoList.Count; ++i) {
                var columnInfo = columnInfoList[i];
                if (columnInfo.IsPrimary) {
                    List<object> columnDatas = new List<object>();
                    for (int j = 0; j < deleteList.Count; ++j) {
                        object colunData = t.InvokeMember(columnInfo.PropertyName, BindingFlags.GetProperty, null, deleteList[j], null);
                        columnDatas.Add(colunData);
                    }
                    values.Add(columnInfo.ColumnDbName, columnDatas.ToArray());
                    break;
                }
            }
            return ExecuteNonQuery(sql.ToString(), types, values, deleteList.Count);
        }

        /// <summary>
        /// ArrayBindによるUPDATE
        /// <note>
        /// 注意事項
        /// 1. PKを条件にした更新のみサポート
        /// 2. 楽観排他用カラム(VERSION)は自動でインクリメントされません。
        /// 3. 最終更新者、最終更新日時
        /// </note>
        /// </summary>
        /// <typeparam name="ENTITY">DBFlute.netのEntityクラスを指定すること</typeparam>
        /// <param name="updateList">更新対象データリスト</param>
        /// <returns>処理された件数</returns>
        public int ExecuteUpdate<ENTITY>(IList<ENTITY> updateList) where ENTITY : Entity {
            if (updateList == null || updateList.Count <= 0) return 0;

            string tableName = updateList[0].TableDbName;
            DBMeta metaData = updateList[0].DBMeta;

            // SQL文の生成、バインドするカラム名と型を設定する
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlWhere = new StringBuilder();
            Dictionary<string, OracleDbType> types = new Dictionary<string, OracleDbType>();
            List<ColumnInfo> columnInfoList = (List<ColumnInfo>)metaData.ColumnInfoList.getList();

            sql.Append(string.Format("UPDATE {0} SET ",tableName));
            bool commaFlag = false;
            foreach (ColumnInfo columnInfo in columnInfoList) {
                types.Add(columnInfo.ColumnDbName, GetOracleDbType(columnInfo));
                if (columnInfo.IsPrimary) {
                    sqlWhere.Append(string.Format(" WHERE {0} = :{0}", columnInfo.ColumnDbName));
                } else {
                    if (commaFlag) sql.Append(",");
                    sql.Append(string.Format(" {0} = :{0} ", columnInfo.ColumnDbName));
                    commaFlag = true;
                }
            }
            sql.Append(sqlWhere.ToString());

            Dictionary<string, object> values = new Dictionary<string, object>();
            Type t = updateList[0].GetType();
            for (int i = 0; i < columnInfoList.Count; ++i) {
                var columnInfo = columnInfoList[i];
                List<object> columnDatas = new List<object>();
                for (int j = 0; j < updateList.Count; ++j) {
                    object colunData = t.InvokeMember(columnInfo.PropertyName, BindingFlags.GetProperty, null, updateList[j], null);
                    columnDatas.Add(colunData);
                }
                values.Add(columnInfo.ColumnDbName, columnDatas.ToArray());
            }

            return ExecuteNonQuery(sql.ToString(), types, values, updateList.Count);
        }

        /// <summary>
        /// ColumnInfoのColumnDbTypeからOracleDbTypeに変換する
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static OracleDbType GetOracleDbType(ColumnInfo columnInfo) {
            OracleDbType oracleDbType = (OracleDbType)0;
            switch (columnInfo.ColumnDbType) {
                case "NUMBER":
                    int? size = columnInfo.ColumnSize;
                    int? decimalSize = columnInfo.ColumnDecimalDigits;
                    if (decimalSize != null && decimalSize > 0) {
                        oracleDbType = OracleDbType.BinaryDouble;
                    } else {
                        if (size != null) {
                            size = size - decimalSize;
                            if (size > 20) {
                                oracleDbType = OracleDbType.Decimal;
                            } else if (size > 9) {
                                oracleDbType = OracleDbType.Int64;
                            } else {
                                oracleDbType = OracleDbType.Int32;
                            }
                        } else {
                            oracleDbType = OracleDbType.Decimal;
                        }
                    }
                    break;
                case "VARCHAR2":
                    oracleDbType = OracleDbType.Varchar2;
                    break;
                case "NVARCHAR2":
                    oracleDbType = OracleDbType.NVarchar2;
                    break;
                case "CHAR":
                    oracleDbType = OracleDbType.Char;
                    break;
                case "DATE":
                    oracleDbType = OracleDbType.Date;
                    break;
                case "TIMESTAMP":
                case "TIMESTAMP(6)":
                    oracleDbType = OracleDbType.TimeStamp;
                    break;
                case "NCLOB":
                    oracleDbType = OracleDbType.NClob;
                    break;
                case "BINARY_DOUBLE":
                    oracleDbType = OracleDbType.BinaryDouble;
                    break;
                default:
                    break;
            }
            return oracleDbType;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            if (conn != null) {
                //conn.Close();
                conn = null;
            }
        }        
    }
}
