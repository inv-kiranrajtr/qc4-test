
using System;
using System.Collections.Generic;

namespace Macromill.QCWeb.Dao.AllCommon.CBean {

    public delegate void OrQuery<OR_CB>(OR_CB orCB) where OR_CB : ConditionBean;
}
