
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
    [Bean(typeof(TOutputHistoryItem))]
    public partial interface TOutputHistoryItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TOutputHistoryItemCB cb);
        IList<TOutputHistoryItem> SelectList(TOutputHistoryItemCB cb);

        int Insert(TOutputHistoryItem entity);
        int UpdateModifiedOnly(TOutputHistoryItem entity);
        int UpdateNonstrictModifiedOnly(TOutputHistoryItem entity);
        int UpdateByQuery(TOutputHistoryItemCB cb, TOutputHistoryItem entity);
        int Delete(TOutputHistoryItem entity);
        int DeleteNonstrict(TOutputHistoryItem entity);
        int DeleteByQuery(TOutputHistoryItemCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
