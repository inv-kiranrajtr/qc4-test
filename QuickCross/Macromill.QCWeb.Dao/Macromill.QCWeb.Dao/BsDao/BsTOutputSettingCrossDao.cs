
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
    [Bean(typeof(TOutputSettingCross))]
    public partial interface TOutputSettingCrossDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSettingCrossCB cb);
        IList<TOutputSettingCross> SelectList(TOutputSettingCrossCB cb);

        int Insert(TOutputSettingCross entity);
        int UpdateModifiedOnly(TOutputSettingCross entity);
        int UpdateNonstrictModifiedOnly(TOutputSettingCross entity);
        int UpdateByQuery(TOutputSettingCrossCB cb, TOutputSettingCross entity);
        int Delete(TOutputSettingCross entity);
        int DeleteNonstrict(TOutputSettingCross entity);
        int DeleteByQuery(TOutputSettingCrossCB cb);// {DBFlute-0.7.9}
    }
}
