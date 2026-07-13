
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
    [Bean(typeof(TCategoryOutputDetail))]
    public partial interface TCategoryOutputDetailDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TCategoryOutputDetailCB cb);
        IList<TCategoryOutputDetail> SelectList(TCategoryOutputDetailCB cb);

        int Insert(TCategoryOutputDetail entity);
        int UpdateModifiedOnly(TCategoryOutputDetail entity);
        int UpdateNonstrictModifiedOnly(TCategoryOutputDetail entity);
        int UpdateByQuery(TCategoryOutputDetailCB cb, TCategoryOutputDetail entity);
        int Delete(TCategoryOutputDetail entity);
        int DeleteNonstrict(TCategoryOutputDetail entity);
        int DeleteByQuery(TCategoryOutputDetailCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
