
using System;
using System.Collections.Generic;

using Seasar.Quill.Attrs;
using Seasar.Dao.Attrs;

using Macromill.QCWeb.Dao.AllCommon;
using Macromill.QCWeb.Dao.AllCommon.S2Dao;
using Macromill.QCWeb.Dao.ExEntity;
using Macromill.QCWeb.Dao.CBean;

namespace Macromill.QCWeb.Dao.ExDao {

    [Implementation]
    [S2Dao(typeof(S2DaoSetting))]
    [Bean(typeof(TOutputRequest))]
    public partial interface TOutputRequestDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputRequestCB cb);
        IList<TOutputRequest> SelectList(TOutputRequestCB cb);

        int Insert(TOutputRequest entity);
        int UpdateModifiedOnly(TOutputRequest entity);
        int UpdateNonstrictModifiedOnly(TOutputRequest entity);
        int UpdateByQuery(TOutputRequestCB cb, TOutputRequest entity);
        int Delete(TOutputRequest entity);
        int DeleteNonstrict(TOutputRequest entity);
        int DeleteByQuery(TOutputRequestCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
