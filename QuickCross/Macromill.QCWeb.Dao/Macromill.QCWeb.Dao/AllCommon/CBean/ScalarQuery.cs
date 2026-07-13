
using System;
using System.Collections.Generic;

namespace Macromill.QCWeb.Dao.AllCommon.CBean {

    public delegate void ScalarQuery<CB>(CB cb) where CB : ConditionBean;
}
