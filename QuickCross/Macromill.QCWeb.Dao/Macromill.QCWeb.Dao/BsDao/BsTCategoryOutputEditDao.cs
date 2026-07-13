
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
    [Bean(typeof(TCategoryOutputEdit))]
    public partial interface TCategoryOutputEditDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TCategoryOutputEditCB cb);
        IList<TCategoryOutputEdit> SelectList(TCategoryOutputEditCB cb);

        int Insert(TCategoryOutputEdit entity);
        int UpdateModifiedOnly(TCategoryOutputEdit entity);
        int UpdateNonstrictModifiedOnly(TCategoryOutputEdit entity);
        int UpdateByQuery(TCategoryOutputEditCB cb, TCategoryOutputEdit entity);
        int Delete(TCategoryOutputEdit entity);
        int DeleteNonstrict(TCategoryOutputEdit entity);
        int DeleteByQuery(TCategoryOutputEditCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
