
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
    [Bean(typeof(TFaData))]
    public partial interface TFaDataDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TFaDataCB cb);
        IList<TFaData> SelectList(TFaDataCB cb);

        int Insert(TFaData entity);
        int UpdateModifiedOnly(TFaData entity);
        int UpdateNonstrictModifiedOnly(TFaData entity);
        int UpdateByQuery(TFaDataCB cb, TFaData entity);
        int Delete(TFaData entity);
        int DeleteNonstrict(TFaData entity);
        int DeleteByQuery(TFaDataCB cb);// {DBFlute-0.7.9}
    }
}
