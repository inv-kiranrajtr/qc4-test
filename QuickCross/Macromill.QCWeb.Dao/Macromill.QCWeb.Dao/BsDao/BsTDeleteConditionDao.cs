
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
    [Bean(typeof(TDeleteCondition))]
    public partial interface TDeleteConditionDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDeleteConditionCB cb);
        IList<TDeleteCondition> SelectList(TDeleteConditionCB cb);

        int Insert(TDeleteCondition entity);
        int UpdateModifiedOnly(TDeleteCondition entity);
        int UpdateNonstrictModifiedOnly(TDeleteCondition entity);
        int UpdateByQuery(TDeleteConditionCB cb, TDeleteCondition entity);
        int Delete(TDeleteCondition entity);
        int DeleteNonstrict(TDeleteCondition entity);
        int DeleteByQuery(TDeleteConditionCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
