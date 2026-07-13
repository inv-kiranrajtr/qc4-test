
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
    [Bean(typeof(TOutputSettingReport))]
    public partial interface TOutputSettingReportDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSettingReportCB cb);
        IList<TOutputSettingReport> SelectList(TOutputSettingReportCB cb);

        int Insert(TOutputSettingReport entity);
        int UpdateModifiedOnly(TOutputSettingReport entity);
        int UpdateNonstrictModifiedOnly(TOutputSettingReport entity);
        int UpdateByQuery(TOutputSettingReportCB cb, TOutputSettingReport entity);
        int Delete(TOutputSettingReport entity);
        int DeleteNonstrict(TOutputSettingReport entity);
        int DeleteByQuery(TOutputSettingReportCB cb);// {DBFlute-0.7.9}
    }
}
