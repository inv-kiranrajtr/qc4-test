
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
    [Bean(typeof(TColorSetInfoGt))]
    public partial interface TColorSetInfoGtDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TColorSetInfoGtCB cb);
        IList<TColorSetInfoGt> SelectList(TColorSetInfoGtCB cb);

        int Insert(TColorSetInfoGt entity);
        int UpdateModifiedOnly(TColorSetInfoGt entity);
        int UpdateNonstrictModifiedOnly(TColorSetInfoGt entity);
        int UpdateByQuery(TColorSetInfoGtCB cb, TColorSetInfoGt entity);
        int Delete(TColorSetInfoGt entity);
        int DeleteNonstrict(TColorSetInfoGt entity);
        int DeleteByQuery(TColorSetInfoGtCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
