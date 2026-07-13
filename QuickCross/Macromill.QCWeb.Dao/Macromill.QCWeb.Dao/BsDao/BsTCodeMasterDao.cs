
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
    [Bean(typeof(TCodeMaster))]
    public partial interface TCodeMasterDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TCodeMasterCB cb);
        IList<TCodeMaster> SelectList(TCodeMasterCB cb);

        int Insert(TCodeMaster entity);
        int UpdateModifiedOnly(TCodeMaster entity);
        int UpdateNonstrictModifiedOnly(TCodeMaster entity);
        int UpdateByQuery(TCodeMasterCB cb, TCodeMaster entity);
        int Delete(TCodeMaster entity);
        int DeleteNonstrict(TCodeMaster entity);
        int DeleteByQuery(TCodeMasterCB cb);// {DBFlute-0.7.9}
    }
}
