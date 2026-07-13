
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
    [Bean(typeof(TOutputSubCross))]
    public partial interface TOutputSubCrossDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSubCrossCB cb);
        IList<TOutputSubCross> SelectList(TOutputSubCrossCB cb);

        int Insert(TOutputSubCross entity);
        int UpdateModifiedOnly(TOutputSubCross entity);
        int UpdateNonstrictModifiedOnly(TOutputSubCross entity);
        int UpdateByQuery(TOutputSubCrossCB cb, TOutputSubCross entity);
        int Delete(TOutputSubCross entity);
        int DeleteNonstrict(TOutputSubCross entity);
        int DeleteByQuery(TOutputSubCrossCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
