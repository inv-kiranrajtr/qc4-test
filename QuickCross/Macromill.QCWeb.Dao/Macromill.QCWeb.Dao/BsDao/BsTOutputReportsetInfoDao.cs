
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
    [Bean(typeof(TOutputReportsetInfo))]
    public partial interface TOutputReportsetInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputReportsetInfoCB cb);
        IList<TOutputReportsetInfo> SelectList(TOutputReportsetInfoCB cb);

        int Insert(TOutputReportsetInfo entity);
        int UpdateModifiedOnly(TOutputReportsetInfo entity);
        int UpdateNonstrictModifiedOnly(TOutputReportsetInfo entity);
        int UpdateByQuery(TOutputReportsetInfoCB cb, TOutputReportsetInfo entity);
        int Delete(TOutputReportsetInfo entity);
        int DeleteNonstrict(TOutputReportsetInfo entity);
        int DeleteByQuery(TOutputReportsetInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
