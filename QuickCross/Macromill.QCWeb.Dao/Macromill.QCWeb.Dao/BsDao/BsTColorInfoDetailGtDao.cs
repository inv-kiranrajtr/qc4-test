
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
    [Bean(typeof(TColorInfoDetailGt))]
    public partial interface TColorInfoDetailGtDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TColorInfoDetailGtCB cb);
        IList<TColorInfoDetailGt> SelectList(TColorInfoDetailGtCB cb);

        int Insert(TColorInfoDetailGt entity);
        int UpdateModifiedOnly(TColorInfoDetailGt entity);
        int UpdateNonstrictModifiedOnly(TColorInfoDetailGt entity);
        int UpdateByQuery(TColorInfoDetailGtCB cb, TColorInfoDetailGt entity);
        int Delete(TColorInfoDetailGt entity);
        int DeleteNonstrict(TColorInfoDetailGt entity);
        int DeleteByQuery(TColorInfoDetailGtCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
