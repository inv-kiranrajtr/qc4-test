
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
    [Bean(typeof(TEditData))]
    public partial interface TEditDataDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TEditDataCB cb);
        IList<TEditData> SelectList(TEditDataCB cb);

        int Insert(TEditData entity);
        int UpdateModifiedOnly(TEditData entity);
        int UpdateNonstrictModifiedOnly(TEditData entity);
        int UpdateByQuery(TEditDataCB cb, TEditData entity);
        int Delete(TEditData entity);
        int DeleteNonstrict(TEditData entity);
        int DeleteByQuery(TEditDataCB cb);// {DBFlute-0.7.9}
    }
}
