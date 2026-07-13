
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
    [Bean(typeof(TCrossScenarioItem))]
    public partial interface TCrossScenarioItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TCrossScenarioItemCB cb);
        IList<TCrossScenarioItem> SelectList(TCrossScenarioItemCB cb);

        int Insert(TCrossScenarioItem entity);
        int UpdateModifiedOnly(TCrossScenarioItem entity);
        int UpdateNonstrictModifiedOnly(TCrossScenarioItem entity);
        int UpdateByQuery(TCrossScenarioItemCB cb, TCrossScenarioItem entity);
        int Delete(TCrossScenarioItem entity);
        int DeleteNonstrict(TCrossScenarioItem entity);
        int DeleteByQuery(TCrossScenarioItemCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
