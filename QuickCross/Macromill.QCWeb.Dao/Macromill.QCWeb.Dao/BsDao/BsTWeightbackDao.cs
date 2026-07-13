
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
    [Bean(typeof(TWeightback))]
    public partial interface TWeightbackDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TWeightbackCB cb);
        IList<TWeightback> SelectList(TWeightbackCB cb);

        int Insert(TWeightback entity);
        int UpdateModifiedOnly(TWeightback entity);
        int UpdateNonstrictModifiedOnly(TWeightback entity);
        int UpdateByQuery(TWeightbackCB cb, TWeightback entity);
        int Delete(TWeightback entity);
        int DeleteNonstrict(TWeightback entity);
        int DeleteByQuery(TWeightbackCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
