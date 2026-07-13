
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
    [Bean(typeof(TSelectConditionInfo))]
    public partial interface TSelectConditionInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TSelectConditionInfoCB cb);
        IList<TSelectConditionInfo> SelectList(TSelectConditionInfoCB cb);

        int Insert(TSelectConditionInfo entity);
        int UpdateModifiedOnly(TSelectConditionInfo entity);
        int UpdateNonstrictModifiedOnly(TSelectConditionInfo entity);
        int Delete(TSelectConditionInfo entity);
        int DeleteNonstrict(TSelectConditionInfo entity);
    }
}
