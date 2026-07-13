
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
    [Bean(typeof(TDeleteSampleIdList))]
    public partial interface TDeleteSampleIdListDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDeleteSampleIdListCB cb);
        IList<TDeleteSampleIdList> SelectList(TDeleteSampleIdListCB cb);

        int Insert(TDeleteSampleIdList entity);
        int UpdateModifiedOnly(TDeleteSampleIdList entity);
        int UpdateNonstrictModifiedOnly(TDeleteSampleIdList entity);
        int UpdateByQuery(TDeleteSampleIdListCB cb, TDeleteSampleIdList entity);
        int Delete(TDeleteSampleIdList entity);
        int DeleteNonstrict(TDeleteSampleIdList entity);
        int DeleteByQuery(TDeleteSampleIdListCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
