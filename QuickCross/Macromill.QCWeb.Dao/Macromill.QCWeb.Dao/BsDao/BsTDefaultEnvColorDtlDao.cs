
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
    [Bean(typeof(TDefaultEnvColorDtl))]
    public partial interface TDefaultEnvColorDtlDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDefaultEnvColorDtlCB cb);
        IList<TDefaultEnvColorDtl> SelectList(TDefaultEnvColorDtlCB cb);

        int Insert(TDefaultEnvColorDtl entity);
        int UpdateModifiedOnly(TDefaultEnvColorDtl entity);
        int UpdateNonstrictModifiedOnly(TDefaultEnvColorDtl entity);
        int UpdateByQuery(TDefaultEnvColorDtlCB cb, TDefaultEnvColorDtl entity);
        int Delete(TDefaultEnvColorDtl entity);
        int DeleteNonstrict(TDefaultEnvColorDtl entity);
        int DeleteByQuery(TDefaultEnvColorDtlCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
