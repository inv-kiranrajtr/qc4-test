
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
    [Bean(typeof(TDefaultEnvColorDtlC))]
    public partial interface TDefaultEnvColorDtlCDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDefaultEnvColorDtlCCB cb);
        IList<TDefaultEnvColorDtlC> SelectList(TDefaultEnvColorDtlCCB cb);

        int Insert(TDefaultEnvColorDtlC entity);
        int UpdateModifiedOnly(TDefaultEnvColorDtlC entity);
        int UpdateNonstrictModifiedOnly(TDefaultEnvColorDtlC entity);
        int UpdateByQuery(TDefaultEnvColorDtlCCB cb, TDefaultEnvColorDtlC entity);
        int Delete(TDefaultEnvColorDtlC entity);
        int DeleteNonstrict(TDefaultEnvColorDtlC entity);
        int DeleteByQuery(TDefaultEnvColorDtlCCB cb);// {DBFlute-0.7.9}

        int? SelectNextVal();
    }
}
