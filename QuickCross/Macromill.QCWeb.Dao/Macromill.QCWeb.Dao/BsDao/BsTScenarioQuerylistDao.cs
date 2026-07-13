
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
    [Bean(typeof(TScenarioQuerylist))]
    public partial interface TScenarioQuerylistDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TScenarioQuerylistCB cb);
        IList<TScenarioQuerylist> SelectList(TScenarioQuerylistCB cb);

        int Insert(TScenarioQuerylist entity);
        int UpdateModifiedOnly(TScenarioQuerylist entity);
        int UpdateNonstrictModifiedOnly(TScenarioQuerylist entity);
        int UpdateByQuery(TScenarioQuerylistCB cb, TScenarioQuerylist entity);
        int Delete(TScenarioQuerylist entity);
        int DeleteNonstrict(TScenarioQuerylist entity);
        int DeleteByQuery(TScenarioQuerylistCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
