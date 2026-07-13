
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
    [Bean(typeof(TReport))]
    public partial interface TReportDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TReportCB cb);
        IList<TReport> SelectList(TReportCB cb);

        int Insert(TReport entity);
        int UpdateModifiedOnly(TReport entity);
        int UpdateNonstrictModifiedOnly(TReport entity);
        int UpdateByQuery(TReportCB cb, TReport entity);
        int Delete(TReport entity);
        int DeleteNonstrict(TReport entity);
        int DeleteByQuery(TReportCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
