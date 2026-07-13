
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
    [Bean(typeof(TOutputCommon))]
    public partial interface TOutputCommonDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputCommonCB cb);
        IList<TOutputCommon> SelectList(TOutputCommonCB cb);

        int Insert(TOutputCommon entity);
        int UpdateModifiedOnly(TOutputCommon entity);
        int UpdateNonstrictModifiedOnly(TOutputCommon entity);
        int UpdateByQuery(TOutputCommonCB cb, TOutputCommon entity);
        int Delete(TOutputCommon entity);
        int DeleteNonstrict(TOutputCommon entity);
        int DeleteByQuery(TOutputCommonCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
