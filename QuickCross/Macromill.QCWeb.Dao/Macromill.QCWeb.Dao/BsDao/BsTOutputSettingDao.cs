
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
    [Bean(typeof(TOutputSetting))]
    public partial interface TOutputSettingDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSettingCB cb);
        IList<TOutputSetting> SelectList(TOutputSettingCB cb);

        int Insert(TOutputSetting entity);
        int UpdateModifiedOnly(TOutputSetting entity);
        int UpdateNonstrictModifiedOnly(TOutputSetting entity);
        int UpdateByQuery(TOutputSettingCB cb, TOutputSetting entity);
        int Delete(TOutputSetting entity);
        int DeleteNonstrict(TOutputSetting entity);
        int DeleteByQuery(TOutputSettingCB cb);// {DBFlute-0.7.9}
    }
}
