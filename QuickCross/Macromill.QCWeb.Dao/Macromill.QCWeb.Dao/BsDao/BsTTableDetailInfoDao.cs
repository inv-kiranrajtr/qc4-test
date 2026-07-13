
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
    [Bean(typeof(TTableDetailInfo))]
    public partial interface TTableDetailInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TTableDetailInfoCB cb);
        IList<TTableDetailInfo> SelectList(TTableDetailInfoCB cb);

        int Insert(TTableDetailInfo entity);
        int UpdateModifiedOnly(TTableDetailInfo entity);
        int UpdateNonstrictModifiedOnly(TTableDetailInfo entity);
        int Delete(TTableDetailInfo entity);
        int DeleteNonstrict(TTableDetailInfo entity);
    }
}
