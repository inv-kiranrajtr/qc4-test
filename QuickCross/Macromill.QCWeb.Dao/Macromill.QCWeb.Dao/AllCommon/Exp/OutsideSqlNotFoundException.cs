
using System;
using System.Collections;

namespace Macromill.QCWeb.Dao.AllCommon.Exp {

    /// <summary>
    /// The exception of when the outside-sql is not found.
    /// Author: DBFlute(AutoGenerator)
    /// </summary>
    public class OutsideSqlNotFoundException : SystemException {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param="msg">Exception message.</param>
        public OutsideSqlNotFoundException(String msg)
        : base(msg) {}
    }
}
