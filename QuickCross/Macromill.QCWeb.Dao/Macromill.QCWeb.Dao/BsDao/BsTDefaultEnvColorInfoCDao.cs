
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
    [Bean(typeof(TDefaultEnvColorInfoC))]
    public partial interface TDefaultEnvColorInfoCDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDefaultEnvColorInfoCCB cb);
        IList<TDefaultEnvColorInfoC> SelectList(TDefaultEnvColorInfoCCB cb);

        int Insert(TDefaultEnvColorInfoC entity);
        int UpdateModifiedOnly(TDefaultEnvColorInfoC entity);
        int UpdateNonstrictModifiedOnly(TDefaultEnvColorInfoC entity);
        int UpdateByQuery(TDefaultEnvColorInfoCCB cb, TDefaultEnvColorInfoC entity);
        int Delete(TDefaultEnvColorInfoC entity);
        int DeleteNonstrict(TDefaultEnvColorInfoC entity);
        int DeleteByQuery(TDefaultEnvColorInfoCCB cb);// {DBFlute-0.7.9}

        int? SelectNextVal();
    }
}
