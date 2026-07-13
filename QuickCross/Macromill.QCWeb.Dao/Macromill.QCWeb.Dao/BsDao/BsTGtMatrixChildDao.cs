
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
    [Bean(typeof(TGtMatrixChild))]
    public partial interface TGtMatrixChildDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TGtMatrixChildCB cb);
        IList<TGtMatrixChild> SelectList(TGtMatrixChildCB cb);

        int Insert(TGtMatrixChild entity);
        int UpdateModifiedOnly(TGtMatrixChild entity);
        int UpdateNonstrictModifiedOnly(TGtMatrixChild entity);
        int UpdateByQuery(TGtMatrixChildCB cb, TGtMatrixChild entity);
        int Delete(TGtMatrixChild entity);
        int DeleteNonstrict(TGtMatrixChild entity);
        int DeleteByQuery(TGtMatrixChildCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
