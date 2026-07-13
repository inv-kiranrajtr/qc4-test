
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
    [Bean(typeof(TFaScenarioItem))]
    public partial interface TFaScenarioItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TFaScenarioItemCB cb);
        IList<TFaScenarioItem> SelectList(TFaScenarioItemCB cb);

        int Insert(TFaScenarioItem entity);
        int UpdateModifiedOnly(TFaScenarioItem entity);
        int UpdateNonstrictModifiedOnly(TFaScenarioItem entity);
        int UpdateByQuery(TFaScenarioItemCB cb, TFaScenarioItem entity);
        int Delete(TFaScenarioItem entity);
        int DeleteNonstrict(TFaScenarioItem entity);
        int DeleteByQuery(TFaScenarioItemCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
