
using System;

namespace Macromill.QCWeb.Dao.AllCommon.Annotation {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OutsideSql : Attribute {
        public OutsideSql() {
        }
    }
}
