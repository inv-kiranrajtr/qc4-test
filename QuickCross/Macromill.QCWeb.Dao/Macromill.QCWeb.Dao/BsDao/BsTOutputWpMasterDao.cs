
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
    [Bean(typeof(TOutputWpMaster))]
    public partial interface TOutputWpMasterDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputWpMasterCB cb);
        IList<TOutputWpMaster> SelectList(TOutputWpMasterCB cb);

        int Insert(TOutputWpMaster entity);
        int UpdateModifiedOnly(TOutputWpMaster entity);
        int UpdateNonstrictModifiedOnly(TOutputWpMaster entity);
        int UpdateByQuery(TOutputWpMasterCB cb, TOutputWpMaster entity);
        int Delete(TOutputWpMaster entity);
        int DeleteNonstrict(TOutputWpMaster entity);
        int DeleteByQuery(TOutputWpMasterCB cb);// {DBFlute-0.7.9}
    }
}
