
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
    [Bean(typeof(TIntegCondition))]
    public partial interface TIntegConditionDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TIntegConditionCB cb);
        IList<TIntegCondition> SelectList(TIntegConditionCB cb);

        int Insert(TIntegCondition entity);
        int UpdateModifiedOnly(TIntegCondition entity);
        int UpdateNonstrictModifiedOnly(TIntegCondition entity);
        int UpdateByQuery(TIntegConditionCB cb, TIntegCondition entity);
        int Delete(TIntegCondition entity);
        int DeleteNonstrict(TIntegCondition entity);
        int DeleteByQuery(TIntegConditionCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
