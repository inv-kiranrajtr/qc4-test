
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
    [Bean(typeof(TEditCondition))]
    public partial interface TEditConditionDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TEditConditionCB cb);
        IList<TEditCondition> SelectList(TEditConditionCB cb);

        int Insert(TEditCondition entity);
        int UpdateModifiedOnly(TEditCondition entity);
        int UpdateNonstrictModifiedOnly(TEditCondition entity);
        int UpdateByQuery(TEditConditionCB cb, TEditCondition entity);
        int Delete(TEditCondition entity);
        int DeleteNonstrict(TEditCondition entity);
        int DeleteByQuery(TEditConditionCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
