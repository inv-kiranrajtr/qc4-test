
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
    [Bean(typeof(TOutputSettingGt))]
    public partial interface TOutputSettingGtDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSettingGtCB cb);
        IList<TOutputSettingGt> SelectList(TOutputSettingGtCB cb);

        int Insert(TOutputSettingGt entity);
        int UpdateModifiedOnly(TOutputSettingGt entity);
        int UpdateNonstrictModifiedOnly(TOutputSettingGt entity);
        int UpdateByQuery(TOutputSettingGtCB cb, TOutputSettingGt entity);
        int Delete(TOutputSettingGt entity);
        int DeleteNonstrict(TOutputSettingGt entity);
        int DeleteByQuery(TOutputSettingGtCB cb);// {DBFlute-0.7.9}
    }
}
