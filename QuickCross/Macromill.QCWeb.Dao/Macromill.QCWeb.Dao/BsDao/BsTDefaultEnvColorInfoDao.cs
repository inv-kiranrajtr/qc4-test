
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
    [Bean(typeof(TDefaultEnvColorInfo))]
    public partial interface TDefaultEnvColorInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDefaultEnvColorInfoCB cb);
        IList<TDefaultEnvColorInfo> SelectList(TDefaultEnvColorInfoCB cb);

        int Insert(TDefaultEnvColorInfo entity);
        int UpdateModifiedOnly(TDefaultEnvColorInfo entity);
        int UpdateNonstrictModifiedOnly(TDefaultEnvColorInfo entity);
        int UpdateByQuery(TDefaultEnvColorInfoCB cb, TDefaultEnvColorInfo entity);
        int Delete(TDefaultEnvColorInfo entity);
        int DeleteNonstrict(TDefaultEnvColorInfo entity);
        int DeleteByQuery(TDefaultEnvColorInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
