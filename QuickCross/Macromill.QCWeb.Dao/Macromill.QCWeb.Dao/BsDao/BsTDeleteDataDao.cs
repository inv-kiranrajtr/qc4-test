
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
    [Bean(typeof(TDeleteData))]
    public partial interface TDeleteDataDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDeleteDataCB cb);
        IList<TDeleteData> SelectList(TDeleteDataCB cb);

        int Insert(TDeleteData entity);
        int UpdateModifiedOnly(TDeleteData entity);
        int UpdateNonstrictModifiedOnly(TDeleteData entity);
        int UpdateByQuery(TDeleteDataCB cb, TDeleteData entity);
        int Delete(TDeleteData entity);
        int DeleteNonstrict(TDeleteData entity);
        int DeleteByQuery(TDeleteDataCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
