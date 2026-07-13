
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
    [Bean(typeof(TReportset))]
    public partial interface TReportsetDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TReportsetCB cb);
        IList<TReportset> SelectList(TReportsetCB cb);

        int Insert(TReportset entity);
        int UpdateModifiedOnly(TReportset entity);
        int UpdateNonstrictModifiedOnly(TReportset entity);
        int UpdateByQuery(TReportsetCB cb, TReportset entity);
        int Delete(TReportset entity);
        int DeleteNonstrict(TReportset entity);
        int DeleteByQuery(TReportsetCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
