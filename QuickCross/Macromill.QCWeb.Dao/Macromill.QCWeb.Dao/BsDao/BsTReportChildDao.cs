
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
    [Bean(typeof(TReportChild))]
    public partial interface TReportChildDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TReportChildCB cb);
        IList<TReportChild> SelectList(TReportChildCB cb);

        int Insert(TReportChild entity);
        int UpdateModifiedOnly(TReportChild entity);
        int UpdateNonstrictModifiedOnly(TReportChild entity);
        int UpdateByQuery(TReportChildCB cb, TReportChild entity);
        int Delete(TReportChild entity);
        int DeleteNonstrict(TReportChild entity);
        int DeleteByQuery(TReportChildCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
