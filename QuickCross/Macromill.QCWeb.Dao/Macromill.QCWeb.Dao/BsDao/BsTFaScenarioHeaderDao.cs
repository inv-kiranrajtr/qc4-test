
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
    [Bean(typeof(TFaScenarioHeader))]
    public partial interface TFaScenarioHeaderDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TFaScenarioHeaderCB cb);
        IList<TFaScenarioHeader> SelectList(TFaScenarioHeaderCB cb);

        int Insert(TFaScenarioHeader entity);
        int UpdateModifiedOnly(TFaScenarioHeader entity);
        int UpdateNonstrictModifiedOnly(TFaScenarioHeader entity);
        int UpdateByQuery(TFaScenarioHeaderCB cb, TFaScenarioHeader entity);
        int Delete(TFaScenarioHeader entity);
        int DeleteNonstrict(TFaScenarioHeader entity);
        int DeleteByQuery(TFaScenarioHeaderCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
