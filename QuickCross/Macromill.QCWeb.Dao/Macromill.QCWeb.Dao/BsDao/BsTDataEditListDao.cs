
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
    [Bean(typeof(TDataEditList))]
    public partial interface TDataEditListDao : DaoWritable {
		void InitializeDaoMetaData(String methodName);// Very Internal Method!

        int SelectCount(TDataEditListCB cb);
        IList<TDataEditList> SelectList(TDataEditListCB cb);

        int Insert(TDataEditList entity);
        int UpdateModifiedOnly(TDataEditList entity);
        int UpdateNonstrictModifiedOnly(TDataEditList entity);
        int UpdateByQuery(TDataEditListCB cb, TDataEditList entity);
        int Delete(TDataEditList entity);
        int DeleteNonstrict(TDataEditList entity);
        int DeleteByQuery(TDataEditListCB cb);// {DBFlute-0.7.9}

        decimal? SelectNextVal();
    }
}
