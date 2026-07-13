
using System;
using System.Collections.Generic;

namespace Macromill.QCWeb.Dao.AllCommon.CBean {

    public delegate void UnionQuery<UNION_CB>(UNION_CB unionCB) where UNION_CB : ConditionBean;
}
