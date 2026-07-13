
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
    [Bean(typeof(TApplication))]
    public partial interface TApplicationDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TApplicationCB cb);
        IList<TApplication> SelectList(TApplicationCB cb);

        int Insert(TApplication entity);
        int UpdateModifiedOnly(TApplication entity);
        int UpdateNonstrictModifiedOnly(TApplication entity);
        int UpdateByQuery(TApplicationCB cb, TApplication entity);
        int Delete(TApplication entity);
        int DeleteNonstrict(TApplication entity);
        int DeleteByQuery(TApplicationCB cb);// {DBFlute-0.7.9}
    }
}
