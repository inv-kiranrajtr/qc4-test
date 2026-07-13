
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
    [Bean(typeof(TAllocationCellInfo))]
    public partial interface TAllocationCellInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TAllocationCellInfoCB cb);
        IList<TAllocationCellInfo> SelectList(TAllocationCellInfoCB cb);

        int Insert(TAllocationCellInfo entity);
        int UpdateModifiedOnly(TAllocationCellInfo entity);
        int UpdateNonstrictModifiedOnly(TAllocationCellInfo entity);
        int Delete(TAllocationCellInfo entity);
        int DeleteNonstrict(TAllocationCellInfo entity);
    }
}
