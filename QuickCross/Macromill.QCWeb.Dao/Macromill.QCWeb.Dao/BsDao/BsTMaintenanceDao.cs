
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
    [Bean(typeof(TMaintenance))]
    public partial interface TMaintenanceDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TMaintenanceCB cb);
        IList<TMaintenance> SelectList(TMaintenanceCB cb);

        int Insert(TMaintenance entity);
        int UpdateModifiedOnly(TMaintenance entity);
        int UpdateNonstrictModifiedOnly(TMaintenance entity);
        int UpdateByQuery(TMaintenanceCB cb, TMaintenance entity);
        int Delete(TMaintenance entity);
        int DeleteNonstrict(TMaintenance entity);
        int DeleteByQuery(TMaintenanceCB cb);// {DBFlute-0.7.9}
    }
}
