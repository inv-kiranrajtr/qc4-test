using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seasar.Dao.Attrs;

namespace Macromill.QCWeb.Dao.HelloDBEntity
{
    /// <summary>
    /// Testテーブルの行をあらわす
    /// </summary>
    [Table("DUAL")]
    public class Dual
    {
        /// <summary>
        /// DUMMY
        /// </summary>
        private string _dummy;

        /// <summary>
        /// DUMMY
        /// </summary>
        [Column("DUMMY")]
        public string dummy
        {
            get { return _dummy; }
            set { _dummy = value; }
        }
    }
}