
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
    [Bean(typeof(TItemInfo))]
    public partial interface TItemInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TItemInfoCB cb);
        IList<TItemInfo> SelectList(TItemInfoCB cb);

        int Insert(TItemInfo entity);
        int UpdateModifiedOnly(TItemInfo entity);
        int UpdateNonstrictModifiedOnly(TItemInfo entity);
        int UpdateByQuery(TItemInfoCB cb, TItemInfo entity);
        int Delete(TItemInfo entity);
        int DeleteNonstrict(TItemInfo entity);
        int DeleteByQuery(TItemInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
