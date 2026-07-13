
using System;
using System.Collections.Generic;

namespace Macromill.QCWeb.Dao.AllCommon.CBean {

    public interface PagingHandler<ENTITY> {

        PagingBean PagingBean { get; }
        int Count();
        IList<ENTITY> Paging();
    }
}
