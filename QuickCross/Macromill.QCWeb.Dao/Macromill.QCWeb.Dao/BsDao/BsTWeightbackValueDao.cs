
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
    [Bean(typeof(TWeightbackValue))]
    public partial interface TWeightbackValueDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TWeightbackValueCB cb);
        IList<TWeightbackValue> SelectList(TWeightbackValueCB cb);

        int Insert(TWeightbackValue entity);
        int UpdateModifiedOnly(TWeightbackValue entity);
        int UpdateNonstrictModifiedOnly(TWeightbackValue entity);
        int UpdateByQuery(TWeightbackValueCB cb, TWeightbackValue entity);
        int Delete(TWeightbackValue entity);
        int DeleteNonstrict(TWeightbackValue entity);
        int DeleteByQuery(TWeightbackValueCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
