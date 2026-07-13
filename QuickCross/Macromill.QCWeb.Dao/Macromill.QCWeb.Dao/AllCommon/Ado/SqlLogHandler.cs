
using System;

namespace Macromill.QCWeb.Dao.AllCommon.Ado {

    public delegate void SqlLogHandler(String executedSql, String displaySql, Object[] args, Type[] argTypes);
}
