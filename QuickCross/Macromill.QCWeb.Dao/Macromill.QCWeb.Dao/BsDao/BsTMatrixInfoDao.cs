
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
    [Bean(typeof(TMatrixInfo))]
    public partial interface TMatrixInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TMatrixInfoCB cb);
        IList<TMatrixInfo> SelectList(TMatrixInfoCB cb);

        int Insert(TMatrixInfo entity);
        int UpdateModifiedOnly(TMatrixInfo entity);
        int UpdateNonstrictModifiedOnly(TMatrixInfo entity);
        int UpdateByQuery(TMatrixInfoCB cb, TMatrixInfo entity);
        int Delete(TMatrixInfo entity);
        int DeleteNonstrict(TMatrixInfo entity);
        int DeleteByQuery(TMatrixInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
