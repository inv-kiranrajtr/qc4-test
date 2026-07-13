
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
    [Bean(typeof(TNotice))]
    public partial interface TNoticeDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TNoticeCB cb);
        IList<TNotice> SelectList(TNoticeCB cb);

        int Insert(TNotice entity);
        int UpdateModifiedOnly(TNotice entity);
        int UpdateNonstrictModifiedOnly(TNotice entity);
        int UpdateByQuery(TNoticeCB cb, TNotice entity);
        int Delete(TNotice entity);
        int DeleteNonstrict(TNotice entity);
        int DeleteByQuery(TNoticeCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
