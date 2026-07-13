
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
    [Bean(typeof(TCrossScenarioTarget))]
    public partial interface TCrossScenarioTargetDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TCrossScenarioTargetCB cb);
        IList<TCrossScenarioTarget> SelectList(TCrossScenarioTargetCB cb);

        int Insert(TCrossScenarioTarget entity);
        int UpdateModifiedOnly(TCrossScenarioTarget entity);
        int UpdateNonstrictModifiedOnly(TCrossScenarioTarget entity);
        int UpdateByQuery(TCrossScenarioTargetCB cb, TCrossScenarioTarget entity);
        int Delete(TCrossScenarioTarget entity);
        int DeleteNonstrict(TCrossScenarioTarget entity);
        int DeleteByQuery(TCrossScenarioTargetCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
