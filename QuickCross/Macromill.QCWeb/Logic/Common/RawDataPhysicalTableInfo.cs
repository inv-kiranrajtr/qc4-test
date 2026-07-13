#region Copyright
/****************************************************************
 * 著　作　権：株式会社マクロミル
 * システム名：Quick-CROSS Web
 * ファイル名：RawDataPhysicalTableInfo.cs
 * バージョン：1.0.0
 * 概　　　要：物理テーブル情報保持用Bean
 * 作　成　日：2012/04/19
 * 作　成　者：小松 正明
 * $Id$ / $Date$ / $Rev$ / $Author$
  ***************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Macromill.QCWeb.Logic.Common {
    /// <summary>
    /// ローデータテーブル情報保持クラス
    /// </summary>
    [Serializable, ComVisible(false), Guid("DA74D2DC-D072-4a4f-BF0A-0385B76863DE")]
    public class RawDataPhysicalTableInfo {
        /// <summary>
        /// テーブル名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// フィールド名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// テーブルNo
        /// </summary>
        public int TableNo { get; set; }
        /// <summary>
        /// フィールドNo
        /// </summary>
        public int FieldNo { get; set; }
    }
}
