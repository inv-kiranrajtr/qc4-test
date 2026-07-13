
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
    [Bean(typeof(TRawdataDeleteQue))]
    public partial interface TRawdataDeleteQueDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TRawdataDeleteQueCB cb);
        IList<TRawdataDeleteQue> SelectList(TRawdataDeleteQueCB cb);

        int Insert(TRawdataDeleteQue entity);
        int UpdateModifiedOnly(TRawdataDeleteQue entity);
        int UpdateNonstrictModifiedOnly(TRawdataDeleteQue entity);
        int UpdateByQuery(TRawdataDeleteQueCB cb, TRawdataDeleteQue entity);
        int Delete(TRawdataDeleteQue entity);
        int DeleteNonstrict(TRawdataDeleteQue entity);
        int DeleteByQuery(TRawdataDeleteQueCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
