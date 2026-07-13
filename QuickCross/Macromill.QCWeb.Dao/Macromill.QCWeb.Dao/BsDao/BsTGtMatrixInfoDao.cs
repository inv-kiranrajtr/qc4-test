
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
    [Bean(typeof(TGtMatrixInfo))]
    public partial interface TGtMatrixInfoDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TGtMatrixInfoCB cb);
        IList<TGtMatrixInfo> SelectList(TGtMatrixInfoCB cb);

        int Insert(TGtMatrixInfo entity);
        int UpdateModifiedOnly(TGtMatrixInfo entity);
        int UpdateNonstrictModifiedOnly(TGtMatrixInfo entity);
        int UpdateByQuery(TGtMatrixInfoCB cb, TGtMatrixInfo entity);
        int Delete(TGtMatrixInfo entity);
        int DeleteNonstrict(TGtMatrixInfo entity);
        int DeleteByQuery(TGtMatrixInfoCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
