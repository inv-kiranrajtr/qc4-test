
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
    [Bean(typeof(TOutputSettingFa))]
    public partial interface TOutputSettingFaDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputSettingFaCB cb);
        IList<TOutputSettingFa> SelectList(TOutputSettingFaCB cb);

        int Insert(TOutputSettingFa entity);
        int UpdateModifiedOnly(TOutputSettingFa entity);
        int UpdateNonstrictModifiedOnly(TOutputSettingFa entity);
        int UpdateByQuery(TOutputSettingFaCB cb, TOutputSettingFa entity);
        int Delete(TOutputSettingFa entity);
        int DeleteNonstrict(TOutputSettingFa entity);
        int DeleteByQuery(TOutputSettingFaCB cb);// {DBFlute-0.7.9}
    }
}
