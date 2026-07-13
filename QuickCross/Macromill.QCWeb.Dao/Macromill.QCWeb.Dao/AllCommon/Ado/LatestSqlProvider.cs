
using System;
using System.Collections.Generic;

using Seasar.Quill.Attrs;

using Macromill.QCWeb.Dao.AllCommon.S2Dao;

namespace Macromill.QCWeb.Dao.AllCommon.Ado {

    [Implementation(typeof(SqlLogRegistryLatestSqlProvider))]
    public interface LatestSqlProvider {
        String GetDisplaySql();
        IList<String> ExtractDisplaySqlList();
	    void ClearSqlCache();
    }
}
