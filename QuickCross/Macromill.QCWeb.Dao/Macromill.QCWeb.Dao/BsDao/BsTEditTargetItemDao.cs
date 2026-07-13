
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
    [Bean(typeof(TEditTargetItem))]
    public partial interface TEditTargetItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TEditTargetItemCB cb);
        IList<TEditTargetItem> SelectList(TEditTargetItemCB cb);

        int Insert(TEditTargetItem entity);
        int UpdateModifiedOnly(TEditTargetItem entity);
        int UpdateNonstrictModifiedOnly(TEditTargetItem entity);
        int UpdateByQuery(TEditTargetItemCB cb, TEditTargetItem entity);
        int Delete(TEditTargetItem entity);
        int DeleteNonstrict(TEditTargetItem entity);
        int DeleteByQuery(TEditTargetItemCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
