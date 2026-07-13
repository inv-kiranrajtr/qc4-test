
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
    [Bean(typeof(TDataProcessNewItemSrc))]
    public partial interface TDataProcessNewItemSrcDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDataProcessNewItemSrcCB cb);
        IList<TDataProcessNewItemSrc> SelectList(TDataProcessNewItemSrcCB cb);

        int Insert(TDataProcessNewItemSrc entity);
        int UpdateModifiedOnly(TDataProcessNewItemSrc entity);
        int UpdateNonstrictModifiedOnly(TDataProcessNewItemSrc entity);
        int UpdateByQuery(TDataProcessNewItemSrcCB cb, TDataProcessNewItemSrc entity);
        int Delete(TDataProcessNewItemSrc entity);
        int DeleteNonstrict(TDataProcessNewItemSrc entity);
        int DeleteByQuery(TDataProcessNewItemSrcCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
