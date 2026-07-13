
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
    [Bean(typeof(TFaListAddItem))]
    public partial interface TFaListAddItemDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TFaListAddItemCB cb);
        IList<TFaListAddItem> SelectList(TFaListAddItemCB cb);

        int Insert(TFaListAddItem entity);
        int UpdateModifiedOnly(TFaListAddItem entity);
        int UpdateNonstrictModifiedOnly(TFaListAddItem entity);
        int UpdateByQuery(TFaListAddItemCB cb, TFaListAddItem entity);
        int Delete(TFaListAddItem entity);
        int DeleteNonstrict(TFaListAddItem entity);
        int DeleteByQuery(TFaListAddItemCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
