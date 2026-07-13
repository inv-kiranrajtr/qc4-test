
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
    [Bean(typeof(TDefaultEnv))]
    public partial interface TDefaultEnvDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDefaultEnvCB cb);
        IList<TDefaultEnv> SelectList(TDefaultEnvCB cb);

        int Insert(TDefaultEnv entity);
        int UpdateModifiedOnly(TDefaultEnv entity);
        int UpdateNonstrictModifiedOnly(TDefaultEnv entity);
        int UpdateByQuery(TDefaultEnvCB cb, TDefaultEnv entity);
        int Delete(TDefaultEnv entity);
        int DeleteNonstrict(TDefaultEnv entity);
        int DeleteByQuery(TDefaultEnvCB cb);// {DBFlute-0.7.9}
    }
}
