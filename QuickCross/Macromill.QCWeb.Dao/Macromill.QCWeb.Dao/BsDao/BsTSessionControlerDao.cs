
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
    [Bean(typeof(TSessionControler))]
    public partial interface TSessionControlerDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TSessionControlerCB cb);
        IList<TSessionControler> SelectList(TSessionControlerCB cb);

        int Insert(TSessionControler entity);
        int UpdateModifiedOnly(TSessionControler entity);
        int UpdateNonstrictModifiedOnly(TSessionControler entity);
        int Delete(TSessionControler entity);
        int DeleteNonstrict(TSessionControler entity);
    }
}
