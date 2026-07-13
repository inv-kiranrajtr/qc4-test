
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
    [Bean(typeof(TDataProcessNewItem))]
    public partial interface TDataProcessNewItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDataProcessNewItemCB cb);
        IList<TDataProcessNewItem> SelectList(TDataProcessNewItemCB cb);

        int Insert(TDataProcessNewItem entity);
        int UpdateModifiedOnly(TDataProcessNewItem entity);
        int UpdateNonstrictModifiedOnly(TDataProcessNewItem entity);
        int UpdateByQuery(TDataProcessNewItemCB cb, TDataProcessNewItem entity);
        int Delete(TDataProcessNewItem entity);
        int DeleteNonstrict(TDataProcessNewItem entity);
        int DeleteByQuery(TDataProcessNewItemCB cb);// {DBFlute-0.7.9}
    }
}
