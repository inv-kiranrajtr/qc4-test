
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
    [Bean(typeof(TGtScenarioItem))]
    public partial interface TGtScenarioItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TGtScenarioItemCB cb);
        IList<TGtScenarioItem> SelectList(TGtScenarioItemCB cb);

        int Insert(TGtScenarioItem entity);
        int UpdateModifiedOnly(TGtScenarioItem entity);
        int UpdateNonstrictModifiedOnly(TGtScenarioItem entity);
        int UpdateByQuery(TGtScenarioItemCB cb, TGtScenarioItem entity);
        int Delete(TGtScenarioItem entity);
        int DeleteNonstrict(TGtScenarioItem entity);
        int DeleteByQuery(TGtScenarioItemCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
