#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：DatabaseReader.cs
 * バージョン：1.0.0
 * 概　　　要：ODP.NETでテーブルを読み込む
 * 作　成　日：2012/12/13
 * 作　成　者：中山大介
 * 更　新　日：
 * $Id$ / $Date$ / $Rev$ / $Author$
 ***************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seasar.Quill.Database.Tx;
using Seasar.Quill.Util;
using Seasar.Quill.Database.Tx.Impl;
using Seasar.Extension.Tx;
using Oracle.DataAccess.Client;

namespace Macromill.QCWeb.Dao.DBReader
{
    /// <summary>
    /// DBから読み込んでstringで出力するクラス
    /// </summary>
    public class DatabaseReader : IDisposable
    {
        #region メンバ変数
        private OracleConnection conn = null;
        private OracleDataReader myReader = null;
        private OracleCommand cmd = null;

        /// <summary>
        /// FieldCountプロパティ
        /// </summary>
        public int FieldCount
        {
            get
            {
                return myReader != null ? myReader.FieldCount : 0;
            }
        }

        /// <summary>
        /// RowSizeプロパティ
        /// </summary>
        public long RowSize
        {
            get
            {
                return cmd != null ? cmd.RowSize : 0;
            }
        }

        /// <summary>
        /// FetchSizeプロパティ
        /// </summary>
        private long fetchSize = 0;
        public long FetchSize
        {
            get
            {
                return fetchSize;
            }
            set
            {
                fetchSize = value;
            }
        }
        #endregion
        
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="current">ITransactionContext</param>
        public DatabaseReader(ITransactionContext current = null)
        {
            conn = (OracleConnection)current.Connection;
        }
        #endregion コンストラクタ

        public void ExecuteReader(String sql)
        {
            cmd = new OracleCommand(sql, conn);
            myReader = cmd.ExecuteReader();
        }

        /// <summary>
        /// GetMyReader
        /// </summary>
        /// <param name="i">i</param>
        /// <returns>
        /// 読み取った文字列
        /// </returns>
        public string GetMyReader(int i)
        {
            return (string)myReader[i];
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <returns>
        /// OracleDataReader.Read
        /// </returns>
        public bool Read()
        {
            return myReader.Read();
        }

        /// <summary>
        /// IsDBNull
        /// </summary>
        /// <param name="i">i</param>
        /// <returns>
        /// OracleDataReader.IsDBNull
        /// </returns>
        public bool IsDBNull(int i)
        {
            return myReader.IsDBNull(i);
        }

        /// <summary>
        /// GetString
        /// </summary>
        /// <param name="i">i</param>
        /// <returns>
        /// OracleDataReader.GetString
        /// </returns>
        public string GetString(int i)
        {
            return myReader.GetString(i);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }        
    }
}
