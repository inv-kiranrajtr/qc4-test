
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
    [Bean(typeof(TEditMenuMaster))]
    public partial interface TEditMenuMasterDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TEditMenuMasterCB cb);
        IList<TEditMenuMaster> SelectList(TEditMenuMasterCB cb);

        int Insert(TEditMenuMaster entity);
        int UpdateModifiedOnly(TEditMenuMaster entity);
        int UpdateNonstrictModifiedOnly(TEditMenuMaster entity);
        int UpdateByQuery(TEditMenuMasterCB cb, TEditMenuMaster entity);
        int Delete(TEditMenuMaster entity);
        int DeleteNonstrict(TEditMenuMaster entity);
        int DeleteByQuery(TEditMenuMasterCB cb);// {DBFlute-0.7.9}
    }
}
