
using System;
using System.Collections.Generic;

using Macromill.QCWeb.Dao.AllCommon;

namespace Macromill.QCWeb.Dao.AllCommon.Bhv.Setup {

    public delegate void EntityListSetupper<ENTITY>(IList<ENTITY> entityList) where ENTITY : Entity;
}
