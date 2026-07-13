
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
    [Bean(typeof(TOutputSubGt))]
    public partial interface TOutputSubGtDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSubGtCB cb);
        IList<TOutputSubGt> SelectList(TOutputSubGtCB cb);

        int Insert(TOutputSubGt entity);
        int UpdateModifiedOnly(TOutputSubGt entity);
        int UpdateNonstrictModifiedOnly(TOutputSubGt entity);
        int UpdateByQuery(TOutputSubGtCB cb, TOutputSubGt entity);
        int Delete(TOutputSubGt entity);
        int DeleteNonstrict(TOutputSubGt entity);
        int DeleteByQuery(TOutputSubGtCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
