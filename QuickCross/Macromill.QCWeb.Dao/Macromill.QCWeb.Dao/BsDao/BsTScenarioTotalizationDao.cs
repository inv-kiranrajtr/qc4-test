
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
    [Bean(typeof(TScenarioTotalization))]
    public partial interface TScenarioTotalizationDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TScenarioTotalizationCB cb);
        IList<TScenarioTotalization> SelectList(TScenarioTotalizationCB cb);

        int Insert(TScenarioTotalization entity);
        int UpdateModifiedOnly(TScenarioTotalization entity);
        int UpdateNonstrictModifiedOnly(TScenarioTotalization entity);
        int UpdateByQuery(TScenarioTotalizationCB cb, TScenarioTotalization entity);
        int Delete(TScenarioTotalization entity);
        int DeleteNonstrict(TScenarioTotalization entity);
        int DeleteByQuery(TScenarioTotalizationCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
