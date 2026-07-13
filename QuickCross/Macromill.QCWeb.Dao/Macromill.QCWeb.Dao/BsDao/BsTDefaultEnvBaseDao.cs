
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
    [Bean(typeof(TDefaultEnvBase))]
    public partial interface TDefaultEnvBaseDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDefaultEnvBaseCB cb);
        IList<TDefaultEnvBase> SelectList(TDefaultEnvBaseCB cb);

        int Insert(TDefaultEnvBase entity);
        int UpdateModifiedOnly(TDefaultEnvBase entity);
        int UpdateNonstrictModifiedOnly(TDefaultEnvBase entity);
        int UpdateByQuery(TDefaultEnvBaseCB cb, TDefaultEnvBase entity);
        int Delete(TDefaultEnvBase entity);
        int DeleteNonstrict(TDefaultEnvBase entity);
        int DeleteByQuery(TDefaultEnvBaseCB cb);// {DBFlute-0.7.9}
    }
}
