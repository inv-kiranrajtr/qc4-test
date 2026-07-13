
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
    [Bean(typeof(TAccessPermissionsInfo))]
    public partial interface TAccessPermissionsInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TAccessPermissionsInfoCB cb);
        IList<TAccessPermissionsInfo> SelectList(TAccessPermissionsInfoCB cb);

        int Insert(TAccessPermissionsInfo entity);
        int UpdateModifiedOnly(TAccessPermissionsInfo entity);
        int UpdateNonstrictModifiedOnly(TAccessPermissionsInfo entity);
        int UpdateByQuery(TAccessPermissionsInfoCB cb, TAccessPermissionsInfo entity);
        int Delete(TAccessPermissionsInfo entity);
        int DeleteNonstrict(TAccessPermissionsInfo entity);
        int DeleteByQuery(TAccessPermissionsInfoCB cb);// {DBFlute-0.7.9}
    }
}
