
using System;
using System.Collections;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.CBean;
using Macromill.QCWeb.Dao.AllCommon.CBean.SClause;
using Macromill.QCWeb.Dao.CBean.CQ.BS;

namespace Macromill.QCWeb.Dao.CBean.CQ {

    [System.Serializable]
    public class TCategoryOutputDetailCQ : BsTCategoryOutputDetailCQ {

        // ===============================================================================
        //                                                                     Constructor
        //                                                                     ===========
        // You should NOT touch with this constructor.
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="childQuery">Child query as interface. (NullAllowed: If null, this is base instance.)</param>
        /// <param name="sqlClause">SQL clause instance. (NotNull)</param>
        /// <param name="aliasName">My alias name. (NotNull)</param>
        /// <param name="nestLevel">Nest level.</param>
        public TCategoryOutputDetailCQ(ConditionQuery childQuery, SqlClause sqlClause, String aliasName, int nestLevel)
            : base(childQuery, sqlClause, aliasName, nestLevel) {}

        // ===============================================================================
        //                                                                   Arrange Query
        //                                                                   =============
        // You can make your arranged query methods here.
        //public void ArrangeXxx() {
        //    ...
        //}
    }
}
