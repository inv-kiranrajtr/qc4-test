
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
    [Bean(typeof(TCategoryInfo))]
    public partial interface TCategoryInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TCategoryInfoCB cb);
        IList<TCategoryInfo> SelectList(TCategoryInfoCB cb);

        int Insert(TCategoryInfo entity);
        int UpdateModifiedOnly(TCategoryInfo entity);
        int UpdateNonstrictModifiedOnly(TCategoryInfo entity);
        int UpdateByQuery(TCategoryInfoCB cb, TCategoryInfo entity);
        int Delete(TCategoryInfo entity);
        int DeleteNonstrict(TCategoryInfo entity);
        int DeleteByQuery(TCategoryInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
