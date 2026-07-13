
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
    [Bean(typeof(TTableControl))]
    public partial interface TTableControlDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TTableControlCB cb);
        IList<TTableControl> SelectList(TTableControlCB cb);

        int Insert(TTableControl entity);
        int UpdateModifiedOnly(TTableControl entity);
        int UpdateNonstrictModifiedOnly(TTableControl entity);
        int UpdateByQuery(TTableControlCB cb, TTableControl entity);
        int Delete(TTableControl entity);
        int DeleteNonstrict(TTableControl entity);
        int DeleteByQuery(TTableControlCB cb);// {DBFlute-0.7.9}
    }
}
